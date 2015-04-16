using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Utils
{
    public class AutoTypeInspector<TInstance>
        where TInstance : class
    {

        private Lazy<Type> _type = new Lazy<Type>(() => typeof(TInstance));

        private interface IPropertyDefinition
        {
            string Name { get; set; }
        }

        private class PropertyDefinition<TValue> : IPropertyDefinition
        {
            public string Name { get; set; }
            public Func<TInstance, TValue> Getter { get; internal set; }
            public Action<TInstance, TValue> Setter { get; internal set; }
            public Func<TValue, TValue, bool> EqualTo { get; internal set; }
        }

        private Lazy<Dictionary<string, IPropertyDefinition>> _properties;

        private Dictionary<string, IPropertyDefinition> InspectType()
        {
            var definitions = new Dictionary<string, IPropertyDefinition>();

            var properties = Type.GetProperties();            
            var inspectPropertyBase = 
                this.GetType().GetMethod("InspectProperty", 
                BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.GetProperty | BindingFlags.SetProperty);
            foreach (var property in properties)
            {
                var inspectPropertySpecialized = inspectPropertyBase.MakeGenericMethod(property.PropertyType);
                definitions.Add(property.Name, (IPropertyDefinition) 
                    inspectPropertySpecialized.Invoke(this, new object[]{property}));
            }

            return definitions;
        }

        private IPropertyDefinition InspectProperty<TValue>(PropertyInfo propertyInfo)
        {
            var definition = new PropertyDefinition<TValue>
            {
                Name = propertyInfo.Name,
                Getter = obj => (TValue) propertyInfo.GetGetMethod().Invoke(obj, null),
                Setter = (obj,val) => propertyInfo.GetSetMethod().Invoke(obj, new object[]{val}),
                EqualTo = null
            };
            return definition;
        }
        

        public AutoTypeInspector()
        {
            _properties = new Lazy<Dictionary<string, IPropertyDefinition>>(InspectType);
        }

        public Type Type { get { return _type.Value; } }

        private Dictionary<string, IPropertyDefinition> Properties
        {
            get
            {
                return _properties.Value;
            }
        }

        public AutoTypeInspector<TInstance> RegisterProperty<TValue>(string name,
            Func<TInstance, TValue> get, Action<TInstance, TValue> set, Func<TValue, TValue, bool> equals = null)
        {
            var definition = new PropertyDefinition<TValue>
            {
                Name = name,
                Getter = get,
                Setter = set,
                EqualTo = equals
            };
            Properties[name] = definition;
            return this;
        }

        public IEnumerable<Func<TInstance, object>> Getters
        {
            get
            {
                return Properties.Select(kvp => ((PropertyDefinition<object>)kvp.Value).Getter).ToList();
            }
        }

        public IEnumerable<Action<TInstance, object>> Setters
        {
            get
            {
                return Properties.Select(kvp => ((PropertyDefinition<object>)kvp.Value).Setter).ToList();
            }
        }

        public Func<TInstance, TValue> GetGetter<TValue>(string name)
        {
            IPropertyDefinition iDefinition;
            Properties.TryGetValue(name, out iDefinition);

            var definition = iDefinition as PropertyDefinition<TValue>;
            if (definition == null)
            {
                throw new AutoTypeInspectorException(String.Format("No getter found named {0}", name));
            }

            return definition.Getter;
        }



    }
}

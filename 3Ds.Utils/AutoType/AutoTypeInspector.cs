using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace _3Ds.Utils.AutoType
{
    public class AutoTypeInspector<TInstance>
        where TInstance : class
    {

        private Lazy<Type> _type = new Lazy<Type>(() => typeof(TInstance));
        private Lazy<InspectionResult> _inspectionResult;

        private InspectionResult InspectType()
        {
            var result = new InspectionResult();
            result.TypeName = Type.Name;
            var properties = Type.GetProperties();
            foreach (var property in properties)
            {
                result[property.Name] = InspectProperty(property);
            }
            return result;
        }

        private PropertyDefinition<TInstance> InspectProperty(PropertyInfo propertyInfo)
        {
            var definition = new PropertyDefinition<TInstance>
            {
                Name = propertyInfo.Name,
                ValueType = propertyInfo.PropertyType,
                Getter = obj => propertyInfo.GetGetMethod().Invoke(obj, null),
                Setter = (obj, val) => propertyInfo.GetSetMethod().Invoke(obj, new object[] { val }),
                EqualTo = null
            };
            return definition;
        }

        private InspectionResult Result
        {
            get
            {
                return _inspectionResult.Value;
            }
        }

        public AutoTypeInspector()
        {
            _inspectionResult = new Lazy<InspectionResult>(InspectType);
        }

        public Type Type { get { return _type.Value; } }

        public TInstance CreateInstance(params object[] args)
        {
            return (TInstance)Activator.CreateInstance(typeof(TInstance), args);
        }

        public string TypeName { get { return Result.TypeName; } }

        public List<PropertyDefinition<TInstance>> AllProperties
        {
            get
            {
                return Result.AllProperties;
            }
        }

        public bool HasProperty(string name)
        {
            return Result.AllProperties.Any(p => p.Name == name);
        }

        public AutoTypeInspector<TInstance> OverrideProperty<TValue>(PropertyDefinition<TInstance> definition)
        {
            Result[definition.Name] = definition;
            return this;
        }

        public IEnumerable<Func<TInstance, object>> Getters
        {
            get
            {
                return Result.AllProperties
                    .Select(p => p.Getter).ToList();
            }
        }

        public IEnumerable<Action<TInstance, object>> Setters
        {
            get
            {
                return Result.AllProperties
                    .Select(p => p.Setter).ToList();
            }
        }

        public Func<TInstance, object> Getter(string name)
        {
            PropertyDefinition<TInstance> definition = Result[name];
            return definition.Getter;
        }

        public Action<TInstance, object> Setter(string name)
        {
            PropertyDefinition<TInstance> definition = Result[name];
            return definition.Setter;
        }

        private class InspectionResult
        {
            private Dictionary<string, PropertyDefinition<TInstance>> _properties = 
                new Dictionary<string,PropertyDefinition<TInstance>>();

            public string TypeName { get; set; }
            public string FullyQualifiedTypeName { get; set; }
            public Guid Guid { get; set; }

            public List<PropertyDefinition<TInstance>> AllProperties
            {
                get
                {
                    return _properties.Values.ToList();
                }
            }

            public PropertyDefinition<TInstance> this[string propertyName]
            {
                get
                {
                    PropertyDefinition<TInstance> definition;
                    _properties.TryGetValue(propertyName, out definition);
                    if (definition == null)
                    {
                        throw new AutoTypeInspectorException(String.Format("No property found named {0}", propertyName));
                    }
                    return definition;
                }

                set { _properties[propertyName] = value; }
            }

        }

    }
}

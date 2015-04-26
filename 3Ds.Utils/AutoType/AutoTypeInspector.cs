﻿using System;
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

        private Lazy<Dictionary<string, PropertyDefinition<TInstance>>> _properties;

        private Dictionary<string, PropertyDefinition<TInstance>> InspectType()
        {
            var definitions = new Dictionary<string, PropertyDefinition<TInstance>>();
            var properties = Type.GetProperties();                   
            foreach (var property in properties)
            {                
                definitions.Add(property.Name, InspectProperty(property));
            }

            return definitions;
        }

        private PropertyDefinition<TInstance> InspectProperty(PropertyInfo propertyInfo)
        {
            var definition = new PropertyDefinition<TInstance>
            {
                Name = propertyInfo.Name,
                ValueType = propertyInfo.PropertyType,
                Getter = obj => propertyInfo.GetGetMethod().Invoke(obj, null),
                Setter = (obj,val) => propertyInfo.GetSetMethod().Invoke(obj, new object[]{val}),
                EqualTo = null
            };
            return definition;
        }
        

        public AutoTypeInspector()
        {
            _properties = new Lazy<Dictionary<string, PropertyDefinition<TInstance>>>(InspectType);
        }

        public Type Type { get { return _type.Value; } }

        public List<PropertyDefinition<TInstance>> PropertyDefinitions
        {
            get
            {
                return _properties.Value.Values.ToList();
            }
        }

        private Dictionary<string, PropertyDefinition<TInstance>> Properties
        {
            get
            {
                return _properties.Value;
            }
        }

        public AutoTypeInspector<TInstance> RegisterProperty<TValue>(string name,
            Func<TInstance, object> get, Action<TInstance, object> set, Func<object, object, bool> equals = null)
        {
            var definition = new PropertyDefinition<TInstance>
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
                return Properties.Select(kvp => ((PropertyDefinition<TInstance>)kvp.Value).Getter).ToList();
            }
        }

        public IEnumerable<Action<TInstance, object>> Setters
        {
            get
            {
                return Properties.Select(kvp => ((PropertyDefinition<TInstance>)kvp.Value).Setter).ToList();
            }
        }

        public Func<TInstance, object> GetGetter(string name)
        {
            PropertyDefinition<TInstance> definition;
            Properties.TryGetValue(name, out definition);            
            if (definition == null)
            {
                throw new AutoTypeInspectorException(String.Format("No getter found named {0}", name));
            }

            return definition.Getter;
        }

        public Action<TInstance, object> GetSetter(string name)
        {
            PropertyDefinition<TInstance> definition;
            Properties.TryGetValue(name, out definition);
            if (definition == null)
            {
                throw new AutoTypeInspectorException(String.Format("No setter found named {0}", name));
            }
            return definition.Setter;
        }

    }
}
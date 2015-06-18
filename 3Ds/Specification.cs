using _3Ds.Utils.AutoType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _3Ds.Core
{
    
    public interface ISpecification
    {
        bool IsSatisfiedBy(IEntity entity);
    }

    public interface ISpecification<TEntity>
    {
        bool IsSatisfiedBy(TEntity entity);
    }

    public interface IConjunction<TEntity>
        where TEntity : class, IEntity
    {
        Specification<TEntity> And();
    }

    public class Specification<TEntity> : ISpecification, ISpecification<TEntity>, IConjunction<TEntity>
        where TEntity : class, IEntity
    {

        private ISpecification _property;
        private ISpecification _conjunction;
        
        public PropertySpecification<TEntity, TProperty> Property<TProperty>(
            Func<TEntity, TProperty> selector)
             where TProperty: IComparable<TProperty>
        {
            _property = new PropertySpecification<TEntity, TProperty>(this, selector);
            return (PropertySpecification<TEntity, TProperty>)_property;
        }

        Specification<TEntity> IConjunction<TEntity>.And()
        {
            Specification<TEntity> other = new Specification<TEntity>();
            _conjunction = new AndSpecification<TEntity>(this, other);
            return other;
        }
        /*
        public Specification<TEntity> And(Specification<TEntity> other)
        {
            _conjunction = new AndSpecification<TEntity>(this, other);
            return this;
        }

        public Specification<TEntity> Or()
        {
            Specification<TEntity> other = new Specification<TEntity>();
            _conjunction = new OrSpecification<TEntity>(this, other);
            return other;
        }

        public Specification<TEntity> Or(Specification<TEntity> other)
        {
            _conjunction = new OrSpecification<TEntity>(this, other);
            return this;
        }

        public Specification<TEntity> Not()
        {
            Specification<TEntity> other = new Specification<TEntity>();
            _conjunction = new NotSpecification<TEntity>(other);
            return other;
        }               

        public bool IsSatisfiedBy(IEntity ientity)
        {
            TEntity entity = ientity as TEntity;
            if (entity == null)
            {
                return false;
            }
            return _property.IsSatisfiedBy(entity);
        }
        */

        public bool IsSatisfiedBy(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public bool IsSatisfiedBy(IEntity entity)
        {
            throw new NotImplementedException();
        }
    }



    public class PropertySpecification<TEntity, TProperty> : ISpecification
        where TEntity : class, IEntity
        where TProperty : IComparable<TProperty>
    {

        private Func<TEntity, TProperty> _selector;        
        private Func<TProperty, bool> _evaluate;
        private Specification<TEntity> _parent;
        
        internal PropertySpecification(Specification<TEntity> parent, 
            Func<TEntity, TProperty> selector)
        {
            _parent = parent;
            _selector = selector;
        }

        public bool IsSatisfiedBy(IEntity ientity)
        {
            TEntity entity = ientity as TEntity;
            if (entity == null)
            {
                return false;         
            }
            var value = _selector(entity);
            return _evaluate(value);
        }

        public IConjunction<TEntity> EqualTo(TProperty other)
        {            
            _evaluate = value => value.CompareTo(other) == 0;
            return _parent;
        }

        public IConjunction<TEntity> NotEqualTo(TProperty other)
        {
            _evaluate = value => value.CompareTo(other) != 0;
            return _parent;
        }

        public IConjunction<TEntity> LessThan(TProperty other)
        {
            _evaluate = value => value.CompareTo(other) < 0;
            return _parent;
        }

        public IConjunction<TEntity> GreaterThan(TProperty other)
        {
            _evaluate = value => value.CompareTo(other) > 0;
            return _parent;
        }
    }


    public class AndSpecification<TEntity> : ISpecification
        where TEntity : class, IEntity
    {
        public Specification<TEntity> Lhs { get; private set; }
        public Specification<TEntity> Rhs { get; private set; }

        public AndSpecification(Specification<TEntity> lhs, Specification<TEntity> rhs)
        {
            Lhs = lhs;
            Rhs = rhs;
        }
       
        public bool IsSatisfiedBy(IEntity ientity)
        {
            TEntity entity = ientity as TEntity;
            if (entity == null)
            {
                return false;
            }
            return Lhs.IsSatisfiedBy(entity) && Rhs.IsSatisfiedBy(entity);
        }
    }

    public class OrSpecification<TEntity> : ISpecification
        where TEntity : class, IEntity
    {
        public Specification<TEntity> Lhs { get; private set; }
        public Specification<TEntity> Rhs { get; private set; }

        public OrSpecification(Specification<TEntity> lhs, Specification<TEntity> rhs)
        {
            Lhs = lhs;
            Rhs = rhs;
        }

        public bool IsSatisfiedBy(IEntity ientity)
        {
            TEntity entity = ientity as TEntity;
            if (entity == null)
            {
                return false;
            }
            return Lhs.IsSatisfiedBy(entity) || Rhs.IsSatisfiedBy(entity);
        }
    }

    public class NotSpecification<TEntity> : ISpecification
        where TEntity : class, IEntity
    {
        public Specification<TEntity> Lhs { get; private set; }

        public NotSpecification(Specification<TEntity> lhs)
        {
            Lhs = lhs;            
        }

        public bool IsSatisfiedBy(IEntity ientity)
        {
            TEntity entity = ientity as TEntity;
            if (entity == null)
            {
                return false;
            }
            return !Lhs.IsSatisfiedBy(entity);
        }
    }

    
}

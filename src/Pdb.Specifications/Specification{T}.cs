namespace Pdb.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    public abstract class Specification<T> : ISpecification<T>
    {
        protected Specification()
            : this(e => true)
        {
        }

        protected Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria ?? throw new ArgumentNullException(nameof(criteria));
        }

        public virtual Expression<Func<T, bool>> Criteria { get; }

        public virtual IEnumerable<Expression<Func<T, object?>>> Includes { get; } = new List<Expression<Func<T, object?>>>();

        public virtual IEnumerable<string> IncludeStrings { get; } = new List<string>();

        public static implicit operator Expression<Func<T, bool>>(Specification<T> specification)
        {
            return specification.Criteria;
        }

        public static bool operator false(Specification<T> specification)
        {
            return false;
        }

        public static bool operator true(Specification<T> specification)
        {
            return false;
        }

        public static Specification<T> operator &(Specification<T> left, Specification<T> right)
        {
            return new AndSpecification<T>(left, right);
        }

        public static Specification<T> operator |(Specification<T> left, Specification<T> right)
        {
            return new OrSpecification<T>(left, right);
        }

        public static Specification<T> operator !(Specification<T> specification)
        {
            return new NotSpecification<T>(specification);
        }

        public bool IsSatisfiedBy(T value)
        {
            if (Criteria == null)
            {
                return false;
            }

            var predicate = Criteria.Compile();
            return predicate(value);
        }

        public bool IsSatisfiedBy(params T[] value)
        {
            if (Criteria == null)
            {
                return false;
            }

            var predicate = Criteria.Compile();
            return value.All(e => predicate(e));
        }

        protected void Include(Expression<Func<T, object?>> navigationExpression)
        {
            ((List<Expression<Func<T, object?>>>)Includes).Add(navigationExpression);
        }

        protected void Include(string navigationString)
        {
            ((List<string>)IncludeStrings).Add(navigationString);
        }

        protected void IncludeAll<TProperty>(Func<IncludeAggregator<T>, IIncludeQuery<T, TProperty>> includeGenerator)
        {
            if (includeGenerator == null)
            {
                throw new ArgumentNullException(nameof(includeGenerator));
            }

            var includeQuery = includeGenerator(new IncludeAggregator<T>());
            ((List<string>)IncludeStrings).AddRange(includeQuery.Paths);
        }
    }
}
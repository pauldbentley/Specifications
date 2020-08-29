namespace Pdb.Specifications
{
    using System;
    using System.Linq.Expressions;

    internal sealed class NotSpecification<T> : Specification<T>
    {
        public NotSpecification(Specification<T> specification)
        {
            Specification = specification ?? throw new ArgumentNullException(nameof(specification));
        }

        public Specification<T> Specification { get; }

        public override Expression<Func<T, bool>> Criteria =>
            Expression.Lambda<Func<T, bool>>(
                Expression.Not(Specification.Criteria.Body),
                Specification.Criteria.Parameters);
    }
}
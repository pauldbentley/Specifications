namespace Pdb.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;

    internal sealed class OrSpecification<T> : Specification<T>
    {
        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            Left = left ?? throw new ArgumentNullException(nameof(left));
            Right = right ?? throw new ArgumentNullException(nameof(right));
        }

        public ISpecification<T> Left { get; }

        public ISpecification<T> Right { get; }

        public override Expression<Func<T, bool>> Criteria =>
            Left.Criteria.OrElse(Right.Criteria);

        public override IEnumerable<Expression<Func<T, object?>>> Includes => Left.Includes.Union(Right.Includes);

        public override IEnumerable<string> IncludeStrings => Left.IncludeStrings.Union(Right.IncludeStrings);
    }
}

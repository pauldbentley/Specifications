namespace Pdb.Specifications
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;

    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }

        IEnumerable<Expression<Func<T, object?>>> Includes { get; }

        IEnumerable<string> IncludeStrings { get; }

        bool IsSatisfiedBy(T value);

        bool IsSatisfiedBy(params T[] value);
    }
}

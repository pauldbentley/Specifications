namespace Pdb.Specifications
{
    using System;
    using System.Linq.Expressions;

    public class AdHocSpecification<T> : Specification<T>
    {
        public AdHocSpecification(Expression<Func<T, bool>> criteria)
            : base(criteria)
        {
        }
    }
}

namespace Pdb.Specifications.Tests.Core.Specifications
{
    using System.Linq;
    using Pdb.Specifications;
    using Pdb.Specifications.Tests.Core;

    public class CustomerNameEndsWithSpecification : Specification<Customer>
    {
        public CustomerNameEndsWithSpecification(params string[] names)
            : base(e => names.Any(name => e.Name.EndsWith(name)))
        {
        }
    }
}

namespace Pdb.Specifications.Tests.Core.Specifications
{
    using Pdb.Specifications;
    using Pdb.Specifications.Tests.Core;

    public class CustomerWithAllLevelsSpecification : Specification<Customer>
    {
        public CustomerWithAllLevelsSpecification()
        {
            IncludeAll((e) => e.Include(e => e.Level1).ThenInclude(e => e.Level2).ThenInclude(e => e.Level3));
        }
    }
}

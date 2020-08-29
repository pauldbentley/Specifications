namespace Pdb.Specifications
{
    public sealed class FalseSpecification<T> : Specification<T>
    {
        public FalseSpecification()
            : base(e => false)
        {
        }
    }
}

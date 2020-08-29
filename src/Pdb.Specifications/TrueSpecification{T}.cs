namespace Pdb.Specifications
{
    public sealed class TrueSpecification<T> : Specification<T>
    {
        public TrueSpecification()
            : base(e => true)
        {
        }
    }
}

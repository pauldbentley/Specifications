namespace Pdb.Specifications
{
    using System.Collections.Generic;

    public interface IIncludeQuery
    {
        internal Dictionary<IIncludeQuery, string> PathMap { get; }

        internal IncludeVisitor Visitor { get; }

        internal HashSet<string> Paths { get; }
    }

    public interface IIncludeQuery<TEntity, out TPreviousProperty> : IIncludeQuery
    {
    }
}
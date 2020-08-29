namespace Pdb.Specifications
{
    using System.Collections.Generic;
    using System.Linq;

    internal sealed class IncludeQuery<TEntity, TPreviousProperty> : IIncludeQuery<TEntity, TPreviousProperty>
    {
        private readonly Dictionary<IIncludeQuery, string> _pathMap;

        public IncludeQuery(Dictionary<IIncludeQuery, string> pathMap)
        {
            _pathMap = pathMap ?? throw new System.ArgumentNullException(nameof(pathMap));
        }

        Dictionary<IIncludeQuery, string> IIncludeQuery.PathMap => _pathMap;

        IncludeVisitor IIncludeQuery.Visitor { get; } = new IncludeVisitor();

        HashSet<string> IIncludeQuery.Paths => _pathMap.Select(x => x.Value).ToHashSet();
    }
}
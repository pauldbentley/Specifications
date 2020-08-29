namespace Pdb.Specifications.Tests.Core
{
    using System.Diagnostics;

    [DebuggerDisplay("{" + nameof(GetDebuggerDisplay) + "(),nq}")]
    public class Customer
    {
        public string Name { get; set; }

        public string Category { get; set; }

        public CustomerLevel1 Level1 { get; set; }

        private string GetDebuggerDisplay() => "{" + Name + "}";
    }
}

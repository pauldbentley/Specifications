namespace Pdb.Specifications.Tests
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Pdb.Specifications.Tests.Core.Specifications;
    using Shouldly;
    using Xunit;

    public class SpecificationTests
    {
        [Fact]
        public void Can_include_nested_properties()
        {
            var specification = new CustomerWithAllLevelsSpecification();

            specification.IncludeStrings.ShouldContain("Level1.Level2.Level3");
        }

        [Fact]
        public void Should_implicitly_convert_to_an_expression()
        {
            var spec = new AdHocSpecification<string>(s => s.Length == 2);
            Expression<Func<string, bool>> expr = spec;

            expr.Compile().Invoke("ab").ShouldBeTrue();
            expr.Compile().Invoke("abcd").ShouldBeFalse();
        }

        [Fact]
        public void Can_be_combined_with_And()
        {
            var startsWithP = new AdHocSpecification<string>(n => n.StartsWith("P"));
            var endsWithL = new AdHocSpecification<string>(n => n.EndsWith("l"));

            var repository = new[]
            {
                "Paul",
                "Mark",
                "Glenn",
                "Phillip",
            }.AsQueryable();

            var result = repository.Where(startsWithP && endsWithL);

            result.ShouldContain("Paul");
            result.ShouldNotContain("Mark");
            result.ShouldNotContain("Glenn");
            result.ShouldNotContain("Phillip");
        }

        [Fact]
        public void Can_be_combined_with_Or()
        {
            var startsWithP = new AdHocSpecification<string>(n => n.StartsWith("P"));
            var endsWithL = new AdHocSpecification<string>(n => n.EndsWith("l"));

            var repository = new[]
            {
                "Paul",
                "Mark",
                "Glenn",
                "Phillip",
            }.AsQueryable();

            var result = repository.Where(startsWithP || endsWithL);

            result.ShouldContain("Paul");
            result.ShouldNotContain("Mark");
            result.ShouldNotContain("Glenn");
            result.ShouldContain("Phillip");
        }

        [Fact]
        public void Can_be_negated()
        {
            var startsWithP = new AdHocSpecification<string>(n => n.StartsWith("P"));

            var repository = new[]
            {
                "Paul",
                "Mark",
                "Glenn",
                "Phillip",
            }.AsQueryable();

            var result = repository.Where(!startsWithP);

            result.ShouldNotContain("Paul");
            result.ShouldContain("Mark");
            result.ShouldContain("Glenn");
            result.ShouldNotContain("Phillip");
        }

        [Fact]
        public void Can_be_combined()
        {
            var startsWithP = new AdHocSpecification<string>(n => n.StartsWith("P"));
            var endsWithN = new AdHocSpecification<string>(n => n.EndsWith("n"));
            var containsL = new AdHocSpecification<string>(n => n.Contains("l"));

            var repository = new[]
            {
                "Paul",
                "Mark",
                "Glenn",
                "Phillip",
            }.AsQueryable();

            var result = repository.Where(startsWithP || (!endsWithN && !containsL));

            result.ShouldContain("Paul");
            result.ShouldContain("Mark");
            result.ShouldNotContain("Glenn");
            result.ShouldContain("Phillip");
        }
    }
}

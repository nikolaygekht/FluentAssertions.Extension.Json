using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace FluentAssertions.Extension.Json.Test
{
    public class TestStringExtension
    {
        [Fact]
        public void AsJsonTest()
        {
            var json = "{ \"a\" : \"b\" }".AsJson();

            json.Should()
                .BeOfType<JsonElement>();

            json.GetProperty("a").Should()
                .NotBeNull()
                .And
                .Match<JsonElement>(x => x.ValueKind == JsonValueKind.String)
                .And
                .Match<JsonElement>(x => x.GetString() == "b");
        }

        [Theory]
        [InlineData("{}")]
        [InlineData("{ \"a\" : \"b\" }")]
        [InlineData("{ \"a\" : [1, 2, 3] }")]
        public void BeCorrectJsonTest_Ok(string json)
        {
            ((Action)(() => json.Should().BeCorrectJson())).Should().NotThrow<XunitException>();
        }

        [Theory]
        [InlineData("{")]
        [InlineData("[ a : \"b\" ]")]
        [InlineData("{ a : \"b\" }")]
        [InlineData("random text")]
        public void BeCorrectJsonTest_Fail(string json)
        {
            ((Action)(() => json.Should().BeCorrectJson())).Should().Throw<XunitException>();
        }
    }
}
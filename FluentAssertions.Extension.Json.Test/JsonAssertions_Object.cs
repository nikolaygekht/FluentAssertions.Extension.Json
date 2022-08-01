using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xunit;
using Xunit.Sdk;

namespace FluentAssertions.Extension.Json.Test
{
    public class JsonAssertions_Object
    {
        [Fact]
        public void BeObject_OK()
        {
            var node = "{}".AsJson();
            ((Action)(() => node.Should().BeObject())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeObject_Fail_Null()
        {
            var node = "{ \"a\" : null }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeObject())).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeObject_Fail_WrongType()
        {
            var node = "{ \"a\" : \"a\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeObject())).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveProperty_OK()
        {
            var node = "{ \"a\" : null }".AsJson();

            ((Action)(() => node.Should().HaveProperty("a"))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void HaveProperty_Which_IsFilled()
        {
            var node = "{ \"a\" : [ 1, 2, 3 ] }".AsJson();
            var which = node.Should().HaveProperty("a").Which;
            which.Should()
                .Match<JsonElement>(e => e.ValueKind == JsonValueKind.Array && e.GetArrayLength() == 3);
        }

        [Fact]
        public void HaveProperty_Fail()
        {
            var node = "{ \"a\" : null }".AsJson();

            ((Action)(() => node.Should().HaveProperty("b"))).Should().Throw<XunitException>();
        }

    }
}
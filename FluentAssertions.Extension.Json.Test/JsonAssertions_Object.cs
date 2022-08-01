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

        [Fact]
        public void HaveNoProperty_OK()
        {
            var node = "{ \"a\" : null }".AsJson();

            ((Action)(() => node.Should().HaveNoProperty("b"))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void HaveNoProperty_Failed_Name()
        {
            var node = "{ \"a\" : null }".AsJson();

            ((Action)(() => node.Should().HaveNoProperty("a"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveNoProperty_Failed_Type()
        {
            var node = "{ \"a\" : null }".AsJson().GetProperty("a");

            ((Action)(() => node.Should().HaveNoProperty("a"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HavePropertyExtension()
        {
            var node = "{ \"a\" : null }".AsJson();

            node.HaveProperty("a").Should().BeTrue();
            node.HaveProperty("b").Should().BeFalse();
            node.GetProperty("a").HaveProperty("a").Should().BeFalse();
        }

        [Fact]
        public void HaveIntegerProperty_OK()
        {
            var node = "{ \"a\" : 1 }".AsJson();

            ((Action)(() => node.Should().HaveIntegerProperty("a", i => i == 1))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void HaveIntegerProperty_Fail_NotObject()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");

            ((Action)(() => node.Should().HaveIntegerProperty("a", i => i == 1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveIntegerProperty_Fail_Predicate()
        {
            var node = "{ \"a\" : 2 }".AsJson();

            ((Action)(() => node.Should().HaveIntegerProperty("a", i => i == 1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveIntegerProperty_Fail_NoProperty()
        {
            var node = "{ \"a\" : 1 }".AsJson();

            ((Action)(() => node.Should().HaveIntegerProperty("b", i => i == 1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveIntegerProperty_Fail_NotType()
        {
            var node = "{ \"a\" : \"b\" }".AsJson();

            ((Action)(() => node.Should().HaveIntegerProperty("a", i => i == 1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void NumberPropertyProperty_OK()
        {
            var node = "{ \"a\" : 1 }".AsJson();

            ((Action)(() => node.Should().HaveNumberProperty("a", i => i == 1))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void HaveNumberProperty_Fail_NotObject()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");

            ((Action)(() => node.Should().HaveNumberProperty("a", i => i == 1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveNumberProperty_Fail_Predicate()
        {
            var node = "{ \"a\" : 2 }".AsJson();

            ((Action)(() => node.Should().HaveNumberProperty("a", i => i == 1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveNumberProperty_Fail_NoProperty()
        {
            var node = "{ \"a\" : 1 }".AsJson();

            ((Action)(() => node.Should().HaveNumberProperty("b", i => i == 1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveNumberProperty_Fail_NotType()
        {
            var node = "{ \"a\" : \"b\" }".AsJson();

            ((Action)(() => node.Should().HaveNumberProperty("a", i => i == 1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveStringProperty_OK()
        {
            var node = "{ \"a\" : \"1\" }".AsJson();

            ((Action)(() => node.Should().HaveStringProperty("a", i => i == "1"))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void HaveStringProperty_Fail_NotObject()
        {
            var node = "{ \"a\" : \"1\" }".AsJson().GetProperty("a");

            ((Action)(() => node.Should().HaveStringProperty("a", i => i == "1"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveStringProperty_Fail_Predicate()
        {
            var node = "{ \"a\" : \"2\" }".AsJson();

            ((Action)(() => node.Should().HaveStringProperty("a", i => i == "1"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveStringProperty_Fail_NoProperty()
        {
            var node = "{ \"a\" : 1 }".AsJson();

            ((Action)(() => node.Should().HaveStringProperty("b", i => i == "1"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveStringProperty_Fail_NotType()
        {
            var node = "{ \"a\" : 2 }".AsJson();

            ((Action)(() => node.Should().HaveStringProperty("a", i => i == "2"))).Should().Throw<XunitException>();
        }


        [Fact]
        public void HaveObjectProperty_OK()
        {
            var node = "{ \"a\" : {\"b\" : \"1\" } }".AsJson();

            ((Action)(() => node.Should().HaveObjectProperty("a"))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void HaveObjectProperty_Fail_NotObject()
        {
            var node = "{ \"a\" : \"1\" }".AsJson().GetProperty("a");

            ((Action)(() => node.Should().HaveObjectProperty("a"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveObjectProperty_Fail_NoProperty()
        {
            var node = "{ \"a\" : {\"b\" : \"1\" } }".AsJson();

            ((Action)(() => node.Should().HaveObjectProperty("b"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveObjectProperty_Fail_NotType()
        {
            var node = "{ \"a\" : 2 }".AsJson();

            ((Action)(() => node.Should().HaveObjectProperty("a"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveNullProperty_OK()
        {
            var node = "{ \"a\" : null }".AsJson();

            ((Action)(() => node.Should().HaveNullProperty("a"))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void HaveNullProperty_Fail_NotObject()
        {
            var node = "{ \"a\" : \"1\" }".AsJson().GetProperty("a");

            ((Action)(() => node.Should().HaveNullProperty("a"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveNullProperty_Fail_NoProperty()
        {
            var node = "{ \"a\" : null }".AsJson();

            ((Action)(() => node.Should().HaveNullProperty("b"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveNullProperty_Fail_NotType()
        {
            var node = "{ \"a\" : 2 }".AsJson();

            ((Action)(() => node.Should().HaveNullProperty("a"))).Should().Throw<XunitException>();
        }
    }
}
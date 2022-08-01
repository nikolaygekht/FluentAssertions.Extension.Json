using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
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
                .BeOfType<JsonObject>();

            json["a"].Should()
                .NotBeNull();

            json["a"].GetValue<string>().Should().Be("b");
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
        [InlineData("[ a : b ]")]
        [InlineData("random text")]
        public void BeCorrectJsonTest_Fail(string json)
        {
            ((Action)(() => json.Should().BeCorrectJson())).Should().Throw<XunitException>();
        }
    }

    public class JsonAssertions_Object
    {
        [Fact]
        public void BeObject_OK()
        {
            var node = new JsonObject();
            ((Action)(() => node.Should().BeObject())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeObject_Fail_Null()
        {
            var node = (JsonObject)null;
            ((Action)(() => node.Should().BeObject())).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeObject_Fail_WrongType()
        {
            var node = new JsonArray();
            ((Action)(() => node.Should().BeObject())).Should().Throw<XunitException>();
        }
    }

    public class JsonAssertions_Array
    {
        [Fact]
        public void BeArray_OK()
        {
            var node = new JsonArray();
            ((Action)(() => node.Should().BeArray())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeArray_Fail_Null()
        {
            var node = (JsonArray)null;
            ((Action)(() => node.Should().BeArray())).Should().Throw<XunitException>();

        }

        [Fact]
        public void BeArray_Fail_WrongType()
        {
            var node = new JsonObject();
            ((Action)(() => node.Should().BeArray())).Should().Throw<XunitException>();
        }
    }

    public class JsonAssertions_Value
    {
        [Fact]
        public void BeValue_OK()
        {
            var node = JsonValue.Create(1);
            ((Action)(() => node.Should().BeValue())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeValue_Fail_Null()
        {
            var node = (JsonValue)null;
            ((Action)(() => node.Should().BeValue())).Should().Throw<XunitException>();

        }

        [Fact]
        public void BeValue_Fail_WrongType()
        {
            var node = new JsonArray();
            ((Action)(() => node.Should().BeValue())).Should().Throw<XunitException>();
        }
    }
}
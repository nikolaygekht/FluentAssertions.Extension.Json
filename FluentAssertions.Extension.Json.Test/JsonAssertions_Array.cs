using System;
using System.Text.Json;
using System.Text.Json.Nodes;
using Xunit;
using Xunit.Sdk;

namespace FluentAssertions.Extension.Json.Test
{
    public class JsonAssertions_Array
    {
        [Fact]
        public void BeArray_OK()
        {
            var node = "{ \"a\" : [ 1, 2, 3 ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeArray())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeArray_Fail_WrongType()
        {
            var node = "{ \"a\" : { \"x\" : [ 1, 2, 3 ] } }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeArray())).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeArray_Fail_Null()
        {
            var node = "{ \"a\" : null }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeArray())).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveLength_OK()
        {
            var node = "{ \"a\" : [ 1, 2, 3 ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().HaveLength(3))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void HaveLength_Fail_More()
        {
            var node = "{ \"a\" : [ 1, 2, 3 ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().HaveLength(2))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveLength_Fail_Less()
        {
            var node = "{ \"a\" : [ 1, 2, 3 ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().HaveLength(4))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveLength_Fail_Type()
        {
            var node = "{ \"a\" : null }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().HaveLength(4))).Should().Throw<XunitException>();
        }
        
        [Fact]
        public void HaveLengthAtLeast_OK_Exact()
        {
            var node = "{ \"a\" : [ 1, 2, 3 ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().HaveLengthAtLeast(3))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void HaveLengthAtLeast_OK_More()
        {
            var node = "{ \"a\" : [ 1, 2, 3 ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().HaveLengthAtLeast(2))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void HaveLengthAtLeast_Fail_Less()
        {
            var node = "{ \"a\" : [ 1, 2, 3 ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().HaveLengthAtLeast(4))).Should().Throw<XunitException>();
        }

        [Fact]
        public void HaveLengthAtLeast_Fail_Type()
        {
            var node = "{ \"a\" : null }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().HaveLengthAtLeast(4))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeEmpty_OK()
        {
            var node = "{ \"a\" : [ ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeEmpty())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeEmpty_Fail()
        {
            var node = "{ \"a\" : [ 1 ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeEmpty())).Should().Throw<XunitException>();
        }

        [Fact]
        public void NotBeEmpty_OK()
        {
            var node = "{ \"a\" : [ 1, 2, 3 ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().NotBeEmpty())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void NotBeEmpty_Fail()
        {
            var node = "{ \"a\" : [ ] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().NotBeEmpty())).Should().Throw<XunitException>();
        }
    }
}
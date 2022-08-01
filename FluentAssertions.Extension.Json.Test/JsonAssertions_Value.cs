using System;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Sdk;

namespace FluentAssertions.Extension.Json.Test
{
    public class JsonAssertions_Value
    {
        [Fact]
        public void BeValue_OK_Number()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeValue())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeValue_OK_String()
        {
            var node = "{ \"a\" : \"1\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeValue())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeValue_OK_Null()
        {
            var node = "{ \"a\" : null }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeValue())).Should().NotThrow<XunitException>();
        }


        [Fact]
        public void BeValue_Fail_Object()
        {
            var node = "{ \"a\" : \"1\" }".AsJson();
            ((Action)(() => node.Should().BeValue())).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeNull_OK()
        {
            var node = "{ \"a\" : null }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeNull())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeNull_Fail()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeNull())).Should().Throw<XunitException>();
        }

        [Fact]
        public void NotBeNull_OK()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().NotBeNull())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void NotBeNull_Fail()
        {
            var node = "{ \"a\" : null }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().NotBeNull())).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeTrue_OK()
        {
            var node = "{ \"a\" : true }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeTrue())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeTrue_Fail_false()
        {
            var node = "{ \"a\" : false }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeTrue())).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeTrue_Fail_type()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeTrue())).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeFalse_OK()
        {
            var node = "{ \"a\" : false }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeFalse())).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeFalse_Fail_true()
        {
            var node = "{ \"a\" : true }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeFalse())).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeFalse_Fail_type()
        {
            var node = "{ \"a\" : \"false\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeFalse())).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeBoolean_OK_false()
        {
            var node = "{ \"a\" : false }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(false))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeBoolean_OK_true()
        {
            var node = "{ \"a\" : true }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(true))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeBoolean_Fail_false()
        {
            var node = "{ \"a\" : true }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(false))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeBoolean_Fail_true()
        {
            var node = "{ \"a\" : false }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(true))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeBoolean_Fail_type()
        {
            var node = "{ \"a\" : null }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(true))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeInteger_OK()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(1))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeInteger_Fail_Value()
        {
            var node = "{ \"a\" : 2 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeInteger_Fail_Type()
        {
            var node = "{ \"a\" : \"1\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(1))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeDouble_OK_Exact()
        {
            var node = "{ \"a\" : 1.0 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(1.0))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeDouble_OK_Appoximately()
        {
            var node = "{ \"a\" : 1.0 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(1.01, 0.05))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeDouble_Fail_Value()
        {
            var node = "{ \"a\" : 1.0 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(1.00001))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeDouble_Fail_Type()
        {
            var node = "{ \"a\" : [\"1.0\"] }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be(1.0))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeString_Ok_CaseSensitive()
        {
            var node = "{ \"a\" : \"b\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be("b"))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeString_Ok_CaseInsensitive()
        {
            var node = "{ \"a\" : \"b\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be("B", StringComparison.OrdinalIgnoreCase))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeString_Fail_Case()
        {
            var node = "{ \"a\" : \"b\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be("B"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeString_Fail_Value_Sensitive()
        {
            var node = "{ \"a\" : \"b\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be("c"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeString_Fail_Value_Insensitive()
        {
            var node = "{ \"a\" : \"b\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Be("c", StringComparison.OrdinalIgnoreCase))).Should().Throw<XunitException>();
        }

        [Fact]
        public void MatchString_Ok_CaseSensitive()
        {
            var node = "{ \"a\" : \"abc\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Match("a.+"))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void MatchString_Ok_CaseInsensitive()
        {
            var node = "{ \"a\" : \"abc\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Match("A.+", RegexOptions.IgnoreCase))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void MatchString_Fail_Value()
        {
            var node = "{ \"a\" : \"abc\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Match("^b.+"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void MatchString_Fail_Type()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().Match("^b.+"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeStringMatching_OK()
        {
            var node = "{ \"a\" : \"abc\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeStringMatching(s => s == "abc"))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeStringMatching_Fail()
        {
            var node = "{ \"a\" : \"abc\" }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeStringMatching(s => s != "abc"))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeIntegerMatching_OK()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeIntegerMatching(s => s == 1))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeIntegerMatching_Fail()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeIntegerMatching(s => s == 2))).Should().Throw<XunitException>();
        }

        [Fact]
        public void BeDoubleMatching_OK()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeNumberMatching(s => s >= 1))).Should().NotThrow<XunitException>();
        }

        [Fact]
        public void BeDoubleMatching_Fail()
        {
            var node = "{ \"a\" : 1 }".AsJson().GetProperty("a");
            ((Action)(() => node.Should().BeNumberMatching(s => s < 1))).Should().Throw<XunitException>();
        }
    }
}
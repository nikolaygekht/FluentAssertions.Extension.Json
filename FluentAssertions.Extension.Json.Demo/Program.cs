using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace FluentAssertions.Extension.Json.Demo
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            //prepare json
            var source = new
            {
                a = new { a1 = "text" },
                b = true,
                c = false,
                d = 1,
                e = 3.1415,
                f = "string",
                i = new object[] { 1, 2, 3 }
            };
            
            var jsonText = JsonSerializer.Serialize(source);

            //validate that the text is a correct json
            jsonText.Should().BeCorrectJson();

            //parse json using AsJson extension
            var json = jsonText.AsJson();

            //HaveProperty extension for Json Element
            json.HasProperty("a").Should().BeTrue();

            //validate that json has a property
            json.Should()
                .HaveProperty("a");

            //validate that json has no property
            json.Should()
                .HaveNoProperty("x");

            //validate that json has a property and the property is an object and has a text property 
            //using which property
            json.Should()
                .HaveProperty("a")
                .Which.Should()
                    .NotBeNull()
                    .And
                    .BeObject()
                    .And
                    .HaveProperty("a1")
                        .Which.Should()
                        .Be("text");

            //or do the same without chaining
            json.Should()
                .HaveProperty("a");
            
            json.GetProperty("a").Should()
                    .BeObject()
                    .And
                    .HaveProperty("a1");
            
            json.GetProperty("a").GetProperty("a1").Should()
                    .Be("text");
                        
            //validate boolean properties
            json.Should()
                .HaveProperty("b")
                .Which.Should()
                    .BeValue()
                    .And
                    .BeTrue()
                    .And
                    .Be(true);

            json.Should()
                .HaveProperty("c")
                .Which.Should()
                    .BeFalse()
                    .And
                    .Be(false);

            //check numbers
            json.Should()
                .HaveProperty("d")
                .Which.Should()
                    .Be(1)
                    .And
                    .BeIntegerMatching(x => x < 2);

            json.Should()
                .HaveProperty("e")
                .Which.Should()
                    .Be(3.1415)
                    .And
                    .Be(3.14, 0.005)
                    .And
                    .BeNumberMatching(x => x > 3.0);

            //check string for equality
            json.Should()
                .HaveProperty("f")
                .Which.Should()
                    .Be("string")
                    .And
                    .Be("STRING", StringComparison.OrdinalIgnoreCase)
                    .And
                    .BeStringMatching(s => s.StartsWith("s"));

            //check string for matching a regular expression
            json.Should()
                .HaveProperty("f")
                .Which.Should()
                    .Match("^.tr.{3}$");

            //check three properties at a time
            json.Should()
                .HaveIntegerProperty("d", x => x == 1)
                .And
                .HaveNumberProperty("e", x => x > 3)
                .And
                .HaveStringProperty("f", s => s == "string");


            //check array
            json.Should()
                .HaveProperty("i")
                .Which.Should()
                .BeArray()
                .And
                .NotBeEmpty()
                .And
                .HaveLength(3)
                .And
                .HaveLengthAtLeast(1);

            json.GetProperty("i")[0].Should()
                .BeValue()
                .And
                .Be(1);
        }
    }
}

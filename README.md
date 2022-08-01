# FluentAssertions.Extension.Json

![unit tests](https://github.com/nikolaygekht/FluentAssertions.Extension.Json/actions/workflows/test.yml/badge.svg)

The library is an extensions for validating a json documents in [Fluent assertions](https://fluentassertions.com/introduction)

Unlike `FluentAssertions.Json` it is based on `System.Text.Json` instead of `Newtonsoft.Json`.

The syntax of the assertions is self-explaining.

Please check the example below for details:

Test Json file

```json
{
    "a" : { "a1" : "text" },
    "b" : true,
    "c" : false,
    "d" : 1,
    "e" : 3.1415,
    "f" : "string",
    "i" : [ 1, 2, 3 ],
    "j" : null
}
```

The assertions

```csharp
            //validate that the text is a correct json
            jsonText.Should().BeCorrectJson();

            //parse json using AsJson extension
            var json = jsonText.AsJson();

            //HaveProperty extension for Json Element
            json.HaveProperty("a").Should().BeTrue();

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
;

            json.Should()
                .HaveProperty("e")
                .Which.Should()
                    .Be(3.1415)
                    .And
                    .Be(3.14, 0.005)
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

            json.Should()
                .HaveProperty("j")
                .Which.Should()
                    .BeNull();
```

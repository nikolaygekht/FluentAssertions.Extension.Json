using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace FluentAssertions.Extension.Json
{
    /// <summary>
    /// Json extensions
    /// </summary>
    public static class JsonExtensions
    {
        /// <summary>
        /// Returns Json node assertions
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public static JsonElementAssertions Should(this JsonElement node) => new JsonElementAssertions(node);

        /// <summary>
        /// Parses a string into a Json object.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static JsonElement AsJson(this string value) => JsonSerializer.Deserialize<JsonDocument>(value).RootElement;

        /// <summary>
        /// Checks whether the element has a property with the name specified
        /// </summary>
        /// <param name="element"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasProperty(this JsonElement element, string name) => element.ValueKind == JsonValueKind.Object && element.TryGetProperty(name, out var _);

        /// <summary>
        /// Checks whether the element is an empty object, array or string
        /// </summary>
        /// <returns></returns>
        public static bool IsEmpty(this JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Object)
            {
                var e = element.EnumerateObject();
                e.Reset();
                return !e.MoveNext();
            }
            else if (element.ValueKind == JsonValueKind.Array)
            {
                return element.GetArrayLength() == 0;
            }
            else if (element.ValueKind == JsonValueKind.String)
            {
                return string.IsNullOrEmpty(element.GetString());
            }
            return false;
        }

        /// <summary>
        /// Checks whether the string is a correct Json
        /// </summary>
        /// <param name="assertions"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public static AndConstraint<StringAssertions> BeCorrectJson(this StringAssertions assertions, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => assertions.Subject)
                .ForCondition(s =>
                {
                    try
                    {
                        return JsonSerializer.Deserialize<JsonDocument>(s) != null;
                    }
                    catch (Exception)
                    {
                        return false;
                    }
                })
                .FailWith("Expected {context:string} should be a correct json but it is not");
            return new AndConstraint<StringAssertions>(assertions);
        }
    }
}

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
        public static JsonNodeAssertions Should(this JsonNode node) => new JsonNodeAssertions(node);

        /// <summary>
        /// Parses a string into a Json object.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static JsonObject AsJson(this string value) => JsonSerializer.Deserialize<JsonObject>(value);


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
                        return JsonSerializer.Deserialize<JsonObject>(s) != null;
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

using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System.Text.Json.Nodes;

namespace FluentAssertions.Extension.Json
{
    /// <summary>
    /// 
    /// </summary>
    public class JsonNodeAssertions : ReferenceTypeAssertions<JsonNode, JsonNodeAssertions>
    {
        public JsonNodeAssertions(JsonNode subject) : base(subject)
        {
        }

        protected override string Identifier => "json";

        /// <summary>
        /// Checks whether the node is a value
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonNodeAssertions> BeValue(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json is JsonValue)
                .FailWith("Expected {context:string} should be a value but it is {0}", Subject?.GetType()?.Name);

            return new AndConstraint<JsonNodeAssertions>(this);
        }

        /// <summary>
        /// Checks whether the node is an object
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonNodeAssertions> BeObject(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json is JsonObject)
                .FailWith("Expected {context:string} should be an object but it is {0}", Subject?.GetType()?.Name);

            return new AndConstraint<JsonNodeAssertions>(this);
        }

        /// <summary>
        /// Checks whether the node is an array
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonNodeAssertions> BeArray(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json is JsonArray)
                .FailWith("Expected {context:string} should be an array but it is {0}", Subject?.GetType()?.Name);

            return new AndConstraint<JsonNodeAssertions>(this);
        }
    }
}
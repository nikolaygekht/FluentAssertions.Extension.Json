using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace FluentAssertions.Extension.Json
{
    public partial class JsonElementAssertions
    {
        /// <summary>
        /// Checks whether the node is an object
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> BeObject(string because = null, params object[] becauseParameters) => BeOfKind(JsonValueKind.Object, because, becauseParameters);

        /// <summary>
        /// Checks whether the object has a property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public JsonHavePropertyAndContraint HaveProperty(string name, string because = null, params object[] becauseParameters)
        {
            JsonElement which = new JsonElement();

            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Object)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", JsonValueKind.Object, Subject.ValueKind)
                .Then
                .ForCondition(json => json.TryGetProperty(name, out which))
                .FailWith("Expected {context:json} should have a property {0} but it does not", name);

            return new JsonHavePropertyAndContraint(this, which);
        }
    }
}

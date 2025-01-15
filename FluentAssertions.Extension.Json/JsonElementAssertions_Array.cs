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
        /// Checks whether the node is an array
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> BeArray(string because = null, params object[] becauseParameters) => BeOfKind(JsonValueKind.Array, because, becauseParameters);

        /// <summary>
        /// Checks whether the node is an array of the specified length
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> HaveLength(int value, string because = null, params object[] becauseParameters)
        {
            mChain
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Array)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", JsonValueKind.Array, Subject.ValueKind)
                .Then
                .ForCondition(json => value == json.GetArrayLength())
                .FailWith("Expected {context:json} should have {0} elements but it has {1}", value, Subject.GetArrayLength());

            return new AndConstraint<JsonElementAssertions>(this);
        }

        /// <summary>
        /// Checks whether the node is an array of the specified length or is longer
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> HaveLengthAtLeast(int value, string because = null, params object[] becauseParameters)
        {
            mChain
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Array)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", JsonValueKind.Array, Subject.ValueKind)
                .Then
                .ForCondition(json => value <= json.GetArrayLength())
                .FailWith("Expected {context:json} should have at least {0} elements but it has {1}", value, Subject.GetArrayLength());

            return new AndConstraint<JsonElementAssertions>(this);
        }
    }
}

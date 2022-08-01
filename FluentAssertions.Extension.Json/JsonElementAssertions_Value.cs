using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Json.Nodes;

namespace FluentAssertions.Extension.Json
{
    public partial class JsonElementAssertions
    {
        /// <summary>
        /// Checks whether the node is a value
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> BeValue(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.String || json.ValueKind == JsonValueKind.Number || json.ValueKind == JsonValueKind.True || json.ValueKind == JsonValueKind.False || json.ValueKind == JsonValueKind.Null)
                .FailWith("Expected {context:json} should be a value but it is {0}", Subject.ValueKind);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        /// <summary>
        /// Check whether the node is a null value
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        new public AndConstraint<JsonElementAssertions> BeNull(string because = null, params object[] becauseParameters) => BeOfKind(JsonValueKind.Null, because, becauseParameters);

        /// <summary>
        /// Checks whether the node is not a null value
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        new public AndConstraint<JsonElementAssertions> NotBeNull(string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind != JsonValueKind.Null)
                .FailWith("Expected {context:json} should not be a null but it is {0}", Subject.ValueKind);

            return new AndConstraint<JsonElementAssertions>(this);
        }

        /// <summary>
        /// Check whether the node is true
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> BeTrue(string because = null, params object[] becauseParameters) => BeOfKind(JsonValueKind.True, because, becauseParameters);
        
        /// <summary>
        /// Check whether the node is false
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> BeFalse(string because = null, params object[] becauseParameters) => BeOfKind(JsonValueKind.False, because, becauseParameters);

        /// <summary>
        /// Check whether the node is a specified boolean value
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> Be(bool value, string because = null, params object[] becauseParameters) => value ? BeTrue(because, becauseParameters) : BeFalse(because, becauseParameters);

        /// <summary>
        /// Checks whether the node is equal to an integer value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> Be(int value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Number)
                .FailWith("Expected {context:json} should be a number but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(json => json.GetInt32() == value)
                .FailWith("Expected {context:json} should be {0} but it is {1}", value, Subject.GetInt32());

            return new AndConstraint<JsonElementAssertions>(this);
        }

        /// <summary>
        /// Checks whether the node is approximately equal to a double value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> Be(double value, double accurancy = 1e-15, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Number)
                .FailWith("Expected {context:json} should be a number but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(json => Math.Abs(json.GetDouble() - value) < accurancy)
                .FailWith("Expected {context:json} should be {0}+-{1} but it is {2}", value, accurancy, Subject.GetDouble());

            return new AndConstraint<JsonElementAssertions>(this);
        }

        /// <summary>
        /// Checks whether the node is approximately equal to a double value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> Be(string value, StringComparison comparison = StringComparison.InvariantCulture, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.String)
                .FailWith("Expected {context:json} should be a number but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(json => string.Compare(json.GetString(), value, comparison) == 0)
                .FailWith("Expected {context:json} should be {0} but it is {1}", value, Subject.GetString());

            return new AndConstraint<JsonElementAssertions>(this);
        }

        /// <summary>
        /// Checks whether the node is approximately equal to a double value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> Match(string value, RegexOptions options = RegexOptions.None, string because = null, params object[] becauseParameters)
        {
            var re = new Regex(value, options);

            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.String)
                .FailWith("Expected {context:json} should be a number but it is {0}", Subject.ValueKind)
                .Then
                .ForCondition(json => re.IsMatch(json.GetString()))
                .FailWith("Expected {context:json} should match {0} but it is {1}", value, Subject.GetString());

            return new AndConstraint<JsonElementAssertions>(this);
        }
    }
}

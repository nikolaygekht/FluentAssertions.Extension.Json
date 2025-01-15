using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System.Text.Json;

namespace FluentAssertions.Extension.Json
{
    /// <summary>
    /// Assertions for a json element
    /// </summary>
    public partial class JsonElementAssertions : ReferenceTypeAssertions<JsonElement, JsonElementAssertions>
    {
        private AssertionChain mChain;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="subject"></param>
        /// <param name="chain"></param>
        public JsonElementAssertions(JsonElement subject, AssertionChain chain) : base(subject, chain)
        {
            mChain = chain;
        }

        /// <summary>
        /// The context identifier 
        /// </summary>
        protected override string Identifier => "json";

        /// <summary>
        /// Checks whether the node a specified kind of the element
        /// </summary>
        /// <param name="kind"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public AndConstraint<JsonElementAssertions> BeOfKind(JsonValueKind kind, string because = null, params object[] becauseParameters)
        {
            mChain
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == kind)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", kind, Subject.ValueKind);

            return new AndConstraint<JsonElementAssertions>(this);
        }
    }
}
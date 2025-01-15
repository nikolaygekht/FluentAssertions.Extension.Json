using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
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

            mChain
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Object)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", JsonValueKind.Object, Subject.ValueKind)
                .Then
                .ForCondition(json => json.TryGetProperty(name, out which))
                .FailWith("Expected {context:json} should have a property {0} but it does not", name);

            return new JsonHavePropertyAndContraint(this, which);
        }

        /// <summary>
        /// Checks whether the object has a property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public JsonHavePropertyAndContraint HaveObjectProperty(string name, string because = null, params object[] becauseParameters)
        {
            JsonElement which = new JsonElement();

            mChain
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Object)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", JsonValueKind.Object, Subject.ValueKind)
                .Then
                .ForCondition(json => json.TryGetProperty(name, out which))
                .FailWith("Expected {context:json} should have a property {0} but it does not", name)
                .Then
                .ForCondition(json => which.ValueKind == JsonValueKind.Object)
                .FailWith("Expected {context:json} the property {0} is expected to be is an object, but it is {1}", name, which.ValueKind);

            return new JsonHavePropertyAndContraint(this, which);
        }

        /// <summary>
        /// Checks whether the object has a property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public JsonHavePropertyAndContraint HaveNullProperty(string name, string because = null, params object[] becauseParameters)
        {
            JsonElement which = new JsonElement();

            mChain
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Object)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", JsonValueKind.Object, Subject.ValueKind)
                .Then
                .ForCondition(json => json.TryGetProperty(name, out which))
                .FailWith("Expected {context:json} should have a property {0} but it does not", name)
                .Then
                .ForCondition(json => which.ValueKind == JsonValueKind.Null)
                .FailWith("Expected {context:json} the property {0} is expected to be is a null value but it is {1}", name, which.ValueKind);

            return new JsonHavePropertyAndContraint(this, which);
        }

        /// <summary>
        /// Checks whether the object has a property matching the predicate
        /// </summary>
        /// <param name="name"></param>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public JsonHavePropertyAndContraint HaveIntegerProperty(string name, Expression<Func<int, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            JsonElement which = new JsonElement();

            mChain
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Object)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", JsonValueKind.Object, Subject.ValueKind)
                .Then
                .ForCondition(json => json.TryGetProperty(name, out which))
                .FailWith("Expected {context:json} should have a property {0} but it does not", name)
                .Then
                .ForCondition(json => which.ValueKind == JsonValueKind.Number)
                .FailWith("Expected {context:json} the property {0} is expected to be a number but it is {1}", name, which.ValueKind)
                .Then
                .ForCondition(json => predicate.Compile()(which.GetInt32()))
                .FailWith("Expected {context:json} the property {0} is expected to match {1} its value is {2}", name, predicate, which.GetInt32());

            return new JsonHavePropertyAndContraint(this, which);
        }

        /// <summary>
        /// Checks whether the object has a property matching the predicate
        /// </summary>
        /// <param name="name"></param>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public JsonHavePropertyAndContraint HaveNumberProperty(string name, Expression<Func<double, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            JsonElement which = new JsonElement();

            mChain
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Object)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", JsonValueKind.Object, Subject.ValueKind)
                .Then
                .ForCondition(json => json.TryGetProperty(name, out which))
                .FailWith("Expected {context:json} should have a property {0} but it does not", name)
                .Then
                .ForCondition(json => which.ValueKind == JsonValueKind.Number)
                .FailWith("Expected {context:json} the property {0} is expected to be a number but it is {1}", name, which.ValueKind)
                .Then
                .ForCondition(json => predicate.Compile()(which.GetDouble()))
                .FailWith("Expected {context:json} the property {0} is expected to match {1} its value is {2}", name, predicate, which.GetDouble());

            return new JsonHavePropertyAndContraint(this, which);
        }

        /// <summary>
        /// Checks whether the object has a property matching the predicate
        /// </summary>
        /// <param name="name"></param>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public JsonHavePropertyAndContraint HaveStringProperty(string name, Expression<Func<string, bool>> predicate, string because = null, params object[] becauseParameters)
        {
            JsonElement which = new JsonElement();

            mChain
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Object)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", JsonValueKind.Object, Subject.ValueKind)
                .Then
                .ForCondition(json => json.TryGetProperty(name, out which))
                .FailWith("Expected {context:json} should have a property {0} but it does not", name)
                .Then
                .ForCondition(json => which.ValueKind == JsonValueKind.String)
                .FailWith("Expected {context:json} the property {0} is expected to be a string but it is {1}", name, which.ValueKind)
                .Then
                .ForCondition(json => predicate.Compile()(which.GetString()))
                .FailWith("Expected {context:json} the property {0} is expected to match {1} its value is {2}", name, predicate, which.GetString());

            return new JsonHavePropertyAndContraint(this, which);
        }

        /// <summary>
        /// Checks whether the object does not have a property
        /// </summary>
        /// <param name="name"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public JsonHavePropertyAndContraint HaveNoProperty(string name, string because = null, params object[] becauseParameters)
        {
            JsonElement which = new JsonElement();

            mChain
                .BecauseOf(because, becauseParameters)
                .Given(() => Subject)
                .ForCondition(json => json.ValueKind == JsonValueKind.Object)
                .FailWith("Expected {context:json} should be a {0} but it is {1}", JsonValueKind.Object, Subject.ValueKind)
                .Then
                .ForCondition(json => !json.TryGetProperty(name, out which))
                .FailWith("Expected {context:json} should not have a property {0} but it does", name);

            return new JsonHavePropertyAndContraint(this, which);
        }
    }
}

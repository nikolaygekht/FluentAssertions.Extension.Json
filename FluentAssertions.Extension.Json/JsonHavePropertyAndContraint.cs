using System.Text.Json;

namespace FluentAssertions.Extension.Json
{
    /// <summary>
    /// Special case of And condition for HaveProperty assertion. 
    /// 
    /// This condition allows to refer to the property which has been just validated 
    /// by <see cref="JsonElementAssertions.HaveProperty(string, string, object[])"/> method.
    /// </summary>
    public class JsonHavePropertyAndContraint : AndConstraint<JsonElementAssertions>
    {
        /// <summary>
        /// The property that has been just validated.
        /// </summary>
        public JsonElement Which { get; }

        internal JsonHavePropertyAndContraint(JsonElementAssertions parent, JsonElement which) : base(parent)
        {
            Which = which;
        }
    }

}

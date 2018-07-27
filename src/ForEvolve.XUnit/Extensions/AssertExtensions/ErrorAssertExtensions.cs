using ForEvolve.Api.Contracts.Errors;
using Newtonsoft.Json;
using Xunit;

namespace ForEvolve.Api.Contracts.Errors
{
    public static class ErrorAssertExtensions
    {
        public static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        public static void AssertEqual(this Error expected, Error actual)
        {
            var expectedJson = JsonConvert.SerializeObject(expected, JsonSerializerSettings);
            var actualJson = JsonConvert.SerializeObject(actual, JsonSerializerSettings);
            Assert.Equal(expectedJson, actualJson);
        }
    }
}

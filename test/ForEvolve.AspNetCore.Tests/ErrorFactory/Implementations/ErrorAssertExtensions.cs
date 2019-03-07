using ForEvolve.Contracts.Errors;
using Newtonsoft.Json;
using System;
using Xunit;

namespace ForEvolve.AspNetCore.ErrorFactory.Implementations
{
    [Obsolete(ObsoleteMessage.Xunit, false)]
    public static class ErrorAssertExtensions
    {
        [Obsolete(ObsoleteMessage.Xunit, false)]
        public static JsonSerializerSettings JsonSerializerSettings => new JsonSerializerSettings
        {
            NullValueHandling = NullValueHandling.Ignore
        };

        [Obsolete(ObsoleteMessage.Xunit, false)]
        public static void AssertEqual(this Error expected, Error actual)
        {
            var expectedJson = JsonConvert.SerializeObject(expected, JsonSerializerSettings);
            var actualJson = JsonConvert.SerializeObject(actual, JsonSerializerSettings);
            Assert.Equal(expectedJson, actualJson);
        }
    }
}

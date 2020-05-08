using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.OperationResults
{
    public abstract class OperationResultSerializationTest
    {
        public class Failure : OperationResultSerializationTest
        {
            protected override IOperationResult MakeOperationResult()
            {
                try
                {
                    throw new Exception();
                }
                catch (Exception ex)
                {
                    return OperationResult.Failure(ex);
                }
            }
        }

        public class Success : OperationResultSerializationTest
        {
            protected override IOperationResult MakeOperationResult()
            {
                return OperationResult.Success();
            }
        }

        [Fact]
        public void Should_serialize_using_SystemTextJson()
        {
            // Arrange
            var operationResult = MakeOperationResult();

            // Act
            var json = JsonSerializer.Serialize(operationResult);

            // Assert
            Assert.NotNull(json);
        }

        protected abstract IOperationResult MakeOperationResult();
    }
}

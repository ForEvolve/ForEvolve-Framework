using ForEvolve.AspNetCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ForEvolve.XUnit.OperationResults
{
    public class OperationResultFactoryFakeTest
    {
        [Fact]
        public void Instance_should_be_DefaultOperationResultFactory()
        {
            var sut = new OperationResultFactoryFake();
            Assert.IsType<DefaultOperationResultFactory>(sut.Instance);
        }

        public class Create
        {
            [Fact]
            public void Should_return_an_operation_result()
            {
                var sut = new OperationResultFactoryFake();
                var result = sut.Create();
                Assert.NotNull(result);
            }
        }

        public class Create_object
        {
            [Fact]
            public void Should_return_an_operation_result()
            {
                var sut = new OperationResultFactoryFake();
                var result = sut.Create<object>();
                Assert.NotNull(result);
            }
        }

        public class Create_object_with_data
        {
            [Fact]
            public void Should_return_an_operation_result()
            {
                var obj = new object();
                var sut = new OperationResultFactoryFake();
                var result = sut.Create(obj);
                Assert.NotNull(result);
                Assert.Same(obj, result.Value);
            }
        }
    }
}

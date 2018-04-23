using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Microsoft.WindowsAzure.Storage.Table
{
    public class TableQueryExtensionsTest
    {
        public class StartWith
        {
            [Fact]
            public void Should_return_the_expected_expression()
            {
                var query = new TableQuery<MyEntity>();
                var result = query.StartWith("RowKey", "Something");
                Assert.Equal("(RowKey ge 'Something') and (RowKey lt 'Somethinh')", result.FilterString);
            }

            [Fact]
            public void Should_return_the_expected_expression_added_to_the_current_filter()
            {
                var query = new TableQuery<MyEntity>();
                var result = query.Where("PartitionKey eq 'PK'").StartWith("RowKey", "Something");
                Assert.Equal("(PartitionKey eq 'PK') and ((RowKey ge 'Something') and (RowKey lt 'Somethinh'))", result.FilterString);
            }

            [Fact]
            public void Should_guard_against_null_columnName()
            {
                var query = new TableQuery<MyEntity>();
                Assert.Throws<ArgumentNullException>("columnName", () => query.StartWith(null, "Something", TableOperators.And));
            }

            [Fact]
            public void Should_guard_against_null_value()
            {
                var query = new TableQuery<MyEntity>();
                Assert.Throws<ArgumentNullException>("value", () => query.StartWith("RowKey", null, TableOperators.And));
            }

            [Fact]
            public void Should_guard_against_null_operatorString()
            {
                var query = new TableQuery<MyEntity>();
                Assert.Throws<ArgumentNullException>("operatorString", () => query.StartWith("RowKey", "Something", null));
            }
        }

        private class MyEntity : TableEntity
        {
        }
    }
}

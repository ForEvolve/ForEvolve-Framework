using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.WindowsAzure.Storage.Table
{
    public static class TableQueryExtensions
    {
        public static TableQuery<TElement> StartWith<TElement>(this TableQuery<TElement> query, string columnName, string value)
            where TElement : ITableEntity, new()
        {
            return query.StartWith(columnName, value, TableOperators.And);
        }

        public static TableQuery<TElement> StartWith<TElement>(this TableQuery<TElement> query, string columnName, string value, string operatorString)
            where TElement : ITableEntity, new()
        {
            if (columnName == null) { throw new ArgumentNullException(nameof(columnName)); }
            if (value == null) { throw new ArgumentNullException(nameof(value)); }
            if (operatorString == null) { throw new ArgumentNullException(nameof(operatorString)); }


            var lastChar = value[value.Length - 1];
            var lastCharCode = (int)lastChar;
            var nextChar = (char)(lastCharCode + 1);
            var lessThanValue = $"{value.Substring(0, value.Length - 1)}{nextChar}";

            var filter = TableQuery.CombineFilters(
                TableQuery.GenerateFilterCondition(
                    columnName,
                    QueryComparisons.GreaterThanOrEqual,
                    value
                ),
                TableOperators.And,
                TableQuery.GenerateFilterCondition(
                    columnName,
                    QueryComparisons.LessThan,
                    lessThanValue
                )
            );
            if (string.IsNullOrEmpty(query.FilterString))
            {
                return query.Where(filter);
            }
            return query.Where(TableQuery.CombineFilters(
                query.FilterString,
                operatorString,
                filter
            ));
        }
    }
}

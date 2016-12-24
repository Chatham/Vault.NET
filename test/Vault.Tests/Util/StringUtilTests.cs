using System;
using System.Collections.Generic;
using Vault.Util;
using Xunit;

namespace Vault.Tests.Util
{
    public class StringUtilTests
    {
        [Fact]
        public void ListToCsvString_Null_ReturnsNull()
        {
            var actual = StringUtil.ListToCsvString(null);
            Assert.Null(actual);
        }

        [Fact]
        public void ListToCsvString_SingleItem_ReturnString()
        {
            var input = new List<string> {Guid.NewGuid().ToString()};
            var actual = StringUtil.ListToCsvString(input);
            Assert.Equal(input[0], actual);
        }

        [Fact]
        public void ListToCsvString_MultipleItems_ReturnCsvList()
        {
            var input = new List<string>
            {
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString(),
                Guid.NewGuid().ToString()
            };
            var actual = StringUtil.ListToCsvString(input);
            Assert.Equal($"{input[0]},{input[1]},{input[2]}", actual);
        }

        [Fact]
        public void CsvStringToList_Null_ReturnsNull()
        {
            var actual = StringUtil.CsvStringToList(null);
            Assert.Null(actual);
        }

        [Fact]
        public void CsvStringToList_SingleItem_ReturnString()
        {
            var input = Guid.NewGuid().ToString();
            var actual = StringUtil.CsvStringToList(input);
            Assert.Equal(input, actual[0]);
        }

        [Fact]
        public void CsvStringToList_MultipleItems_ReturnList()
        {
            var item1 = Guid.NewGuid().ToString();
            var item2 = Guid.NewGuid().ToString();
            var item3 = Guid.NewGuid().ToString();
            var input = $"{item1},{item2},{item3}";

            var actual = StringUtil.CsvStringToList(input);
            Assert.Equal(item1, actual[0]);
            Assert.Equal(item2, actual[1]);
            Assert.Equal(item3, actual[2]);
        }
    }
}

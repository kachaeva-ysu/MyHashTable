using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HashTableForStudents;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CountEqualsZeroAfterTableCreation()
        {
            var table = new OpenAddressHashTable<int, string>();
            Assert.AreEqual(0, table.Count);
        }
        [TestMethod]
        public void CountIncreasesAfterAdding()
        {
            var table = new OpenAddressHashTable<int, string>();
            for (int i = 0; i < 100; i++)
                table.Add(i, "s");
            Assert.AreEqual(table.Count, 100);
        }
        [TestMethod]
        public void ItemsExistAfterAdding()
        {
            var table = new OpenAddressHashTable<int, int>();
            for (int i = 0; i < 100; i++)
                table.Add(i, i);
            for (int i = 0; i < 100; i++)
                Assert.AreEqual(i, table[i]);
        }
    }
}
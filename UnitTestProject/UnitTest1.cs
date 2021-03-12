using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using HashTableForStudents;

namespace UnitTestProject
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
            Assert.AreEqual(100, table.Count);
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
        [TestMethod]
        public void CountDecreasesAfterRemoving()
        {
            var table = new OpenAddressHashTable<int, string>();
            for (int i = 0; i < 100; i++)
                table.Add(i, "s");
            for(int i=20;i<50;i++)
                table.Remove(i);
            Assert.AreEqual(70, table.Count);
        }
       [TestMethod]
       public void IndexatorSetsCorrectly()
        {
            var table = new OpenAddressHashTable<int, string>();
            for (int i = 0; i < 100; i++)
                table.Add(i, "s");
            table[50] = "a";
            Assert.AreEqual("a", table[50]);
        }
        [TestMethod]
        public void ContainsReturnsTrueIfItemExists()
        {
            var table = new OpenAddressHashTable<int, string>();
            for (int i = 0; i < 100; i++)
                table.Add(i, "s");
            Assert.AreEqual(true, table.Contains(50));
        }
        [TestMethod]
        public void ContainsReturnsFalseIfItemDoesNotExist()
        {
            var table = new OpenAddressHashTable<int, string>();
            for (int i = 0; i < 100; i++)
                table.Add(i, "s");
            Assert.AreEqual(false, table.Contains(150));
        }
    }
}

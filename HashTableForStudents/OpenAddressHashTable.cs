using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HashTableForStudents
{
    public class OpenAddressHashTable<TKey, TValue> : IHashTable<TKey, TValue> where TKey : IEquatable<TKey>
    {
        private Pair<TKey, TValue>[] table;
        private int _capacity;
        private HashMaker<TKey> _hashMaker1, _hashMaker2;
        public int Count { get; private set; }
        private readonly GetPrimeNumber _primeNumber = new GetPrimeNumber();
        private const double FillFactor = 0.85;

        public OpenAddressHashTable() : this(3)
        { }

        public OpenAddressHashTable(int m)
        {
            table = new Pair<TKey, TValue>[m];
            _capacity = m;
            _hashMaker1 = new HashMaker<TKey>(_capacity);
            _hashMaker2 = new HashMaker<TKey>(_capacity - 1);
            Count = 0;
        }

        public void Add(TKey key, TValue value)
        {
            var h = _hashMaker1.ReturnHash(key);

            if (!TryToPut(h, key, value)) // ячейка занята
            {
                int iterationNumber = 1;
                while (true)
                {
                    var place = (h + iterationNumber * (1 + _hashMaker2.ReturnHash(key))) % _capacity;
                    if (TryToPut(place, key, value))
                        break;
                    iterationNumber++;
                    if (iterationNumber >= _capacity)
                        throw new ApplicationException("HashTable full!!!");
                }
            }
            if ((double)Count / _capacity >= FillFactor)
            {
                IncreaseTable();
            }
        }

        private bool TryToPut(int place, TKey key, TValue value)
        {
            if (table[place] == null || table[place].IsDeleted())
            {
                table[place] = new Pair<TKey, TValue>(key, value);
                Count++;
                return true;
            }
            if (table[place].Key.Equals(key))
            {
                throw new ArgumentException();
            }
            return false;
        }

        private int FindIndex(TKey x)
        {
            var h = _hashMaker1.ReturnHash(x);
            if (table[h] == null)
                throw new KeyNotFoundException();
            if (!table[h].IsDeleted() && table[h].Key.Equals(x))
                return h;
            int iterationNumber = 1;
            while (true)
            {
                var place = (h + iterationNumber * (1 + _hashMaker2.ReturnHash(x))) % _capacity;
                if (table[place] == null)
                    throw new KeyNotFoundException();
                if (!table[place].IsDeleted() && table[place].Key.Equals(x))
                    return place;
                iterationNumber++;
                if (iterationNumber >= Count)
                    throw new KeyNotFoundException();
            }
        }

        public TValue this[TKey x]
        {
            get { return table[FindIndex(x)].Value; }
            set
            {
                table[FindIndex(x)].Value = value;
            }
        }

        private void IncreaseTable()
        {
            var newCapacity = _primeNumber.Next();
            _capacity = newCapacity;
            var tempTable = table;
            table = new Pair<TKey, TValue>[newCapacity];
            _hashMaker1 = new HashMaker<TKey>(newCapacity);
            _hashMaker2 = new HashMaker<TKey>(newCapacity - 1);
            Count = 0;
            foreach (var item in tempTable)
            {
                if (item != null && !item.IsDeleted())
                {
                    var pair = new Pair<TKey, TValue>(item.Key, item.Value);
                    Add(pair.Key, pair.Value);
                }
            }
        }

        public bool Contains(TKey key)
        {
            try
            {
                FindIndex(key);
            }
            catch(KeyNotFoundException)
            {
                return false;
            }
            return true;
        }

        public bool Remove(TKey x)
        {
            try
            {
                table[FindIndex(x)].DeletePair();
            }
            catch(KeyNotFoundException)
            {
                return false;
            }
            Count--;
            return true;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < _capacity; i++)
            {
                if (table[i] != null && !table[i].IsDeleted())
                    result.AppendLine(i + " " + table[i].Key);
            }
            return result.ToString();
        }
    }
}
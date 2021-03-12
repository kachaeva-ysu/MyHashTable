using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashTableForStudents
{
    internal class GetPrimeNumber
    {
        private int _current;
        private readonly int[] _primes = { 11, 23, 61, 127, 257, 523, 1087, 2213, 4519, 9619, 19717, 40009, 84673, 170003, 340007 };

        public int Next()
        {
            if (_current < _primes.Length)
            {
                var value = _primes[_current];
                _current++;
                return value;
            }
            _current++;
            return (_current - _primes.Length) * _primes[_primes.Length - 1];
        }
    }
}
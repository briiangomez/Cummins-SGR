namespace CMM.Diagnostics
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public class LoopWatch
    {
        private static List<Dictionary<string, long>> _records = new List<Dictionary<string, long>>();
        private static Stopwatch _sw;

        public static IDictionary<string, long> Average()
        {
            Dictionary<string, long> dictionary = new Dictionary<string, long>();
            foreach (Dictionary<string, long> dictionary2 in _records)
            {
                foreach (KeyValuePair<string, long> pair in dictionary2)
                {
                    if (dictionary.ContainsKey(pair.Key))
                    {
                        Dictionary<string, long> dictionary4;
                        string str2;
                        (dictionary4 = dictionary)[str2 = pair.Key] = dictionary4[str2] + pair.Value;
                    }
                    else
                    {
                        dictionary.Add(pair.Key, pair.Value);
                    }
                }
            }
            foreach (string str in dictionary.Keys.ToArray<string>())
            {
                dictionary[str] /= (long) _records.Count;
            }
            return dictionary;
        }

        public static void Record(string key)
        {
            if (_sw.IsRunning)
            {
                _sw.Stop();
                if ((_records.Count == 0) || _records[_records.Count - 1].ContainsKey(key))
                {
                    _records.Add(new Dictionary<string, long>());
                }
                if (_records.Count == 1)
                {
                    _records[_records.Count - 1].Add(key, 0L);
                }
                else
                {
                    _records[_records.Count - 1].Add(key, _sw.ElapsedMilliseconds);
                }
            }
        }

        public static void Start()
        {
            _sw = new Stopwatch();
        }

        public static void Wait()
        {
            _sw.Restart();
        }
    }
}


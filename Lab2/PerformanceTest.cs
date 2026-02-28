using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lab2;

namespace Lab2
{
    public static class PerformanceTest
    {
        private static readonly Random rand = new Random(42);

        public static List<Hotel> GenerateUniqueHotels(int count)
        {
            var result = new List<Hotel>(count);
            for (int i = 1; i <= count; i++)
            {
                string name = $"Hotel{i:D6}";
                result.Add(new Hotel(name, rand.Next(0,101), rand.Next(10,201),
                    1000m + rand.Next(0,5000), $"Адрес {i}",
                    1.0 + rand.NextDouble()*4, rand.NextDouble()>0.5));
            }
            return result;
        }

        public static long MeasureInsert(HotelsHashtableCollection ht, List<Hotel> hotels)
        {
            var sw = Stopwatch.StartNew();
            foreach (var h in hotels) ht.Add(h);
            sw.Stop();
            ht.Clear();
            return sw.ElapsedMilliseconds;
        }

        public static long MeasureInsert(List<Hotel> list, List<Hotel> hotels)
        {
            var sw = Stopwatch.StartNew();
            foreach (var h in hotels) list.Add(h);
            sw.Stop();
            list.Clear();
            return sw.ElapsedMilliseconds;
        }

        public static void InsertUniqueHotels(HotelsHashtableCollection ht, List<Hotel> hotels)
        {
            foreach (var h in hotels) ht.Add(h);
        }

        public static void InsertUniqueHotels(List<Hotel> list, List<Hotel> hotels)
        {
            foreach (var h in hotels) list.Add(h);
        }

        public static long MeasureSeqGet(HotelsHashtableCollection ht)
        {
            var sw = Stopwatch.StartNew();
            int i = 0;
            foreach (var h in ht.Values) { _ = h.Name; i++; }
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        public static long MeasureSeqListGet(List<Hotel> list)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < list.Count; i++) _ = list[i].Name;
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        public static long MeasureRandGet(HotelsHashtableCollection ht, List<string> names)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
                _ = ht[names[rand.Next(names.Count)]]?.Name;  // O(1) по ключу
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        // ✅ ИСПРАВЛЕНО: случайный ИНДЕКС вместо поиска!
        public static long MeasureRandListGet(List<Hotel> list, List<string> names)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
            {
                int index = rand.Next(list.Count);  // Случайный индекс O(1)
                _ = list[index].Name;
            }
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}

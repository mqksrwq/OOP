using System;
using System.Diagnostics;
using Lab2;

namespace Lab2
{
    /// <summary>
    /// Класс для тестирования производительности 
    /// </summary>
    public static class PerformanceTest
    {
        private static readonly Random rand = new Random(42);
        private const long TICKS_PER_NS = 10;

        /// <summary>
        /// Генерация уникальных гостиниц (массив)
        /// </summary>
        public static Hotel[] GenerateUniqueHotels(int count)
        {
            var result = new Hotel[count];
            for (int i = 0; i < count; i++)
            {
                string name = $"Hotel{i:D6}";
                result[i] = new Hotel(name, rand.Next(0, 101), rand.Next(10, 201),
                    1000m + rand.Next(0, 5000), $"Адрес {i}",
                    1.0 + rand.NextDouble() * 4, rand.NextDouble() > 0.5);
            }

            return result;
        }

        /// <summary>
        /// Замер вставки в HashTable
        /// </summary>
        public static long MeasureInsert(HotelsHashtableCollection ht, Hotel[] hotels)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < hotels.Length; i++)
                ht.Add(hotels[i]);
            sw.Stop();
            ht.Clear();
            return sw.ElapsedTicks / TICKS_PER_NS;
        }

        /// <summary>
        /// Вставка уникальных гостиниц в HashTable
        /// </summary>
        public static void InsertUniqueHotels(HotelsHashtableCollection ht, Hotel[] hotels)
        {
            for (int i = 0; i < hotels.Length; i++)
                ht.Add(hotels[i]);
        }

        /// <summary>
        /// Замер последовательной выборки из HashTable 
        /// </summary>
        public static long MeasureSeqGet(HotelsHashtableCollection ht)
        {
            long totalTicks = 0;
            const int REPEATS = 100;
            for (int repeat = 0; repeat < REPEATS; repeat++)
            {
                var sw = Stopwatch.StartNew();
                foreach (var h in ht.Values)
                    _ = h.Name;
                sw.Stop();
                totalTicks += sw.ElapsedTicks;
            }

            return (totalTicks / REPEATS) / TICKS_PER_NS;
        }

        /// <summary>
        /// Замер случайной выборки из HashTable 
        /// </summary>
        public static long MeasureRandGet(HotelsHashtableCollection ht, string[] names)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
                _ = ht[names[rand.Next(names.Length)]]?.Name;
            sw.Stop();
            return sw.ElapsedTicks / TICKS_PER_NS;
        }

        /// <summary>
        /// Замер вставки в массив 
        /// </summary>
        public static long MeasureArrayInsert(Hotel[] array, Hotel[] hotels)
        {
            long totalTicks = 0;
            const int REPEATS = 10;
            for (int repeat = 0; repeat < REPEATS; repeat++)
            {
                var sw = Stopwatch.StartNew();
                Array.Copy(hotels, array, hotels.Length);
                sw.Stop();
                totalTicks += sw.ElapsedTicks;
                Array.Clear(array, 0, array.Length);
            }

            return (totalTicks / REPEATS) / TICKS_PER_NS;
        }

        /// <summary>
        /// Замер последовательной выборки из массива 
        /// </summary>
        public static long MeasureArraySeqGet(Hotel[] array)
        {
            long totalTicks = 0;
            const int REPEATS = 100;
            for (int repeat = 0; repeat < REPEATS; repeat++)
            {
                var sw = Stopwatch.StartNew();
                for (int i = 0; i < array.Length; i++)
                    _ = array[i]?.Name;
                sw.Stop();
                totalTicks += sw.ElapsedTicks;
            }

            return (totalTicks / REPEATS) / TICKS_PER_NS;
        }

        /// <summary>
        /// Замер случайной выборки из массива 
        /// </summary>
        public static long MeasureArrayRandGet(Hotel[] array)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
            {
                int index = rand.Next(array.Length);
                _ = array[index]?.Name;
            }

            sw.Stop();
            return sw.ElapsedTicks / TICKS_PER_NS;
        }
    }
}
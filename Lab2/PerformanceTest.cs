using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Lab2;

namespace Lab2
{
    /// <summary>
    /// Класс для тестирования производительности коллекций
    /// </summary>
    public static class PerformanceTest
    {
        private static readonly Random rand = new Random(42);

        /// <summary>
        /// Генерация уникальных гостиниц
        /// </summary>
        /// <param name="count"> Количество гостиниц </param>
        /// <returns> Список с гостиницами </returns>
        public static List<Hotel> GenerateUniqueHotels(int count)
        {
            var result = new List<Hotel>(count);
            for (int i = 1; i <= count; i++)
            {
                string name = $"Hotel{i:D6}";
                result.Add(new Hotel(name, rand.Next(0, 101), rand.Next(10, 201),
                    1000m + rand.Next(0, 5000), $"Адрес {i}",
                    1.0 + rand.NextDouble() * 4, rand.NextDouble() > 0.5));
            }

            return result;
        }

        /// <summary>
        /// Замер времени вставки
        /// </summary>
        /// <param name="ht"> Коллекция HashTable </param>
        /// <param name="hotels"> Коллекция Array </param>
        /// <returns> Время в милисекундах </returns>
        public static long MeasureInsert(HotelsHashtableCollection ht, List<Hotel> hotels)
        {
            var sw = Stopwatch.StartNew();
            foreach (var h in hotels) ht.Add(h);
            sw.Stop();
            ht.Clear();
            return sw.ElapsedMilliseconds;
        }

        /// <summary>
        /// Вставка уникальных гостиниц
        /// </summary>
        /// <param name="ht"> Коллекция HashTable</param>
        /// <param name="hotels"> Коллекция List </param>
        public static void InsertUniqueHotels(HotelsHashtableCollection ht, List<Hotel> hotels)
        {
            foreach (var h in hotels) ht.Add(h);
        }

        /// <summary>
        /// Замер последовательной выборки
        /// </summary>
        /// <param name="ht"> Коллекция HashTable </param>
        /// <returns> Время в милисекундах </returns>
        public static long MeasureSeqGet(HotelsHashtableCollection ht)
        {
            long total = 0;
            for (int repeat = 0; repeat < 100; repeat++)
            {
                var sw = Stopwatch.StartNew();
                foreach (var h in ht.Values) _ = h.Name;
                sw.Stop();
                total += sw.ElapsedMilliseconds;
            }

            return total / 100;
        }

        /// <summary>
        /// Замер случайной выборки
        /// </summary>
        /// <param name="ht"> Коллекция HashTable </param>
        /// <param name="names"> Коллекция ключей </param>
        /// <returns> Время в милисекундах </returns>
        public static long MeasureRandGet(HotelsHashtableCollection ht, List<string> names)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
                _ = ht[names[rand.Next(names.Count)]]?.Name;
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        /// <summary>
        /// Замер вставки
        /// </summary>
        /// <param name="array"> Массив </param>
        /// <param name="hotels"> Коллекция List </param>
        /// <returns> Время в милисекундах </returns>
        public static long MeasureArrayInsert(Hotel[] array, List<Hotel> hotels)
        {
            var sw = Stopwatch.StartNew();
            Array.Copy(hotels.ToArray(), array, hotels.Count);
            sw.Stop();
            Array.Clear(array, 0, array.Length);
            return sw.ElapsedMilliseconds;
        }

        /// <summary>
        /// Замер последовательной выборки
        /// </summary>
        /// <param name="array"> Массив </param>
        /// <returns> Время в милисекундах </returns>
        public static long MeasureArraySeqGet(Hotel[] array)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < array.Length; i++)
                _ = array[i]?.Name;
            sw.Stop();
            return sw.ElapsedMilliseconds;
        }

        /// <summary>
        /// Замер случайной выборки
        /// </summary>
        /// <param name="array"> Массив </param>
        /// <returns> Время в милисекундах </returns>
        public static long MeasureArrayRandGet(Hotel[] array)
        {
            var sw = Stopwatch.StartNew();
            for (int i = 0; i < 100000; i++)
            {
                int index = rand.Next(array.Length);
                _ = array[index]?.Name;
            }

            sw.Stop();
            return sw.ElapsedMilliseconds;
        }
    }
}
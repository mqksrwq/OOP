using System;

namespace Lab2
{
    /// <summary>
    /// Слушатель событий
    /// </summary>
    public class HotelsCollectionListener
    {
        private readonly HotelsHashtableCollection _collection;

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="collection"> Коллекция гостиниц </param>
        public HotelsCollectionListener(HotelsHashtableCollection collection)
        {
            _collection = collection;
            _collection.Changed += OnHotelsChanged;
        }

        /// <summary>
        /// Обработчик события
        /// </summary>
        /// <param name="sender"> Объект, вызвавший метод </param>
        /// <param name="e"> Информация о событии </param>
        private void OnHotelsChanged(object sender, HotelsChangedEventArgs e)
        {
            Console.WriteLine($"[{e.Action}] {e.Key}: {e.Message}");
        }
    }
}
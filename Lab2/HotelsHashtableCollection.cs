// HotelsHashtableCollection.cs

using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab2
{
    // Делегат для события изменений 
    public delegate void HotelsChangedHandler(object sender, HotelsChangedEventArgs e);

    /// <summary>
    /// Класс коллекции гостиниц типа HashTable
    /// </summary>
    public class HotelsHashtableCollection
    {
        private readonly Hashtable _items = new Hashtable();

        // Событие изменений
        public event HotelsChangedHandler? Changed;

        public int Count => _items.Count;

        /// <summary>
        /// Добавление гостиницы в коллекцию
        /// </summary>
        /// <param name="hotel"> Гостиница </param>
        /// <exception cref="ArgumentException"> Поле не прошло валидацию </exception>
        /// <exception cref="InvalidOperationException"> Ошибка операции </exception>
        public void Add(Hotel hotel)
        {
            if (string.IsNullOrWhiteSpace(hotel.Name))
                throw new ArgumentException("Name не может быть пустым");

            if (_items.ContainsKey(hotel.Name))
                throw new InvalidOperationException($"Отель с ключом '{hotel.Name}' уже существует");

            _items.Add(hotel.Name, hotel);
            Changed?.Invoke(this,
                new HotelsChangedEventArgs("Added", hotel.Name, hotel, $"Добавлен отель: {hotel.Name}"));
        }

        /// <summary>
        /// Удаление гостиницы из коллекции
        /// </summary>
        /// <param name="name"> Имя гостиницы </param>
        /// <returns></returns>
        public bool Remove(string name)
        {
            if (!_items.ContainsKey(name)) return false;

            var hotel = (Hotel)_items[name]!;
            _items.Remove(name);
            Changed?.Invoke(this, new HotelsChangedEventArgs("Removed", name, hotel, $"Удалён отель: {name}"));
            return true;
        }

        /// <summary>
        /// Очищение коллекции
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            Changed?.Invoke(this, new HotelsChangedEventArgs("Cleared", "", null, "Коллекция очищена"));
        }

        // Типизированный доступ
        public Hotel? this[string key]
        {
            get => (Hotel?)_items[key];
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (!_items.ContainsKey(key))
                    throw new KeyNotFoundException($"Ключ '{key}' не найден");

                _items[key] = value;
                Changed?.Invoke(this, new HotelsChangedEventArgs("Updated", key, value, $"Обновлён: {key}"));
            }
        }

        // Вывод в методе RenderHotels
        public IEnumerable<Hotel> Values
        {
            get
            {
                foreach (Hotel h in _items.Values)
                    yield return h;
            }
        }
    }
}
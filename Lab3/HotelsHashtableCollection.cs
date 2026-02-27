using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab3
{
    public delegate void HotelsChangedHandler(object sender, HotelsChangedEventArgs e);

    public class HotelsHashtableCollection
    {
        // 🔹 Singleton реализация
        private static readonly Lazy<HotelsHashtableCollection> _instance =
            new Lazy<HotelsHashtableCollection>(() => new HotelsHashtableCollection());

        public static HotelsHashtableCollection Instance => _instance.Value;

        // 🔹 Твой исходный код
        private readonly Hashtable _items = new Hashtable();

        public event HotelsChangedHandler? Changed;

        public int Count => _items.Count;

        // 🔒 Конструктор теперь private!
        private HotelsHashtableCollection()
        {
        }

        public void Add(Hotel hotel)
        {
            if (string.IsNullOrWhiteSpace(hotel.Name))
                throw new ArgumentException("Name не может быть пустым");

            if (_items.ContainsKey(hotel.Name))
                throw new InvalidOperationException($"Отель с ключом '{hotel.Name}' уже существует");

            _items.Add(hotel.Name, hotel);
            Changed?.Invoke(this, new HotelsChangedEventArgs("Added", hotel.Name, hotel, $"Добавлен отель: {hotel.Name}"));
        }

        public bool Remove(string name)
        {
            if (!_items.ContainsKey(name)) return false;

            var hotel = (Hotel)_items[name]!;
            _items.Remove(name);
            Changed?.Invoke(this, new HotelsChangedEventArgs("Removed", name, hotel, $"Удалён отель: {name}"));
            return true;
        }

        public void Clear()
        {
            _items.Clear();
            Changed?.Invoke(this, new HotelsChangedEventArgs("Cleared", "", null, "Коллекция очищена"));
        }

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
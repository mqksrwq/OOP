using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab4
{
    // Делегат для события изменений 
    public delegate void HotelsChangedHandler(object sender,
        HotelsChangedEventArgs e);

    /// <summary>
    /// Композит: коллекция гостиниц, совместимая с IHotelComponent.
    /// </summary>
    public class HotelsHashtableCollection : IHotelComponent
    {
        private readonly Hashtable _items = new Hashtable();

        public string Name { get; }

        // Событие изменений
        public event HotelsChangedHandler? Changed;

        public HotelsHashtableCollection(string name = "HotelsCollection")
        {
            Name = name;
        }

        /// <summary>
        /// Добавление гостиницы/компонента в коллекцию
        /// </summary>
        /// <param name="component"> Компонент </param>
        /// <exception cref="ArgumentException"> Поле не прошло валидацию </exception>
        /// <exception cref="InvalidOperationException"> Ошибка операции </exception>
        public void Add(IHotelComponent component)
        {
            if (_items.ContainsKey(component.Name))
            {
                throw new InvalidOperationException(
                    $"Элемент с ключом '{component.Name}' уже существует");
            }

            _items.Add(component.Name, component);
            Changed?.Invoke(this,
                new HotelsChangedEventArgs("Added", component.Name, component,
                    $"Добавлен элемент: {component.Name}"));
        }

        /// <summary>
        /// Удаление гостиницы из коллекции
        /// </summary>
        /// <param name="name"> Имя гостиницы </param>
        /// <returns></returns>
        public bool Remove(string name)
        {
            if (!_items.ContainsKey(name)) return false;

            var component = (IHotelComponent)_items[name]!;
            _items.Remove(name);
            Changed?.Invoke(this,
                new HotelsChangedEventArgs("Removed", name, component,
                    $"Удалён элемент: {name}"));
            return true;
        }

        /// <summary>
        /// Очищение коллекции
        /// </summary>
        public void Clear()
        {
            _items.Clear();
            Changed?.Invoke(this,
                new HotelsChangedEventArgs("Cleared", "", null,
                    "Коллекция очищена"));
        }

        // Типизированный доступ
        public IHotelComponent? this[string key]
        {
            get => (IHotelComponent?)_items[key];
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (!_items.ContainsKey(key))
                    throw new KeyNotFoundException($"Ключ '{key}' не найден");

                _items[key] = value;
                Changed?.Invoke(this,
                    new HotelsChangedEventArgs("Updated", key, value,
                        $"Обновлён: {key}"));
            }
        }

        public IEnumerable<IHotelComponent> Children
        {
            get
            {
                foreach (IHotelComponent h in _items.Values)
                    yield return h;
            }
        }

        /// <summary>
        /// Совместимость со старым API: перечисление значений.
        /// </summary>
        public IEnumerable<IHotelComponent> Values => Children;

        public string Describe(int indent = 0)
        {
            var pad = new string(' ', Math.Max(0, indent));
            var lines = new List<string> { $"{pad}Коллекция \"{Name}\" (кол-во: {_items.Count})" };
            foreach (var child in Children)
            {
                lines.Add(child.Describe(indent + 2));
            }
            return string.Join(Environment.NewLine, lines);
        }

        public IHotelComponent? Find(string name)
        {
            if (Name == name) return this;

            foreach (IHotelComponent child in Children)
            {
                if (child.Name == name) return child;
                var nested = child.Find(name);
                if (nested != null) return nested;
            }

            return null;
        }
    }
}

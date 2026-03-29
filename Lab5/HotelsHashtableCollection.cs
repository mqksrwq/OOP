using System;
using System.Collections;
using System.Collections.Generic;

namespace Lab5
{
    // Делегат для события изменений 
    /// <summary>
    /// Делегат для обработки событий изменения коллекции
    /// </summary>
    public delegate void HotelsChangedHandler(object sender,
        HotelsChangedEventArgs e);

    /// <summary>
    /// Композит: коллекция гостиниц, совместимая с IHotelComponent.
    /// </summary>
    public class HotelsHashtableCollection : IHotelComponent
    {
        /// <summary>
        /// Внутреннее хранилище элементов
        /// </summary>
        private readonly Hashtable _items = new Hashtable();

        /// <summary>
        /// Наименование коллекции
        /// </summary>
        public string Name { get; }

        // Событие изменений
        /// <summary>
        /// Возникает при изменении состава коллекции
        /// </summary>
        public event HotelsChangedHandler? Changed;

        /// <summary>
        /// Инициализирует новую коллекцию
        /// </summary>
        /// <param name="name">Название коллекции</param>
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
        /// <summary>
        /// Возвращает или задает компонент по ключу
        /// </summary>
        /// <param name="key">Ключ компонента</param>
        /// <returns>Найденный компонент</returns>
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

        /// <summary>
        /// Возвращает все дочерние элементы коллекции
        /// </summary>
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

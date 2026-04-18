using System;

namespace Lab5
{
    /// <summary>
    /// Контейнер данных о событии
    /// </summary>
    public class HotelsChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Наименование действия
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// Ключ, по которому произошел доступ
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Затронутый компонент
        /// </summary>
        public IHotelComponent? Component { get; }

        /// <summary>
        /// Сообщение о событии
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="action"> Действие </param>
        /// <param name="key"> Поле </param>
        /// <param name="component"> Компонент (отель или коллекция) </param>
        /// <param name="message"> Сообщение </param>
        public HotelsChangedEventArgs(string action, string key, IHotelComponent? component, string message)
        {
            Action = action;
            Key = key;
            Component = component;
            Message = message;
        }
    }
}

using System;

namespace Lab4
{
    /// <summary>
    /// Контейнер данных о событии
    /// </summary>
    public class HotelsChangedEventArgs : EventArgs
    {
        // События
        public string Action { get; }

        // Доступ по ключу
        public string Key { get; }

        public IHotelComponent? Component { get; }
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

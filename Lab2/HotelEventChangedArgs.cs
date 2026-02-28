using System;

namespace Lab2
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

        public Hotel? Hotel { get; }
        public string Message { get; }

        /// <summary>
        /// Конструктор с параметрами
        /// </summary>
        /// <param name="action"> Действие </param>
        /// <param name="key"> Поле </param>
        /// <param name="hotel"> Гостиница </param>
        /// <param name="message"> Сообщение </param>
        public HotelsChangedEventArgs(string action, string key, Hotel? hotel, string message)
        {
            Action = action;
            Key = key;
            Hotel = hotel;
            Message = message;
        }
    }
}
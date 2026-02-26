// 1. Новые файлы: HotelsChangedEventArgs.cs и HotelsHashtableCollection.cs
// HotelsChangedEventArgs.cs
using System;

namespace Lab2
{
    public class HotelsChangedEventArgs : EventArgs
    {
        public string Action { get; }  // "Added", "Removed", "Cleared"
        public string Key { get; }     // hotel.Name
        public Hotel? Hotel { get; }   // null для Clear
        public string Message { get; }

        public HotelsChangedEventArgs(string action, string key, Hotel? hotel, string message)
        {
            Action = action;
            Key = key;
            Hotel = hotel;
            Message = message;
        }
    }
}
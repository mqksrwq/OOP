// 2. Слушатель событий: HotelsCollectionListener.cs (отдельный класс)
using System;

namespace Lab2
{
    public class HotelsCollectionListener
    {
        private readonly HotelsHashtableCollection _collection;

        public HotelsCollectionListener(HotelsHashtableCollection collection)
        {
            _collection = collection;
            _collection.Changed += OnHotelsChanged;
        }

        private void OnHotelsChanged(object sender, HotelsChangedEventArgs e)
        {
            Console.WriteLine($"[{e.Action}] {e.Key}: {e.Message}");
            // УБРАТЬ Form1.Instance — оно null!
            // MessageBox.Show(e.Message);  // если хотите popup
        }

    }
}
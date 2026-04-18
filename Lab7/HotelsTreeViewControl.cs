using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab7;

/// <summary>
/// Представление дерева групп и гостиниц.
/// Автоматически обновляется при изменениях коллекции.
/// </summary>
public class HotelsTreeViewControl : UserControl
{
    /// <summary>
    /// Дерево отображения структуры групп и гостиниц.
    /// </summary>
    private readonly TreeView _tree;

    /// <summary>
    /// Корневая коллекция гостиниц.
    /// </summary>
    private readonly HotelsHashtableCollection _root;

    /// <summary>
    /// Набор коллекций, на которые подписан контроль для отслеживания изменений.
    /// </summary>
    private readonly HashSet<HotelsHashtableCollection> _watched = new();

    /// <summary>
    /// Инициализирует контрол и настраивает первичную подписку на события модели.
    /// </summary>
    public HotelsTreeViewControl()
    {
        _tree = new TreeView { Dock = DockStyle.Fill };
        Controls.Add(_tree);

        _root = HotelAppState.Instance.Hotels;
        SubscribeRecursive(_root);

        Load += (_, _) => RefreshTree();
    }

    /// <summary>
    /// Обработчик события изменения коллекции гостиниц.
    /// </summary>
    /// <param name="sender">Источник события.</param>
    /// <param name="e">Аргументы изменения коллекции.</param>
    private void RootOnChanged(object? sender, HotelsChangedEventArgs e)
    {
        if (IsHandleCreated)
            BeginInvoke(RefreshTree);
    }

    /// <summary>
    /// Полностью перестраивает визуальное дерево по текущему состоянию модели.
    /// </summary>
    private void RefreshTree()
    {
        SubscribeRecursive(_root);

        _tree.BeginUpdate();
        _tree.Nodes.Clear();

        var rootNode = new TreeNode(_root.Name);
        _tree.Nodes.Add(rootNode);
        FillNode(rootNode, _root);
        _tree.ExpandAll();

        _tree.EndUpdate();
    }

    /// <summary>
    /// Рекурсивно наполняет узел дерева дочерними элементами компонента.
    /// </summary>
    /// <param name="node">Целевой узел дерева.</param>
    /// <param name="comp">Компонент модели, чьи дочерние элементы нужно отобразить.</param>
    private static void FillNode(TreeNode node, IHotelComponent comp)
    {
        foreach (var child in comp.Children)
        {
            var childNode = new TreeNode(child.Name);
            node.Nodes.Add(childNode);
            FillNode(childNode, child);
        }
    }

    /// <summary>
    /// Рекурсивно подписывает все вложенные коллекции на событие изменений.
    /// </summary>
    /// <param name="collection">Коллекция для подписки.</param>
    private void SubscribeRecursive(HotelsHashtableCollection collection)
    {
        if (_watched.Add(collection))
        {
            collection.Changed += RootOnChanged;
        }

        foreach (var child in collection.Children)
        {
            if (child is HotelsHashtableCollection nested)
                SubscribeRecursive(nested);
        }
    }

    /// <summary>
    /// Освобождает ресурсы и снимает подписки на события коллекций.
    /// </summary>
    /// <param name="disposing">Признак управляемого освобождения.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            foreach (var col in _watched)
            {
                col.Changed -= RootOnChanged;
            }
            _watched.Clear();
        }
        base.Dispose(disposing);
    }
}



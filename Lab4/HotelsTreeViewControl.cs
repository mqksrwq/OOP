using System.Collections.Generic;
using System.Windows.Forms;

namespace Lab4;

public class HotelsTreeViewControl : UserControl
{
    private readonly TreeView _tree;
    private readonly HotelsHashtableCollection _root;
    private readonly HashSet<HotelsHashtableCollection> _watched = new();

    public HotelsTreeViewControl()
    {
        _tree = new TreeView { Dock = DockStyle.Fill };
        Controls.Add(_tree);

        _root = HotelAppState.Instance.Hotels;
        SubscribeRecursive(_root);

        Load += (_, _) => RefreshTree();
    }

    private void RootOnChanged(object? sender, HotelsChangedEventArgs e)
    {
        if (IsHandleCreated)
            BeginInvoke(RefreshTree);
    }

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

    private static void FillNode(TreeNode node, IHotelComponent comp)
    {
        foreach (var child in comp.Children)
        {
            var childNode = new TreeNode(child.Name);
            node.Nodes.Add(childNode);
            FillNode(childNode, child);
        }
    }

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

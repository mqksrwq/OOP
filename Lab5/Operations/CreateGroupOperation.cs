using System;
using System.Windows.Forms;

namespace Lab5.Operations;

public sealed class CreateGroupOperation : UiOperationTemplate
{
    private readonly string _name;
    private readonly Action? _afterSuccess;

    public CreateGroupOperation(string name, Action? afterSuccess = null)
    {
        _name = name.Trim();
        _afterSuccess = afterSuccess;
    }

    protected override string SuccessMessage => "Группа создана.";

    protected override bool Validate()
    {
        if (string.IsNullOrWhiteSpace(_name))
        {
            MessageBox.Show("Введите название группы.", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        var root = HotelAppState.Instance.Hotels;
        if (root.Find(_name) != null)
        {
            MessageBox.Show("Компонент с таким именем уже существует.", "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        return true;
    }

    protected override void ExecuteCore()
    {
        HotelAppState.Instance.Hotels.Add(new HotelsHashtableCollection(_name));
    }

    protected override void OnSuccess()
    {
        base.OnSuccess();
        _afterSuccess?.Invoke();
    }
}


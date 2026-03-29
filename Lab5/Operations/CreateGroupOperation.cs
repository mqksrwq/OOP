using System;
using System.Windows.Forms;

namespace Lab5.Operations;

/// <summary>
/// Операция создания новой группы гостиниц
/// </summary>
public sealed class CreateGroupOperation : UiOperationTemplate
{
    private readonly string _name;
    private readonly string _parentName;
    private readonly Action? _afterSuccess;
    private HotelsHashtableCollection _parentGroup = null!;

    /// <summary>
    /// Инициализирует операцию создания группы
    /// </summary>
    /// <param name="name">Имя группы</param>
    /// <param name="parentName">Имя родительской группы</param>
    /// <param name="afterSuccess">Действие после успешного выполнения</param>
    public CreateGroupOperation(string name, string parentName, Action? afterSuccess = null)
    {
        _name = name.Trim();
        _parentName = parentName.Trim();
        _afterSuccess = afterSuccess;
    }

    /// <summary>
    /// Сообщение об успешном выполнении операции
    /// </summary>
    protected override string SuccessMessage => "Группа создана.";

    /// <summary>
    /// Проверяет валидность данных перед выполнением
    /// </summary>
    /// <returns>Признак успешной валидации</returns>
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

        _parentGroup = root;
        if (!string.IsNullOrEmpty(_parentName))
        {
            var parentComp = root.Find(_parentName);
            if (parentComp is not HotelsHashtableCollection parentGroup)
            {
                MessageBox.Show("Родительская группа не найдена.", "Ошибка",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            _parentGroup = parentGroup;
        }

        return true;
    }

    /// <summary>
    /// Выполняет основную логику создания группы
    /// </summary>
    protected override void ExecuteCore()
    {
        _parentGroup.Add(new HotelsHashtableCollection(_name));
    }

    /// <summary>
    /// Вызывается при успешном выполнении операции
    /// </summary>
    protected override void OnSuccess()
    {
        base.OnSuccess();
        _afterSuccess?.Invoke();
    }
}


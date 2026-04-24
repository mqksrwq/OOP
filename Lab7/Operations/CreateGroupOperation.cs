using System;
using System.Windows.Forms;

namespace Lab7.Operations;

/// <summary>
/// Операция создания новой группы гостиниц
/// </summary>
public sealed class CreateGroupOperation : UiOperationTemplate
{
    /// <summary>
    /// Имя создаваемой группы.
    /// </summary>
    private readonly string _name;

    /// <summary>
    /// Имя родительской группы.
    /// </summary>
    private readonly string _parentName;

    /// <summary>
    /// Действие, выполняемое после успешного завершения.
    /// </summary>
    private readonly Action? _afterSuccess;

    /// <summary>
    /// Нормализованное имя создаваемой группы.
    /// </summary>
    private string _validatedName = string.Empty;

    /// <summary>
    /// Группа, в которую будет добавлена новая группа.
    /// </summary>
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
        if (!FieldValidation.TryNormalizeName(_name, "Название группы", 60, out _validatedName, out var error))
        {
            MessageBox.Show(error, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        var root = HotelAppState.Instance.Hotels;
        if (root.Find(_validatedName) != null)
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
        _parentGroup.Add(new HotelsHashtableCollection(_validatedName));
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




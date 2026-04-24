namespace Lab8;

/// <summary>
/// Контракт представления формы групп в MVP.
/// </summary>
public interface IGroupView
{
    /// <summary>
    /// Событие запроса создания группы.
    /// </summary>
    event EventHandler? CreateGroupRequested;

    /// <summary>
    /// Событие запроса очистки формы.
    /// </summary>
    event EventHandler? ClearRequested;

    /// <summary>
    /// Название создаваемой группы.
    /// </summary>
    string GroupName { get; }

    /// <summary>
    /// Название родительской группы.
    /// </summary>
    string ParentGroupName { get; }

    /// <summary>
    /// Очищает поля формы.
    /// </summary>
    void ClearForm();
}

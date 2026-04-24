using Lab8.Operations;

namespace Lab8;

/// <summary>
/// Презентер экрана групп гостиниц.
/// Управляет созданием групп и очисткой формы в рамках MVP.
/// </summary>
public sealed class GroupPresenter
{
    /// <summary>
    /// Представление экрана групп.
    /// </summary>
    private readonly IGroupView _view;

    /// <summary>
    /// Инициализирует презентер и подписывает обработчики событий представления.
    /// </summary>
    /// <param name="view">Представление экрана групп.</param>
    public GroupPresenter(IGroupView view)
    {
        _view = view;

        _view.CreateGroupRequested += (_, _) =>
            new CreateGroupOperation(_view.GroupName, _view.ParentGroupName, _view.ClearForm).Execute();

        _view.ClearRequested += (_, _) => _view.ClearForm();
    }
}

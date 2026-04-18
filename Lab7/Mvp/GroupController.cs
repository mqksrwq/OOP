using Lab7.Operations;

namespace Lab7;

/// <summary>
/// Presenter экрана групп гостиниц.
/// Управляет созданием групп и очисткой формы в рамках MVP.
/// </summary>
public sealed class GroupPresenter
{
    /// <summary>
    /// Представление экрана групп.
    /// </summary>
    private readonly IGroupView _view;

    /// <summary>
    /// Инициализирует presenter и подписывает обработчики событий представления.
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



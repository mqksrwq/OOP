using Lab5.Operations;

namespace Lab5;

/// <summary>
/// Presenter экрана гостиницы.
/// Содержит логику поиска, создания и сохранения гостиниц в паттерне MVP.
/// </summary>
public sealed class HotelPresenter
{
    /// <summary>
    /// Представление формы гостиницы.
    /// </summary>
    private readonly IHotelView _view;

    /// <summary>
    /// Текущая группа найденной гостиницы.
    /// </summary>
    private HotelsHashtableCollection? _currentGroup;

    /// <summary>
    /// Текущая найденная гостиница для редактирования.
    /// </summary>
    private Hotel? _currentHotel;

    /// <summary>
    /// Инициализирует presenter и подписывает обработчики событий представления.
    /// </summary>
    /// <param name="view">Представление формы гостиницы.</param>
    public HotelPresenter(IHotelView view)
    {
        _view = view;

        _view.CreateHotelRequested += (_, _) =>
            new CreateHotelOperation(_view.ReadFormData(), AfterSave).Execute();

        _view.SaveHotelRequested += (_, _) =>
            new SaveHotelOperation(_currentGroup, _currentHotel, _view.ReadFormData(), AfterSave).Execute();

        _view.FindHotelRequested += (_, _) => FindHotel();

        _view.ClearRequested += (_, _) => ResetFormState();
    }

    /// <summary>
    /// Выполняет поиск гостиницы по имени и подготавливает форму к редактированию.
    /// </summary>
    private void FindHotel()
    {
        var search = _view.SearchText.Trim();
        if (string.IsNullOrWhiteSpace(search))
        {
            _view.ShowInfo("Введите имя гостиницы для поиска.");
            return;
        }

        var root = HotelAppState.Instance.Hotels;
        if (!TryFindHotelWithParent(root, search, out var group, out var hotel))
        {
            _view.ShowInfo("Гостиница не найдена.");
            return;
        }

        _currentGroup = group;
        _currentHotel = hotel;
        _view.FillForm(group, hotel);
        _view.SetSaveEnabled(true);
    }

    /// <summary>
    /// Сбрасывает состояние формы после успешной операции сохранения/создания.
    /// </summary>
    private void AfterSave()
    {
        _view.ClearForm();
        _currentGroup = null;
        _currentHotel = null;
        _view.SetSaveEnabled(false);
    }

    /// <summary>
    /// Полностью очищает форму и внутреннее состояние presenter-а.
    /// </summary>
    private void ResetFormState()
    {
        _view.ClearForm();
        _currentGroup = null;
        _currentHotel = null;
        _view.SetSaveEnabled(false);
    }

    /// <summary>
    /// Рекурсивно ищет гостиницу и возвращает группу-родителя найденного отеля.
    /// </summary>
    /// <param name="root">Корневая группа поиска.</param>
    /// <param name="name">Имя гостиницы.</param>
    /// <param name="group">Найденная родительская группа.</param>
    /// <param name="hotel">Найденная гостиница.</param>
    /// <returns><c>true</c>, если гостиница найдена; иначе <c>false</c>.</returns>
    private static bool TryFindHotelWithParent(HotelsHashtableCollection root, string name,
        out HotelsHashtableCollection group, out Hotel hotel)
    {
        foreach (var child in root.Children)
        {
            if (child is Hotel h && h.Name == name)
            {
                group = root;
                hotel = h;
                return true;
            }

            if (child is HotelsHashtableCollection nested &&
                TryFindHotelWithParent(nested, name, out group, out hotel))
            {
                return true;
            }
        }

        group = null!;
        hotel = null!;
        return false;
    }
}

using System;
using System.Windows.Forms;

namespace Lab7.Operations;

/// <summary>
/// Операция сохранения отредактированной гостиницы
/// </summary>
public sealed class SaveHotelOperation : UiOperationTemplate
{
    /// <summary>
    /// Текущая группа редактируемой гостиницы.
    /// </summary>
    private readonly HotelsHashtableCollection? _currentGroup;

    /// <summary>
    /// Текущая редактируемая гостиница.
    /// </summary>
    private readonly Hotel? _currentHotel;

    /// <summary>
    /// Новые данные формы.
    /// </summary>
    private readonly HotelFormData _data;

    /// <summary>
    /// Действие после успешного сохранения.
    /// </summary>
    private readonly Action? _afterSuccess;

    /// <summary>
    /// Группа, в которую должна быть помещена обновлённая гостиница.
    /// </summary>
    private HotelsHashtableCollection? _targetGroup;

    /// <summary>
    /// Новый экземпляр гостиницы с обновлёнными данными.
    /// </summary>
    private Hotel? _newHotel;

    /// <summary>
    /// Инициализирует операцию сохранения гостиницы
    /// </summary>
    /// <param name="currentGroup">Текущая группа гостиницы</param>
    /// <param name="currentHotel">Редактируемая гостиница</param>
    /// <param name="data">Новые данные</param>
    /// <param name="afterSuccess">Действие после успешного выполнения</param>
    public SaveHotelOperation(
        HotelsHashtableCollection? currentGroup,
        Hotel? currentHotel,
        HotelFormData data,
        Action? afterSuccess = null)
    {
        _currentGroup = currentGroup;
        _currentHotel = currentHotel;
        _data = data;
        _afterSuccess = afterSuccess;
    }

    /// <summary>
    /// Сообщение об успешном выполнении операции
    /// </summary>
    protected override string SuccessMessage => "Изменения сохранены.";

    /// <summary>
    /// Проверяет валидность данных перед сохранением
    /// </summary>
    /// <returns>Признак успешной валидации</returns>
    protected override bool Validate()
    {
        if (_currentGroup == null || _currentHotel == null)
        {
            MessageBox.Show("Сначала найдите гостиницу для редактирования.", "Инфо",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return false;
        }

        return HotelFormDataParser.TryBuild(_data, out _targetGroup, out _newHotel);
    }

    /// <summary>
    /// Выполняет основную логику сохранения
    /// </summary>
    protected override void ExecuteCore()
    {
        _currentGroup?.Remove(_currentHotel!.Name);
        _targetGroup?.Add(_newHotel!);
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




using System;
using System.Windows.Forms;

namespace Lab5.Operations;

/// <summary>
/// Операция сохранения отредактированной гостиницы
/// </summary>
public sealed class SaveHotelOperation : UiOperationTemplate
{
    private readonly HotelsHashtableCollection? _currentGroup;
    private readonly Hotel? _currentHotel;
    private readonly HotelFormData _data;
    private readonly Action? _afterSuccess;

    private HotelsHashtableCollection? _targetGroup;
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


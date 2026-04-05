using System;

namespace Lab5.Operations;

/// <summary>
/// Операция создания новой гостиницы
/// </summary>
public sealed class CreateHotelOperation : UiOperationTemplate
{
    private readonly HotelFormData _data;
    private readonly Action? _afterSuccess;
    private HotelsHashtableCollection _group = null!;
    private Hotel _hotel = null!;

    /// <summary>
    /// Инициализирует операцию создания гостиницы
    /// </summary>
    /// <param name="data">Данные формы</param>
    /// <param name="afterSuccess">Действие после успешного выполнения</param>
    public CreateHotelOperation(HotelFormData data, Action? afterSuccess = null)
    {
        _data = data;
        _afterSuccess = afterSuccess;
    }

    /// <summary>
    /// Сообщение об успешном выполнении операции
    /// </summary>
    protected override string SuccessMessage => "Гостиница создана и добавлена в группу.";

    /// <summary>
    /// Проверяет валидность данных перед выполнением
    /// </summary>
    /// <returns>Признак успешной валидации</returns>
    protected override bool Validate()
    {
        return HotelFormDataParser.TryBuild(_data, out _group!, out _hotel!);
    }

    /// <summary>
    /// Выполняет основную логику создания гостиницы
    /// </summary>
    protected override void ExecuteCore()
    {
        _group.Add(_hotel);
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


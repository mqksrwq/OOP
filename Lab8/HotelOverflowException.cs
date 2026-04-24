/// <summary>
/// Кастомное исключение для ошибок переполнения гостиницы
/// </summary>
public class HotelOverflowException : OverflowException
{
    /// <summary>
    /// Название поля, в котором произошло переполнение
    /// </summary>
    public string FieldName { get; }

    /// <summary>
    /// Значение, вызвавшее переполнение
    /// </summary>
    public string Value { get; }

    /// <summary>
    /// Конструктор с параметрами
    /// </summary>
    /// <param name="fieldName"> Название поля </param>
    /// <param name="value"> Значение поля </param>
    public HotelOverflowException(string fieldName, string value)
        // вызов конструктора родителя
        : base($"Переполнение для поля '{fieldName}': значение '{value}' слишком велико")
    {
        FieldName = fieldName;
        Value = value;
    }
}
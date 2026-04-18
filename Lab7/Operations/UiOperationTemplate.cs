using System;
using System.Windows.Forms;

namespace Lab7.Operations;

/// <summary>
/// Шаблонный класс для UI операций
/// </summary>
public abstract class UiOperationTemplate
{
    /// <summary>
    /// Запускает выполнение операции по шаблону
    /// </summary>
    public void Execute()
    {
        try
        {
            if (!Validate())
                return;

            ExecuteCore();
            OnSuccess();
        }
        catch (Exception ex)
        {
            OnError(ex);
        }
    }

    /// <summary>
    /// Метод для проверки валидности данных перед выполнением
    /// </summary>
    /// <returns>Признак успешной валидации</returns>
    protected abstract bool Validate();

    /// <summary>
    /// Основная логика операции
    /// </summary>
    protected abstract void ExecuteCore();

    /// <summary>
    /// Сообщение об успешном выполнении
    /// </summary>
    protected abstract string SuccessMessage { get; }

    /// <summary>
    /// Действия при успешном выполнении операции
    /// </summary>
    protected virtual void OnSuccess()
    {
        MessageBox.Show(SuccessMessage, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    /// <summary>
    /// Обработчик ошибок операции
    /// </summary>
    /// <param name="ex">Возникшее исключение</param>
    protected virtual void OnError(Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}


using System;
using System.Windows.Forms;

namespace Lab5.Operations;

public abstract class UiOperationTemplate
{
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

    protected abstract bool Validate();
    protected abstract void ExecuteCore();
    protected abstract string SuccessMessage { get; }

    protected virtual void OnSuccess()
    {
        MessageBox.Show(SuccessMessage, "Успех", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }

    protected virtual void OnError(Exception ex)
    {
        MessageBox.Show(ex.Message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
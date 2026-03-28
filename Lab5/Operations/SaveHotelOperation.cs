using System;
using System.Windows.Forms;

namespace Lab5.Operations;

public sealed class SaveHotelOperation : UiOperationTemplate
{
    private readonly HotelsHashtableCollection? _currentGroup;
    private readonly Hotel? _currentHotel;
    private readonly HotelFormData _data;
    private readonly Action? _afterSuccess;

    private HotelsHashtableCollection _targetGroup = null!;
    private Hotel _newHotel = null!;

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

    protected override string SuccessMessage => "Изменения сохранены.";

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

    protected override void ExecuteCore()
    {
        _currentGroup!.Remove(_currentHotel!.Name);
        _targetGroup.Add(_newHotel);
    }

    protected override void OnSuccess()
    {
        base.OnSuccess();
        _afterSuccess?.Invoke();
    }
}


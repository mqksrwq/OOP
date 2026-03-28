using System;

namespace Lab5.Operations;

public sealed class CreateHotelOperation : UiOperationTemplate
{
    private readonly HotelFormData _data;
    private readonly Action? _afterSuccess;
    private HotelsHashtableCollection _group = null!;
    private Hotel _hotel = null!;

    public CreateHotelOperation(HotelFormData data, Action? afterSuccess = null)
    {
        _data = data;
        _afterSuccess = afterSuccess;
    }

    protected override string SuccessMessage => "Гостиница создана и добавлена в группу.";

    protected override bool Validate()
    {
        return HotelFormDataParser.TryBuild(_data, out _group, out _hotel);
    }

    protected override void ExecuteCore()
    {
        _group.Add(_hotel);
    }

    protected override void OnSuccess()
    {
        base.OnSuccess();
        _afterSuccess?.Invoke();
    }
}


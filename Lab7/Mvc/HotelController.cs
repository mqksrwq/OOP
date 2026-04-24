namespace Lab7;

public sealed class HotelController
{
    private readonly IHotelView _view;
    private Hotel? _editingHotel;

    public HotelController(IHotelView view)
    {
        _view = view;
        _view.CreateRequested += (_, _) => CreateHotel();
        _view.ApplyRequested += (_, _) => ApplyEdit();
        _view.CancelRequested += (_, _) => ExitEditMode();
        _view.ExitRequested += (_, _) => _view.CloseView();
        _view.EditRequested += (_, e) => StartEditHotel(e.Hotel);
    }

    private void CreateHotel()
    {
        try
        {
            if (!_view.TryGetHotelFromForm(out var input))
            {
                return;
            }

            _ = new Hotel(input.Name, input.OccupiedRooms, input.TotalRooms, input.PricePerDay,
                input.Address, input.Rating, input.HasFreeWiFi);
            _view.RenderHotels();
            _view.ClearFormFields();
        }
        catch (HotelOverflowException ex)
        {
            _view.ShowError(ex.Message);
        }
        catch (Exception ex)
        {
            _view.ShowWarning($"Ошибка создания гостиницы: {ex.Message}");
        }
    }

    private void ApplyEdit()
    {
        try
        {
            if (_editingHotel == null)
            {
                return;
            }

            if (!_view.TryGetHotelFromForm(out var input))
            {
                return;
            }

            _editingHotel.Name = input.Name;
            _editingHotel.OccupiedRooms = input.OccupiedRooms;
            _editingHotel.TotalRooms = input.TotalRooms;
            _editingHotel.PricePerDay = input.PricePerDay;
            _editingHotel.Address = input.Address;
            _editingHotel.Rating = input.Rating;
            _editingHotel.HasFreeWiFi = input.HasFreeWiFi;

            ExitEditMode();
            _view.RenderHotels();
        }
        catch (HotelOverflowException ex)
        {
            _view.ShowError(ex.Message);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Ошибка редактирования гостиницы: {ex.Message}");
        }
    }

    private void StartEditHotel(Hotel hotel)
    {
        try
        {
            _editingHotel = hotel;
            _view.FillForm(hotel);

            _view.SetEditMode(true);
        }
        catch (HotelOverflowException ex)
        {
            _view.ShowError(ex.Message);
        }
        catch (Exception ex)
        {
            _view.ShowError($"Ошибка редактирования гостиницы: {ex.Message}");
        }
    }

    private void ExitEditMode()
    {
        _editingHotel = null;
        _view.ClearFormFields();
        _view.SetEditMode(false);
    }
}



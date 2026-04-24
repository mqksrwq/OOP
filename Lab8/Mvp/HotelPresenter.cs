namespace Lab8;

public sealed class HotelPresenter
{
    private readonly IHotelView _view;
    private Hotel? _editingHotel;

    public HotelPresenter(IHotelView view)
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
            if (!TryGetHotelFromView(out string name, out int occupiedRooms, out int totalRooms,
                    out decimal pricePerDay, out string address, out double rating, out bool hasFreeWiFi))
            {
                return;
            }

            _ = new Hotel(name, occupiedRooms, totalRooms, pricePerDay, address, rating, hasFreeWiFi);
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

            if (!TryGetHotelFromView(out string name, out int occupiedRooms, out int totalRooms,
                    out decimal pricePerDay, out string address, out double rating, out bool hasFreeWiFi))
            {
                return;
            }

            _editingHotel.Name = name;
            _editingHotel.OccupiedRooms = occupiedRooms;
            _editingHotel.TotalRooms = totalRooms;
            _editingHotel.PricePerDay = pricePerDay;
            _editingHotel.Address = address;
            _editingHotel.Rating = rating;
            _editingHotel.HasFreeWiFi = hasFreeWiFi;

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
            _view.HotelNameText = hotel.Name;
            _view.OccupiedRoomsText = hotel.OccupiedRooms.ToString();
            _view.TotalRoomsText = hotel.TotalRooms.ToString();
            _view.PricePerDayText = hotel.PricePerDay.ToString();
            _view.AddressText = hotel.Address;
            _view.RatingText = hotel.Rating.ToString();
            _view.HasFreeWiFiChecked = hotel.HasFreeWiFi;

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

    private bool TryGetHotelFromView(out string name, out int occupiedRooms, out int totalRooms,
        out decimal pricePerDay, out string address, out double rating, out bool hasFreeWiFi)
    {
        name = _view.HotelNameText.Trim();
        if (string.IsNullOrWhiteSpace(name))
        {
            _view.ShowError("Введите название гостиницы.");
            occupiedRooms = totalRooms = 0;
            pricePerDay = 0;
            address = string.Empty;
            rating = 0;
            hasFreeWiFi = false;
            return false;
        }

        if (!SafeParseInt(_view.OccupiedRoomsText.Trim(), "Заселено мест", out occupiedRooms))
        {
            totalRooms = 0;
            pricePerDay = 0;
            address = string.Empty;
            rating = 0;
            hasFreeWiFi = false;
            return false;
        }

        if (!SafeParseInt(_view.TotalRoomsText.Trim(), "Общее число мест", out totalRooms))
        {
            pricePerDay = 0;
            address = string.Empty;
            rating = 0;
            hasFreeWiFi = false;
            return false;
        }

        if (occupiedRooms > totalRooms)
        {
            _view.ShowError("'Заселено мест' не может быть больше, чем 'Общее число мест'.");
            pricePerDay = 0;
            address = string.Empty;
            rating = 0;
            hasFreeWiFi = false;
            return false;
        }

        if (!SafeParseDecimal(_view.PricePerDayText.Trim(), "Оплата за день", out pricePerDay))
        {
            address = string.Empty;
            rating = 0;
            hasFreeWiFi = false;
            return false;
        }

        address = _view.AddressText.Trim();
        if (string.IsNullOrWhiteSpace(address))
        {
            _view.ShowError("Введите адрес гостиницы.");
            rating = 0;
            hasFreeWiFi = false;
            return false;
        }

        if (!SafeParseDouble(_view.RatingText.Trim(), "Рейтинг", out rating))
        {
            hasFreeWiFi = false;
            return false;
        }

        if (rating < 0 || rating > 10)
        {
            _view.ShowError("Неверное значение для 'Рейтинг'. Введите число от 0 до 10.");
            hasFreeWiFi = false;
            return false;
        }

        hasFreeWiFi = _view.HasFreeWiFiChecked;
        return true;
    }

    private bool SafeParseInt(string text, string fieldName, out int result)
    {
        text = text.Trim();
        try
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                _view.ShowError($"Введите значение для '{fieldName}'.");
                result = 0;
                return false;
            }

            result = checked(int.Parse(text));
            if (result < 0)
            {
                _view.ShowWarning($"Для '{fieldName}' введите число ≥ 0.");
                result = 0;
                return false;
            }

            return true;
        }
        catch (OverflowException)
        {
            result = 0;
            throw new HotelOverflowException(fieldName, text);
        }
        catch (FormatException)
        {
            _view.ShowError($"Неверный формат числа в '{fieldName}'.");
            result = 0;
            return false;
        }
    }

    private bool SafeParseDecimal(string text, string fieldName, out decimal result)
    {
        text = text.Trim();
        try
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                _view.ShowError($"Введите значение для '{fieldName}'.");
                result = 0;
                return false;
            }

            result = checked(decimal.Parse(text));
            if (result < 0)
            {
                _view.ShowWarning($"Для '{fieldName}' введите число ≥ 0.");
                result = 0;
                return false;
            }

            return true;
        }
        catch (OverflowException)
        {
            result = 0;
            throw new HotelOverflowException(fieldName, text);
        }
        catch (FormatException)
        {
            _view.ShowError($"Неверный формат числа в '{fieldName}'.");
            result = 0;
            return false;
        }
    }

    private bool SafeParseDouble(string text, string fieldName, out double result)
    {
        text = text.Trim();
        try
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                _view.ShowError($"Введите значение для '{fieldName}'.");
                result = 0;
                return false;
            }

            result = double.Parse(text);
            return true;
        }
        catch (OverflowException)
        {
            result = 0;
            throw new HotelOverflowException(fieldName, text);
        }
        catch (FormatException)
        {
            _view.ShowError($"Неверный формат числа в '{fieldName}'.");
            result = 0;
            return false;
        }
    }
}

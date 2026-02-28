using System.Runtime.InteropServices;

namespace Lab2;

/// <summary>
/// Класс формы
/// </summary>
public partial class Form1 : Form
{
    private Hotel _hotel;
    private Hotel? _editingHotel = null;

    private HotelsHashtableCollection hotels;
    private HotelsCollectionListener? listener;


    // Импорт MessageBoxW из библиотеки user32.dll, строки передаются как Unicode
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern int MessageBoxW(IntPtr hWnd, string text, string caption, uint type);

    private const uint MB_OK = 0x00000000; // кнопка ОК
    private const uint MB_ICONERROR = 0x00000010; // красная иконка ошибки
    private const uint MB_ICONWARNING = 0x00000030; // желтая иконка предупреждения

    /// <summary>
    /// Метод для вызова сообщения с ошибкой 
    /// </summary>
    /// <param name="message"> Выводимое сообщение </param>
    /// <param name="title"> Сообщение в шапке </param>
    private void ShowUser32Error(string message, string title = "Ошибка")
    {
        MessageBoxW(this.Handle, message, title, MB_OK | MB_ICONERROR);
    }

    /// <summary>
    /// Метод для вызова сообщения с предупреждением
    /// </summary>
    /// <param name="message"> Выводимое сообщение </param>
    /// <param name="title"> Сообщение в шапке </param>
    private void ShowUser32Warning(string message, string title = "Предупреждение")
    {
        MessageBoxW(this.Handle, message, title, MB_OK | MB_ICONWARNING);
    }

    /// <summary>
    /// Конструктор формы
    /// </summary>
    public Form1()
    {
        InitializeComponent();
        
        hotels = new HotelsHashtableCollection();
        listener = new HotelsCollectionListener(hotels);
    
        // Подписка на автообновление UI
        hotels.Changed += (_, e) => this.Invoke(new Action(() => RenderHotels()));

        MessageBox.Show(this,
            "Лабораторная №1 - Вариант 9 (Гостиница)\n\nГруппа 24ВП1 - Студенты: Бояркин Максим и Мишин Артём",
            "Привет!!");
        
        MinimumSize = new Size(700, 400);
        RenderHotels();
        // tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
    }

    /// <summary>
    /// Метод для создания гостиницы
    /// </summary>
    /// <param name="sender"> Ссылка на объект, вызвавшего событие </param>
    /// <param name="e"> Дополнительные сведения о событии </param>
    private void buttonCreate_Click(object sender, EventArgs e)
    {
        try
        {
            if (!TryGetHotelFromForm(out string name, out int occupiedRooms, out int totalRooms, 
                    out decimal pricePerDay, out string address, out double rating, out bool hasFreeWiFi))
                return;

            Hotel newHotel = new Hotel(name, occupiedRooms, totalRooms, pricePerDay, address, rating, hasFreeWiFi);
            hotels.Add(newHotel);  // Добавит + события сработают автоматически
        
            ClearFormFields();
        }
        catch (HotelOverflowException ex)
        {
            ShowUser32Error(ex.Message);
        }
        catch (Exception ex)
        {
            ShowUser32Warning(ex.Message);
        }
    }

    private void AddTestRow(string operation, long htTime, long listTime)
    {
        var item = new ListViewItem(operation);
        item.SubItems.Add($"{htTime} мс");
        item.SubItems.Add($"{listTime} мс");
        item.SubItems.Add($"{listTime - htTime:+0;-0} мс");
        listView1.Items.Add(item);
    }

    /// <summary>
    /// Обработка нажатия кнопки выхода
    /// </summary>
    /// <param name="sender"> Ссылка на объект, вызвавшего событие</param>
    /// <param name="e"> Дополнительные сведения о событии </param>
    private void buttonExit_Click(object sender, EventArgs e)
    {
        try
        {
            Close();
        }
        catch (Exception ex)
        {
            ShowUser32Error($"Ошибка закрытия приложения: {ex.Message}");
        }
    }

    
    /// <summary>
    /// Обработка нажатия кнопки редактирования
    /// </summary>
    /// <param name="sender"> Ссылка на объект, вызвавшего событие</param>
    /// <param name="e"> Дополнительные сведения о событии </param>
    private void buttonApply_Click(object sender, EventArgs e)
    {
        try
        {
            if (_editingHotel == null) return;

            string oldName = _editingHotel.Name;  // Старый ключ

            if (!TryGetHotelFromForm(out string name, out int occupiedRooms, out int totalRooms, 
                    out decimal pricePerDay, out string address, out double rating, out bool hasFreeWiFi))
                return;

            // Если имя не меняется — просто обновляем объект по старому ключу
            if (name == oldName)
            {
                _editingHotel.Name = name;  // Сеттер есть!
                _editingHotel.OccupiedRooms = occupiedRooms;
                _editingHotel.TotalRooms = totalRooms;
                _editingHotel.PricePerDay = pricePerDay;
                _editingHotel.Address = address;
                _editingHotel.Rating = rating;
                _editingHotel.HasFreeWiFi = hasFreeWiFi;

                // Обновляем в коллекции (заменяем объект по ключу)
                hotels[oldName] = _editingHotel;
                // Можно добавить событие "Updated" в будущем
            }
            else
            {
                // Имя поменялось: удаляем старый, создаём новый
                hotels.Remove(oldName);  // Событие Removed сработает

                Hotel updatedHotel = new Hotel(name, occupiedRooms, totalRooms, pricePerDay, address, rating, hasFreeWiFi);
                hotels.Add(updatedHotel);  // Событие Added сработает
            }

            ExitEditMode();
            RenderHotels();  // Авто-обновление уже есть через подписку
        }
        catch (HotelOverflowException ex)
        {
            ShowUser32Error(ex.Message);
        }
        catch (Exception ex)
        {
            ShowUser32Error(ex.Message);
        }
    }


    /// <summary>
    /// Обработка нажатия кнопки отмены редактирования
    /// </summary>
    /// <param name="sender"> Ссылка на объект, вызвавшего событие</param>
    /// <param name="e"> Дополнительные сведения о событии </param>
    private void buttonCancel_Click(object sender, EventArgs e)
    {
        try
        {
            ExitEditMode();
        }
        catch (Exception ex)
        {
            ShowUser32Error($"Ошибка отметы действия: {ex.Message}");
        }
    }

    /// <summary>
    /// Метод выхода из режима редактирования
    /// </summary>
    private void ExitEditMode()
    {
        _editingHotel = null;

        ClearFormFields();

        buttonCreate.Visible = true;
        buttonApply.Visible = false;
        buttonCancel.Visible = false;
    }

    /// <summary>
    /// Метод для рендера списка гостиниц
    /// </summary>
    private void RenderHotels()
    {
        if (flowHotels?.InvokeRequired == true)
        {
            flowHotels.Invoke(new Action(RenderHotels));
            return;
        }

        flowHotels.Controls.Clear();
        if (hotels == null) return;
    
        foreach (Hotel hotel in hotels.Values)  // Теперь из вашей Hashtable!
        {
            
            int btnPadding = 8;
            Panel card = new Panel();
            card.Width = 250;
            card.Height = 200;
            card.BorderStyle = BorderStyle.FixedSingle;
            card.Margin = new Padding(0, 0, 0, 10);

// INFO — МЕНЬШЕ высота, чтобы не лезло на кнопки
            Label info = new Label();
            info.Text = hotel.ToString();
            info.AutoSize = false;
            info.Size = new Size(card.Width - 20, 120);  // Было 95 → 85px (WiFi влезет!)
            info.Location = new Point(8, 8);
            info.Font = new Font("Segoe UI", 8.5F);  // Шрифт чуть меньше
            info.TextAlign = ContentAlignment.TopLeft;  // Выравнивание текста
            card.Controls.Add(info);

// Кнопка ИЗМЕНИТЬ (справа внизу)
            Button btnEdit = new Button();
            btnEdit.Text = "Изменить";
            btnEdit.TextAlign = ContentAlignment.MiddleCenter;
            btnEdit.Size = new Size(100, 28);
            btnEdit.Location = new Point(card.Width - btnEdit.Width - 12, card.Height - btnEdit.Height - btnPadding);
            btnEdit.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            btnEdit.BackColor = Color.LightBlue;
            btnEdit.FlatStyle = FlatStyle.Flat;
            btnEdit.Click += (s, e) => StartEditHotel(hotel);
            card.Controls.Add(btnEdit);

            
// Кнопка УДАЛЕНИЯ (слева внизу)
            Button btnDelete = new Button();
            btnDelete.Text = "Удалить";
            btnDelete.TextAlign = ContentAlignment.MiddleCenter;
            btnDelete.Size = new Size(80, 28);
            btnDelete.Location = new Point(8, card.Height - btnDelete.Height - btnPadding);  // 170-28-8=134
            btnDelete.BackColor = Color.LightCoral;
            btnDelete.FlatStyle = FlatStyle.Flat;
            btnDelete.Click += (s, e) => {
                if (MessageBox.Show($"Удалить '{hotel.Name}'?", "Подтверждение", 
                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    hotels.Remove(hotel.Name);
                }
            };
            card.Controls.Add(btnDelete);

            flowHotels.Controls.Add(card);

        }
    }


    /// <summary>
    /// Метод для редактирования гостиницы
    /// </summary>
    /// <param name="hotel"> Редактируемая гостиница </param>
    private void StartEditHotel(Hotel hotel)
    {
        try
        {
            _editingHotel = hotel;

            textBoxName.Text = hotel.Name;
            textBoxOccupiedRooms.Text = hotel.OccupiedRooms.ToString();
            textBoxTotalRooms.Text = hotel.TotalRooms.ToString();
            textBoxPricePerDay.Text = hotel.PricePerDay.ToString();
            textBoxAddress.Text = hotel.Address;
            textBoxRating.Text = hotel.Rating.ToString();
            checkBoxHasFreeWiFi.Checked = hotel.HasFreeWiFi;

            buttonCreate.Visible = false;
            buttonApply.Visible = true;
            buttonCancel.Visible = true;
        }
        catch (HotelOverflowException ex)
        {
            ShowUser32Error(ex.Message);
        }
        catch (Exception ex)
        {
            ShowUser32Error($"Ошибка редактирования гостиницы: {ex.Message}");
        }
    }

    /// <summary>
    /// Метод для очистки формы
    /// </summary>
    private void ClearFormFields()
    {
        try
        {
            textBoxName.Clear();
            textBoxOccupiedRooms.Clear();
            textBoxTotalRooms.Clear();
            textBoxPricePerDay.Clear();
            textBoxAddress.Clear();
            textBoxRating.Clear();
            checkBoxHasFreeWiFi.Checked = false;
        }
        catch (Exception ex)
        {
            ShowUser32Error($"Ошибка очистки формы: {ex.Message}");
        }
    }

    /// <summary>
    /// Метод для валидации полей формы
    /// </summary>
    /// <param name="name"> Название </param>
    /// <param name="occupiedRooms"> Занятые места </param>
    /// <param name="totalRooms"> Количество мест </param>
    /// <param name="pricePerDay"> Стоимость за день </param>
    /// <param name="address"> Адрес </param>
    /// <param name="rating"> Рейтинг </param>
    /// <param name="hasFreeWiFi"> Наличие бесплатного WiFi </param>
    /// <returns> Успешная валидация или нет </returns>
    private bool TryGetHotelFromForm(out string name, out int occupiedRooms, out int totalRooms,
        out decimal pricePerDay, out string address, out double rating,
        out bool hasFreeWiFi)
    {
        try
        {
            name = textBoxName.Text.Trim();
            if (string.IsNullOrWhiteSpace(name))
            {
                ShowUser32Error("Введите название гостиницы.");
                textBoxName.Select();
                occupiedRooms = totalRooms = 0;
                pricePerDay = 0;
                address = "";
                rating = 0;
                hasFreeWiFi = false;
                return false;
            }

            if (!SafeParseInt(textBoxOccupiedRooms.Text.Trim(), "Заселено мест", out occupiedRooms))
            {
                textBoxOccupiedRooms.Select();
                totalRooms = 0;
                pricePerDay = 0;
                address = "";
                rating = 0;
                hasFreeWiFi = false;
                return false;
            }

            if (!SafeParseInt(textBoxTotalRooms.Text.Trim(), "Общее число мест", out totalRooms))
            {
                textBoxTotalRooms.Select();
                pricePerDay = 0;
                address = "";
                rating = 0;
                hasFreeWiFi = false;
                return false;
            }

            if (occupiedRooms > totalRooms)
            {
                ShowUser32Error("'Заселено мест' не может быть больше, чем 'Общее число мест'.");
                textBoxOccupiedRooms.Select();
                pricePerDay = 0;
                address = "";
                rating = 0;
                hasFreeWiFi = false;
                return false;
            }

            if (!SafeParseDecimal(textBoxPricePerDay.Text.Trim(), "Оплата за день", out pricePerDay))
            {
                textBoxPricePerDay.Select();
                address = "";
                rating = 0;
                hasFreeWiFi = false;
                return false;
            }

            address = textBoxAddress.Text.Trim();
            if (string.IsNullOrWhiteSpace(address))
            {
                ShowUser32Error("Введите адрес гостиницы.");
                textBoxAddress.Select();
                rating = 0;
                hasFreeWiFi = false;
                return false;
            }

            if (!SafeParseDouble(textBoxRating.Text.Trim(), "Рейтинг", out rating))
            {
                textBoxRating.Select();
                hasFreeWiFi = false;
                return false;
            }

            if (rating < 0 || rating > 10)
            {
                ShowUser32Error("Неверное значение для 'Рейтинг'. Введите число от 0 до 10.");
                textBoxRating.Select();
                hasFreeWiFi = false;
                return false;
            }

            hasFreeWiFi = checkBoxHasFreeWiFi.Checked;
            return true;
        }
        catch (Exception ex)
        {
            ShowUser32Error($"Ошибка ввода: {ex.Message}");
            name = "";
            occupiedRooms = totalRooms = 0;
            pricePerDay = 0;
            address = "";
            rating = 0;
            hasFreeWiFi = false;
            return false;
        }
    }

    /// <summary>
    /// Метод для безопасного парсинга string в int
    /// </summary>
    /// <param name="text"> Строка </param>
    /// <param name="fieldName"> Название поля </param>
    /// <param name="result"> Результат парсинга </param>
    /// <returns> Статус выполнения </returns>
    private bool SafeParseInt(string text, string fieldName, out int result)
    {
        text = text.Trim();
        try
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                ShowUser32Error($"Введите значение для '{fieldName}'."); // user32.dll
                result = 0;
                return false;
            }
            
            result = checked(int.Parse(text));

            if (result < 0)
            {
                ShowUser32Warning($"Для '{fieldName}' введите число ≥ 0.");
                result = 0;
                return false;
            }

            return true;
        }
        catch (OverflowException)
        {
            var customEx = new HotelOverflowException(fieldName, text);
            System.Windows.Forms.MessageBox.Show(this, customEx.Message); // System.Windows.Forms!
            result = 0;
            return false;
        }
        catch (FormatException)
        {
            ShowUser32Error($"Неверный формат числа в '{fieldName}'."); // user32.dll
            result = 0;
            return false;
        }
    }

    /// <summary>
    /// Метод для безопасного парсинга string в decimal
    /// </summary>
    /// <param name="text"> Строка </param>
    /// <param name="fieldName"> Название поля </param>
    /// <param name="result"> Результат парсинга </param>
    /// <returns> Статус выполнения </returns>
    private bool SafeParseDecimal(string text, string fieldName, out decimal result)
    {
        text = text.Trim();
        try
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                ShowUser32Error($"Введите значение для '{fieldName}'."); // user32.dll
                result = 0;
                return false;
            }

            result = checked(decimal.Parse(text));

            if (result < 0)
            {
                ShowUser32Warning($"Для '{fieldName}' введите число ≥ 0."); // user32.dll
                result = 0;
                return false;
            }

            return true;
        }
        catch (OverflowException)
        {
            var customEx = new HotelOverflowException(fieldName, text);
            System.Windows.Forms.MessageBox.Show(this, customEx.Message); // System.Windows.Forms!
            result = 0;
            return false;
        }
        catch (FormatException)
        {
            ShowUser32Error($"Неверный формат числа в '{fieldName}'."); // user32.dll
            result = 0;
            return false;
        }
    }

    /// <summary>
    /// Метод для безопасного парсинга string в double
    /// </summary>
    /// <param name="text"> Строка </param>
    /// <param name="fieldName"> Название поля </param>
    /// <param name="result"> Результат парсинга </param>
    /// <returns> Статус выполнения </returns>
    private bool SafeParseDouble(string text, string fieldName, out double result)
    {
        text = text.Trim();
        try
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                ShowUser32Error($"Введите значение для '{fieldName}'."); // user32.dll
                result = 0;
                return false;
            }

            result = double.Parse(text);

            return true;
        }
        catch (OverflowException)
        {
            var customEx = new HotelOverflowException(fieldName, text);
            System.Windows.Forms.MessageBox.Show(this, customEx.Message); // System.Windows.Forms!
            result = 0;
            return false;
        }
        catch (FormatException)
        {
            ShowUser32Error($"Неверный формат числа в '{fieldName}'."); // user32.dll
            result = 0;
            return false;
        }
    }

    private void buttonTest_Click(object sender, EventArgs e)
    {
        try
        {
            // Очистка и настройка колонок
            listView1.Items.Clear();
            listView1.View = View.Details;
            listView1.Columns.Clear();
            listView1.Columns.Add("Операция", 150);
            listView1.Columns.Add("Hashtable", 120);
            listView1.Columns.Add("Array", 100);
            listView1.Columns.Add("Разница", 100);

            // ✅ РЕАЛЬНЫЕ ТЕСТЫ (5-10 сек)
            var hotels = PerformanceTest.GenerateUniqueHotels(100000);
            var names = hotels.Select(h => h.Name).ToList();

            // 1. ВСТАВКА
            var htInsert = new HotelsHashtableCollection();
            var listInsert = new List<Hotel>();
            long htInsertTime = PerformanceTest.MeasureInsert(htInsert, hotels);
            long listInsertTime = PerformanceTest.MeasureInsert(listInsert, hotels);
            AddTestRow("Вставка 100k", htInsertTime, listInsertTime);

            // 2. ПОСЛЕДОВАТЕЛЬНАЯ ВЫБОРКА
            var htSeq = new HotelsHashtableCollection();
            var listSeq = new List<Hotel>();
            PerformanceTest.InsertUniqueHotels(htSeq, hotels);
            PerformanceTest.InsertUniqueHotels(listSeq, hotels);
            long htSeqTime = PerformanceTest.MeasureSeqGet(htSeq);
            long listSeqTime = PerformanceTest.MeasureSeqListGet(listSeq);
            AddTestRow("Посл. выборка", htSeqTime, listSeqTime);

            // 3. СЛУЧАЙНАЯ ВЫБОРКА
            long htRandTime = PerformanceTest.MeasureRandGet(htSeq, names);
            long listRandTime = PerformanceTest.MeasureRandListGet(listSeq, names);
            AddTestRow("Случай. выборка", htRandTime, listRandTime);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Ошибка теста: {ex.Message}\n\nПроверьте:\n1. Добавлен Clear() в HotelsHashtableCollection\n2. PerformanceTest.cs скомпилирован", "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
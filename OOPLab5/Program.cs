namespace OOPLab5;

class Program
{
    static List<Satellite> arr = new List<Satellite>();
    static int maxCount;
    static Random rnd = new Random();

    static void Main(string[] args)
    {
        
        maxCount = ReadValidInt("Введіть максимальну кількість супутників: ", 1, int.MaxValue);
        
        bool isRunning = true;
        while (isRunning)
        {
            Console.WriteLine("1 – Додати об'єкт\n" +
                              "2 – Переглянути всі об'єкти\n" +
                              "3 – Знайти об'єкт\n" +
                              "4 – Продемонструвати поведінку\n" +
                              "5 – Видалити об'єкт\n" +
                              "6 – Продемонструвати static-методи\n" +
                              "0 – Вийти з програми");
            Console.Write("Виберіть пункт меню: ");

            string choice = Console.ReadLine();
            Console.WriteLine();
            
            switch (choice)
            {
                case "1": AddSatellite(); break;
                case "2": DisplaySatellites(arr); break;
                case "3": SearchSatellites(); break;
                case "4": DemonstrateSatellites(); break;
                case "5": DeleteSatellite(); break;
                case "6": DemonstrateStaticMethods(); break;
                case "0":
                    Console.WriteLine("Вихід з програми");
                    isRunning = false;
                    break;
                default:
                    Console.WriteLine("Введіть знов");
                    break;
            }
        }
    }

    static int ReadValidInt(string message, int min, int max)
    {
        while (true)
        {
            Console.Write(message);
            if (int.TryParse(Console.ReadLine(), out int res) && res >= min && res <= max)
                return res;
            Console.WriteLine($"Введіть ціле число в діапазоні {min} - {max}");
        }
    }

    static double ReadValidDouble(string message, double min, double max)
    {
        while (true)
        {
            Console.Write(message);
            if (double.TryParse(Console.ReadLine(), out double res) && res >= min && res <= max)
                return res;
            Console.WriteLine($"Введіть дійсне число в діапазоні {min} - {max}");
        }
    }

    static void AddSatelliteHandle()
    {
        string name;
        while (true)
        {
            Console.Write("Введіть назву (3-15 символів): ");
            name = Console.ReadLine();
            if (name.Length >= 3 && name.Length <= 15)
                break;
            Console.WriteLine("Назва має містити від 3 до 15 символів");
        }

        Console.WriteLine("Виберіть тип супутника: 1-Communication, 2-Weather, 3-Scientific, 4-Military");
        int typeVal = ReadValidInt("Введіть номер типу (1-4): ", 1, 4);
        SType type = (SType)typeVal;

        double altitude = ReadValidDouble("Введіть висоту орбіти у км: ", 160.0, 40000.0);

        DateTime launchDate;
        DateTime minDate = new DateTime(1957, 10, 4);
        while (true)
        {
            Console.Write("Введіть дату запуску (формат dd.MM.yyyy): ");
            if (DateTime.TryParseExact(Console.ReadLine(), "dd.MM.yyyy",
                    System.Globalization.CultureInfo.InvariantCulture,
                    System.Globalization.DateTimeStyles.None, out launchDate)
                && launchDate >= minDate && launchDate <= DateTime.Now)
                break;
            Console.WriteLine($"Введіть дату у форматі dd.MM.yyyy в діапазоні {minDate:dd.MM.yyyy} - {DateTime.Now:dd.MM.yyyy}");
        }

        Satellite s = new Satellite(name, type, altitude, launchDate);
        arr.Add(s);
        Console.WriteLine("Супутник успішно додано\n");
    }

    static void AddSatellite()
    {
        if (arr.Count >= maxCount)
        {
            Console.WriteLine($"Досягнуто максимальної кількості супутників ({maxCount}). Додавання неможливе.\n");
            return;
        }

        Console.WriteLine("Виберіть режим додавання: \n" +
                          "1 - Ввести дані вручну (повний конструктор) \n" +
                          "2 - Створити тестовий супутник за замовченням\n" +
                          "3 - Випадковий конструктор\n" +
                          "4 - Ввести рядком (TryParse)");
        Console.Write("Виберіть пункт меню: ");
        string choice = Console.ReadLine();

        if (choice == "1")
        {
            AddSatelliteHandle();
        }

        if (choice == "2")
        {
            arr.Add(new Satellite
            {
                Name = $"Experimental {Satellite.ExpCount}",
                Type = SType.Communication,
                OrbitAltitude = 400,
                LaunchDate = DateTime.Now
            });
            Console.WriteLine("Супутник за замовченням додано\n");
            return;
        }

        if (choice == "3")
        {
            int num = rnd.Next(1, 4);
            switch (num)
            {
                case 1:
                    Console.WriteLine("Використано повний конструктор");
                    AddSatelliteHandle();
                    break;
                case 2:
                    Console.WriteLine("Використано конструктор за замовчуванням");
                    arr.Add(new Satellite
                    {
                        Name = $"Experimental {Satellite.ExpCount}",
                        Type = SType.Communication,
                        OrbitAltitude = 400,
                        LaunchDate = DateTime.Now
                    });
                    break;
                case 3:
                    Console.WriteLine("Використано неповний конструктор");
                    string name;
                    while (true)
                    {
                        Console.Write("Введіть назву (3-15 символів): ");
                        name = Console.ReadLine();
                        if (name.Length >= 3 && name.Length <= 15)
                            break;
                        Console.WriteLine("Назва має містити від 3 до 15 символів");
                    }

                    Console.WriteLine("Виберіть тип супутника: 1-Communication, 2-Weather, 3-Scientific, 4-Military");
                    int typeVal = ReadValidInt("Введіть номер типу (1-4): ", 1, 4);
                    SType type = (SType)typeVal;
                    arr.Add(new Satellite(name, type));
                    break;
            }
        }
        if (choice == "4")
        {
            Console.Write("Введіть дані супутника (назва; тип; висота; дата): ");
            string input = Console.ReadLine();

            if (Satellite.TryParse(input, out Satellite s))
            {
                arr.Add(s);
                Console.WriteLine("Супутник успішно додано\n");
            }
            else
            {
                Console.WriteLine("Некоректний формат або значення — супутник не додано\n");
            }
        }
    }

    static void DisplaySatellites(List<Satellite> list)
    {
        if (list.Count == 0)
        {
            Console.WriteLine("Жодного супутника не знайдено");
            return;
        }

        Console.WriteLine($"Всього супутників: {Satellite.Count}");
        Console.WriteLine($"Всього експерементальних супутників: {Satellite.ExpCount}");
        Console.WriteLine($"Всього витрачено палива: {Satellite.UsedFuel} %");
        
        Console.WriteLine(new string('-', 110));
        Console.WriteLine($"| {"№",-3} | {"Назва",-12} | {"Тип",-13} | {"Орбіта (км)",-12} " +
                          $"| {"Дата запуску",-12} | {"Пальне",-8} | {"Стан",-6} | {"Категорія",-15} |");
        Console.WriteLine(new string('-', 110));

        for (int i = 0; i < list.Count; i++)
        {
            var s = list[i];
            Console.WriteLine($"| {i + 1,-3} | {s.Name,-12} | {s.Type,-13} | {s.OrbitAltitude,-12} " +
                              $"| {s.LaunchDate,-12:dd.MM.yyyy} | {s.FuelLevel,-8} | {(s.IsActive ? "On" : "Off"),-6} | {s.OrbitCategory,-15} |");
        }
        Console.WriteLine(new string('-', 110));
    }

    static void SearchSatellites()
    {
        if (arr.Count == 0) { Console.WriteLine("Список порожній"); return; }
        Console.WriteLine("Виберіть характеристику для пошуку: \n1 - За типом супутника \n2 - За назвою");
        Console.Write("Виберіть пункт меню: ");
        string opt = Console.ReadLine();
        List<Satellite> results = new List<Satellite>();

        if (opt == "1")
        {
            int typeVal = ReadValidInt("Введіть тип (1-4): ", 1, 4);
            SType type = (SType)typeVal;
            foreach (Satellite s in arr)
                if (s.Type == type) results.Add(s);
        }
        else if (opt == "2")
        {
            Console.Write("Введіть назву (або її частину): ");
            string query = Console.ReadLine().ToLower();
            foreach (Satellite s in arr)
                if (s.Name.ToLower().Contains(query)) results.Add(s);
        }
        else
        {
            Console.WriteLine("Некоректний вибір характеристики");
            return;
        }

        Console.WriteLine("\nРезультати пошуку");
        DisplaySatellites(results);
    }

    static void DemonstrateSatellites()
    {
        if (arr.Count == 0) { Console.WriteLine("Список порожній"); return; }
        DisplaySatellites(arr);
        int index = ReadValidInt("Введіть номер супутника для взаємодії: ", 1, arr.Count);
        Satellite selected = arr[index - 1];

        Console.WriteLine($"\nКерування супутником '{selected.Name}'");
        Console.WriteLine("1 - Передати дані\n2 - Перемкнути живлення\n3 - Змінити висоту орбіти\n4 - Симуляція відхилення по днях");
        Console.Write("Виберіть пункт меню: ");
        string subChoice = Console.ReadLine();
        Console.WriteLine();

        switch (subChoice)
        {
            case "1":
                try { Console.WriteLine(selected.GetData()); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                break;
            case "2":
                try { Console.WriteLine(selected.SwitchPower()); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                break;
            case "3":
                int choice = ReadValidInt("1 - Звичайна \n2 - Термінова \n3 - За замовчуванням \nВибір: ", 1, 3);
                Console.WriteLine();
                switch (choice)
                {
                    case 1:
                        double change = ReadValidDouble("Введіть зміну орбіти у км (від -500 до 500): ", -500, 500);
                        try { Console.WriteLine(selected.AdjustAltitude(change)); }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                    case 2:
                        double changee = ReadValidDouble("Введіть зміну орбіти у км (від -500 до 500): ", -500, 500);
                        try { Console.WriteLine(selected.AdjustAltitude(changee, true)); }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                    case 3:
                        try { Console.WriteLine(selected.AdjustAltitude()); }
                        catch (Exception ex) { Console.WriteLine(ex.Message); }
                        break;
                }
                break;
            case "4":
                int days = ReadValidInt("Введіть кількість днів для симуляції (від 0 до 300): ", 0, 300);
                try { Console.WriteLine(selected.LeaveTimePassing(days)); }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
                break;
        }
    }

    static void DeleteSatellite()
    {
        if (arr.Count == 0) { Console.WriteLine("Список порожній"); return; }
        Console.WriteLine("Виберіть спосіб видалення:\n1 - За порядковим номером у таблиці\n2 - За характеристикою (тип)\n");
        string opt = Console.ReadLine();

        if (opt == "1")
        {
            DisplaySatellites(arr);
            int index = ReadValidInt("Введіть номер об'єкта для видалення: ", 1, arr.Count) - 1;
            string removedName = arr[index].Name;
            arr.RemoveAt(index);
            Console.WriteLine($"Об'єкт '{removedName}' успішно видалено");
        }
        else if (opt == "2")
        {
            int typeVal = ReadValidInt("Введіть тип (1-4): ", 1, 4);
            SType targetType = (SType)typeVal;

            int removedCount = 0;
            for (int i = arr.Count - 1; i >= 0; i--)
            {
                if (arr[i].Type == targetType) { arr.RemoveAt(i); removedCount++; }
            }

            if (removedCount > 0)
                Console.WriteLine($"Успішно видалено {removedCount} об'єкт(ів) типу {targetType}");
            else
                Console.WriteLine("Об'єктів із вказаною характеристикою не знайдено");
        }
        else
        {
            Console.WriteLine("Некоректний вибір");
        }
    }
    static void DemonstrateStaticMethods()
    {
        Console.WriteLine($"Всього супутників: {Satellite.Count}");
        Console.WriteLine($"Всього експерементальних супутників: {Satellite.ExpCount}");
        Console.WriteLine($"Всього витрачено палива: {Satellite.UsedFuel} %");

        double from = ReadValidDouble("Введіть першу висоту (км): ", 160, 40000);
        double to = ReadValidDouble("Введіть другу висоту (км): ", 160, 40000);

        try
        {
            Console.WriteLine(Satellite.CalculateFuel(from, to));
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
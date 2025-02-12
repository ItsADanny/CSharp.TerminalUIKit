using System.Globalization;

public static class TerminalUIKit {
    public static int IntSelector(string header=null, int minValue=1, int maxValue=5, int startInAmount=999999999, string icon=null, int step=1) {
        bool done = false;
        int returnValue = 0;
        if (startInAmount == 999999999) {
            startInAmount = minValue;
        }
        int selectedAmount = startInAmount;
        int pos = 0;
        
        do {
            Console.Clear();

            if (header is not null) {
                Console.WriteLine(header);
            }
            
            string row = "";

            if (icon is not null) {
                string icons = "";
                for (int x = 0; x != selectedAmount; x++) {
                    icons += icon;
                }
                Console.WriteLine(icons);
                
                row += "   ";
            }

            for (int x = 0; x != 3; x++) {
                if (x == pos) {
                    row += "\x1b[44m";
                }

                if (x == 0) {
                    row += "<\x1b[49m";
                } else if (x == 2) {
                    row += ">\x1b[49m";
                } else {
                    row += $" {selectedAmount} ";
                }
            }
            Console.WriteLine(row);

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.LeftArrow:
                    if (pos == 2) {
                        pos = 0;
                    } else {
                        pos = 2;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    if (pos == 2) {
                        pos = 0;
                    } else {
                        pos = 2;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (pos == 0) {
                        if (selectedAmount > minValue) {
                            selectedAmount -= step;
                        }
                    } else {
                        if (selectedAmount < maxValue) {
                            selectedAmount += step;
                        }
                    }
                    break;
                case ConsoleKey.Enter:
                    done = true;
                    returnValue = selectedAmount;
                    break;
            }
        } while (!done);

        return returnValue;
    }

    public static TimeOnly TimeSelector(string Header, string startTime="00:00", string minValue="00:01", string maxValue="24:00") {
        TimeOnly time = TimeOnly.FromDateTime(DateTime.ParseExact(startTime, "HH:mm", CultureInfo.InvariantCulture));
        bool Done = false;
        string errorMessage = null;
        int posY = 0;
        int posX = 0;

        do {
            Console.Clear();
            Console.WriteLine(Header);

            if (errorMessage is not null) {
                Console.WriteLine(errorMessage);
                errorMessage = null;
            }

            for (int y = 0; y != 3; y++) {
                string row = "";
                if (y == 0) {
                    for (int x = 0; x != 5; x++) {
                        if (x == 2) {
                            row += " ";
                        } else {
                            if (posX == x & posY == y) {
                                row += "\x1b[44m";
                            }
                            row += "↑\x1b[49m";
                        }
                    }
                } else if (y == 1) {
                    row = time.ToString("HH:mm");
                } else {
                    for (int x = 0; x != 5; x++) {
                        if (x == 2) {
                            row += " ";
                        } else {
                            if (posX == x & posY == y) {
                                row += "\x1b[44m";
                            }
                            row += "↓\x1b[49m";
                        }
                    }
                }
                Console.WriteLine(row);
            }

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (posY == 2) {
                        posY = 0;
                    } else {
                        posY = 2;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (posY == 0) {
                        posY = 2;
                    } else {
                        posY = 0;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    posX--;
                    if (posX == 2) {
                        posX--;
                    }
                    if (posX < 0) {
                        posX = 5;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    posX++;
                    if (posX == 2) {
                        posX++;
                    }
                    if (posX > 5) {
                        posX = 0;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    Dictionary<int, string> dict_posHourMinute = new Dictionary<int, string> {
                            {0, "H"}, {1, "H"},
                            {2, "S"},
                            {3, "M"}, {4, "M"}
                    };

                    if (dict_posHourMinute[posX] == "H") {
                            if (posY == 0) {
                                if (posX == 0) {
                                    time = time.AddHours(10);
                                } else {
                                    time = time.AddHours(1);
                                }
                            } else {
                                if (posX == 0) {
                                    time = time.AddHours(-10);
                                } else {
                                    time = time.AddHours(-1);
                                }
                            }
                    } else if (dict_posHourMinute[posX] == "M") {
                        if (posY == 0) {
                                if (posX == 3) {
                                    time = time.AddMinutes(10);
                                } else {
                                    time = time.AddMinutes(1);
                                }
                            } else {
                                if (posX == 3) {
                                    time = time.AddMinutes(-10);
                                } else {
                                    time = time.AddMinutes(-1);
                                }
                            }
                    }
                    break;
                case ConsoleKey.C:
                    time = TimeOnly.FromDateTime(DateTime.ParseExact(startTime, "HH:mm", CultureInfo.InvariantCulture));
                    break;
                case ConsoleKey.Enter:
                    Done = true;
                    break;
            }

        } while (!Done);

        return time;
    }

    public static DateOnly DateSelector(string Header=null, string Footer=null, bool CanGoIntoThePast=false) {
        string str_date = DateTime.Now.ToString("dd/MM/yyyy");
        string selectedDate = null;
        string errorMessage = null;
        int posY = 0;
        int posX = 0;

        do {
            Console.Clear();
            if (Header is not null) {
                Console.WriteLine(Header);
            }

            if (errorMessage is not null) {
                Console.WriteLine(errorMessage);
                errorMessage = null;
            }

            for (int y = 0; y != 3; y++) {
                string row = "";
                if (y == 0) {
                    for (int x = 0; x != str_date.Length; x++) {
                        if (x == 2 | x == 5) {
                            row += " ";
                        } else {
                            if (posX == x & posY == y) {
                                row += "\x1b[44m";
                            }
                            row += "↑\x1b[49m";
                        }
                    }
                } else if (y == 1) {
                    row = str_date;
                } else {
                    for (int x = 0; x != str_date.Length; x++) {
                        if (x == 2 | x == 5) {
                            row += " ";
                        } else {
                            if (posX == x & posY == y) {
                                row += "\x1b[44m";
                            }
                            row += "↓\x1b[49m";
                        }
                    }
                }
                Console.WriteLine(row);
            }

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (posY == 2) {
                        posY = 0;
                    } else {
                        posY = 2;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (posY == 0) {
                        posY = 2;
                    } else {
                        posY = 0;
                    }
                    break;
                case ConsoleKey.LeftArrow:
                    posX--;
                    if (posX == 2 | posX == 5) {
                        posX--;
                    }
                    if (posX < 0) {
                        posX = 9;
                    }
                    break;
                case ConsoleKey.RightArrow:
                    posX++;
                    if (posX == 2 | posX == 5) {
                        posX++;
                    }
                    if (posX > 9) {
                        posX = 0;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    try {
                        Dictionary<int, string> dict_posDayMonthYear = new Dictionary<int, string> {
                            {0, "D"}, {1, "D"},
                            {2, "S"},
                            {3, "M"}, {4, "M"},
                            {5, "S"},
                            {6, "Y"}, {7, "Y"}, {8, "Y"}, {9, "Y"}
                        };

                        DateTime date = DateTime.ParseExact(str_date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        switch (dict_posDayMonthYear[posX]) {
                            case "D":
                                if (posY == 0) {
                                    if (posX == 0) {
                                        date = date.AddDays(10);
                                    } else {
                                        date = date.AddDays(1);
                                    }
                                } else {
                                    if (posX == 0) {
                                        date = date.AddDays(-10);
                                    } else {
                                        date = date.AddDays(-1);
                                    }
                                }
                                break;
                            case "M":
                                if (posY == 0) {
                                    if (posX == 3) {
                                        date = date.AddMonths(10);
                                    } else {
                                        date = date.AddMonths(1);
                                    }
                                } else {
                                    if (posX == 3) {
                                        date = date.AddMonths(-10);
                                    } else {
                                        date = date.AddMonths(-1);
                                    }
                                }
                                break;
                            case "Y":
                                if (posY == 0) {
                                    if (posX == 6) {
                                        date = date.AddYears(1000);
                                    } else if (posX == 7) {
                                        date = date.AddYears(100);
                                    } else if (posX == 8) {
                                        date = date.AddYears(10);
                                    } else {
                                        date = date.AddYears(1);
                                    }
                                } else {
                                    if (posX == 6) {
                                        date = date.AddYears(-1000);
                                    } else if (posX == 7) {
                                        date = date.AddYears(-100);
                                    } else if (posX == 8) {
                                        date = date.AddYears(-10);
                                    } else {
                                        date = date.AddYears(-1);
                                    }
                                }
                                break;
                        }
                        if (!CanGoIntoThePast) {
                            if (date < DateTime.Now) {
                                errorMessage = "You can't select a date in the past, please select a date in the future";
                                date = DateTime.Now;
                            }
                        }
                        
                        str_date = date.ToString("dd/MM/yyyy");
                    }
                    catch (System.Exception)
                    {
                        errorMessage = "You tried to select an invalid date, please select a date";
                    }
                    break;
                case ConsoleKey.C:
                    str_date = DateTime.Now.ToString("dd/MM/yyyy");
                    break;
                case ConsoleKey.Enter:
                    selectedDate = str_date;
                    break;
            }
        } while (selectedDate is null);

        return DateOnly.FromDateTime(DateTime.ParseExact(selectedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture));
    }

    public static Dictionary<string, bool> CheckBoxSelector(string Header=null, string Footer=null, List<string> options_string=null) {
        if (options_string is null) throw new Exception("No options inputted, to use this function please add options to select");

        List<bool> options_bool = new ();
        for (int i = 0; i != options_string.Count(); i++) {
            options_bool.Add(false);
        }

        bool done = false;
        int pos = 0;

        do {
            Console.Clear();
            
            if (Header is not null) {
                Console.WriteLine(Header);
            }

            for (int y = 0; y != options_string.Count(); y++) {
                string row = "";
                if (pos == y) {
                    row += "\x1b[44m>";
                } else {
                    row += " ";
                }

                if (options_bool[y]) {
                    row += " [X] ";
                } else {
                    row += " [ ] ";
                }

                row += $" {options_string[y]}\x1b[49m";
                Console.WriteLine(row);
            }

            if (Footer is not null) {
                Console.WriteLine(Footer);
            }

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (pos <= 0) {
                        pos = options_string.Count() - 1;
                    } else {
                        pos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pos >= (options_string.Count() - 1)) {
                        pos = 0;
                    } else {
                        pos += 1;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (options_bool[pos]) {
                        options_bool[pos] = false;
                    } else {
                        options_bool[pos] = true;
                    }
                    break;
                case ConsoleKey.Enter:
                    done = true;
                    break;
            }
        } while (!done);

        int curr_pos = 0;
        Dictionary<string, bool> returnDict = new ();
        foreach (string option in options_string) {
            returnDict.Add(option, options_bool[curr_pos]);
            curr_pos++;
        }
        return returnDict;
    }

    public static Dictionary<string, bool> CheckBoxSelector(string Header=null, string Footer=null, Dictionary<string, bool> options=null) {
        if (options is null) throw new Exception("No options inputted, to use this function please add options to select");

        List<string> options_string = new ();
        List<bool> options_bool = new ();
        foreach (var KeyPairValueItem in options) {
            options_string.Add(KeyPairValueItem.Key);
            options_bool.Add(KeyPairValueItem.Value);
        }

        bool done = false;
        int pos = 0;

        do {
            Console.Clear();

            if (Header is not null) {
                Console.WriteLine(Header);
            }
            
            for (int y = 0; y != options_string.Count(); y++) {
                string row = "";
                if (pos == y) {
                    row += "\x1b[44m>";
                } else {
                    row += " ";
                }

                if (options_bool[y]) {
                    row += " [X] ";
                } else {
                    row += " [ ] ";
                }

                row += $" {options_string[y]}\x1b[49m";
                Console.WriteLine(row);
            }

            if (Footer is not null) {
                Console.WriteLine(Footer);
            }
                
            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (pos <= 0) {
                        pos = options_string.Count() - 1;
                    } else {
                        pos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pos >= (options_string.Count() - 1)) {
                        pos = 0;
                    } else {
                        pos += 1;
                    }
                    break;
                case ConsoleKey.Spacebar:
                    if (options_bool[pos]) {
                        options_bool[pos] = false;
                    } else {
                        options_bool[pos] = true;
                    }
                    break;
                case ConsoleKey.Enter:
                    done = true;
                    break;
            }
        } while (!done);

        int curr_pos = 0;
        Dictionary<string, bool> returnDict = new ();
        foreach (string option in options_string) {
            returnDict.Add(option, options_bool[curr_pos]);
            curr_pos++;
        }
        return returnDict;
    }

    public static int OptionSelector(string Header=null, string Footer=null, List<string> options=null) {
        if (options is null) throw new Exception("No options inputted, to use this function please add options to select");

        int selectedOption = 999999999;
        int pos = 0;

        do {
            Console.Clear();
            if (Header is not null) {
                Console.WriteLine(Header);
            }
            
            for (int y = 0; y != options.Count(); y++) {
                string row = "";
                if (pos == y) {
                    row += "\x1b[44m>";
                } else {
                    row += " ";
                }

                row += $" {options[y]}\x1b[49m";
                Console.WriteLine(row);
            }

            if (Footer is not null) {
                Console.WriteLine(Footer);
            }

            var input = Console.ReadKey();
            switch (input.Key) {
                case ConsoleKey.UpArrow:
                    if (pos <= 0) {
                        pos = options.Count() - 1;
                    } else {
                        pos -= 1;
                    }
                    break;
                case ConsoleKey.DownArrow:
                    if (pos >= (options.Count() - 1)) {
                        pos = 0;
                    } else {
                        pos += 1;
                    }
                    break;
                case ConsoleKey.Enter:
                    selectedOption = pos;
                    break;
            }
        } while (selectedOption == 999999999);
        return selectedOption;
    }
}
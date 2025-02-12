static class Program {
    public static string footer = "=========================\nExamples made by ItsDanny\n=========================";

    static void Main() {
        bool Done = false;
        string header = "=========================\nTerminalUIKit UI examples\n=========================";
        List<string> UIOptions = ["Option selector example", "Int selector example", "Int selector (with Emoji) example", "Date selector", "Time selector", "Checkbox selector", "Checkbox selector (Dictionary)", "Exit"];

        while (!Done) {
            switch (TerminalUIKit.OptionSelector(header, footer, UIOptions)) {
                case 0:
                    UseOptionSelector();
                    break;
                case 1:
                    UseIntSelector();
                    break;
                case 2:
                    UseIntSelectorWithEmoji();
                    break;
                case 3:
                    UseDateSelector();
                    break;
                case 4:
                    UseTimeSelector();
                    break;
                case 5:
                    UseCheckBoxSelector();
                    break;
                case 6:
                    UseCheckBoxSelector_Dict();
                    break;
                case 7:
                    Done = true;
                    Console.WriteLine("Closing down program, thank you for using this example!");
                    break;
            }
        } 
    }

    static void UseOptionSelector() {
        List<string> options = ["Option 1", "Option 2", "Option 3", "Option 4", "Option 5", "Option 6", "Option 7"];
        string header = "=========================\nOption selector - Example\n=========================";
        int returnValue = TerminalUIKit.OptionSelector(header, footer, options);
        Console.WriteLine($"Selected option: {options[returnValue]}");
        Thread.Sleep(2500);
    }

    static void UseCheckBoxSelector() {
        List<string> options = new List<string>(){
            "Is the cake a lie?",
            "Is the Companion Cube alive?",
            "Do you like Portal references?",
            "Is this usefull for you?",
            "Did you check/uncheck everything you wanted?"
        };

        string header = "=====================================\nCheckBox selector (Dictionary) - Example\n=====================================";
        Dictionary<string, bool> returnValue = TerminalUIKit.CheckBoxSelector(header, footer, options);
        
        foreach (var KeyPairValueItem in returnValue) {
            Console.WriteLine($"Key: {KeyPairValueItem.Key}, Value: {KeyPairValueItem.Value}");
        }

        Thread.Sleep(2500);
    }

    static void UseCheckBoxSelector_Dict() {
        Dictionary<string, bool> options = new Dictionary<string, bool>(){
            {"Is the cake a lie?", true},
            {"Is the Companion Cube alive?", false},
            {"Do you like Portal references?", true},
            {"Is this usefull for you?", false},
            {"Did you check/uncheck everything you wanted?", false}
        };

        string header = "=====================================\nCheckBox selector (Dictionary) - Example\n=====================================";
        Dictionary<string, bool> returnValue = TerminalUIKit.CheckBoxSelector(header, footer, options);
        
        foreach (var KeyPairValueItem in returnValue) {
            Console.WriteLine($"Key: {KeyPairValueItem.Key}, Value: {KeyPairValueItem.Value}");
        }

        Thread.Sleep(2500);
    }

    static void UseIntSelector() {
        string header = "======================\nInt selector - Example\n======================";
        int returnValue = TerminalUIKit.IntSelector(header, 1, 5, 3, null, 1);
        Console.WriteLine($"Selected integer amount: {returnValue}");
        Thread.Sleep(2500);
    }

    static void UseIntSelectorWithEmoji() {
        string header = "===================================\nInt selector (With Emoji) - Example\n===================================";
        int returnValue = TerminalUIKit.IntSelector(header, 1, 5, 3, "⭐️", 1);
        string SelectedStarAmount = "";
        for (int i = 0; i != returnValue; i++) {
            SelectedStarAmount += "⭐️";
        } 
        Console.WriteLine($"Selected integer amount: {returnValue}, Stars: {SelectedStarAmount}");
        Thread.Sleep(2500);
    }

    static void UseDateSelector() {
        string header = "";
        DateOnly date = TerminalUIKit.DateSelector(header);
        Console.WriteLine($"Selected date: {date}");
        Thread.Sleep(2500);
    }

    static void UseTimeSelector() {
        string header = "";
        TimeOnly returnValue = TerminalUIKit.TimeSelector(header);
        Console.WriteLine($"Selected time: {returnValue}");
        Thread.Sleep(2500);
    }
}
using System.Runtime.Serialization;

namespace MathGame
{
    class MathGame
    {
        static string[] mainMenuLines =
        {
        "Math Game",
        "",
        "",
        "Created by Miguel NL",
        "",
        ""
        };
        static Dictionary<string, string> startMenuValues = new Dictionary<string, string>()
    {
        {"Start Game","start" },
        {"Settings", "settings" },
        {"Exit", "exit" }
    };
        static Dictionary<string, string> difficultyMenuValues = new Dictionary<string, string>()
    {
        {"Easy", "1" },
        {"Medium", "0.75" },
        {"Hard", "0.5" }
    };

        static void Main(string[] args)
        {
            //Create Menus Needed 
            AnyKeyMenu MainMenu = new AnyKeyMenu(mainMenuLines);
            OptionMenu StartMenu = new OptionMenu(startMenuValues);
            MainGame Game = new MainGame();
            //Game State Variables
            double curDifficulty = 1.00;
            ToggleMenu difficultyToggleMenu = new ToggleMenu(difficultyMenuValues, curDifficulty);
            MainMenu.Ask();
            bool killGame = false;
            while (!killGame)
            {
                string res = StartMenu.Ask();
                int settingsres;
                switch (res)
                {
                    case "start":
                        Game.Run(curDifficulty);
                        break;
                    case "settings":
                        settingsres = difficultyToggleMenu.Ask();
                        curDifficulty = double.Parse(difficultyMenuValues.Values.ToArray()[settingsres]);
                        break;
                    case "exit":
                        killGame = true;
                        break;
                }
            }

        }
    }
}

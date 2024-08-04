using System.Security.Cryptography.X509Certificates;
using System.Windows.Markup;

namespace MathGame
{
    public class OptionMenu
    {
        private Dictionary<string, string> valueDictionary = new Dictionary<string, string>();
        public OptionMenu(Dictionary<string, string> input)
        {
            valueDictionary = input;
        }
        public string[] GetLines()
        {
            return valueDictionary.Keys.ToArray();
        }
        public string[] GetValues()
        {
            return valueDictionary.Values.ToArray();
        }
        public string Ask()
        {
            string[] lines = valueDictionary.Keys.ToArray();
            string[] values = valueDictionary.Values.ToArray();
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < lines.Length; i++)
                {
                    Console.WriteLine("[" + i + "] " + lines[i]);
                }

                string read = Console.ReadLine().Trim();
                int readInt;

                if (int.TryParse(read, out readInt))
                {
                    try
                    {
                        return values[readInt];

                    }
                    catch (Exception e)
                    {

                    }
                }
            }

        }
    }

    public class AnyKeyMenu
    {
        private string[] valueDictionary;
        public AnyKeyMenu(string[] input)
        {
            valueDictionary = input;
        }
        public void Ask()
        {
            for (int i = 0; i < valueDictionary.Length; i++)
            {
                Console.WriteLine(valueDictionary[i]);
            }
            Console.WriteLine("\nPress Any Key To Continue . . .");
            Console.ReadKey(true);
        }
    }

    public class ToggleMenu
    {
        private Dictionary<string, string> valueDictionary = new Dictionary<string, string>();
        private string currentValue;
        private string[] values;
        private string[] lines;
        private int currentIndex;

        public ToggleMenu(Dictionary<string, string> inp, double curVal)
        {
            valueDictionary = inp;
            currentValue = curVal.ToString();
            values = valueDictionary.Values.ToArray();
            lines = valueDictionary.Keys.ToArray();
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == currentValue)
                {
                    currentIndex = i;
                    break;
                }
            }
        }
        public int GetCurIndex(string val)
        {
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] == val)
                {
                    return i;
                }
            }
            return -1;
        }
        public int GetPrevIndex(int val)
        {
            if (val <= 0)
            {
                return values.Length - 1;
            }
            else
            {
                return val - 1;
            }
        }
        public int GetNextIndex(int val)
        {
            if (val >= values.Length - 1)
            {
                return 0;
            }
            else
            {
                return val + 1;
            }
        }

        public string[] GetLines() { return valueDictionary.Keys.ToArray(); }
        public string[] GetValues() { return valueDictionary.Values.ToArray(); }
        public int Ask()
        {
            int curIndex = GetCurIndex(currentValue);
            int prevIndex = GetPrevIndex(curIndex);
            int nextIndex = GetNextIndex(curIndex);
            while (true)
            {
                Console.Clear();
                prevIndex = GetPrevIndex(curIndex);
                nextIndex = GetNextIndex(curIndex);
                Console.WriteLine("Currently Set to: " + lines[currentIndex]);
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i == curIndex)
                    {
                        Console.WriteLine(">>> " + lines[i]);
                    }
                    else
                    {
                        Console.WriteLine(lines[i]);
                    }
                }
                Console.WriteLine("\nUse UP/DOWN Arrows or W/S to move Selection, Space to Select, and Enter to Apply and Return\n");
                ConsoleKeyInfo key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.W:
                        curIndex = prevIndex;
                        break;
                    case ConsoleKey.S:
                        curIndex = nextIndex;
                        break;
                    case ConsoleKey.UpArrow:
                        curIndex = prevIndex;
                        break;
                    case ConsoleKey.DownArrow:
                        curIndex = nextIndex;
                        break;
                    case ConsoleKey.Enter:
                        return curIndex;
                    case ConsoleKey.Spacebar:
                        currentValue = values[curIndex];
                        currentIndex = curIndex;
                        break;

                    default:
                        break;
                }

            }



        }
    }


}
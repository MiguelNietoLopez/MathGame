
using System.Data;

namespace MathGame
{
    class MainGame
    {
        double gameDifficulty;
        char[] charArray = { '+', '-', '*', '/' };
        double additionOdds;
        double subtractionOdds;
        double multOdds;
        double divOdds;
        double sqrtOdds;
        int maxDigits;
        bool allowNegativeSub;
        public MainGame()
        {

        }
        private string GenAdd()
        {
            int num1Size = new Random().Next(1, maxDigits + 1);
            int num2Size = new Random().Next(1, maxDigits + 1);
            int num1 = 0;
            int num2 = 0;

            string res = "";
            for (int i = 0; i < num1Size; i++)
            {
                res += new Random().Next(0, 10);
            }
            num1 = int.Parse(res);
            res = "";
            for (int i = 0; i < num2Size; i++)
            {
                res += new Random().Next(0, 10);
            }
            num2 = int.Parse(res);

            return $" {num1} + {num2} ";
        }
        private string GenSub()
        {
            int num1Size = new Random().Next(1, maxDigits + 1);
            int num2Size = new Random().Next(1, maxDigits + 1);
            string num1 = "";
            string num2 = "";
            for (int i = 0; i < num1Size; i++)
            {
                num1 += new Random().Next(0, 10);
            }
            for (int i = 0; i < num2Size; i++)
            {
                num2 += new Random().Next(0, 10);
            }
            if (allowNegativeSub)
            {
                return $" {num1} - {num2} ";
            }
            else
            {
                if (int.Parse(num1) > int.Parse(num2))
                {
                    return $" {num1} - {num2} ";
                }
                else
                {
                    return $" {num2} - {num1} ";
                }
            }
        }
        private string GenDiv()
        {
            string num1 = "";
            string num2 = "";
            for (int i = 0; i < new Random().Next(1, maxDigits + 1); i++)
            {
                int numTemp = new Random().Next(1, 10);
                num1 += numTemp;
            }
            num2 = "" + int.Parse(num1) * new Random().Next(1, 10);

            return $" {num2} / {num1} ";
        }
        private string GenMult()
        {
            string num1 = "";
            string num2 = "";

            for (int i = 0; i < new Random().Next(1, maxDigits + 1); i++)
            {
                num1 += new Random().Next(0, 10);
            }
            for (int i = 0; i < new Random().Next(1, maxDigits + 1); i++)
            {
                num2 += new Random().Next(0, 10);
            }

            return $" {num1} x {num2} ";

        }
        private string GenSqrt()
        {
            int[] squares = { 4, 9, 16, 25, 36, 49, 64, 81, 100 };

            return $"SQRT({new Random().Next(0, squares.Length)})";
        }
        private void SetGameOdds(double diff)
        {
            switch (diff)
            {
                case 1:
                    additionOdds = 0.4;
                    subtractionOdds = 0.2;
                    multOdds = 0.3;
                    divOdds = 0.1;
                    sqrtOdds = 0;
                    maxDigits = 1;
                    allowNegativeSub = false;
                    break;
                case 0.75:
                    additionOdds = 0.3;
                    subtractionOdds = 0.3;
                    multOdds = 0.2;
                    divOdds = 0.2;
                    sqrtOdds = 0;
                    maxDigits = 2;
                    allowNegativeSub = false;
                    break;
                case 0.5:
                    additionOdds = 0.1;
                    subtractionOdds = 0.1;
                    multOdds = 0.2;
                    divOdds = 0.3;
                    sqrtOdds = 0.3;
                    allowNegativeSub = true;
                    break;
            }


        }
        private string SelProbType()
        {
            Random ran = new Random();
            while (true)
            {
                if (ran.NextDouble() <= additionOdds)
                {
                    return "add";
                }
                else if (ran.NextDouble() <= multOdds)
                {
                    return "mult";
                }
                else if (ran.NextDouble() <= subtractionOdds)
                {
                    return "sub";
                }
                else if (ran.NextDouble() <= divOdds)
                {
                    return "div";
                }
                else if (ran.NextDouble() <= sqrtOdds)
                {
                    return "sqrt";
                }
            }
        }

        public  void Run(double difficulty)
        {
            Console.Clear();
            gameDifficulty = difficulty;
            int score = 0;
            string curProblem = "";
            DataTable table = new DataTable();
            bool ansWrong = false;

            SetGameOdds(gameDifficulty);

            while (!ansWrong)
            {


                Console.WriteLine("#############");
                Console.WriteLine("# MATH GAME #");
                Console.WriteLine("#############");
                Console.WriteLine("");
                Console.WriteLine($"Score: {score}");
                switch (SelProbType())
                {
                    case "add":
                        curProblem = GenAdd();
                        break;
                    case "sub":
                        curProblem = GenSub();
                        break;
                    case "div":
                        curProblem = GenDiv();
                        break;
                    case "mult":
                        curProblem = GenMult();
                        break;
                    case "sqrt":
                        curProblem = GenSqrt();
                        break;
                }


                while (true)
                {
                    Console.WriteLine(curProblem);
                    string reply = Console.ReadLine().Trim();
                    try
                    {
                        int repInt;
                        int answer = Convert.ToInt16(table.Compute(curProblem.Replace('x', '*'), null));

                        try
                        {
                            repInt = int.Parse(reply);
                            if (repInt == answer)
                            {
                                score++;
                                break;
                            }
                            else
                            {
                                ansWrong = true;
                                break;
                            }

                        }
                        catch
                        {
                            continue;
                        }


                    }
                    catch
                    {
                        if (curProblem.Split('(')[0] == "SQRT")
                        {
                            int sqrt = (int)Math.Sqrt(int.Parse(curProblem.Split('(')[1].Split(')')[0]));
                            if (sqrt == int.Parse(reply))
                            {
                                Console.WriteLine("pass");
                                score++;
                                break;
                            }
                            else
                            {
                                ansWrong = true;
                                break;
                            }
                        }
                        continue;
                    }
                }

                Console.Clear();

            }

            Console.WriteLine("########################\n");
            Console.WriteLine("Sorry Wrong Answer :((");
            Console.WriteLine("");
            Console.WriteLine($"You got {score} right!");
            Console.WriteLine("########################\n");
            Console.WriteLine("Press Any Key to Continue...");
            Console.ReadKey(true);
            return;
        }
    }
}

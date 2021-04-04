using System;
using System.Collections.Generic;

namespace Calculator_and_Cars
{
    class Calculator
    {
        public enum Operations { Add = 1, Substr = 2, Mult = 3, Div = 4 };
        private Operations operation;
        private Func<double> func;
        public void SetOperation(Operations operation)
        {
            this.operation = operation;
        }
        public double Calculate(double number1, double number2)
        {
            if ((int)operation == 1)
            {
                Console.Write("Add = ");
                func = () => { return number1 + number2; };
            }
            else if ((int)operation == 2)
            {
                Console.Write("Substr = ");
                func = () => { return number1 - number2; };
            }
            else if ((int)operation == 3)
            {
                Console.Write("Mult = ");
                func = () => { return number1 * number2; };
            }
            else
            {
                if (number2 == 0)
                {
                    throw new Exception("Denom is 0");
                }
                Console.Write("Div = ");
                func = () => { return number1 / number2; };
            }
            return func.Invoke();
        }
    }
    abstract class Car
    {
        public string Name { get; set; }
        Random random = new Random();
        public delegate void Move(string name, int x);
        public delegate void Finish(string name);
        public event Move move;
        public event Finish finish;

        public int number;
        public int Number
        {
            get => number;
            set
            {
                number = value;
            }
        }
        public byte speed;
        public byte Speed { get => speed; }


        public void Go()
        {
            speed = (byte)random.Next(0, 11);
            if (Number + speed >= 100)
            {
                move?.Invoke(Name, Number);
                finish?.Invoke(Name);

                Number = 100;
            }
            else
            {
                Number += speed;
                move?.Invoke(Name, Number);
                Go();
            }

        }
    }
    class SportCar : Car
    {
        public SportCar()
        {
            this.Name = "SportCar";
        }
    }
    class Racing : Car
    {
        public Racing()
        {
            this.Name = "Racing";
        }
    }
    class Sedan : Car
    {
        public Sedan()
        {
            this.Name = "Sedan";
        }
    }
    class Game
    {
        public List<Car> cars = new List<Car>();
        public void GameStart()
        {
            foreach (var item in cars)
            {
                item.finish += this.Finish;
                item.move += this.Move;
            }
            foreach (var item in cars)
            {
                item.Go();
            }
        }
        public void Move(string name, int number)
        {
            Console.WriteLine($"Car {name} has started: {number}");
        }
        public void Finish(string name)
        {
            Console.WriteLine($"Car {name} is finished");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Calculator calculator = new Calculator();
            calculator.SetOperation(Calculator.Operations.Add);
            Console.WriteLine($"{calculator.Calculate(5, 5)}");
            Console.WriteLine("\n\n======CAR======");
            Car car = new Racing();
            Game game = new Game();
            game.cars.Add(car);
            game.cars.Add(new Sedan());
            game.cars.Add(new Racing());
            game.GameStart();
        }
    }
}

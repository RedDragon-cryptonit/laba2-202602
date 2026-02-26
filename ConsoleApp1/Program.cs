using System;
using System.Collections.Generic;

// Базовый абстрактный класс Animal
abstract class Animal
{
    public string Name { get; set; }
    public int Age { get; set; }
    public string Habitat { get; set; }
    public string DietType { get; set; }
    public double Weight { get; set; } // дополнительное поле
    public string Color { get; set; } // дополнительное поле

    protected Animal(string name, int age, string habitat, string dietType, double weight, string color)
    {
        Name = name;
        Age = age;
        Habitat = habitat;
        DietType = dietType;
        Weight = weight;
        Color = color;
    }

    public virtual string GetInfo()
    {
        return $"Кличка: {Name}, Возраст: {Age}, Среда: {Habitat}, Питание: {DietType}, Вес: {Weight} кг, Окрас: {Color}";
    }
}

// Класс Mammal (млекопитающее)
class Mammal : Animal
{
    public bool HasFur { get; set; }

    public Mammal(string name, int age, string habitat, string dietType, double weight, string color, bool hasFur)
        : base(name, age, habitat, dietType, weight, color)
    {
        HasFur = hasFur;
    }

    public override string GetInfo()
    {
        string furStatus = HasFur ? "есть" : "нет";
        return base.GetInfo() + $", Тип: Млекопитающее, Шерсть: {furStatus}";
    }
}

// Класс Bird (птица)
class Bird : Animal
{
    public double WingSpan { get; set; }

    public Bird(string name, int age, string habitat, string dietType, double weight, string color, double wingSpan)
        : base(name, age, habitat, dietType, weight, color)
    {
        WingSpan = wingSpan;
    }

    public override string GetInfo()
    {
        return base.GetInfo() + $", Тип: Птица, Размах крыльев: {WingSpan} м";
    }
}

// Класс Fish (рыба)
class Fish : Animal
{
    public string WaterType { get; set; }

    public Fish(string name, int age, string habitat, string dietType, double weight, string color, string waterType)
        : base(name, age, habitat, dietType, weight, color)
    {
        WaterType = waterType;
    }

    public override string GetInfo()
    {
        return base.GetInfo() + $", Тип: Рыба, Тип воды: {WaterType}";
    }
}

// Класс Reptile (пресмыкающееся)
class Reptile : Animal
{
    public bool IsVenomous { get; set; }

    public Reptile(string name, int age, string habitat, string dietType, double weight, string color, bool isVenomous)
        : base(name, age, habitat, dietType, weight, color)
    {
        IsVenomous = isVenomous;
    }

    public override string GetInfo()
    {
        string venomStatus = IsVenomous ? "ядовитое" : "неядовитое";
        return base.GetInfo() + $", Тип: Пресмыкающееся, Ядовитость: {venomStatus}";
    }
}

// Класс Amphibian (земноводное)
class Amphibian : Animal
{
    public string SkinMoisture { get; set; }

    public Amphibian(string name, int age, string habitat, string dietType, double weight, string color, string skinMoisture)
        : base(name, age, habitat, dietType, weight, color)
    {
        SkinMoisture = skinMoisture;
    }

    public override string GetInfo()
    {
        return base.GetInfo() + $", Тип: Земноводное, Влажность кожи: {SkinMoisture}";
    }
}

// Singleton-класс AnimalManager
class AnimalManager
{
    private static AnimalManager _instance;
    private List<Animal> _animals;

    private AnimalManager()
    {
        _animals = new List<Animal>();
    }

    public static AnimalManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new AnimalManager();
            }
            return _instance;
        }
    }

    public void AddAnimal(Animal animal)
    {
        _animals.Add(animal);
        Console.WriteLine($"Животное {animal.Name} успешно добавлено!");
    }

    public void DisplayAllAnimals()
    {
        if (_animals.Count == 0)
        {
            Console.WriteLine("В зоопарке пока нет животных.");
            return;
        }

        Console.WriteLine("\n--- Список всех животных ---");
        for (int i = 0; i < _animals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_animals[i].GetInfo()}");
        }
        Console.WriteLine();
    }

    public void DisplayAnimalByIndex(int index)
    {
        if (index >= 0 && index < _animals.Count)
        {
            Console.WriteLine(_animals[index].GetInfo());
        }
        else
        {
            Console.WriteLine("Животное с таким индексом не найдено.");
        }
    }

    public void DisplayAnimalByName(string name)
    {
        var animal = _animals.Find(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        if (animal != null)
        {
            Console.WriteLine(animal.GetInfo());
        }
        else
        {
            Console.WriteLine("Животное с такой кличкой не найдено.");
        }
    }
}

// Главный класс программы
class Program
{
    static void Main(string[] args)
    {
        // Создаём несколько животных
        var cat = new Mammal("Барсик", 5, "город", "хищник", 4.5, "полосатый", true);
        var eagle = new Bird("Орёл", 3, "горы", "хищник", 6.2, "коричневый", 2.1);
        var trout = new Fish("Форель", 2, "река", "хищник", 1.8, "серебристый", "пресная");
        var snake = new Reptile("Кобра", 4, "пустыня", "хищник", 3.0, "коричневый", true);
        var frog = new Amphibian("Лягушка", 1, "болото", "всеядное", 0.1, "зелёная", "высокая");

        // Добавляем в менеджер
        AnimalManager.Instance.AddAnimal(cat);
        AnimalManager.Instance.AddAnimal(eagle);
        AnimalManager.Instance.AddAnimal(trout);
        AnimalManager.Instance.AddAnimal(snake);
        AnimalManager.Instance.AddAnimal(frog);

        // Запускаем меню
        ShowMenu();
    }

    static void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("=== МЕНЮ УПРАВЛЕНИЯ ЖИВОТНЫМИ ===");
            Console.WriteLine("1. Показать всех животных");
            Console.WriteLine("2. Найти животное по индексу");
            Console.WriteLine("3. Найти животное по кличке");
            Console.WriteLine("4. Добавить новое животное");
            Console.WriteLine("5. Выйти");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    AnimalManager.Instance.DisplayAllAnimals();
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    break;
                case "2":
                    Console.Write("Введите индекс животного (начиная с 1): ");
                    if (int.TryParse(Console.ReadLine(), out int index) && index >= 1)
                    {
                        AnimalManager.Instance.DisplayAnimalByIndex(index - 1);
                    }
                    else
                    {
                        Console.WriteLine("Некорректный индекс. Должен быть положительным числом.");
                    }
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    break;
                case "3":
                    Console.Write("Введите кличку животного: ");
                    string name = Console.ReadLine();
                    AnimalManager.Instance.DisplayAnimalByName(name);
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    break;
                case "4":
                    AddNewAnimal();
                    break;
                case "5":
                    Console.WriteLine("Выход из программы.");
                    return;
                default:
                    Console.WriteLine("Неверный выбор. Пожалуйста, выберите пункт от 1 до 5.");
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    break;
            }
        }
        static void AddNewAnimal()
        {
            Console.WriteLine("\nВыберите тип животного:");
            Console.WriteLine("1. Млекопитающее");
            Console.WriteLine("2. Птица");
            Console.WriteLine("3. Рыба");
            Console.WriteLine("4. Пресмыкающееся");
            Console.WriteLine("5. Земноводное");
            Console.Write("Ваш выбор: ");

            string typeChoice = Console.ReadLine();

            switch (typeChoice)
            {
                case "1":
                    AddMammal();
                    break;
                case "2":
                    AddBird();
                    break;
                case "3":
                    AddFish();
                    break;
                case "4":
                    AddReptile();
                    break;
                case "5":
                    AddAmphibian();
                    break;
                default:
                    Console.WriteLine("Неверный тип животного.");
                    Console.WriteLine("Нажмите любую клавишу для продолжения...");
                    Console.ReadKey();
                    break;
            }
        }

        static void AddMammal()
        {
            Console.Write("Кличка: ");
            string name = Console.ReadLine();
            Console.Write("Возраст: ");
            int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Среда обитания: ");
            string habitat = Console.ReadLine();
            Console.Write("Тип питания: ");
            string dietType = Console.ReadLine();
            Console.Write("Вес (кг): ");
            double.TryParse(Console.ReadLine(), out double weight);
            Console.Write("Окрас: ");
            string color = Console.ReadLine();
            Console.Write("Есть шерсть? (да/нет): ");
            bool hasFur = Console.ReadLine().ToLower() == "да";

            var mammal = new Mammal(name, age, habitat, dietType, weight, color, hasFur);
            AnimalManager.Instance.AddAnimal(mammal);
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        static void AddBird()
        {
            Console.Write("Кличка: ");
            string name = Console.ReadLine();
            Console.Write("Возраст: ");
            int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Среда обитания: ");
            string habitat = Console.ReadLine();
            Console.Write("Тип питания: ");
            string dietType = Console.ReadLine();
            Console.Write("Вес (кг): ");
            double.TryParse(Console.ReadLine(), out double weight);
            Console.Write("Окрас: ");
            string color = Console.ReadLine();
            Console.Write("Размах крыльев (м): ");
            double.TryParse(Console.ReadLine(), out double wingSpan);

            var bird = new Bird(name, age, habitat, dietType, weight, color, wingSpan);
            AnimalManager.Instance.AddAnimal(bird);
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        static void AddFish()
        {
            Console.Write("Кличка: ");
            string name = Console.ReadLine();
            Console.Write("Возраст: ");
            int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Среда обитания: ");
            string habitat = Console.ReadLine();
            Console.Write("Тип питания: ");
            string dietType = Console.ReadLine();
            Console.Write("Вес (кг): ");
            double.TryParse(Console.ReadLine(), out double weight);
            Console.Write("Окрас: ");
            string color = Console.ReadLine();
            Console.Write("Тип воды (пресная/морская): ");
            string waterType = Console.ReadLine();

            var fish = new Fish(name, age, habitat, dietType, weight, color, waterType);
            AnimalManager.Instance.AddAnimal(fish);
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        static void AddReptile()
        {
            Console.Write("Кличка: ");
            string name = Console.ReadLine();
            Console.Write("Возраст: ");
            int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Среда обитания: ");
            string habitat = Console.ReadLine();
            Console.Write("Тип питания: ");
            string dietType = Console.ReadLine();
            Console.Write("Вес (кг): ");
            double.TryParse(Console.ReadLine(), out double weight);
            Console.Write("Окрас: ");
            string color = Console.ReadLine();
            Console.Write("Ядовитое? (да/нет): ");
            bool isVenomous = Console.ReadLine().ToLower() == "да";

            var reptile = new Reptile(name, age, habitat, dietType, weight, color, isVenomous);
            AnimalManager.Instance.AddAnimal(reptile);
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }

        static void AddAmphibian()
        {
            Console.Write("Кличка: ");
            string name = Console.ReadLine();
            Console.Write("Возраст: ");
            int.TryParse(Console.ReadLine(), out int age);
            Console.Write("Среда обитания: ");
            string habitat = Console.ReadLine();
            Console.Write("Тип питания: ");
            string dietType = Console.ReadLine();
            Console.Write("Вес (кг): ");
            double.TryParse(Console.ReadLine(), out double weight);
            Console.Write("Окрас: ");
            string color = Console.ReadLine();
            Console.Write("Влажность кожи: ");
            string skinMoisture = Console.ReadLine();

            var amphibian = new Amphibian(name, age, habitat, dietType, weight, color, skinMoisture);
            AnimalManager.Instance.AddAnimal(amphibian);
            Console.WriteLine("Нажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }
} // Закрытие класса Program

using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLB8
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Task1();
            //Task2();
            //Task3();
            //Task4();
            Task5();


        }

        //------------------------------------
        //Задание 1 Коллизия имен
        //------------------------------------
        public static void Task1() 
        {

            Console.WriteLine("--- Задание 1. Коллизия имен ---\n");

            // Способ 1 Склеивание
            Console.WriteLine("Способ 1 - Склеивание");
            CarGlued car1 = new CarGlued("Mercedes");
            car1.Move();
            car1.Service();
            car1.Stop();
            Console.WriteLine();


            // Способ 2 Явное указание интерфейса
            Console.WriteLine("Способ 2 -  Явное указание интерфейса");
            CarExplict car2 = new CarExplict("BMW");
            car2.Move();
            car2.Service();
            ((IMovable)car2).Stop();
            ((IServicable)car2).Stop();
            Console.WriteLine();

            // Способ 3 обертывание
            Console.WriteLine("Способ 3 - обертывание");
            CarWrepped car3 = new CarWrepped("Honda");
            car3.Move();
            car3.Service();

            car3.StopMovement();
            car3.StopService();
            Console.WriteLine();

            Console.WriteLine("--- Задание 1 завершено ---\n");

        }

        //------------------------------------
        // Задание 2. Обобщенные интерфейсы
        //------------------------------------
        static void Task2()
        {
            Console.WriteLine("--- Задание 2. Обобщенные интерфейсы ---\n");

            // Демонстрация ковариантности (out)
            Console.WriteLine("=== КОВАРИАНТНОСТЬ (out) ===");
            Console.WriteLine("Фабрика Car может быть присвоена фабрике Vehicle\n");

            // Создаем фабрику автомобилей
            CarFactory carFactory = new CarFactory();

            // Ковариантность: IVehicleFactory<Car> присваивается IVehicleFactory<Vehicle>
            // Это безопасно, т.к. Car является подтипом Vehicle
            IVehicleFactory<Vehicle> vehicleFactory = carFactory;

            Console.WriteLine("Создание через CarFactory:");
            Car car = carFactory.Produce();
            Console.WriteLine(car);
            car.Refuel(20);  // Метод из Car
            Console.WriteLine();

            Console.WriteLine("Создание через IVehicleFactory<Vehicle> (ковариантность):");
            Vehicle vehicle = vehicleFactory.Produce();  // Вернет Car
            Console.WriteLine(vehicle);
            Console.WriteLine();

            // Демонстрация что без out это было бы невозможно
            Console.WriteLine("ВАЖНО: Без 'out' присваивание было бы ошибкой компиляции!");
            Console.WriteLine("'out' гарантирует что T используется только на выходе\n");

            Console.WriteLine("--- Задание 2 завершено ---\n");
        }

        //------------------------------------
        // Задание 3. Работа с массивом
        //------------------------------------
        static void Task3() 
        {
            Console.WriteLine("--- Задание 3. Работа с массивом ---\n");

            IMovable[] movables = new IMovable[]
            {
                new Vehicle("Базовое ТС"),
                new Car("Легковой автомобиль"),
                new CarGlued("Авто со склейкой"),
                new Vehicle("Базовое ТС2"),
                new CarWrepped("Авто с оборткой")
            };

            Console.WriteLine("=== Цикл по массиву IMovable ===\n");
            for (int i = 0; i < movables.Length; i++) 
            {
                Console.WriteLine($"Элемент: {i}");
                // Выозов метода ToString
                Console.WriteLine($"ToString: {movables[i].ToString()}");

                if (movables[i] is IServicable servicable) 
                {
                    Console.WriteLine("Объект поддерживает IServicable");
                    Console.WriteLine($"Интервал обслуживани: {servicable.GetServiceInterval()}");
                    servicable.Service();
                }
                else 
                {
                    Console.WriteLine($"  НЕ поддерживает IServicable");
                }

                Console.WriteLine();
            }

            Console.WriteLine("--- Задание 3 завершено ---\n");
        }


        //------------------------------------
        // Задание 4. Использование стандартных интерфейсов
        //------------------------------------
        static void Task4()
        {
            Console.WriteLine("--- Задание 4. Использование стандартных интерфейсов ---\n");

            // Создаем массив Vehicle с дополнительными полями для сортировки
            VehicleComparable[] vehicles = new VehicleComparable[]
            {
                new VehicleComparable("Honda", 2015, 350),
                new VehicleComparable("BMW", 2020, 250),
                new VehicleComparable("Audi", 2018, 280),
                new VehicleComparable("Mercedes", 2019, 300),
                new VehicleComparable("Toyota", 2021, 200)
            };

            Console.WriteLine("Исходный массив:");
            PrintVehicles(vehicles);

            // СОРТИРОВКА 1: IComparable (по имени)
            Console.WriteLine("\n=== Сортировка через IComparable (по Name) ===");
            Array.Sort(vehicles);
            PrintVehicles(vehicles);

            // СОРТИРОВКА 2: IComparer (по году выпуска)
            Console.WriteLine("\n=== Сортировка через IComparer (по Year) ===");
            Array.Sort(vehicles, new VehicleYearComparer());
            PrintVehicles(vehicles);

            // СОРТИРОВКА 3: IComparer (по максимальной скорости)
            Console.WriteLine("\n=== Сортировка через IComparer (по MaxSpeed) ===");
            Array.Sort(vehicles, new VehicleSpeedComparer());
            PrintVehicles(vehicles);

            Console.WriteLine("\n--- Задание 4 завершено ---\n");
        }

        public static void PrintVehicles(VehicleComparable[] vehicles) 
        {
            foreach(var v in vehicles) 
                Console.WriteLine($" {v}"); 
        }

        static void Task5()
        {
            Console.WriteLine("--- Задание 5. Именованные итераторы ---");
            Console.WriteLine("Вариант 9: Значения факториала от start не больше end\n");

            FactorialIterator iterator = new FactorialIterator();

            Console.WriteLine("--- Пример 1: Факториалы от 1 до 1000000 ---");
            foreach (var item in iterator.GetFactorials(1, 1000000))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n--- Пример 2: Факториалы от 5 до 10000 ---");
            foreach (var item in iterator.GetFactorials(5, 10000))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n--- Пример 3: Факториалы от 0 до 100 ---");
            foreach (var item in iterator.GetFactorials(0, 100))
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("\n--- Задание 5 завершено ---\n");
        }

    }







    //------------------------------------
    //Задание 1 Коллизия имен
    //------------------------------------

            //Создаем интрефейсы , где буедт коллизия у метода Stop()
    interface IMovable 
    {
        void Move();
        void Stop();
        string GetStatus ();

    }

    interface IServicable 
    {
        void Service();
        void Stop();
        int GetServiceInterval();

    }

    //Создаем базовые классы 

    // Класс 1: Базовый класс транспортного средства с реализацией IMovable
    class Vehicle : IMovable 
    {
        public string Name { get; set; }
        protected bool isMoving ;

        public Vehicle(string name) 
        {
            Name = name;
            isMoving = false;
        }

       public void Move() 
        {
            isMoving = true;
            Console.WriteLine($"{Name}Начинает дважение");
        }

        public void Stop() 
        {
            isMoving=false;
            Console.WriteLine($"{Name}Прекращает движение");
        }

        public virtual string GetStatus() 
        {
            return isMoving ? "В движении" : "Стоит";
        }

        public override string ToString()
        {
            return $"[Vehicle] {Name}, статус: {GetStatus()}";
        }

    }

    //Класс 2: Станция обслуживания с реализацией IServicable
    class SerciceStation : IServicable 
    {
        public string StationName { get; set; }
        protected bool isServicing;

        public SerciceStation(string name) 
        {
            StationName = name;
            isServicing = false;
        }

        public void Service() 
        {
            isServicing = true;
            Console.WriteLine($"{StationName} Начинает обслуживание");
        }


       public void Stop() 
        {
            isServicing=false;
            Console.WriteLine($"{StationName} Заканчивает обслуживание");
        }

       public int GetServiceInterval() 
        {
            return 9000; //Каждые 9тыс км
        }

        public override string ToString()
        {
            return $"[SeviceStation]{StationName}, статус: {(isServicing? "обслуживает":"Свободна")}";
        }

    }

    // Класс для демонстриции склеивания 
    class CarGlued : Vehicle, IMovable, IServicable 
    {
        private bool isUnderService;

        public CarGlued(string name) : base(name) { }

        public new void Stop() 
        {
            isMoving = false;
            isUnderService = false;
            Console.WriteLine($"[CarGlued] {Name} Общая остановка движение + обслуживаение");
        }

        public void Service() 
        {
            isUnderService =true;
            Console.WriteLine($"[CarGlued] {Name} Начинаю обслуживание");
        }

        public int GetServiceInterval() 
        {
            return 15000;
        }

        public override string ToString()
        {
            return $"[CarGlued] {Name}, движение: {isMoving}, обслуживание: {isUnderService}";
        }

    }

    // Класс для демонстрации явного указания интерфейса
    class CarExplict : Vehicle, IMovable, IServicable 
    {
        private bool isUnderService;

        public CarExplict(string name) : base(name) { }

        void IMovable.Stop()
        {
            isMoving =false;
            Console.WriteLine($"[CarExplict]{Name} Остановка движения (IMovable)");
        }

        void IServicable.Stop()
        {
            isUnderService=false;
            Console.WriteLine($"[CarExplict]{Name} Остановка обслуживания (IServicable)");
        }

        public void Service() 
        {
            isUnderService = true;
            Console.WriteLine($"[CarExplict]{Name} Начинаю обслуживание");
        }

        public int GetServiceInterval() 
        {
            return 15000;
        }

        public override string ToString()
        {
            return $"[CarExplict] {Name}, движение: {isMoving}, обслуживание: {isUnderService}";
        }

    }

    // Класс для демонстрации обертки
    class CarWrepped : Vehicle, IMovable, IServicable 
    {
        private bool isUnderService;
        public CarWrepped(string name) : base(name) { }


        public void StopMovement() 
        {
            isMoving =false;
            Console.WriteLine($"[CarWrepped]{Name} Останавливаю движение(через обертку StopMovement)");
        }

        public void StopService() 
        {
            isUnderService = false;
            Console.WriteLine($"[CarWrepped]{Name} Останавливаю обслуживание(через обертку StopService)");
        }

        void IMovable.Stop()
        {
            StopMovement();
        }

        void IServicable.Stop()
        {
            StopService();
        }

        public void Service() 
        {
            isUnderService = true;
            Console.WriteLine($"[CarWrepped]{Name} Начинаю обслуживание");
        }

        public int GetServiceInterval() 
        {
            return 15000;
        }

        public override String ToString() 
        {
            return $"[CarWrepped] {Name}, движение: {isMoving}, обслуживание: {isUnderService}";
        }
    }


    //------------------------------------
    // Задание 2. Обобщенные интерфейсы
    //------------------------------------
    //Обобщенный интерфейс 
    interface IVehicleFactory<out T> 
    {
        T Produce();
    }

    class Car : Vehicle 
    {
        public int FuelLevel { get; set; }
        
        public Car (string name) : base (name) 
        {
            FuelLevel = 0;
        }

        public void Refuel(int fuel)
        {
            FuelLevel = fuel;
            Console.WriteLine($"{Name} заполнен до {FuelLevel}");
        }

        public override string ToString()
        {
            return ($"[Car]{Name}, статус {GetStatus()}, топливо: {FuelLevel}");
        }
    }

    class CarFactory : IVehicleFactory<Car> 
    {
        int counter = 0;
        public Car Produce() 
        {
            counter ++;
            Car car = new Car ($"Car - {counter}");
            return car;
        }
    }

    // ---------------------------------------------------------
    // ЗАДАНИЕ 4: Стандартные интерфейсы IComparable и IComparer
    // ---------------------------------------------------------

    class VehicleComparable : IComparable<VehicleComparable>
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public int MaxSpeed { get; set; }

        public VehicleComparable(string name,int year,int maxSpeed) 
        {
            Name = name;
            Year = year;
            MaxSpeed = maxSpeed;
        }

        // соритирова по умолчанию по Имени
        public int CompareTo(VehicleComparable other) 
        {
            if (other  == null) return 1;
            return Name.CompareTo(other.Name);
        }

        public override string ToString()
        {
            return $"{Name} (год: {Year}, максимальная скорость: {MaxSpeed})";
        }
    }

    // Создание класса компоратора для сравниения по году
    class VehicleYearComparer : IComparer<VehicleComparable> 
    {
        public int Compare(VehicleComparable x , VehicleComparable y) 
        {
            if (ReferenceEquals(x, y)) return 0;  // оба null или один и тот же объект
            if (x == null) return -1;             // null меньше не-null
            if (y == null) return 1;              // не-null больше null
            return x.Year.CompareTo(y.Year); // от меньшего к большему 
            return y.Year.CompareTo(x.Year); // от большего к меньшему
        }
    }

    // Создание класса компоратора для сравнения по максимально скорости 
    class VehicleSpeedComparer : IComparer<VehicleComparable> 
    {
        public int Compare(VehicleComparable x , VehicleComparable y) 
        {
            if (ReferenceEquals(x, y))return 0;
            if (x == null) return -1;
            if (y == null) return 1;
            //return x.MaxSpeed.CompareTo(y.MaxSpeed); // от меньшего к большему 
            return y.MaxSpeed.CompareTo(x.MaxSpeed); // от большего к меньшему 
        }
    }


    class FactorialIterator
    {
        // Именованный итератор - возвращает факториалы от start! до значения, не превышающего end
        public IEnumerable<string> GetFactorials(int start, long end)
        {
            // Валидация входных данных
            if (start < 0)
            {
                yield return "Ошибка: start не может быть отрицательным";
                yield break;
            }

            if (end < 0)
            {
                yield return "Ошибка: end не может быть отрицательным";
                yield break;
            }

            long factorial = 1;

            // Вычисляем факториал для start
            for (int i = 1; i <= start; i++)
            {
                factorial *= i;
            }

            // Если уже стартовое значение больше end
            if (factorial > end)
            {
                yield return $"Факториал {start}! = {factorial} уже превышает {end}";
                yield break;
            }

            // Возвращаем значения факториалов
            int n = start;
            while (factorial <= end)
            {
                yield return $"{n}! = {factorial}";

                n++;
                factorial *= n;

            }
        }
    }




}

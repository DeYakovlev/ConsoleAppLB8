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
            Task1();


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

        public string GetStatus() 
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
            Console.WriteLine($"[Service()] {Name} Начинаю обслуживание");
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



}

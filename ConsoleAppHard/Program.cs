using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ConsoleAppHard.Program;

namespace ConsoleAppHard
{
    internal class Program
    {
    public  class Vehicle 
        {
            public string Type = "Траноспорт";
        }

    public class Car : Vehicle 
        {
            public Car() 
            {
                Type = "Машина";
            }
        }

    public class Truck : Vehicle 
        {
            public Truck() 
            {
                Type = "Грузовик";
            }
        }

    public class Bicycle : Vehicle
        {
            public Bicycle() 
            {
                Type = "Велосипед";
            }
        }

    public  interface IVehicleFactory<out T>
        {

            T Produce();           // при out
           // void Accept(T item); // при in 

        }

    public class CarFactory : IVehicleFactory<Car> 
        {
            public Car Produce() 
            {
                return new Car();
            }
        }

        class  TruckFactory : IVehicleFactory<Truck> 
        {
            public Truck Produce() 
            {
                return new Truck();
            }
        }

        class BicycleFactory : IVehicleFactory<Bicycle> 
        {
            public Bicycle Produce() 
            {
                return new Bicycle();
            }
        }

        public static void ShowVehicle(IVehicleFactory<Vehicle> factory) 
        {

            Console.WriteLine(factory.Produce().Type);
            
        }





        static void Main(string[] args)
        {
            CarFactory cf = new CarFactory();
            TruckFactory tf = new TruckFactory();
            BicycleFactory bf = new BicycleFactory();


            ShowVehicle(cf);
            ShowVehicle(tf);
            ShowVehicle(bf);

        }
    }
}

using DeluxeParkingSQL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParkingSQL
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {
                Console.Clear();
 

                //Models.Car newCar = new Models.Car
                //{
                //    Plate = "XPQ" + rnd.Next(100, 999),
                //    Make = "Tesla",
                //    Color = "Röd"
                //};


                Console.WriteLine();
                Console.WriteLine("Tryck B för att lägga till en ny bil");
                Console.WriteLine("Tryck P för att se alla parkeringshus");
                Console.WriteLine("Tryck S för att se alla städer med parkeringshus");
                Console.WriteLine("Tryck L för att se alla parkerade bilar");
                Console.WriteLine("Tryck C för att lägga till en ny stad");
                Console.WriteLine("Tryck O för att lägga till parkeringsplatser");
                Console.WriteLine("Tryck V för att se alla tillgängliga parkeringsplatser");

                var key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case 'b':
                        // Lägg till ny bil
                        Random rnd = new Random();
                        Models.Car newCar = new Models.Car
                        {
                            Plate = "XPQ" + rnd.Next(100, 999),
                            Make = "Tesla",
                            Color = "Röd"
                        };

                        break;

                    case 'p':
                        Console.WriteLine("Output Parking Houses");
                        List<Models.ParkingHouse> pHouse = DatabaseDapper.GetAllParkingHouse();
                        foreach (Models.ParkingHouse house in pHouse)
                        {
                            Console.WriteLine(house.Id + "\t" + house.HouseName + "\t" + house.CityId);
                        }
                        break;

                    case 'l':
                        List<Models.Car> cars2 = DatabaseDapper.GetAllCars();
                        foreach (Models.Car car in cars2)
                        {
                            Console.WriteLine(car.Id + "\t" + car.Plate + "\t" + car.Make + "\t" + car.Color + "\t" + car.ParkingSlotsId);
                        }
                        
                        break;

                    case 'c':
                        Console.WriteLine("Vänligen ange namnet på staden som ska läggas till.");
                        string input = Console.ReadLine();
                        Models.City city2 = new Models.City
                        {
                            CityName = input
                        };
                        int rowsAffected = DatabaseDapper.InsertCity(city2);
                        Console.WriteLine("Städer som har lagts till " + rowsAffected);
                        break;

                    case 's':
                        Console.WriteLine("Output Cities");
                        List<Models.City> cities = DatabaseDapper.GetAllCities();
                        foreach (Models.City city in cities)
                        {
                            Console.WriteLine(city.Id + "\t" + city.CityName);
                        }
                        break;
                    case 'o':
                        Console.WriteLine("Vänligen ange parkeringsplatsen som ska läggas till");
                        string input2 = Console.ReadLine();
                        Console.WriteLine("Vänligen ange parkeringshusid detta avser");
                        string input3 = Console.ReadLine();
                        Models.ParkingSlots pSlot = new Models.ParkingSlots
                        {
                            SlotNumber = int.Parse(input2),
                            ParkingHouseId = int.Parse(input3)
                        };
                        int rowsAffected2 = DatabaseDapper.InsertParkingSlots(pSlot);
                        Console.WriteLine("Parkeringsplats som har lagts till " + rowsAffected2 + " på" + " parkeringshusid " + input3);
                        break;
                    case 'v':
                        Console.WriteLine("Vänligen ange parkeringshusid detta avser");
                        string input4 = Console.ReadLine();
                        List<Models.VacantSpots> vSpot = DatabaseDapper.GetVacantSpots(int.Parse(input4));
                        foreach (Models.VacantSpots spots in vSpot)
                        {
                            Console.WriteLine(spots.CityName + "\t" + spots.HouseName + "\t" + spots.SlotNumber + "\t" + spots.Id);
                        }
                        break;
                    case 'u':
                        List<Models.Car> cars3 = DatabaseDapper.GetAllCars();
                        foreach (Models.Car car in cars3)
                        {
                            Console.WriteLine(car.Id + "\t" + car.Plate + "\t" + car.Make + "\t" + car.Color + "\t" + car.ParkingSlotsId);
                        }
                        Console.WriteLine("Vänligen välj bil-id för bilen du önskar att parkera");
                        string input5 = Console.ReadLine();
                        Console.Clear();
                        List<Models.ParkingHouse> pHouse1 = DatabaseDapper.GetAllParkingHouse();
                        foreach (Models.ParkingHouse house in pHouse1)
                        {
                            Console.WriteLine(house.Id + "\t" + house.HouseName + "\t" + house.CityId);
                        }
                        Console.WriteLine("Vänligen ange parkeringshus du önskar parkera på");
                        input5 = Console.ReadLine();

                        List<Models.ParkingSlots> pSlots = DatabaseDapper.GetAllParkingSlots();
                        foreach (Models.ParkingSlots slot in pSlots)
                        {
                            if (slot.ParkingHouseId == int.Parse(input5))
                            {
                                Console.WriteLine(slot.Id);
                            }
                        }
                        break;
                }
                Console.ReadLine();
            }
        }
    }
}


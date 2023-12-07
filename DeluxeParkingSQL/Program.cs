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
            string input;

            while (true)
            {
                Console.Clear();
 

                //Models.Car newCar = new Models.Car
                //{
                //    Plate = "XPQ" + rnd.Next(100, 999),
                //    Make = "Tesla",
                //    Color = "Röd"
                //};


                Console.WriteLine("Vänligen ange en knapptryckning för nedan alternativ: ");
                Console.WriteLine("Tryck B för att lägga till en ny bil");
                Console.WriteLine("Tryck P för att se alla parkeringshus");
                Console.WriteLine("Tryck S för att se alla städer med parkeringshus");
                Console.WriteLine("Tryck L för att se alla parkerade bilar");
                Console.WriteLine("Tryck C för att lägga till en ny stad");
                Console.WriteLine("Tryck O för att lägga till parkeringsplatser");
                Console.WriteLine("Tryck V för att se alla tillgängliga parkeringsplatser");
                Console.WriteLine("Tryck U för att parkera en bil på valfri lediga plats");
                Console.WriteLine("Tryck D för att köra iväg med din parkerade bil");
                Console.WriteLine("Tryck E för att se tillgängliga parkeringsplatser med elplatser per parkeringshus");
                Console.WriteLine("Tryck F för att se tillgängliga parkeringsplatser med elplatser per stad");

                var key = Console.ReadKey(true);

                switch (key.KeyChar)
                {
                    case 'b':
                        // Lägg till ny bil
                        Console.WriteLine("Vänligen ange bilens registeringsnummer");
                        string inputReg = Console.ReadLine();
                        Console.WriteLine("Vänligen ange bilens bilmärke");
                        string inputMake = Console.ReadLine();
                        Console.WriteLine("Vänligen ange bilens färg");
                        string inputColor = Console.ReadLine();
                        Models.Car newCar = new Models.Car
                        {
                            Plate = inputReg,
                            Make = inputMake,
                            Color = inputColor
                        };
                        int affectedRow = DatabaseDapper.InsertCar(newCar);
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
                        input = Console.ReadLine();
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
                        input = Console.ReadLine();
                        Console.WriteLine("Vänligen ange parkeringshusid detta avser");
                        input = Console.ReadLine();
                        Models.ParkingSlots pSlot = new Models.ParkingSlots
                        {
                            SlotNumber = int.Parse(input),
                            ParkingHouseId = int.Parse(input)
                        };
                        int rowsAffected2 = DatabaseDapper.InsertParkingSlots(pSlot);
                        Console.WriteLine("Parkeringsplats som har lagts till " + rowsAffected2 + " på" + " parkeringshusid " + input);
                        break;
                    case 'v':
                        Console.WriteLine("Vänligen ange parkeringshusid detta avser");
                        input = Console.ReadLine();
                        List<Models.VacantSpots> vSpot = DatabaseDapper.GetVacantSpots(int.Parse(input));
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
                        input = Console.ReadLine();
                        Console.Clear();
                        List<Models.ParkingHouse> pHouse1 = DatabaseDapper.GetAllParkingHouse();
                        foreach (Models.ParkingHouse house in pHouse1)
                        {
                            Console.WriteLine(house.Id + "\t" + house.HouseName + "\t" + house.CityId);
                        }
                        Console.WriteLine("Vänligen ange parkeringshus du önskar parkera på");
                        string input2 = Console.ReadLine();
                        Console.Clear();        
                        List<Models.ParkingSlots> pSlots = DatabaseDapper.GetAllParkingSlots();
                        foreach (Models.ParkingSlots slot in pSlots)
                        {
                            if (slot.ParkingHouseId == int.Parse(input2))
                            {
                                Console.WriteLine(slot.Id);
                            }

                        }
                        Console.WriteLine("Vänligen ange parkeringsplatsen du önskar att parkera på.");
                        string input3 = Console.ReadLine();
                        rowsAffected = DatabaseDapper.ParkCar(int.Parse(input), int.Parse(input3));
                        Console.WriteLine("Bil med id " + input + " har parkerats på plats " + input3 + " på parkeringshuset med id " + input2);
                        break;
                    case 'd':
                        List<Models.Car> cars4 = DatabaseDapper.GetAllCars();
                        foreach (Car car in cars4)
                        {
                            Console.WriteLine(car.Id + "\t" + car.Plate + "\t" + car.Make + "\t" + car.Color + "\t" + car.ParkingSlotsId);
                        }
                        Console.WriteLine("Vänligen ange fordonet du önskar att köra iväg med");
                        input = Console.ReadLine();
                        rowsAffected = DatabaseDapper.ParkCar(int.Parse(input), null);
                        break;
                    case 'e':
                        List<Models.ElectricOutletsSlots> eSlots = DatabaseDapper.GetAllElectricOutletSlots();
                        foreach (var eSlot in eSlots)
                        {
                            Console.WriteLine("Parkeringsnamn: " + "\t" + eSlot.HouseName + "\t" + eSlot.ElectricOutletsAmount);
                        }
                    break;
                    case 'f':
                        List<Models.ElectricOutletSlotsCity> eSlots2 = DatabaseDapper.GetAllElectricOutletSlotsCity();
                        foreach (var eSlot in eSlots2)
                        {
                            Console.WriteLine("Stad: " + "\t" + eSlot.CityName + "\t" + eSlot.ElectricOutlets);
                        }
                        break;
                }
                Console.ReadLine();
            }
        }
    }
}


using Dapper;
using DeluxeParkingSQL.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeluxeParkingSQL
{
    internal class DatabaseDapper
    {
        static string connString = "data source=.\\SQLEXPRESS; initial catalog = Parking10; persist security info = True; Integrated Security = True;";

        public static List<Models.Car> GetAllCars()
        {
            string sql = "SELECT * FROM Cars";
            List<Models.Car> cars = new List<Models.Car>();

            using (var connection = new SqlConnection(connString))
            {
                cars = connection.Query<Models.Car>(sql).ToList();
            }
            return cars;
        }

        public static List<Models.ParkingHouse> GetAllParkingHouse()
        {
            string sql = "SELECT * FROM ParkingHouses";
            List<Models.ParkingHouse> pHouse = new List<Models.ParkingHouse>();

            using (var connection = new SqlConnection(connString))
            {
                pHouse = connection.Query<Models.ParkingHouse>(sql).ToList();
            }
            return pHouse;
        }

        public static List<Models.ParkingSlots> GetAllParkingSlots()
        {
            string sql = "SELECT * FROM ParkingSlots";
            List<ParkingSlots> pSlots = new List<ParkingSlots>();

            using (var connection = new SqlConnection(connString))
            {
                pSlots = connection.Query<Models.ParkingSlots>(sql).ToList();
            }
            return pSlots;
        }

        public static List<Models.VacantSpots> GetVacantSpots(int input)
        {
            string sql = $@"
              SELECT c.CityName, ph.HouseName, ps.SlotNumber, ps.Id
              FROM ParkingSlots ps
              JOIN ParkingHouses ph ON ps.ParkingHouseID = ph.Id
              JOIN Cities c ON ph.CityId = c.Id
              WHERE c.Id = ({input})
              GROUP BY c.CityName, ph.HouseName, ps.SlotNumber, ps.Id
              ORDER BY ph.HouseName
                ";

            List<Models.VacantSpots> spotsPerHouse = new List<Models.VacantSpots>();

            using (var connection = new SqlConnection(connString))
            {
                spotsPerHouse = connection.Query<Models.VacantSpots>(sql).ToList();
            }

            return spotsPerHouse;

        }

        public static int InsertCar(Models.Car car)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO Cars(Plate, Make, Color) VALUES ('{car.Plate}', '{car.Make}', '{car.Color}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }




        public static List<Models.City> GetAllCities()
        {
            string sql = "SELECT * FROM Cities";
            List<City> cities = new List<City>();

            using (var connection = new SqlConnection(connString))
            {
                cities = connection.Query<Models.City>(sql).ToList();
            }
            return cities;
        }

        public static int InsertCity(Models.City cities)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO Cities(CityName) VALUES ('{cities.CityName}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }

        public static int InsertParkingSlots(Models.ParkingSlots pSlot)
        {
            int affectedRows = 0;
            string sql = $"INSERT INTO ParkingSlots(SlotNumber, ElectricOutlet, ParkingHouseId) VALUES ('{pSlot.SlotNumber}', '{pSlot.ElectricOutlet}', '{pSlot.ParkingHouseId}')";

            using (var connection = new SqlConnection(connString))
            {
                affectedRows = connection.Execute(sql);
            }
            return affectedRows;
        }

        //public static int ParkCar(int carId, int? spotId)
        //{
        //    int affectedRows = 0;

        //    string sql = $"UPDATE Cars SET ParkingSlotsId=" + (spotId == null ? "NULL" : spotId) + $" WHERE Id = {carId}";
        //    using (var connection = new SqlConnection(connString))
        //    {
        //        affectedRows = connection.Execute(sql);
        //    }
        //    return affectedRows;
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Home0326_7_1
{
    [Serializable]
    public class BusTrip
    {
        public string BusNumber { get; set; }
        public BusType BusType { get; set; }
        public string Destination { get; set; }
        public DateTime Departure { get; set; } = DateTime.Now;
        public DateTime Arrivel { get; set; } = DateTime.Now;
        public Guid TripCode { get; set; } = Guid.NewGuid();

        [NonSerialized]
        public DateTime Created = DateTime.Now;
       
        public string GetDepartureDate() { return Departure.ToShortDateString(); }
        public string GetDepartureTime() { return Departure.ToShortTimeString(); }
        public string GetArrivelDate() { return Arrivel.ToShortDateString(); }
        public string GetArrivelTime() { return Arrivel.ToShortTimeString(); }

        public static BusTrip DeepCopy(BusTrip trip)
        {
            return new BusTrip()
            {
                BusNumber = trip.BusNumber,
                Destination = trip.Destination,
                Departure = trip.Departure,
                Arrivel = trip.Arrivel,
                BusType = trip.BusType,
                TripCode = Guid.NewGuid(),
                Created = DateTime.Now
            };
        }

        public override string ToString()
        {
            return $"Number: {BusNumber}, Bus type: {BusType.ToString()}," +
                $"Destination: {Destination}, Departure: {Departure}, Arrivel: {Arrivel}";
        }

        public static BusTrip[] busTrips = new BusTrip[]
        {
            new BusTrip()
            {
                BusNumber = "AA3245BA",
                BusType = BusType.Medium,
                Destination = "Kyiv",
                Departure = new DateTime(2021,04,10,9,45,00),
                Arrivel = new DateTime(2021,04,10,15,30,00),
            },
            new BusTrip()
            {
                BusNumber = "BA1198CB",
                BusType = BusType.Big,
                Destination = "Odessa",
                Departure = new DateTime(2021,04,15,10,20,00),
                Arrivel = new DateTime(2021,04,15,18,55,00),
            },
            new BusTrip()
            {
                BusNumber = "DD5566AA",
                BusType = BusType.Medium,
                Destination = "Lviv",
                Departure = new DateTime(2021,04,14,8,30,00),
                Arrivel = new DateTime(2021,04,14,13,15,00),
            },
            new BusTrip()
            {
                BusNumber = "BB0980AX",
                BusType = BusType.Small,
                Destination = "Ternopyl",
                Departure = new DateTime(2021,04,20,14,30,00),
                Arrivel = new DateTime(2021,04,20,16,15,00),
            },
            new BusTrip()
            {
                BusNumber = "AX7733MN",
                BusType = BusType.Small,
                Destination = "Rivne",
                Departure = new DateTime(2021,04,30,9,45,00),
                Arrivel = new DateTime(2021,04,30,16,25,00),
            }
        };
    }
}

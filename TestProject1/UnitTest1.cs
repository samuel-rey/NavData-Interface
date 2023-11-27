using AviationCalcUtilNet.GeoTools;
using NavData_Interface.DataSources;
using System.Security.Cryptography;

namespace TestProject1
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public static void TestGetClosestAirportWithinRadius1()
        {
            var navDataInterface = new DFDSource("e_dfd_2311.s3db");
            var westOfLoughNeagh = new GeoPoint(54.686784, -6.544965);
            var closestAirport = navDataInterface.GetClosestAirportWithinRadius(westOfLoughNeagh, 30_000);
            Assert.That(closestAirport.Identifier, Is.EqualTo("EGAL"));
        }

        [Test]
        public static void TestGetClosestAirportWithinRadius2()
        {
            var navDataInterface = new DFDSource("e_dfd_2311.s3db");
            var point = new GeoPoint(-17.953955, -179.99);
            var closestAirport = navDataInterface.GetClosestAirportWithinRadius(point, 100_000);
            Assert.That(closestAirport.Identifier, Is.EqualTo("NFMO"));
        }

        [Test]
        public static void TestGetClosestAirportWithinRadius3()
        {
            var navDataInterface = new DFDSource("e_dfd_2311.s3db");
            var point = new GeoPoint(-89.75, -142.284902);
            var closestAirport = navDataInterface.GetClosestAirportWithinRadius(point, 100_000);
            Assert.That(closestAirport.Identifier, Is.EqualTo("NZSP"));
        }

        [Test]
        public static void TestGetClosestAirportWithinRadius4()
        {
            var navDataInterface = new DFDSource("e_dfd_2311.s3db");
            var point = new GeoPoint(-27.058760, 83.227773);
            var closestAirport = navDataInterface.GetClosestAirportWithinRadius(point, 100_000);
            Assert.That(closestAirport,  Is.Null);
        }
    }
}
using ElevatorConsoleApp.Models;
using ElevatorConsoleApp.Services;
using Newtonsoft.Json;
using System.Xml.Linq;

namespace ElevatorConsoleApp.Tests
{
    public class ElevatorTests
    {
        [Fact]
        public void CallElevator_tests()
        {
            // On Elevator Creation maxnumber of Floors is 5
            List<Elevator> elevatorList = new List<Elevator>();
            var inputString = @"[
                                   {
                                      ""Id"": 0,
                                      ""Name"": ""1"",
                                      ""CurrentFloor"": 4,
                                      ""HasCapacity"": false,
                                      ""NumOfPplInside"": 9,
                                      ""Direction"": 0,
                                      ""Status"": 1,
                                      ""CallFloorDifference"": 0
                                   },
                                   {
                                      ""Id"": 1,
                                      ""Name"": ""2"",
                                      ""CurrentFloor"": 4,
                                      ""HasCapacity"": false,
                                      ""NumOfPplInside"": 2,
                                      ""Direction"": 0,
                                      ""Status"": 1,
                                      ""CallFloorDifference"": 0
                                   },
                                   {
                                      ""Id"": 2,
                                      ""Name"": ""3"",
                                      ""CurrentFloor"": 3,
                                      ""HasCapacity"": true,
                                      ""NumOfPplInside"": 2,
                                      ""Direction"": 0,
                                      ""Status"": 2,
                                      ""CallFloorDifference"": 0
                                   }
                                ]";

            elevatorList = JsonConvert.DeserializeObject<List<Elevator>>(inputString);

            int requestFloor = 3;
            int numOfPplWaiting = 3;
            ElevatorProssesor elevatorProssesor = new ElevatorProssesor();
            string expected = "3";
            var elevator = elevatorProssesor.CallElevator(elevatorList, requestFloor, numOfPplWaiting);
            var actual = elevator;
            Assert.Equal(expected, actual.Name);
        }

        [Fact]
        public void MoveElevator_tests()
        {
            // continuation from above list of elevators which are 3 and number of floors 5
            Elevator elevator = new Elevator()
            {
                Id = 2,
                Name = "3",
                CurrentFloor = 3,
                HasCapacity = true,
                NumOfPplInside = 2,
                Direction = 0,
                Status = (ElevatorStatus)2,
                CallFloorDifference = 0,
            };
            int requestFloor = 3;
            ElevatorProssesor elevatorProssesor = new ElevatorProssesor();
            var expectedDestinationFloor = 3;
            var elevatorFloor = elevatorProssesor.MoveElevator(elevator, requestFloor);
            var actual = elevator;
            Assert.Equal(expectedDestinationFloor, elevatorFloor);
        }

        [Fact]
        public void CheckClosestElevator_tests()
        {
            List<Elevator> elevatorList = new List<Elevator>();
            var inputString = @"[
                                   {
                                      ""Id"": 0,
                                      ""Name"": ""1"",
                                      ""CurrentFloor"": 4,
                                      ""HasCapacity"": false,
                                      ""NumOfPplInside"": 9,
                                      ""Direction"": 0,
                                      ""Status"": 1,
                                      ""CallFloorDifference"": 0
                                   },
                                   {
                                      ""Id"": 1,
                                      ""Name"": ""2"",
                                      ""CurrentFloor"": 4,
                                      ""HasCapacity"": false,
                                      ""NumOfPplInside"": 2,
                                      ""Direction"": 0,
                                      ""Status"": 1,
                                      ""CallFloorDifference"": 0
                                   },
                                   {
                                      ""Id"": 2,
                                      ""Name"": ""3"",
                                      ""CurrentFloor"": 3,
                                      ""HasCapacity"": true,
                                      ""NumOfPplInside"": 2,
                                      ""Direction"": 0,
                                      ""Status"": 2,
                                      ""CallFloorDifference"": 0
                                   }
                                ]";

            elevatorList = JsonConvert.DeserializeObject<List<Elevator>>(inputString);
            
            int requestFloor = 3;
            ElevatorProssesor elevatorProssesor = new ElevatorProssesor();
            var expected = "3";
            var elevator = elevatorProssesor.CheckClosest(requestFloor, elevatorList);
            var actual = elevator;
            Assert.Equal(expected, actual.Name);
        }
    }
}
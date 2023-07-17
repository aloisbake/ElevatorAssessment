using ElevatorConsoleApp.Models;

namespace ElevatorConsoleApp.Services
{
    public class ElevatorProssesor : IElevatorProssesor
    {
        public ElevatorProssesor()
        {
        }
        public Elevator CallElevator(List<Elevator> elevatorList, int requestFloor, int numOfPplWaiting)
        {
            var setCapacityElevatorList = SetCapacity(elevatorList, numOfPplWaiting);

            var elevatorsWithCapacity = setCapacityElevatorList.Where(x => x.HasCapacity == true).ToList();

            Elevator closestElevator = CheckClosest(requestFloor, elevatorsWithCapacity);

            Console.WriteLine($"Selected Elevator is : {closestElevator.Name} which is on floor : {closestElevator.CurrentFloor}");

            return closestElevator;
        }

        public List<Elevator> SetCapacity(List<Elevator> elevators, int numWaiting)
        {
            int maxNumOfPplInElevator = 10;

            foreach (Elevator elevator in elevators)
            {
                if (elevator.NumOfPplInside < maxNumOfPplInElevator)
                {
                    elevator.HasCapacity = true;
                }
                else 
                {
                    elevator.HasCapacity = false;
                }
            }

            return elevators;
        }
        public Elevator CheckClosest(int requestFloor, List<Elevator> elevators)
        {
            foreach (Elevator elevator in elevators)
            {
                elevator.CallFloorDifference = Math.Abs(requestFloor - elevator.CurrentFloor);
            }

            var result = elevators.Where((x) => x.CallFloorDifference == elevators.Min(y => y.CallFloorDifference)).ToList();

            return new Elevator
            {
                CallFloorDifference = result[0].CallFloorDifference,
                CurrentFloor = result[0].CurrentFloor,
                HasCapacity = result[0].HasCapacity,
                Direction = result[0].Direction,
                Id = result[0].Id,
                Name = result[0].Name,
                Status = result[0].Status,
            };
        }

        public int MoveElevator(Elevator elevator, int requestFloor)
        {
            // foreach step print floor number to console till requested floor
            Console.WriteLine("Begin Closest Elevetor move as below : ");
            int difference = Math.Abs(elevator.CurrentFloor - requestFloor);
            var current = elevator.CurrentFloor;
            if (elevator.CurrentFloor < requestFloor) // means we going up
            { 
                for (int i = 0; i <= elevator.CallFloorDifference; i++)
                {
                    this.PrintStep(current, "Up");
                    current++;
                }
            }
            else if (elevator.CurrentFloor >= requestFloor) // means we going down
            { 
                for (int i = 0; i < elevator.CallFloorDifference; i++)
                {
                    this.PrintStep(current, "Down");
                    current--;
                }
            }
            else
            { // we on the same floor
                this.PrintStep(current, "Same Floor");
            }
            elevator.CurrentFloor = requestFloor;
            return elevator.CurrentFloor;
        }

        public List<Elevator> CreateRandomElevators(int numOfElevators, int maxNumOfFloors) // create random elevators to begin with
        {
            var listOfElevators = new List<Elevator>();

            for (int i = 0; i < numOfElevators; i++)
            {
                var random = new Random();
                var elevator = new Elevator
                {
                    CurrentFloor = random.Next(maxNumOfFloors),
                    Direction = (ElevatorDirection)random.Next(typeof(ElevatorDirection).GetEnumValues().Length),
                    HasCapacity = (random.Next(2) == 1),
                    NumOfPplInside = random.Next(10),
                    Status = (ElevatorStatus)random.Next(typeof(ElevatorStatus).GetEnumValues().Length),
                    Name = (i + 1).ToString()
                };
                listOfElevators.Add(elevator);
            }
            return listOfElevators;
        }

        public void PrintStep(int currentFloor, string direction)
        {
            Console.WriteLine($"Elevator on Floor : {currentFloor}, direction is : {direction}");
        }

        public void OpenDoor(Elevator elevator)
        {
            Console.WriteLine($"Elevator : {elevator.Name} door is open on Floor : {elevator.CurrentFloor}");
        }


        public void PrintElevatorDetails(Elevator elevator)
        {
            Console.WriteLine($"Elevator : {elevator.Name} on Floor : {elevator.CurrentFloor} with {elevator.NumOfPplInside} people inside");            
        }
    }
}

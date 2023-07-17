using ElevatorConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorConsoleApp.Services
{
    public interface IElevatorProssesor
    {
        int MoveElevator(Elevator elevator, int requestFloor);
        Elevator CallElevator(List<Elevator> elevatorList, int requestFloor, int numOfPplWaiting);
        Elevator CheckClosest(int requestFloor, List<Elevator> elevators);
        List<Elevator> SetCapacity(List<Elevator> elevators, int numWaiting);
        List<Elevator> CreateRandomElevators(int numOfElevators, int maxNumOfFloors);
        void PrintStep(int currentFloor);
        void PrintElevatorDetails(Elevator elevator);
        void OpenDoor(Elevator elevator);

    }
}

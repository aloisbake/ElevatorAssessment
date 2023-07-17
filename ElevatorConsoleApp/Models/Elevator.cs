using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElevatorConsoleApp.Models
{
    public class Elevator
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CurrentFloor { get; set; }
        public bool HasCapacity { get; set; }
        public int NumOfPplInside { get; set; }
        public ElevatorDirection Direction { get; set; }
        public ElevatorStatus Status { get; set; }
        public int CallFloorDifference { get; set; }
    }

    public enum ElevatorDirection 
    {
        Up,
        Down
    }

    public enum ElevatorStatus 
    {
        Moving, 
        Stagnant,
        faulty
    }
}

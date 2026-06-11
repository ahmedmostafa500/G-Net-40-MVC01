using System.Numerics;
using Microsoft.Identity.Client;

namespace GymSystem.DAL.Entities
{
    public class Member:GymUser
    {
        public string? photo { get; set; } = null!;
        public HealthRecord healthRecord { get; set; } = null!;

        public ICollection<MemberShip>memberShips=new HashSet<MemberShip>();
        public ICollection<Booking>bookings=new HashSet<Booking>();
    }
}
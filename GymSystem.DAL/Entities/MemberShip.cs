using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities
{
    public class MemberShip:BaseEntity
    {
        public Member member { get; set; } = null!;
        public int MemberId {  get; set; }

        public Plan plan { get; set; } = null!;
        public int PlanId {  get; set; }

        public DateTime EndDate { get; set; }
        public string Status => IsActive ? "Active" : "Expired";

        [NotMapped]
        public bool IsActive => EndDate > DateTime.Now;
    }
}

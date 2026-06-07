using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystem.DAL.Entities.Enums;

namespace GymSystem.DAL.Entities
{
    public class Trainer:GymUser
    {
        public Specialities specialize{  get; set; }
        public DateTime HiringDate { get; set; }

        public ICollection<Sessions> sessions { get; set; }=new HashSet<Sessions>();
    }
}

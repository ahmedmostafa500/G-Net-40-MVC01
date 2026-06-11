using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymSystem.DAL.Entities
{
    public class Category:BaseEntity
    {
        public string CategoryName { get; set; } = null!;

            public ICollection<Sessions> sessions { get; set; } =new HashSet<Sessions>();
    }
}

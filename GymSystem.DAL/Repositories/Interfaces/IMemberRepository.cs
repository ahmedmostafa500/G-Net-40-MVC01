using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystem.DAL.Entities;

namespace GymSystem.DAL.Repositories.Interfaces
{
    public interface IMemberRepository:IGenericRepository<Member>
    {
        Task<Member> GetMemberByHealthRecordById(); 
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using GymSystem.DAL.Contexts;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymSystem.DAL.Repositories.Classes
{
    public class MemberRepository : GenericRepository<Member>,IMemberRepository

    {
        private readonly GymDbcontext dbcontext;

        public MemberRepository(GymDbcontext dbcontext):base(dbcontext) 
        {
            this.dbcontext = dbcontext;
        }
        public void Add(Member member)
        {
            dbcontext.members.Add(member);
        }

        public async Task<int> CompleteAsync()
        {
            return await dbcontext.SaveChangesAsync();
        }

        public void Delete(int id)
        {
            var member = dbcontext.members.FirstOrDefault(m => m.Id == id);
            if (member != null) { 
            dbcontext.members.Remove(member);
            }
        }

        public async Task<IEnumerable<Member>> GetAll(bool isTracked, CancellationToken ct = default)
        {
            var members = isTracked ? dbcontext.members : dbcontext.members.AsNoTracking();
            return await dbcontext.members.ToListAsync();
        }

        public Task<IEnumerable<Member>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<Member?> GetById(int id)
        {
            var members = await dbcontext.members.FirstOrDefaultAsync (p => p.Id == id);
            return members;
        }

        public Task<Member> GetMemberByHealthRecordById()
        {
            throw new NotImplementedException();
        }

        public void Update(Member member)
        {
            dbcontext.members.Update(member);
        }
    }
}

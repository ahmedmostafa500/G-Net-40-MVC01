using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using GymSystem.BLL.Services.Interfaces;
using GymSystem.BLL.ViewModels.MembersViewModels;
using GymSystem.DAL.Entities;
using GymSystem.DAL.Repositories.Interfaces;

namespace GymSystem.BLL.Services.Classes
{
    public class MemberServices : IMemberServices
    {
        private readonly IUnitOfWork unitOfWork;

        public MemberServices(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<MemberViewModel>> GetAllMemberAsync(CancellationToken ct = default)
        {
            var members = await unitOfWork.GetRepository<Member>().GetAll(false,ct);
            if (!members.Any()) return [];

            var membersViewModel = members.Select(m => new MemberViewModel()
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Photo=m.photo,
                Gender=m.Gender.ToString(),          
            });
            return membersViewModel;
        }

        public async Task<MemberViewModel?> GetMemberDetailAsync(int memberId, CancellationToken ct = default)
        {
             var member= await unitOfWork.GetRepository<Member>().GetById(memberId,ct);
            if (member is null)  
                return null;

            var MemberVM = new MemberViewModel()
            {
                Name=member.Name,
                Email=member.Email,
                Phone=member.Phone,
                DateOfBirth=member.DateOfBirth.ToShortDateString(),
                Gender=member.Gender.ToString(),
                Address =$"{member.Address.BuildingNumber} - {member.Address.Street} - {member.Address.City}"
            };

            var ActiveMemberShip = await unitOfWork.GetRepository<MemberShip>().FiristOrDefaultAsync(mb=>mb.MemberId==memberId && mb.EndDate > DateTime.Now,false,ct);
             if(ActiveMemberShip is not null)
            {
                var ActivePlan=await unitOfWork.GetRepository<Plan>().GetById(ActiveMemberShip.PlanId,ct);

                MemberVM.PlanName = ActivePlan?.Name;
                MemberVM.MembershipStartDate=ActiveMemberShip.CreatedAt.ToShortDateString();
                MemberVM.MembershipEndDate=ActiveMemberShip.EndDate.ToShortDateString();
            }
             return MemberVM;
        }

        public async Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int memberid, CancellationToken ct = default)
        {
            var Record = await unitOfWork.GetRepository<HealthRecord>().FiristOrDefaultAsync(r => r.MemberId == memberid,false,ct);
            if(Record is null) return null;
            return new HealthRecordViewModel()
            {
                Weight=Record.Weight,
                Height=Record.Height,
                BloodType=Record.BloodType,
                Note=Record.Note,
            };
        }

        public async Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int memberId, CancellationToken ct = default)
        {
          var member= await unitOfWork.GetRepository<Member>().GetById(memberId,ct);
            if (member is null) return null;
            return new MemberToUpdateViewModel()
            {
                Name=member.Name,
                Phone=member.Phone,
                Email=member.Email,
                Street=member.Address.Street,
                City=member.Address.City,
                BuildingNumber=member.Address.BuildingNumber,
                Photo=member.photo

            };
        }



        public async Task<bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct = default)
        {
            var emailExist= await unitOfWork.GetRepository<Member>().AnyAsync(m=>m.Email==model.Email);
            var PhoneExist= await unitOfWork.GetRepository<Member>().AnyAsync(m=>m.Phone==model.Phone);
            if(emailExist ||  PhoneExist) return false;

            var member = new Member()
            {
                Name = model.Name,
                Phone = model.Phone,
                Email = model.Email,
                DateOfBirth = model.DateOfBirth,
                Gender = model.Gender,
                Address = new Address()
                {
                    BuildingNumber = model.BuildingNumber,
                    City = model.City,
                    Street = model.Street,

                },
                healthRecord = new HealthRecord()
                {
                    Weight = model.HealthRecordViewModel.Weight,
                    Height = model.HealthRecordViewModel.Height,
                    BloodType = model.HealthRecordViewModel.BloodType,
                    Note = model.HealthRecordViewModel.Note,
                }
            };
            unitOfWork.GetRepository<Member>().Add(member);
            var result = await unitOfWork.CompleteAsync();

            return result > 0;
        }
        public async Task<bool> UpdateMemberDetailsAsync(int id, MemberToUpdateViewModel model, CancellationToken ct = default)
        {
            var member= await unitOfWork.GetRepository<Member>().GetById(id);
            if(member == null) return false;

            if( await unitOfWork.GetRepository<Member>().AnyAsync(m=>m.Email==model.Email && m.Id != id)) return false;
            if (await unitOfWork.GetRepository<Member>().AnyAsync(m => m.Phone == model.Phone && m.Id != id)) return false;

            member.Phone= model.Phone;
            member.Email= model.Email;
            member.Address.City = model.City;
            member.Address.Street = model.Street;
            member.Address.BuildingNumber = model.BuildingNumber;
            member.UpdatedAt=DateTime.Now;
            unitOfWork.GetRepository<Member>().Update(member);

            var result = await unitOfWork.CompleteAsync();

            return result > 0;

        }
        public async Task<bool> DeleteMemberAsync(int id, CancellationToken ct = default)
        {
         
            var HasFutureSessions= await unitOfWork.GetRepository<Booking>().AnyAsync(b=> b.MemberId==id&& b.Sessions.EndDate>DateTime.Now);

            if (HasFutureSessions) return false;

            unitOfWork.GetRepository<Member>().Delete(id);

            var result = await unitOfWork.CompleteAsync();

            return result > 0;
        }
    }
}

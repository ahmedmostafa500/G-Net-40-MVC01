using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GymSystem.BLL.ViewModels.MembersViewModels;

namespace GymSystem.BLL.Services.Interfaces
{
    public interface IMemberServices
    {
        Task<IEnumerable<MemberViewModel>>GetAllMemberAsync(CancellationToken ct=default);
        Task <bool> CreateMemberAsync(CreateMemberViewModel model, CancellationToken ct=default);
        Task<MemberViewModel?> GetMemberDetailAsync(int memberId, CancellationToken ct= default);
        Task<HealthRecordViewModel?> GetMemberHealthRecordAsync(int memberid,CancellationToken ct=default);
        Task<MemberToUpdateViewModel> GetMemberToUpdateAsync(int memberId,CancellationToken ct=default);
        Task<bool>UpdateMemberDetailsAsync(int id, MemberToUpdateViewModel model, CancellationToken ct=default);
        Task<bool>DeleteMemberAsync(int id, CancellationToken ct=default);
    }
}

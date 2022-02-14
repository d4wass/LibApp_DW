using System.Collections.Generic;
using LibApp.Models;

namespace LibApp.Interfaces;

public interface IMembershipRepository
{
    IEnumerable<MembershipType> GetMembershipTypes();
    MembershipType GetMembershipTypeById(int membershipTypeId);
    void AddMembershipType(MembershipType membershipType);
    void UpdateMembershipType(MembershipType membershipType);
    void DeleteMembershipType(MembershipType membershipType);
    void Save();
}
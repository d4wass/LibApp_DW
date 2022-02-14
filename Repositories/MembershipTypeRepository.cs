using System.Collections.Generic;
using LibApp.Data;
using LibApp.Interfaces;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LibApp.Repositories;

public class MembershipTypeRepository : IMembershipRepository
{
    private readonly ApplicationDbContext _context;

    public MembershipTypeRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public IEnumerable<MembershipType> GetMembershipTypes()
    {
        return _context.MembershipTypes;
    }

    public MembershipType GetMembershipTypeById(int membershipTypeId)
    {
        return _context.MembershipTypes.Find(membershipTypeId);
    }

    public void AddMembershipType(MembershipType membershipType)
    {
        _context.MembershipTypes.Add(membershipType);
    }

    public void UpdateMembershipType(MembershipType membershipType)
    {
        _context.MembershipTypes.Update(membershipType);
    }

    public void DeleteMembershipType(MembershipType membershipType)
    {
        _context.MembershipTypes.Remove(membershipType);
    }

    public void Save()
    {
        _context.SaveChanges();
    }
}
using System.Collections.Generic;
using LibApp.Models;

namespace LibApp.ViewModels;

public class CustomerDetailsViewModel
{
    public Customer Customer { get; set; }
    public string Name { get; set; }
    public byte? MembershipTypeId { get; }
    public bool HasNewsletterSubscribed { get; }
    public IEnumerable<MembershipType> MembershipTypes { get; set; }

}
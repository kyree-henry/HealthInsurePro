using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthInsurePro.Domain
{
    public enum ExpenseType
    {
        Procedure,
        Prescription
    }

    public enum ClaimStatus
    {
        Submitted = 100,
        Approved = 202,
        Declined = 500,
        InReview = 102,
    }

    public enum UserType
    {
        User = 0,
        Admin = 2,
        Audit = 3
    }
}

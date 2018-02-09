using System;
using System.Collections.Generic;
using DomainDrivenDesign.DomainCheckin.Entities;

namespace DomainDrivenDesign.DomainCheckin.Services
{
    public interface IEmployeeServices
    {
        List<EmployeeInfo> FindStaffWithValidEffectiveDate(DateTime startDate, DateTime endDate);
    }
}
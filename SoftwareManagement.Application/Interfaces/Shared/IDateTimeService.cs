using System;

namespace SoftwareManagement.Application.Interfaces.Shared
{
    public interface IDateTimeService
    {
        DateTime NowUtc { get; }
    }
}
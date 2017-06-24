using System;
namespace BebemundiWebAPI.Services
{
    public interface IBebemundiWebAPIIdentityService
    {
        string CurrentUser { get; }
    }
}

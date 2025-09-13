using Grad_Project_G2.BLL.ViewModels;
using System.Collections.Generic;

namespace Grad_Project_G2.BLL.Services
{
    public interface ISessionService
    {
        PagedResult<SessionViewModel> GetAllSessions(string? search, int page, int pageSize);
        SessionViewModel? GetSessionById(int id);
        void AddSession(SessionViewModel vm);
        void UpdateSession(SessionViewModel vm);
        void DeleteSession(int id);
    }
}

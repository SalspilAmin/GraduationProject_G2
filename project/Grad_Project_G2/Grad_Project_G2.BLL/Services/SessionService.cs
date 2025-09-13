using Grad_Project_G2.BLL.Services.Interfaces;
using Grad_Project_G2.BLL.ViewModels;
using Grad_Project_G2.DAL.Models;
using Grad_Project_G2.DAL.Repositories.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Grad_Project_G2.BLL.Services
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SessionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public PagedResult<SessionViewModel> GetAllSessions(string? search, int page, int pageSize)
        {
            var sessions = _unitOfWork.Sessions.GetAll().ToList();
            var courses = _unitOfWork.Courses.GetAll().ToList();

            var sessionVMs = sessions
                .Join(
                    courses,
                    s => s.CourseId,
                    c => c.Id,
                    (s, c) => new SessionViewModel
                    {
                        Id = s.Id,
                        CourseId = s.CourseId,
                        CourseName = c.Name,
                        StartDate = s.StartDate,
                        EndDate = s.EndDate
                    }
                );

            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToLower();
                sessionVMs = sessionVMs.Where(s => s.CourseName != null && s.CourseName.ToLower().Contains(search));
            }

            var totalItems = sessionVMs.Count();

            var items = sessionVMs
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return new PagedResult<SessionViewModel>
            {
                Items = items,
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = totalItems
            };
        }


        public SessionViewModel? GetSessionById(int id)
        {
            var session = _unitOfWork.Sessions.GetById(id);
            if (session == null) return null;

            var course = _unitOfWork.Courses.GetById(session.CourseId);

            return new SessionViewModel
            {
                Id = session.Id,
                CourseId = session.CourseId,
                StartDate = session.StartDate,
                EndDate = session.EndDate,
                CourseName = course?.Name  
            };
        }

        public void AddSession(SessionViewModel vm)
        {
            var session = new Session
            {
                CourseId = vm.CourseId,
                StartDate = vm.StartDate,
                EndDate = vm.EndDate
            };

            _unitOfWork.Sessions.Add(session);
            _unitOfWork.Save();
        }

        public void UpdateSession(SessionViewModel vm)
        {
            var session = _unitOfWork.Sessions.GetById(vm.Id);
            if (session == null) return;

            session.CourseId = vm.CourseId;
            session.StartDate = vm.StartDate;
            session.EndDate = vm.EndDate;

            _unitOfWork.Sessions.Update(session);
            _unitOfWork.Save();
        }

        public void DeleteSession(int id)
        {
            _unitOfWork.Sessions.Delete(id);
            _unitOfWork.Save();
        }
    }
}

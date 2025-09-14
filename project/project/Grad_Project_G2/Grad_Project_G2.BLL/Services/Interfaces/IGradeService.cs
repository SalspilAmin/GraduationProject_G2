using Grad_Project_G2.BLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.BLL.Services.Interfaces
{
    public interface IGradeService
    {
        Task<PagedResult<GradeViewModel>> GetPagedGradesAsync(int pageNumber, int pageSize, string search);
        Task<GradeViewModel> PrepareGradeViewModel(GradeViewModel? model = null);
        Task CreateGradeAsync(GradeViewModel model);
        Task UpdateGradeAsync(GradeViewModel model);
        Task DeleteGradeAsync(int id);
        Task<GradeViewModel?> GetByIdAsync(int id);
    }
}

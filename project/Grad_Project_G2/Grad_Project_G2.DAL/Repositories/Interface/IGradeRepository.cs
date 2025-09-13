using Grad_Project_G2.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Grad_Project_G2.DAL.Repositories.Interface
{
    public interface IGradeRepository : IGenericRepository<Grade>
    {
        Task<IEnumerable<Grade>> GetGradesByTraineeAsync(int traineeId);
        Task<Grade?> GetBySessionAndTraineeAsync(int sessionId, int traineeId);
    }
}

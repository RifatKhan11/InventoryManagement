using InventoryManagementNewVision.Data;
using InventoryManagementNewVision.Services.Dapper.IInterfaces;
using InventoryManagementNewVision.Services.EmployeeService.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;

namespace InventoryManagementNewVision.Services.EmployeeService
{
    public class EmployeeServices : IEmployeeServices
    {
        private readonly AppDbContext _context;
        private readonly IDapper _dapper;

        public EmployeeServices(AppDbContext context, IDapper dapper)
        {
            _context = context;
            this._dapper = dapper;
        }

       // #region Employee 
       // public async Task<IEnumerable<EmployeeInfoModel>> GetAllAsync()
       // {
       //     return await _context.Employees
       //        .Include(e => e.department).Where(x => x.isActive == true)
       //        .Select(e => new EmployeeInfoModel
       //        {
       //            Id = e.Id,
       //            name = e.name,
       //            email = e.email,
       //            phoneNumber = e.phoneNumber,
       //            departmentId = e.departmentId,
       //            department = e.department != null ? e.department.departmentName : null,
       //            position = e.position,
       //            joiningDate = e.joiningDate,

       //        }).AsNoTracking().ToListAsync();
       // }

       // public async Task<IEnumerable<EmployeeInfoModel>> GetAllAsync(int page, int pageSize)
       // {
       //     int count = await _context.Employees.Where(x => x.isActive == true).CountAsync();
       //     return await _context.Employees
       //.Include(e => e.department).Where(x => x.isActive == true)
       //.Select(e => new EmployeeInfoModel
       //{
       //    Id = e.Id,
       //    name = e.name,
       //    email = e.email,
       //    phoneNumber = e.phoneNumber,
       //    departmentId = e.departmentId,
       //    department = e.department != null ? e.department.departmentName : null,
       //    position = e.position,
       //    joiningDate = e.joiningDate,
       //    CurrentPage = page,
       //    TotalRecords = count,
       //    PageSize = pageSize,
       //    TotalPages = (int)Math.Ceiling((double)count / pageSize)
       //}).Skip((page - 1) * pageSize).Take(pageSize).OrderByDescending(x=>x.Id).ToListAsync();
       // }

       // public async Task<EmployeeInfo> GetByIdAsync(int id)
       // {
       //     return await _context.Employees
       //         .Include(e => e.department).AsNoTracking()
       //         .FirstOrDefaultAsync(e => e.Id == id);
       // }

       // public async Task AddAsync(EmployeeInfo employee)
       // {
       //     await _context.Employees.AddAsync(employee);
       //     await _context.SaveChangesAsync();
       // }

       // public async Task UpdateAsync(EmployeeInfo employee)
       // {
       //     var existingEmployee = await _context.Employees.FindAsync(employee.Id);
       //     if (existingEmployee != null)
       //     {
       //         existingEmployee.name = employee.name;
       //         existingEmployee.email = employee.email;
       //         existingEmployee.phoneNumber = employee.phoneNumber;
       //         existingEmployee.departmentId = employee.departmentId;
       //         existingEmployee.position = employee.position;
       //         existingEmployee.joiningDate = employee.joiningDate;
       //         existingEmployee.isActive = true;

       //         _context.Employees.Update(existingEmployee);
       //         await _context.SaveChangesAsync();
       //     }
       // }

       // public async Task DeleteAsync(int id)
       // {
       //     var existingEmployee = await _context.Employees.FindAsync(id);
       //     if (existingEmployee != null)
       //     {
       //         existingEmployee.isActive = false;

       //         _context.Employees.Update(existingEmployee);
       //         await _context.SaveChangesAsync();
       //     }
       // }

       // public async Task<IEnumerable<Sp_EmployeeSearchModel>> GetEmployeeByFilter(string name, int deptId, string position, int score, int page, int pageSize)
       // {

       //     var leave = (page - 1) * pageSize;
       //     var data = await _dapper.FromSqlAsync<Sp_EmployeeSearchModel>($"Sp_GetEmployeeBy '{name}',{deptId},'{position}',{score},{leave},{pageSize}");            
       //     return data;
       // }
       // #endregion

       // #region Performance

       // public async Task<IEnumerable<PerformanceReviewModel>> GetAllPerformanceAsync(int page, int pageSize)
       // {
       //     int count = await _context.PerformanceReviews.Where(x => x.isActive == true).CountAsync();

       //                 return await _context.PerformanceReviews
       //            .Include(e => e.employee).Include(x => x.employee.department)
       //            .Where(x => x.isActive == true)
       //            .Select(e => new PerformanceReviewModel
       //            {
       //                Id = e.Id,
       //                reviewDate = e.reviewDate,
       //                employeeName = e.employee.name,
       //                score = e.score,
       //                notes = e.notes,
       //                dept = e.employee.department.departmentName,

       //                CurrentPage = page,
       //                TotalRecords = count,
       //                PageSize = pageSize,
       //                TotalPages = (int)Math.Ceiling((double)count / pageSize)
       //            }).Skip((page - 1) * pageSize).Take(pageSize).AsNoTracking().OrderByDescending(x=>x.Id).ToListAsync();
       // }
       // public async Task<PerformanceReviewModel> GetAllPerformanceAsync(int Id)
       // {
       //     int count = await _context.PerformanceReviews.Where(x => x.isActive == true).CountAsync();

       //                 return await _context.PerformanceReviews
       //            .Include(e => e.employee)
       //            .Where(x => x.isActive == true && x.Id==Id)
       //            .Select(e => new PerformanceReviewModel
       //            {
       //                Id = e.Id,
       //                employeeId=e.employeeId,
       //                reviewDate = e.reviewDate,
       //                employeeName = e.employee.name,
       //                score = e.score,
       //                notes = e.notes,    
       //            }).AsNoTracking().FirstOrDefaultAsync();
       // }

       // public async Task AddPerformanceAsync(PerformanceReview perfm)
       // {
       //     await _context.PerformanceReviews.AddAsync(perfm);
       //     await _context.SaveChangesAsync();
       // }

       // public async Task UpdatePerformanceAsync(PerformanceReview perfm)
       // {
       //     var perf = await _context.PerformanceReviews.FindAsync(perfm.Id);
       //     if (perf != null)
       //     {
       //         perf.employeeId = perfm.employeeId;
       //         perf.reviewDate = perfm.reviewDate;
       //         perf.score = perfm.score;
       //         perf.notes = perfm.notes;

       //         perf.isActive = true;

       //         _context.PerformanceReviews.Update(perf);
       //         await _context.SaveChangesAsync();
       //     }
       // }

       // public async Task DeleteReview(int id)
       // {
       //     var existingEmployee = await _context.PerformanceReviews.FindAsync(id);
       //     if (existingEmployee != null)
       //     {
       //         existingEmployee.isActive = false;

       //         _context.PerformanceReviews.Update(existingEmployee);
       //         await _context.SaveChangesAsync();
       //     }
       // }



       // public async Task<IEnumerable<Sp_PerformanceReviewModel>> GetScoreReport()
       // {
       //     var data = await _dapper.FromSqlAsync<Sp_PerformanceReviewModel>($"Sp_PerformanceReview");
            
       //     return data;




       // }

       // #endregion
    }
}

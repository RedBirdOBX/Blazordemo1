using BethanysPieShopHRM.Shared.Domain;

namespace BethanysPieShopHRM.App.Services
{
    public interface IJobCategoryDataService
    {
        Task<IEnumerable<JobCategory>> GetAllJobCategories();
        Task<JobCategory> GetJobCategory(int jobCategoryId);
    }
}

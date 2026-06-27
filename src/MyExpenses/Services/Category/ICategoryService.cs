using MyExpenses.Dtos.Category;
using MyExpenses.Dtos.Common;
using MyExpenses.Results;

namespace MyExpenses.Services.Category
{
    public interface ICategoryService
    {
        Task<Result<ResponseCategoryDto>> Create(CreateCategoryDto createCategoryDto, Guid userId);
        Task<Result<List<ResponseCategoryDto>>> FindAllByUser(Guid userId);
        Task<Result<PagedResultDto<ResponseCategoryDto>>> FindAllByUserPaginated(Guid userId, int page, int pageSize);
        Task<Result<ResponseCategoryDto>> FindById(Guid categoryId);
        Task<Result<ResponseCategoryDto>> FindByName(string name, Guid userId);
        Task<Result<ResponseCategoryDto>> UpdateById(Guid id, string name);
        Task<Result> DeleteById(Guid userId, Guid categoryId);
        Task<Result> DeleteByName(string name, Guid userId);
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectSoftwareWorkshop.Contracts;
using ProjectSoftwareWorkshop.Data;
using ProjectSoftwareWorkshop.Models.Category;
using ProjectSoftwareWorkshop.Models.Purchase;

namespace ProjectSoftwareWorkshop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MonthlyBudgetController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IPurchasesRepository _purchasesRepository;

    public MonthlyBudgetController(IMapper mapper, IPurchasesRepository purchasesRepository)
    {
        _mapper = mapper;
        _purchasesRepository = purchasesRepository;
    }

    [HttpGet("{month}/{year}")]
    public async Task<ActionResult<List<CategoryBudgetDto>>> GetMonthlyCategoriesBudget(int month, int year)
    {
        var purchases = await _purchasesRepository.GetAllAsync();

        var filteredPurchases = purchases
            .Where(p => p.Date.Month == month && p.Date.Year == year)
            .ToList();

        var categoryGroups = filteredPurchases
            .GroupBy(p => p.Category.Id)
            .ToList();

        var result = new List<CategoryBudgetDto>();

        foreach (var group in categoryGroups)
        {
            var category = group.First().Category;
            var categoryDto = _mapper.Map<CategoryDto>(category);
            var total = group.Sum(p => p.BillCost);

            result.Add(new CategoryBudgetDto
            {
                Category = categoryDto,
                Total = total
            });
        }

        return Ok(result);
    }
    
}
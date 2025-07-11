using ProjectSoftwareWorkshop.Contracts;
using ProjectSoftwareWorkshop.Data;

namespace ProjectSoftwareWorkshop.Repositories;

public class PurchasesRepository : GenericRepository<Purchase>, IPurchasesRepository
{
    public PurchasesRepository(ProjectSoftwareWorkshopDbContext context) : base(context)
    {
    }
}
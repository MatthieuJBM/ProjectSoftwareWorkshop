using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectSoftwareWorkshop.Contracts;
using ProjectSoftwareWorkshop.Data;
using ProjectSoftwareWorkshop.Models.Purchase;

namespace ProjectSoftwareWorkshop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PurchasesController : ControllerBase
{
            private readonly IMapper _mapper;
        private readonly IPurchasesRepository _purchasesRepository;

        public PurchasesController(IMapper mapper, IPurchasesRepository purchasesRepository)
        {
            _mapper = mapper;
            _purchasesRepository = purchasesRepository;
        }

        // GET: api/Purchases
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PurchaseDto>>> GetPurchases()
        {
            var purchases = await _purchasesRepository.GetAllAsync();
            var records = _mapper.Map<List<PurchaseDto>>(purchases);
            return Ok(records);
        }

        // GET: api/Purchases/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PurchaseDto>> GetPurchase(int id)
        {
            var purchase = await _purchasesRepository.GetAsync(id);

            if (purchase == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<PurchaseDto>(purchase));
        }

        // PUT: api/Purchases/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPurchase(int id, PurchaseDto purchaseDto)
        {
            if (id != purchaseDto.Id)
            {
                return BadRequest();
            }

            var purchase = await _purchasesRepository.GetAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            _mapper.Map(purchaseDto, purchase);

            try
            {
                await _purchasesRepository.UpdateAsync(purchase);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await PurchaseExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Purchases
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Purchase>> PostPurchase(PurchaseCreateDto purchaseCreateDto)
        {
            var purchase = _mapper.Map<Purchase>(purchaseCreateDto);
            await _purchasesRepository.AddAsync(purchase);

            return CreatedAtAction("GetPurchase", new { id = purchase.Id }, purchase);
        }

        // DELETE: api/Purchases/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePurchase(int id)
        {
            var purchase = await _purchasesRepository.GetAsync(id);
            if (purchase == null)
            {
                return NotFound();
            }

            await _purchasesRepository.DeleteAsync(id);

            return NoContent();
        }

        private async Task<bool> PurchaseExists(int id)
        {
            return await _purchasesRepository.Exists(id);
        }
}
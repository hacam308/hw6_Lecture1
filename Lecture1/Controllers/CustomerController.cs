using Lecture1.Data;
using Lecture1.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly IGenericRepository<Customer> _itemRepository;

    public CustomersController(IGenericRepository<Customer> itemRepository)
    {
        _itemRepository = itemRepository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var items = await _itemRepository.GetAllAsync();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var item = await _itemRepository.GetByIdAsync(id);
        if (item == null) return NotFound();
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Create(Customer item)
    {
        await _itemRepository.AddAsync(item);
        return CreatedAtAction(nameof(GetById), new { id = item.Id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Customer item)
    {
        if (id != item.Id) return BadRequest();

        await _itemRepository.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _itemRepository.DeleteAsync(id);
        return NoContent();
    }
}

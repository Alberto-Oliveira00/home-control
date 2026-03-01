using home_control_api.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static home_control_api.DTOs.TransactionDTO;

namespace home_control_api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var transactions = await _transactionService.GetAllAsync();
        return Ok(transactions);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTransactionDto dto)
    {
        try
        {
            var createdTransaction = await _transactionService.CreateTransactionAsync(dto);
            return CreatedAtAction(nameof(GetAll), new { id = createdTransaction.TransactionId }, createdTransaction);
        }
        catch (ArgumentException ex) // Erros de ID não encontrado (Usuário ou Categoria)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex) // Erros de regras de negócio (Menor de idade, Finalidade incorreta)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}

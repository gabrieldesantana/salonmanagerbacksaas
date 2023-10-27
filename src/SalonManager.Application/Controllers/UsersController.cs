using Microsoft.AspNetCore.Mvc;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Services;
using Serilog;
using System;


namespace SalonManager.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;
    public UsersController(IUserService service)
    {
        _service = service;
    }

    // GET api/[controller]
    [ProducesResponseType((200), Type = typeof(List<User>))]
    [ProducesResponseType((400))]
    [HttpGet("")]
    public async Task<IActionResult> GetAllAsync() 
    {
        try
        {
            Log.Information("#### Obtendo todos os usuarios ####");
            var users = await _service.GetAllAsync();
            return Ok(users);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // GET api/[controller]/{id}
    [ProducesResponseType((200), Type = typeof(User))]
    [ProducesResponseType((400))]
    [ProducesResponseType((404))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id) 
    {
        try
        {
            Log.Information($"#### Obtendo o usuario de ID: {id} ####");

            var user = await _service.GetByIdAsync(id);

            if (user is null)
            {
                Log.Error($"**** Não foi possível localizar o registro useriro de ID: {id} ");
                return NotFound();
            }

            return Ok(user);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // POST api/[controller]
    [ProducesResponseType((200), Type = typeof(User))]
    [ProducesResponseType((400))]
    [HttpPost("")]
    public async Task<IActionResult> PostAsync(InputUserModel inputModel)
    {
        try
        {
            Log.Information($"#### Inserindo um novo usuario ####");

            if (inputModel is null)
            {
                Log.Error("**** As informações da Model não foram preenchidas");
                return BadRequest();
            }

            await _service.InsertAsync(inputModel);

            return Ok(inputModel);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // PUT api/[controller]
    [ProducesResponseType((204), Type = typeof(User))]
    [ProducesResponseType((400))]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, EditUserModel editModel) 
    {
        try
        {
            Log.Information($"#### Atualizando o usuario de ID = {id} ####");

            if (editModel is null)
            {
                Log.Error("**** As informações da Model não foram preenchidas");
                return BadRequest();
            }

            var model = await _service.UpdateAsync(id, editModel);

            if (model is null)
            {
                Log.Error("**** Houve um problema ao processar essa requisição");
                return BadRequest();
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // DELETE api/[controller]/{id}
    [ProducesResponseType((204))]
    [ProducesResponseType((400))]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id) 
    {
        try
        {
            Log.Information($"#### Excluindo o usuario de ID = {id} ####");

            var wasDelete = await _service.DeleteAsync(id);

            if (!wasDelete)
            {
                Log.Error("**** Houve um problema ao processar essa requisição");
                return BadRequest();
            }

            return NoContent();
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }
}

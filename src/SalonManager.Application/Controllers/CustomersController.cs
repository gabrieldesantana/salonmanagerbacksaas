﻿using Microsoft.AspNetCore.Mvc;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Services;
using Serilog;


namespace SalonManager.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerService _service;
    public CustomersController(ICustomerService service)
    {
        _service = service;
    }

    // GET api/[controller]
    [ProducesResponseType((200), Type = typeof(List<Customer>))]
    [ProducesResponseType((404))]
    [HttpGet("")]
    public async Task<IActionResult> GetAllAsync() 
    {
        try
        {
            Log.Information("#### Obtendo todos os clientes ####");
            var customers = await _service.GetAllAsync();
            return Ok(customers);
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            return StatusCode(400, exception.Message);
        }
    }


    // GET api/[controller]/{id}
    [ProducesResponseType((200), Type = typeof(Customer))]
    [ProducesResponseType((404))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id) 
    {
        try
        {
            Log.Information($"#### Obtendo o cliente de ID = {id} ####");

            var customer = await _service.GetByIdAsync(id);

            if (customer is null) return NotFound();

            return Ok(customer);
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            return StatusCode(400, exception.Message);
        }
    }


    // POST api/[controller]
    [ProducesResponseType((200), Type = typeof(Customer))]
    [ProducesResponseType((400))]
    [ProducesResponseType((404))]
    [HttpPost("")]
    public async Task<IActionResult> PostAsync(InputCustomerModel inputModel)
    {
        try
        {
            Log.Information($"#### Inserindo um novo cliente ####");

            if (inputModel is null) return BadRequest();

            await _service.InsertAsync(inputModel);

            return CreatedAtAction(nameof(GetByIdAsync), new { Id = inputModel.Id }, inputModel);
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            return StatusCode(400, exception.Message);
        }
    }


    // PUT api/[controller]
    [ProducesResponseType((200), Type = typeof(Customer))]
    [ProducesResponseType((404))]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, EditCustomerModel editModel) 
    {
        try
        {
            Log.Information($"#### Atualizando o cliente de ID = {id} ####");

            if (editModel is null) return BadRequest();

            var model = await _service.UpdateAsync(id, editModel);

            if (model is null) return BadRequest();
            return NoContent();
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message);
            return StatusCode(400, ex.Message);
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
            Log.Information($"#### Excluindo o cliente de ID = {id} ####");

            var wasDelete = await _service.DeleteAsync(id);

            if (!wasDelete) return BadRequest();
            return NoContent();
        }
        catch (Exception exception)
        {
            Log.Error(exception.Message);
            return StatusCode(400, exception.Message);
        }
    }
}

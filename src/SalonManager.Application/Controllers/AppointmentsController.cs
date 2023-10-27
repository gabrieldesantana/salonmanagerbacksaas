using Microsoft.AspNetCore.Mvc;
using SalonManager.Domain.Entities;
using SalonManager.Domain.Interfaces.Services;
using Serilog;
using System;


namespace SalonManager.Application.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AppointmentsController : ControllerBase
{
    private readonly IAppointmentService _service;
    public AppointmentsController(IAppointmentService service)
    {
        _service = service;
    }

    // GET api/[controller]
    [ProducesResponseType((200), Type = typeof(List<Appointment>))]
    [ProducesResponseType((400))]
    [HttpGet("")]
    public async Task<IActionResult> GetAllAsync()
    {
        try
        {
            Log.Information("#### Obtendo todos os agendamentos ####");
            var appointments = await _service.GetAllAsync();
            return Ok(appointments);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // GET api/[controller]/{id}
    [ProducesResponseType((200), Type = typeof(Appointment))]
    [ProducesResponseType((400))]
    [ProducesResponseType((404))]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        try
        {
            Log.Information($"#### Obtendo o agendamento de ID = {id} ####");

            var appointment = await _service.GetByIdAsync(id);

            if (appointment is null)
            {
                Log.Error($"**** Não foi possível localizar o agendamento de ID: {id} ");
                return NotFound();
            }

            return Ok(appointment);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }

    // GET api/[controller]/customerId/{id}
    [ProducesResponseType((200), Type = typeof(Appointment))]
    [ProducesResponseType((400))]
    [ProducesResponseType((404))]
    [HttpGet("customerId/{id}")]
    public async Task<IActionResult> GetByCustomerIdAsync(int customerId)
    {
        try
        {
            Log.Information($"#### Obtendo os agendamento do cliente de ID = {customerId} ####");

            var appointment = await _service.GetByCustomerIdAsync(customerId);

            if (appointment is null)
            {
                Log.Error($"**** Não foi possível localizar os agendamento do cliente de ID: {customerId} ");
                return NotFound();
            }

            return Ok(appointment);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }

    [ProducesResponseType((204))]
    [ProducesResponseType((400))]
    [ProducesResponseType((404))]
    [HttpPut("updateStatus/{id}")]
    public async Task<IActionResult> UpdateStatusAsync(int id, [FromBody] EditAppointmentModel editModel)
    {
        try
        {
            Log.Information($"#### Atualizando o status do agendamento de ID: {id} ####");

            if (editModel is null)
            {
                Log.Error("**** As informações da Model não foram preenchidas");
                return BadRequest();
            }

            var model = await _service.UpdateStatusAsync(id, editModel);
            if (model is false)
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


    // POST api/[controller]
    [ProducesResponseType((200), Type = typeof(Appointment))]
    [ProducesResponseType((400))]
    [HttpPost("")]
    public async Task<IActionResult> PostAsync([FromBody] InputAppointmentModel inputModel)
    {
        try
        {
            Log.Information($"#### Inserindo um novo agendamento ####");

            if (inputModel is null)
            {
                Log.Error("**** As informações da Model não foram preenchidas");
                return BadRequest();
            }

            var appointment = await _service.InsertAsync(inputModel);

            if (appointment is null)
               return BadRequest();

            return Ok(inputModel);
        }
        catch (Exception exception)
        {
            Log.Error($"**** {exception.Message}");
            return StatusCode(400, exception.Message);
        }
    }


    // PUT api/[controller]
    [ProducesResponseType((204))]
    [ProducesResponseType((400))]
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] EditAppointmentModel editModel) 
    {
        try
        {
            Log.Information($"#### Atualizando o agendamento de ID = {id} ####");

            if (editModel is null)
            {
                Log.Error("**** As informações da Model não foram preenchidas");
                return BadRequest();
            }

            var model = await _service.UpdateAsync(id, editModel);

            if (model is false)
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
            Log.Information($"#### Excluindo o agendamento de ID = {id} ####");

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

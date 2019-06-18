using System;
using Microsoft.AspNetCore.Mvc;
using manageContacts.Repositories;
using System.Collections.Generic;
using manageContacts.Entities.Models;
using manageContacts.Services;
using manageContacts.Entities.ExtendedModels;
using System.IO;
using Newtonsoft.Json;
using manageContacts.Entities;
using System.Threading.Tasks;

[Route("contacts")]
[ApiController]
public class contactController : ControllerBase
{
    private SimContactRepository _simContactRepo;

    private ContactService _contactService;

    public contactController(
        SimContactRepository simContactRepository,
        ContactService contactService
        )
    {
        _simContactRepo = simContactRepository;
        _contactService = contactService;
    }

    [HttpGet]
    public IActionResult getContacts()
    {
        try
        {
            List<SimContactExtended> contacts = _contactService.getAllExtendedContacts();
            return Ok(contacts);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpGet("{guid}")]
    public IActionResult getContact(Guid guid)
    {
        try
        {
            SimContact contact = _simContactRepo.findByGuid(guid);
            contactDetailsExtended clientContact = new contactDetailsExtended(contact);
            return Ok(JsonConvert.SerializeObject(clientContact));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpPost("new")]
    public IActionResult createContact([FromBody] ClientContactModel clientContactModel)
    {
        try
        {
            IEntity contact = _contactService.createContact(clientContactModel);
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpPost("edit")]
    public IActionResult editContact([FromBody] ClientContactModel clientContactModel)
    {
        try
        {
            IEntity contact = _contactService.editContact(clientContactModel);
            return Ok(contact);
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }

    [HttpGet("delete/{guid}")]
    public IActionResult deleteContact(Guid guid)
    {
        try
        {
            _contactService.deleteContact(guid);
            return Ok(_contactService.getAllExtendedContacts());
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}
using System;
using Microsoft.AspNetCore.Mvc;
using manageContacts.Repositories;
using System.Collections.Generic;
using manageContacts.Entities.Models;
using manageContacts.Services;
using Newtonsoft.Json;

[Route("mobileNumbers")]
[ApiController]
public class mobileNumberController : ControllerBase
{
    private MobileNumberRepository _mobileNumberRepo;
    private MobileNumberService _mobileNumberService;
    private contactTagRepository _contactTagRepo;
    private SimContactRepository _simContactRepo;
    public mobileNumberController(
        MobileNumberRepository mobileNumberRepo,
        MobileNumberService mobileNumberService,
        contactTagRepository contactTagRepository,
        SimContactRepository simContactRepository
        )
    {
        _mobileNumberRepo = mobileNumberRepo;
        _mobileNumberService = mobileNumberService;
        _contactTagRepo = contactTagRepository;
        _simContactRepo = simContactRepository;
    }


    [HttpGet("get/{number}")]
    public IActionResult getContactsByMobileNumber(string number)
    {
        try
        {
            SimContact contact = _mobileNumberService.getContactByMobileNumber(number);
            return Ok(JsonConvert.SerializeObject(contact));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex);
        }
    }
}
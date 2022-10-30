﻿using Bachelor_Server.BusinessLayer.Services.Account;
using Bachelor_Server.BusinessLayer.Services.Requests;
using Bachelor_Server.Models.Account;
using Bachelor_Server.Models.WorkerConfiguration;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Bachelor_Server.BusinessLayer.Controllers;

[ApiController]
public class AccountController : ControllerBase
{
    private IAccountService _accountService;
    
    
    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost]
    public async Task<AccountModel> GetLoggedAccount()
    {
        var reader = new StreamReader(Request.Body);
        reader.BaseStream.Seek(0, SeekOrigin.Begin);
        var rawMessage = await reader.ReadToEndAsync();
        return await _accountService.GetLoggedAccount(
            JsonConvert.DeserializeObject<AccountModel>(rawMessage));
    }
}
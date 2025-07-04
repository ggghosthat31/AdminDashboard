﻿using AdminDashboard.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AdminDashboard.Entity.Dto;
using AdminDashboard.Repository.Managers;

namespace AdminDashboard.API.Controller;

[ApiController]
[Route("api/authentication")]
public class AuthenticationController : ControllerBase
{
    private readonly UserManager<Client> _userManager;
    private readonly AuthenticationManager _authManager;

    public AuthenticationController(
        AuthenticationManager authManager,
        UserManager<Client> userManager)
    {
        _authManager = authManager;
        _userManager = userManager;
    }


    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] ClientForRegistration clientForregistration)
    {
        var client = new Client
        {
            UserName = clientForregistration.Name,
            Email = clientForregistration.Email,
            Password = clientForregistration.Password,
        };

        var result = await _userManager.CreateAsync(client, client.Password);
        if (!result.Succeeded)
        {
            foreach (var err in result.Errors)
                ModelState.TryAddModelError(err.Code, err.Description);

            return BadRequest(ModelState);
        }

        await _userManager.AddToRolesAsync(client, clientForregistration.Roles);
        
        return StatusCode(201);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] ClientForAuthorization userAuthentication)
    {
        if (!await _authManager.ValidateUser(userAuthentication))
            return Unauthorized();

        return Ok(new { Token = await _authManager.CreateToken() });
    }
}
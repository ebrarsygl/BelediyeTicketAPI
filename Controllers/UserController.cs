using BelediyeTicketAPI.DTOs.User;
using BelediyeTicketAPI.Helpers;
using BelediyeTicketAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BelediyeTicketAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<ApiResponse<IEnumerable<UserDto>>>> GetUsers()
    {
        var users = await _userService.GetAllAsync();

        return Ok(new ApiResponse<IEnumerable<UserDto>>(
            true,
            "Kullanıcılar başarıyla getirildi.",
            users));
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<UserDto>>> GetUser(int id)
    {
        var user = await _userService.GetByIdAsync(id);

        if (user == null)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Kullanıcı bulunamadı.",
                null));
        }

        return Ok(new ApiResponse<UserDto>(
            true,
            "Kullanıcı başarıyla getirildi.",
            user));
    }

    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<ApiResponse<UserDto>>> CreateUser(CreateUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "Gönderilen veriler hatalı.",
                ModelState));
        }

        var createdUser = await _userService.CreateAsync(dto);

        return CreatedAtAction(
            nameof(GetUser),
            new { id = createdUser.Id },
            new ApiResponse<UserDto>(
                true,
                "Kullanıcı başarıyla oluşturuldu.",
                createdUser));
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> UpdateUser(int id, UpdateUserDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(new ApiResponse<object>(
                false,
                "Gönderilen veriler hatalı.",
                ModelState));
        }

        var updated = await _userService.UpdateAsync(id, dto);

        if (!updated)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Kullanıcı bulunamadı.",
                null));
        }

        return Ok(new ApiResponse<object>(
            true,
            "Kullanıcı başarıyla güncellendi.",
            null));
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<ApiResponse<object>>> DeleteUser(int id)
    {
        var deleted = await _userService.DeleteAsync(id);

        if (!deleted)
        {
            return NotFound(new ApiResponse<object>(
                false,
                "Kullanıcı bulunamadı.",
                null));
        }

        return Ok(new ApiResponse<object>(
            true,
            "Kullanıcı başarıyla silindi.",
            null));
    }
}
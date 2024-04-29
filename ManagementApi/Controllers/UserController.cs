using Auth.Authorization;
using ManagementApi.Repo.Users.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ManagementApi.Controllers;

[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class UserController(IUserRepo userRepo) : ControllerBase
{
    // GET api/<UserController>/5
    [HttpGet]
    [AuthorizedAction(Permissions.UserRead)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UserOutputModelSimple>))]
    public async Task<IActionResult> Get()
    {
        var res = await userRepo.GetAllAsync();
        return Ok(res);
    }

    // GET api/<UserController>/5
    [HttpGet("{id}")]
    [AuthorizedAction(Permissions.UserRead)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserOutputModelDetailed))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var res = await userRepo.GetByIdAsync(id);

        return res != null ? Ok(res) : NotFound();
    }

    // POST api/<UserController>
    [HttpPost]
    [AuthorizedAction(Permissions.UserCreate)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserOutputModelSimple))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Post([FromBody] UserInputModel value)
    {
        var res = await userRepo.CreateAsync(value);
        return res.Match<ObjectResult>(Ok, BadRequest);
    }

    // DELETE api/<UserController>/5
    [HttpDelete]
    [AuthorizedAction(Permissions.UserDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int[] ids)
    {
        await userRepo.DeleteAsync(ids);
        return NoContent();
    }

    [HttpGet("filter")]
    [AuthorizedAction(Permissions.UserRead)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationModel<UserOutputModelSimple>))]
    public async Task<IActionResult> Filter([FromQuery] UserFilterModel filter)
    {
        var res = await userRepo.FilterAsync(filter);
        return Ok(res);
    }

    // PUT api/<BranchController>/5
    [HttpPut]
    [AuthorizedAction(Permissions.UserWrite)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserOutputModelSimple))]
    [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Put([FromBody] UserUpdateModel value)
    {
        var res = await userRepo.UpdateAsync(value);
        return res.Match<ObjectResult>(Ok, NotFound);
    }
}


namespace ManagementApi.Controllers;

[Route("api/[controller]")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class RoleController(IRoleRepo roleRepo) : ControllerBase
{
    // GET: api/<RoleController>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await roleRepo.GetAllAsync());
    }

    // GET api/<RoleController>/5
    [HttpGet("{id}")]
    [AuthorizedAction(Permissions.RoleRead)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleOutputModelDetailed))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var res = await roleRepo.GetByIdAsync(id);

        return res != null ? Ok(res) : NotFound();
    }

    // POST api/<RoleController>
    [HttpPost]
    [AuthorizedAction(Permissions.RoleCreate)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleOutputModelSimple))]
    [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ErrorResponse))]
    public async Task<IActionResult> Post([FromBody] RoleInputModel value)
    {
        var res = await roleRepo.CreateAsync(value);
        return res.Match<ObjectResult>(Ok, BadRequest);
    }

    // DELETE api/<RoleController>/5
    [HttpDelete]
    [AuthorizedAction(Permissions.RoleDelete)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Delete(int[] ids)
    {
        await roleRepo.DeleteAsync(ids);
        return NoContent();
    }
    [HttpGet("filter")]
    [AuthorizedAction(Permissions.RoleRead)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PaginationModel<RoleOutputModelSimple>))]
    public async Task<IActionResult> Filter([FromQuery] RoleFilterModel filter)
    {
        PaginationModel<RoleOutputModelSimple>? res = await roleRepo.FilterAsync(filter);
        return Ok(res);
    }

    // PUT api/<BranchController>/5
    [HttpPut]
    [AuthorizedAction(Permissions.RoleWrite)]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(RoleOutputModelSimple))]
    public async Task<IActionResult> Put([FromBody] RoleUpdateModel value)
    {
        var res = await roleRepo.UpdateAsync(value);
        return res.Match<ObjectResult>(Ok, NotFound);

    }

}


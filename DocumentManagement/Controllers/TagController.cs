namespace DocumentManagement.Controllers;

[Route("api/[controller]")]
public class TagController : ControllerBase
{
    private readonly ITagRepo _tagRepo;
    public TagController(ITagRepo tagRepo)
    {
        _tagRepo = tagRepo;
    }
    // GET: api/<TagController>
    [HttpGet("{id}")]
    [AuthorizedAction(Permissions.TagRead)]

    public async Task<IActionResult> Get(int id)
    {
        var res = await _tagRepo.FindById(id);
        return res == null ? NotFound() : Ok(res);
    }

    [HttpGet]
    [AuthorizedAction(Permissions.TagRead)]
    public async Task<IActionResult> Get()
    {
        var res = await _tagRepo.GetAll();
        return Ok(res);
    }

    // POST api/<TagController>
    [HttpPost]
    [AuthorizedAction(Permissions.TagCreate)]
    public async Task<IActionResult> Post([FromBody] TagInputModel value)
    {
        var res = await _tagRepo.PostAsync(value);
        return Ok(res);
    }

    // PUT api/<TagController>/5
    [HttpPut]
    [AuthorizedAction(Permissions.TagWrite)]
    public async Task<IActionResult> Put([FromBody] TagUpdateModel value)
    {
        var res = await _tagRepo.UpdateAsync(value);
        return res.Match<ObjectResult>(Ok, BadRequest);
    }

    // DELETE api/<TagController>/5
    [HttpDelete]
    [AuthorizedAction(Permissions.TagDelete)]
    public async Task<IActionResult> Delete(int[] ids)
    {
        await _tagRepo.DeleteAsync(ids);
        return NoContent();
    }
    [HttpPut("Enable")]
    [AuthorizedAction(Permissions.TagWrite)]

    public async Task<IActionResult> Enable(int[] ids)
    {
        await _tagRepo.EnableAsync(ids);
        return NoContent();
    }
    [HttpPut("Disable")]
    [AuthorizedAction(Permissions.TagWrite)]
    public async Task<IActionResult> Disable(int[] ids)
    {
        await _tagRepo.DisableAsync(ids);
        return NoContent();
    }
    [HttpGet("filter")]
    [AuthorizedAction(Permissions.TagRead)]
    public async Task<IActionResult> Filter([FromQuery] TagFilterModel filter)
    {
        var res = await _tagRepo.FilterAsync(filter);
        return Ok(res);
    }
}


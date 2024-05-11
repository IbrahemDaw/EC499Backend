

namespace DocumentManagement.Controllers;

[Route("api/[controller]")]
public class CategoryController(ICategoryRepo categoryRepo) : ControllerBase
{
    // GET: api/<CategoryController>
    [HttpGet]
    [AuthorizedAction(Permissions.CategoryRead)]
    public async Task<IActionResult> Get()
    {
        var res = await categoryRepo.GetAsync();
        return Ok(res);
    }

    // GET api/<CategoryController>/5
    [HttpGet("{id}")]
    [AuthorizedAction(Permissions.CategoryRead)]
    public async Task<IActionResult> Get(int id)
    {
        var res = await categoryRepo.GetAsync(id);
        return res == null ? NotFound() : Ok(res);
    }

    // POST api/<CategoryController>
    [HttpPost]
    [AuthorizedAction(Permissions.CategoryCreate)]
    public async Task<IActionResult> Post([FromBody] CategoryInputModel Model)
    {
        var res = await categoryRepo.CreateAsync(Model);
        return res.Match<ObjectResult>(Ok, NotFound);
    }

    // PUT api/<CategoryController>/5
    [HttpPut]
    [AuthorizedAction(Permissions.CategoryWrite)]
    public async Task<IActionResult> Put([FromBody] CategoryUpdateModel model)
    {
        var res = await categoryRepo.UpdateAsync(model);
        return res.Match<ObjectResult>(Ok, BadRequest);
    }

    // DELETE api/<CategoryController>/5
    [HttpDelete]
    [AuthorizedAction(Permissions.CategoryDelete)]
    public async Task<IActionResult> Delete(int[] ids)
    {
        var res = await categoryRepo.DeleteAsync(ids);
        return res == null ? NoContent() : BadRequest(res);
    }
    [HttpGet("filter")]
    [AuthorizedAction(Permissions.CategoryRead)]
    public async Task<IActionResult> FilterAsync(CategoryFilterModel filterModel)
    {
        var res = await categoryRepo.FilterAsync(filterModel);
        return Ok(res);
    }
}

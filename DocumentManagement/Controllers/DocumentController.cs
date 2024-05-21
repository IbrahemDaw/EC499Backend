namespace DocumentManagement.Controllers;
[Route("api/[controller]")]
public class DocumentController(IDocumentRepo _documentRepo) : ControllerBase
{
    [HttpGet("{id}")]
    [AuthorizedAction(Permissions.DocumentRead)]
    public async Task<IActionResult> GetDocumentById(int id)
    {
        var document = await _documentRepo.GetByIdAsync(id);
        return Ok(document);
    }

    [HttpPost]
    [AuthorizedAction(Permissions.DocumentCreate)]

    public async Task<IActionResult> UploadDocument(DocumentInputModel model)
    {
        await _documentRepo.UplaodAsync(model);
        return NoContent();
    }

    [HttpPut]
    [AuthorizedAction(Permissions.DocumentWrite)]
    public async Task<IActionResult> UpdateDocument([FromBody] DocumentUpdateModel model)
    {
        await _documentRepo.UpdateAsync(model);
        return NoContent();
    }

    [HttpDelete]
    [AuthorizedAction(Permissions.DocumentDelete)]
    public async Task<IActionResult> DeleteDocument(int[] ids)
    {
        await _documentRepo.DeleteAsync(ids);
        return NoContent();
    }
    [HttpGet("filter")]
    [AuthorizedAction(Permissions.DocumentRead)]
    public async Task<IActionResult> Filter(DocumentFilterModel filter)
    {
        var res = await _documentRepo.FilterAsync(filter);
        return Ok(res);
    }
    [HttpGet("download/{Id}")]
    [AuthorizedAction(Permissions.DocumentRead)]
    public async Task<IActionResult> DownloadAsync(int Id)
    {
        var res = await _documentRepo.DownloadAsync(Id);
        return File(res.File, res.FileContent, res.Title);
    }
}

namespace DMS;
[Route("api/[controller]")]
public class DocumentController(IDocumentRepo _documentRepo) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetDocumentById(int id)
    {
        var document = await _documentRepo.GetByIdAsync(id);
        return Ok(document);
    }

    [HttpPost]
    public async Task<IActionResult> UploadDocument(DocumentInputModel model)
    {
        await _documentRepo.UplaodAsync(model);
        return NoContent();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateDocument([FromBody] DocumentUpdateModel model)
    {
        await _documentRepo.UpdateAsync(model);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDocument(int id)
    {
        await _documentRepo.DeleteAsync(id);
        return NoContent();
    }
    [HttpGet("filter")]
    public async Task<IActionResult> Filter(DocumentFilterModel filter)
    {
        var res = await _documentRepo.FilterAsync(filter);
        return Ok(res);
    }
    [HttpGet("download/{Id}")]
    public async Task<IActionResult> DownloadAsync(int Id)
    {
        var res = await _documentRepo.DownloadAsync(Id);
        return File(res.File, res.FileContent, res.Title);
    }
}

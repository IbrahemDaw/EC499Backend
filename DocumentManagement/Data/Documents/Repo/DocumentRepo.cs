namespace DocumentManagement.Data.Repo;
public class DocumentRepo(DMSDbContext _db) : IDocumentRepo
{
    public async Task DeleteAsync(int Ids)
    {
        var doc = await _db.Documents.Where(x => Ids == x.Id).SingleAsync();
        if (File.Exists(doc.Path))
        {
            File.Delete(doc.Path);
        }
        _db.Documents.Remove(doc);
        await _db.SaveChangesAsync();
    }

    public async Task<DocumentDownloadModel> DownloadAsync(int Id)
    {
        var doc = (await _db.Documents.FindAsync(Id))!;
        var fileByte = await File.ReadAllBytesAsync(doc.Path);
        return new DocumentDownloadModel
        {
            File = fileByte,
            Title = doc.Title,
            FileContent = GetMimeType(doc.Path)
        };
    }

    public async Task<PaginationModel<DocumentOutputModelSimple>> FilterAsync(DocumentFilterModel filter)
    {
        var res = _db.Documents.Where(x => (string.IsNullOrEmpty(filter.Title) || x.Title.Contains(filter.Title))
                                            && (string.IsNullOrEmpty(filter.Description) || x.Description.Contains(filter.Description)));
        if (filter.Tags.Count > 0)
        {
            foreach (var tag in filter.Tags)
            {
                res = res.Where(x => x.Tags.Select(t => t.Id).Contains(tag));
            }
        }
        if (filter.Categories.Count > 0)
        {
            foreach (var cat in filter.Categories)
            {
                res = res.Where(x => x.Tags.Select(t => t.Id).Contains(cat));
            }
        }
        return await res.MapTo<DocumentOutputModelSimple>().ToPaginationModelAsync(filter);

    }

    public async Task<DocumentOutputModel?> GetByIdAsync(int Id)
    {
        return await _db.Documents.Where(x => x.Id == Id)
            .Include(x => x.Tags)
            .Include(x => x.Categories)
            .MapTo<DocumentOutputModel>()
            .SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(DocumentUpdateModel model)
    {
        var doc = await _db.Documents.Where(x => x.Id == model.Id)
            .Include(x => x.Categories)
            .Include(x => x.Tags)
            .SingleAsync();
        doc.Title = model.Title;
        doc.Description = model.Description;
        doc.Categories = await _db.Categories.Where(x => model.Categories.Contains(x.Id))
            .ToListAsync();
        doc.Tags = await _db.Tags.Where(x => model.Tags.Contains(x.Id))
            .ToListAsync();
        await _db.SaveChangesAsync();
    }


    public async Task UplaodAsync(DocumentInputModel model)
    {
        var document = new Document
        {
            Title = model.Title,
            Description = model.Description,
            Categories = await _db.Categories.Where(c => model.Categories.Contains(c.Id)).ToListAsync(),
            Tags = await _db.Tags.Where(t => model.Tags.Contains(t.Id)).ToListAsync(),
            Path = "testPath"
        };

        await _db.Documents.AddAsync(document);
        await _db.SaveChangesAsync();
    }
    private string GetMimeType(string path)
    {
        var extension = Path.GetExtension(path);
        var mimeTypes = new Dictionary<string, string>
        {
            {".txt", "text/plain"},
            {".pdf", "application/pdf"},
            {".doc", "application/vnd.ms-word"},
            {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
            {".xls", "application/vnd.ms-excel"},
            {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
            {".jpg", "image/jpeg"},
            {".png", "image/png"},
            {".gif", "image/gif"},
            {".csv", "text/csv"}
        };

        if (mimeTypes.ContainsKey(extension))
        {
            return mimeTypes[extension];
        }
        else
        {
            throw new Exception("Unsupported file extension"); // غيرها باش لما يجيب فيل مش معروف يرده

        }

    }
}

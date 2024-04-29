﻿using DMS.Data.Models.Categories;
using DMS.Data.Models.Folders;
using DMS.Data.Models.Tags;

namespace DMS.Data.Models.Documents;

public class DocumentOutputModelSimple
{
    public int Id { get; set; }
    public string Title { get; set; } = null!;
    public string Description { get; set; } = null!;

}
public class DocumentOutputModel : DocumentOutputModelSimple
{
    public string Previwe { get; set; } = null!;
    public List<CategoryOutputModelSimple> Categories { get; set; } = [];
    public List<TagOutputModel> Tags { get; set; } = [];
}
public class DocumentFileOutputModel
{
    public string Path { get; set; } = null!;
    public string Title { get; set; } = null!;
}
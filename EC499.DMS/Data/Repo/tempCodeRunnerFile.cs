        var fileName = Path.GetFileName(model.File.FileName);
        var filePath = Path.Combine(webHostEnvironment.ContentRootPath, fileName);
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await model.File.CopyToAsync(stream);
        }
        document.Path = filePath;
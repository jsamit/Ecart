﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Catalog.API.Entities;

[Table("TBLPRODUCT")]
public class ProductEO
{
    public Guid Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string ImageFile { get; set; } = default!;
    public List<string> Category { get; set; } = new List<string>();
}

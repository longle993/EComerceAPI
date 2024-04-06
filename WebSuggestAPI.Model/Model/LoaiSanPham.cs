using System;
using System.Collections.Generic;

namespace WebSuggestAPI.Model.Model;

public partial class LoaiSanPham
{
    public string IdLoaiSp { get; set; } = null!;

    public string? LoaiSp { get; set; }

    public virtual ICollection<SanPham> SanPhams { get; set; } = new List<SanPham>();
}

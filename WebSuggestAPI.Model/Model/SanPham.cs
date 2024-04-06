using System;
using System.Collections.Generic;

namespace WebSuggestAPI.Model.Model;

public partial class SanPham
{
    public string IdSanPham { get; set; } = null!;

    public string? TenSanPham { get; set; }

    public string? IdLoaiSp { get; set; }

    public decimal? GiaSp { get; set; }

    public virtual ICollection<HinhAnhSanPham> HinhAnhSanPhams { get; set; } = new List<HinhAnhSanPham>();

    public virtual LoaiSanPham? IdLoaiSpNavigation { get; set; }
}

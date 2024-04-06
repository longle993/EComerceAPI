using System;
using System.Collections.Generic;

namespace WebSuggestAPI.Model.Model;

public partial class HinhAnhSanPham
{
    public string IdHinhAnh { get; set; } = null!;

    public string? IdSanPham { get; set; }

    public string? HinhAnh { get; set; }

    public virtual SanPham? IdSanPhamNavigation { get; set; }
}

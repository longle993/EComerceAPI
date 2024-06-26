﻿using System;
using System.Collections.Generic;

namespace WebSuggestAPI.Model.Model;

public partial class HoaDonShow
{
    public string IdHoaDon { get; set; } = null!;

    public string? SanPham { get; set; }

    public HoaDonShow() { }
    public HoaDonShow(string idHoaDon, string? sanPham)
    {
        IdHoaDon = idHoaDon;
        SanPham = sanPham;
    }
}

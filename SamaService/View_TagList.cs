﻿using System;

namespace SamaService
{
    /// <summary>
    /// لیست تگ
    /// </summary>
    public class View_TagList
    {
        public int ID { get; set; }
        public string Tag { get; set; }
        public DateTime dateRegister { get; set; }
        public int Reg { get; set; }
        public int TypeImport { get; set; }
    }
}
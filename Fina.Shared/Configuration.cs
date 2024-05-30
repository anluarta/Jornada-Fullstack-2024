﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fina.Shared
{
    public static class Configuration
    {
        public const int DefaultStatusCode  = 200;
        public const int DefaultPageNumber  = 1;
        public const int DefaultPageSize = 10;

        public static string BackendUrl { get; set; } = string.Empty;
        public static string FrontendUrl { get; set; } = string.Empty;

    }
}

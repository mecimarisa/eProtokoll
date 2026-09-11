using System;
using System.Collections.Generic;
using System.Text;

namespace eProtokoll
{
    internal static class UserSession
    {
        public static int PerdoruesiId { get; set; }

        public static string EmriPlote { get; set; }
            = string.Empty;

        public static string Roli { get; set; }
            = string.Empty;

        public static void Pastro()
        {
            PerdoruesiId = 0;
            EmriPlote = string.Empty;
            Roli = string.Empty;
        }
    }
}

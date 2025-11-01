using System;
using System.Collections.Generic;
using System.Text;

namespace OpenRPReloaded.Utility
{

    /// <summary>
    /// Esta classe utlitaria é feita para obter cores de maneira mais legivel em outros pedaços de código.
    /// </summary>
    public static class ColorGTA
    {
        // Cores básicas SA-MP (hex sem o prefixo 0x)
        public const string White = "{FFFFFF}";
        public const string Black = "{000000}";
        public const string Red = "{FF0000}";
        public const string Green = "{00FF00}";
        public const string Blue = "{0000FF}";
        public const string Yellow = "{FFFF00}";
        public const string Cyan = "{00FFFF}";
        public const string Magenta = "{FF00FF}";
        public const string Gray = "{C0C0C0}";
        public const string LightBlue = "{33CCFF}";
        public const string Orange = "{FFA500}";
        public const string Purple = "{800080}";

        /// <summary>
        /// Gera uma cor no formato {RRGGBB} a partir de valores RGB.
        /// </summary>
        public static string FromRGB(byte r, byte g, byte b)
        {
            return $"{{{r:X2}{g:X2}{b:X2}}}";
        }

        /// <summary>
        /// Gera uma cor no formato {RRGGBB} a partir de um Color .NET.
        /// </summary>
        public static string FromColor(System.Drawing.Color color)
        {
            return $"{{{color.R:X2}{color.G:X2}{color.B:X2}}}";
        }
    }
}

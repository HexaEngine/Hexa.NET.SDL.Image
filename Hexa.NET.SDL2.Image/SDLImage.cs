﻿namespace Hexa.NET.SDL2.Image
{
    using System.Runtime.InteropServices;

    public partial class SDLImage
    {
        static SDLImage()
        {
            InitApi();
        }

        public static string GetLibraryName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "SDL2_image";
            }

            return "libSDL2_image";
        }
    }
}
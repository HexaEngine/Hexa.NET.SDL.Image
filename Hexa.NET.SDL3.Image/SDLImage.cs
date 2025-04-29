namespace Hexa.NET.SDL3.Image
{
    using System.Runtime.InteropServices;

    public static partial class SDLImage
    {
        static SDLImage()
        {
            InitApi();
        }

        public static string GetLibraryName()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return "SDL3_image";
            }

            return "libSDL3_image";
        }
    }
}
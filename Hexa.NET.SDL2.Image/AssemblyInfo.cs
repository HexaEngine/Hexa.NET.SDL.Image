using HexaGen.Runtime;

[assembly: NativeLibrary("SDL2_image", TargetPlatform.Windows)]
[assembly: NativeLibrary("libSDL2_image", TargetPlatform.Linux)]
[assembly: NativeLibrary("libSDL2_image", TargetPlatform.OSX)]
[assembly: NativeLibrary("libSDL2_image", TargetPlatform.Android)]
#if NET7_0_OR_GREATER
[assembly: System.Runtime.CompilerServices.DisableRuntimeMarshalling]
#endif
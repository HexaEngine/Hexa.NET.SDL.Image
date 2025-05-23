# Hexa.NET.SDL.Image

Hexa.NET.SDL.Image is a minimal C# wrapper for the SDL_image (2 + 3) library, providing a thin, 1:1 binding to SDL_image's C functions. This wrapper is designed for developers who need direct access to the SDL_image library's functionality from C#, without added complexity or overhead.

## Features

- **Minimal Wrapper**: Provides a direct, 1:1 mapping to the SDL_image C functions, staying true to the original API.
- **Multi-Format Support**: Allows loading of various image formats including PNG, JPEG, BMP, and more, leveraging SDL_image.
- **Cross-Platform Compatibility**: Works across all platforms supported by SDL, including Windows, Linux, Android, and macOS.
- **Pre-Built Native Libraries**: The package includes pre-built native libraries, so you don't need to worry about installing SDL or SDL_image separately.

## Requirements

- **Hexa.NET.SDL2**: This package has a dependency on the Hexa.NET.SDL2 package.
- **Hexa.NET.SDL3**: This package has a dependency on the Hexa.NET.SDL3 package.

## Installation

You can install Hexa.NET.SDL.Image via NuGet:

```bash
dotnet add package Hexa.NET.SDL2.Image
```

```bash
dotnet add package Hexa.NET.SDL3.Image
```

Alternatively, you can visit the NuGet package page [SDL2](https://www.nuget.org/packages/Hexa.NET.SDL2.Image), [SDL3](https://www.nuget.org/packages/Hexa.NET.SDL3.Image) and follow the instructions there.

## Usage

After installation, you can use Hexa.NET.SDL.Image to load and manipulate images in your SDL-based C# applications, with direct access to the underlying SDL_image functions.

## Contributing

Contributions are welcome! If you encounter any issues or have suggestions for improvements, feel free to open an issue or submit a pull request.

## License

Hexa.NET.SDL.Image is licensed under the MIT License. See the [LICENSE](https://github.com/HexaEngine/Hexa.NET.SDL.Image/blob/master/LICENSE.txt) file for more information.

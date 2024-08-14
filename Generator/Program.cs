// See https://aka.ms/new-console-template for more information
using HexaGen;

CsCodeGeneratorSettings settings = CsCodeGeneratorSettings.Load("generator.json");
settings.SystemIncludeFolders.Add("sdl");
CsCodeGenerator generator = new(settings);
generator.Generate(new List<string>() { "include/SDL_image.h" }, "../../../../Hexa.NET.SDL2.Image/Generated");
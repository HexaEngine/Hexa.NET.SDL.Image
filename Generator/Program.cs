// See https://aka.ms/new-console-template for more information
using HexaGen;
using HexaGen.Patching;
using System.Text.RegularExpressions;
using System.Text;

Regex regex = new("#include <(.*)>", RegexOptions.Multiline | RegexOptions.Compiled);
string[] files = [.. Directory.GetFiles("sdl3_image/include"), .. Directory.GetFiles("sdl3")];
foreach (var file in files)
{
    var content = File.ReadAllText(file);
    StringBuilder sb = new(content);
    var matches = regex.Matches(content);
    foreach (Match match in matches)
    {
        var value = match.Groups[1].Value;
        if (!value.StartsWith("SDL3/"))
        {
            continue;
        }
        value = value.Replace("SDL3/", "");
        sb.Replace(match.Value, $"#include \"{value}\"");
    }
    File.WriteAllText(file, sb.ToString());
}

BatchGenerator.Create()
    /*  .Setup<CsCodeGenerator>("sdl2_image/generator.json")
      .AddPrePatch(new NamingPatch(["IMG"], NamingPatchOptions.None))
      .Generate(["sdl2_image/include/SDL_image.h"], "../../../../Hexa.NET.SDL2.Image/Generated")*/
    .Setup<CsCodeGenerator>("sdl3_image/generator.json")
    .AddPrePatch(new NamingPatch(["IMG"], NamingPatchOptions.None))
    .Generate(["sdl3_image/include/SDL_image.h"], "../../../../Hexa.NET.SDL3.Image/Generated")
    .Finish();
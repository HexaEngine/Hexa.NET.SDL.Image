// See https://aka.ms/new-console-template for more information
using Example;
using Hexa.NET.SDL3;
using Hexa.NET.SDL3.Image;

unsafe
{
    if (!SDL.Init(SDLInitFlags.Video))
    {
        Console.WriteLine("SDL could not initialize! SDL_Error: " + SDL.GetErrorS());
        return;
    }

    var window = SDL.CreateWindow("SDL Image Example", 600, 600, 0);
    if (window == null)
    {
        Console.WriteLine("Window could not be created! SDL_Error: " + SDL.GetErrorS());
        SDL.Quit();
        return;
    }

    SDLRenderer* renderer = SDL.CreateRenderer(window, (byte*)null);
    if (renderer == null)
    {
        Console.WriteLine("Renderer could not be created! SDL_Error: " + SDL.GetErrorS());
        SDL.DestroyWindow(window);
        SDL.Quit();
        return;
    }

    using var fs = File.OpenRead("icon.png");
    using SDLStream stream = new(fs);

    // Load an image as an SDL surface
    SDLSurface* surface = SDLImage.LoadIO(stream, false);
    if (surface == null)
    {
        Console.WriteLine("Unable to load image! SDL_image Error: ");
        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        SDL.Quit();
        return;
    }

    // Create a texture from the surface
    SDLTexture* texture = SDL.CreateTextureFromSurface(renderer, surface);
    SDL.Free(surface);

    if (texture == null)
    {
        Console.WriteLine("Unable to create texture! SDL_Error: " + SDL.GetErrorS());
        SDL.DestroyRenderer(renderer);
        SDL.DestroyWindow(window);
        SDL.Quit();
        return;
    }

    // Clear the screen
    SDL.RenderClear(renderer);

    // Render the texture
    SDL.RenderTexture(renderer, texture, null, null);

    // Update the screen
    SDL.RenderPresent(renderer);

    // Wait for a few seconds
    SDL.Delay(5000);

    // Clean up
    SDL.DestroyTexture(texture);
    SDL.DestroyRenderer(renderer);
    SDL.DestroyWindow(window);
    SDL.Quit();
}
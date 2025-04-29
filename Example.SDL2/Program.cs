// See https://aka.ms/new-console-template for more information
using Hexa.NET.SDL2;
using Hexa.NET.SDL2.Image;

unsafe
{
    if (SDL.Init(SDL.SDL_INIT_VIDEO) < 0)
    {
        Console.WriteLine("SDL could not initialize! SDL_Error: " + SDL.GetErrorS());
        return;
    }

    SDLImage.Init((int)IMGInitFlags.Png);

    var window = SDL.CreateWindow("SDL Image Example", (int)SDL.SDL_WINDOWPOS_CENTERED_MASK, (int)SDL.SDL_WINDOWPOS_CENTERED_MASK, 600, 600, (uint)SDLWindowFlags.Shown);
    if (window == null)
    {
        Console.WriteLine("Window could not be created! SDL_Error: " + SDL.GetErrorS());
        SDL.Quit();
        return;
    }

    SDLRenderer* renderer = SDL.CreateRenderer(window, -1, (uint)SDLRendererFlags.Accelerated);
    if (renderer == null)
    {
        Console.WriteLine("Renderer could not be created! SDL_Error: " + SDL.GetErrorS());
        SDL.DestroyWindow(window);
        SDL.Quit();
        return;
    }

    // Load an image as an SDL surface
    SDLSurface* surface = SDLImage.Load("icon.png");
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
    SDL.FreeSurface(surface);

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
    SDL.RenderCopy(renderer, texture, null, null);

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
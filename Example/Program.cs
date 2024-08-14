// See https://aka.ms/new-console-template for more information
using Hexa.NET.SDL2;
using Hexa.NET.SDL2.Image;

unsafe
{
    if (SDL.SDLInit(SDL.SDL_INIT_VIDEO) < 0)
    {
        Console.WriteLine("SDL could not initialize! SDL_Error: " + SDL.SDLGetErrorS());
        return;
    }

    SDLImage.IMGInit((int)IMGInitFlags.Png);

    var window = SDL.SDLCreateWindow("SDL Image Example", (int)SDL.SDL_WINDOWPOS_CENTERED_MASK, (int)SDL.SDL_WINDOWPOS_CENTERED_MASK, 600, 600, (uint)SDLWindowFlags.Shown);
    if (window == null)
    {
        Console.WriteLine("Window could not be created! SDL_Error: " + SDL.SDLGetErrorS());
        SDL.SDLQuit();
        return;
    }

    SDLRenderer* renderer = SDL.SDLCreateRenderer(window, -1, (uint)SDLRendererFlags.Accelerated);
    if (renderer == null)
    {
        Console.WriteLine("Renderer could not be created! SDL_Error: " + SDL.SDLGetErrorS());
        SDL.SDLDestroyWindow(window);
        SDL.SDLQuit();
        return;
    }

    // Load an image as an SDL surface
    SDLSurface* surface = SDLImage.IMGLoad("icon.png");
    if (surface == null)
    {
        Console.WriteLine("Unable to load image! SDL_image Error: ");
        SDL.SDLDestroyRenderer(renderer);
        SDL.SDLDestroyWindow(window);
        SDL.SDLQuit();
        return;
    }

    // Create a texture from the surface
    SDLTexture* texture = SDL.SDLCreateTextureFromSurface(renderer, surface);
    SDL.SDLFreeSurface(surface);

    if (texture == null)
    {
        Console.WriteLine("Unable to create texture! SDL_Error: " + SDL.SDLGetErrorS());
        SDL.SDLDestroyRenderer(renderer);
        SDL.SDLDestroyWindow(window);
        SDL.SDLQuit();
        return;
    }

    // Clear the screen
    SDL.SDLRenderClear(renderer);

    // Render the texture
    SDL.SDLRenderCopy(renderer, texture, null, null);

    // Update the screen
    SDL.SDLRenderPresent(renderer);

    // Wait for a few seconds
    SDL.SDLDelay(5000);

    // Clean up
    SDL.SDLDestroyTexture(texture);
    SDL.SDLDestroyRenderer(renderer);
    SDL.SDLDestroyWindow(window);
    SDL.SDLQuit();
}
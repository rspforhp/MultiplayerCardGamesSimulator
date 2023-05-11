using System;
using System.Net;
using SDLTooSharp.Bindings.SDL2;
using SDLTooSharp.Bindings.SDL2Net;

namespace MultiplayerCardGamesSimulator;

public static class Extensions
{
    public static SDL.SDL_Rect Create(this SDL.SDL_Rect a,int x,int y,int w,int h)
    {
        a.H = h;
        a.Y = y;
        a.W = w;
        a.X = x;
        return a;
    }
}
public static class Program
{
   
    private static IntPtr _window;
    private static IntPtr _renderer;
    private const int ScreenWidth = 1280;
    private const int ScreenHeight = 768;
    public static event Action? DrawStuff;
    private static void Main()
    {
        if(SDL.SDL_Init( SDL.SDL_INIT_EVERYTHING ) < 0 )
        {
            Console.WriteLine( $"SDL could not initialize! SDL_Error: {SDL.SDL_GetError()}\n");
            return;
        }
        _window = SDL.SDL_CreateWindow( "SDL Tutorial", (int)SDL.SDL_WINDOWPOS_UNDEFINED, (int)SDL.SDL_WINDOWPOS_UNDEFINED, ScreenWidth, ScreenHeight, (uint)SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN );
        if( _window.CompareTo(null)==0 )
        {
            Console.WriteLine( $"Window could not be created! SDL_Error: {SDL.SDL_GetError()}\n");
            return;
        }

        _renderer = SDL.SDL_CreateRenderer( _window,0,0 );
        if( _renderer.CompareTo(null)==0 )
        {
            Console.WriteLine( $"Renderer could not be created! SDL_Error: {SDL.SDL_GetError()}\n");
            return;
        }
        DrawStuff+= delegate
        {
            SDL.SDL_SetRenderDrawColor(_renderer, 0xFF, 0xFF, 0xFF, 0xFF);
            SDL.SDL_RenderFillRect( _renderer, new SDL.SDL_Rect().Create(100,100,250,250) );

        };
        
        
        var ip = new SDLNet.IPaddress();
        SDLNet.SDLNet_Init();
        SDLNet.SDLNet_ResolveHost(ref ip, "192.168.0.129", 25565);
        Console.WriteLine(SDLNet.SDLNet_ResolveIP(in ip));
        
        
        SDL.SDL_Event e; bool quit = false;
        while (quit == false)
        {
            SDL.SDL_SetRenderDrawColor(_renderer, 0, 0, 0, 0);
            SDL.SDL_RenderClear(_renderer);
            while (SDL.SDL_PollEvent(out e) != 0)
            {
                if( e.Type ==(ulong)SDL.SDL_EventType.SDL_QUIT ) quit = true;
            }

            DrawStuff?.Invoke();
            SDL.SDL_RenderPresent(_renderer);

        }
        SDL.SDL_DestroyWindow(_window);
        SDL.SDL_DestroyRenderer(_renderer);
     
        SDL.SDL_Quit();
 

    }


}
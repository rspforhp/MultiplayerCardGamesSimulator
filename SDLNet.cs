using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;
using SDLTooSharp.Bindings.SDL2;
#pragma warning disable CS1591
namespace SDLTooSharp.Bindings.SDL2Net;

[ExcludeFromCodeCoverage]
public static partial class SDLNet
{
    private const string dllName = "SDL2_net";
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SDLNet_Linked_Version")]
    private static extern IntPtr _SDLNet_Linked_Version();
    public static SDL.SDL_version SDLNet_Linked_Version()
    {
        IntPtr version = _SDLNet_Linked_Version();
        return Marshal.PtrToStructure<SDL.SDL_version>(version);
    }
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDLNet_Init();
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDLNet_Quit();
    public struct IPaddress
    {
        public uint host;/* 32-bit IPv4 host address */
        public ushort port;/* 16-bit protocol port */
    }
    public enum INADDR : long
    {
        INADDR_ANY,
        INADDR_NONE=0xFFFFFFFF,
        INADDR_LOOPBACK=0x7f000001,
        INADDR_BROADCAST =   0xFFFFFFFF,
    }
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDLNet_ResolveHost(ref IPaddress address,[MarshalAs(UnmanagedType.BStr)] string host, ushort port );
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl,EntryPoint = "SDLNet_ResolveIP")]
    private static extern IntPtr _SDLNet_ResolveIP(in IPaddress address);
    public static string SDLNet_ResolveIP(in IPaddress address)=>Marshal.PtrToStringAnsi(_SDLNet_ResolveIP(address)) ?? string.Empty;
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDLNet_GetLocalAddresses(ref IPaddress address,int maxCount );
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDLNet_TCP_OpenServer(ref IPaddress address );
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDLNet_TCP_OpenClient(ref IPaddress address );
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDLNet_TCP_Open(ref IPaddress address );
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr SDLNet_TCP_Accept(IntPtr server);
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern ref IPaddress SDLNet_TCP_GetPeerAddress(IntPtr sock);
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDLNet_TCP_Send(IntPtr sock,in object data, int len);
    [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
    public static extern int SDLNet_TCP_Recv(IntPtr sock,ref  object data, int len);


    
    
    public static int SDLNet_SetError(string error) => SDL2.SDL.SDL_SetError(error);
    public static string SDLNet_GetError() => SDL2.SDL.SDL_GetError();
}
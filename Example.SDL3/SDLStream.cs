// See https://aka.ms/new-console-template for more information
using Hexa.NET.SDL3;
using HexaGen.Runtime;
using System.Runtime.InteropServices;

namespace Example
{
    public unsafe class SDLStream : IDisposable
    {
        private readonly Stream stream;
        private readonly bool closeStream;
        private SDLIOStreamInterface* iface;
        private SDLIOStream* ioStream;
        private bool disposedValue;

        NativeCallback<Read> readCallback;
        NativeCallback<Write> writeCallback;
        NativeCallback<Size> sizeCallback;
        NativeCallback<Seek> seekCallback;
        NativeCallback<Flush> flushCallback;
        NativeCallback<Close> closeCallback;

        public SDLStream(Stream stream, bool closeStream = true)
        {
            this.stream = stream;
            this.closeStream = closeStream;
            iface = (SDLIOStreamInterface*)NativeMemory.Alloc((nuint)sizeof(SDLIOStreamInterface));
            readCallback = new(ReadCallback);
            writeCallback = new(WriteCallback);
            sizeCallback = new(SizeCallback);
            seekCallback = new(SeekCallback);
            flushCallback = new(FlushCallback);
            closeCallback = new(CloseCallback);

            *iface = new SDLIOStreamInterface((uint)sizeof(SDLIOStreamInterface));
            iface->Read = (void*)Marshal.GetFunctionPointerForDelegate(readCallback.Callback!);
            iface->Write = (void*)Marshal.GetFunctionPointerForDelegate(writeCallback.Callback!);
            iface->Size = (void*)Marshal.GetFunctionPointerForDelegate(sizeCallback.Callback!);
            iface->Seek = (void*)Marshal.GetFunctionPointerForDelegate(seekCallback.Callback!);
            iface->Flush = (void*)Marshal.GetFunctionPointerForDelegate(flushCallback.Callback!);
            iface->Close = (void*)Marshal.GetFunctionPointerForDelegate(closeCallback.Callback!);

            ioStream = SDL.OpenIO(iface, null);
        }

        private byte CloseCallback(void* userdata)
        {
            stream.Close();
            return 1;
        }

        private byte FlushCallback(void* userdata, SDLIOStatus* status)
        {
            stream.Flush();
            return 1;
        }

        private long SeekCallback(void* userdata, long offset, SDLIOWhence whence)
        {
            SeekOrigin origin = whence switch
            {
                SDLIOWhence.SeekSet => SeekOrigin.Begin,
                SDLIOWhence.SeekCur => SeekOrigin.Current,
                SDLIOWhence.SeekEnd => SeekOrigin.End,
                _ => throw new NotSupportedException()
            };
            return stream.Seek(offset, origin);
        }

        private nuint WriteCallback(void* userdata, void* ptr, nuint size, SDLIOStatus* status)
        {
            stream.Write(new Span<byte>(ptr, (int)size));
            return size;
        }

        private nuint ReadCallback(void* userdata, void* ptr, nuint size, SDLIOStatus* status)
        {
            return (nuint)stream.Read(new Span<byte>(ptr, (int)size));
        }

        private long SizeCallback(void* userdata)
        {
            return stream.Length;
        }

        public static implicit operator SDLIOStream*(SDLStream stream)
        {
            return stream.ioStream;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (ioStream != null)
                {
                    SDL.CloseIO(ioStream);
                    ioStream = null;
                }

                if (iface != null)
                {
                    NativeMemory.Free(iface);
                    iface = null;
                }

                readCallback.Dispose();
                writeCallback.Dispose();
                sizeCallback.Dispose();
                seekCallback.Dispose();
                flushCallback.Dispose();
                closeCallback.Dispose();

                if (closeStream)
                {
                    stream.Close();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
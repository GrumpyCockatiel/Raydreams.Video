using System;
using System.IO;
using SkiaSharp;
using SharpAvi.Output;
using SharpAvi;

namespace Raydreams.Video
{
    /// <summary>Console Harness for a .NET 5 code that can write AVI files on a Mac or Linux natively without relying on installed encoders like FFMPEG or GDI+</summary>
    public class MovieMaker
    {
        public static readonly string DesktopPath = Environment.GetFolderPath( Environment.SpecialFolder.DesktopDirectory );

        private int _width = 640;
        private int _height = 480;

        static void Main( string[] args )
        {
            MovieMaker app = new MovieMaker();
            app.Run();

            Console.WriteLine( "Done Writing Movie" );
        }

        /// <summary>Bootstrap and Run the app</summary>
        public void Run()
        {
            var bytes = this.DrawFrame( new SKPoint( 320, 240 ) );

            RayBitmap bmp = new RayBitmap( _width, _height );
            bmp.WriteToFile( bytes, DesktopPath, "ray-test" );

            //this.WriteAVI();
        }

        /// <summary>Writes some BMPs to an AVI file</summary>
        /// <remarks>The written AVI file only works on Windows?</remarks>
        public void WriteAVI()
        {
            var writer = new AviWriter( DesktopPath + "/test.avi" )
            {
                FramesPerSecond = 8,
                // Emitting AVI v1 index in addition to OpenDML index (AVI v2)
                // improves compatibility with some software, including 
                // standard Windows programs like Media Player and File Explorer
                EmitIndex1 = true
            };

            // returns IAviVideoStream
            IAviVideoStream stream = writer.AddVideoStream();

            // set standard VGA resolution
            stream.Width = _width;
            stream.Height = _height;
            // class SharpAvi.KnownFourCCs.Codecs contains FOURCCs for several well-known codecs
            // Uncompressed is the default value, just set it for clarity
            stream.Codec = KnownFourCCs.Codecs.Uncompressed;
            // Uncompressed format requires to also specify bits per pixel
            stream.BitsPerPixel = BitsPerPixel.Bpp32;

            // write a bunch of frames but only 1000 for now
            for ( int i = 0; i < _width && i < 1000; ++i )
            {
                var frameData = DrawFrame( new SKPoint( i, _height / 2 ) );

                // write data to a frame
                stream.WriteFrame( true, // is key frame? (many codecs use concept of key frames, for others - all frames are keys)
                    frameData, // array with frame data
                    0, // starting index in the array
                    frameData.Length // length of the data
                );
            }

            writer.Close();
        }

        /// <summary>Draw a single frame for now which is a shape on a white background</summary>
        /// <param name="loc">Postisition to draw the shape</param>
        /// <returns>Raw pixel bytes in the RGBA sequence</returns>
        public byte[] DrawFrame(SKPoint loc)
        {
            SKPaint paint = new SKPaint()
            {
                IsAntialias = true,
                Color = SKColors.Red,
                Style = SKPaintStyle.Fill,
                // when true the path is ONLY stroked
                IsStroke = false,
                StrokeWidth = 0
            };

            SKImageInfo info = new SKImageInfo( _width, _height, SKColorType.Rgba8888 );
            SKSurface surface = SKSurface.Create( info );

            surface.Canvas.Clear( SKColors.White );
            surface.Canvas.DrawPath( Shapes.Star( 100, loc, 0 ), paint );

            // get RGBA Pixels
            return SKBitmap.FromImage( surface.Snapshot() ).Bytes;
        }

        /// <summary>Test just writing bytes to a file</summary>
        public void WriteBytes()
        {
            using MemoryStream mem = new MemoryStream();

            short a = 1;
            int b = 2;

            mem.WriteBigEndian( a );
            mem.WriteBigEndian( b );
            File.WriteAllBytes( DesktopPath + "/test.bin", mem.ToArray() );
        }

    }

}

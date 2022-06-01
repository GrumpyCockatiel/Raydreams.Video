using System;
using System.IO;
using SkiaSharp;
using SharpAvi.Output;
using SharpAvi;
using Raydreams.Video.Model;
using Raydreams.Video.Tests;

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
            app.CurrentScene = Mocks.Scene1();
            app.Run();

            Console.WriteLine( "Done Writing Movie" );
        }

        /// <summary></summary>
        public Scene CurrentScene { get; set; }

        /// <summary>Bootstrap and Run the app</summary>
        public void Run()
        {
            // to test writing a single Windows BMP file
            //var bytes = this.DrawFrame( new SKPoint( 320, 240 ) );
            //RayBitmap bmp = new RayBitmap( _width, _height ) { Order = Endianness.Big };
            //bmp.WriteToFile( bytes, DesktopPath, "ray-test" );

            // write an AVI movie
            this.WriteAVI();
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
            for ( int i = 0; i < _width && i < 250; ++i )
            {
                byte[] raw = DrawFrame( new SKPoint( i, _height / 2 ) );

                RayBitmap bmp = new RayBitmap( _width, _height ) { Order = Endianness.Big };
                byte[] frameData = bmp.Encode( raw );

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
        public byte[] DrawFrame(Frame frame, SKColor background )
        {
            using SKPaint fill = new SKPaint()
            {
                IsAntialias = true,
                Style = SKPaintStyle.Fill,
                // when true the path is ONLY stroked
                IsStroke = false,
                StrokeWidth = 0
            };

            using SKPaint stroke = new SKPaint()
            {
                IsAntialias = true,
                Style = SKPaintStyle.Stroke,
                // when true the path is ONLY stroked
                IsStroke = true,
                StrokeWidth = 1
            };

            SKImageInfo info = new SKImageInfo( _width, _height, SKColorType.Rgba8888 );
            SKSurface surface = SKSurface.Create( info );

            // start a new surface
            surface.Canvas.Clear( background );

            foreach (AssetRender ass in frame.Entites )
            {
                // get the original asset
                Asset original = this.CurrentScene.GetAsset( ass.ID );

                // foreach path in the asset
                foreach ( SKPath path in original.Paths )
                {
                    fill.Color = original.FillColor;
                    path.Transform( SKMatrix.CreateRotationDegrees( ass.Rotation ) );
                    surface.Canvas.DrawPath( path, fill );
                }
            }

            // get RGBA Pixels
            return SKBitmap.FromImage( surface.Snapshot() ).Bytes;
        }

    }

}

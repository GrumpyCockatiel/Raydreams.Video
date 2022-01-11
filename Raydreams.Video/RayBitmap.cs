using System;
using System.IO;

namespace Raydreams.Video
{
	/// <summary>Writes a Windows Bitmap in .NET 5 on Mac/Linux platforms</summary>
    /// <remarks>For now ONLY supports 4 channel RGBA 8888 - 32 bit images with no compression</remarks>
    public class RayBitmap
    {
        #region [ Fields ]

        /// <summary>ColorMap is not used in 24 bit RGBA</summary>
        public static readonly int ColorMapSize = 0;

		/// <summary>For now the header size is fixed at 14 + 40</summary>
		public static readonly int HeaderSize = 54;

		/// <summary>4 channels</summary>
		public static readonly int BytesPerPixel = 4;

		#endregion [ Fields ]

		/// <summary>Init with an image width and height</summary>
		/// <param name="width"></param>
		/// <param name="height"></param>
		public RayBitmap( int width, int height )
        {
			this.Cols = width;
			this.Rows = height;
			this.Filesize = HeaderSize + ColorMapSize + ( RowBytes * this.Rows );
			this.PixelOffset = HeaderSize + ColorMapSize;
			this.CompressionSize = ( RowBytes * this.Rows );
		}

		#region [ Properties ]

		/* Info Header is 14 Bytes */

		/// <summary>first magic byte is 'B'</summary>
		public char Magic1 { get; set; } = 'B';

		/// <summary>second magic byte is 'M'</summary>
		public char Magic2 { get; set; } = 'M';

		/// <summary>size of entire image file - header, color map and data</summary>
		public int Filesize { get; set; }

		/// <summary>unused</summary>
		public short Reserved1 { get; set; } = 0;

		/// <summary>unused</summary>
		public short Reserved2 { get; set; } = 0;

		/// <summary>number of bytes to start of image data from file beginning</summary>
		public int PixelOffset { get; set; }

		/* DIB Starts Here BITMAP INFO HEADER 40 bytes */

		/// <summary>Size of the DIB block header - magic number 40</summary>
		public int BMISize { get; set; } = 40;

		/// <summary>number of columns in image</summary>
		public int Cols { get; set; }

		/// <summary>number of rows in image</summary>
		public int Rows { get; set; }

		/// <summary>number of planes in image</summary>
		public short Planes { get; set; } = 1;

		/// <summary>bits per pixel - 1, 4, 8, 24, 32</summary>
		public short BitsPerPixel { get; set; } = 32;

		/// <summary>compression used</summary>
		public int Compression { get; set; } = 0;

		/// <summary>size of compressed image which is its full pixel byte length if uncompressed</summary>
		public int CompressionSize { get; set; }

		/// <summary>pixels per meter</summary>
        public int XScale { get; set; } = 0;

		/// <summary>pixels per meter</summary>
		public int YScale { get; set; } = 0;

		/// <summary>Number of colors in the color palette</summary>
		public int Colors { get; set; } = 0;

		/// <summary>Important colors</summary>
		public int ImpColors { get; set; } = 0;

		/// <summary>Bytes per row</summary>
		public int RowBytes => BytesPerPixel * this.Cols;

		#endregion [ Properties ]

		#region [ Methods ]

		/// <summary>Encode an image and write it to a file in one step</summary>
		/// <param name="rgba">The raw image bytes in RGBA format</param>
		/// <param name="dir">Full path to folder to write into</param>
		/// <param name="filename">Filename without the extension</param>
		public void WriteToFile( byte[] rgba, string dir, string filename )
        {
			if ( String.IsNullOrWhiteSpace( dir ) || String.IsNullOrWhiteSpace( filename ) )
				return;

			byte[] bmp = Encode( rgba );
			File.WriteAllBytes( Path.Combine(dir, $"{filename}.bmp" ), bmp );
		}

		/// <summary>Take raw image pixels in RGBA format and encode into simple Windows Bitmap</summary>
		/// <param name="rgba">The raw image bytes in RGBA format</param>
		/// <returns>Bitmap byte array</returns>
		public byte[] Encode(byte[] rgba)
		{
			using MemoryStream mem = new MemoryStream();
            mem.Write( Magic1 );
            mem.Write( Magic2 );
            mem.WriteLittleEndian( Filesize );
            mem.WriteLittleEndian( Reserved1 );
            mem.WriteLittleEndian( Reserved2 );
            mem.WriteLittleEndian( PixelOffset );
            mem.WriteLittleEndian( BMISize );
            mem.WriteLittleEndian( Cols );
            mem.WriteLittleEndian( Rows );
            mem.WriteLittleEndian( Planes );
            mem.WriteLittleEndian( BitsPerPixel );
            mem.WriteLittleEndian( Compression );
            mem.WriteLittleEndian( CompressionSize );
            mem.WriteLittleEndian( XScale );
            mem.WriteLittleEndian( YScale );
            mem.WriteLittleEndian( Colors );
            mem.WriteLittleEndian( ImpColors );

			// if we were supporting color map
			if ( ColorMapSize > 0 )
				throw new System.NotImplementedException("Color Maps are not currently supported");

			// move to the last bytes in the image stream
			int p = RowBytes * this.Rows;

			// loop through the image data
			for ( int row = 0; row < this.Rows; ++row )
			{
				// backup one row
				p -= RowBytes;

				// write the pixel BGRA
				for ( int col = 0; col < this.Cols; ++col )
				{
					mem.WriteByte( rgba[p + 2] );
					mem.WriteByte( rgba[p + 1] );
					mem.WriteByte( rgba[p] );
					mem.WriteByte( rgba[p + 3] );

					// advanced Bytes per pixel
					p += BytesPerPixel;
				}

				// backup to the start of the row again
				p -= RowBytes;
			}

			mem.Position = 0;
			return mem.ToArray();
		}

		#endregion [ Methods ]
	}
}

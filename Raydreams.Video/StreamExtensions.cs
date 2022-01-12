using System;
using System.IO;
using System.Text;

namespace Raydreams.Video
{
	/// <summary>Platform specific order to write bytes</summary>
	public enum Endianness
	{
		/// <summary>LSBss are written first</summary>
		Little = 0,
		/// <summary>MSBs are written first</summary>
		Big = 1
	}

	/// <summary>Extension to help write specific data types to a Memory stream</summary>
	/// <remarks>These truncate anything beyond the first 8 LSBs</remarks>
	public static class StreamExtensions
	{
		/// <summary>Write a single char to the stream</summary>
        /// <param name="stream"></param>
        /// <param name="value"></param>
		public static void Write( this Stream stream, char value )
        {
			stream.WriteByte( (byte)value );
		}

		/// <summary>Writes the characters from an ASCII string into the specified stream</summary>
		/// <param name="stream">The stream to write the data to.</param>
		/// <param name="value">The value to write</param>
		public static void Write( this Stream stream, string value )
		{
			foreach ( char c in value )
			{
				stream.WriteByte( (byte)c );
			}
		}

		/// <summary>Writes a floating point number in big-endian format.</summary>
		/// <param name="stream">The stream to write the data to.</param>
		/// <param name="value">The value to write</param>
		public static void WriteBigEndian( this Stream stream, float value )
		{
			byte[] bytes = BitConverter.GetBytes( value );

			stream.WriteByte( bytes[3] );
			stream.WriteByte( bytes[2] );
			stream.WriteByte( bytes[1] );
			stream.WriteByte( bytes[0] );
		}

		/// <summary>Writes a unicode string to the specified stream in big-endian format</summary>
		/// <param name="stream">The stream.</param>
		/// <param name="value">The value.</param>
		public static void WriteBigEndian( this Stream stream, string value )
		{
			byte[] data;

			data = Encoding.BigEndianUnicode.GetBytes( value );

			stream.WriteBigEndian( (ushort)value.Length );
			stream.Write( data, 0, data.Length );
		}

		/// <summary>Writes a 16bit unsigned integer in big-endian format.</summary>
		/// <param name="stream">The stream to write the data to.</param>
		/// <param name="value">The value to write</param>
		public static void WriteBigEndian( this Stream stream, ushort value )
		{
			// write MSBs first
			stream.WriteByte( (byte)( value >> 8 ) );
			stream.WriteByte( (byte)( value >> 0 ) );
		}

		/// <summary>Writes a 16bit signed integer in big-endian format.</summary>
		/// <param name="stream">The stream to write the data to.</param>
		/// <param name="value">The value to write</param>
		public static void WriteBigEndian( this Stream stream, short value )
		{
			// write MSBs first
			stream.WriteByte( (byte)( value >> 8 ) );
			stream.WriteByte( (byte)value );
		}

		/// <summary>Writes a 32bit signed integer in big-endian format.</summary>
		/// <param name="stream">The stream to write the data to.</param>
		/// <param name="value">The value to write</param>
		public static void WriteBigEndian( this Stream stream, int value )
		{
			// keep the left most byte and shift to the right most register
			stream.WriteByte( (byte)( ( value & 0xFF000000 ) >> 24 ) );

			stream.WriteByte( (byte)( ( value & 0x00FF0000 ) >> 16 ) );
			stream.WriteByte( (byte)( ( value & 0x0000FF00 ) >> 8 ) );
			stream.WriteByte( (byte)( ( value & 0x000000FF ) >> 0 ) );
		}

		/// <summary>Writes a 16bit signed integer in little-endian format.</summary>
		/// <param name="stream">The stream to write the data to.</param>
		/// <param name="value">The value to write</param>
		public static void WriteLittleEndian( this Stream stream, short value )
		{
			// write the LSBs first
			stream.WriteByte( (byte)( value & 0xFF ) );
			stream.WriteByte( (byte)( (value >> 8 ) & 0xFF  ) );
		}

		/// <summary>Writes a 32bit signed integer in little-endian format.</summary>
		/// <param name="stream">The stream to write the data to.</param>
		/// <param name="value">The value to write</param>
		public static void WriteLittleEndian( this Stream stream, int value )
		{
			stream.WriteByte( (byte)( value & 0x000000FF ) );
			stream.WriteByte( (byte)( ( value >> 8 ) & 0x000000FF ) );
			stream.WriteByte( (byte)( ( value >> 16 ) & 0x000000FF ) );
			stream.WriteByte( (byte)( ( value >> 24 ) & 0x000000FF ) );
		}
	}
}

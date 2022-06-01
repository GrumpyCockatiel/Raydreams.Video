using System;
using System.IO;

namespace Raydreams.Video.Tests
{
    /// <summary>Move to Unit Tests</summary>
    public class Tester
    {
        public Tester()
        {
        }

        /// <summary>Test just writing bytes to a file</summary>
        public void WriteBytes()
        {
            using MemoryStream mem = new MemoryStream();

            short a = 1;
            int b = 2;

            mem.WriteBigEndian( a );
            mem.WriteBigEndian( b );
            File.WriteAllBytes( MovieMaker.DesktopPath + "/test.bin", mem.ToArray() );
        }
    }
}

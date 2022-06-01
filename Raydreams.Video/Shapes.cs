using System;
using SkiaSharp;

namespace Raydreams.Video
{
	/// <summary>Pre generates the path for test shapes</summary>
    public static class Shapes
    {
        /// <summary>Creates a square inscribed inside the spefified circle of radius</summary>
        public static SKPath Circle( float radius, SKPoint offset, float rotation = 0 )
        {
            SKPath path = new SKPath() { FillType = SKPathFillType.EvenOdd };
            path.AddCircle( 0, 0, radius, SKPathDirection.Clockwise );
            path.Close();

            path.Transform( SKMatrix.CreateTranslation( offset.X, offset.Y ) );
            return path;
        }

        /// <summary>Generate points for a star centered around an origin</summary>
        public static SKPath Star( float radius, SKPoint offset, float rotation )
		{
			return Star( radius, radius / 2.5F, offset, rotation );
		}

		/// <summary>Generate points for a star with inner and outer radius</summary>
		public static SKPath Star( float outerRadius, float innerRadius, SKPoint offset, float rotation )
		{
			SKPath path = new SKPath() { FillType = SKPathFillType.EvenOdd };
			path.MoveTo( outerRadius * Angles.COS270, outerRadius * Angles.SIN270 );
			path.LineTo( innerRadius * Angles.COS306, innerRadius * Angles.SIN306 );
			path.LineTo( outerRadius * Angles.COS342, outerRadius * Angles.SIN342 );
			path.LineTo( innerRadius * Angles.COS18, innerRadius * Angles.SIN18 );
			path.LineTo( outerRadius * Angles.COS54, outerRadius * Angles.SIN54 );
			path.LineTo( innerRadius * Angles.COS90, innerRadius * Angles.SIN90 );
			path.LineTo( outerRadius * Angles.COS126, outerRadius * Angles.SIN126 );
			path.LineTo( innerRadius * Angles.COS162, innerRadius * Angles.SIN162 );
			path.LineTo( outerRadius * Angles.COS198, outerRadius * Angles.SIN198 );
			path.LineTo( innerRadius * Angles.COS234, innerRadius * Angles.SIN234 );
			path.Close();

			if ( rotation != 0 )
				path.Transform( SKMatrix.CreateRotationDegrees( rotation ) );

			path.Transform( SKMatrix.CreateTranslation( offset.X, offset.Y ) );
			return path;
		}
	}

    /// <summary>Precalcs common angels</summary>
    public static class Angles
    {
        /// <summary>Precalc Cosine angles as radians floats</summary>
        public static readonly float COS0 = 1.0F;
        public static readonly float COS18 = Convert.ToSingle( Math.Cos( 18.0 * Math.PI / 180.0 ) );
        public static readonly float COS30 = Convert.ToSingle( Math.Cos( 30.0 * Math.PI / 180.0 ) );
        public static readonly float COS45 = Convert.ToSingle( Math.Cos( 45.0 * Math.PI / 180.0 ) );
        public static readonly float COS54 = Convert.ToSingle( Math.Cos( 54.0 * Math.PI / 180.0 ) );
        public static readonly float COS60 = Convert.ToSingle( Math.Cos( 60.0 * Math.PI / 180.0 ) ); // 0.5
        public static readonly float COS75 = Convert.ToSingle( Math.Cos( 75.0 * Math.PI / 180.0 ) );
        public static readonly float COS80 = Convert.ToSingle( Math.Cos( 80.0 * Math.PI / 180.0 ) );
        public static readonly float COS90 = 0;
        public static readonly float COS100 = Convert.ToSingle( Math.Cos( 100.0 * Math.PI / 180.0 ) );
        public static readonly float COS105 = Convert.ToSingle( Math.Cos( 105.0 * Math.PI / 180.0 ) );
        public static readonly float COS120 = Convert.ToSingle( Math.Cos( 120.0 * Math.PI / 180.0 ) );
        public static readonly float COS126 = Convert.ToSingle( Math.Cos( 126.0 * Math.PI / 180.0 ) );
        public static readonly float COS135 = Convert.ToSingle( Math.Cos( 135.0 * Math.PI / 180.0 ) );
        public static readonly float COS150 = Convert.ToSingle( Math.Cos( 150.0 * Math.PI / 180.0 ) );
        public static readonly float COS162 = Convert.ToSingle( Math.Cos( 162.0 * Math.PI / 180.0 ) );
        public static readonly float COS180 = -1.0F;
        public static readonly float COS198 = Convert.ToSingle( Math.Cos( 198.0 * Math.PI / 180.0 ) );
        public static readonly float COS225 = Convert.ToSingle( Math.Cos( 225.0 * Math.PI / 180.0 ) );
        public static readonly float COS234 = Convert.ToSingle( Math.Cos( 234.0 * Math.PI / 180.0 ) );
        public static readonly float COS240 = Convert.ToSingle( Math.Cos( 240.0 * Math.PI / 180.0 ) );
        public static readonly float COS255 = Convert.ToSingle( Math.Cos( 255.0 * Math.PI / 180.0 ) );
        public static readonly float COS260 = Convert.ToSingle( Math.Cos( 260.0 * Math.PI / 180.0 ) );
        public static readonly float COS270 = 0;
        public static readonly float COS280 = Convert.ToSingle( Math.Cos( 280.0 * Math.PI / 180.0 ) );
        public static readonly float COS285 = Convert.ToSingle( Math.Cos( 285.0 * Math.PI / 180.0 ) );
        public static readonly float COS300 = Convert.ToSingle( Math.Cos( 300.0 * Math.PI / 180.0 ) );
        public static readonly float COS306 = Convert.ToSingle( Math.Cos( 306.0 * Math.PI / 180.0 ) );
        public static readonly float COS315 = Convert.ToSingle( Math.Cos( 315.0 * Math.PI / 180.0 ) );
        public static readonly float COS342 = Convert.ToSingle( Math.Cos( 342.0 * Math.PI / 180.0 ) );
        public static readonly float COS360 = 1.0F;

        /// <summary>Precalc sin angles as radians</summary>
        public static readonly float SIN0 = 0;
        public static readonly float SIN18 = Convert.ToSingle( Math.Sin( 18.0 * Math.PI / 180.0 ) );
        public static readonly float SIN30 = Convert.ToSingle( Math.Sin( 30.0 * Math.PI / 180.0 ) );
        public static readonly float SIN45 = Convert.ToSingle( Math.Sin( 45.0 * Math.PI / 180.0 ) );
        public static readonly float SIN54 = Convert.ToSingle( Math.Sin( 54.0 * Math.PI / 180.0 ) );
        public static readonly float SIN60 = Convert.ToSingle( Math.Sin( 60.0 * Math.PI / 180.0 ) ); // .8660
        public static readonly float SIN75 = Convert.ToSingle( Math.Sin( 75.0 * Math.PI / 180.0 ) );
        public static readonly float SIN80 = Convert.ToSingle( Math.Sin( 80.0 * Math.PI / 180.0 ) );
        public static readonly float SIN90 = 1.0F;
        public static readonly float SIN100 = Convert.ToSingle( Math.Sin( 100.0 * Math.PI / 180.0 ) );
        public static readonly float SIN105 = Convert.ToSingle( Math.Sin( 105.0 * Math.PI / 180.0 ) );
        public static readonly float SIN120 = Convert.ToSingle( Math.Sin( 120.0 * Math.PI / 180.0 ) );
        public static readonly float SIN126 = Convert.ToSingle( Math.Sin( 126.0 * Math.PI / 180.0 ) );
        public static readonly float SIN135 = Convert.ToSingle( Math.Sin( 135.0 * Math.PI / 180.0 ) );
        public static readonly float SIN150 = Convert.ToSingle( Math.Sin( 150.0 * Math.PI / 180.0 ) );
        public static readonly float SIN162 = Convert.ToSingle( Math.Sin( 162.0 * Math.PI / 180.0 ) );
        public static readonly float SIN180 = 0;
        public static readonly float SIN198 = Convert.ToSingle( Math.Sin( 198.0 * Math.PI / 180.0 ) );
        public static readonly float SIN225 = Convert.ToSingle( Math.Sin( 225.0 * Math.PI / 180.0 ) );
        public static readonly float SIN234 = Convert.ToSingle( Math.Sin( 234.0 * Math.PI / 180.0 ) );
        public static readonly float SIN240 = Convert.ToSingle( Math.Sin( 240.0 * Math.PI / 180.0 ) );
        public static readonly float SIN255 = Convert.ToSingle( Math.Sin( 255.0 * Math.PI / 180.0 ) );
        public static readonly float SIN260 = Convert.ToSingle( Math.Sin( 260.0 * Math.PI / 180.0 ) );
        public static readonly float SIN270 = -1.0F;
        public static readonly float SIN280 = Convert.ToSingle( Math.Sin( 280.0 * Math.PI / 180.0 ) );
        public static readonly float SIN285 = Convert.ToSingle( Math.Sin( 285.0 * Math.PI / 180.0 ) );
        public static readonly float SIN300 = Convert.ToSingle( Math.Sin( 300.0 * Math.PI / 180.0 ) );
        public static readonly float SIN306 = Convert.ToSingle( Math.Sin( 306.0 * Math.PI / 180.0 ) );
        public static readonly float SIN315 = Convert.ToSingle( Math.Sin( 315.0 * Math.PI / 180.0 ) );
        public static readonly float SIN342 = Convert.ToSingle( Math.Sin( 342.0 * Math.PI / 180.0 ) );
        public static readonly float SIN360 = 0;

        /// <summary>Returns an angle in Radians from Degrees</summary>
        /// <param name="angle"></param>
        /// <returns></returns>
        public static float Deg2Rad( float deg ) => deg * Convert.ToSingle( Math.PI / 180.0F );

        /// <summary>Returns an angle in degrees from radians</summary>
        public static float Rad2Deg( float rad ) => rad * Convert.ToSingle( 180.0F / Math.PI );
    }
}

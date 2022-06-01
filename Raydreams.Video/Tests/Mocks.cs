using System;
using System.Collections.Generic;
using Raydreams.Video.Model;
using SkiaSharp;

namespace Raydreams.Video.Tests
{
    public static class Mocks
    {
        public static Scene Scene1()
        {
            string line1 = "<line x1=\"-50\" y1=\"-50\" x2=\"50\" y2=\"50\" />";

            string ellipse1 = "<ellipse cx=\"0\" cy=\"0\" rx=\"70\" ry=\"100\" />";

            Scene scene1 = new Scene();

            Asset asset1 = new Asset() { ID = 1, StrokeColor = SKColors.Black };
            asset1.Paths.Add( SKPath.ParseSvgPathData( line1 ) );
            scene1.Assets.Add( asset1 );

            Asset asset2 = new Asset() { ID = 2, FillColor = SKColors.Red, StrokeColor = SKColors.Black };
            asset1.Paths.Add( SKPath.ParseSvgPathData( ellipse1 ) );
            scene1.Assets.Add( asset2 );

            Frame frame1 = new Frame();
            frame1.Entites.Add( new AssetRender { ID = 1, Position = new SKPoint() } );
            scene1.Frames.Add( frame1 );

            return scene1;
        }
    }
}

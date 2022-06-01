using System;
using System.Collections.Generic;
using System.Linq;
using SkiaSharp;

namespace Raydreams.Video.Model
{
    /// <summary>A scene is a collection of similar frames</summary>
    public class Scene
    {
        public Scene()
        {
        }

        public SKColor StageColor { get; set; } = SKColors.White;

        public List<Asset> Assets { get; set; } = new List<Asset>();

        public List<Frame> Frames { get; set; } = new List<Frame>();

        public Asset GetAsset( int id ) => this.Assets.Where( i => i.ID == id ).FirstOrDefault();
    }

    /// <summary>An asset is a single item in the scene composed of a collection of paths</summary>
    public class Asset
    {
        public Asset()
        {
        }

        public int ID { get; set; } = 0;

        public List<SKPath> Paths { get; set; } = new List<SKPath>();

        /// <summary>Default stroke color</summary>
        public SKColor StrokeColor { get; set; } = SKColors.Black;

        /// <summary>Default fill color</summary>
        public SKColor FillColor { get; set; } = SKColors.Red;
    }

    /// <summary>A single frame in the scene</summary>
    public class Frame
    {
        public List<AssetRender> Entites { get; set; } = new List<AssetRender>();
    }

    /// <summary>An instance of an asset in a frame</summary>
    public class AssetRender
    {
        public int ID { get; set; } = 0;

        public SKPoint Position { get; set; }

        public float Rotation { get; set; } = 0;
    }
}

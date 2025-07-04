using SkiaSharp;

namespace SwordEngine.Tiles
{
    public abstract class Tile
    {
        #region Static Variables

        // Master tiles
        public static Tile[] mTiles = new Tile[256];

        // World1 tiles
        public static Tile grassTile = new GrassTile(0);
        public static Tile brickTile = new BrickTile(1);
        public static Tile lavaTile = new LavaTile(2);
        public static Tile treeTile = new TreeTile(3);
        public static Tile caveEnt = new CaveEnterTile(4);
        public static Tile brickgrassTile = new BrickGrassTile(5);
        public static Tile rockTile = new RockTile(6);
        public static Tile waterTile = new WaterTile(7);
        public static Tile sandTile = new SandTile(8);

        #endregion

        #region World2 Tiles

        // World2 tiles
        public static Tile caveFloor = new CaveFloorTile(9);
        public static Tile caveWall = new CaveWallTile(10);
        public static Tile caveRock = new CaveRockTile(11);
        public static Tile caveMush = new CaveMushTile(12);
        public static Tile caveSlime = new CaveSlimeTile(13);
        public static Tile caveExit = new CaveExitTile(14);

        #endregion

        /// <summary>
        /// The image of the texture.
        /// </summary>
        protected SKBitmap texture;

        /// <summary>
        /// The identifier for a tile.
        /// </summary>
        protected int id; // this shouldn't be changed once it's set

        /// <summary>
        /// The games tile width and heights.
        /// </summary>
        public static int TILEWIDTH = 40,
                          TILEHEIGHT = 40;

        /// <summary>
        /// Instantiates a new game tile.
        /// </summary>
        /// <param name="texture">The texture to load in the tile.</param>
        /// <param name="id">The identifier of the tile.</param>
        public Tile(SKBitmap texture, int id)
        {
            this.texture = texture;
            this.id = id;

            mTiles[id] = this;
        }

        /// <summary>
        /// Updates the tile.
        /// </summary>
        public void Update()
        {
            // Empty
        }

        /// <summary>
        /// Renders the tile on a canvas.
        /// </summary>
        /// <param name="canvas">The canvas.</param>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Render(SKCanvas canvas, int x, int y)
        {
            SKPoint sKPoint = new SKPoint(x, y);
            SKRect sKRect = SKRect.Create(TILEWIDTH, TILEHEIGHT);
            canvas.DrawBitmap(texture, sKPoint, new SKPaint());
        }

        /// <summary>
        /// Determines if the tile is solid.
        /// </summary>
        public virtual bool IsSolid()
        {
            return false;
        }

        /// <summary>
        /// Determines if the tile is in the camera view.
        /// </summary>
        public bool IsCamera()
        {
            return false;
        }

        /// <summary>
        /// Gets the idenitifier of the tile.
        /// </summary>
        public int GetId()
        {
            return id;
        }
    }
}

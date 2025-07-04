using SkiaSharp;
using SwordEngine.Entities;
using SwordEngine.Entities.Creatures;
using SwordEngine.Entities.Statics;
using SwordEngine.Tiles;
using SwordEngine.Weapons;

namespace SwordEngine.World
{

    /// <summary>
    /// The world class is in charge of storing the tile layout of the world
    /// adds entities to the world(and they're spawn locations, including the player),
    /// rendering the world, and contains some methods for extracting information about the world.
    /// </summary>
    public class World
    {
        /// <summary>
        /// The main game handler.
        /// </summary>
        private Handler handler;

        /// <summary>
        /// The width/height of the world in Tiles.
        /// </summary>
        private int width, height;
        
        /// <summary>
        /// The spawn location of the player entity in Tixels determined by the world file.
        /// </summary>
        private int spawnX, spawnY;
        

        /// <summary>
        /// A multidimensional array containing the layout of the entire world, 
        /// determined by the world file
        /// </summary>
        private int[][] wTiles;

        #region Entities

        /// <summary>
        /// Contains all of the world's entities.
        /// </summary>
        private EntityManager entityManager;

        #endregion

        /// <summary>
        /// Constructs a world object, sets variable defaults, 
        /// adds new entities as well as the player, 
        /// loads the world from the the given world file path, and sets the player's spawn point.
        /// </summary>
        /// <param name="handler">The main game handler.</param>
        /// <param name="path">The file path.</param>
        public World(Handler handler, String path)
        {
            this.handler = handler;
            //All enemies
            entityManager = new EntityManager(handler, new Player(handler, 100, 100));
            entityManager.addEntity(new Slime(handler, (Tile.TILEWIDTH * 15), (Tile.TILEHEIGHT * 9)));
            entityManager.addEntity(new Slime(handler, (Tile.TILEWIDTH * 10), (Tile.TILEHEIGHT * 7)));
            entityManager.addEntity(new Slime(handler, (Tile.TILEWIDTH * 7), (Tile.TILEHEIGHT * 39)));
            entityManager.addEntity(new Slime(handler, (Tile.TILEWIDTH * 5), (Tile.TILEHEIGHT * 33)));
            entityManager.addEntity(new Mouse(handler, (Tile.TILEWIDTH * 3), (Tile.TILEHEIGHT * 9)));
            entityManager.addEntity(new Mouse(handler, (Tile.TILEWIDTH * 10), (Tile.TILEHEIGHT * 26)));
            entityManager.addEntity(new Snake(handler, (Tile.TILEWIDTH * 12), (Tile.TILEHEIGHT * 20)));
            entityManager.addEntity(new Snake(handler, (Tile.TILEWIDTH * 9), (Tile.TILEHEIGHT * 20)));
            entityManager.addEntity(new Snake(handler, (Tile.TILEWIDTH * 7), (Tile.TILEHEIGHT * 20)));
            entityManager.addEntity(new Skeleton(handler, (Tile.TILEWIDTH * 13), (Tile.TILEHEIGHT * 44)));
            entityManager.addEntity(new Skeleton(handler, (Tile.TILEWIDTH * 13), (Tile.TILEHEIGHT * 35)));
            entityManager.addEntity(new Skeleton(handler, (Tile.TILEWIDTH * 35), (Tile.TILEHEIGHT * 46)));
            //All chests
            entityManager.addEntity(new Chest(handler, (Tile.TILEWIDTH * 3), (Tile.TILEHEIGHT * 6)));
            entityManager.addEntity(new Chest(handler, (Tile.TILEWIDTH * 14), (Tile.TILEHEIGHT * 20)));
            entityManager.addEntity(new Chest(handler, (Tile.TILEWIDTH * 3), (Tile.TILEHEIGHT * 38)));


            //Item that ends the game on pick up
            entityManager.addWeapon(new Katana(handler, Tile.TILEWIDTH, Tile.TILEHEIGHT, (Tile.TILEWIDTH * 48), (Tile.TILEHEIGHT * 48)));

            loadWorld(path);

            entityManager.getPlayer().setX(spawnX);
            entityManager.getPlayer().setY(spawnY);
        }

        /// <summary>
        /// Calls the entityManager's update method, updating all the entities.
        /// </summary>
        public void Update()
        {
            entityManager.update();
        }

        /// <summary>
        /// Renders all the world tiles based off of where the character is and 
        /// the gameCamera object, then calls the entityManager's render method 
        /// which renders all entities.
        /// </summary>
        /// <param name="canvas"></param>
        public void Render(SKCanvas canvas)
        {
            // Render Tiles first.
            int xStart = (int)Math.Max(0, handler.GetGameCamera().GetXOffset() / Tile.TILEWIDTH);
            int xEnd = (int)Math.Min(width
                    , (handler.GetGameCamera().GetXOffset() + handler.getWidth()) / Tile.TILEWIDTH + 1);
            int yStart = (int)Math.Max(0, handler.GetGameCamera().GetYOffset() / Tile.TILEHEIGHT);
            int yEnd = (int)Math.Min(height
                    , (handler.GetGameCamera().GetYOffset() + handler.getHeight()) / Tile.TILEHEIGHT + 1);

            for (int y = yStart; y < yEnd; y++)
            {
                for (int x = xStart; x < xEnd; x++)
                {
                    getTile(x, y).Render(canvas, (int)(x * Tile.TILEWIDTH - handler.GetGameCamera().GetXOffset())
                            , (int)(y * Tile.TILEHEIGHT - handler.GetGameCamera().GetYOffset()));
                }
            }

            // Finally render all entities on top of the tiles.
            entityManager.render(canvas);
        }

        /**
         * Returns the Tile object at the given TILE location
         * @param x
         * 	The location of the tile on the x axis, in TILES
         * @param y
         * 	The location of the tile on the y axis, in TILES
         * @return
         * 	The Tile object at the specified location
         */
        public Tile getTile(int x, int y)
        {

            if (x < 0 || y < 0 || x >= width || y >= height)
                return Tile.grassTile;

            Tile t = Tile.mTiles[wTiles[x][y]];
            if (t == null)
                return Tile.grassTile;

            return t;
        }

        /**
         * Loads the world from the given world file path (parses through the file)
         * Determines the width/height of the world in TILES
         * Determines the spawn location of the player in TIXELS
         * Saves the tile information into the wTiles array
         * @param path
         * 	The File path of the world
         */
        private void loadWorld(String path)
        {
            String file = Utils.loadFileAsString(path);
            String[] tokens = file.split("\\s+"); // split on whitespace

            width = int.Parse(tokens[0]);
            height = int.Parse(tokens[1]);
            spawnX = int.Parse(tokens[2]) * Tile.TILEWIDTH;
            spawnY = int.Parse(tokens[3]) * Tile.TILEHEIGHT;

            wTiles = new int[width][height];
            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    wTiles[x][y] = int.Parse(tokens[(x + y * width) + 4]);
                }
            }

        }

        public void teleport()
        {

        }

        #region Getters and Setters

        /// <summary>
        /// Returns the width of the world in TILES.
        /// </summary>
        public int GetWidth()
        {
            return width;
        }

        /// <summary>
        /// Returns the height of the world in TILES.
        /// </summary>
        public int GetHeight()
        {
            return height;
        }

        /// <summary>
        /// Returns the player x axis spawn location in TIXELS.
        /// </summary>
        public int GetSpawnX()
        {
            return spawnX;
        }

        /// <summary>
        /// Returns the player y axis spawn location in TIXELS.
        /// </summary>
        public int GetSpawnY()
        {
            return spawnY;
        }

        /// <summary>
        /// Returns the entityManager containing all entities in the world.
        /// </summary>
        public EntityManager GetEntityManager()
        {
            return entityManager;
        }

        /// <summary>
        /// Sets the entityManager that will contain all entities in the world.
        /// </summary>
        /// <param name="entityManager">The entity manager to set.</param>
        public void SetEntityManager(EntityManager entityManager)
        {
            this.entityManager = entityManager;
        }

        #endregion
    }
}

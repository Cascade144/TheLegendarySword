using SkiaSharp;
using SwordEngine.Utilities;

namespace SwordEngine.Gfx
{
    public class Assets
    {
        /// <summary>
        /// The width/height of the textures in the given images (pixel values).
        /// </summary>
        private static int width = 20, height = 20;

        /// <summary>
        /// World 1 images.
        /// </summary>
        public static SKBitmap grass, stone, lava, tree, cE1, rock, stonegrass, water, sand;

        /// <summary>
        /// World 2 images.
        /// </summary>
        public static SKBitmap cFloor, cRock, cMushroom, cSlime, cExit, cWall;

        /// <summary>
        /// Player images
        /// </summary>
        public static SKBitmap player;

        /// <summary>
        /// Enemy Images
        /// </summary>
        public static SKBitmap slime, mouse, skeleton, snake;

        /// <summary>
        /// Static Images.
        /// </summary>
        public static SKBitmap chest;

        /// <summary>
        /// Static Images.
        /// </summary>
        public static SKBitmap[] dagger = new SKBitmap[5];
        public static SKBitmap[] longSword = new SKBitmap[5];
        public static SKBitmap[] axe = new SKBitmap[5];
        public static SKBitmap[] ballNChain = new SKBitmap[2];
        public static SKBitmap[] spear = new SKBitmap[5];
        public static SKBitmap katana;
        
        /// <summary>
        /// This function loads and crops out sprites from the /res/textures folder 
        /// and places the images in their respective bufferedImage variable
        /// </summary>
        public static void init()
        {
            var texturesPath = ResourcePaths.ResolveResourcePath("res", "textures");

            // Finds a tile sheet from resources folder.
            SKImage spriteSheetWorld1 = SKImage.FromEncodedData(Path.Combine(texturesPath, "world1tileSheet.png"));
            SKBitmap bitmapWorld1 = SKBitmap.FromImage(spriteSheetWorld1);
            SpriteSheet world1 = new SpriteSheet(bitmapWorld1);

            // Grab each sprite from sheet and apply info to each tile variable.
            rock = world1.Crop(0, 0, width, height);
            grass = world1.Crop(width, 0, width, height);
            stone = world1.Crop(width * 2, 0, width, height);
            lava = world1.Crop(width * 3, 0, width, height);
            tree = world1.Crop(width * 4, 0, width, height);
            cE1 = world1.Crop(0, height, width, height);
            stonegrass = world1.Crop(width, height, width, height);
            water = world1.Crop(width * 2, height, width, height);
            sand = world1.Crop(width * 3, height, width, height);

            // Sprites for world2 tilesheet.
            SKImage spriteSheetWorld2 = SKImage.FromEncodedData(Path.Combine(texturesPath, "world2tileSheet.png"));
            SKBitmap bitmapWorld2 = SKBitmap.FromImage(spriteSheetWorld2);
            SpriteSheet world2 = new SpriteSheet(bitmapWorld2);
            cFloor = world2.Crop(0, 0, width, height);
            cRock = world2.Crop(width, 0, width, height);
            cRock = world2.Crop(width, 0, width, height);
            cMushroom = world2.Crop(width * 2, 0, width, height);
            cSlime = world2.Crop(width * 3, 0, width, height);
            cExit = world2.Crop(width * 4, 0, width, height);
            cWall = world2.Crop(0, height, width, height);

            //Finds a creature sheet from resources folder
            SKImage spriteSheetCreature = SKImage.FromEncodedData(Path.Combine(texturesPath, "creatureSheet.png"));
            SKBitmap bitmapCreature = SKBitmap.FromImage(spriteSheetCreature);
            SpriteSheet creatureSheet = new SpriteSheet(bitmapCreature);

            //Grab each sprite from the sheet and apply to each creature variable
            slime = creatureSheet.Crop(0, 0, width, height);
            mouse = creatureSheet.Crop(width, 0, width, height);
            skeleton = creatureSheet.Crop(width * 2, 0, width, height);
            snake = creatureSheet.Crop(width * 3, 0, width, height);

            // Load Character sheet.
            SKImage spriteSheetChar = SKImage.FromEncodedData(Path.Combine(texturesPath, "characterSheet.png"));
            SKBitmap bitmapChar = SKBitmap.FromImage(spriteSheetChar);
            SpriteSheet sheet3 = new SpriteSheet(bitmapChar);
            player = sheet3.Crop(0, 0, width, height);

            // Load Object sheet.
            SKImage spriteSheetObject = SKImage.FromEncodedData(Path.Combine(texturesPath, "objectSheet.png"));
            SKBitmap bitmapObject = SKBitmap.FromImage(spriteSheetObject);
            SpriteSheet sheet4 = new SpriteSheet(bitmapObject);
            chest = sheet4.Crop(0, 0, width, height);

            // Load Weap sheet
            SKImage spriteSheetWeap = SKImage.FromEncodedData(Path.Combine(texturesPath, "weaponTiles.png"));
            SKBitmap bitmapWeap= SKBitmap.FromImage(spriteSheetWeap);
            SpriteSheet weapon = new SpriteSheet(bitmapWeap);

            // Grab each sprite from the sheet and apply to each creature variable
            for (int i = 0; i < 5; i++)
            {
                dagger[i] = weapon.Crop(0, (height * 2) * i, (width * 2), (height * 2));
                longSword[i] = weapon.Crop((width * 2), (height * 2) * i, (width * 2), (height * 2));
                axe[i] = weapon.Crop((width * 4), (height * 2) * i, (width * 2), (height * 2));
                spear[i] = weapon.Crop((width * 6), (height * 2) * i, (width * 2), (height * 2));
            }
            ballNChain[0] = weapon.Crop((width * 8), 0, (width * 2), (height * 2));
            ballNChain[1] = weapon.Crop((width * 8), (height * 2), (width * 2), (height * 2));
            katana = weapon.Crop(0, (height * 10), (width * 2), (height * 2));
        }
    }
}

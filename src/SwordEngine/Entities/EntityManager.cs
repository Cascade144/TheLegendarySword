using SkiaSharp;
using SwordEngine.Weapons;
using SwordEngine.Entities.Creatures;

namespace SwordEngine.Entities
{
    /// <summary>
    /// This class contains an array to manage all entites in the game engine.
    /// Updates and renders all entities.
    /// </summary>
    public class EntityManager
    {
        /// <summary>
        /// Returns the main handler.
        /// </summary>
        private Handler handler;

        /// <summary>
        /// The Controllable Player Entity
        /// </summary>
        private Player player;

        /// <summary>
        /// List of all the entites in the game.
        /// </summary>
        private List<Entity> entities;

        /// <summary>
        /// List of all the weapons in the game.
        /// </summary>
        private List<Weapon> weapons;

        /// <summary>
        /// Constructs the entity manager.
        /// </summary>
        /// <param name="handler">The main game handler.</param>
        /// <param name="player">The main player.</param>
        public EntityManager(Handler handler, Player player)
        {
            this.handler = handler;
            this.player = player;
            entities = new List<Entity>();
            weapons = new List<Weapon>();
            addEntity(player);
        }

        public void update()
        {
            foreach (Entity entity in entities.ToList())
            {
                entity.Update();
                if (!entity.IsAlive())
                {
                    entities.Remove(entity);
                }
            }

            foreach (Weapon weapon in weapons.ToList())
            {
                weapon.Update(player);
                if (weapon.IsPickedUp())
                {
                    weapons.Remove(weapon);
                }
            }

            // Sort entites later.
            //entities.sort(renderSorter);
        }


        /// <summary>
        /// Goes through every entity in the ArrayList of entities and calls their render methods.
        /// Since the update method is always called before the render method is, the ArrayList is 
        /// already sorted and will correctly render all entities to the screen.
        /// </summary>
        /// <param name="canvas">The current game canvas.</param>
        public void render(SKCanvas canvas)
        {
            foreach(Entity e in entities)
            {
                if (e != player) e.Render(canvas);
            }

            foreach(Weapon w in weapons)
            {
                w.Render(canvas);
            }

            player.Render(canvas);
        }

        /// <summary>
        ///  Adds an entity to the List of entities.
        /// </summary>
        /// <param name="e"></param>
        public void addEntity(Entity e)
        {
            entities.Add(e);
        }

        /// <summary>
        /// Adds a weapon to the List of weapons.
        /// </summary>
        /// <param name="w"></param>
        public void addWeapon(Weapon w)
        {
            weapons.Add(w);
        }

        #region Getters and Setters

        /// <summary>
        /// Returns the main handler for the game.
        /// </summary>
        /// <returns>The main handler.</returns>
        public Handler getHandler()
        {
            return handler;
        }

        /// <summary>
        /// Sets the main handler of this object.
        /// </summary>
        /// <param name="handler"></param>
        public void setHandler(Handler handler)
        {
            this.handler = handler;
        }

        /// <summary>
        /// Returns the player entity.
        /// </summary>
        /// <returns></returns>
        public Player GetPlayer()
        {
            return player;
        }

        /// <summary>
        /// Sets the player.
        /// </summary>
        /// <param name="player"></param>
        public void SetPlayer(Player player)
        {
            this.player = player;
        }

        /// <summary>
        /// Gets the entities.
        /// </summary>
        public List<Entity> GetEntities()
        {
            return entities;
        }

        /// <summary>
        /// Gets the list of weapons.
        /// </summary>
        public List<Weapon> GetWeapons()
        {
            return weapons;
        }

        /// <summary>
        /// Sets a new List that will contain another set of entities.
        /// </summary>
        /// <param name="entities">The new List.</param>
        public void setEntities(List<Entity> entities)
        {
            this.entities = entities;
        }

        #endregion

    }
}

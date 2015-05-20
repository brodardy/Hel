/*
 * Author : Yannick R. Brodard
 * File name : HEntity.cs
 * Version : 0.5.201505110823
 * Description : Base abstract class for the entities of the game
 */

using HelHelProject.Tools;
using HelProject.Features;
using HelProject.GameWorld.Map;
using HelProject.Tools;
using HelProject.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace HelProject.GameWorld.Entities
{
    /// <summary>
    /// Base abstract class for the entities of the game
    /// </summary>
    public abstract class HEntity : HObject
    {
        public const float DEFAULT_STRENGHT = 5.0f;
        public const float DEFAULT_AGILITY = 5.0f;
        public const float DEFAULT_VITALITY = 5.0f;
        public const float DEFAULT_MAGIC = 5.0f;
        public const float DEFAULT_ATTACKSPEED = 0.6f;
        public const float DEFAULT_MINUMUMDAMAGE = 1.0f;
        public const float DEFAULT_MAXIMUMDAMAGE = 3.0f;
        public const float DEFAULT_MANAREGENERATION = 1.0f;
        public const float DEFAULT_MOVEMENTSPEED = 5.0f;
        public const float DEFAULT_LIFEPOINTS = 100.0f;
        public const float DEFAULT_ATTACKBOUND_WIDTH = 1.8f;
        public const float DEFAULT_ATTACKBOUND_HEIGHT = 2.7f;

        private FeatureCollection _initialFeatures;
        private FeatureCollection _actualFeatures;
        private FeatureCollection _maximizedFeatures;
        private FeatureManager _featureCalculator;
        private EntityState _state;
        private Vector2 _direction;
        private FRectangle _bounds;
        private FRectangle _attackBounds;
        private Texture2D _texture;

        /// <summary>
        /// Attack bounds of the entity
        /// </summary>
        public FRectangle AttackBounds
        {
            get { return _attackBounds; }
            set { _attackBounds = value; }
        }

        /// <summary>
        /// Texture of the entity
        /// </summary>
        public Texture2D Texture
        {
            get { return _texture; }
            set { _texture = value; }
        }

        /// <summary>
        /// Bounds of the entity
        /// </summary>
        public FRectangle Bounds
        {
            get { return _bounds; }
            set { _bounds = value; }
        }

        /// <summary>
        /// Direction the character is facing
        /// </summary>
        public Vector2 Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }

        /// <summary>
        /// State of the entity
        /// </summary>
        public EntityState State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// Maximized features
        /// </summary>
        public FeatureCollection MaximizedFeatures
        {
            get { return _maximizedFeatures; }
            set { _maximizedFeatures = value; }
        }

        /// <summary>
        /// Feature manager to calculate the actual features
        /// </summary>
        public FeatureManager FeatureCalculator
        {
            get { return _featureCalculator; }
            set { _featureCalculator = value; }
        }

        /// <summary>
        /// Actual features of the entity
        /// </summary>
        public FeatureCollection ActualFeatures
        {
            get { return _actualFeatures; }
            set { _actualFeatures = value; }
        }

        /// <summary>
        /// Initial feature of the entity
        /// </summary>
        public FeatureCollection InitialFeatures
        {
            get { return _initialFeatures; }
            set { _initialFeatures = value; }
        }

        /// <summary>
        /// Creates an entity
        /// </summary>
        public HEntity() : this(Vector2.Zero) { /* no code... */ }

        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="position">Position of the entity</param>
        public HEntity(Vector2 position)
            : this(new FeatureCollection()
            {
                Strenght = DEFAULT_STRENGHT,
                Vitality = DEFAULT_VITALITY,
                Agility = DEFAULT_AGILITY,
                Magic = DEFAULT_MAGIC,
                InitialAttackSpeed = DEFAULT_ATTACKSPEED,
                MinimumDamage = DEFAULT_MINUMUMDAMAGE,
                MaximumDamage = DEFAULT_MAXIMUMDAMAGE,
                InitialManaRegeneration = DEFAULT_MANAREGENERATION,
                InitialMovementSpeed = DEFAULT_MOVEMENTSPEED,
                InitialLifePoints = DEFAULT_LIFEPOINTS
            }, position, 0f, 0f, null) { /* no code... */ }

        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="initialFeatures">Initial Features of the enitity</param>
        /// <param name="position">Position of the entity</param>
        public HEntity(FeatureCollection initialFeatures, Vector2 position, float width, float height, string textureName)
            : base(true, position)
        {
            this.InitialFeatures = initialFeatures;
            this.Position = position;
            this.FeatureCalculator = new FeatureManager(this.InitialFeatures);
            this.ActualFeatures = this.FeatureCalculator.GetCalculatedFeatures();
            this.MaximizedFeatures = (FeatureCollection)this.ActualFeatures.Clone();
            this.State = EntityState.Idle;
            this.Texture = TextureManager.Instance.GetTexture(textureName);
            this.Bounds = new FRectangle(width, height);
            this.Bounds.SetBoundsWithTexture(position, this.Texture.Width, this.Texture.Height);
            this.AttackBounds = new FRectangle(DEFAULT_ATTACKBOUND_WIDTH, DEFAULT_ATTACKBOUND_HEIGHT);
            this.AttackBounds.X = this.Position.X - this.AttackBounds.Width / 2f;
            this.AttackBounds.Y = this.Position.Y - this.AttackBounds.Height / 2f;
        }

        /// <summary>
        /// Updates the entity
        /// </summary>
        /// <param name="gameTime"></param>
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            this.AttackBounds.X = this.Position.X - this.AttackBounds.Width / 2f;
            this.AttackBounds.Y = this.Position.Y - this.AttackBounds.Height / 2f;
        }

        /// <summary>
        /// Draws the entity
        /// </summary>
        /// <param name="spriteBatch"></param>
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (this.Texture != null && this.Bounds != null)
            {
                Vector2 boundsPosA = new Vector2(this.Bounds.X, this.Bounds.Y);

                Vector2 position = ScreenManager.Instance.GetCorrectScreenPosition(boundsPosA, PlayScreen.Instance.Camera.Position);
                spriteBatch.Draw(this.Texture, position, Color.White);
            }

            Vector2 start = ScreenManager.Instance.GetCorrectScreenPosition(this.AttackBounds.Position, PlayScreen.Instance.Camera.Position);
            Vector2 end = ScreenManager.Instance.GetCorrectScreenPosition(new Vector2(this.AttackBounds.Position.X + this.AttackBounds.Width, this.AttackBounds.Position.Y + this.AttackBounds.Height), PlayScreen.Instance.Camera.Position);
            end.X += 1f;
            end.Y += 1f;
            Primitives2D.Instance.DrawRectangle(spriteBatch, start, end, Color.Red);
        }

        /// <summary>
        /// Corrects the movement. When the entity touches an unwalkable object, it slides instead of stopping completly
        /// </summary>
        /// <param name="direction">Direction of the movement</param>
        /// <param name="newPosition">The new calculated position</param>
        /// <param name="newBounds">The new bounds according to the position</param>
        /// <param name="elapsedTime">Elapsed game time in seconds</param>
        public void ApplyFluidMovement(Vector2 direction, Vector2 newPosition, FRectangle newBounds, float elapsedTime)
        {
            // Is the position of the hero on a walkable area ?
            if (this.IsCharacterSurfaceWalkable(newPosition, newBounds))
            {
                this.Position = newPosition; // Apply the new position to the hero
                this.Bounds = newBounds; // Apply the new bounds to the hero
            }
            else // try with the biggest axis
            {
                newPosition = this.Position;
                float nX = (direction.X >= 0) ? direction.X : direction.X * -1;
                float nY = (direction.Y >= 0) ? direction.Y : direction.Y * -1;

                if (nX > nY)
                {
                    newPosition += new Vector2(direction.X, 0.0f) * elapsedTime * (this.FeatureCalculator.GetTotalMovementSpeed());
                }
                else if (nX < nY)
                {
                    newPosition += new Vector2(0.0f, direction.Y) * elapsedTime * (this.FeatureCalculator.GetTotalMovementSpeed());
                }

                newBounds.SetBoundsWithTexture(newPosition, this.Texture.Width, this.Texture.Height);

                if (this.IsCharacterSurfaceWalkable(newPosition, newBounds))
                {
                    this.Position = newPosition; // Apply the new position to the hero
                    this.Bounds = newBounds; // Apply the new bounds to the hero
                }
                else // try with the smallest axis
                {
                    newPosition = this.Position;

                    if (nX < nY)
                    {
                        newPosition += new Vector2(direction.X, 0.0f) * elapsedTime * (this.FeatureCalculator.GetTotalMovementSpeed());
                    }
                    else if (nX > nY)
                    {
                        newPosition += new Vector2(0.0f, direction.Y) * elapsedTime * (this.FeatureCalculator.GetTotalMovementSpeed());
                    }

                    newBounds.SetBoundsWithTexture(newPosition, this.Texture.Width, this.Texture.Height);

                    if (this.IsCharacterSurfaceWalkable(newPosition, newBounds))
                    {
                        this.Position = newPosition; // Apply the new position to the hero
                        this.Bounds = newBounds; // Apply the new bounds to the hero
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the surface where the hero is present if walkable
        /// </summary>
        /// <param name="position">Position of the hero</param>
        /// <param name="bounds">Bounds of the hero</param>
        /// <returns></returns>
        public bool IsCharacterSurfaceWalkable(Vector2 position, FRectangle bounds)
        {
            bool validArea = true;
            List<HCell> unwalkableAdjacentCells = PlayScreen.Instance.CurrentMap.GetAdjacentUnwalkableCells((int)this.Position.X, (int)this.Position.Y, 1, 1);
            List<HHostile> hostiles = PlayScreen.Instance.CurrentMap.Hostiles;
            HHero hero = PlayScreen.Instance.PlayableCharacter;
            int nbrCells = unwalkableAdjacentCells.Count;
            int nbrHostiles = hostiles.Count;

            for (int i = 0; i < nbrCells; i++)
            {
                if (bounds.Intersects(unwalkableAdjacentCells[i].Bounds))
                    validArea = false;
            }

            for (int i = 0; i < nbrHostiles; i++)
            {
                if (this != hostiles[i] && bounds.Intersects(hostiles[i].Bounds))
                    validArea = false;
            }

            if (this != hero && bounds.Intersects(hero.Bounds))
                validArea = false;

            return validArea;
        }

        /// <summary>
        /// State of the entity
        /// </summary>
        public enum EntityState
        {
            Idle,
            Running,
            MeleeAttacking,
            RangeAttacking,
            SpellCasting,
        }
    }
}

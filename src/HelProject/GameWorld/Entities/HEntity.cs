/*
 * Author : Yannick R. Brodard
 * File name : HEntity.cs
 * Version : 0.5.201505110823
 * Description : Base abstract class for the entities of the game
 */

using HelProject.Features;
using HelProject.Tools;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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

        private FeatureCollection _initialFeatures;
        private FeatureCollection _actualFeatures;
        private FeatureCollection _maximizedFeatures;
        private FeatureManager _featureCalculator;
        private EntityState _state;
        private Vector2 _direction;

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
            }, position) { /* no code... */ }

        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="initialFeatures">Initial Features of the enitity</param>
        /// <param name="position">Position of the entity</param>
        public HEntity(FeatureCollection initialFeatures, Vector2 position)
            : base(true, position)
        {
            this.InitialFeatures = initialFeatures;
            this.Position = position;
            this.FeatureCalculator = new FeatureManager(this.InitialFeatures);
            this.ActualFeatures = this.FeatureCalculator.GetCalculatedFeatures();
            this.MaximizedFeatures = (FeatureCollection)this.ActualFeatures.Clone();
            this.State = EntityState.Idle;
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

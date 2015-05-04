/*
 * Author : Yannick R. Brodard
 * File name : HEntity.cs
 * Version : 0.1.201505040924
 * Description : Base entity of the game
 */

using HelProject.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.GameWorld.Entities
{
    public class HEntity : HObject
    {
        public const float DEFAULT_STRENGHT = 5.0f;
        public const float DEFAULT_AGILITY = 5.0f;
        public const float DEFAULT_VITALITY = 5.0f;
        public const float DEFAULT_MAGIC = 5.0f;
        public const float DEFAULT_ATTACKSPEED = 0.6f;
        public const float DEFAULT_MINUMUMDAMAGE = 1.0f;
        public const float DEFAULT_MAXIMUMDAMAGE = 3.0f;
        public const float DEFAULT_MANAREGENERATION = 1.0f;
        public const float DEFAULT_MOVEMENTSPEED = 1.0f;

        private FeatureCollection _initialFeatures;

        public FeatureCollection InitialFeatures
        {
            get { return _initialFeatures; }
            set { _initialFeatures = value; }
        }

        /// <summary>
        /// Creates an entity
        /// </summary>
        public HEntity()
            : this(DEFAULT_STRENGHT, DEFAULT_VITALITY, DEFAULT_AGILITY, DEFAULT_MAGIC, DEFAULT_ATTACKSPEED,
                DEFAULT_MINUMUMDAMAGE, DEFAULT_MAXIMUMDAMAGE, DEFAULT_MANAREGENERATION, DEFAULT_MOVEMENTSPEED) { /* no code... */ }

        /// <summary>
        /// Creates an entity
        /// </summary>
        /// <param name="initialStrenght"></param>
        /// <param name="initialVitality"></param>
        /// <param name="initialAgility"></param>
        /// <param name="initialMagic"></param>
        /// <param name="initialAttackSpeed"></param>
        /// <param name="initialMinimumDamage"></param>
        /// <param name="initialManaRegeneration"></param>
        /// <param name="initialMovementSpeed"></param>
        public HEntity(float initialStrenght, float initialVitality, float initialAgility, float initialMagic, float initialAttackSpeed, float initialMinimumDamage, float initialMaximumDamage, float initialManaRegeneration, float initialMovementSpeed)
            : base(true, new Tools.FPosition())
        {
            this._initialFeatures = new FeatureCollection();
            this._initialFeatures.Strenght = initialStrenght;
            this._initialFeatures.Vitality = initialVitality;
            this._initialFeatures.Agility = initialAgility;
            this._initialFeatures.Magic = initialMagic;
            this._initialFeatures.InitialAttackSpeed = initialAttackSpeed;
            this._initialFeatures.MinimumDamage = initialMinimumDamage;
            this._initialFeatures.MaximumDamage = initialMaximumDamage;
            this._initialFeatures.ManaRegeneration = initialManaRegeneration;
            this._initialFeatures.MovementSpeed = initialMovementSpeed;
        }
    }
}

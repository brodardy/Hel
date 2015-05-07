/*
 * Author : Yannick R. Brodard
 * File name : HSpellZone.cs
 * Version : 0.1.201505070916
 * Description : Abstract class and base of all spell-zones
 */

using HelProject.Features;
using HelProject.GameWorld.Entities;

namespace HelProject.GameWorld.Spells
{
    /// <summary>
    /// Spell-zone
    /// </summary>
    public abstract class HSpellZone : HSpell
    {
        private float _range;
        private float _areaOfEffect;

        /// <summary>
        /// Casting range of the spell
        /// </summary>
        public float Range
        {
            get { return _range; }
            set { _range = value; }
        }

        /// <summary>
        /// Area of effect diameter of the spell
        /// </summary>
        public float AreaOfEffect
        {
            get { return _areaOfEffect; }
            set { _areaOfEffect = value; }
        }

        /// <summary>
        /// Creates a spell zone
        /// </summary>
        /// <param name="hero">Hero that the spell is attached to</param>
        /// <param name="features">Features of the spell</param>
        /// <param name="timeOfEffect">Time of effect once the spell is active</param>
        /// <param name="name">Name of the spell</param>
        /// <param name="range">Casting range of the spell</param>
        /// <param name="areaOfEffect">Area of effect diameter of the spell</param>
        public HSpellZone(HHero hero, FeatureCollection features, float timeOfEffect, string name, float range, float areaOfEffect)
            : base(hero, features, timeOfEffect, name)
        {
            this.Range = range;
            this.AreaOfEffect = areaOfEffect;
        }
    }
}

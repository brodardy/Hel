/*
 * Author : Yannick R. Brodard
 * File name : HSpellSingelTarget.cs
 * Version : 0.1.201505070928
 * Description : Abstract class and base of all single targeted spells
 */

using HelProject.Features;
using HelProject.GameWorld.Entities;

namespace HelProject.GameWorld.Spells
{
    /// <summary>
    /// Single targeted spell
    /// </summary>
    public abstract class HSpellSingleTarget : HSpell
    {
        private HEntity _target;

        /// <summary>
        /// Target of the spell
        /// </summary>
        public HEntity Target
        {
            get { return _target; }
            set { _target = value; }
        }

        /// <summary>
        /// Creates a single target spell
        /// </summary>
        /// <param name="hero">Hero that the spell is attached to</param>
        /// <param name="features">Features of the spell</param>
        /// <param name="timeOfEffect">Time of effect once the spell is active</param>
        /// <param name="name">Name of the spell</param>
        /// <param name="target">Target of the spell</param>
        public HSpellSingleTarget(HHero hero, FeatureCollection features, float timeOfEffect, string name, HEntity target)
            : base(hero, features, timeOfEffect, name)
        {
            this.Target = target;
        }
    }
}

/*
 * Author : Yannick R. Brodard
 * File name : HSpellBuff.cs
 * Version : 0.1.201505070911
 * Description : Abstract class and base of all spell-buffs
 */

#region USING STATEMENTS
using HelProject.Features;
using HelProject.GameWorld.Entities;
#endregion

namespace HelProject.GameWorld.Spells
{
    /// <summary>
    /// Spell buff
    /// </summary>
    public abstract class HSpellBuff : HSpell
    {
        /// <summary>
        /// Creates a spell buff
        /// </summary>
        /// <param name="hero">Hero that the spell is attached to</param>
        /// <param name="features">Features of the spell</param>
        /// <param name="timeOfEffect">Time of effect once the spell is active</param>
        /// <param name="name">Name of the spell</param>
        public HSpellBuff(HHero hero, FeatureCollection features, float timeOfEffect, string name) : base(hero, features, timeOfEffect, name) { /* no code... */ }
    }
}

/*
 * Author : Yannick R. Brodard
 * File name : Feature.cs
 * Date of creation : 21.04.2015
 * Time of creation : 11:16
 * Date of latest modification : 21.04.2015
 * Time of latest modification : 11:30
 * Version : 0.1
 * Description : Class of the features for the characters.
 *               Theses features upgrades the character it is attributed too
 *               for it to become more powerful in that specific feature.
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.Content
{
    public class Feature
    {
        /// <summary>
        /// Names of all the existing features
        /// </summary>
        public static enum Name
        {
            Strenght,               // Gives more damage
            Agility,                // Gives more movement speed and more stealthiness
            Vitality,               // Gives more life
            Magic,                  // Give more magic damage
            AttackSpeed,            // Give more attack speed
            InitialAttackSpeed,     // Sets the initial attack speed
            PhysicalDamage,         // Sets the initial physical damage
            MagicalDamage,          // Sets the initial magical damage
            Armor,                  // Decreases the received physical damage
            MagicResistance,        // Decreases the received magical damage
            LifeRegeneration,       // Increases the life regeneration
            ManaRegeneration        // Increases the mana regeneration
        };

        public static enum Type
        {
            RawValue,               // Raw value of the feature that adds to the initial value
            Percentage,             // The feature is a percentage that increases the initial value
        };


    }
}

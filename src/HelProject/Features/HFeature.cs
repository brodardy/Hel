/*
 * Author : Yannick R. Brodard
 * File name : HFeature.cs
 * Version : 0.1.201504211340
 * Description : Class of the features for the characters.
 *               Theses features upgrades the character it is attributed too
 *               for it to become more powerful in that specific feature.
 */

using System;

namespace HelProject
{
    public class HFeature : Object
    {
        #region STATIC_VALUES

        /// <summary>
        /// Names of all the existing features
        /// </summary>
        public enum Name
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

        #endregion

        #region CONSTANTS

        private const float DEFAULT_FEATURE_VALUE = 0.0f; // default value of a feature

        #endregion

        #region VARIABLES

        private float _value;           // value of the feature
        private HFeature.Name _name;     // name of the feature

        #endregion

        #region PROPRIETIES

        /// <summary>
        /// Value of the feature
        /// </summary>
        public float Value
        {
            get { return this._value; }
            set { this._value = value; }
        }

        /// <summary>
        /// Name of the feature
        /// </summary>
        public HFeature.Name FeatureName
        {
            get { return this._name; }
            private set { this._name = value; }
        }

        #endregion

        #region CONSTRUCTORS

        /// <summary>
        /// Create a feature with the default value
        /// </summary>
        /// <param name="pName">Name of the feature</param>
        /// <param name="pType">Type of the feature</param>
        public HFeature(HFeature.Name pName) : this(pName, DEFAULT_FEATURE_VALUE) { /* no code... */ }

        /// <summary>
        /// Create a feature
        /// </summary>
        /// <param name="pName">Name of the feature</param>
        /// <param name="pType">Type of the feature</param>
        /// <param name="pValue">Value of the feature</param>
        public HFeature(HFeature.Name pName, float pValue)
        {
            this.FeatureName = pName;
            this.Value = pValue;
        }

        #endregion
    }
}
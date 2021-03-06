﻿/*
 * Author : Yannick R. Brodard
 * File name : FeatureCollection.cs
 * Version : 0.1.201505041040
 * Description : Represents a collection of all possible features
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.Features
{
    public class FeatureCollection : Object, ICloneable
    {
        private float _initialAttackSpeed;
        private float _initialMovementSpeed;
        private float _initialManaRegeneration;
        private float _initialLifePoints;
        private float _strenght;
        private float _agility;
        private float _vitality;
        private float _magic;
        private float _attackSpeed;
        private float _minimumDamage;
        private float _maximumDamage;
        private float _minimumMagicDamage;
        private float _maximumMagicDamage;
        private float _armor;
        private float _magicResistance;
        private float _lifeRegeneration;
        private float _manaRegeneration;
        private float _movementSpeed;
        private float _lifePoints;

        /// <summary>
        /// Imposed life points
        /// </summary>
        public float InitialLifePoints
        {
            get { return _initialLifePoints; }
            set { _initialLifePoints = value; }
        }

        /// <summary>
        /// Imposed attack speed (attacks per second)
        /// </summary>
        public float InitialAttackSpeed
        {
            get { return _initialAttackSpeed; }
            set { _initialAttackSpeed = value; }
        }

        /// <summary>
        /// Imposed movement speed (meters / second)
        /// </summary>
        public float InitialMovementSpeed
        {
            get { return _initialMovementSpeed; }
            set { _initialMovementSpeed = value; }
        }

        /// <summary>
        /// Imposed mana regeneration (mana per second)
        /// </summary>
        public float InitialManaRegeneration
        {
            get { return _initialManaRegeneration; }
            set { _initialManaRegeneration = value; }
        }

        /// <summary>
        /// Strenght points
        /// </summary>
        public float Strenght
        {
            get { return _strenght; }
            set { _strenght = value; }
        }

        /// <summary>
        /// Agility points
        /// </summary>
        public float Agility
        {
            get { return _agility; }
            set { _agility = value; }
        }

        /// <summary>
        /// Vitality points
        /// </summary>
        public float Vitality
        {
            get { return _vitality; }
            set { _vitality = value; }
        }

        /// <summary>
        /// Magic points
        /// </summary>
        public float Magic
        {
            get { return _magic; }
            set { _magic = value; }
        }

        /// <summary>
        /// Attack speed buff percentage
        /// </summary>
        /// <example>25%</example>
        public float AttackSpeed
        {
            get { return _attackSpeed; }
            set { _attackSpeed = value; }
        }

        /// <summary>
        /// Minimum damage
        /// </summary>
        public float MinimumDamage
        {
            get { return _minimumDamage; }
            set { _minimumDamage = value; }
        }

        /// <summary>
        /// Maximum damage
        /// </summary>
        public float MaximumDamage
        {
            get { return _maximumDamage; }
            set { _maximumDamage = value; }
        }

        /// <summary>
        /// Minimum magic damage
        /// </summary>
        public float MinimumMagicDamage
        {
            get { return _minimumMagicDamage; }
            set { _minimumMagicDamage = value; }
        }

        /// <summary>
        /// Maximum magic damage
        /// </summary>
        public float MaximumMagicDamage
        {
            get { return _maximumMagicDamage; }
            set { _maximumMagicDamage = value; }
        }

        /// <summary>
        /// Armor points
        /// </summary>
        public float Armor
        {
            get { return _armor; }
            set { _armor = value; }
        }

        /// <summary>
        /// Magic resistance points
        /// </summary>
        public float MagicResistance
        {
            get { return _magicResistance; }
            set { _magicResistance = value; }
        }

        /// <summary>
        /// Life regeneration (life per second)
        /// </summary>
        public float LifeRegeneration
        {
            get { return _lifeRegeneration; }
            set { _lifeRegeneration = value; }
        }

        /// <summary>
        /// Mana regeneration (mana per second)
        /// </summary>
        public float ManaRegeneration
        {
            get { return _manaRegeneration; }
            set { _manaRegeneration = value; }
        }

        /// <summary>
        /// Movement speed buff (percentage)
        /// </summary>
        /// <example>25%</example>
        public float MovementSpeed
        {
            get { return _movementSpeed; }
            set { _movementSpeed = value; }
        }

        /// <summary>
        /// Life points
        /// </summary>
        public float LifePoints
        {
            get { return _lifePoints; }
            set { _lifePoints = value; }
        }

        /// <summary>
        /// Creates a feature collection
        /// </summary>
        public FeatureCollection()
        {
            this._agility = 0f;
            this._armor = 0f;
            this._attackSpeed = 0f;
            this._initialAttackSpeed = 0f;
            this._initialMovementSpeed = 0f;
            this._initialManaRegeneration = 0f;
            this._lifeRegeneration = 0f;
            this._magic = 0f;
            this._magicResistance = 0f;
            this._manaRegeneration = 0f;
            this._maximumDamage = 0f;
            this._maximumMagicDamage = 0f;
            this._minimumDamage = 0f;
            this._minimumMagicDamage = 0f;
            this._movementSpeed = 0f;
            this._strenght = 0f;
            this._vitality = 0f;
            this._initialLifePoints = 0f;
            this._lifePoints = 0f;
        }

        /// <summary>
        /// Clones the object
        /// </summary>
        /// <returns>object</returns>
        public object Clone()
        {
            FeatureCollection obj = new FeatureCollection();

            obj.InitialAttackSpeed = this.InitialAttackSpeed;
            obj.InitialLifePoints = this.InitialLifePoints;
            obj.InitialManaRegeneration = this.InitialManaRegeneration;
            obj.InitialMovementSpeed = this.InitialMovementSpeed;
            obj.LifePoints = this.LifePoints;
            obj.LifeRegeneration = this.LifeRegeneration;
            obj.Magic = this.Magic;
            obj.MagicResistance = this.MagicResistance;
            obj.ManaRegeneration = this.ManaRegeneration;
            obj.MaximumDamage = this.MaximumDamage;
            obj.MaximumMagicDamage = this.MaximumMagicDamage;
            obj.MinimumDamage = this.MinimumDamage;
            obj.MinimumMagicDamage = this.MinimumMagicDamage;
            obj.MovementSpeed = this.MovementSpeed;
            obj.Strenght = this.Strenght;
            obj.Vitality = this.Vitality;

            return obj;
        }

        /// <summary>
        /// Sets all features to zero
        /// </summary>
        public void SetAllToZero()
        {
            this._agility = 0f;
            this._armor = 0f;
            this._attackSpeed = 0f;
            this._initialAttackSpeed = 0f;
            this._initialMovementSpeed = 0f;
            this._initialManaRegeneration = 0f;
            this._lifeRegeneration = 0f;
            this._magic = 0f;
            this._magicResistance = 0f;
            this._manaRegeneration = 0f;
            this._maximumDamage = 0f;
            this._maximumMagicDamage = 0f;
            this._minimumDamage = 0f;
            this._minimumMagicDamage = 0f;
            this._movementSpeed = 0f;
            this._strenght = 0f;
            this._vitality = 0f;
            this._initialLifePoints = 0f;
            this._lifePoints = 0f;
        }

        /// <summary>
        /// Sets the features for a draugr level 1
        /// </summary>
        public void SetToDraugrLvlOne()
        {
            this._agility = 0f;
            this._armor = .0f;
            this._attackSpeed = 0f;
            this._initialAttackSpeed = 0.6f;
            this._initialMovementSpeed = 3.0f;
            this._initialManaRegeneration = .0f;
            this._lifeRegeneration = .0f;
            this._magic = 0f;
            this._magicResistance = .0f;
            this._manaRegeneration = 0f;
            this._maximumDamage = 3f;
            this._maximumMagicDamage = .0f;
            this._minimumDamage = 1f;
            this._minimumMagicDamage = .0f;
            this._movementSpeed = .0f;
            this._strenght = 0f;
            this._vitality = 0f;
            this._initialLifePoints = 30.0f;
            this._lifePoints = .0f;
        }
    }
}

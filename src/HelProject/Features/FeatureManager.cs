/*
 * Author : Yannick R. Brodard
 * File name : FeatureManager.cs
 * Version : 0.1.201505071332
 * Description : Manages the calculation of the feature for the entities
 */

using HelProject.GameWorld;
using HelProject.GameWorld.Spells;
using System.Collections.Generic;

namespace HelProject.Features
{
    /// <summary>
    /// Manages the calculation of the feature for the entities
    /// </summary>
    public class FeatureManager
    {
        public const float LIFE_PER_VITALITY = 100.0f;

        private List<HItem> _activeItems;
        private List<HSpell> _activeSpells;
        private FeatureCollection _initialFeatures;

        /// <summary>
        /// Items worn by the hero
        /// </summary>
        public List<HItem> ActiveItems
        {
            get { return _activeItems; }
            set { _activeItems = value; }
        }

        /// <summary>
        /// Currently casted spells of the hero
        /// </summary>
        public List<HSpell> ActiveSpells
        {
            get { return _activeSpells; }
            set { _activeSpells = value; }
        }

        /// <summary>
        /// Initial features of the hero
        /// </summary>
        public FeatureCollection InitialFeatures
        {
            get { return _initialFeatures; }
            set { _initialFeatures = value; }
        }

        /// <summary>
        /// Creates a feature manager
        /// </summary>
        /// <param name="features">Initial features of the hero</param>
        /// <param name="spells">Active spells of the hero</param>
        /// <param name="items">Worn items of the hero</param>
        /// <remarks>Null values will be initialized (except the "features" parameter).</remarks>
        public FeatureManager(FeatureCollection features, List<HSpell> spells = null, List<HItem> items = null)
        {
            this.InitialFeatures = features;

            if (spells != null)
                this.ActiveSpells = spells;
            else
                this.ActiveSpells = new List<HSpell>();

            if (items != null)
                this.ActiveItems = items;
            else
                this.ActiveItems = new List<HItem>();
        }

        /// <summary>
        /// Gets the calculated features
        /// </summary>
        /// <returns>Feature collection</returns>
        /// <remarks>For movement speed, use the initial movement speed</remarks>
        public FeatureCollection GetCalculatedFeatures()
        {
            return new FeatureCollection()
            {
                Strenght = this.GetTotalStrenght(),
                Agility = this.GetTotalAgility(),
                Vitality = this.GetTotalVitality(),
                Magic = this.GetTotalMagic(),
                Armor = this.GetTotalArmor(),
                AttackSpeed = this.GetTotalAttackSpeed(),
                LifeRegeneration = this.GetTotalLifeRegeneration(),
                MagicResistance = this.GetTotalMagicResistance(),
                ManaRegeneration = this.GetTotalManaRegeneration(),
                MaximumDamage = this.GetTotalMaximumDamage(),
                MaximumMagicDamage = this.GetTotalMaximumMagicDamage(),
                MinimumDamage = this.GetTotalMinimumDamage(),
                MinimumMagicDamage = this.GetTotalMinimumMagicDamage(),
                InitialMovementSpeed = this.GetTotalMovementSpeed(),
                LifePoints = this.GetTotalLifePoints(),
            };
        }

        /// <summary>
        /// Calaculates the received damage
        /// </summary>
        /// <param name="damage">Damage given</param>
        /// <returns>Real received damage</returns>
        public float GetReceivedPhysicalDamage(float damage)
        {
            return damage - (this.GetTotalArmor() * 0.075f * damage);
        }

        /// <summary>
        /// Calculates the total strenght within the items, spells and initial values
        /// </summary>
        /// <returns>Total strenght</returns>
        public float GetTotalStrenght()
        {
            float str = this.InitialFeatures.Strenght;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                str += this.ActiveSpells[i].Features.Strenght;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                str += this.ActiveItems[i].Features.Strenght;
            }

            return str;
        }

        /// <summary>
        /// Calculates the total agility within the items, spells and initial values
        /// </summary>
        /// <returns>Total agility</returns>
        public float GetTotalAgility()
        {
            float agi = this.InitialFeatures.Agility;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                agi += this.ActiveSpells[i].Features.Agility;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                agi += this.ActiveItems[i].Features.Agility;
            }

            return agi;
        }

        /// <summary>
        /// Calculates the total Vitality within the items, spells and initial values
        /// </summary>
        /// <returns>Total Vitality</returns>
        public float GetTotalVitality()
        {
            float vit = this.InitialFeatures.Vitality;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                vit += this.ActiveSpells[i].Features.Vitality;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                vit += this.ActiveItems[i].Features.Vitality;
            }

            return vit;
        }

        /// <summary>
        /// Calculates the total Magic within the items, spells and initial values
        /// </summary>
        /// <returns>Total Magic</returns>
        public float GetTotalMagic()
        {
            float mag = this.InitialFeatures.Magic;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                mag += this.ActiveSpells[i].Features.Magic;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                mag += this.ActiveItems[i].Features.Magic;
            }

            return mag;
        }

        /// <summary>
        /// Calculates the total AttackSpeed within the items, spells and initial values
        /// </summary>
        /// <returns>Total AttackSpeed</returns>
        /// <remarks>Gets the highest initial attack speed and adds all the attack speed percentages</remarks>
        public float GetTotalAttackSpeed()
        {
            float attsp = this.InitialFeatures.InitialAttackSpeed;
            float totAttspBuff = this.InitialFeatures.AttackSpeed;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                if (this.ActiveSpells[i].Features.InitialAttackSpeed > attsp)
                    attsp = this.ActiveSpells[i].Features.InitialAttackSpeed;

                totAttspBuff += this.ActiveSpells[i].Features.AttackSpeed;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                if (this.ActiveItems[i].Features.InitialAttackSpeed > attsp)
                    attsp = this.ActiveItems[i].Features.InitialAttackSpeed;

                totAttspBuff += this.ActiveItems[i].Features.AttackSpeed;
            }

            return attsp * (totAttspBuff / 100.0f + 1.0f);
        }

        /// <summary>
        /// Calculates the total MinimumDamage within the items, spells and initial values
        /// </summary>
        /// <returns>Total MinimumDamage</returns>
        /// <remarks>Gets the total of minimum damage and adds 1% more per Strenght</remarks>
        public float GetTotalMinimumDamage()
        {
            float minDmg = this.InitialFeatures.MinimumDamage;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                minDmg += this.ActiveSpells[i].Features.MinimumDamage;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                minDmg += this.ActiveItems[i].Features.MinimumDamage;
            }

            return minDmg * (this.GetTotalStrenght() / 100.0f + 1.0f);
        }

        /// <summary>
        /// Calculates the total MaximumDamage within the items, spells and initial values
        /// </summary>
        /// <returns>Total MaximumDamage</returns>
        /// <remarks>Gets the total of maximum damage and adds 1% more per Strenght</remarks>
        public float GetTotalMaximumDamage()
        {
            float maxDmg = this.InitialFeatures.MaximumDamage;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                maxDmg += this.ActiveSpells[i].Features.MaximumDamage;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                maxDmg += this.ActiveItems[i].Features.MaximumDamage;
            }

            return maxDmg * (this.GetTotalStrenght() / 100.0f + 1.0f);
        }

        /// <summary>
        /// Calculates the total MinimumMagicDamage within the items, spells and initial values
        /// </summary>
        /// <returns>Total MinimumMagicDamage</returns>
        /// <remarks>Gets the total of minimum magic damage and adds 1% more per Magic point</remarks>
        public float GetTotalMinimumMagicDamage()
        {
            float minMagDmg = this.InitialFeatures.MinimumMagicDamage;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                minMagDmg += this.ActiveSpells[i].Features.MinimumMagicDamage;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                minMagDmg += this.ActiveItems[i].Features.MinimumMagicDamage;
            }

            return minMagDmg * (this.GetTotalMagic() / 100.0f + 1.0f);
        }

        /// <summary>
        /// Calculates the total MaximumMagicDamage within the items, spells and initial values
        /// </summary>
        /// <returns>Total MaximumMagicDamage</returns>
        /// <remarks>Gets the total of maximum magic damage and adds 1% more per Magic point</remarks>
        public float GetTotalMaximumMagicDamage()
        {
            float maxMagDmg = this.InitialFeatures.MaximumMagicDamage;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                maxMagDmg += this.ActiveSpells[i].Features.MaximumMagicDamage;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                maxMagDmg += this.ActiveItems[i].Features.MaximumMagicDamage;
            }

            return maxMagDmg * (this.GetTotalMagic() / 100.0f + 1.0f);
        }

        /// <summary>
        /// Calculates the total Armor within the items, spells and initial values
        /// </summary>
        /// <returns>Total Armor</returns>
        public float GetTotalArmor()
        {
            float arm = this.InitialFeatures.Armor;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                arm += this.ActiveSpells[i].Features.Armor;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                arm += this.ActiveItems[i].Features.Armor;
            }

            return arm;
        }

        /// <summary>
        /// Calculates the total MagicResistance within the items, spells and initial values
        /// </summary>
        /// <returns>Total MagicResistance</returns>
        public float GetTotalMagicResistance()
        {
            float magRes = this.InitialFeatures.MagicResistance;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                magRes += this.ActiveSpells[i].Features.MagicResistance;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                magRes += this.ActiveItems[i].Features.MagicResistance;
            }

            return magRes;
        }

        /// <summary>
        /// Calculates the total LifeRegeneration within the items, spells and initial values
        /// </summary>
        /// <returns>Total LifeRegeneration</returns>
        public float GetTotalLifeRegeneration()
        {
            float lifReg = this.InitialFeatures.LifeRegeneration;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                lifReg += this.ActiveSpells[i].Features.LifeRegeneration;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                lifReg += this.ActiveItems[i].Features.LifeRegeneration;
            }

            return lifReg;
        }

        /// <summary>
        /// Calculates the total ManaRegeneration within the items, spells and initial values
        /// </summary>
        /// <returns>Total ManaRegeneration</returns>
        public float GetTotalManaRegeneration()
        {
            float manReg = this.InitialFeatures.ManaRegeneration;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                manReg += this.ActiveSpells[i].Features.ManaRegeneration;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                manReg += this.ActiveItems[i].Features.ManaRegeneration;
            }

            return manReg;
        }

        /// <summary>
        /// Calculates the total MovementSpeed within the items, spells and initial values
        /// </summary>
        /// <returns>Total MovementSpeed</returns>
        /// <remarks>Gets the highest initial movement speed and adds all the movement speed percentages</remarks>
        public float GetTotalMovementSpeed()
        {
            float iniMovSp = this.InitialFeatures.InitialMovementSpeed;
            float totMovSpBuff = this.InitialFeatures.MovementSpeed;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                if (this.ActiveSpells[i].Features.InitialMovementSpeed > iniMovSp)
                    iniMovSp = this.ActiveSpells[i].Features.InitialMovementSpeed;

                totMovSpBuff += this.ActiveSpells[i].Features.MovementSpeed;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                if (this.ActiveItems[i].Features.InitialMovementSpeed > iniMovSp)
                    iniMovSp = this.ActiveItems[i].Features.InitialMovementSpeed;

                totMovSpBuff += this.ActiveItems[i].Features.MovementSpeed;
            }

            return iniMovSp * (totMovSpBuff / 100.0f + 1.0f);
        }

        /// <summary>
        /// Calculates the total LifePoints within the items, spells and initial values
        /// </summary>
        /// <returns>Total LifePoints</returns>
        public float GetTotalLifePoints()
        {
            float iniLifePoints = this.InitialFeatures.InitialLifePoints;

            for (int i = 0; i < this.ActiveSpells.Count; i++)
            {
                if (iniLifePoints < this.ActiveSpells[i].Features.InitialLifePoints)
                    iniLifePoints = this.ActiveSpells[i].Features.InitialLifePoints;
            }

            for (int i = 0; i < this.ActiveItems.Count; i++)
            {
                if (iniLifePoints < this.ActiveItems[i].Features.InitialLifePoints)
                    iniLifePoints = this.ActiveItems[i].Features.InitialLifePoints;
            }

            return this.GetTotalVitality() * LIFE_PER_VITALITY + iniLifePoints;
        }
    }
}

/*
 * Author : Yannick R. Brodard
 * File name : HUDManager.cs
 * Version : 0.1.201505191335
 * Description : Manager for the in-game HUD (Heads-up display)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HelProject.UI.HUD
{
    /// <summary>
    /// Manager for the in-game HUD (Heads-up display)
    /// </summary>
    public class HUDManager
    {
        /* SINGLETON START */
        private static HUDManager _instance;

        /// <summary>
        /// Instance of the HUD
        /// </summary>
        public static HUDManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new HUDManager();
                return _instance;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        private HUDManager() { /* no code... */ }
        /* SINGLETON END */

        private FillingBar _playerHealth;
        private FillingBar _playerMana;

        /// <summary>
        /// Filling bar for the player's health
        /// </summary>
        public FillingBar PlayerHealth
        {
            get { return _playerHealth; }
            set { _playerHealth = value; }
        }

        /// <summary>
        /// Filling bar for the player's mana
        /// </summary>
        public FillingBar PlayerMana
        {
            get { return _playerMana; }
            set { _playerMana = value; }
        }
    }
}

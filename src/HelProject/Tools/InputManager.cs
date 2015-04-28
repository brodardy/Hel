/*
 * Author : Yannick R. Brodard
 * File name : InputManager.cs
 * Version : 0.1.201504281237
 * Description : Manages all the input in the game
 *               It is a singleton class
 */

#region USING STATEMENTS
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
#endregion

namespace HelProject.Tools
{
    public class InputManager
    {
        #region ATTRIBUTES
        private static InputManager _instance;
        private KeyboardState _kbState;
        private List<Keys> _downKeys;
        private List<Keys> _pressedKeys;
        private List<Keys> _releasedKeys;
        private MouseState _msState;
        
        #endregion

        #region PROPRIETIES
        /// <summary>
        /// List of the keys that are released
        /// </summary>
        public List<Keys> ReleasedKeys
        {
            get { return _releasedKeys; }
            private set { _releasedKeys = value; }
        }

        /// <summary>
        /// List of the keys that are at a down state
        /// </summary>
        public List<Keys> DownKeys
        {
            get { return _downKeys; }
            private set { _downKeys = value; }
        }

        /// <summary>
        /// List of the keys that are at a pressed state
        /// </summary>
        public List<Keys> PressedKeys
        {
            get { return _pressedKeys; }
            private set { _pressedKeys = value; }
        }

        /// <summary>
        /// Current keyboard state
        /// </summary>
        public KeyboardState KbState
        {
            get { return _kbState; }
            private set { _kbState = value; }
        }

        /// <summary>
        /// Current mouse state
        /// </summary>
        public MouseState MsState
        {
            get { return _msState; }
            set { _msState = value; }
        }

        /// <summary>
        /// Instance of the class
        /// </summary>
        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new InputManager();
                }
                return _instance;
            }
        }
        #endregion

        #region CONSTRUCTORS
        /// <summary>
        /// Creates an inputmanager
        /// </summary>
        private InputManager()
        {
            KbState = new KeyboardState();
            this.PressedKeys = new List<Keys>();
            this.ReleasedKeys = new List<Keys>();
            this.DownKeys = new List<Keys>();
        }
        #endregion

        #region METHODS
        /// <summary>
        /// Updates the states of the inputs
        /// </summary>
        /// <param name="gameTime"></param>
        public void Update(GameTime gameTime)
        {
            this.UpdateKeyboardInput();
            this.UpdateMouseInput();
        }

        /// <summary>
        /// Checks if a key is up.
        /// </summary>
        /// <param name="key">Key that is checked</param>
        /// <returns>Boolean</returns>
        public bool IsKeyboardKeyReleased(Keys key)
        {
            foreach (Keys k in this.ReleasedKeys)
            {
                if (key == k)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the key is down
        /// </summary>
        /// <param name="key">Key that is checked</param>
        /// <returns>Boolean</returns>
        public bool IsKeyboardKeyDown(Keys key)
        {
            foreach (Keys k in this.DownKeys)
            {
                if (key == k)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the key is pressed
        /// </summary>
        /// <param name="key">Key that is checked</param>
        /// <returns>Boolean</returns>
        public bool IsKeyboardKeyPressed(Keys key)
        {
            foreach (Keys k in this.PressedKeys)
            {
                if (key == k)
                    return true;
            }
            return false;
        }
        #endregion

        #region PRIVATE METHODS
        /// <summary>
        /// Updates the states of the keyboard
        /// </summary>
        private void UpdateKeyboardInput()
        {
            KbState = Keyboard.GetState();
            
            // Verifies all the keys of the keyboard
            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                // If the key is not pressed and it is not in the release key list and is in one of the other two list
                // we add it to the released key list and remove it in the others
                if (KbState.IsKeyUp(key) && !this.IsKeyboardKeyReleased(key) &&
                    (this.IsKeyboardKeyPressed(key) || this.IsKeyboardKeyDown(key)))
                {
                    this.DownKeys.Remove(key);
                    this.PressedKeys.Remove(key);
                    this.ReleasedKeys.Add(key);
                }
                else if (KbState.IsKeyUp(key) && this.IsKeyboardKeyReleased(key)) // If it is already in the released key list
                {                                                                 // remove it
                    this.ReleasedKeys.Remove(key);
                }
                else
                {
                    // If the key is down and is not yet pressed
                    if (KbState.IsKeyDown(key) && !this.IsKeyboardKeyDown(key) && !this.IsKeyboardKeyPressed(key))
                    {
                        // remove it from the other lists (to be sure)
                        this.ReleasedKeys.Remove(key);
                        this.PressedKeys.Remove(key);
                        this.DownKeys.Add(key); // and add it to the down key list
                    }
                    else if (KbState.IsKeyDown(key) && !this.IsKeyboardKeyPressed(key)) // If it's already in the down key list
                    {                                                                   // and isn't in the pressed list
                        this.DownKeys.Remove(key); // remove it from the other lists
                        this.ReleasedKeys.Remove(key);
                        this.PressedKeys.Add(key); // add it to the pressed list
                    }
                }
            }
        }

        /// <summary>
        /// Updates the states of the mouse
        /// </summar>
        private void UpdateMouseInput()
        {
            this.MsState = Mouse.GetState();
        }
        #endregion
    }
}

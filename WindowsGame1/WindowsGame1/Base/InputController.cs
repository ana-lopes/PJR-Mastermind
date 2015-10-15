using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public delegate void KeyEventHandler(float arg);

    public enum MouseButtons { LeftButton, MiddleButton, RightButton, XButton1, XButton2 }
    public enum MouseValues { ScrollWheel, X, Y }
    public enum GamePadThumbsticks { LeftX, LeftY, RightX, RightY }

    class InputController
    {
        private bool keyboard, mouse, gamepad;
        private MouseState msa;
        private KeyboardState ksa;
        private GamePadState gpsa;

        private Dictionary<Keys, KeyEventHandler> keyPressed = new Dictionary<Keys,KeyEventHandler>();
        private Dictionary<Keys, KeyEventHandler> keyReleased = new Dictionary<Keys, KeyEventHandler>();

        private Dictionary<Buttons, KeyEventHandler> buttonPressed = new Dictionary<Buttons, KeyEventHandler>();
        private Dictionary<Buttons, KeyEventHandler> buttonReleased = new Dictionary<Buttons, KeyEventHandler>();
        private Dictionary<GamePadThumbsticks, KeyEventHandler> thumbstickValueChanged = new Dictionary<GamePadThumbsticks, KeyEventHandler>();
        
        private Dictionary<MouseButtons, KeyEventHandler> mouseButtonPressed = new Dictionary<MouseButtons, KeyEventHandler>();
        private Dictionary<MouseButtons, KeyEventHandler> mouseButtonReleased = new Dictionary<MouseButtons, KeyEventHandler>();
        private Dictionary<MouseValues, KeyEventHandler> mouseValueChanged = new Dictionary<MouseValues, KeyEventHandler>();

        public InputController(bool keyboard, bool mouse, bool gamepad)
        {
            this.keyboard = keyboard; this.mouse = mouse; this.gamepad = gamepad;

            if (keyboard)
            {
                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    keyPressed.Add(key, delegate { });
                    keyReleased.Add(key, delegate { });
                }
            }

            if (gamepad)
            {
                foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
                {
                    buttonPressed.Add(button, delegate { });
                    buttonReleased.Add(button, delegate { });
                }

                foreach (GamePadThumbsticks tb in Enum.GetValues(typeof(GamePadThumbsticks)))
                {
                    thumbstickValueChanged.Add(tb, delegate { });
                }
            }

            if (mouse)
            {
                foreach (MouseButtons button in Enum.GetValues(typeof(MouseButtons)))
                {
                    mouseButtonPressed.Add(button, delegate { });
                    mouseButtonReleased.Add(button, delegate { });
                }

                foreach (MouseValues value in Enum.GetValues(typeof(MouseValues)))
                {
                    mouseValueChanged.Add(value, delegate { });
                }
            }
        }

        public void Update()
        {
            if (keyboard)
            {
                KeyboardState ks = Keyboard.GetState();
                foreach (Keys key in Enum.GetValues(typeof(Keys)))
                {
                    if (ks.IsKeyDown(key) != ksa.IsKeyDown(key))
                    {
                        if (ks.IsKeyDown(key))
                            keyPressed[key](0);
                        else
                            keyReleased[key](0);
                    }
                }
                ksa = ks;
            }

            if (gamepad)
            {
                GamePadState gps = GamePad.GetState(Microsoft.Xna.Framework.PlayerIndex.One);
                foreach(Buttons button in Enum.GetValues(typeof(Buttons)))
                {
                    if (gps.IsButtonDown(button) != gpsa.IsButtonDown(button))
                    {
                        if (gps.IsButtonDown(button))
                            buttonPressed[button](0);
                        else
                            buttonReleased[button](0);
                    }
                }

                if (gps.ThumbSticks.Left.X != gpsa.ThumbSticks.Left.X)
                {
                    thumbstickValueChanged[GamePadThumbsticks.LeftX](gps.ThumbSticks.Left.X - gpsa.ThumbSticks.Left.X);
                }
                if (gps.ThumbSticks.Left.Y != gpsa.ThumbSticks.Left.Y)
                {
                    thumbstickValueChanged[GamePadThumbsticks.LeftY](gps.ThumbSticks.Left.Y - gpsa.ThumbSticks.Left.Y);
                }
                if (gps.ThumbSticks.Right.X != gpsa.ThumbSticks.Right.X)
                {
                    thumbstickValueChanged[GamePadThumbsticks.RightX](gps.ThumbSticks.Right.X - gpsa.ThumbSticks.Right.X);
                }
                if (gps.ThumbSticks.Right.Y != gpsa.ThumbSticks.Right.Y)
                {
                    thumbstickValueChanged[GamePadThumbsticks.RightY](gps.ThumbSticks.Right.Y - gpsa.ThumbSticks.Right.Y);
                }
                gpsa = gps;
            }

            if (mouse)
            {
                MouseState ms = Mouse.GetState();
                if (ms.LeftButton != msa.LeftButton)
                {
                    if (ms.LeftButton == ButtonState.Pressed)
                        mouseButtonPressed[MouseButtons.LeftButton](0);
                    else
                        mouseButtonReleased[MouseButtons.LeftButton](0);
                }
                if (ms.MiddleButton != msa.MiddleButton)
                {
                    if (ms.MiddleButton == ButtonState.Pressed)
                        mouseButtonPressed[MouseButtons.MiddleButton](0);
                    else
                        mouseButtonReleased[MouseButtons.MiddleButton](0);
                }
                if (ms.RightButton != msa.RightButton)
                {
                    if (ms.RightButton == ButtonState.Pressed)
                        mouseButtonPressed[MouseButtons.RightButton](0);
                    else
                        mouseButtonReleased[MouseButtons.RightButton](0);
                }
                if (ms.XButton1 != msa.XButton1)
                {
                    if (ms.XButton1 == ButtonState.Pressed)
                        mouseButtonPressed[MouseButtons.XButton1](0);
                    else
                        mouseButtonReleased[MouseButtons.XButton1](0);
                }
                if (ms.XButton2 != msa.XButton2)
                {
                    if (ms.XButton2 == ButtonState.Pressed)
                        mouseButtonPressed[MouseButtons.XButton2](0);
                    else
                        mouseButtonReleased[MouseButtons.XButton2](0);
                }

                if (ms.ScrollWheelValue != msa.ScrollWheelValue)
                {
                    mouseValueChanged[MouseValues.ScrollWheel](ms.ScrollWheelValue - msa.ScrollWheelValue);
                }
                if (ms.X != msa.X)
                {
                    mouseValueChanged[MouseValues.X](ms.X - msa.X);
                }
                if (ms.Y != msa.Y)
                {
                    mouseValueChanged[MouseValues.Y](ms.Y - msa.Y);
                }

                msa = ms;
            }
        }

        public void RegistarListener(Keys key, KeyEventHandler handler, KeyState keyState)
        {
            if (keyState == KeyState.Down)
                keyPressed[key] += handler;
            else
                keyReleased[key] += handler;
        }

        public void RegistarListener(Buttons button, KeyEventHandler handler, ButtonState buttonState)
        {
            if (buttonState == ButtonState.Pressed)
                buttonPressed[button] += handler;
            else
                buttonReleased[button] += handler;
        }

        public void RegistarListener(MouseButtons button, KeyEventHandler handler, ButtonState buttonState)
        {
            if (buttonState == ButtonState.Pressed)
                mouseButtonPressed[button] += handler;
            else
                mouseButtonReleased[button] += handler;
        }

        internal void RegistarListener(MouseValues value, KeyEventHandler handler)
        {
            mouseValueChanged[value] += handler;
        }

        internal void RegistarListener(GamePadThumbsticks tb, KeyEventHandler handler)
        {
            thumbstickValueChanged[tb] += handler;
        }
    }
}

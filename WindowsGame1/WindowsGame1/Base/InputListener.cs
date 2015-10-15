using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public enum InputListenerClass { Key, Button, MouseButton, MouseValue, ThumbstickValue}
    class InputListener
    {
        private InputListenerClass classe;
        private KeyState keyState;
        private ButtonState buttonState;
        public Action<float> updateAction;
        
        /*InputListener il = new InputListener(Keys.A, ic)
        {            
            updateAction = (arg) =>
            {
                //do your xtuffz herezz... :D
            }
        };*/

        public InputListener(Keys key, InputController ic, KeyState keyState)
        {
            ic.RegistarListener(key, new KeyEventHandler(UpdateMethod), keyState);
            classe = InputListenerClass.Key;
            this.keyState = keyState;
        }

        public InputListener(Buttons button, InputController ic, ButtonState buttonState)
        {
            ic.RegistarListener(button, new KeyEventHandler(UpdateMethod), buttonState);
            classe = InputListenerClass.Button;
            this.buttonState = buttonState;
        }

        public InputListener(MouseButtons button, InputController ic, ButtonState buttonState)
        {
            ic.RegistarListener(button, new KeyEventHandler(UpdateMethod), buttonState);
            classe = InputListenerClass.MouseButton;
            this.buttonState = buttonState;
        }

        public InputListener(MouseValues value, InputController ic)
        {
            ic.RegistarListener(value, new KeyEventHandler(UpdateMethod));
            classe = InputListenerClass.MouseValue;
        }

        public InputListener(GamePadThumbsticks tb, InputController ic)
        {
            ic.RegistarListener(tb, new KeyEventHandler(UpdateMethod));
            classe = InputListenerClass.ThumbstickValue;
        }

        private void UpdateMethod(float arg)
        {
            updateAction(arg);
        }
    }
}

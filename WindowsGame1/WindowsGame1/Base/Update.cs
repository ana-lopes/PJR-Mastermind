using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public class Update
    {
        private Action<float> updateAction;
        private float elapsed = 0;

        public void UpdateMe(float delta)
        {
            elapsed += delta;
            updateAction(delta);
        }

        /*Update up = new Update()
            {
                UpdateAction = (delta1) =>
                {
                    //do your xtuffz herezz... :D
                }
            };*/
        public Action<float> UpdateAction
        {
            set { updateAction = value; }
        }

        public float Elapsed
        {
            get { return elapsed; }
        }
    }
}

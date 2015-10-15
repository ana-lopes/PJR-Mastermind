using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class MyGame : Game
    {
        public static MyGame instance;
        private Cena cenaAtual;
        public string name = "lol, XNA e Forms";

        protected override void LoadContent()
        {
            instance = this;
            cenaAtual = SplashScene.instance = new SplashScene(4, 3);
        }

        protected override void Update(TimeSpan delta)
        {
            cenaAtual.Update((float)delta.TotalSeconds);
        }

        protected override void Render()
        {
            cenaAtual.Render();
        }

        public Cena CenaAtual
        {
            set
            {
                cenaAtual.Hide();
                cenaAtual = value;
                cenaAtual.Show();
            }
        }
    }
}

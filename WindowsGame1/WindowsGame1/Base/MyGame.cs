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

        public bool gaming;
        public int player;

        public static string name = "Mastermind";

        protected override void LoadContent()
        {
            instance = this;
            cenaAtual = SplashScene.instance = new SplashScene();
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

        public void Start1()
        {
            gaming = true;
            this.player = 1;
            MainMenu.instance.tabuleiro.Reset();
        }

        public void Start2()
        {
            gaming = true;
            this.player = 2;
            MainMenu.instance.tabuleiro.Reset();
        }
    }
}

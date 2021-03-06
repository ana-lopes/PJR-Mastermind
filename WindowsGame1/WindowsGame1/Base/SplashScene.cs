﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WindowsGame1
{
    public class SplashScene : Cena
    {
        public static SplashScene instance;
        private Texture2D splashTexture;
        private Animacao splash;

        public SplashScene()
            : base()
        {

        }

        protected override void CriarCena()
        {
            splashTexture = MyGame.instance.Content.Load<Texture2D>("Splash");
            splash = new Animacao(splashTexture,
                new Vector2(MyGame.instance.GraphicsDevice.Viewport.Width / 2,
                    MyGame.instance.GraphicsDevice.Viewport.Height / 2), 6, 2, 0.1f, this);
            RegistarAnimacao(splash, 0);
            Thread thread = new Thread(CreateMainMenu);
            thread.Start();
        }

        private void CreateMainMenu()
        {
            Assets.Load(); 
            MainMenu.instance = new MainMenu();
        }

        protected override void Atuar(float delta)
        {
            if (!Assets.IsLoading() && MyGame.instance.gaming)
            {
                MyGame.instance.CenaAtual = MainMenu.instance;
                this.Hide();
            }
        }

        protected override void Desenhar()
        {

        }

        public override void Hide()
        {

        }

        public override void Show()
        {

        }

        public override void Dispose()
        {
            splashTexture.Dispose();
        }
    }
}

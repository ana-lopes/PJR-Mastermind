using Microsoft.Xna.Framework;
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
        private Thread thread;
        private float minSplashTime, splashAnimTime;

        public SplashScene(float minSplashTime, float splashAnimTime)
            : base()
        {
            this.minSplashTime = minSplashTime;
            this.splashAnimTime = splashAnimTime;
        }

        protected override void CriarCena()
        {
            splashTexture = MyGame.instance.Content.Load<Texture2D>("Splash");
            splash = new Animacao(splashTexture,
                new Vector2(MyGame.instance.GraphicsDevice.Viewport.Width / 2,
                    MyGame.instance.GraphicsDevice.Viewport.Height / 2), 2, 1, 0.1f, this);
            RegistarAnimacao(splash, 0);
            thread = new Thread(Assets.Load);
            thread.Start();
        }

        protected override void Atuar(float delta)
        {
            if (!Assets.IsLoading() && minSplashTime < 0)
            {
                //Game1.instance.SetCena(MainMenu.instance = new MainMenu());
                this.Hide();
            }
            else
            {
                if (splashAnimTime > 0)
                    splashAnimTime -= delta;
                else
                    paused = true;
                minSplashTime -= delta;
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

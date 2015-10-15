using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Windows.Forms;

namespace WindowsGame1
{
    abstract class Game : GraphicsDeviceControl
    {
        private ContentManager content;
        private SpriteBatch spriteBatch;
        private TimeSpan timespan;

        protected override void Initialize()
        {
            content = new ContentManager(Services, "Content");
            spriteBatch = new SpriteBatch(GraphicsDevice);
            LoadContent();
            Application.Idle += delegate { Invalidate(); };
        }
        
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                content.Unload();
            }

            base.Dispose(disposing);
        }

        protected override void Draw()
        {
            TimeSpan delta = Program.timer.Elapsed - timespan;
            timespan = Program.timer.Elapsed;

            Update(delta);

            GraphicsDevice.Clear(Color.CornflowerBlue);
            
            spriteBatch.Begin();
            Render();

            spriteBatch.End();            
        }
        protected abstract void LoadContent();

        protected abstract void Update(TimeSpan delta);

        protected abstract void Render();

        public ContentManager Content
        {
            get { return content; }
        }

        public SpriteBatch SpriteBatch
        {
            get { return spriteBatch; }
        }
    }
}

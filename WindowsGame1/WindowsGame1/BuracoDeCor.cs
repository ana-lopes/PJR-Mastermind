using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    internal class BuracoDeCor : Animacao
    {
        const float escalaX = 3 / 20f;
        const float escalaY = 1 / 13f;
        public ColorName colorName = ColorName.Empty;

        public BuracoDeCor(Cena cena, float posicaoX, float posicaoY)
            : base(Assets.buraco, new Vector2(posicaoX * MyGame.instance.GraphicsDevice.Viewport.Width, 
                posicaoY * MyGame.instance.GraphicsDevice.Viewport.Height), cena)
        {
            escala = new Vector2(escalaX * MyGame.instance.GraphicsDevice.Viewport.Width / frameSize.X,
                escalaY * MyGame.instance.GraphicsDevice.Viewport.Height / frameSize.Y);
        }
    }
}
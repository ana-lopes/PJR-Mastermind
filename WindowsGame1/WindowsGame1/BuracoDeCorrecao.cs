using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    internal class BuracoDeCorrecao : Animacao
    {
        const float escalaX = 3 / 40f;
        const float escalaY = 1 / 26f;
        public ColorName colorName = ColorName.Empty;

        public BuracoDeCorrecao(Cena cena, float posicaoX, float posicaoY) : 
            base(Assets.buraco, new Vector2(posicaoX * MyGame.instance.GraphicsDevice.Viewport.Width,
                posicaoY * MyGame.instance.GraphicsDevice.Viewport.Height), cena)
        {
            escala = new Vector2(escalaX * MyGame.instance.GraphicsDevice.Viewport.Width / frameSize.X,
                escalaY * MyGame.instance.GraphicsDevice.Viewport.Height / frameSize.Y);
            color = Color.CornflowerBlue;
        }

        internal void Reset()
        {
            color = Color.CornflowerBlue;
        }
    }
}
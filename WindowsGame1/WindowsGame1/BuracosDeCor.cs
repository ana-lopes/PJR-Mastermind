using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    internal class BuracosDeCor : HUD
    {
        public BuracosDeCor(Cena cena, float escalaX, float escalaY, float posicaoX, float posicaoY)
            : base(Assets.buraco, cena, escalaX, escalaY, posicaoX, posicaoY)
        {
        }
    }
}
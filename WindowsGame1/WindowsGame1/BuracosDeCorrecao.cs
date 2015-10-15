using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    internal class BuracosDeCorrecao : HUD
    {
        public BuracosDeCorrecao(Cena cena, float escalaX, float escalaY) : base(Assets.buraco, cena, escalaX, escalaY, 0, 0)
        {
        }
    }
}
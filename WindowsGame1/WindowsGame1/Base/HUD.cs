using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class HUD : Animacao
    {
        private float escalaEcraX, escalaEcraY, posicaoEcraX, posicaoEcraY;

        public HUD(Texture2D textura, Cena cena,
            float escalaEcraX, float escalaEcraY, float posicaoEcraX, float posicaoEcraY) :
            this(textura, 1, 1, 0, cena, escalaEcraX, escalaEcraY, posicaoEcraX, posicaoEcraY)
        { }

        public HUD(Texture2D textura, int colunas, int linhas, float frameDuration,
            Cena cena, float escalaEcraX, float escalaEcraY, float posicaoEcraX, float posicaoEcraY) :
            base(textura, Vector2.Zero, colunas, linhas, frameDuration, cena)
        {
            GameForm.instance.SplitContainer.Panel2.Resize += new EventHandler(OnPanelResize);
            this.escalaEcraX = escalaEcraX;
            this.escalaEcraY = escalaEcraY;
            this.posicaoEcraX = posicaoEcraX;
            this.posicaoEcraY = posicaoEcraY;
            OnPanelResize(null, null);
        }

        private void OnPanelResize(object obj, EventArgs args)
        {
            SetCenter(new Vector2(
                GameForm.instance.SplitContainer.Panel2.Width * posicaoEcraX,
                GameForm.instance.SplitContainer.Panel2.Height * posicaoEcraY));
            escala.X = GameForm.instance.SplitContainer.Panel2.Width * escalaEcraX / frameSize.X;
            escala.Y = GameForm.instance.SplitContainer.Panel2.Height * escalaEcraY / frameSize.Y;
        }

        public override void Desenhar()
        {
            spriteBatch.Draw(textura,
                new Rectangle(
                    (int)(posicao.X - cena.camaraAtual.Posicao.X + origem.X),
                    (int)(posicao.Y - cena.camaraAtual.Posicao.Y + origem.Y),
                    (int)(frameSize.X * escala.X),
                    (int)(frameSize.Y * escala.Y)),
                new Rectangle(
                    (int)(currentFrame.X * frameSize.X),
                    (int)(currentFrame.Y * frameSize.Y),
                    (int)(frameSize.X),
                    (int)(frameSize.Y)),
                Color.White, rotacao - cena.camaraAtual.rotacao, origem, spriteEffects, 0);
        }

        public override bool ContainsPointOnScreen(Point point)
        {
            Rectangle rec = new Rectangle(
                    (int)(posicao.X - cena.camaraAtual.Posicao.X + origem.X),
                    (int)(posicao.Y - cena.camaraAtual.Posicao.Y + origem.Y),
                    (int)(frameSize.X * escala.X),
                    (int)(frameSize.Y * escala.Y));
            return rec.Contains(point);
        }
    }
}

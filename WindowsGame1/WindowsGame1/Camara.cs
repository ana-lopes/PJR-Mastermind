using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public class Camara
    {
        private Rectangle moldura;
        public float rotacao;

        public Camara(int pX, int pY, int largura, int altura)
        {
            moldura = new Rectangle(pX, pY, largura, altura);
        }

        public Vector2 Posicao
        {
            get { return new Vector2(moldura.X, moldura.Y); }
        }

        public int Width
        {
            get { return moldura.Width; }
        }

        public int Height
        {
            get { return moldura.Height; }
        }
    }
}

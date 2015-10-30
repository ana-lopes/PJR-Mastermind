using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public enum ColorName { Red, Green, Blue, Yellow, Pink, Cyan, Empty, Branco, Preto }
    class Fila : Animacao
    {
        BuracoDeCor[] cores = new BuracoDeCor[4];
        BuracoDeCorrecao[,] correcao = new BuracoDeCorrecao[2, 2];

        public int indexCor, indexCorrecao;

        private bool sequence = false;

        const float escalaX = 3 / 4f;
        const float escalaY = 1 / 13f;
        const float posicaoX = 1 / 2f;
        
        public Fila(Cena cena, int indice) : base(Assets.fila, 
            new Vector2(posicaoX * MyGame.instance.GraphicsDevice.Viewport.Width, 
                (1 + indice)/13f * MyGame.instance.GraphicsDevice.Viewport.Height), cena)
        {
            indexCor = 0;
            indexCorrecao = 0;
            escala = new Vector2(escalaX * MyGame.instance.GraphicsDevice.Viewport.Width / frameSize.X,
                escalaY * MyGame.instance.GraphicsDevice.Viewport.Height / frameSize.Y);
            if (indice != 11)
            {
                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < 2; j++)
                    {
                        correcao[i, j] = new BuracoDeCorrecao(cena,
                            posicaoX + escalaX * (7 + 2 * j) / 20,
                            (3 + i * 2 + indice * 4) / 52f);
                        cena.RegistarAnimacao(correcao[i, j], 2);
                    }
                }
            }
            else sequence = true;

            for (int i = 0; i < 4; i++)
            {
                cores[i] = new BuracoDeCor(cena,
                    1 / 8f + 3 / 40f + (i * 3 / 20f),
                    (1 + indice) / 13f);
                cena.RegistarAnimacao(cores[i], 3);

                if (indice == 11 && MyGame.instance.player == 2)
                    this.textura = null;
            }
        }

        public void PorCor(ColorName color)
        {
            if (color == ColorName.Blue)
                PorCor(Color.Blue);
            else if (color == ColorName.Cyan)
                PorCor(Color.Cyan);
            else if (color == ColorName.Green)
                PorCor(Color.Green);
            else if (color == ColorName.Pink)
                PorCor(Color.Pink);
            else if (color == ColorName.Red)
                PorCor(Color.Red);
            else if (color == ColorName.Yellow)
                PorCor(Color.Yellow);
        }

        public void PorCor(Color color)
        {            
            if (indexCor < 4)
            {
                bool exists = false;
                foreach (BuracoDeCor b in cores)
                {
                    if (b.color == color)
                    {
                        exists = true;
                        break;
                    }
                }
                if (!exists)
                {
                    if (color == Color.Red)
                        cores[indexCor].colorName = ColorName.Red;
                    else if (color == Color.Blue)
                        cores[indexCor].colorName = ColorName.Blue;
                    else if (color == Color.Green)
                        cores[indexCor].colorName = ColorName.Green;
                    else if (color == Color.Pink)
                        cores[indexCor].colorName = ColorName.Pink;
                    else if (color == Color.Yellow)
                        cores[indexCor].colorName = ColorName.Yellow;
                    else if (color == Color.Cyan)
                        cores[indexCor].colorName = ColorName.Cyan;
                    cores[indexCor].color = color;
                    indexCor++;
                }
            }
        }

        public void PorCorrecao(ColorName color)
        {
            if (color == ColorName.Branco)
                PorCorrecao(Color.White);
            else if (color == ColorName.Preto)
                PorCorrecao(Color.Black);
        }

        public void PorCorrecao(Color color)
        {
            if (indexCorrecao < 4)
            {
                if (color == Color.White)
                    correcao[indexCorrecao/2, indexCorrecao%2].colorName = ColorName.Branco;
                else if (color == Color.Black)
                    correcao[indexCorrecao / 2, indexCorrecao % 2].colorName = ColorName.Preto;
                correcao[indexCorrecao/2, indexCorrecao % 2].color = color;
                indexCorrecao++;
            }
        }

        public void TirarCor()
        {
            if (indexCor > 0)
            {
                cores[indexCor - 1].color = Color.White;
                cores[indexCor - 1].colorName = ColorName.Empty;
                indexCor--;
            }
        }

        public void TirarCorrecao()
        {
            if (indexCorrecao > 0)
            {
                correcao[(indexCorrecao - 1)/2, (indexCorrecao - 1) % 2].color = Color.CornflowerBlue;
                correcao[(indexCorrecao - 1) / 2, (indexCorrecao - 1) % 2].colorName = ColorName.Empty;
                indexCorrecao--;
            }
        }

        public string GetColorSequence(int i)
        {
            return cores[i].colorName.ToString();
        }

        public string GetCorrectionSequence(int i)
        {
            return correcao[i / 2, i % 2].colorName.ToString();
        }

        internal void Reset()
        {
            foreach(BuracoDeCor b in cores)
            {
                b.Reset();
            }

            if (!sequence)
            {
                foreach (BuracoDeCorrecao b in correcao)
                {
                    b.Reset();
                }
            }

            indexCor = indexCorrecao = 0;
        }
    }
}

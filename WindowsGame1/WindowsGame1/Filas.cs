using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Fila : Animacao
    {
        BuracoDeCor[] cores = new BuracoDeCor[4];
        BuracoDeCorrecao[,] correcao = new BuracoDeCorrecao[2, 2];

        static float escalaX = 3 / 4f;
        static float escalaY = 1 / 13f;
        static float posicaoX = 1 / 2f;
        
        public Fila(Cena cena, int indice) : base(Assets.fila, 
            new Vector2(posicaoX * MyGame.instance.GraphicsDevice.Viewport.Width, 
                (1 + indice)/13f * MyGame.instance.GraphicsDevice.Viewport.Height), cena)
        {
            escala = new Vector2(escalaX * MyGame.instance.GraphicsDevice.Viewport.Width / frameSize.X,
                escalaY * MyGame.instance.GraphicsDevice.Viewport.Height / frameSize.Y);
            if (indice != 11 || MyGame.instance.player != 2)
            {
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

                for (int i = 0; i < 4; i++)
                {
                    cores[i] = new BuracoDeCor(cena,
                        1 / 8f + 3 / 40f + (i * 3 / 20f),
                        (1 + indice) / 13f);
                    cena.RegistarAnimacao(cores[i], 3);
                }
            }
        }
    }
}

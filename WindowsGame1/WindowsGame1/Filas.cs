using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class Filas : HUD
    {
        BuracosDeCor[] cores = new BuracosDeCor[4];
        BuracosDeCorrecao[,] correcao = new BuracosDeCorrecao[2, 2];

        static float escalaX = 3 / 4f;
        static float escalaY = 1 / 13f;
        static float posicaoX = 1 / 2f;
        
        public Filas(Cena cena, int indice) : base(Assets.fila, cena, escalaX, escalaY, posicaoX, (1 + indice)/13f)
        {
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 2; j++)
                {
                    correcao[i, j] = new BuracosDeCorrecao(cena, escalaX / 5 / 2, escalaY / 2, 
                        posicaoX + escalaX * (7 + 2*j) / 20, 
                        (3 + i * 2 + indice * 4) / 52f);
                    cena.RegistarAnimacao(correcao[i, j], 2);
                }
            }

            for (int i = 0; i < 4; i++)
            {
                cores[i] = new BuracosDeCor(cena, escalaX / 5, escalaY, 
                    posicaoX + (-2 + i) * escalaX / 5, 
                    (1 + indice)/13f);
                cena.RegistarAnimacao(cores[i], 3);
            }
        }
    }
}

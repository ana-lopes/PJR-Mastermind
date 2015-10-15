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

        }
    }
}

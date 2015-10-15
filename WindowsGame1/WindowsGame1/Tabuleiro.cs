using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Tabuleiro : HUD
    {
        Filas[] filas = new Filas[10];

        public Tabuleiro(Cena cena) : base(Assets.wood, cena, 1, 1, 0.5f, 0.5f)
        {
            for (int i = 0; i < 10; i++)
            {
                filas[i] = new Filas(cena, i);
                cena.RegistarAnimacao(filas[i], 1);
            }
        }

        
    }
}

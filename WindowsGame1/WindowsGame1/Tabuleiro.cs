using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace WindowsGame1
{
    class Tabuleiro : Animacao
    {
        Filas[] filas = new Filas[10];
        Filas sequencia;

        public Tabuleiro(Cena cena) : base(Assets.wood, new Vector2(0.5f * MyGame.instance.GraphicsDevice.Viewport.Width, 
            0.5f * MyGame.instance.GraphicsDevice.Viewport.Height), cena)
        {
            for (int i = 0; i < 10; i++)
            {
                filas[i] = new Filas(cena, i);
                cena.RegistarAnimacao(filas[i], 1);
            }
            sequencia = new Filas(cena, 11);
            escala = new Vector2(MyGame.instance.GraphicsDevice.Viewport.Width / frameSize.X,
                MyGame.instance.GraphicsDevice.Viewport.Height / frameSize.Y);
        }

        
    }
}

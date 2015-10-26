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
        Fila[] filas = new Fila[10];
        Fila sequencia;

        public Tabuleiro(Cena cena) : base(Assets.wood, new Vector2(0.5f * MyGame.instance.GraphicsDevice.Viewport.Width, 
            0.5f * MyGame.instance.GraphicsDevice.Viewport.Height), cena)
        {
            for (int i = 0; i < 10; i++)
            {
                filas[i] = new Fila(cena, i);
                cena.RegistarAnimacao(filas[i], 1);
            }
            sequencia = new Fila(cena, 11);
            cena.RegistarAnimacao(sequencia, 1);
            escala = new Vector2(MyGame.instance.GraphicsDevice.Viewport.Width / frameSize.X,
                MyGame.instance.GraphicsDevice.Viewport.Height / frameSize.Y);
        }

        
    }
}

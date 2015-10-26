using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public class Animacao
    {
        protected SpriteBatch spriteBatch;
        protected Cena cena;

        protected Texture2D textura;
        protected Vector2 posicao, origem, escala;
        protected float rotacao;
        protected SpriteEffects spriteEffects;

        protected int colunas, linhas;
        protected Vector2 frameSize, currentFrame;
        protected float frameDuration, time;
        public Color color = Color.White;
        private bool updatable;

        public Animacao(Texture2D textura, Vector2 posicao, Cena cena) : this(textura, posicao, 1, 1, 0, cena)
        {

        }

        public Animacao(Texture2D textura, Vector2 posicao, int colunas, int linhas, float frameDuration, Cena cena)
        {
            this.textura = textura;
            this.colunas = colunas;
            if (colunas > 1 || frameDuration <= 0)
                updatable = true;
            this.linhas = linhas;
            frameSize = new Vector2(textura.Width / colunas, textura.Height / linhas);
            origem = frameSize / 2;
            escala = Vector2.One;
            SetCenter(posicao);
            this.frameDuration = frameDuration;
            this.spriteBatch = MyGame.instance.SpriteBatch;
            this.cena = cena;
        }

        public void SetCenter(Vector2 v)
        {
            posicao.X = v.X - frameSize.X / 2;
            posicao.Y = v.Y - frameSize.Y / 2;
        }

        virtual public void Update(float delta)
        {
            if (updatable)
            {
                time += delta;
                if (time >= frameDuration)
                {
                    time -= frameDuration;
                    currentFrame.X++;
                    if (currentFrame.X >= colunas)
                        currentFrame.X = 0;
                }
            }
        }

        public void MudarAnimacao(int linha)
        {
            currentFrame.Y = linha;
            time = 0;
        }

        virtual public void Desenhar()
        {
            spriteBatch.Draw(textura,
                new Rectangle(
                    (int)((posicao.X - cena.camaraAtual.Posicao.X + origem.X) * MyGame.instance.GraphicsDevice.Viewport.Width / cena.camaraAtual.Width),
                    (int)((posicao.Y - cena.camaraAtual.Posicao.Y + origem.Y) * MyGame.instance.GraphicsDevice.Viewport.Height / cena.camaraAtual.Height),
                    (int)(frameSize.X * escala.X) * MyGame.instance.GraphicsDevice.Viewport.Width / cena.camaraAtual.Width,
                    (int)(frameSize.Y * escala.Y) * MyGame.instance.GraphicsDevice.Viewport.Height / cena.camaraAtual.Height),
                new Rectangle(
                    (int)(currentFrame.X * frameSize.X),
                    (int)(currentFrame.Y * frameSize.Y),
                    (int)(frameSize.X),
                    (int)(frameSize.Y)),
                color, rotacao - cena.camaraAtual.rotacao, origem, spriteEffects, 0);
        }

        virtual public bool ContainsPointOnScreen(Point point)
        {
            Rectangle rec = new Rectangle(
                    (int)((posicao.X - cena.camaraAtual.Posicao.X + origem.X) * MyGame.instance.GraphicsDevice.Viewport.Width / cena.camaraAtual.Width),
                    (int)((posicao.Y - cena.camaraAtual.Posicao.Y + origem.Y) * MyGame.instance.GraphicsDevice.Viewport.Height / cena.camaraAtual.Height),
                    (int)(frameSize.X * escala.X) * MyGame.instance.GraphicsDevice.Viewport.Width / cena.camaraAtual.Width,
                    (int)(frameSize.Y * escala.Y) * MyGame.instance.GraphicsDevice.Viewport.Height / cena.camaraAtual.Height);
            return rec.Contains(point);
        }
    }
}

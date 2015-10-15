using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    public abstract class Cena
    {
        protected SpriteBatch spriteBatch;
        GraphicsDevice graphicsDevice;
        Color clearColor = Color.CornflowerBlue;

        private List<Update> updates, updatesRemover, updatesAdicionar;
        private SortedDictionary<int, List<Animacao>> anim, animRemover, animAdicionar;

        protected bool paused;

        public Camara camaraAtual;

        InputController inputController;

        public Cena()
        {
            this.spriteBatch = MyGame.instance.SpriteBatch;
            this.graphicsDevice = MyGame.instance.GraphicsDevice;
            updates = new List<Update>();
            updatesRemover = new List<Update>();
            updatesAdicionar = new List<Update>();
            anim = new SortedDictionary<int, List<Animacao>>();
            animRemover = new SortedDictionary<int, List<Animacao>>();
            animAdicionar = new SortedDictionary<int, List<Animacao>>();
            camaraAtual = new Camara(0, 0, graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            inputController = new InputController(true, true, true);
            CriarCena();
        }

        protected abstract void CriarCena();

        protected abstract void Atuar(float delta); //update específico da cena

        protected abstract void Desenhar(); //draw específico da cena

        public void Update(float delta)
        {            
            inputController.Update();
            Atuar(delta);
            if (!paused)
            {
                foreach (Update u in updates) u.UpdateMe(delta);
                foreach (Update u in updatesRemover) updates.Remove(u);
                updatesRemover.Clear();
                foreach (Update u in updatesAdicionar) updates.Add(u);
                updatesAdicionar.Clear();

                foreach (KeyValuePair<int, List<Animacao>> camada in anim)
                {
                    foreach (Animacao animacao in anim[camada.Key])
                        animacao.Update(delta);
                }
                foreach (KeyValuePair<int, List<Animacao>> camada in animRemover)
                {
                    foreach (Animacao animacao in animRemover[camada.Key])
                        anim[camada.Key].Remove(animacao);
                    animRemover[camada.Key].Clear();
                }
                foreach (KeyValuePair<int, List<Animacao>> camada in animAdicionar)
                {
                    foreach (Animacao animacao in animAdicionar[camada.Key])
                        anim[camada.Key].Add(animacao);
                    animAdicionar[camada.Key].Clear();
                }
            }
        }

        public void Render()
        {
            Desenhar();

            foreach (KeyValuePair<int, List<Animacao>> camada in anim)
            {
                foreach (Animacao animacao in anim[camada.Key])
                    animacao.Desenhar();
            }
        }

        public void RegistarUpdate(Update update)
        {
            updatesAdicionar.Add(update);
        }

        public void RemoverUpdate(Update update)
        {
            updatesRemover.Add(update);
        }

        public void RegistarAnimacao(Animacao animacao, int camada)
        {
            if (!animAdicionar.Keys.Contains(camada))
            {
                animAdicionar.Add(camada, new List<Animacao>());
                anim.Add(camada, new List<Animacao>());
            }
            animAdicionar[camada].Add(animacao);
        }

        public void RemoverAnimacao(Animacao animacao, int camada)
        {
            if (!animRemover.Keys.Contains(camada))
                animRemover.Add(camada, new List<Animacao>());
            animRemover[camada].Add(animacao);
        }

        public abstract void Hide();

        public abstract void Show();

        public abstract void Dispose();
    }
}

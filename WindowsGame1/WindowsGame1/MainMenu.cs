using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class MainMenu : Cena
    {
        public static MainMenu instance;
        public Tabuleiro tabuleiro;

        public override void Dispose()
        {

        }

        public override void Hide()
        {

        }

        public override void Show()
        {

        }

        protected override void Atuar(float delta)
        {
        }

        protected override void CriarCena()
        {
            tabuleiro = new Tabuleiro(this);
            RegistarAnimacao(tabuleiro, 0);
        }

        protected override void Desenhar()
        {

        }
    }
}

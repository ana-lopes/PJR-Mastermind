using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    class MainMenu : Cena
    {
        public static MainMenu instance;
        int p = 1;
        Player p1, p2;
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
            if (p == 1)
            {
                if (p1.Jogar())
                {
                    p = 2;
                }
            }
            else
            {
                if (p2.Jogar())
                {
                    p = 1;
                }
            }
        }

        protected override void CriarCena()
        {
            p1 = new PlayerLocal();
            p2 = new PlayerRemoto();

            tabuleiro = new Tabuleiro(this);
            RegistarAnimacao(tabuleiro, 0);
        }

        protected override void Desenhar()
        {

        }
    }
}

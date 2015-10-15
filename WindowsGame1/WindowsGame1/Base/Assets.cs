using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsGame1
{
    static class Assets
    {
        private static List<Texture2D> lista = new List<Texture2D>();

        private static bool loading = true;
        public static Texture2D wood;

        public static void Load()
        {
            //lista.Add(nave = MyGame.instance.content.Load<Texture2D>("Capturar"));

            lista.Add(wood = MyGame.instance.Content.Load<Texture2D>("wood"));
            loading = false;
        }

        public static bool IsLoading()
        {
            return loading;
        }

        public static void Dispose()
        {
            foreach (Texture2D t in lista)
                t.Dispose();
        }
    }
}

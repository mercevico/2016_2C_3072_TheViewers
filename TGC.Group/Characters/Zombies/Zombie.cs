using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TGC.Group.Characters.Zombies
{
    class Zombie
    {
        public int health = 100;
        public int cooldown = 2;
        public int movementSpeed = 5;
        public int atacarDanio = 10;

        private String meshPath;

        public int attack()
        {
            //Atacar
            return atacarDanio;
        }

        public void render()
        {
            //Renderizar el meshPath
        }
    }
}

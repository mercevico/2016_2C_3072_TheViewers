using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TGC.Core.Direct3D;
using TGC.Core.Example;
using TGC.Core.Geometry;
using TGC.Core.Input;
using TGC.Core.SceneLoader;
using TGC.Core.Textures;
using TGC.Core.Utils;
using TGC.Core.Collision;

using TGC.Core.SkeletalAnimation;

using TGC.Core.BoundingVolumes;

using TGC.Group.Characters.Zombies;
using TGC.Group.Characters.Plants;

namespace TGC.Group.Characters.Plants
{
    class Plant
    {
        public int health = 100;
        public int cooldown = 2;
        private String meshPath;
        public TgcMesh mesh;
        public bool muerta = false;

        public void shoot()
        {
            //Disparar
        }

        public void render()
        {
            //Renderizar el meshPath
        }

        public void crearMESH(string MediaDir)
        {
            mesh = new TgcSceneLoader().loadSceneFromFile(MediaDir).Meshes[0];
        }


    }
}

using Microsoft.DirectX;
using Microsoft.DirectX.DirectInput;
using System.Drawing;
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
using System.Collections.Generic;
using TGC.Group.Stage;
using System;

namespace TGC.Group.Characters.Plants
{
    class Plant
    {
        public int health = 100;
        public int cooldown = 2;
        private String meshPath;
        public TgcMesh plantaMesh;
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
            plantaMesh = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Planta2\\Planta2-TgcScene.xml").Meshes[0];
            plantaMesh.Position = new Vector3(12000, 3, -600);
            plantaMesh.Scale = new Vector3(25, 50, 25);

        }
        public void rendermesh()
        {
            plantaMesh.render();
            //plantaMesh.createBoundingBox();
          //  plantaMesh.BoundingBox.render();

        }
        public void disposeMesh()
        {
            plantaMesh.dispose();
           // plantaMesh.BoundingBox.dispose();

        }
    }
}

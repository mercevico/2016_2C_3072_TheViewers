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
using TGC.Group.Characters.soles;

namespace TGC.Group.Characters.Plants
{
    class Sunflower : Plant
    {
        
        public new int cooldown = 4;
        public TgcMesh plantaMeshSun;
        public Sol sunny;
        public string mymedia;
        public int i = 10000;
        public void crearMESHSun(string MediaDir)
        {
            mymedia = MediaDir;
            plantaMeshSun = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Palmera\\Palmera-TgcScene.xml").Meshes[0];
            plantaMeshSun.Position = new Vector3(-3000, 3, -1000);
            plantaMeshSun.Scale = new Vector3(10, 5, 10);
            
        }
        public void rendermeshSun()
        {

            plantaMeshSun.render();
            
            if (i == 10000)
            {
                sunny = new Sol();
                sunny.crearMESH(plantaMeshSun.Position, mymedia);

                sunny.sunnyMesh.render();
                i = 0;
              
            }
            sunny.rendermesh();
            i++;
            //plantaMesh.createBoundingBox();
            //  plantaMesh.BoundingBox.render();

        }
        public void disposeMeshSun()
        {
            plantaMeshSun.dispose();
            // plantaMesh.BoundingBox.dispose();

        }
    }
}

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

namespace TGC.Group.Characters.Peas
{
    class Peas
    {
        public TgcMesh bomba;
        public Vector3 base1;
        public int dmg = 50;
        public bool impacto = false;

        public void crearMESH(Vector3 posicion, string MediaDir)
        {
            base1 = posicion;
            bomba = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Roca\\Roca-TgcScene.xml").Meshes[0];
            bomba.Position = posicion;
            bomba.Scale = new Vector3(20, 20, 20);

        }
        public void rendermesh(bool hit)
        {
            if (hit)
            {
                bomba.dispose();
            }
            else
            { bomba.Position = base1 + new Vector3(3000, -5500, 0); bomba.render(); }
            base1 = bomba.Position;
            //sunnyMesh.rotateX(20);
            // sunnyMesh.rotateZ(20);
            bomba.render();
            //sunnyMesh.BoundingBox.render();

        }
        public void disposeMesh()
        {
            bomba.dispose();
            // plantaMesh.BoundingBox.dispose();

        }

    }

}

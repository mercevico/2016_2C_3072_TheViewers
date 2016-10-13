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

namespace TGC.Group.Characters.soles
{
    class Sol
    {
        public TgcMesh sunnyMesh;
        public Vector3 base1;

        public void crearMESH(Vector3 posicion, string MediaDir)
        {
            base1 = posicion;
            sunnyMesh = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Piedra3\\Piedra3-TgcScene.xml").Meshes[0];
            sunnyMesh.Position = posicion;
            sunnyMesh.Scale = new Vector3(40, 20, 40);
            
        }
        public void rendermesh()
        {
            if (sunnyMesh.Position.Y != 7500)
            {
                sunnyMesh.Position = sunnyMesh.Position + new Vector3(0, 5, 0);
                sunnyMesh.rotateY(20);
            }
            else
            { sunnyMesh.Position = base1 + new Vector3(3000, -5500, 0); }
            base1 = sunnyMesh.Position;
            //sunnyMesh.rotateX(20);
            // sunnyMesh.rotateZ(20);
            sunnyMesh.render();
            //sunnyMesh.BoundingBox.render();
            
        }
        public void disposeMesh()
        {
            sunnyMesh.dispose();
            // plantaMesh.BoundingBox.dispose();

        }

    }
}

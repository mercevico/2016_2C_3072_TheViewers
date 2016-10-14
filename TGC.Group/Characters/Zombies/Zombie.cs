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
using Microsoft.DirectX;
using System.Drawing;



namespace TGC.Group.Characters.Zombies
{
    class Zombie
    {
        public int health = 100;
        public int cooldown = 2;
        public int movementSpeed = 5;
        public int atacarDanio = 10;
        public TgcSkeletalMesh mesh;
        public TgcSkeletalBoneAttach attachment2;
        public float rotationY;
        public float ACU_TIEMPO_ATAQUE = 0f;
        public bool mover = true;

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

        public void crearMESH(string pathMesh, string mediaPath, Vector3 InitialPosition, float rotY)
        {
            var animationsPath = new string[1];
            animationsPath[0] = mediaPath + "Empujar" + "-TgcSkeletalAnim.xml";
            var loader1 = new TgcSkeletalLoader();
            mesh = loader1.loadMeshAndAnimationsFromFile(pathMesh, mediaPath, animationsPath);
            mesh.buildSkletonMesh();
            // new Vector3(-500, 0, -14526);
            mesh.Position = InitialPosition;


            attachment2 = new TgcSkeletalBoneAttach();
            var attachmentBox = TgcBox.fromSize(new Vector3(5, 100, 5), Color.Blue);
            attachment2.Mesh = attachmentBox.toMesh("attachment");
            attachment2.Bone = mesh.getBoneByName("Bip01 L Hand");
            attachment2.Offset = Matrix.Translation(10, -40, 0);
            attachment2.updateValues();

            rotationY = rotY;

        }

    }
}

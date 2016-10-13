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

namespace TGC.Group.Stage
{
    public class stage0
    {
        public TgcBox collisionPointMesh;
        public TgcArrow directionArrow;
        public TgcMesh mesh1;
        public Matrix meshRotationMatrix;
        public Vector3 newPosition;
        public Vector3 originalMeshRot;
        public TgcPickingRay pickingRay;
        public TgcPlane suelo0;
        public TgcMesh reja;
        public TgcMesh reja1;
        public TgcMesh reja2;
        public TgcMesh reja3;
        public TgcMesh reja4;
        public TgcMesh reja5;
        public TgcMesh reja6;
        public TgcMesh reja7;
        public TgcMesh reja8;
        public TgcMesh reja9;
        public TgcMesh reja10;
        public TgcMesh reja11;
        public TgcMesh reja12;
        public TgcMesh reja13;
        public TgcMesh reja14;
        public TgcMesh reja15;
        public TgcMesh tumba;
        public TgcMesh tumba1;
        public TgcMesh tumba2;
        public TgcMesh tumba3;
        public TgcMesh tumba4;
        public TgcMesh tumba5;
        public TgcMesh tumba6;
        public TgcMesh tumba7;
        public TgcMesh maceta;
        public TgcMesh maceta1;
        public TgcMesh maceta2;
        public TgcMesh maceta3;
        public TgcMesh maceta4;
        public TgcMesh maceta5;
        public TgcMesh maceta6;
        public TgcMesh maceta7;
        public TgcMesh maceta8;
        public TgcMesh maceta9;
        public TgcMesh maceta10;
        public TgcMesh maceta11;
        public TgcMesh maceta12;
        public TgcMesh maceta13;
        public TgcMesh maceta14;
        public TgcMesh maceta15;
        public TgcMesh maceta16;
        public TgcMesh maceta17;
        public TgcMesh maceta18;
        public TgcMesh maceta19;
        public TgcMesh maceta20;
        public TgcMesh maceta21;
        public TgcMesh maceta22;
        public TgcMesh maceta23;
        public TgcMesh maceta24;
        public TgcMesh maceta25;
        public TgcMesh maceta26;
        public TgcMesh maceta27;
        public TgcMesh maceta28;
        public TgcMesh maceta29;
        public TgcMesh maceta30;
        public TgcMesh maceta31;

        public TgcMesh carretilla { get; set; }

        //Suelo.
        public TgcBox Suelo { get; set; }

        //Fondo 
        public TgcBox Fondo { get; set; }

        public void crearMesh(string MediaDir) {
            //Cargar suelo
            var texture = TgcTexture.createTexture(D3DDevice.Instance.Device, MediaDir + "Grass.jpg");
            suelo0 = new TgcPlane(new Vector3(0, 4f, 0), new Vector3(10000, 0f, 10000), TgcPlane.Orientations.XZplane, texture);
          
            //Textura de la carperta Media. Game.Default es un archivo de configuracion (Game.settings) util para poner cosas.
            //Pueden abrir el Game.settings que se ubica dentro de nuestro proyecto para configurar.
            var pathTexturaSuelo = MediaDir + "Grass.jpg";
            var pathTexturaFondo = MediaDir + "grass_fence.jpg";
           
            //Cargamos una textura, tener en cuenta que cargar una textura significa crear una copia en memoria.
            //Es importante cargar texturas en Init, si se hace en el render loop podemos tener grandes problemas si instanciamos muchas.
            var texture1 = TgcTexture.createTexture(pathTexturaSuelo);
            var texturefondo = TgcTexture.createTexture(pathTexturaFondo);
            
            //Creamos una caja 3D ubicada de dimensiones (5, 10, 5) y la textura como color.
            var size = new Vector3(30000, 5, 30000);
            var sizefondo = new Vector3(30000, 30000, 30000); /////
            //Construimos una caja según los parámetros, por defecto la misma se crea con centro en el origen y se recomienda así para facilitar las transformaciones.
            Suelo = TgcBox.fromSize(size, texture);
            Fondo = TgcBox.fromSize(sizefondo, texturefondo);
            //Posición donde quiero que este la caja, es común que se utilicen estructuras internas para las transformaciones.
            //Entonces actualizamos la posición lógica, luego podemos utilizar esto en render para posicionar donde corresponda con transformaciones.
            Suelo.Position = new Vector3(-25, 0, 0);
            Fondo.Position = new Vector3(-25, 450, 0); /////

            carretilla = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Carretilla\\Carretilla-TgcScene.xml").Meshes[0];


            reja = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja1 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja2 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja3 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja4 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja5 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja6 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja7 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja8 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja9 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja10 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja11 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja12 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja13 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja14 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            reja15 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
            tumba = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Sarcofago\\Sarcofago-TgcScene.xml").Meshes[0];
            tumba1 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Sarcofago\\Sarcofago-TgcScene.xml").Meshes[0];
            tumba2 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Sarcofago\\Sarcofago-TgcScene.xml").Meshes[0];
            tumba3 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Sarcofago\\Sarcofago-TgcScene.xml").Meshes[0];
            tumba4 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Sarcofago\\Sarcofago-TgcScene.xml").Meshes[0];
            tumba5 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Sarcofago\\Sarcofago-TgcScene.xml").Meshes[0];
            tumba6 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Sarcofago\\Sarcofago-TgcScene.xml").Meshes[0];
            tumba7 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Sarcofago\\Sarcofago-TgcScene.xml").Meshes[0];
            maceta = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta1 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta2 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta3 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta4 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta5 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta6 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta7 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta8 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta9 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta10 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta11 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta12 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta13 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta14 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta15 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta16 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta17 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta18 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta19 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta20 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta21 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta22 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta23 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta24 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta25 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta26 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta27 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta28 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta29 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta30 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            maceta31 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];


            //pongo las rejas en posicion

            reja.rotateY(-44.9f);
            reja1.rotateY(-45f);
            reja4.rotateY(45f);
            reja5.rotateY(45f);
            reja6.rotateY(-80.1f);
            reja7.rotateY(-80.1f);
            reja8.rotateY(-44.88f);
            reja9.rotateY(-44.9f);
            reja12.rotateY(44.8f);
            reja13.rotateY(44.8f);
            reja14.rotateY(-80.1f);
            reja15.rotateY(-80.1f);
            tumba.rotateX(45.5f);
            tumba1.rotateX(45.5f);
            tumba1.rotateY(45.5f);
            tumba2.rotateX(45.5f);
            tumba2.rotateY(-47.5f);
            tumba3.rotateX(45.5f);
            tumba3.rotateY(-47.1f);
            tumba4.rotateX(45.5f);
            tumba4.rotateY(-46.5f);
            tumba5.rotateX(45.5f);
            tumba5.rotateY(-45.5f);
            tumba6.rotateX(45.5f);
            tumba6.rotateY(-45.0f);
            tumba7.rotateX(45.5f);

            carretilla.Position = new Vector3(-450, 3, -941);

            reja.Position = new Vector3(6980, 3, 8764);
            reja1.Position = new Vector3(10000, 3, 8864);
            reja2.Position = new Vector3(11000, 3, 334);
            reja3.Position = new Vector3(11000, 3, -1800);
            reja4.Position = new Vector3(9000, 3, -8500);
            reja5.Position = new Vector3(6000, 3, -10000);
            reja6.Position = new Vector3(1500, 3, -8900);
            reja7.Position = new Vector3(-2300, 3, -9000);
            reja8.Position = new Vector3(-6980, 3, -8764);
            reja9.Position = new Vector3(-10000, 3, -8864);
            reja10.Position = new Vector3(-10810, 3, -2100);
            reja11.Position = new Vector3(-10110, 3, -134);
            reja12.Position = new Vector3(-9500, 3, 5500);
            reja13.Position = new Vector3(-8500, 3, 7654);
            reja14.Position = new Vector3(-1650, 3, 7900);
            reja15.Position = new Vector3(1000, 3, 7900);
            tumba.Position = new Vector3(12500, 0, 14500);
            tumba1.Position = new Vector3(14500, 0, -580);
            tumba2.Position = new Vector3(10500, 0, -14026);
            tumba3.Position = new Vector3(-500, 0, -14526);
            tumba4.Position = new Vector3(-13000, 0, -14526);
            tumba5.Position = new Vector3(-14500, 0, -994);
            tumba6.Position = new Vector3(-14500, 0, 12084);
            tumba7.Position = new Vector3(-300, 0, 14500);
           // mesh2.Position = new Vector3(-550, 2, -14026);
           // mesh2.Transform = Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(16) * Matrix.Translation(mesh2.Position);

            maceta.Position = new Vector3(12000, 3, -600);
            maceta1.Position = new Vector3(9000, 3, -600);
            maceta2.Position = new Vector3(6000, 3, -600);
            maceta3.Position = new Vector3(3000, 3, -600);
            maceta4.Position = new Vector3(-12000, 3, -1000);
            maceta5.Position = new Vector3(-9000, 3, -1000);
            maceta6.Position = new Vector3(-6000, 3, -1000);
            maceta7.Position = new Vector3(-3000, 3, -1000);
            maceta8.Position = new Vector3(-340, 3, -12000);
            maceta9.Position = new Vector3(-340, 3, -9000);
            maceta10.Position = new Vector3(-340, 3, -6000);
            maceta11.Position = new Vector3(-340, 3, -3000);
            maceta12.Position = new Vector3(-340, 3, 12000);
            maceta13.Position = new Vector3(-340, 3, 8500);
            maceta14.Position = new Vector3(-340, 3, 5500);
            maceta15.Position = new Vector3(-340, 3, 2500);
            maceta16.Position = new Vector3(9500, 3, 10000);
            maceta17.Position = new Vector3(8000, 3, 8000);
            maceta18.Position = new Vector3(6500, 3, 6000);
            maceta19.Position = new Vector3(4500, 3, 3000);
            maceta20.Position = new Vector3(8800, 3, -12000);
            maceta21.Position = new Vector3(7500, 3, -9000);
            maceta22.Position = new Vector3(5500, 3, -6000);
            maceta23.Position = new Vector3(4000, 3, -3000);
            maceta24.Position = new Vector3(-11000, 3, -12000);
            maceta25.Position = new Vector3(-9000, 3, -9000);
            maceta26.Position = new Vector3(-7000, 3, -7000);
            maceta27.Position = new Vector3(-5000, 3, -4000);
            maceta28.Position = new Vector3(-11000, 3, 9000);
            maceta29.Position = new Vector3(-9500, 3, 7200);
            maceta30.Position = new Vector3(-8000, 3, 5000);
            maceta31.Position = new Vector3(-5000, 3, 2000);


            reja.Scale = new Vector3(250, 50, 50);
            reja1.Scale = new Vector3(250, 50, 50);
            reja2.Scale = new Vector3(155, 50, 50);
            reja3.Scale = new Vector3(150, 50, 50);
            reja4.Scale = new Vector3(195, 50, 50);
            reja5.Scale = new Vector3(200, 50, 50);
            reja6.Scale = new Vector3(140, 50, 50);
            reja7.Scale = new Vector3(150, 50, 50);
            reja8.Scale = new Vector3(188, 50, 50);
            reja9.Scale = new Vector3(210, 50, 50);
            reja10.Scale = new Vector3(150, 50, 50);
            reja11.Scale = new Vector3(150, 50, 50);
            reja12.Scale = new Vector3(190, 50, 50);
            reja13.Scale = new Vector3(240, 50, 50);
            reja14.Scale = new Vector3(180, 50, 50);
            reja15.Scale = new Vector3(175, 50, 50);
            tumba.Scale = new Vector3(8, 8, 18);
            tumba1.Scale = new Vector3(8, 8, 18);
            tumba2.Scale = new Vector3(8, 8, 18);
            tumba3.Scale = new Vector3(8, 8, 18);
            tumba4.Scale = new Vector3(8, 8, 18);
            tumba5.Scale = new Vector3(8, 8, 18);
            tumba6.Scale = new Vector3(8, 8, 18);
            tumba7.Scale = new Vector3(8, 8, 18);
           // mesh2.Scale = new Vector3(450, 450, 450);
            carretilla.Scale = new Vector3(30, 30, 30);

            maceta.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta1.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta2.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta3.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta4.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta5.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta6.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta7.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta8.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta9.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta10.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta11.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta12.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta13.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta14.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta15.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta16.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta17.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta18.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta19.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta20.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta21.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta22.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta23.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta24.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta25.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta26.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta27.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta28.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta29.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta30.Scale = new Vector3(2.5f, 2.5f, 2.5f);
            maceta31.Scale = new Vector3(2.5f, 2.5f, 2.5f);
        }

        public void rendermesh() {
            Fondo.render();
            Suelo.render();
            reja.render();
            reja1.render();
            reja2.render();
            reja3.render();
            reja4.render();
            reja5.render();
            reja6.render();
            reja7.render();
            reja8.render();
            reja9.render();
            reja10.render();
            reja11.render();
            reja12.render();
            reja13.render();
            reja14.render();
            reja15.render();
            tumba.render();
            tumba1.render();
            tumba2.render();
            tumba3.render();
            tumba4.render();
            tumba5.render();
            tumba6.render();
            tumba7.render();

            maceta.render();
            maceta1.render();
            maceta2.render();
            maceta3.render();
            maceta4.render();
            maceta5.render();
            maceta6.render();
            maceta7.render();
            maceta8.render();
            maceta9.render();
            maceta10.render();
            //plantaEnMedio.mesh.render();
            maceta11.render();
            maceta12.render();
            maceta13.render();
            maceta14.render();
            maceta15.render();
            maceta16.render();
            maceta17.render();
            maceta18.render();
            maceta19.render();
            maceta20.render();
            maceta21.render();
            maceta22.render();
            maceta23.render();
            maceta24.render();
            maceta25.render();
            maceta26.render();
            maceta27.render();
            maceta28.render();
            maceta29.render();
            maceta30.render();
            maceta31.render();

            carretilla.render();

            reja.BoundingBox.render();
            reja1.BoundingBox.render();
            reja2.BoundingBox.render();
            reja3.BoundingBox.render();
            reja4.BoundingBox.render();
            reja5.BoundingBox.render();
            reja6.BoundingBox.render();
            reja7.BoundingBox.render();
            reja8.BoundingBox.render();
            reja9.BoundingBox.render();
            reja10.BoundingBox.render();
            reja11.BoundingBox.render();
            reja12.BoundingBox.render();
            reja13.BoundingBox.render();
            reja14.BoundingBox.render();
            reja15.BoundingBox.render();


        }

        public void disposeMesh()
        {
            Suelo.dispose();
            Fondo.dispose();
            reja.dispose();
            reja1.dispose();
            reja2.dispose();
            reja3.dispose();
            reja4.dispose();
            reja5.dispose();
            reja6.dispose();
            reja7.dispose();
            reja8.dispose();
            reja9.dispose();
            reja10.dispose();
            reja11.dispose();
            reja12.dispose();
            reja13.dispose();
            reja14.dispose();
            reja15.dispose();

            tumba.dispose();
            tumba1.dispose();
            tumba2.dispose();
            tumba3.dispose();
            tumba4.dispose();
            tumba5.dispose();
            tumba6.dispose();
            tumba7.dispose();

            maceta.dispose();
            maceta1.dispose();
            maceta2.dispose();
            maceta3.dispose();
            maceta4.dispose();
            maceta5.dispose();
            maceta6.dispose();
            maceta7.dispose();
            maceta8.dispose();
            maceta9.dispose();
            maceta10.dispose();
            //plantaEnMedio.mesh.dispose();
            maceta11.dispose();
            maceta12.dispose();
            maceta13.dispose();
            maceta14.dispose();
            maceta15.dispose();
            maceta16.dispose();
            maceta17.dispose();
            maceta18.dispose();
            maceta19.dispose();
            maceta20.dispose();
            maceta21.dispose();
            maceta22.dispose();
            maceta23.dispose();
            maceta24.dispose();
            maceta25.dispose();
            maceta26.dispose();
            maceta27.dispose();
            maceta28.dispose();
            maceta29.dispose();
            maceta30.dispose();
            maceta31.dispose();
        }
    }
}

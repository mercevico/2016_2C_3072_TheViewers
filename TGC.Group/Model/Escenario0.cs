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
using TGC.Group.Characters.Plants;

namespace TGC.Group.Escenario

{
    /// <summary>
    ///     Ejemplo para implementar el TP.
    ///     Inicialmente puede ser renombrado o copiado para hacer más ejemplos chicos, en el caso de copiar para que se
    ///     ejecute el nuevo ejemplo deben cambiar el modelo que instancia GameForm <see cref="Form.GameForm.InitGraphics()" />
    ///     line 97.
    /// </summary>
    public class Escenario0 : TgcExample
    {
        /// <summary>
        ///     Constructor del juego.
        /// </summary>
        /// <param name="mediaDir">Ruta donde esta la carpeta con los assets</param>
        /// <param name="shadersDir">Ruta donde esta la carpeta con los shaders</param>
        public Escenario0(string mediaDir, string shadersDir) : base(mediaDir, shadersDir)
        {
            Category = Game.Default.Category;
            Name = Game.Default.Name;
            Description = Game.Default.Description;
        }


        private bool applyMovement;
        private Vector3 collisionPoint;
        private TgcBox collisionPointMesh;
        private TgcArrow directionArrow;
        private TgcMesh mesh1;
        private Matrix meshRotationMatrix;
        private Vector3 newPosition;
        private Vector3 originalMeshRot;
        private TgcPickingRay pickingRay;
        private TgcPlane suelo0;
        private TgcMesh reja;
        private TgcMesh reja1;
        private TgcMesh reja2;
        private TgcMesh reja3;
        private TgcMesh reja4;
        private TgcMesh reja5;
        private TgcMesh reja6;
        private TgcMesh reja7;
        private TgcMesh reja8;
        private TgcMesh reja9;
        private TgcMesh reja10;
        private TgcMesh reja11;
        private TgcMesh reja12;
        private TgcMesh reja13;
        private TgcMesh reja14;
        private TgcMesh reja15;
        private TgcMesh tumba;
        private TgcMesh tumba1;
        private TgcMesh tumba2;
        private TgcMesh tumba3;
        private TgcMesh tumba4;
        private TgcMesh tumba5;
        private TgcMesh tumba6;
        private TgcMesh tumba7;
        private TgcMesh[] maceta = new TgcMesh[32];

        //Suelo.
        private TgcBox Suelo { get; set; }

        //Fondo 
        private TgcBox Fondo { get; set; }

        //animacion
        private TgcSkeletalBoneAttach attachment2;
        private TgcSkeletalMesh mesh2;
        /*----------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------*/
        private TgcSkeletalBoneAttach attachment3;
        private TgcSkeletalMesh mesh3;
        private TgcSkeletalMesh selectedMesh;
        private TgcMesh selectedMaceta;

        private Matrix meshRotationMatrixZombie;
        private Vector3 newPositionZombie;
        private Vector3 originalMeshRotZombie;


        private TgcBoundingCylinder colliderCylinder;
        private TgcBoundingCylinderFixedY colliderCylinderFixedY;
        private TgcBoundingSphere collisionableSphere;
        private TgcMesh collisionableMeshAABB;
        private TgcBoundingCylinderFixedY collisionableCylinder;
        private TgcBoundingSphere characterSphere;

        private bool ThirdPersonCamera;
        /*----------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------*/

        //Meshes de Objetos del suelo--------------------------------------------------------///
        // Por ahora los comentamos y los creamos a mano, pero es un refactor a futuro
        //private Repeater[] Repeaters = new Repeater[500];
        //private Peashooter[] Peashooters = new Peashooter[500];
        //private Sunflower[] Sunflowers = new Sunflower[500];

        private TgcMesh[] Repeaters = new TgcMesh[500];
        private TgcMesh[] Peashooters = new TgcMesh[500];
        private TgcMesh[] Sunflowers = new TgcMesh[500];

        private TgcMesh carretilla { get; set; }

        //Boleano para ver si dibujamos el boundingbox
        private bool BoundingBox { get; set; }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo.
        ///     Escribir aquí todo el código de inicialización: cargar modelos, texturas, estructuras de optimización, todo
        ///     procesamiento que podemos pre calcular para nuestro juego.
        ///     Borrar el codigo ejemplo no utilizado.
        /// </summary>
        /// 
        private TgcScene scene;

        //Posición de la camara. La defino como variable global para volver siempre a este punto
        private Vector3 cameraPosition = new Vector3(50, 9500, 1024);
        //Quiero que la camara mire hacia el origen (0,0,0).
        private Vector3 lookAt = new Vector3(1, 200, 200);


        public override void Init()
        {


            //Paths para archivo XML de la malla
            var pathMesh = MediaDir + "Robot\\Robot-TgcSkeletalMesh.xml";

            //Path para carpeta de texturas de la malla
            var mediaPath = MediaDir + "Robot\\";

            var animationsPath = new string[1];
            animationsPath[0] = mediaPath + "Empujar" + "-TgcSkeletalAnim.xml";
            var loader1 = new TgcSkeletalLoader();
            mesh2 = loader1.loadMeshAndAnimationsFromFile(pathMesh, mediaPath, animationsPath);
            mesh2.buildSkletonMesh();

            attachment2 = new TgcSkeletalBoneAttach();
            var attachmentBox = TgcBox.fromSize(new Vector3(5, 100, 5), Color.Blue);
            attachment2.Mesh = attachmentBox.toMesh("attachment");
            attachment2.Bone = mesh2.getBoneByName("Bip01 L Hand");
            attachment2.Offset = Matrix.Translation(10, -40, 0);
            attachment2.updateValues();
            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/

            mesh3 = loader1.loadMeshAndAnimationsFromFile(pathMesh, mediaPath, animationsPath);
            mesh3.buildSkletonMesh();

            attachment3 = new TgcSkeletalBoneAttach();
            //var attachmentBox = TgcBox.fromSize(new Vector3(5, 100, 5), Color.Blue);
            attachment3.Mesh = attachmentBox.toMesh("attachment");
            attachment3.Bone = mesh3.getBoneByName("Bip01 L Hand");
            attachment3.Offset = Matrix.Translation(10, -40, 0);
            attachment3.updateValues();


            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/


            //Cargar suelo
            var texture = TgcTexture.createTexture(D3DDevice.Instance.Device, MediaDir + "Grass.jpg");
            suelo0 = new TgcPlane(new Vector3(0, 4f, 0), new Vector3(10000, 0f, 10000), TgcPlane.Orientations.XZplane, texture);
            
            //Iniciarlizar PickingRay
            pickingRay = new TgcPickingRay(Input);

            //Cargar personaje
            var loader = new TgcSceneLoader();
            var scene =
                loader.loadSceneFromFile(MediaDir + "\\Esqueletos\\EsqueletoHumano2\\Esqueleto2-TgcScene.xml");

            mesh1 = scene.Meshes[0];

            //Rotación original de la malla, hacia -Z
            originalMeshRot = new Vector3(0, 0, -1);

            //Manipulamos los movimientos del mesh a mano
            mesh1.AutoTransformEnable = false;
            meshRotationMatrix = Matrix.Identity;

            newPosition = mesh1.Position;
            applyMovement = false;

            //Crear caja para marcar en que lugar hubo colision
            collisionPointMesh = TgcBox.fromSize(new Vector3(3, 100, 3), Color.Red);

            //Flecha para marcar la dirección
            directionArrow = new TgcArrow();
            directionArrow.Thickness = 5;
            directionArrow.HeadSize = new Vector2(10, 10);



            //Device de DirectX para crear primitivas.
            var d3dDevice = D3DDevice.Instance.Device;

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

            for (int i = 0; i < 31; i++)
            {
                maceta[i] = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Olla\\Olla-TgcScene.xml").Meshes[0];
            }

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

            //for (int i = 0; i < 500; i++)
            //{
            //    Repeaters[i] = new TgcSceneLoader().loadSceneFromFile(MediaDir + "Plantas\\Repeater-TgcScene.xml").Meshes[0];
            //    Peashooters[i] = new TgcSceneLoader().loadSceneFromFile(MediaDir + "Plantas\\Peashooter-TgcScene.xml").Meshes[0];
            //    Sunflowers[i] = new TgcSceneLoader().loadSceneFromFile(MediaDir + "Plantas\\Sunflower-TgcScene.xml").Meshes[0];
            //}

            carretilla.Position = new Vector3(-450, 3, -941);

            reja.Position = new Vector3(6980,3,8764);
            reja1.Position = new Vector3(10000,3,8864);
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
            mesh2.Position = new Vector3(-550, 2, -14026);
            mesh2.Transform = Matrix.Scaling(new Vector3(25,25,25)) * Matrix.RotationY(16)* Matrix.Translation(mesh2.Position);

            maceta[0].Position = new Vector3(12000, 3, -600);
            maceta[1].Position = new Vector3(9000, 3, -600);
            maceta[2].Position = new Vector3(6000, 3, -600);
            maceta[3].Position = new Vector3(3000, 3, -600);
            maceta[4].Position = new Vector3(-12000, 3, -1000);
            maceta[5].Position = new Vector3(-9000, 3, -1000);
            maceta[6].Position = new Vector3(-6000, 3, -1000);
            maceta[7].Position = new Vector3(-3000, 3, -1000);
            maceta[8].Position = new Vector3(-340, 3, -12000);
            maceta[9].Position = new Vector3(-340, 3, -9000);
            maceta[10].Position = new Vector3(-340, 3, -6000);
            maceta[11].Position = new Vector3(-340, 3, -3000);
            maceta[12].Position = new Vector3(-340, 3, 12000);
            maceta[13].Position = new Vector3(-340, 3, 8500);
            maceta[14].Position = new Vector3(-340, 3, 5500);
            maceta[15].Position = new Vector3(-340, 3, 2500);
            maceta[16].Position = new Vector3(9500, 3, 10000);
            maceta[17].Position = new Vector3(8000, 3, 8000);
            maceta[18].Position = new Vector3(6500, 3, 6000);
            maceta[19].Position = new Vector3(4500, 3, 3000);
            maceta[20].Position = new Vector3(8800, 3, -12000);
            maceta[21].Position = new Vector3(7500, 3, -9000);
            maceta[22].Position = new Vector3(5500, 3, -6000);
            maceta[23].Position = new Vector3(4000, 3, -3000);
            maceta[24].Position = new Vector3(-11000, 3, -12000);
            maceta[25].Position = new Vector3(-9000, 3, -9000);
            maceta[26].Position = new Vector3(-7000, 3, -7000);
            maceta[27].Position = new Vector3(-5000, 3, -4000);
            maceta[28].Position = new Vector3(-11000, 3, 9000);
            maceta[29].Position = new Vector3(-9500, 3, 7200);
            maceta[30].Position = new Vector3(-8000, 3, 5000);
            maceta[31].Position = new Vector3(-5000, 3, 2000);
            
            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            //Rotar modelo en base a la nueva dirección a la que apunta

            mesh3.Position = new Vector3(10250, 5, -13500);
            mesh3.Transform = Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(9) * Matrix.Translation(mesh3.Position);

            mesh3.AutoUpdateBoundingBox = false;
            mesh3.BoundingBox.transform(Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(9) * Matrix.Translation(mesh3.Position));


            characterSphere = new TgcBoundingSphere(mesh3.BoundingBox.calculateBoxCenter(), mesh3.BoundingBox.calculateBoxRadius());


            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            //Defino una escala en el modelo logico del m1esh que es muy grande.
            //Mesh.Scale = new Vector3(0.5f, 0.5f, 0.5f);



            //Suelen utilizarse objetos que manejan el comportamiento de la camara.
            //Lo que en realidad necesitamos gráficamente es una matriz de View.
            //El framework maneja una cámara estática, pero debe ser inicializada.

            //Configuro donde esta la posicion de la camara y hacia donde mira.
            Camara.SetCamera(cameraPosition, lookAt);
            //Internamente el framework construye la matriz de view con estos dos vectores.
            //Luego en nuestro juego tendremos que crear una cámara que cambie la matriz de view con variables como movimientos o animaciones de escenas.
        }

        /// <summary>
        ///     Se llama en cada frame.
        ///     Se debe escribir toda la lógica de computo del modelo, así como también verificar entradas del usuario y reacciones
        ///     ante ellas.
        /// </summary>
        public override void Update()
        {
            PreUpdate();

            //Si hacen clic con el mouse, ver si hay colision con el suelo
            if (Input.buttonPressed(TgcD3dInput.MouseButtons.BUTTON_LEFT))
            {
                //Actualizar Ray de colisión en base a posición del mouse
                pickingRay.updateRay();

                var aabb = mesh3;
                //Detectar colisión Ray-AABB
                if (TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, aabb.BoundingBox, out collisionPoint))
                {
                    selectedMesh = aabb;
                    selectedMaceta = null;
                }
                else
                {
                    selectedMesh = null;
                }
                // Detecto Colisión con macetas
                for (int i = 0; i < 31; i++)
                {
                    if (TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, maceta[i].BoundingBox, out collisionPoint))
                    {
                        selectedMaceta = maceta[i];
                        selectedMesh = null;
                    }
                    else
                    {
                        selectedMaceta = null;
                    }
                }

            }

            //Capturar Input teclado
            if (Input.keyPressed(Key.F))
            {
                BoundingBox = !BoundingBox;
            }

            if (Input.keyPressed(Key.C))
            {
                if (selectedMesh != null)
                {
                    ThirdPersonCamera = !ThirdPersonCamera; //Invierto la cámara

                    if (!ThirdPersonCamera) //Si no estoy en tercera persona
                    {
                        Camara.SetCamera(cameraPosition, lookAt);
                    }
                }

            }

            if (Input.keyPressed(Key.Q))
            {
                if (selectedMaceta != null)
                {
                    // Crear Peashooter
                }
            }

            if (Input.keyPressed(Key.W))
            {
                if (selectedMaceta != null)
                {
                    // Crear Repeater
                }
            }
            if (Input.keyPressed(Key.E))
            {
                if (selectedMaceta != null)
                {
                    // Crear Sunflower
                }
            }


            if (ThirdPersonCamera)
            {
                Camara.SetCamera(selectedMesh.Position + new Vector3(0, 100, 500), selectedMesh.Position + new Vector3(1, 1, 0));
            }
                //Capturar Input Mouse
                /* if (Input.buttonUp(TgcD3dInput.MouseButtons.BUTTON_LEFT))
                 {

                     //Como ejemplo podemos hacer un movimiento simple de la cámara.
                     //En este caso le sumamos un valor en Y
                     Camara.SetCamera(Camara.Position + new Vector3(0, 10f, 0), Camara.LookAt);
                     //Ver ejemplos de cámara para otras operaciones posibles.

                     //Si superamos cierto Y volvemos a la posición original.
                     if (Camara.Position.Y > 300f)
                     {
                         Camara.SetCamera(new Vector3(Camara.Position.X, 0f, Camara.Position.Z), Camara.LookAt);
                     }
                 }*/


            if (Input.keyDown(Key.A))
            {
                Camara.SetCamera(Camara.Position + new Vector3(30, 0, 0), Camara.LookAt + new Vector3(30, 0, 0));
            }
            if (Input.keyDown(Key.D))
            {

                Camara.SetCamera(Camara.Position + new Vector3(-30, 0, 0), Camara.LookAt + new Vector3(-30, 0, 0));
            }
            if (Input.keyDown(Key.W))
            {

                Camara.SetCamera(Camara.Position + new Vector3(0, 0, -30), Camara.LookAt + new Vector3(0, 0, -30));
            }
            if (Input.keyDown(Key.S))
            {

                Camara.SetCamera(Camara.Position + new Vector3(0, 0, 30), Camara.LookAt + new Vector3(0, 0, 30));
            }
            

            if (Camara.Position.Z < -11826)
            {
                Camara.SetCamera(new Vector3(Camara.Position.X, Camara.Position.Y, -11826), Camara.LookAt + new Vector3(0, 0, 30));
            }


            if (Camara.Position.Z > 13000)
            {
                Camara.SetCamera(new Vector3(Camara.Position.X, Camara.Position.Y, 13000), Camara.LookAt + new Vector3(0, 0, -30));
            }

            if (Camara.Position.X >10000)
            {
                Camara.SetCamera(new Vector3(10000, Camara.Position.Y, Camara.Position.Z), Camara.LookAt + new Vector3(-30, 0, 0));
            }

            if (Camara.Position.X < -10000)
            {
                Camara.SetCamera(new Vector3(-10000, Camara.Position.Y, Camara.Position.Z), Camara.LookAt + new Vector3(30, 0, 0));
            }


            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/

            
            mesh2.Transform = Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(15.8f) * Matrix.Translation(mesh2.Position);
            
            const float MOVEMENT_SPEED = 1f;


            var movement = new Vector3(0, 0, 0);

            //movement.X = 0;
            //movement.Y = 0;
            //movement.Z = 1;

            movement = carretilla.Position - mesh2.Position;

            movement *= MOVEMENT_SPEED * ElapsedTime;

            movement.Normalize();

            //mesh2.move(movement);

            mesh2.Position = mesh2.Position + movement;

            mesh2.Position.Normalize();

            mesh2.Transform = Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(15.8f) * Matrix.Translation(mesh2.Position);

            



            //const float MOVEMENT_SPEED = 1f;

            var movement2 = new Vector3(0, 0, 0);

            var directionZombie3 = carretilla.Position - mesh3.Position;

            /*
            movement.X = 0;
            movement.Y = 0;
            movement.Z = 1;
            */

            movement2 = directionZombie3;

            movement2 *= MOVEMENT_SPEED * ElapsedTime;

            movement2.Normalize();

            
            mesh3.Position = mesh3.Position + movement2;

            mesh3.Position.Normalize();

            mesh3.Transform = Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(9) * Matrix.Translation(mesh3.Position);

            mesh3.BoundingBox.transform(Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(9) * Matrix.Translation(mesh3.Position));
            characterSphere.setCenter(mesh3.BoundingBox.calculateBoxCenter());



            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/




        }

        /// <summary>
        ///     Se llama cada vez que hay que refrescar la pantalla.
        ///     Escribir aquí todo el código referido al renderizado.
        ///     Borrar todo lo que no haga falta.
        /// </summary>
        public override void Render()
        {
            //Inicio el render de la escena, para ejemplos simples. Cuando tenemos postprocesado o shaders es mejor realizar las operaciones según nuestra conveniencia.
            PreRender();

            //Interporlar movimiento, si hay que mover
            if (applyMovement)
            {
                //Ver si queda algo de distancia para mover
                var posDiff = newPosition - mesh1.Position;
                var posDiffLength = posDiff.LengthSq();
                if (posDiffLength > float.Epsilon)
                {
                    //Movemos el mesh interpolando por la velocidad
                    var currentVelocity = 1500 * ElapsedTime;
                    posDiff.Normalize();
                    posDiff.Multiply(currentVelocity);

                    //Ajustar cuando llegamos al final del recorrido
                    var newPos = mesh1.Position + posDiff;
                    if (posDiff.LengthSq() > posDiffLength)
                    {
                        newPos = newPosition;
                    }

                    //Actualizar flecha de movimiento
                    directionArrow.PStart = new Vector3(mesh1.Position.X, 30f, mesh1.Position.Z);
                    directionArrow.updateValues();

                    //Actualizar posicion del mesh
                    mesh1.Position = newPos;

                    //Como desactivamos la transformacion automatica, tenemos que armar nosotros la matriz de transformacion
                    mesh1.Transform = meshRotationMatrix * Matrix.Translation(mesh1.Position);

                    
                }
                //Se acabo el movimiento
                else
                {
                    applyMovement = false;
                }
            }

            //Mostrar caja con lugar en el que se hizo click, solo si hay movimiento
            if (applyMovement)
            {
                collisionPointMesh.render();
                directionArrow.render();
            }



            //Dibuja un texto por pantalla
            
            DrawText.drawText("ElapsedTime: " + ElapsedTime, 0, 70, Color.GreenYellow);


            DrawText.drawText("Con la tecla F se dibuja el bounding box.", 0, 20, Color.OrangeRed);
            DrawText.drawText(
                "Con clic izquierdo subimos la camara [Actual]: " + TgcParserUtils.printVector3(Camara.Position), 0, 30,
                Color.OrangeRed);
            DrawText.drawText(
               "Con clic derecho rotamos la camara [Actual]: " + TgcParserUtils.printVector3(Camara.Position), 0, 50,
               Color.Black);
            //Siempre antes de renderizar el modelo necesitamos actualizar la matriz de transformacion.
            //Debemos recordar el orden en cual debemos multiplicar las matrices, en caso de tener modelos jerárquicos, tenemos control total.
            Suelo.Transform = Matrix.Scaling(Suelo.Scale) *
                            Matrix.RotationYawPitchRoll(Suelo.Rotation.Y, Suelo.Rotation.X, Suelo.Rotation.Z) *
                            Matrix.Translation(Suelo.Position);

            Fondo.Transform = Matrix.Scaling(Fondo.Scale) *
                            Matrix.RotationYawPitchRoll(Suelo.Rotation.Y, Suelo.Rotation.X, Suelo.Rotation.Z) * Matrix.Translation(Fondo.Position);
            carretilla.Transform = Matrix.Scaling(new Vector3(0, 0, 2));
            reja.Scale = new Vector3(250,50,50);
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
            mesh2.Scale = new Vector3(450, 450, 450);
            carretilla.Scale = new Vector3(30, 30, 30);
            
            for (int i = 0; i < 31; i++)
            {
                maceta[i].Scale = new Vector3(2.5f, 2.5f, 2.5f);
                maceta[i].render();
            }

            //A modo ejemplo realizamos toda las multiplicaciones, pero aquí solo nos hacia falta la traslación.
            //Finalmente invocamos al render de la caja
            Fondo.render();
            Suelo.render();
            carretilla.render();

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

            
            //mesh2.Transform = mesh2.Transform + Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(16) * Matrix.Translation(mesh2.Position + new Vector3(25, 0, 25));
            mesh2.playAnimation("Empujar",true,2);
            
            mesh2.animateAndRender(0.1f);


            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            
            mesh3.playAnimation("Empujar", true, 2);
            mesh3.animateAndRender(0.1f);

            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/


            //scene.renderAll();

            //Cuando tenemos modelos mesh podemos utilizar un método que hace la matriz de transformación estándar.
            //Es útil cuando tenemos transformaciones simples, pero OJO cuando tenemos transformaciones jerárquicas o complicadas.
            //Mesh.UpdateMeshTransform();
            for (int i = 0; i < 500; i++)
            {
                //Planta[i].UpdateMeshTransform();
            }
            for (int i = 0; i < 500; i++)
            {
                //Planta1[i].UpdateMeshTransform();
            }
            //Render del mesh
            //Mesh.render();
            for (int i = 0; i < 500; i++)
            {
                //Planta[i].render();
            }
            for (int i = 0; i < 500; i++)
            {
                //Planta1[i].render(); mesh1.render();
            }

            //Render de BoundingBox, muy útil para debug de colisiones.
            if (BoundingBox)
            {
                Suelo.BoundingBox.render();
                //Mesh.BoundingBox.render();
                mesh2.BoundingBox.render();
                mesh3.BoundingBox.render();


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

                characterSphere.render();
            }

            //Finaliza el render y presenta en pantalla, al igual que el preRender se debe para casos puntuales es mejor utilizar a mano las operaciones de EndScene y PresentScene
            PostRender();
        }

        /// <summary>
        ///     Se llama cuando termina la ejecución del ejemplo.
        ///     Hacer Dispose() de todos los objetos creados.
        ///     Es muy importante liberar los recursos, sobretodo los gráficos ya que quedan bloqueados en el device de video.
        /// </summary>
        public override void Dispose()
        {
            //Dispose de la caja.
            Suelo.dispose();
            Fondo.dispose();
            mesh1.dispose();
            //Dispose del mesh.
            //Mesh.dispose();
            for (int i = 0; i < 500; i++)
            {
                Peashooters[i].dispose();
                Repeaters[i].dispose();
                Sunflowers[i].dispose();
            }

            carretilla.dispose();
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

            for (int i = 0; i < 31; i++)
            {
                maceta[i].dispose();
            }

            //  scene.disposeAll();

        }
    }
}
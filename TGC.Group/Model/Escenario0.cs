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
using TGC.Group.Characters.soles;


namespace TGC.Group.Escenario

{
   
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
        

        //animacion
        private TgcSkeletalBoneAttach attachment2;
        private TgcSkeletalMesh zombie2;
        /*----------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------*/
        private TgcSkeletalBoneAttach attachment3;
        private TgcSkeletalMesh mesh3;
        private TgcMesh selectedMesh;
        private TgcMesh selectedMaceta;
        private Plant selectedPlant;

        private TgcBoundingSphere characterSphere;
        public int cantSoles = 0;
        private bool moverMESH2 = true;
        private bool moverMESH3 = true;

        private Zombie zombieMESH2 = new Zombie();
        private Plant plantaCARRETILLA = new Plant();

        private float ACU_TIEMPO_ATAQUE = 0f;

        /******************************************************************************************************************************************/
        private List<TgcMesh> objetosColisionables = new List<TgcMesh>();
        private List<TgcMesh> objetosColisionablesMACETAS = new List<TgcMesh>();
        private List<TgcMesh> objetosColisionablesSOLES = new List<TgcMesh>();
        private List<Plant> objetosColisionablesPLANTS = new List<Plant>();


        private Plant plantaCentro = new Plant();
        private Plant plantaEnMedio = new Plant();

        private Vector3 CENTRO_ESCENARIO;


        private bool ThirdPersonCamera;
        /*----------------------------------------------------------------------------------------------------------------------------------------*/
        /*----------------------------------------------------------------------------------------------------------------------------------------*/


        //Boleano para ver si dibujamos el boundingbox
        private bool BoundingBox { get; set; }

        /// <summary>
        ///     Se llama una sola vez, al principio cuando se ejecuta el ejemplo.
        ///     Escribir aquí todo el código de inicialización: cargar modelos, texturas, estructuras de optimización, todo
        ///     procesamiento que podemos pre calcular para nuestro juego.
        ///     Borrar el codigo ejemplo no utilizado.
        /// </summary>
        /// 
       

        //Posición de la camara. La defino como variable global para volver siempre a este punto
        private Vector3 cameraPosition = new Vector3(50, 9500, 1024);
        //Quiero que la camara mire hacia el origen (0,0,0).
        private Vector3 lookAt = new Vector3(1, 200, 200);
        private stage0 stage = new stage0();
        private Plant repeater = new Plant();
        private Sol sol0 = new Sol();
        private Sol sol1 = new Sol();
        private Sol sol2 = new Sol();
        private Sol sol3 = new Sol();
        private Sol sol4 = new Sol();
        private Sol sol5 = new Sol();
        private Sol sol6 = new Sol();
        private Sol sol7 = new Sol();
        private Sol sol8 = new Sol();
        private Sol sol9 = new Sol();

        private Sunflower SunFlo = new Sunflower();

        public override void Init()
        {


            //Paths para archivo XML de la malla
            var pathMesh = MediaDir + "Robot\\Robot-TgcSkeletalMesh.xml";

            //Path para carpeta de texturas de la malla
            var mediaPath = MediaDir + "Robot\\";

            var animationsPath = new string[1];
            animationsPath[0] = mediaPath + "Empujar" + "-TgcSkeletalAnim.xml";
            var loader1 = new TgcSkeletalLoader();
            zombie2 = loader1.loadMeshAndAnimationsFromFile(pathMesh, mediaPath, animationsPath);
            zombie2.buildSkletonMesh();
            zombie2.Position = new Vector3(-500, 0, -14526);


            attachment2 = new TgcSkeletalBoneAttach();
            var attachmentBox = TgcBox.fromSize(new Vector3(5, 100, 5), Color.Blue);
            attachment2.Mesh = attachmentBox.toMesh("attachment");
            attachment2.Bone = zombie2.getBoneByName("Bip01 L Hand");
            attachment2.Offset = Matrix.Translation(10, -40, 0);
            attachment2.updateValues();
            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/

            mesh3 = loader1.loadMeshAndAnimationsFromFile(pathMesh, mediaPath, animationsPath);
            mesh3.buildSkletonMesh();

            attachment3 = new TgcSkeletalBoneAttach();
            attachment3.Mesh = attachmentBox.toMesh("attachment");
            attachment3.Bone = mesh3.getBoneByName("Bip01 L Hand");
            attachment3.Offset = Matrix.Translation(10, -40, 0);
            attachment3.updateValues();


            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/


            stage.crearMesh(MediaDir);
            repeater.crearMESH(MediaDir, new Vector3(12000, 3, -600));
            sol0.crearMESH(new Vector3(2000, 100, -600),MediaDir);
            sol1.crearMESH(new Vector3(-2000, 100, -2600), MediaDir);
            sol2.crearMESH(new Vector3(2900, 100, -11600), MediaDir);
            sol3.crearMESH(new Vector3(7860, 100, 6200), MediaDir);
            sol4.crearMESH(new Vector3(-12000, 100, -600), MediaDir);
            sol5.crearMESH(new Vector3(2900, 100, -1600), MediaDir);
            sol6.crearMESH(new Vector3(3000, 100, 8600), MediaDir);
            sol7.crearMESH(new Vector3(-9000, 100, -800), MediaDir);
            sol8.crearMESH(new Vector3(2590, 100, -11100), MediaDir);
            sol9.crearMESH(new Vector3(-12000, 100, 11600), MediaDir);
           
            SunFlo.crearMESHSun(MediaDir);

            //Iniciarlizar PickingRay
            pickingRay = new TgcPickingRay(Input);

           

            //Crear caja para marcar en que lugar hubo colision
            collisionPointMesh = TgcBox.fromSize(new Vector3(3, 100, 3), Color.Red);

            //Flecha para marcar la dirección
            directionArrow = new TgcArrow();
            directionArrow.Thickness = 5;
            directionArrow.HeadSize = new Vector2(10, 10);



            //Device de DirectX para crear primitivas.
            var d3dDevice = D3DDevice.Instance.Device;

           


            
            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            //Rotar modelo en base a la nueva dirección a la que apunta

            mesh3.Position = new Vector3(10250, 5, -13500);
            mesh3.Transform = Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(9) * Matrix.Translation(mesh3.Position);

            mesh3.AutoUpdateBoundingBox = false;
            mesh3.BoundingBox.transform(Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(9) * Matrix.Translation(mesh3.Position));




            characterSphere = new TgcBoundingSphere(mesh3.BoundingBox.calculateBoxCenter(), mesh3.BoundingBox.calculateBoxRadius());


            /******************************************************************************************************************************************/
            plantaCentro.crearMESH(MediaDir, new Vector3(-450, 3, -941));
           // plantaCentro.mesh.Position = new Vector3(-450, 3, -941);
            //plantaCentro.mesh.Scale = new Vector3(30, 30, 30);

            plantaEnMedio.crearMESH(MediaDir, new Vector3(-340, 3, -6000));
            //plantaEnMedio.mesh.Position = new Vector3(-340, 3, -6000);
            //plantaEnMedio.mesh.Scale = new Vector3(2.5f, 2.5f, 2.5f);


            objetosColisionablesPLANTS.Clear();
            //objetosColisionablesPLANTS.Add(plantaCentro);
            //objetosColisionablesPLANTS.Add(plantaEnMedio);

            CENTRO_ESCENARIO = new Vector3(-450, 3, -941); 

            //Almacenar volumenes de colision del escenario
            objetosColisionables.Clear();

            objetosColisionables.Add(stage.carretilla);
            objetosColisionables.Add(stage.maceta10);


            // Array de macetas en donde se pueden plantar las plantas

            objetosColisionablesMACETAS.Clear();

            objetosColisionablesMACETAS.Add(stage.maceta);
            objetosColisionablesMACETAS.Add(stage.maceta1);
            objetosColisionablesMACETAS.Add(stage.maceta2);
            objetosColisionablesMACETAS.Add(stage.maceta3);
            objetosColisionablesMACETAS.Add(stage.maceta4);
            objetosColisionablesMACETAS.Add(stage.maceta5);
            objetosColisionablesMACETAS.Add(stage.maceta6);
            objetosColisionablesMACETAS.Add(stage.maceta7);
            objetosColisionablesMACETAS.Add(stage.maceta8);
            objetosColisionablesMACETAS.Add(stage.maceta9);
            objetosColisionablesMACETAS.Add(stage.maceta10);
            objetosColisionablesMACETAS.Add(stage.maceta11);
            objetosColisionablesMACETAS.Add(stage.maceta12);
            objetosColisionablesMACETAS.Add(stage.maceta13);
            objetosColisionablesMACETAS.Add(stage.maceta14);
            objetosColisionablesMACETAS.Add(stage.maceta15);
            objetosColisionablesMACETAS.Add(stage.maceta16);
            objetosColisionablesMACETAS.Add(stage.maceta17);
            objetosColisionablesMACETAS.Add(stage.maceta18);
            objetosColisionablesMACETAS.Add(stage.maceta19);
            objetosColisionablesMACETAS.Add(stage.maceta20);
            objetosColisionablesMACETAS.Add(stage.maceta21);
            objetosColisionablesMACETAS.Add(stage.maceta22);
            objetosColisionablesMACETAS.Add(stage.maceta23);
            objetosColisionablesMACETAS.Add(stage.maceta24);
            objetosColisionablesMACETAS.Add(stage.maceta25);
            objetosColisionablesMACETAS.Add(stage.maceta26);
            objetosColisionablesMACETAS.Add(stage.maceta27);
            objetosColisionablesMACETAS.Add(stage.maceta28);
            objetosColisionablesMACETAS.Add(stage.maceta29);
            objetosColisionablesMACETAS.Add(stage.maceta30);
            objetosColisionablesMACETAS.Add(stage.maceta31);

            objetosColisionablesSOLES.Add(sol0.sunnyMesh);
            objetosColisionablesSOLES.Add(sol1.sunnyMesh);
            objetosColisionablesSOLES.Add(sol2.sunnyMesh);
            objetosColisionablesSOLES.Add(sol3.sunnyMesh);
            objetosColisionablesSOLES.Add(sol4.sunnyMesh);
            objetosColisionablesSOLES.Add(sol5.sunnyMesh);
            objetosColisionablesSOLES.Add(sol6.sunnyMesh);
            objetosColisionablesSOLES.Add(sol7.sunnyMesh);
            objetosColisionablesSOLES.Add(sol8.sunnyMesh);
            objetosColisionablesSOLES.Add(sol9.sunnyMesh);
            
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

            if (Input.keyPressed(Key.Z) && selectedMaceta != null)
            {
                Plant repeaterNuevo = new Plant();
                repeaterNuevo.crearMESH(MediaDir, selectedMaceta.Position);
                //repeater.crearMESH(MediaDir, selectedMaceta.Position);
                //repeaterNuevo.rendermesh();
                objetosColisionablesPLANTS.Add(repeaterNuevo);

            }

            if (Input.keyPressed(Key.X) && selectedPlant != null)
            {
                /*
                if ( ! (objetosColisionablesPLANTS.Contains(selectedPlant) ))
                {
                    selectedPlant.plantaMesh.dispose();
                }
                */
                selectedPlant.plantaMesh.dispose();
                objetosColisionablesPLANTS.Remove(selectedPlant);

            }

            if (Input.keyPressed(Key.P))
            {
                SunFlo.i = 10000;
                cantSoles++;
            }
         


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

            zombie2.Transform = Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(15.8f) * Matrix.Translation(zombie2.Position);
            
            const float MOVEMENT_SPEED = 1f;
            ACU_TIEMPO_ATAQUE += ElapsedTime;

            if (moverMESH2 == true)
            {
                var movement = new Vector3(0, 0, 0);

                //movement.X = 0;
                //movement.Y = 0;
                //movement.Z = 1;

                movement = CENTRO_ESCENARIO - zombie2.Position;

                movement *= (MOVEMENT_SPEED * ElapsedTime);

                movement.Normalize();

                //mesh2.move(movement);

                zombie2.Position = zombie2.Position + movement;

                zombie2.Position.Normalize();

                zombie2.Transform = Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(15.8f) * Matrix.Translation(zombie2.Position);
                zombie2.AutoUpdateBoundingBox = false;
                zombie2.BoundingBox.transform(Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(15.8f) * Matrix.Translation(zombie2.Position));

            }



            foreach (var planta in objetosColisionablesPLANTS)
            {

                if (planta.muerta == false) { 
                    if(true)// ((TgcCollisionUtils.testAABBAABB(zombie2.BoundingBox, repeater.plantaMesh.BoundingBox)) )
                    {
                        zombie2.BoundingBox.setRenderColor(Color.Red);
                        moverMESH2 = false;
                        if (ACU_TIEMPO_ATAQUE > 0.55f)
                        {
                            planta.health -= zombieMESH2.attack();
                            ACU_TIEMPO_ATAQUE = 0f;
                        }

                        if (planta.health <= 0)
                        {
                            planta.muerta = true;
                            zombie2.BoundingBox.setRenderColor(Color.Yellow);
                            moverMESH2 = true;
                        }


                    }
                    else
                    {
                        zombie2.BoundingBox.setRenderColor(Color.Yellow);
                        moverMESH2 = true;
                    }
               }
            }





            /*
            if (TgcCollisionUtils.testAABBAABB(mesh2.BoundingBox, plantaCentro.mesh.BoundingBox))
            {
                mesh2.BoundingBox.setRenderColor(Color.Red);
                moverMESH2 = false;

                if (ACU_TIEMPO_ATAQUE > 0.45f) {
                    plantaCARRETILLA.health -= zombieMESH2.attack();
                    ACU_TIEMPO_ATAQUE = 0f;
                }
            }
            else
            {
                mesh2.BoundingBox.setRenderColor(Color.Yellow);
                moverMESH2 = true;
            }
            */




            //const float MOVEMENT_SPEED = 1f;

            if (moverMESH3 == true)
            {
                var movement2 = new Vector3(0, 0, 0);

                //var directionZombie3 = carretilla.Position - mesh3.Position;
                var directionZombie3 = CENTRO_ESCENARIO - mesh3.Position;

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
            }

            foreach (var planta in objetosColisionablesPLANTS)
            {
                if(planta.muerta == false)
                {
                    if (TgcCollisionUtils.testSphereAABB(characterSphere, repeater.plantaMesh.BoundingBox))
                    {
                        characterSphere.setRenderColor(Color.Red);
                        moverMESH3 = false;
                    }
                    else
                    {
                        characterSphere.setRenderColor(Color.Yellow);
                        moverMESH3 = true;
                    }
                }



            }
            //Si hacen clic con el mouse, ver si hay colision con el suelo
            if (Input.buttonPressed(TgcD3dInput.MouseButtons.BUTTON_LEFT))
            {
                //Actualizar Ray de colisión en base a posición del mouse
                pickingRay.updateRay();

                //var aabb = mesh3;
                ////Detectar colisión Ray-AABB
                //if (TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, aabb.BoundingBox, out collisionPoint))
                //{
                //    selectedMesh = aabb;

                //}
                //else
                //{
                //    selectedMesh = null;
                //}

                if (TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, stage.Suelo.BoundingBox, out newPosition))
                {
                    selectedMaceta = null;
                    selectedMesh = null;
                    selectedPlant = null;
                }

                foreach (var maceta in objetosColisionablesMACETAS)
                {
                    if (TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, maceta.BoundingBox, out collisionPoint))
                    {
                        selectedMaceta = maceta;
                        selectedMesh = null;
                        selectedPlant = null;
                    }
                }


                foreach (var plant in objetosColisionablesPLANTS)
                {
                    if (TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, plant.boundingBox(), out collisionPoint))
                    {
                        selectedMaceta = null;
                        selectedMesh = null;
                        selectedPlant = plant;
                    }
                }

                foreach (var soles in objetosColisionablesSOLES)
                {
                    if (TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, soles.BoundingBox, out collisionPoint))
                    {
                        if (soles!=null) { soles.dispose(); }
                        cantSoles++;
                    }
                }

                foreach (var soles in SunFlo.objetosColisionablesSOLES)
                {
                    if (TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, soles.BoundingBox, out collisionPoint))
                    {
                        soles.dispose();
                        cantSoles++;
                    }
                }

            }
            /*
            if (TgcCollisionUtils.testSphereAABB(characterSphere, plantaCentro.mesh.BoundingBox))
            {
                characterSphere.setRenderColor(Color.Red);
                moverMESH3 = false;
            }
            else
            {
                characterSphere.setRenderColor(Color.Yellow);
                moverMESH3 = true;
            }
            */

            //reja.AutoUpdateBoundingBox = false;
            //reja.BoundingBox.transform(Matrix.Scaling(reja.Scale) * Matrix.RotationY(-44.9f) * Matrix.Translation(reja.Position));

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
            

           


            //Dibuja un texto por pantalla
            
            DrawText.drawText("ElapsedTime: " + ElapsedTime, 0, 70, Color.GreenYellow);
            DrawText.drawText("Soles Recogidos: " + cantSoles, 0, 200, Color.GreenYellow);

            DrawText.drawText("Con la tecla F se dibuja el bounding box.", 0, 20, Color.OrangeRed);
            DrawText.drawText(
                "Con clic izquierdo subimos la camara [Actual]: " + TgcParserUtils.printVector3(Camara.Position), 0, 30,
                Color.OrangeRed);
            DrawText.drawText(
               "Con clic derecho rotamos la camara [Actual]: " + TgcParserUtils.printVector3(Camara.Position), 0, 50,
               Color.Black);
            //Siempre antes de renderizar el modelo necesitamos actualizar la matriz de transformacion.
            //Debemos recordar el orden en cual debemos multiplicar las matrices, en caso de tener modelos jerárquicos, tenemos control total.




            //A modo ejemplo realizamos toda las multiplicaciones, pero aquí solo nos hacia falta la traslación.
            //Finalmente invocamos al render de la caja
            stage.rendermesh();
            //repeater.rendermesh();
            sol0.rendermesh();
            sol1.rendermesh();
            sol2.rendermesh();
            sol3.rendermesh();
            sol4.rendermesh();
            sol5.rendermesh();
            sol6.rendermesh();
            sol7.rendermesh();
            sol8.rendermesh();
            sol9.rendermesh();
            
            SunFlo.rendermeshSun();

            foreach (var plant in objetosColisionablesPLANTS )
            {
                if (plant.health > 0)
                {
                   plant.plantaMesh.render();
                }
            }
            /*
            if (plantaCARRETILLA.health > 0)
            {
                //carretilla.render();
                plantaCentro.mesh.render();
            }
            */

         
            


            //mesh2.Transform = mesh2.Transform + Matrix.Scaling(new Vector3(25, 25, 25)) * Matrix.RotationY(16) * Matrix.Translation(mesh2.Position + new Vector3(25, 0, 25));
            zombie2.playAnimation("Empujar",true,2);
            
            zombie2.animateAndRender(0.1f);


            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            
            mesh3.playAnimation("Empujar", true, 2);
            mesh3.animateAndRender(0.1f);

            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            /*----------------------------------------------------------------------------------------------------------------------------------------*/
            
            //Cuando tenemos modelos mesh podemos utilizar un método que hace la matriz de transformación estándar.
            //Es útil cuando tenemos transformaciones simples, pero OJO cuando tenemos transformaciones jerárquicas o complicadas.
            //Mesh.UpdateMeshTransform();
            
            
            //Render de BoundingBox, muy útil para debug de colisiones.
            if (BoundingBox)
            {
                stage.Suelo.BoundingBox.render();
                //Mesh.BoundingBox.render();
                zombie2.BoundingBox.render();
                mesh3.BoundingBox.render();


                //carretilla.BoundingBox.render();
                // plantaCentro.mesh.BoundingBox.render();
                foreach (var plant in objetosColisionablesPLANTS)
                {
                    if (plant.health > 0)
                    {
                        plant.plantaMesh.BoundingBox.render();
                    }
                }


               
                //plantaEnMedio.mesh.BoundingBox.render();

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
            
            foreach (var plant in objetosColisionablesPLANTS)
            {
                if (plant.health <= 0)
                {
                   plant.plantaMesh.dispose();
                   objetosColisionablesPLANTS.Remove(plant);
                }
            }

            stage.disposeMesh();
            sol0.disposeMesh();
            SunFlo.disposeMeshSun();
           // repeater.disposeMesh();//  scene.disposeAll();

        }
    }
}
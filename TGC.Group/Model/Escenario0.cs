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

        //Suelo.
        private TgcBox Suelo { get; set; }

        //Fondo 
        private TgcBox Fondo { get; set; }


        
        //Meshes de Objetos del suelo--------------------------------------------------------///
        //private TgcMesh[] Planta = new TgcMesh[500];
        //private TgcMesh[] Planta1 = new TgcMesh[500];
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


        public override void Init()
        {


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
            var size = new Vector3(20000, 5, 20000);
            var sizefondo = new Vector3(20000, 20000, 20000); /////
            //Construimos una caja según los parámetros, por defecto la misma se crea con centro en el origen y se recomienda así para facilitar las transformaciones.
            Suelo = TgcBox.fromSize(size, texture);
            Fondo = TgcBox.fromSize(sizefondo, texturefondo);
            //Posición donde quiero que este la caja, es común que se utilicen estructuras internas para las transformaciones.
            //Entonces actualizamos la posición lógica, luego podemos utilizar esto en render para posicionar donde corresponda con transformaciones.
            Suelo.Position = new Vector3(-25, 0, 0);
            Fondo.Position = new Vector3(-25, 450, 0); /////


            //Cargo los  mesh que tiene la escena.
            //Mesh = new TgcSceneLoader().loadSceneFromFile(MediaDir + "LogoTGC-TgcScene.xml").Meshes[0];
            for (int i = 0; i < 500; i++)
            {
                //Planta[i] = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Planta3\\Planta3-TgcScene.xml").Meshes[0];
            }
            for (int i = 0; i < 500; i++)
            {
                //Planta1[i] = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Planta3\\Planta3-TgcScene.xml").Meshes[0];
            }
                carretilla = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\Carretilla\\Carretilla-TgcScene.xml").Meshes[0];
                reja = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
                reja1 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
                reja2 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];
                reja3 = new TgcSceneLoader().loadSceneFromFile(MediaDir + "\\RejaPinches\\RejaPinches-TgcScene.xml").Meshes[0];

            //pongo las rejas en posicion
            reja.rotateY(-44.9f);
            reja1.rotateY(-45f);


            for (int i = 0; i < 500; i++)
            {
                //Planta[i].Position = new Vector3(0+i*25*i^i, 3, i/2 +(-i*15*i));
            }

            for (int i = 0; i < 500; i++)
            {
                //Planta1[i].Position = new Vector3(0 + i * 25 *- i ^ i, 3, i + (i * 15 * i)); ;
            }
                    carretilla.Position = new Vector3(950, 3, -800);
            reja.Position = new Vector3(6980,3,8764);
            reja1.Position = new Vector3(10000,3,8864);
            reja2.Position = new Vector3(7500, 3, 134);
            reja3.Position = new Vector3(7500, 3, -1800);
            
            //Defino una escala en el modelo logico del mesh que es muy grande.
            //Mesh.Scale = new Vector3(0.5f, 0.5f, 0.5f);
            
            
            
            //Suelen utilizarse objetos que manejan el comportamiento de la camara.
            //Lo que en realidad necesitamos gráficamente es una matriz de View.
            //El framework maneja una cámara estática, pero debe ser inicializada.
            //Posición de la camara.
            var cameraPosition = new Vector3(50, 6383, 1024);
            //Quiero que la camara mire hacia el origen (0,0,0).
            var lookAt = new Vector3(1, 200, 200);
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



            if (Input.buttonPressed(TgcD3dInput.MouseButtons.BUTTON_RIGHT))
            {
                //Como ejemplo podemos hacer un movimiento simple de la cámara.
                //En este caso le sumamos un valor en Y
                Camara.SetCamera(Camara.Position + new Vector3(10f, 0, 0), Camara.LookAt);
                //Ver ejemplos de cámara para otras operaciones posibles.

                //Si superamos cierto Y volvemos a la posición original.
                if (Camara.Position.Y > 300f)
                {
                    Camara.SetCamera(new Vector3(Camara.Position.X, 0f, Camara.Position.Z), Camara.LookAt);
                }
            }

            if (Input.keyDown(Key.A))
            {

                Camara.SetCamera(Camara.Position + new Vector3(10, 0, 0), Camara.LookAt + new Vector3(10, 0, 0));
            }
            if (Input.keyDown(Key.D))
            {

                Camara.SetCamera(Camara.Position + new Vector3(-10, 0, 0), Camara.LookAt + new Vector3(-10, 0, 0));
            }
            if (Input.keyDown(Key.W))
            {

                Camara.SetCamera(Camara.Position + new Vector3(0, 0, -10), Camara.LookAt + new Vector3(0, 0, -10));
            }
            if (Input.keyDown(Key.S))
            {

                Camara.SetCamera(Camara.Position + new Vector3(0, 0, 10), Camara.LookAt + new Vector3(0, 0, 10));
            }
            

            if (Camara.Position.Z < -7826)
            {
                Camara.SetCamera(new Vector3(Camara.Position.X, Camara.Position.Y, -7826), Camara.LookAt + new Vector3(0, 0, 10));
            }


            if (Camara.Position.Z > 8774)
            {
                Camara.SetCamera(new Vector3(Camara.Position.X, Camara.Position.Y, 8774), Camara.LookAt + new Vector3(0, 0, -10));
            }

            if (Camara.Position.X >6990)
            {
                Camara.SetCamera(new Vector3(6990, Camara.Position.Y, Camara.Position.Z), Camara.LookAt + new Vector3(-10, 0, 0));
            }

            if (Camara.Position.X < -7810)
            {
                Camara.SetCamera(new Vector3(-7810, Camara.Position.Y, Camara.Position.Z), Camara.LookAt + new Vector3(10, 0, 0));
            }
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

            //Si hacen clic con el mouse, ver si hay colision con el suelo
            if (Input.buttonPressed(TgcD3dInput.MouseButtons.BUTTON_LEFT))
            {
                //Actualizar Ray de colisión en base a posición del mouse
                pickingRay.updateRay();

                //Detectar colisión Ray-AABB
                if (TgcCollisionUtils.intersectRayAABB(pickingRay.Ray, suelo0.BoundingBox, out newPosition))
                {
                    //Fijar nueva posición destino
                    applyMovement = true;

                    collisionPointMesh.Position = newPosition;
                    directionArrow.PEnd = new Vector3(newPosition.X, 30f, newPosition.Z);

                    //Rotar modelo en base a la nueva dirección a la que apunta
                    var direction = Vector3.Normalize(newPosition - mesh1.Position);
                    var angle = FastMath.Acos(Vector3.Dot(originalMeshRot, direction));
                    var axisRotation = Vector3.Cross(originalMeshRot, direction);
                    meshRotationMatrix = Matrix.RotationAxis(axisRotation, angle);
                }
            }
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
            reja2.Scale = new Vector3(70, 50, 50);
            reja3.Scale = new Vector3(70, 50, 50);

            carretilla.Scale = new Vector3(15, 15, 15);
            //A modo ejemplo realizamos toda las multiplicaciones, pero aquí solo nos hacia falta la traslación.
            //Finalmente invocamos al render de la caja
            Fondo.render();
            Suelo.render();
            carretilla.render();

            reja.render();
            reja1.render();
            reja2.render();
            reja3.render();
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
                //Planta[i].dispose();
            }
            for (int i = 0; i < 500; i++)
            {

                //Planta1[i].dispose();
            }
            carretilla.dispose();
            reja.dispose();
            reja1.dispose();
            reja2.render();
            reja3.render();

            //  scene.disposeAll();

        }
    }
}
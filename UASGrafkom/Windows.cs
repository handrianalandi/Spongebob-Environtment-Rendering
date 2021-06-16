using System;
using System.Collections.Generic;
using System.Text;
using LearnOpenTK.Common;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace UASGrafkom
{
    class Windows : GameWindow
    {
        //private float[] _vertices =
        //{
        //     //position          // texture
        //     0.5f,  0.5f,  0.0f, 1.0f, 1.0f, // top right
        //     0.5f, -0.5f,  0.0f, 1.0f, 0.0f, // bottom right
        //    -0.5f, -0.5f,  0.0f, 0.0f, 0.0f, // bottom left
        //    -0.5f,  0.5f,  0.0f, 0.0f, 1.0f, // top left

        //    // 0.5f,  0.5f,  0.5f, 0.0f, 0.0f, // top right
        //    // 0.5f, -0.5f,  0.5f, 0.0f, 0.0f, // bottom right
        //    //-0.5f, -0.5f,  0.5f, 0.0f, 0.0f, // bottom left
        //    //-0.5f,  0.5f,  0.5f, 0.0f, 0.0f, // top left

        //    // 0.5f,  0.5f, -0.5f, 0.0f, 0.0f, // top right
        //    // 0.5f, -0.5f, -0.5f, 0.0f, 0.0f, // bottom right
        //    //-0.5f, -0.5f, -0.5f, 0.0f, 0.0f, // bottom left
        //    //-0.5f,  0.5f, -0.5f, 0.0f, 0.0f  // top left
        //};

        //private uint[] _indices =
        //{
        //    // depan
        //    0, 1, 3,
        //    1, 2, 3,
        //    //// belakang
        //    //4, 5, 7,
        //    //5, 6, 7,
        //    //// atas
        //    //0, 3, 7,
        //    //0, 4, 7,
        //    //// bawah
        //    //1, 2, 6,
        //    //1, 5, 6,
        //    //// kiri
        //    //2, 3, 6,
        //    //3, 6, 7,
        //    //// kanan
        //    //0, 1, 5,
        //    //0, 4, 5
        //};

        //private int _vertexBufferObject;
        //private int _vertexArrayObject;
        //private Shader _shader;

        //private int _elementBufferObject;
        //private Texture _texture;

        // Camera

        Mesh object1 = new Mesh();//depan
        Mesh object2 = new Mesh();//blakang
        Mesh object3 = new Mesh();//kiri
        Mesh object4 = new Mesh();//kanan
        Mesh object5 = new Mesh();//atas

        private Vector3 posObject1 = new Vector3(0, 0, -2f);//depan
        private Vector3 posObject2 = new Vector3(0, 0, 2f);//blakang
        private Vector3 posObject3 = new Vector3(-2f, 0, 0);//kiri
        private Vector3 posObject4 = new Vector3(2f, 0, 0);//kanan
        private Vector3 posObject5 = new Vector3(0, 2f, 0);//atas
        private int countertime = 0;

        bool mataharidepan = true;
        private readonly float[] _vertices =
        {
             // Position          Normal
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Front face
             0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
             0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
            -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Back face
             0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
            -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Left face
            -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
            -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Right face
             0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Bottom face
             0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
             0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
            -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Top face
             0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
             0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
            -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
        };

        private Vector3 _lightPos = new Vector3(0f, 0f, 1f);
        private int _vertexBufferObject;
        private int _vaoModel;
        private int _vaoLamp;
        private Shader _lampShader;
        private Shader _lightingShader;

        Mesh testkotak = new Mesh();
        Mesh environment = new Mesh();
        Mesh rumahsquid = new Mesh();
        Mesh jalan = new Mesh();
        Mesh sofa = new Mesh();
        Mesh bantal1 = new Mesh();
        Mesh bantal2 = new Mesh();

        private Camera _camera;
        private Vector3 _objectPos;

        private Vector2 _lastMousePosition;
        private bool _firstmove = true;
        private float sensitivity = .1f;


        public Windows(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings)
        {
        }
        private Matrix4 generateArbRotationMatrix(Vector3 axis, Vector3 center, float degree)
        {
            var rads = MathHelper.DegreesToRadians(degree);

            var secretFormula = new float[4, 4] {
                { (float)Math.Cos(rads) + (float)Math.Pow(axis.X, 2) * (1 - (float)Math.Cos(rads)), axis.X* axis.Y * (1 - (float)Math.Cos(rads)) - axis.Z * (float)Math.Sin(rads),    axis.X * axis.Z * (1 - (float)Math.Cos(rads)) + axis.Y * (float)Math.Sin(rads),   0 },
                { axis.Y * axis.X * (1 - (float)Math.Cos(rads)) + axis.Z * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Y, 2) * (1 - (float)Math.Cos(rads)), axis.Y * axis.Z * (1 - (float)Math.Cos(rads)) - axis.X * (float)Math.Sin(rads),   0 },
                { axis.Z * axis.X * (1 - (float)Math.Cos(rads)) - axis.Y * (float)Math.Sin(rads),   axis.Z * axis.Y * (1 - (float)Math.Cos(rads)) + axis.X * (float)Math.Sin(rads),   (float)Math.Cos(rads) + (float)Math.Pow(axis.Z, 2) * (1 - (float)Math.Cos(rads)), 0 },
                { 0, 0, 0, 1}
            };
            var secretFormulaMatrix = new Matrix4(
                new Vector4(secretFormula[0, 0], secretFormula[0, 1], secretFormula[0, 2], secretFormula[0, 3]),
                new Vector4(secretFormula[1, 0], secretFormula[1, 1], secretFormula[1, 2], secretFormula[1, 3]),
                new Vector4(secretFormula[2, 0], secretFormula[2, 1], secretFormula[2, 2], secretFormula[2, 3]),
                new Vector4(secretFormula[3, 0], secretFormula[3, 1], secretFormula[3, 2], secretFormula[3, 3])
            );
            return secretFormulaMatrix;
        }
        protected override void OnLoad()
        {
            GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
            GL.Enable(EnableCap.DepthTest);


            //testing mesh
            //testkotak.LoadObjFile("../../../Assets/mentriangle.obj");
            //testkotak.setupObject((float)Size.X, (float)Size.Y, "lighting");
            //testkotak.setColor(new Vector3(0, 0, 1));
            //testkotak.scale(.1f);
            environment.LoadObjFile("../../../Assets/env.obj");
            environment.setupObject((float)Size.X, (float)Size.Y, "lighting");
            environment.setColor(new Vector3(1, 0, 0));

            jalan.LoadObjFile("../../../Assets/jalan.obj");
            jalan.setupObject((float)Size.X, (float)Size.Y, "lighting");
            jalan.setColor(new Vector3(0.6f, .17f, .17f));

            rumahsquid.LoadObjFile("../../../Assets/rumahsquidward.obj");
            rumahsquid.setupObject((float)Size.X, (float)Size.Y, "lighting");
            rumahsquid.setColor(new Vector3(0.6f, .17f, .17f));
            rumahsquid.translate(0, .3f, -.5f);

            sofa.LoadObjFile("../../../Assets/sofafix.obj");
            sofa.setColor(new Vector3(1, 0, 0));
            sofa.setupObject((float)Size.X, (float)Size.Y, "lighting");

            bantal1.LoadObjFile("../../../Assets/bantal.obj");
            bantal1.setColor(new Vector3(0, 1, 0));
            bantal1.setupObject((float)Size.X, (float)Size.Y, "lighting");
            bantal1.translate(-.105f, .08f, -.08f);

            bantal2.LoadObjFile("../../../Assets/bantal.obj");
            bantal2.setColor(new Vector3(0, 0, 1));
            bantal2.setupObject((float)Size.X, (float)Size.Y, "lighting");
            bantal2.translate(.095f, .08f, -.08f);

            sofa.child.Add(bantal1);
            sofa.child.Add(bantal2);

            //sofa.child.Add(environment);
            //testkotak.rotate();
            //testing mesh



            //light----------------------

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);
            GL.BufferData(BufferTarget.ArrayBuffer, _vertices.Length * sizeof(float), _vertices, BufferUsageHint.StaticDraw);
            _lightingShader = new Shader("../../../Shaders/shaderl.vert", "../../../Shaders/lighting.frag");
            _lampShader = new Shader("../../../Shaders/shaderl.vert", "../../../Shaders/shaderl.frag");

            //Initialize the vao for the model

            _vaoModel = GL.GenVertexArray();
            GL.BindVertexArray(_vaoModel);
            var vertexLocation = _lightingShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            //GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            // We now need to define the layout of the normal so the shader can use it
            var normalLocation = _lightingShader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            //Initialize the vao for the lamp, this is mostly the same as the code for the model cube

            _vaoLamp = GL.GenVertexArray();
            GL.BindVertexArray(_vaoLamp);
            var positionLocation = _lampShader.GetAttribLocation("aPos");
            GL.EnableVertexAttribArray(positionLocation);
            GL.VertexAttribPointer(positionLocation, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);


            //Set the vertex attributes(only position data for our lamp)
            vertexLocation = _lampShader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            //GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            ////lightend--------------------





            //object1.createBoxVertices(posObject1.X, posObject1.Y, posObject1.Z);
            //object1.setupObject((float)Size.X, (float)Size.Y, "");

            //object2.createBoxVertices(posObject2.X, posObject2.Y, posObject2.Z);
            //object2.setupObject((float)Size.X, (float)Size.Y, "");

            //object3.createBoxVertices(posObject3.X, posObject3.Y, posObject3.Z);
            //object3.setupObject((float)Size.X, (float)Size.Y, "");

            //object4.createBoxVertices(posObject4.X, posObject4.Y, posObject4.Z);
            //object4.setupObject((float)Size.X, (float)Size.Y, "");

            //object5.createBoxVertices(posObject5.X, posObject5.Y, posObject5.Z);
            //object5.setupObject((float)Size.X, (float)Size.Y, "");


            // Camera
            var _cameraPosInit = new Vector3(0, 0, 0);
            _camera = new Camera(_cameraPosInit, Size.X / (float)Size.Y);

            //_objectPos = posObject3;
            _camera.Yaw -= 90f;

            CursorGrabbed = true;
            base.OnLoad();
        }

        protected override void OnRenderFrame(FrameEventArgs args)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //_lampShader.Use();

            Matrix4 lampMatrix = Matrix4.CreateScale(0.2f); // We scale the lamp cube down a bit to make it less dominant
            lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightPos);

            _lampShader.SetMatrix4("transform", lampMatrix);
            _lampShader.SetMatrix4("view", _camera.GetViewMatrix());
            _lampShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            //GL.DrawArrays(PrimitiveType.TriangleFan, 0, 36);
            //object1.render(_camera, new Vector3(1, 0, 0));
            //object2.render(_camera, new Vector3(0, 1, 0));
            //object3.render(_camera, new Vector3(0, 0, 1));
            //object4.render(_camera, new Vector3(1, 1, 0));
            //object5.render(_camera, new Vector3(0, 1, 1));

            //_lightingShader.SetMatrix4("transform", Matrix4.Identity);
            //_lightingShader.SetMatrix4("view", _camera.GetViewMatrix());
            //_lightingShader.SetMatrix4("projection", _camera.GetProjectionMatrix());

            //_lightingShader.SetVector3("objectColor", new Vector3(1.0f, 0.5f, 0.31f));
            //_lightingShader.SetVector3("lightColor", new Vector3(1.0f, 1.0f, 1.0f));
            //_lightingShader.SetVector3("lightPos", _lightPos);
            //_lightingShader.SetVector3("viewPos", _camera.Position);

            ////light------------------

            //GL.BindVertexArray(_vaoModel);

            //_lightingShader.Use();
            //GL.DrawArrays(PrimitiveType.Triangles, 0, 36);

            // Matrix4.Identity is used as the matrix, since we just want to draw it at 0, 0, 0
            //testkotak.render(_camera, new Vector3(1f, 0, 0), new Vector3(1f, 1f, 1f), _lightPos);
            //environment.render(_camera, new Vector3(1f, 1f, 1f), _lightPos);
            jalan.render(_camera, new Vector3(1f, 1f, 1f), _lightPos);
            //testkotak.render(_camera , new Vector3(1f, 1f, 1f), _lightPos);

            rumahsquid.render(_camera, new Vector3(1f, 1f, 1f), _lightPos);

            //sofa.render(_camera, new Vector3(1f, 1f, 1f), _lightPos);

            //bantal1.render(_camera, new Vector3(1f, 1f, 1f), _lightPos);
            //environment.scale(.5f);
            //if (countertime == 1000)
            //{
            //    testkotak.rotate();
            //    countertime = 0;
            //}

            //else
            //{
            //    countertime++;
            //}

            // Draw the lamp, this is mostly the same as for the model cube

            //GL.BindVertexArray(_vaoLamp);


            //lightend--------------

            SwapBuffers();

            base.OnRenderFrame(args);
        }

        protected override void OnUpdateFrame(FrameEventArgs args)
        {
            const float cameraSpeed = 1.5f;
            // Escape keyboard
            if (KeyboardState.IsKeyDown(Keys.Escape))
            {
                Close();
            }
            // Zoom in
            if (KeyboardState.IsKeyDown(Keys.I))
            {
                _camera.Fov -= 0.05f;
            }
            // Zoom out
            if (KeyboardState.IsKeyDown(Keys.O))
            {
                _camera.Fov += 0.05f;
            }

            //rotasi kamera pakai keyboard
            //rotasi x di pivot(pitch)
            //lihat ke atas(T)
            if (KeyboardState.IsKeyDown(Keys.T))
            {
                _camera.Pitch += 0.05f;
            }
            //lihat ke bawah(G)
            if (KeyboardState.IsKeyDown(Keys.G))
            {
                _camera.Pitch -= 0.05f;
            }
            //lihat ke kiri(F)
            if (KeyboardState.IsKeyDown(Keys.F))
            {
                _camera.Yaw -= 0.05f;
            }
            //lihat ke kanan(H)
            if (KeyboardState.IsKeyDown(Keys.H))
            {
                _camera.Yaw += 0.05f;
            }

            //pindahin posisi kamera
            //maju (W)
            if (KeyboardState.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)args.Time;
            }
            //mundur (S)
            if (KeyboardState.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)args.Time;
            }
            //kiri (A)
            if (KeyboardState.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)args.Time;
            }
            //kanan (D)
            if (KeyboardState.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)args.Time;
            }

            //naik turun
            //naik (spasi)
            if (KeyboardState.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)args.Time;
            }
            //turun (ctrl)
            if (KeyboardState.IsKeyDown(Keys.LeftControl))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)args.Time;
            }

            const float _rotationSpeed = .02f;
            // M (bawah -> rotasi sumbu x)
            if (KeyboardState.IsKeyDown(Keys.M))
            {
                _objectPos *= 2;
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objectPos;
                _camera.Pitch += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }

            // K (atas  -> rotasi sumbu x)
            if (KeyboardState.IsKeyDown(Keys.K))
            {
                _objectPos *= 2;
                var axis = new Vector3(1, 0, 0);
                _camera.Position -= _objectPos;
                _camera.Pitch -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }
            // N (kiri  -> rotasi sumbu y)
            if (KeyboardState.IsKeyDown(Keys.N))
            {
                _objectPos *= 2;
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objectPos;
                _camera.Yaw += _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }
            // , (kanan -> rotasi sumbu y)
            if (KeyboardState.IsKeyDown(Keys.Comma))
            {
                _objectPos *= 2;
                var axis = new Vector3(0, 1, 0);
                _camera.Position -= _objectPos;
                _camera.Yaw -= _rotationSpeed;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }


            // J (putar -> rotasi sumbu z)
            if (KeyboardState.IsKeyDown(Keys.J))
            {
                _objectPos *= 2;
                var axis = new Vector3(0, 0, 1);
                _camera.Position -= _objectPos;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, _rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }

            // L (putar -> rotasi sumbu z)
            if (KeyboardState.IsKeyDown(Keys.L))
            {

                _objectPos *= 2;
                var axis = new Vector3(0, 0, 1);
                _camera.Position -= _objectPos;
                _camera.Position = Vector3.Transform(_camera.Position,
                    generateArbRotationMatrix(axis, _objectPos, -_rotationSpeed).ExtractRotation());
                _camera.Position += _objectPos;

                _camera._front = -Vector3.Normalize(_camera.Position - _objectPos);
                _objectPos /= 2;
            }
            if (KeyboardState.IsKeyDown(Keys.B))
            {
                Console.WriteLine(bantal1.getRealPos());
                Console.WriteLine(bantal1.scalefloat);
                sofa.rotateall(1f, 0, 0);
            }
            if (KeyboardState.IsKeyPressed(Keys.Left))
            {
                sofa.move(.01f, 0, 0);
            }
            if (KeyboardState.IsKeyPressed(Keys.Right))
            {
                sofa.move(-.01f, 0, 0);
            }
            if (KeyboardState.IsKeyPressed(Keys.Up))
            {
                sofa.move(0, 0, 0.01f);
            }
            if (KeyboardState.IsKeyPressed(Keys.Down))
            {
                sofa.move(0, 0, -0.01f);
            }
            if (KeyboardState.IsKeyDown(Keys.E))
            {
                sofa.rotateall(0, .1f);
            }
            if (KeyboardState.IsKeyDown(Keys.Q))
            {
                sofa.rotateall(0, -.1f);
            }
            //plus sens
            if (KeyboardState.IsKeyReleased(Keys.PageUp))
            {
                sensitivity += .01f;
                if (sensitivity >= 1f)
                {
                    sensitivity = .99f;
                }
            }

            //min sens
            if (KeyboardState.IsKeyReleased(Keys.PageDown))
            {
                sensitivity -= .01f;
                if (sensitivity <= 0f)
                {
                    sensitivity = .01f;
                }
            }

            if (KeyboardState.IsKeyPressed(Keys.Z))
            {
                if (mataharidepan)
                {
                    _lightPos.Z -= .1f;
                    _lightPos.Y += .1f;
                }
                else
                {
                    _lightPos.Z -= .1f;
                    _lightPos.Y -= .1f;
                }
                if(_lightPos.Y >= 1.5f)
                {
                    mataharidepan = false;
                }
                //_camera.Position = _lightPos;
            }


            //rotasi pakai mouse
            if (!IsFocused)
            {
                return;
            }
            if (_firstmove)
            {
                _lastMousePosition = new Vector2(MouseState.X, MouseState.Y);
                _firstmove = false;
            }
            else
            {
                //calc selisih mouse pos
                var deltaX = MouseState.X - _lastMousePosition.X;
                var deltaY = MouseState.Y - _lastMousePosition.Y;
                _lastMousePosition = new Vector2(MouseState.X, MouseState.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }

            base.OnUpdateFrame(args);
        }
    }
}

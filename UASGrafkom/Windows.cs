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
        

        // Camera

        
        private Vector3 _sunPos = new Vector3(0f, 5f, 5f);
        

        
        Mesh tanah = new Mesh();
        Mesh jalan = new Mesh();
        Mesh rumahsquid = new Mesh();
        Mesh mataharisun = new Mesh();
        

        private Camera _camera;
        private Vector3 _objectPos;

        private Vector2 _lastMousePosition;
        private bool _firstmove = true;
        private float sensitivity = .1f;
        bool mataharidepan = true;


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
            GL.ClearColor(0.235f, 0.7f, 0.9f, 1.0f);
            GL.Enable(EnableCap.DepthTest);

            mataharisun.LoadObjFile("../../../Assets/bantal.obj");
            mataharisun.setupObject((float)Size.X, (float)Size.Y, "lighting");
            mataharisun.setColor(new Vector3(1, 1, 1));
            mataharisun.translate(_sunPos.X, _sunPos.Y, _sunPos.Z);

            tanah.LoadObjFile("../../../Assets/tanah.obj");
            tanah.setupObject((float)Size.X, (float)Size.Y, "lighting");
            tanah.setColor(new Vector3((float)208/255, (float)210 / 255, (float)170 / 255));
            tanah.scale(.5f);
            tanah.setAmbientStg(.4f);
            tanah.setShininess(1);

            jalan.LoadObjFile("../../../Assets/jalan.obj");
            jalan.setupObject((float)Size.X, (float)Size.Y, "lighting");
            jalan.setColor(new Vector3((float)104 / 255, (float)122 / 255, (float)130 / 255));
            jalan.setAmbientStg(.6f);
            jalan.setShininess(4);
            jalan.translate(0, .3f, 1f);
            jalan.scale(.3f);





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
            

            mataharisun.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);

            tanah.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);
            jalan.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);


            

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
                Console.WriteLine(mataharisun.getRealPos());
                Console.WriteLine(mataharisun.scalefloat);
                tanah.rotateall(1f, 0, 0);
            }
            if (KeyboardState.IsKeyPressed(Keys.Left))
            {
                tanah.move(.01f, 0, 0);
            }
            if (KeyboardState.IsKeyPressed(Keys.Right))
            {
                tanah.move(-.01f, 0, 0);
            }
            if (KeyboardState.IsKeyPressed(Keys.Up))
            {
                tanah.move(0, 0, 0.01f);
            }
            if (KeyboardState.IsKeyPressed(Keys.Down))
            {
                tanah.move(0, 0, -0.01f);
            }
            if (KeyboardState.IsKeyDown(Keys.E))
            {
                tanah.rotateall(0, .1f);
            }
            if (KeyboardState.IsKeyDown(Keys.Q))
            {
                tanah.rotateall(0, -.1f);
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
                    _sunPos.Z -= .1f;
                    _sunPos.Y += .1f;
                }
                else
                {
                    _sunPos.Z -= .1f;
                    _sunPos.Y -= .1f;
                }
                if(_sunPos.Y >= 1.5f)
                {
                    mataharidepan = false;
                }
                //_camera.Position = _sunPos;
            }
            if (KeyboardState.IsKeyPressed(Keys.C))
            {
                _camera.Position = jalan.getRealPos();
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

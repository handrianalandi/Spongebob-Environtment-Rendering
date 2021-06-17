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
        Mesh mataharisun = new Mesh();

        //rumah squidward
        Mesh rumahsquidwardbase = new Mesh();
        Mesh rumahsquidwardmodif = new Mesh();
        Mesh rumahsquidwardmata = new Mesh();
        Mesh rumahsquidwardpaku = new Mesh();
        Mesh rumahsquidwardpintu = new Mesh();
        Mesh rumahsquidwardgagangpintu= new Mesh();
        Mesh rumahsquidwardkaca= new Mesh();

        //rumah spongebob
        Mesh rumahspongbase = new Mesh();
        Mesh kacajendelapinturumahspong = new Mesh();
        Mesh pipabiru = new Mesh();
        Mesh bungahijau = new Mesh();
        Mesh bungakuning = new Mesh();
        Mesh bungapink = new Mesh();



        //rumah spongebob
        Mesh rumahpatrick = new Mesh();
      


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
        protected void createRumahSquidward()
        {
            rumahsquidwardbase.LoadObjFile("../../../Assets/rsquidwardbase.obj");
            rumahsquidwardbase.setupObject((float)Size.X, (float)Size.Y, "lighting");
            rumahsquidwardbase.setColor(new Vector3((float)0 / 255, (float)40 / 255, (float)89 / 255));
            rumahsquidwardbase.setAmbientStg(.3f);
            rumahsquidwardbase.setShininess(1);
            rumahsquidwardbase.setSpecularStg(.4f);
            rumahsquidwardbase.translate(-.05f, .3f, -.2f);
            rumahsquidwardbase.scale(.7f);

            rumahsquidwardmodif.LoadObjFile("../../../Assets/rsquidwardmodif.obj");
            rumahsquidwardmodif.setupObject((float)Size.X, (float)Size.Y, "lighting");
            rumahsquidwardmodif.setColor(new Vector3((float)0 / 255, (float)60 / 255, (float)109 / 255));
            rumahsquidwardmodif.setAmbientStg(.3f);
            rumahsquidwardmodif.setShininess(1);
            rumahsquidwardmodif.setSpecularStg(.4f);
            rumahsquidwardmodif.translate(-.05f, .3f, -.2f);
            rumahsquidwardmodif.scale(.7f);

            rumahsquidwardmata.LoadObjFile("../../../Assets/rsquidwardmata.obj");
            rumahsquidwardmata.setupObject((float)Size.X, (float)Size.Y, "lighting");
            rumahsquidwardmata.setColor(new Vector3((float)0 / 255, (float)100 / 255, (float)149 / 255));
            rumahsquidwardmata.setAmbientStg(.2f);
            rumahsquidwardmata.setShininess(1);
            rumahsquidwardmata.setSpecularStg(.2f);
            rumahsquidwardmata.translate(-.05f, .3f, -.2f);
            rumahsquidwardmata.scale(.7f);

            rumahsquidwardpaku.LoadObjFile("../../../Assets/rsquidwardpaku.obj");
            rumahsquidwardpaku.setupObject((float)Size.X, (float)Size.Y, "lighting");
            rumahsquidwardpaku.setColor(new Vector3((float)0 / 255, (float)60 / 255, (float)109 / 255));
            rumahsquidwardpaku.setAmbientStg(.5f);
            rumahsquidwardpaku.setShininess(128);
            rumahsquidwardpaku.setSpecularStg(.9f);
            rumahsquidwardpaku.translate(-.05f, .3f, -.2f);
            rumahsquidwardpaku.scale(.7f);

            rumahsquidwardpintu.LoadObjFile("../../../Assets/rsquidwardpintu.obj");
            rumahsquidwardpintu.setupObject((float)Size.X, (float)Size.Y, "lighting");
            rumahsquidwardpintu.setColor(new Vector3((float)184 / 255, (float)109 / 255, (float)49 / 255));
            rumahsquidwardpintu.setAmbientStg(.9f);
            rumahsquidwardpintu.setShininess(2);
            rumahsquidwardpintu.setSpecularStg(.2f);
            rumahsquidwardpintu.translate(-.05f, .3f, -.2f);
            rumahsquidwardpintu.scale(.7f);


            rumahsquidwardgagangpintu.LoadObjFile("../../../Assets/rsquidwardgagangpintu.obj");
            rumahsquidwardgagangpintu.setupObject((float)Size.X, (float)Size.Y, "lighting");
            rumahsquidwardgagangpintu.setColor(new Vector3((float)164 / 255, (float)99 / 255, (float)29 / 255));
            rumahsquidwardgagangpintu.setAmbientStg(.5f);
            rumahsquidwardgagangpintu.setShininess(128);
            rumahsquidwardgagangpintu.setSpecularStg(.5f);
            rumahsquidwardgagangpintu.translate(-.05f, .3f, -.2f);
            rumahsquidwardgagangpintu.scale(.7f);

            rumahsquidwardkaca.LoadObjFile("../../../Assets/rsquidwardkaca.obj");
            rumahsquidwardkaca.setupObject((float)Size.X, (float)Size.Y, "lighting");
            rumahsquidwardkaca.setColor(new Vector3((float)0 / 255, (float)180 / 255, (float)229 / 255));
            rumahsquidwardkaca.setAmbientStg(.1f);
            rumahsquidwardkaca.setShininess(128);
            rumahsquidwardkaca.setSpecularStg(.5f);
            rumahsquidwardkaca.translate(-.05f, .3f, -.2f);
            rumahsquidwardkaca.scale(.7f);


            rumahsquidwardbase.child.Add(rumahsquidwardmodif);
            rumahsquidwardbase.child.Add(rumahsquidwardmata);
            rumahsquidwardbase.child.Add(rumahsquidwardpaku);
            rumahsquidwardbase.child.Add(rumahsquidwardpintu);
            rumahsquidwardbase.child.Add(rumahsquidwardgagangpintu);
            rumahsquidwardbase.child.Add(rumahsquidwardkaca);
        }

        protected void createRumahSpongebob()
        {
            rumahspongbase.LoadObjFile("../../../Assets/rumahspong.obj");
            rumahspongbase.setupObject((float)Size.X, (float)Size.Y, "lighting");
            rumahspongbase.setColor(new Vector3((float)225 / 255, (float)183 / 255, (float)20 / 255));
            rumahspongbase.setAmbientStg(.1f);
            rumahspongbase.setShininess(1);
            rumahspongbase.setSpecularStg(.4f);
            rumahspongbase.translate(19.5f, 15.0f, -8.0f);
            rumahspongbase.scale(.013f);

            
            kacajendelapinturumahspong.LoadObjFile("../../../Assets/kacajendelapinturumahspong.obj");
            kacajendelapinturumahspong.setupObject((float)Size.X, (float)Size.Y, "lighting");
            kacajendelapinturumahspong.setColor(new Vector3((float)195 / 255, (float)195 / 255, (float)195 / 255));
            kacajendelapinturumahspong.setAmbientStg(.3f);
            kacajendelapinturumahspong.setShininess(1);
            kacajendelapinturumahspong.setSpecularStg(.4f);
            kacajendelapinturumahspong.translate(-.007f, .12f, -.51f);
            kacajendelapinturumahspong.scale(.5f);

            pipabiru.LoadObjFile("../../../Assets/pipabiru.obj");
            pipabiru.setupObject((float)Size.X, (float)Size.Y, "lighting");
            pipabiru.setColor(new Vector3((float)29 / 255, (float)152 / 255, (float)182 / 255));
            pipabiru.setAmbientStg(.3f);
            pipabiru.setShininess(1);
            pipabiru.setSpecularStg(.4f);
            pipabiru.translate(.785f, .543f, -.205f);
            pipabiru.scale(.45f);

            bungahijau.LoadObjFile("../../../Assets/bungahijau.obj");
            bungahijau.setupObject((float)Size.X, (float)Size.Y, "lighting");
            bungahijau.setColor(new Vector3((float)0 / 255, (float)243 / 255, (float)0 / 255));
            bungahijau.setAmbientStg(.3f);
            bungahijau.setShininess(1);
            bungahijau.setSpecularStg(.4f);
            bungahijau.translate(.545f, .255f, .03f);
            bungahijau.scale(.45f);

            bungakuning.LoadObjFile("../../../Assets/bungakuning.obj");
            bungakuning.setupObject((float)Size.X, (float)Size.Y, "lighting");
            bungakuning.setColor(new Vector3((float)254 / 255, (float)210 / 255, (float)80 / 255));
            bungakuning.setAmbientStg(.3f);
            bungakuning.setShininess(1);
            bungakuning.setSpecularStg(.4f);
            bungakuning.translate(.545f, .255f, .03f);
            bungakuning.scale(.45f);

            bungapink.LoadObjFile("../../../Assets/bungapink.obj");
            bungapink.setupObject((float)Size.X, (float)Size.Y, "lighting");
            bungapink.setColor(new Vector3((float)255 / 255, (float)120 / 255, (float)11 / 255));
            bungapink.setAmbientStg(.3f);
            bungapink.setShininess(1);
            bungapink.setSpecularStg(.4f);
            bungapink.translate(.545f, .255f, .03f);
            bungapink.scale(.45f);

            rumahspongbase.child.Add(kacajendelapinturumahspong);
            rumahspongbase.child.Add(pipabiru);
            rumahspongbase.child.Add(bungahijau);
            rumahspongbase.child.Add(bungakuning);
            rumahspongbase.child.Add(bungapink);


        }

        protected void createRumahPatrick()
        {
            rumahpatrick.LoadObjFile("../../../Assets/rumahpatrick.obj");
            rumahpatrick.setupObject((float)Size.X, (float)Size.Y, "lighting");
            rumahpatrick.setColor(new Vector3((float)76 / 255, (float)1 / 255, (float)28 / 255));
            rumahpatrick.setAmbientStg(.1f);
            rumahpatrick.setShininess(1);
            rumahpatrick.setSpecularStg(.4f);
            rumahpatrick.translate(-25.5f, 11.0f, 1.0f);
            rumahpatrick.scale(.013f);

           
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
            tanah.setSpecularStg(.1f);

            jalan.LoadObjFile("../../../Assets/jalan.obj");
            jalan.setupObject((float)Size.X, (float)Size.Y, "lighting");
            jalan.setColor(new Vector3((float)104 / 255, (float)122 / 255, (float)130 / 255));
            jalan.setAmbientStg(.6f);
            jalan.setShininess(4);
            jalan.translate(0, .3f, 1f);
            jalan.scale(.3f);

            //rumah squidward
            createRumahSquidward();

            //rumah spongebob
            createRumahSpongebob();

            //rumah patrick
            createRumahPatrick();



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

            //rumah squidward
            rumahsquidwardbase.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);

            //rumah spongebob
            rumahspongbase.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);
            kacajendelapinturumahspong.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);
            pipabiru.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);
            bungahijau.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);
            bungakuning.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);
            bungapink.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);



            //rumah patrick
            rumahpatrick.render(_camera, new Vector3(1f, 1f, 1f), _sunPos);


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
                rumahsquidwardbase.backtozero();
                rumahsquidwardbase.rotateall(0, .5f, 0);
                rumahsquidwardbase.backtonormal();
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
                _camera.Position = rumahsquidwardmodif.getRealPos();
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

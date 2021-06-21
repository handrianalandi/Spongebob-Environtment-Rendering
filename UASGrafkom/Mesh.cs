using LearnOpenTK.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OpenTK.Graphics.OpenGL4;

namespace UASGrafkom
{
    class Mesh
    {
        //Vector 3 pastikan menggunakan OpenTK.Mathematics
        //tanpa protected otomatis komputer menganggap sebagai private
        List<float> realVertices = new List<float>();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> textureVertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<uint> vertexIndices = new List<uint>();
        public Vector3 position;
        public float scalefloat;
        public float degree;
        Vector3 _color;
        int _vertexBufferObject;
        int _elementBufferObject;
        int _vertexArrayObject;
        Shader _shader;
        Matrix4 transform;
        Matrix4 view;
        Matrix4 projection;
        float ambientStrength = .2f;
        float shininess = 32;
        float specularStrength = .5f;

        public Vector3 rotation;

        public List<Mesh> child = new List<Mesh>();
        public Mesh()
        {
            position = new Vector3(0, 0, 0);
            scalefloat = 1;
            degree = 0;

            rotation.X = 0;
            rotation.Y = 0;
            rotation.Z = 0;
        }
        public void move(float x, float y, float z)
        {
            transform = transform * Matrix4.CreateScale(1 / scalefloat);
            transform = transform * Matrix4.CreateTranslation(x, y, z);
            transform = transform * Matrix4.CreateScale(scalefloat);
            position.X += x;
            position.Y += y;
            position.Z += z;
            foreach (var meshobj in child)
            {
                meshobj.move(x, y, z);
            }
        }
        public void backtozero()
        {
            transform = transform * Matrix4.CreateScale(1 / scalefloat);
            transform = transform * Matrix4.CreateTranslation(-position.X, -position.Y, -position.Z);
            foreach (var meshobj in child)
            {
                meshobj.backtozero();
            }
        }
        public void backtonormal()
        {
            transform = transform * Matrix4.CreateTranslation(position.X, position.Y, position.Z);
            transform = transform * Matrix4.CreateScale(scalefloat);
            foreach (var meshobj in child)
            {
                meshobj.backtonormal();
            }
        }
        public Vector3 getRealPos()
        {
            Vector3 hasil = new Vector3(position.X * scalefloat, position.Y * scalefloat, position.Z * scalefloat);
            return hasil;
        }
        public void setColor(Vector3 colors)
        {
            _color = colors;
        }
        public void setAmbientStg(float strenght)
        {
            ambientStrength = strenght;
        }
        public void setShininess(float shine)
        {
            shininess = shine;
        }
        public void setSpecularStg(float spec)
        {
            specularStrength = spec;
        }
        public void setupObject(float Sizex, float Sizey, string abc)
        {
            transform = Matrix4.Identity;

            _vertexBufferObject = GL.GenBuffer();
            GL.BindBuffer(BufferTarget.ArrayBuffer, _vertexBufferObject);

            GL.BufferData<float>(BufferTarget.ArrayBuffer,
                realVertices.Count * sizeof(float),
                realVertices.ToArray(),
                BufferUsageHint.StaticDraw);
            _shader = new Shader("../../../Shaders/lighting.vert", "../../../Shaders/" + abc + ".frag");

            _vertexArrayObject = GL.GenVertexArray();
            GL.BindVertexArray(_vertexArrayObject);


            var vertexLocation = _shader.GetAttribLocation("aPosition");
            GL.EnableVertexAttribArray(vertexLocation);
            GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);

            var normalLocation = _shader.GetAttribLocation("aNormal");
            GL.EnableVertexAttribArray(normalLocation);
            GL.VertexAttribPointer(normalLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 3 * sizeof(float));

            view = Matrix4.CreateTranslation(0.0f, 0.0f, -3.0f);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45f), Sizex / Sizey, 0.1f, 100.0f);

        }
        public void render(Camera _camera, Vector3 _lightColor, Vector3 _lightPos)
        {
            _shader.Use();

            GL.BindVertexArray(_vertexArrayObject);

            _shader.SetMatrix4("transform", transform);
            _shader.SetMatrix4("view", _camera.GetViewMatrix());
            _shader.SetMatrix4("projection", _camera.GetProjectionMatrix());


            _shader.SetVector3("objectColor", _color);
            _shader.SetVector3("lightColor", _lightColor);
            _shader.SetVector3("lightPos", _lightPos);
            _shader.SetVector3("viewPos", _camera.Position);

            _shader.SetFloat("ambientStg", ambientStrength);
            _shader.SetFloat("shininess", shininess);
            _shader.SetFloat("specularStg", specularStrength);


            GL.DrawArrays(PrimitiveType.Triangles, 0, realVertices.Count / 2);

            foreach (var meshobj in child)
            {
                meshobj.render(_camera, _lightColor, _lightPos);
            }
        }
        public List<Vector3> getVertices()
        {
            return vertices;
        }
        public List<uint> getVertexIndices()
        {
            return vertexIndices;
        }

        public void setVertexIndices(List<uint> temp)
        {
            vertexIndices = temp;
        }
        public int getVertexBufferObject()
        {
            return _vertexBufferObject;
        }

        public int getElementBufferObject()
        {
            return _elementBufferObject;
        }

        public int getVertexArrayObject()
        {
            return _vertexArrayObject;
        }

        public Shader getShader()
        {
            return _shader;
        }

        public Matrix4 getTransform()
        {
            return transform;
        }

        public void rotate(float x = 0, float y = 0, float z = 0)
        {
            backtozero();
            transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(x));
            transform = transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(y));
            transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(z));
            degree += y;
            degree %= 360;
            backtonormal();

        }
        public void rotateall(float x = 0, float y = 0, float z = 0)
        {
            transform = transform * Matrix4.CreateRotationX(MathHelper.DegreesToRadians(x));
            transform = transform * Matrix4.CreateRotationY(MathHelper.DegreesToRadians(y));
            transform = transform * Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(z));
            degree += y;
            degree %= 360;

            rotation.X += x;
            rotation.Y += y;
            rotation.Z += z;

            foreach (var meshobj in child)
            {
                meshobj.rotateall(x, y, z);
            }
        }

        public void scale(float scaling)
        {
            transform = transform * Matrix4.CreateScale(scaling);
            scalefloat *= scaling;

            foreach (var meshobj in child)
            {
                meshobj.scale(scaling);
            }
        }
        public void translate(float x, float y, float z)
        {
            transform = transform * Matrix4.CreateTranslation(x, y, z);
            position.X += x;
            position.Y += y;
            position.Z += z;
            foreach (var meshobj in child)
            {
                meshobj.translate(x, y, z);
            }
        }

        public void LoadObjFile(string path)
        {
            //komputer ngecek, apakah file bisa diopen atau tidak
            if (!File.Exists(path))
            {
                //mengakhiri program dan kita kasih peringatan
                throw new FileNotFoundException("Unable to open \"" + path + "\", does not exist.");
            }
            //lanjut ke sini
            using (StreamReader streamReader = new StreamReader(path))
            {
                while (!streamReader.EndOfStream)
                {
                    //aku ngambil 1 baris tersebut -> dimasukkan ke dalam List string -> dengan di split pakai spasi
                    List<string> words = new List<string>(streamReader.ReadLine().ToLower().Split(' '));
                    //removeAll(kondisi dimana penghapusan terjadi)
                    words.RemoveAll(s => s == string.Empty);
                    //Melakukan pengecekkan apakah dalam satu list -> ada isinya atau tidak list nya tersebut
                    //kalau ada continue, perintah-perintah yang ada dibawahnya tidak akan dijalankan 
                    //dan dia bakal kembali keatas lagi / melanjutkannya whilenya
                    if (words.Count == 0)
                        continue;

                    //System.Console.WriteLine("New While");
                    //foreach (string x in words)
                    //               {
                    //	System.Console.WriteLine("tes");
                    //	System.Console.WriteLine(x);
                    //               }

                    string type = words[0];
                    //remove at -> menghapus data dalam suatu indexs dan otomatis data pada indeks
                    //berikutnya itu otomatis mundur kebelakang 1
                    words.RemoveAt(0);


                    switch (type)
                    {
                        // vertex
                        //parse merubah dari string ke tipe variabel yang diinginkan
                        //ada /10 karena saaat ini belum masuk materi camera

                        case "v":
                            vertices.Add(new Vector3(float.Parse(words[0]) / 10, float.Parse(words[1]) / 10, float.Parse(words[2]) / 10));
                            break;

                        case "vt":
                            textureVertices.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]),
                                                            words.Count < 3 ? 0 : float.Parse(words[2])));
                            break;

                        case "vn":
                            normals.Add(new Vector3(float.Parse(words[0]), float.Parse(words[1]), float.Parse(words[2])));
                            break;
                        // face
                        case "f":
                            foreach (string w in words)
                            {
                                if (w.Length == 0)
                                    continue;
                                string[] comps = w.Split('/');

                                //vertices
                                realVertices.Add(vertices[int.Parse(comps[0]) - 1].X);
                                realVertices.Add(vertices[int.Parse(comps[0]) - 1].Y);
                                realVertices.Add(vertices[int.Parse(comps[0]) - 1].Z);

                                //normal
                                realVertices.Add(normals[int.Parse(comps[2]) - 1].X);
                                realVertices.Add(normals[int.Parse(comps[2]) - 1].Y);
                                realVertices.Add(normals[int.Parse(comps[2]) - 1].Z);
                            }
                            break;

                        default:
                            break;
                    }
                }
            }
        }

    }
}

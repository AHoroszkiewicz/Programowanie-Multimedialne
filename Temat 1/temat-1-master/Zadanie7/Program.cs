using OpenTK;
using OpenTK.Graphics.OpenGL4;
using GLFW;
using GlmSharp;

using Shaders;
using Models;

namespace PMLabs
{
    //Implementacja interfejsu dostosowującego metodę biblioteki Glfw służącą do pozyskiwania adresów funkcji i procedur OpenGL do współpracy z OpenTK.
    public class BC : IBindingsContext
    {
        public IntPtr GetProcAddress(string procName)
        {
            return Glfw.GetProcAddress(procName);
        }
    }

    class Program
    {
        public static Torus torus = new Torus();
        public static Teapot teapot = new Teapot();
        public static Sphere sphere = new Sphere();
        public static Cube cube = new Cube();

        public static void InitOpenGLProgram(Window window)
        {
            // Czyszczenie okna na kolor czarny
            GL.ClearColor(0, 0, 0, 1);

            // Ładowanie programów cieniujących
            DemoShaders.InitShaders("Shaders\\");
        }

        public static void DrawScene(Window window, float time)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            mat4 V = mat4.LookAt(
                new vec3(0.0f, 0.0f, -5.0f),
                new vec3(0.0f, 0.0f, 0.0f),
                new vec3(0.0f, 1.0f, 0.0f));
            mat4 P = mat4.Perspective(glm.Radians(50.0f), 1.0f, 1.0f, 50.0f);

            DemoShaders.spConstant.Use();
            GL.UniformMatrix4(DemoShaders.spConstant.U("P"), 1, false, P.Values1D);
            GL.UniformMatrix4(DemoShaders.spConstant.U("V"), 1, false, V.Values1D);

            mat4 M = mat4.Identity;

            //torus1
            M = mat4.Translate(-1f, 0.0f, 0.0f) * mat4.RotateZ(-time);
            GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);
            torus.drawWire();

            for (int i = 0; i < 12; i++)
            {
                mat4 M1 = M;
                M1 *= mat4.RotateZ(glm.Radians(30f * i)) * mat4.Translate(1f, 0, 0) * mat4.Scale(0.1f, 0.1f, 0.1f);
                GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M1.Values1D);
                cube.drawWire();
            }

            //torus2
            M = mat4.Translate(1f, 0.0f, 0.0f) * mat4.RotateZ(time);
            GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);
            torus.drawWire();

            for (int i = 0; i < 12; i++)
            {
                mat4 M1 = M;
                M1 *= mat4.RotateZ(glm.Radians((30f * i) + 15)) * mat4.Translate(1f, 0, 0) * mat4.Scale(0.1f, 0.1f, 0.1f);
                GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M1.Values1D);
                cube.drawWire();
            }

            Glfw.SwapBuffers(window);
        }

        public static void FreeOpenGLProgram(Window window)
        {
            // Możesz dodać odpowiednie czyszczenie zasobów tutaj, jeśli jest to konieczne
        }

        static void Main(string[] args)
        {
            Glfw.Init();

            Window window = Glfw.CreateWindow(500, 500, "Programowanie multimedialne", GLFW.Monitor.None, Window.None);

            Glfw.MakeContextCurrent(window);
            Glfw.SwapInterval(1);

            GL.LoadBindings(new BC()); //Pozyskaj adresy implementacji poszczególnych procedur OpenGL

            InitOpenGLProgram(window);

            Glfw.Time = 0;

            while (!Glfw.WindowShouldClose(window))
            {
                DrawScene(window, (float)Glfw.Time);
                Glfw.PollEvents();
            }


            FreeOpenGLProgram(window);

            Glfw.Terminate();
        }


    }
}
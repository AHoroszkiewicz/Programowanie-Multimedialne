﻿using GLFW;
using GlmSharp;

using Shaders;

using OpenTK;
using OpenTK.Graphics.OpenGL4;

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

        static float speed_y; //Prędkość obrotu wokół osi Y [rad/s]
        static float speed_x; //Prędkość obrotu wokół osi X [rad/s]

        static Torus torus = new Torus();

        static KeyCallback kc = KeyProcessor;

        //Obsługa klawiatury - zmiana prędkości obrotu wokół poszczególnych osi w zależności od wciśniętych klawiszy
        public static void KeyProcessor(System.IntPtr window, Keys key, int scanCode, InputState state, ModifierKeys mods)
        {
            if (state == InputState.Press)
            {
                if (key == Keys.Left) speed_y = -3.14f;
                if (key == Keys.Right) speed_y = 3.14f;
                if (key == Keys.Up) speed_x = -3.14f;
                if (key == Keys.Down) speed_x = 3.14f;
            }
            if (state == InputState.Release)
            {
                if (key == Keys.Left) speed_y = 0;
                if (key == Keys.Right) speed_y = 0;
                if (key == Keys.Up) speed_x = 0;
                if (key == Keys.Down) speed_x = 0;
            }
        }

        public static void InitOpenGLProgram(Window window)
        {
            GL.ClearColor(0, 0, 0, 1);
            DemoShaders.InitShaders("Shaders/");
            Glfw.SetKeyCallback(window, kc); //Zarejestruj metodę obsługi klawiatury
        }

        public static void FreeOpenGLProgram(Window window)
        {

        }

        public static void DrawScene(Window window, float angle_x, float angle_y, float time)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            mat4 P = mat4.Perspective(glm.Radians(50.0f), 1, 1, 50);
            mat4 V = mat4.LookAt(new vec3(0, 0, -5), new vec3(0, 0, 0), new vec3(0, 1, 0));

            DemoShaders.spConstant.Use();
            GL.UniformMatrix4(DemoShaders.spConstant.U("P"), 1, false, P.Values1D);
            GL.UniformMatrix4(DemoShaders.spConstant.U("V"), 1, false, V.Values1D);

            mat4 M = mat4.Rotate(angle_y, new vec3(0, 1, 0)) * mat4.Rotate(angle_x, new vec3(1, 0, 0));
            GL.UniformMatrix4(DemoShaders.spConstant.U("M"), 1, false, M.Values1D);

            float red = (glm.Sin(time) + 1) / 2;
            float green = (MathF.Cos(time) + 1) / 2;
            float blue = (MathF.Sin(time) + MathF.Cos(time)) / 2;
            GL.Uniform4(DemoShaders.spConstant.U("color"), red, green, blue, 1f);
            torus.drawWire();

            Glfw.SwapBuffers(window);
        }



        //Metoda główna
        static void Main(string[] args)
        {
            Glfw.Init();

            Window window = Glfw.CreateWindow(500, 500, "OpenGL", GLFW.Monitor.None, Window.None);

            Glfw.MakeContextCurrent(window);
            Glfw.SwapInterval(1);

            GL.LoadBindings(new BC());

            InitOpenGLProgram(window);
            Glfw.Time = 0;

            float time = 0;
            float angle_x = 0;
            float angle_y = 0;

            while (!Glfw.WindowShouldClose(window))
            {
                angle_x += speed_x * (float)Glfw.Time; //Aktualizuj kat obrotu wokół osi X zgodnie z prędkością obrotu
                angle_y += speed_y * (float)Glfw.Time; //Aktualizuj kat obrotu wokół osi Y zgodnie z prędkością obrotu
                time += (float)Glfw.Time;
                Glfw.Time = 0; //Wyzeruj licznik czasu
                DrawScene(window, angle_x, angle_y, time);

                Glfw.PollEvents();
            }


            FreeOpenGLProgram(window);

            Glfw.Terminate();
        }


    }
}
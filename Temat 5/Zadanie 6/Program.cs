using OpenTK;
using OpenTK.Audio.OpenAL;

using HelpersNS;
using GLFW;

namespace PMLabs
{
    public class BC : IBindingsContext
    {
        public IntPtr GetProcAddress(string procName)
        {
            return Glfw.GetProcAddress(procName);
        }
    }

    class Program
    {


        static KeyCallback kc = KeyProcessor;

        static ALDevice device;
        static ALContext context;
        static int buf;
        static int source;


        public static void KeyProcessor(System.IntPtr window, Keys key, int scanCode, InputState state, ModifierKeys mods)
        {
            if (key == Keys.Alpha1 && state == InputState.Press)
            {
                PlayNote(261.6); // C4
            }
            else if (key == Keys.Alpha2 && state == InputState.Press)
            {
                PlayNote(293.7); // D4
            }
            else if (key == Keys.Alpha3 && state == InputState.Press)
            {
                PlayNote(329.6); // E4
            }
            else if (key == Keys.Alpha4 && state == InputState.Press)
            {
                PlayNote(349.2); // F4
            }
            else if (key == Keys.Alpha5 && state == InputState.Press)
            {
                PlayNote(392.0); // G4
            }
            else if (key == Keys.Alpha6 && state == InputState.Press)
            {
                PlayNote(440.0); // A4
            }
            else if (key == Keys.Alpha7 && state == InputState.Press)
            {
                PlayNote(493.9); // B4
            }
            else if (key == Keys.Alpha8 && state == InputState.Press)
            {
                PlayNote(523.3); // C5
            }
        }

        public static void PlayNote(double hz)
        {
            buf = AL.GenBuffer();

            double f = hz;
            double A = short.MaxValue / 10;
            int fp = 44100;
            double op = 1.0 / fp;
            int lp = 1 * fp;
            short[] data = new short[lp];
            for (int x = 0; x < lp; x++)
            {
                data[x] = signal(op * x, f, A); //generuj kolejne próbki
            }
            AL.BufferData<short>(buf, ALFormat.Mono16, data, fp);
            source = AL.GenSource();

            AL.BindBufferToSource(source, buf);
            AL.SourcePlay(source);
        }

        public static short signal(double t, double f, double A)
        {
            double value = 4 * A * Math.Abs(t * f - Math.Floor(t * f + 0.5)) - A;
            return (short)value;
        }

        public static void InitSound()
        {
            device = ALC.OpenDevice(null);
            context = ALC.CreateContext(device, new ALContextAttributes());
            ALC.MakeContextCurrent(context);


        }

        public static void FreeSound()
        {
            AL.SourceStop(source);
            AL.DeleteSource(source);
            AL.DeleteBuffer(buf);

            if (context != ALContext.Null)
            {
                ALC.MakeContextCurrent(ALContext.Null);
                ALC.DestroyContext(context);
            }
            context = ALContext.Null;

            if (device != ALDevice.Null)
            {
                ALC.CloseDevice(device);
            }
            device = ALDevice.Null;
        }

        public static void SoundEvents()
        {

        }

        static void Main(string[] args)
        {
            Glfw.Init();

            Window window = Glfw.CreateWindow(500, 500, "OpenAL", GLFW.Monitor.None, Window.None);

            Glfw.MakeContextCurrent(window);
            Glfw.SetKeyCallback(window, kc);

            InitSound();


            while (!Glfw.WindowShouldClose(window))
            {
                SoundEvents();
                Glfw.PollEvents();
            }


            FreeSound();
            Glfw.Terminate();
        }


    }
}
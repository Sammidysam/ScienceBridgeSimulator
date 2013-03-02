using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;

namespace Essentials {
	public class Start : GameWindow {
		private bool mouseDown = false;
		private double elapsedTime;
		private Drawer drawer = new Drawer();
		private Button exit;
		private bool started = false;
		public Start() : base(DisplayDevice.Default.Width, DisplayDevice.Default.Height){
			Keyboard.KeyDown += Keyboard_KeyDown;
			Mouse.ButtonDown += Mouse_ButtonDown;
			Mouse.ButtonUp += Mouse_ButtonUp;
		}
		void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e){
            if(e.Key == Key.Escape)
            	this.Exit();
            if(e.Key == Key.F11)
            	if(this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;
        }
		void Mouse_ButtonDown(object sender, MouseButtonEventArgs e){
			mouseDown = true;
		}
		void Mouse_ButtonUp(object sender, MouseButtonEventArgs e){
			mouseDown = false;
		}
		protected override void OnLoad(EventArgs e){
            this.Title = "C# OpenGL Window";
            this.WindowState = WindowState.Fullscreen;
            exit = new Button(0.95f, 0.95f, 0.05f, 0.05f, Color.DarkRed);
        }
		protected override void OnResize(EventArgs e){
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }
		protected override void OnUpdateFrame(FrameEventArgs e){
			elapsedTime += e.Time;
			if(elapsedTime > 0.5){
				drawer.updateEnvironment();
				elapsedTime = 0;
			}
			if(this.Mouse != null){
				var minX = this.Width * 0.975;
				var minY = this.Height * 0.025;
				if(this.Mouse.X >= minX && this.Mouse.Y <= minY && mouseDown)
					this.Exit();
			}
        }
		protected override void OnRenderFrame(FrameEventArgs e){
			drawer.render();
			exit.draw();
			this.SwapBuffers();
        }
		public static void Main(String[] args){
            using (Start start = new Start()){
				Console.WriteLine("Running...");
                start.Run(20, 60);
            }
        }
	}
}
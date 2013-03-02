using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Bridges;

namespace Essentials {
	public class Start : GameWindow {
		private bool mouseDown = false;
		private double elapsedTime;
		private Drawer drawer = new Drawer();
		private Button exit;
		private Button[] bridgeButtons = new Button[5];
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
            exit = new Button(0.9f, 0.85f, 0.1f, 0.15f, Color.DarkRed, "C:\\Users\\Sam\\Documents\\SharpDevelop Projects\\Console\\ScienceBridgeSimulator\\res\\x.png");
//          add relative path
//            Bridge bridge = new Bridge();
//			for(int i = 0; i < 5; i++)
//				bridgeButtons[i] = bridge.getName(i);
//			add images for all other buttons
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
				if(exit.over(this.Mouse.X, this.Mouse.Y, this.Width, this.Height)){
					exit.setColor(Color.Red);
					if(mouseDown)
						this.Exit();
				}
				else
					exit.setColor(Color.DarkRed);
			}
			if(!started && mouseDown)
				started = true;
        }
		protected override void OnRenderFrame(FrameEventArgs e){
			drawer.render(started);
			exit.draw();
			this.SwapBuffers();
        }
		public static void Main(string[] args){
            using (Start start = new Start()){
				Console.WriteLine("Running...");
                start.Run(20, 60);
            }
        }
	}
}
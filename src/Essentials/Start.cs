using System;
using System.Drawing;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Bridges;
using Load;

namespace Essentials {
	public class Start : GameWindow {
		private bool mouseDown = false;
		private double waterTime;
		private double bridgeTime;
		private Drawer drawer = new Drawer();
		private Button exit;
		private Button[] bridgeButtons = new Button[5];
		private bool started = false;
		private Random rand = new Random();
		private Bridge bridge;
		private Vehicle truck = new Truck();
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
            this.Title = "Science Bridge Simulator";
            this.WindowState = WindowState.Fullscreen;
            exit = new Button(0.9f, 0.85f, 0.1f, 0.15f, Color.DarkRed, PathGetter.getPath("res\\x.png"), true);
            Bridge bridge = new Bridge(rand.Next(0, 4));
			for(int i = 0; i < bridgeButtons.Length; i++)
				bridgeButtons[i] = new Button(-0.95f, 0.45f + (i * -0.3f), 0.725f, 0.2f, Color.White, PathGetter.getPath("res\\" + bridge.getName(i) + ".png"), true);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
		}
		protected override void OnResize(EventArgs e){
            GL.Viewport(0, 0, Width, Height);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }
		protected override void OnUpdateFrame(FrameEventArgs e){
			waterTime += e.Time;
			bridgeTime += e.Time;
			if(waterTime > 0.5){
				drawer.updateEnvironment();
				waterTime = 0;
			}
			if(bridgeTime > 3.0){
				drawer.nextBridge();
				bridgeTime = 0;
			}
			if(this.Mouse != null){
				if(exit.over(this.Mouse.X, this.Mouse.Y, this.Width, this.Height)){
					exit.setColor(Color.Red);
					if(mouseDown)
						this.Exit();
				}
				else
					exit.setColor(Color.DarkRed);
				for(int i = 0; i < bridgeButtons.Length; i++){
					if(bridgeButtons[i].over(this.Mouse.X, this.Mouse.Y, this.Width, this.Height)){
						bridgeButtons[i].setColor(Color.Red);
						drawer.setBridge(i);
					}
					else
						bridgeButtons[i].setColor(Color.White);
				}
				bool overOne = false;
				int overWhich = 5;
				for(int i = 0; i < bridgeButtons.Length; i++)
					if(bridgeButtons[i].over(this.Mouse.X, this.Mouse.Y, this.Width, this.Height)){
						overOne = true;
						overWhich = i;
					}
				if(!started && mouseDown && overOne){
					started = true;
					bridge = new Bridge(overWhich);
				}
			}
			if(started)
				truck.increaseX(0.01f);
        }
		protected override void OnRenderFrame(FrameEventArgs e){
			drawer.render(started, truck.getMoving());
			if(!started)
				for(int i = 0; i < bridgeButtons.Length; i++)
					bridgeButtons[i].draw();
			else
				truck.draw(this.Width, this.Height);
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
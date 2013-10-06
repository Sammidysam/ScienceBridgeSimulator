using System;
using System.Drawing;
using System.IO;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using Bridges;
using Load;
using System.Collections.Generic;

namespace Essentials {
	public class Start : GameWindow {
		private bool mouseDown = false;
		private double waterTime;
		private double bridgeTime;
		private Drawer drawer;
		private Button exit;
		private Button[] bridgeButtons = new Button[5];
		private Button go;
		private Button stop;
		private Button[] add = new Button[6];
		private bool started = false;
		private Random rand = new Random();
		private Bridge bridge;
		private List<Vehicle> vehicles = new List<Vehicle>(0);
		private double clickWait = 0;
		private bool waiting = false;
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
            exit = new Button(0.9f, 0.85f, 0.1f, 0.15f, Color.DarkRed, PathGetter.getPath("res" + Path.DirectorySeparatorChar + "x.png"), true);
			for(int i = 0; i < bridgeButtons.Length; i++)
				bridgeButtons[i] = new Button(-0.95f, 0.45f + (i * -0.3f), 0.725f, 0.2f, Color.White, PathGetter.getPath("res" + Path.DirectorySeparatorChar + Bridge.getName(i) + ".png"), true);
			go = new Button(0.8f, 0.5f, 0.2f, 0.3f, Color.White, PathGetter.getPath("res" + Path.DirectorySeparatorChar + "go.png"), true);
			stop = new Button(0.8f, 0.5f, 0.2f, 0.3f, Color.White, PathGetter.getPath("res" + Path.DirectorySeparatorChar + "stop.png"), false);
			for(int i = 0; i < add.Length; i++)
				add[i] = new Button(0.8f - (i * 0.15f), 0.85f, 0.1f, 0.15f, Color.White, PathGetter.getPath(Vehicle.getPath(i)), false, i == add.Length - 1 ? true : false);
			drawer = new Drawer();
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
			GC.Collect();
			waterTime += e.Time;
			bridgeTime += e.Time;
			if(waiting)
				clickWait += e.Time;
			if(waterTime > 0.5){
				drawer.updateEnvironment();
				waterTime = 0;
			}
			if(bridgeTime > 3.0){
				drawer.nextBridge();
				bridgeTime = 0;
			}
			if(clickWait > 0.2){
				waiting = false;
				clickWait = 0;
			}
			if(this.Mouse != null){
				if(exit.over(this.Mouse.X, this.Mouse.Y, this.Width, this.Height)){
					exit.setColor(Color.Red);
					if(mouseDown && !waiting){
						if(started){
							started = false;
							for(int i = 0; i < vehicles.Count; i++)
								GL.DeleteTexture(vehicles[i].getTexture());
							vehicles.Clear();
						}
						else
							this.Exit();
					}
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
				if(started)
					for(int i = 0; i < add.Length; i++)
						if(add[i].over(this.Mouse.X, this.Mouse.Y, this.Width, this.Height)){
							overOne = true;
							overWhich = i;
						}
				if(mouseDown){
					if(overOne && started && overWhich < add.Length && !waiting){
						vehicles.Add(Vehicle.getVehicle(overWhich));
						vehicles[vehicles.Count - 1].setMoving(stop.getOn());
					}
					if(!started && overOne){
						started = true;
						for(int i = 0; i < add.Length; i++)
							add[i].setOn(true);
						go.setOn(true);
						stop.setOn(false);
						bridge = Bridge.getBridge(overWhich);
					}
					else if((go.over(this.Mouse.X, this.Mouse.Y, this.Width, this.Height) || stop.over(this.Mouse.X, this.Mouse.Y, this.Width, this.Height)) && !waiting){
						for(int i = 0; i < vehicles.Count; i++)
							vehicles[i].setMoving(!vehicles[i].getMoving());
						go.setOn(!go.getOn());
						stop.setOn(!stop.getOn());
					}
					waiting = true;
				}
			}
			if(started){
				int totalWeight = 0;
				for(int i = 0; i < vehicles.Count; i++)
					totalWeight += vehicles[i].getWeight();
				if(totalWeight > bridge.maxWeight()){
					bridge.fall(0.01f);
					for(int i = 0; i < vehicles.Count; i++)
						vehicles[i].increaseY(-0.01f);
				}
				else if(vehicles.Count > 0){
					if(vehicles[0].getMoving()){
						for(int i = 0; i < vehicles.Count; i++){
							vehicles[i].increaseX(0.01f);
							vehicles[i].setY(bridge.getYAtX(vehicles[i].getX()));
						}
					}
				}
			}
        }
		protected override void OnRenderFrame(FrameEventArgs e){
			GC.Collect();
			drawer.render(started, bridge);
			if(!started)
				for(int i = 0; i < bridgeButtons.Length; i++)
					bridgeButtons[i].draw();
			else {
				for(int i = 0; i < vehicles.Count; i++)
					vehicles[i].draw(this.Width, this.Height);
				go.draw();
				stop.draw();
				for(int i = 0; i < add.Length; i++)
					add[i].draw();
			}
			exit.draw();
			this.SwapBuffers();
        }
		public static void Main(string[] args){		
            using (Start start = new Start()){
				Console.WriteLine("Running...");
                start.Run(20, 20);
            }
        }
	}
}

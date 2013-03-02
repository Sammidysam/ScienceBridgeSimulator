using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Environment;
using System;

namespace Essentials {
	public class Drawer {
		private bool waterGoingUp = true;
		private Water water = new Water();
		private int currentBridge = 0;
		public void render(bool started, bool vehicleMoving){
			GL.Clear(ClearBufferMask.ColorBufferBit);
			if(started){
				GL.ClearColor(Color.SkyBlue);
				water.draw();
				GL.Color3(Color.ForestGreen);
				GL.Begin(BeginMode.Triangles);
				GL.Vertex2(-1.0f, 0.01f);
				GL.Vertex2(-1.0f, -1.0f);
				GL.Vertex2(-0.24f, -1.0f);
				GL.End();
				GL.Color3(Color.SaddleBrown);
				GL.Begin(BeginMode.Triangles);
				GL.Vertex2(-1.0f, 0);
				GL.Vertex2(-1.0f, -1.0f);
				GL.Vertex2(-0.25f, -1.0f);
				GL.End();
				GL.Color3(Color.ForestGreen);
				GL.Begin(BeginMode.Triangles);
				GL.Vertex2(1.0f, 0.01f);
				GL.Vertex2(1.0f, -1.0f);
				GL.Vertex2(0.24f, -1.0f);
				GL.End();
				GL.Color3(Color.SaddleBrown);
				GL.Begin(BeginMode.Triangles);
				GL.Vertex2(1.0f, 0);
				GL.Vertex2(1.0f, -1.0f);
				GL.Vertex2(0.25f, -1.0f);
				GL.End();
				GL.Color3(Color.White);
				GL.Enable(EnableCap.Texture2D);
				if(!vehicleMoving)
					GL.BindTexture(TextureTarget.Texture2D, TextureHelper.loadTexture(PathGetter.getPath("res\\go.png")));
				else
					GL.BindTexture(TextureTarget.Texture2D, TextureHelper.loadTexture(PathGetter.getPath("res\\stop.png")));
				GL.Begin(BeginMode.Quads);
				GL.TexCoord2(0.0, 1.0);
				GL.Vertex2(0.8f, 0.45f);
				GL.TexCoord2(0.0, 0.0);
				GL.Vertex2(0.8f, 0.75f);
				GL.TexCoord2(1.0, 0.0);
				GL.Vertex2(1.0f, 0.75f);
				GL.TexCoord2(1.0, 1.0);
				GL.Vertex2(1.0f, 0.45f);
				GL.End();
				GL.Disable(EnableCap.Texture2D);
			}
			else {
				GL.ClearColor(Color.Black);
				GL.Color3(Color.White);
				GL.Enable(EnableCap.Texture2D);
				GL.BindTexture(TextureTarget.Texture2D, TextureHelper.loadTexture(PathGetter.getPath("res\\directions.png")));
				GL.Begin(BeginMode.Quads);
				GL.TexCoord2(0.0, 1.0);
				GL.Vertex2(-1.0f, 0.8f);
				GL.TexCoord2(0.0, 0.0);
				GL.Vertex2(-1.0f, 1.0f);
				GL.TexCoord2(1.0, 0.0);
				GL.Vertex2(0.3f, 1.0f);
				GL.TexCoord2(1.0, 1.0);
				GL.Vertex2(0.3f, 0.8f);
				GL.End();
				GL.BindTexture(TextureTarget.Texture2D, TextureHelper.loadTexture(getBridgeLocation(currentBridge)));
				GL.Begin(BeginMode.Quads);
				GL.TexCoord2(0.0, 1.0);
				GL.Vertex2(-0.2f, -1.0f);
				GL.TexCoord2(0.0, 0.0);
				GL.Vertex2(-0.2f, 0.8f);
				GL.TexCoord2(1.0, 0.0);
				GL.Vertex2(1.0f, 0.8f);
				GL.TexCoord2(1.0, 1.0);
				GL.Vertex2(1.0f, -1.0f);
				GL.End();
				GL.Disable(EnableCap.Texture2D);
			}
		}
		public void updateEnvironment(){
			if(waterGoingUp)
				water.changeY(0.002f);
			else
				water.changeY(-0.002f);
			waterGoingUp = !waterGoingUp;
		}
		public void nextBridge(){
			if(currentBridge < 5){
				currentBridge++;
				if(currentBridge == 5)
					currentBridge = 0;
			}
			else
				currentBridge = 0;
		}
		public void setBridge(int index){
			if(index >= 0 && index < 5)
				currentBridge = index;
			else
				Console.WriteLine("Invalid index!");
		}
		private string getBridgeLocation(int bridgeNumber){
			switch (bridgeNumber){
				case 0:
					return PathGetter.getPath("res\\footbridge.jpg");
				case 1:
					return PathGetter.getPath("res\\beambridge.jpg");
				case 2:
					return PathGetter.getPath("res\\suspensionbridge.jpg");
				case 3:
					return PathGetter.getPath("res\\trussbridge.jpg");
				case 4:
					return PathGetter.getPath("res\\archbridge.jpg");
				default:
					throw new System.Exception();
			}
		}
	}
}

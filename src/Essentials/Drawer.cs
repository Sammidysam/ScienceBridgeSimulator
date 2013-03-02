using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Environment;

namespace Essentials {
	public class Drawer {
		private bool waterGoingUp = true;
		private Water water = new Water();
		public void render(bool started){
			GL.Clear(ClearBufferMask.ColorBufferBit);
			if(started){
				GL.ClearColor(Color.SkyBlue);
				water.draw();
				GL.Color3(Color.SaddleBrown);
				GL.Begin(BeginMode.Triangles);
				GL.Vertex2(-1.0f, 0);
				GL.Vertex2(-1.0f, -1.0f);
				GL.Vertex2(-0.25f, -1.0f);
				GL.End();
				GL.Begin(BeginMode.Triangles);
				GL.Vertex2(1.0f, 0);
				GL.Vertex2(1.0f, -1.0f);
				GL.Vertex2(0.25f, -1.0f);
				GL.End();
			}
			else {
				GL.ClearColor(Color.Black);
			}
		}
		public void updateEnvironment(){
			if(waterGoingUp)
				water.changeY(0.002f);
			else
				water.changeY(-0.002f);
			waterGoingUp = !waterGoingUp;
		}
	}
}

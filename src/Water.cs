using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace Environment {
	public class Water {
		private float y = 0;
		public void draw(){
			GL.Color3(Color.RoyalBlue);
			GL.Begin(BeginMode.Quads);
			GL.Vertex2(-1.0f, -1.2f + y);
			GL.Vertex2(-1.0f, -0.5f + y);
			GL.Vertex2(1.0f, -0.5f + y);
			GL.Vertex2(1.0f, -1.2f + y);
			GL.End();
		}
		public void changeY(float delta){
			y += delta;
		}
	}
}

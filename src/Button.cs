using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Essentials {
	public class Button {
		private float x;
		private float y;
		private float width;
		private float height;
		private Color color;
		public Button(float x, float y, float width, float height, Color color){
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
			this.color = color;
		}
		public void draw(){
			GL.Color3(color);
			GL.Begin(BeginMode.Quads);
			GL.Vertex2(0.95f, 0.95f);
			GL.Vertex2(0.95f, 1.0f);
			GL.Vertex2(1.0f, 1.0f);
			GL.Vertex2(1.0f, 0.95f);
			GL.End();
			
		}
	}
}

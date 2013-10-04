using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Bridges {
	public class BeamBridge : Bridge {
		public BeamBridge() : base(1){
			
		}
		public override void draw(){
			GL.Color3(Color.Gray);
			GL.Begin(BeginMode.Quads);
			GL.Vertex2(-1.0f, y);
			GL.Vertex2(-1.0f, y - 0.06f);
			GL.Vertex2(1.0f, y - 0.06f);
			GL.Vertex2(1.0f, y);
			GL.End();
			for(float x = -0.75f; x < 1.0f; x += 0.4f){
				GL.Begin(BeginMode.Quads);
				GL.Vertex2(x, -0.06f);
				GL.Vertex2(x, -1.0f);
				GL.Vertex2(x + 0.05f, -1.0f);
				GL.Vertex2(x + 0.05f, -0.06f);
				GL.End();
			}
		}
	}
}

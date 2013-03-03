using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Bridges {
	public class ArchBridge : Bridge {
		public ArchBridge() : base(4){
			
		}
		public override void draw(){
			GL.Color3(Color.Gray);
			for(float x = -1.0f; x < 1.0f; x += 0.001f){
				GL.Begin(BeginMode.Quads);
				GL.Vertex2(x, (-0.03f + (-0.30f * Math.Pow(x, 2))) + y);
				GL.Vertex2(x, (-0.08f + (-0.30f * Math.Pow(x, 2))) + y);
				GL.Vertex2(x + 0.001f, (-0.08f + (-0.30f * Math.Pow(x, 2))) + y);
				GL.Vertex2(x + 0.001f, (-0.03f + (-0.30f * Math.Pow(x, 2))) + y);
				GL.End();
			}
			for(float x = -1.0f; x < 1.0f; x += 0.001f){
				GL.Begin(BeginMode.Quads);
				GL.Vertex2(x, y);
				GL.Vertex2(x, getBottom(x));
				GL.Vertex2(x + 0.001f, getBottom(x));
				GL.Vertex2(x + 0.001f, y);
				GL.End();
			}
		}
		private float getBottom(float x){
			return (float)((-0.08f + (-0.3f * Math.Pow(x, 2))) + y);
		}
	}
}

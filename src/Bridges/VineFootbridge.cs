using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Bridges {
	public class VineFootbridge : Bridge {
		public VineFootbridge() : base(0){
			
		}
		public override void draw(){
			GL.Color3(Color.SandyBrown);
			for(float x = -1.0f; x < 1.0f; x += 0.0012f){
				GL.Begin(BeginMode.Quads);
				GL.Vertex2(x, (-0.30f + (0.30f * Math.Pow(x, 2))) + y);
				GL.Vertex2(x, (-0.45f + (0.30f * Math.Pow(x, 2))) + y);
				GL.Vertex2(x + 0.001f, (-0.45f + (0.30f * Math.Pow(x, 2))) + y);
				GL.Vertex2(x + 0.001f, (-0.30f + (0.30f * Math.Pow(x, 2))) + y);
				GL.End();
			}
		}
		public override float getYAtX(float x){
			return (float)((-0.3f + (0.3f * Math.Pow(x, 2))) + y);
		}
	}
}

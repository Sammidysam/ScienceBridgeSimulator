using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;

namespace Bridges {
	public class TrussBridge : Bridge {
		public TrussBridge() : base(3){
			
		}
		public override void draw(){
			GL.Color3(Color.Gray);
			GL.Begin(BeginMode.Quads);
			GL.Vertex2(-1.0f, y);
			GL.Vertex2(-1.0f, y - 0.06f);
			GL.Vertex2(1.0f, y - 0.06f);
			GL.Vertex2(1.0f, y);
			GL.End();
			for(float x = -0.75f; x < 1.0f; x += 0.25f){
				GL.Begin(BeginMode.Quads);
				GL.Vertex2(x, -0.06f);
				GL.Vertex2(x, -1.0f);
				GL.Vertex2(x + 0.075f, -1.0f);
				GL.Vertex2(x + 0.075f, -0.06f);
				GL.End();
			}
			float height = 0.2f;
			bool up = true;
			if(y == 0){
				GL.LineWidth(3.0f);
				GL.Begin(BeginMode.Lines);
				GL.Vertex2(-0.9f, height);
				GL.Vertex2(0.9f, height);
				GL.End();
				for(float x = -1.0f; x < 1.0f; x += 0.1f){
					if(x > -1.0f){
						GL.Begin(BeginMode.Quads);
						GL.Vertex2(x, height);
						GL.Vertex2(x, 0.0f);
						GL.Vertex2(x + 0.01f, 0.0f);
						GL.Vertex2(x + 0.01f, height);
						GL.End();
					}
					GL.Begin(BeginMode.Lines);
					if(up){
						GL.Vertex2(x, 0.0f);
						GL.Vertex2(x + 0.1f, 0.2f);
					}
					else {
						GL.Vertex2(x, 0.2f);
						GL.Vertex2(x + 0.1f, 0.0f);
					}
					GL.End();
					up = !up;
				}
			}
		}
	}
}

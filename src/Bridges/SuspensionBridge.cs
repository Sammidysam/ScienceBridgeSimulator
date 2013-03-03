using System;
using System.Drawing;
using OpenTK.Graphics.OpenGL;
using Essentials;

namespace Bridges {
	public class SuspensionBridge : Bridge {
		public SuspensionBridge() : base(2){
			
		}
		public override void draw(){
			GL.Color3(Color.Firebrick);
			GL.Begin(BeginMode.Quads);
			GL.Vertex2(-1.0f, y);
			GL.Vertex2(-1.0f, y - 0.06f);
			GL.Vertex2(1.0f, y - 0.06f);
			GL.Vertex2(1.0f, y);
			GL.End();
			for(float opposite = -1; opposite <= 1; opposite += 2){
				GL.Begin(BeginMode.Quads);
				GL.Vertex2(opposite * 0.45f, 0.6f);
				GL.Vertex2(opposite * 0.45f, -1.0f);
				GL.Vertex2((opposite * 0.45f) + 0.05f, -1.0f);
				GL.Vertex2((opposite * 0.45f) + 0.05f, 0.6f);
				GL.End();
			}
			if(y == 0){
				for(float x = -0.4f; x < 0.45f; x += 0.005f){
					GL.Begin(BeginMode.Quads);
					GL.Vertex2(x, 0.301f + (1.6609f * Math.Pow(x, 2)) - (0.083 * x));
					GL.Vertex2(x, 0.306f + (1.6609f * Math.Pow(x, 2)) - (0.083 * x));
					GL.Vertex2(x + 0.01f, 0.306f + (1.6609f * Math.Pow(x, 2) - (0.083 * x)));
					GL.Vertex2(x + 0.01f, 0.301f + (1.6609f * Math.Pow(x, 2) - (0.083 * x)));
					GL.End();
				}
				for(float x = -1.0f; x < -0.45f; x += 0.005f){
					GL.Begin(BeginMode.Quads);
					GL.Vertex2(x, 1.05 * Math.Abs(x + 1));
					GL.Vertex2(x, 1.05 * Math.Abs(x + 1) + 0.006f);
					GL.Vertex2(x + 0.01f, 1.05 * Math.Abs(x + 1) + 0.006f);
					GL.Vertex2(x + 0.01f, 1.05 * Math.Abs(x + 1));
					GL.End();
				}
				for(float x = 0.5f; x < 1.0f; x += 0.005f){
					GL.Begin(BeginMode.Quads);
					GL.Vertex2(x, 1.15f * Math.Abs(x - 1));
					GL.Vertex2(x, 1.15f * Math.Abs(x - 1) + 0.006f);
					GL.Vertex2(x + 0.01f, 1.15f * Math.Abs(x - 1) + 0.006f);
					GL.Vertex2(x + 0.01f, 1.15f * Math.Abs(x - 1));
				}
				for(float x = -1.0f; x <= 1.0f; x += 0.008f){
					GL.Begin(BeginMode.Quads);
					GL.Vertex2(x, getYOfConnectors(x));
					GL.Vertex2(x, 0);
					GL.Vertex2(x + 0.001f, 0);
					GL.Vertex2(x + 0.001f, getYOfConnectors(x));
					GL.End();
				}
			}
		}
		private float getYOfConnectors(float x){
			if(x >= -1.0f && x < -0.45f)
				return 1.05f * Math.Abs(x + 1);
			if(x >= -0.4f && x <= 0.45f)
				return (float)(0.301f + (1.6609f * Math.Pow(x, 2)) - (0.083 * x));
			if((x >= -0.45f && x < -0.4f) || (x > 0.45f && x < 0.5f))
				return 0;
			else
				return 1.15f * Math.Abs(x - 1);
		}
	}
}

﻿using OpenTK.Graphics.OpenGL;
using Essentials;
using System.Drawing;

namespace Load {
	public class Elephant : Vehicle {
		public override int getWeight(){
			return 100000;
		}
		public override void draw(int screenWidth, int screenHeight){
			float width = 48 / (float)(screenWidth);
			float height = 48 / (float)(screenHeight);
			GL.Color3(Color.White);
			GL.Enable(EnableCap.Texture2D);
			GL.BindTexture(TextureTarget.Texture2D, TextureHelper.loadTexture(PathGetter.getPath("res\\elephant.png")));
			GL.Begin(BeginMode.Quads);
			GL.TexCoord2(1.0, 1.0);
			GL.Vertex2(x, y);
			GL.TexCoord2(1.0, 0.0);
			GL.Vertex2(x, y + height);
			GL.TexCoord2(0.0, 0.0);
			GL.Vertex2(x + width, y + height);
			GL.TexCoord2(0.0, 1.0);
			GL.Vertex2(x + width, y);
			GL.End();
			GL.Disable(EnableCap.Texture2D);
		}
	}
}

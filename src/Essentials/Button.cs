using System.Drawing;
using OpenTK.Graphics.OpenGL;
using System;

namespace Essentials {
	public class Button {
		private float x;
		private float y;
		private float width;
		private float height;
		private Color color;
		private string overlayPath;
		private bool on = true;
		public Button(float x, float y, float width, float height, Color color, string overlayPath, bool on){
			this.x = x;
			this.y = y;
			this.width = width;
			this.height = height;
			this.color = color;
			this.overlayPath = overlayPath;
			this.on = on;
		}
		public void draw(){
			if(on){
				GL.Color3(color);
				GL.Enable(EnableCap.Texture2D);
				GL.BindTexture(TextureTarget.Texture2D, TextureHelper.loadTexture(overlayPath));
				GL.Begin(BeginMode.Quads);
				GL.TexCoord2(0.0, 1.0);
				GL.Vertex2(x, y);
				GL.TexCoord2(0.0, 0.0);
				GL.Vertex2(x, y + height);
				GL.TexCoord2(1.0, 0.0);
				GL.Vertex2(x + width, y + height);
				GL.TexCoord2(1.0, 1.0);
				GL.Vertex2(x + width, y);
				GL.End();
				GL.Disable(EnableCap.Texture2D);
			}
		}
		public bool over(int mouseX, int mouseY, int screenWidth, int screenHeight){
			if(on){
				var minX = screenWidth * translate(x, true);
				var minY = screenHeight * translate(y, false);
				var maxX = screenWidth * (translate(x, true) + (this.width / 2));
				var maxY = screenHeight * (translate(y, false) - (this.height / 2));
				if(mouseX >= minX && mouseY <= minY && mouseY >= maxY && mouseX <= maxX)
					return true;
				else
					return false;
			}
			else
				return false;
		}
		public void setColor(Color color){
			this.color = color;
		}
		private float translate(float drawing, bool isX){
			if(isX)
				return (drawing / 2) + 0.5f;
			else
				return 1.0f - ((drawing / 2) + 0.5f);
		}
		public void setOn(bool on){
			this.on = on;
		}
	}
}

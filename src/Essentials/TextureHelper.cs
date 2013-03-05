using System;
using System.Drawing;
using System.Drawing.Imaging;
using OpenTK.Graphics.OpenGL;

namespace Essentials {
	public class TextureHelper {
		public static int loadTexture(string filename){
    		if(String.IsNullOrEmpty(filename))
        		throw new ArgumentException(filename);
    		int id = GL.GenTexture();
    		Console.WriteLine(id + " " + filename);
    		GL.BindTexture(TextureTarget.Texture2D, id);
    		Bitmap bmp = new Bitmap(filename);
    		BitmapData bmp_data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
    		GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, bmp_data.Width, bmp_data.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bmp_data.Scan0);
    		bmp.UnlockBits(bmp_data);
    		GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
   			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
    		return id;
		}
	}
}

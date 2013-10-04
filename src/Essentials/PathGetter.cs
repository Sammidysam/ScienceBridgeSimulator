using System.IO;

namespace Essentials {
	public class PathGetter {
		public static string getPath(string file){
			// string path = typeof(PathGetter).Assembly.Location.ToString();
			// path = path.Substring(0, path.Length - 26);
			return System.IO.Path.Combine(Directory.GetCurrentDirectory(), file);
		}
	}
}

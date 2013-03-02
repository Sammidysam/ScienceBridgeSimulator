using System;

namespace Bridge {
	public class Bridge {
		private Random rand = new Random();
		private int type;
		public Bridge(){
			type = rand.Next(0, 5);
		}
	}
}

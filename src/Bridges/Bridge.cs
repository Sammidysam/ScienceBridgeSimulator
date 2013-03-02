using System;

namespace Bridges {
	public class Bridge {
		private Random rand = new Random();
		private int type;
		public Bridge(int type){
			this.type = type;
		}
		public virtual void draw(){
			
		}
		public string getName(int type){
			switch (type){
				case 0:
					return "Vine Footbridge";
				case 1:
					return "Beam Bridge";
				case 2:
					return "Suspension Bridge";
				case 3:
					return "Truss Bridge";
				case 4:
					return "Arch Bridge";
				default:
					return "Error Bridge";
			}
		}
	}
}

using System;
using System.IO;
using Essentials;

namespace Bridges {
	public class Bridge {
		private Random rand = new Random();
		private int type;
		protected float y = 0;
		public Bridge(int type){
			this.type = type;
		}
		public virtual void draw(){
			
		}
		public virtual int maxWeight(){
			return getMaxWeight(this.type);
		}
		public virtual float getYAtX(float x){
			return y;
		}
		public void fall(float delta){
			y -= delta;
		}
		public static string getName(int type){
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
		public static Bridge getBridge(int type){
			switch(type){
				case 0:
					return new VineFootbridge();
				case 1:
					return new BeamBridge();
				case 2:
					return new SuspensionBridge();
				case 3:
					return new TrussBridge();
				case 4:
					return new ArchBridge();
				default:
					throw new System.Exception();
			}
		}
		protected int getMaxWeight(int type){
			string[] values = new string[5];
			using (StreamReader read = new StreamReader(PathGetter.getPath("MaxWeights.txt"))){
				for(int i = 0; i < values.Length; i++)
					values[i] = read.ReadLine();
			}
			return Convert.ToInt32(values[type]);
		}
	}
}

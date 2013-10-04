using System;
using System.IO;
using Essentials;

namespace Load {
	public class Vehicle {
		protected float x = -1.0f;
		protected float y = 0;
		protected bool moving = false;
		protected int type;
		protected int texture;
		public Vehicle(int type){
			this.type = type;
			this.texture = TextureHelper.loadTexture(getPath(this.type));
		}
		public virtual int getWeight(){
			return 0;
		}
		public virtual void draw(int screenWidth, int screenHeight){
			
		}
		public void increaseX(float delta){
			if(moving)
				x += delta;
		}
		public void increaseY(float delta){
			y += delta;
		}
		public void setY(float y){
			this.y = y;
		}
		public float getX(){
			return x;
		}
		public bool getMoving(){
			return moving;
		}
		public void setMoving(bool moving){
			this.moving = moving;
		}
		public static Vehicle getVehicle(int type){
			switch (type){
				case 0:
					return new Truck();
				case 1:
					return new Person();
				case 2:
					return new TrainCar();
				case 3:
					return new Elephant();
				case 4:
					return new Anvil();
				case 5:
					return new SpaceShuttle();
				default:
					throw new System.Exception();
			}
		}
		public static string getPath(int type){
			switch (type){
				case 0:
					return "res" + Path.DirectorySeparatorChar + "truck.png";
				case 1:
					return "res" + Path.DirectorySeparatorChar + "person.png";
				case 2:
					return "res" + Path.DirectorySeparatorChar + "train.png";
				case 3:
					return "res" + Path.DirectorySeparatorChar + "elephant.png";
				case 4:
					return "res" + Path.DirectorySeparatorChar + "anvil.png";
				case 5:
					return "res" + Path.DirectorySeparatorChar + "spaceshuttle.png";
				default:
					throw new System.Exception();
			}
		}
		public int getTexture(){
			return this.texture;
		}
	}
}

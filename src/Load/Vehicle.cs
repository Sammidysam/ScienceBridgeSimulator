namespace Load {
	public class Vehicle {
		protected float x = -1.0f;
		protected float y = 0;
		protected bool moving = false;
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
					return "res\\truck.png";
				case 1:
					return "res\\person.png";
				case 2:
					return "res\\train.png";
				case 3:
					return "res\\elephant.png";
				case 4:
					return "res\\anvil.png";
				case 5:
					return "res\\spaceshuttle.png";
				default:
					throw new System.Exception();
			}
		}
	}
}

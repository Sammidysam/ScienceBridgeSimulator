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
		public bool getMoving(){
			return moving;
		}
		public void setMoving(bool moving){
			this.moving = moving;
		}
	}
}

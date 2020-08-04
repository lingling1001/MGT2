
public interface ISpriteEffect
{
	IDynamicProperty Owner{ get; }
	
	void Attach(IDynamicProperty owner);
	
	void Detach();
	
	int Duration { get; }
}
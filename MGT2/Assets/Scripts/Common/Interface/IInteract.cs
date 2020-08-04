
/*
 * 模块交互接口
 */
public interface IInteract
{
	object GetValue(string Key);
	bool SetValue(string Key,object Value);
	bool Hello(string Command,object Content);
}



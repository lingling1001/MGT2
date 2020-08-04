
/// <summary>
/// 动态属性.
/// </summary>
public interface IDynamicProperty
{
	void DoPropertyChanged(int id, object oldvalue, object newValue);
	PropertyItem GetProperty(int id);
}

//属性类
public class PropertyItem
{
    public int ID;
    private object content;
    public object Content
    {
        get
        {
            return this.content;
        }
        set
        {
            if (value != content)
            {
                object oldValue = content;
                this.content = value;

                if (Owner != null)
                {
                    Owner.DoPropertyChanged(ID, oldValue, this.content);
                }
            }
        }
    }

    public void SetValueWithoutEvent(object content)
    {
        this.content = content;
    }


    public IDynamicProperty Owner = null;

    public PropertyItem(int id, object content)
    {
        this.ID = id;
        this.content = content;
    }
}
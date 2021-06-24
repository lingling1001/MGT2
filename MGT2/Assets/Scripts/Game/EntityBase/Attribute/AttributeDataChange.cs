public class AttributeDataChange : AttributeData
{
    public long PreviousValue { get; private set; }
    public AttributeDataChange(int key, long pre, long value) : base(key, value)
    {
        Key = key;
        PreviousValue = pre;
        Value = value;
    }
    public long GetSub()
    {
        return Value - PreviousValue;
    }
}
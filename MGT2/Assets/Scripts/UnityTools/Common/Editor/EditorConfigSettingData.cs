public class EditorConfigSettingData
{
    public string Key;
    public string Value;
    public string ChangeValue;
    public EditorConfigSettingData()
    {

    }
    public EditorConfigSettingData(string key, string value)
    {
        Key = key;
        Value = value;
        ChangeValue = value;
    }
}
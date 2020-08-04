public class ConfigLoadSettingData
{
    public string Key;
    public string Value;
    public string ChangeValue;
    public ConfigLoadSettingData()
    {

    }
    public ConfigLoadSettingData(string key, string value)
    {
        Key = key;
        Value = value;
        ChangeValue = value;
    }
}
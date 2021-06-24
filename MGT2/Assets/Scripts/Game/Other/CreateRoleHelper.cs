using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

public class CreateRoleHelper
{

    public static StringBuilder _cacheSb = new StringBuilder();

    public static EnumGender GetRandomGender()
    {
        Random ran = RandomHelper.GetRandom();
        return (EnumGender)ran.Next(1, 3);
    }

    /// <summary>
    /// Ëæ»ú¶ÁÈ¡Icon
    /// </summary>
    public static string GetRandomHeadIcon(EnumGender type)
    {
        int gender = (int)type;
        List<PrototypeHeadIcon> list = PrototypeManager<PrototypeHeadIcon>.Instance.GetTableList();
        int index;
        while (list.Count > 0)
        {
            index = RandomHelper.GetRandom().Next(0, list.Count);
            PrototypeHeadIcon data = list[index];
            if (data.Type != gender)
            {
                list.Remove(data);
                continue;
            }
            if (Contains(EnumSaveRole.Icon, data.Name))
            {
                list.Remove(data);
                continue;
            }
            return data.Name;
        }
        return string.Empty;
    }

    /// <summary>
    /// 1ÄĞ 2Å®
    /// </summary>
    public static string GetRandomName(EnumGender type)
    {
        _cacheSb.Clear();
        Random ran = RandomHelper.GetRandom();
        _cacheSb.Append(LastNameItems[ran.Next(0, LastNameItems.Length)]);
        int nameCount = ran.Next(1, 3);

        string[] strFirstName = type == EnumGender.Male ? MaleNameItems : FemaleNameItems;

        for (int cnt = 0; cnt < nameCount; cnt++)
        {
            _cacheSb.Append(strFirstName[ran.Next(0, strFirstName.Length)]);
        }
        return _cacheSb.ToString();
    }

    /// <summary>
    /// ĞÕÊÏ
    /// </summary>
    public static string[] LastNameItems = new[]{ "ÕÔ", "Ç®", "Ëï", "Àî", "ÖÜ", "Îâ", "Ö£", "Íõ", "·ë", "³Â", "ñÒ", "ÎÀ",
          "½¯","Éò", "º«", "Ñî", "Öì", "ÇØ", "ÓÈ", "Ğí", "ºÎ", "ÂÀ", "Ê©", "ÕÅ", "¿×", "²Ü", "ÑÏ", "»ª", "½ğ", "Îº",
          "ÌÕ", "½ª", "Æİ", "Ğ»", "×Ş", "Ó÷", "°Ø", "Ë®", "ñ¼", "ÕÂ", "ÔÆ", "ËÕ", "ÅË", "¸ğ", "ŞÉ", "·¶", "Åí",
          "ÀÉ", "Â³", "Î¤", "²ı", "Âí", "Ãç", "·ï", "»¨", "·½", "ÈÎ", "Ô¬", "Áø", "±«", "Ê·", "ÌÆ", "·Ñ", "Ñ¦",
          "À×", "ºØ", "Äß", "ÌÀ", "ëø", "Òó", "ÂŞ", "±Ï", "ºÂ", "°²", "³£", "¸µ", "±å", "Æë", "Ôª", "¹Ë", "ÃÏ",
          "Æ½", "»Æ", "ÄÂ", "Ïô", "Òü", "Ò¦", "ÉÛ", "Õ¿", "Íô", "Æî", "Ã«", "µÒ", "Ã×", "·ü", "³É", "´÷", "Ì¸",
          "ËÎ", "Ã©", "ÅÓ", "ĞÜ", "¼Í", "Êæ", "Çü", "Ïî", "×£", "¶­", "Áº", "¶Å", "Èî", "À¶", "ãÉ", "¼¾", "¼Ö",
          "Â·", "Â¦", "½­", "Í¯", "ÑÕ", "¹ù", "Ã·", "Ê¢", "ÁÖ", "ÖÓ", "Ğì", "Çñ", "Âæ", "¸ß", "ÏÄ", "²Ì", "Ìï",
          "·®", "ºú", "Áè", "»ô", "Óİ", "Íò", "Ö§", "¿Â", "¹Ü", "Â¬", "Äª", "¿Â", "·¿", "ôÃ", "çÑ", "½â", "Ó¦",
          "×Ú", "¶¡", "Ğû", "µË", "µ¥", "º¼", "ºé", "°ü", "Öî", "×ó", "Ê¯", "´Ş", "¼ª", "¹¨", "³Ì", "ïú", "ĞÏ",
          "Åá", "Â½", "ÈÙ", "ÎÌ", "Ü÷", "ÓÚ", "»İ", "Õç", "Çú", "·â", "´¢", "ÖÙ", "ÒÁ", "Äş", "³ğ", "¸Ê", "Îä",
          "·û", "Áõ", "¾°", "Õ²", "Áú", "Ò¶", "ĞÒ", "Ë¾", "Àè", "äß", "Ó¡", "»³", "ÆÑ", "Û¢", "´Ó", "Ë÷", "Àµ",
          "×¿", "ÍÀ", "³Ø", "ÇÇ", "ñã", "ÎÅ", "İ·", "µ³", "µÔ", "Ì·", "¹±", "ÀÍ", "åÌ", "¼§", "Éê", "·ö", "¶Â",
          "È½", "Ô×", "Óº", "É£", "ÊÙ", "Í¨", "Ñà", "ÆÖ", "ÉĞ", "Å©", "ÎÂ", "±ğ", "×¯", "êÌ", "²ñ", "öÄ", "ÑÖ",
          "Á¬", "Ï°", "Èİ", "Ïò", "¹Å", "Ò×", "ÁÎ", "â×", "ÖÕ", "²½", "¶¼", "¹¢", "Âú", "ºë", "¿ï", "¹ú", "ÎÄ",
          "¿Ü", "¹ã", "Â»", "ãÚ", "¶«", "Å·", "Àû", "Ê¦", "¹®", "Äô", "¹Ø", "¾£", "Ë¾Âí", "ÉÏ¹Ù", "Å·Ñô", "ÏÄºî",
          "Öî¸ğ", "ÎÅÈË", "¶«·½", "ºÕÁ¬", "»Ê¸¦", "Î¾³Ù", "¹«Ñò", "å£Ì¨", "¹«Ò±", "×ÚÕş", "å§Ñô", "´¾ÓÚ", "µ¥ÓÚ",
          "Ì«Êå", "ÉêÍÀ", "¹«Ëï", "ÖÙËï", "ĞùÔ¯", "Áîºü", "ĞìÀë", "ÓîÎÄ", "³¤Ëï", "Ä½Èİ", "Ë¾Í½", "Ë¾¿Õ" };
    /// <summary>
    /// ÄĞ
    /// </summary>
    public static string[] MaleNameItems = new[] { "ÌÎ", "²ı", "½ø", "ÁÖ", "ÓĞ", "¼á", "ºÍ", "±ë", "²©", "³Ï", "ÏÈ", "¾´",
        "Õğ", "Õñ", "×³", "»á", "Èº", "ºÀ", "ĞÄ", "°î", "³Ğ", "ÀÖ", "ÉÜ", "¹¦", "ËÉ", "ÉÆ", "ºñ", "Çì", "ÀÚ", "Ãñ",
        "ÓÑ", "Ô£", "ºÓ", "ÕÜ", "½­", "³¬", "ºÆ", "ÁÁ", "Õş", "Ç«", "ºà", "Ææ", "¹Ì", "Ö®", "ÂÖ", "º²", "ÀÊ", "²®",
        "ºê", "ÑÔ", "Èô", "Ãù", "Åó", "±ó", "Áº", "¶°", "Î¬", "Æô", "¿Ë", "Â×", "Ïè", "Ğñ", "Åô", "Ôó", "³¿", "³½",
        "Ê¿", "ÒÔ", "½¨", "¼Ò", "ÖÂ", "Ê÷", "Ñ×", "µÂ", "ĞĞ", "Ê±", "Ì©", "Ê¢", "ĞÛ", "è¡", "¾û", "¹Ú", "²ß", "ÌÚ",
        "Î°", "¸Õ", "ÓÂ", "Òã", "¿¡", "·å", "Ç¿", "¾ü", "Æ½", "±£", "¶«", "ÎÄ", "»Ô", "Á¦", "Ã÷", "ÓÀ", "½¡", "ÊÀ",
        "¹ã", "Ö¾", "Òå", "ĞË", "Á¼", "º£", "É½", "ÈÊ", "²¨", "Äş", "¹ó", "¸£", "Éú", "Áú", "Ôª", "È«", "¹ú", "Ê¤",
        "Ñ§", "Ïé", "²Å", "·¢", "³É", "¿µ", "ĞÇ", "¹â", "Ìì", "´ï", "°²", "ÑÒ", "ÖĞ", "Ã¯", "Îä", "ĞÂ", "Àû", "Çå",
        "·É", "±ò", "¸»", "Ë³", "ĞÅ", "×Ó", "½Ü", "éª", "éÅ", "·ç", "º½", "ºë" };

    /// <summary>
    /// Å®
    /// </summary>
    public static string[] FemaleNameItems = new[] { "¼Î", "Çí", "¹ğ", "æ·", "Ò¶", "èµ", "è´", "æ«", "çù", "¾§", "åû",
        "Üç","Çï", "Éº", "É¯", "½õ", "÷ì", "Çà", "Ù»", "æÃ", "æ¯", "Íñ", "æµ", "èª", "Ó±", "Â¶", "Ñş", "âù", "æ¿", "Ñã",
        "İí", "æı", "ÒÇ", "ºÉ", "µ¤", "ÈØ", "Ã¼", "¾ı", "ÇÙ", "Èï", "Ş±", "İ¼", "ÃÎ", "á°", "Ô·", "æ¼", "Ü°", "è¥",
        "çü", "ÔÏ", "ÈÚ", "Ô°", "ÒÕ", "Ó½", "Çä", "´Ï", "À½", "´¿", "Ø¹", "ÔÃ", "ÕÑ", "±ù", "Ë¬", "çş", "Üø", "Óğ",
        "Ï£", "Äş", "ĞÀ", "Æ®", "Óı", "äŞ", "ğ¥", "óŞ", "Èá", "Öñ", "ö°", "Äı", "Ïş", "»¶", "Ïö", "·ã", "Ü¿", "·Æ",
        "º®", "ÒÁ", "ÑÇ", "ÒË", "¿É", "¼§", "Êæ", "Ó°", "Àó", "Ö¦", "Ë¼", "Àö", "Ğã", "¾ê", "Ó¢", "»ª", "»Û", "ÇÉ",
        "ÃÀ", "ÄÈ", "¾²", "Êç", "»İ", "Öé", "´ä", "ÑÅ", "Ö¥", "Óñ", "Æ¼", "ºì", "¶ğ", "Áá", "·Ò", "·¼", "Ñà", "²Ê",
        "´º", "¾Õ", "ÇÚ", "Õä", "Õê", "Àò", "À¼", "·ï", "½à", "Ã·", "ÁÕ", "ËØ", "ÔÆ", "Á«", "Õæ", "»·", "Ñ©", "ÈÙ",
        "°®", "ÃÃ", "Ï¼", "Ïã", "ÔÂ", "İº", "æÂ", "ÑŞ", "Èğ", "·²", "¼Ñ","À¶" };


    private static Dictionary<int, List<string>> _mapRoleInfos = new Dictionary<int, List<string>>();

    public static bool Addition(EnumSaveRole type, string strName)
    {
        int key = (int)type;
        if (!_mapRoleInfos.ContainsKey(key))
        {
            _mapRoleInfos.Add(key, new List<string>());
        }
        if (_mapRoleInfos[key].Contains(strName))
        {
            return false;
        }
        _mapRoleInfos[key].Add(strName);
        return true;
    }
    public static void Remove(EnumSaveRole type, string strName)
    {
        int key = (int)type;
        if (_mapRoleInfos.ContainsKey(key))
        {
            _mapRoleInfos[key].Remove(strName);
        }
    }
    public static bool Contains(EnumSaveRole type, string strName)
    {
        int key = (int)type;
        if (_mapRoleInfos.ContainsKey(key))
        {
            return _mapRoleInfos[key].Contains(strName);
        }
        return false;
    }

    public static void ClearRoleInfos()
    {
        _mapRoleInfos.Clear();
    }
    public static void ClearRoleInfos(EnumSaveRole type)
    {
        int key = (int)type;
        if (_mapRoleInfos.ContainsKey(key))
        {
            _mapRoleInfos[key].Clear();
        }
    }
}
public enum EnumGender : byte
{
    None = 0,
    /// <summary>
    /// ÄĞ
    /// </summary>
    Male = 1,
    /// <summary>
    /// Å®
    /// </summary>
    Female = 2,
}
public enum EnumSaveRole : byte
{
    Name = 1,
    Icon = 2,
}
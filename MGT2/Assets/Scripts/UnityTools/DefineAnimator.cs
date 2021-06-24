using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefineAnimator
{

    private static Dictionary<int, Dictionary<int, string>> _mapAnimatorName;
    public static string GetAnimatorName(EnumAnimator type, int modType)
    {
        if (_mapAnimatorName == null)
        {
            _mapAnimatorName = new Dictionary<int, Dictionary<int, string>>();
            _mapAnimatorName.Add(0, new Dictionary<int, string>());
            AddAnimatorName(_mapAnimatorName[0], EnumAnimator.Idle, "idle");
            AddAnimatorName(_mapAnimatorName[0], EnumAnimator.Run, "run");
            AddAnimatorName(_mapAnimatorName[0], EnumAnimator.Attack, "attack");
            AddAnimatorName(_mapAnimatorName[0], EnumAnimator.Die, "die");


            _mapAnimatorName.Add(1, new Dictionary<int, string>());
            AddAnimatorName(_mapAnimatorName[1], EnumAnimator.Idle, "stand");
            AddAnimatorName(_mapAnimatorName[1], EnumAnimator.Run, "move");
            AddAnimatorName(_mapAnimatorName[1], EnumAnimator.Attack, "att01");
            AddAnimatorName(_mapAnimatorName[1], EnumAnimator.Die, "die");

        }
        if (_mapAnimatorName.ContainsKey(modType) && _mapAnimatorName[modType].ContainsKey((int)type))
        {
            return _mapAnimatorName[modType][(int)type];
        }
        return string.Empty;
    }

    public static void AddAnimatorName(Dictionary<int, string> map, EnumAnimator type, string name)
    {
        map.Add((int)type, name);
    }


}

public enum EnumAnimator
{
    Idle,
    Run,
    Attack,
    Die,
}

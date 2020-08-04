using MFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : Singleton<MonsterManager>, IUpdate
{
    private List<int> _listAllMonsters = new List<int>();
    private float _generalInterval = 2f;
    private float _tempTime = 0f;
    private int _maxCount = 1;
    public int Priority => DefinePriority.NORMAL;
    public void OnInit()
    {
        RegisterInterfaceManager.RegisteUpdate(this);
    }
    public bool Contains(int id)
    {
        return _listAllMonsters.Contains(id);
    }
    public void Remove(int id)
    {
        _listAllMonsters.Remove(id);
        AssyEntityManager.Instance.Remove(id);

    }

    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        if (_listAllMonsters.Count >= _maxCount)
        {
            return;
        }
        if (_tempTime < 0)
        {
            ClickKeyCodeE();
            _tempTime = _generalInterval;
        }
        else
        {
            _tempTime -= elapseSeconds;
        }



    }
    private void ClickKeyCodeE()
    {
        int roleId = Random.Range(2, 7);
        PrototypeRole roleData = PrototypeManager<PrototypeRole>.Instance.GetPrototype(roleId);
        if (roleData == null)
        {
            return;
        }

        Vector3 pos = new Vector3(5, 1, 5);
        //new Vector3(Random.Range(20, 30), 1, Random.Range(20, 30));
        int entityId = AssyEntityManager.Instance.GetFreeEntityKey();

        AssemblyEntityBase entity = FactoryEntity.CreateEntity(entityId);
        FactoryEntity.InitialData(DefineCamp.MONSTER, roleData, roleData.GetListAttribute(), entity);
        AssemblyRole assemblyRole = entity.GetData<AssemblyRole>(EnumAssemblyType.Role);

        FactoryEntity.InitialView(pos, assemblyRole);

        //FactoryEntity.InitialGoap(assemblyRole);

        AssyEntityManager.Instance.Addition(entityId, entity);


        MapEntityManager.Instance.AddEntity(assemblyRole);

        _listAllMonsters.Add(entityId);

    }
    public void OnRelease()
    {
        for (int cnt = 0; cnt < _listAllMonsters.Count; cnt++)
        {
            AssyEntityManager.Instance.Remove(_listAllMonsters[cnt]);
        }
        _listAllMonsters.Clear();
        RegisterInterfaceManager.UnRegisteUpdate(this);
    }


}

using MFrameWork;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGame : MonoBehaviour
{
    private static MainGame _instance;
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }
        GameStateManager.Instance.OnInit();


    }
    private void Update()
    {

        RegisterInterfaceManager.Update(Time.deltaTime, Time.unscaledDeltaTime);
    }

    private void OnDestroy()
    {
        GameStateManager.Instance.OnRelease();
    }

    #region OLD



    /*
    private Dictionary<int, RoleInfo> _mapRoleInfos = new Dictionary<int, RoleInfo>();
    private void Awake()
    {
        InitialPlayer();





    }

    public List<BulletInfo> listBullets = new List<BulletInfo>();
    private int _tempIndex = 0;
    private void Start()
    {
        Attack(0, GetRandomTarget(0, 0));
    }
    private void Update()
    {
        for (int cnt = 0; cnt < listBullets.Count; cnt++)
        {
            if (listBullets[cnt].CheckIsFinish())
            {
                listBullets[cnt].Finish();
            }
            listBullets[cnt].Update();
        }
    }
    [ContextMenu("Resett111")]
    public void ResetAtt()
    {
        Attack(0, GetRandomTarget(0, 0));
    }
    private void Attack(int index, int[] arrs)
    {

        _tempIndex = index;
    }
    private int[] GetRandomTarget(int index, int startIndex)
    {
        int length = Random.Range(1, 5);
        List<int> list = new List<int>();
        for (int i = 0; i < length; i++)
        {
            int target = startIndex + Random.Range(0, 5);
            if (list.Contains(target))
            {
                continue;
            }
            list.Add(target);
        }
        return list.ToArray();
    }
    private void InitialPlayer()
    {
        Vector3 tempPos;
        int count = 10;
        int offSet = 2;
        int startPos = 50 - (count / 2) / 2 * offSet;
        for (int cnt = 0; cnt < 10; cnt++)
        {
            if (cnt < 5)
            {
                tempPos = new Vector3(startPos + cnt * 2, 0, 45);
            }
            else
            {
                tempPos = new Vector3(startPos + (cnt - 5) * 2, 0, 55);
            }
            _mapRoleInfos.Add(cnt, CreatePlayer(cnt, gameObject, tempPos));
        }

    }
    private RoleInfo CreatePlayer(int id, GameObject parent, Vector3 pos)
    {
        RoleInfo info = new RoleInfo();
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Cube);
        obj.transform.SetParent(parent.transform);
        obj.transform.localPosition = pos;
        info.ObjRole = obj;
        info.RoleId = id;
        return info;
    }
    */
    #endregion
}

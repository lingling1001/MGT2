using MFrameWork;
using System.Collections.Generic;
using UnityEngine;

public class AssemblyAutoMove : AssemblyBase, IUpdate, IObserverAssembly
{
    public int Priority => DefinePriority.NORMAL;
    /// <summary>
    /// 目标点
    /// </summary>
    public Vector3 Start { get; private set; }

    public Vector3 EndPos { get; private set; }
    /// <summary>
    /// 移动状态
    /// </summary>
    private EnumFindPathState _moveState;
    /// <summary>
    /// 是否 移动到最终的索引
    /// </summary>
    public EnumFindPathState MoveState { get { return _moveState; } }



    private List<Vector3> _listFindPath = new List<Vector3>();
    private int _currentIdx = 0;
    private ASMapFindPathData _findData;
    private AssemblyPosition _assPosition;
    private AssemblyAttribute _assAttribute;
    private AssemblyDirection _assDirection;
    private AssemblyAnimator _assAnimator;
    private MapManager _mapManager;

    protected override void OnInit(EntityAssembly owner)
    {
        base.OnInit(owner);
        _mapManager = GameManager<MapManager>.QGetOrAddMgr();

        _assPosition = owner.GetData<AssemblyPosition>();
        _assAttribute = owner.GetData<AssemblyAttribute>();
        _assDirection = owner.GetData<AssemblyDirection>();
        _assAnimator = owner.GetData<AssemblyAnimator>();
        Owner.RegisterObserver(this);
        RegisterInterfaceManager.RegisteUpdate(this);
    }


    public void SetValue(Vector3 end)
    {
        Start = _assPosition.Position;
        EndPos = end;
        _currentIdx = 0;
        _findData = _mapManager.FindPath.FindPathNearest(Start, end);
        if (_findData == null)
        {
            return;
        }
        SetMoveState(EnumFindPathState.Initial);

    }
    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        if (_assPosition == null || _assAttribute == null)
        {
            return;
        }
        //没有开始寻路 或者 移动结束
        if (IsOverFindPath())
        {
            return;
        }
        //检测距离
        float distance = Vector3.Distance(_assPosition.Position, EndPos);
        if (distance < 0.1f)//到达目标点
        {
            StopFindPath(EnumFindPathState.Finish);
            return;
        }
        if (MoveState == EnumFindPathState.Initial)//初始化状态
        {
            if (_findData != null && _findData.IsDone())
            {
                _listFindPath = _mapManager.FindPath.ConverNodeToVectors(_findData.ListNode);
                SetMoveState(EnumFindPathState.Start);
            }
            return;
        }
        if (MoveState == EnumFindPathState.Start)//开始移动
        {
            SetMoveState(EnumFindPathState.Finding);
            return;
        }
        // Finding  寻路中 
        if (_currentIdx >= _listFindPath.Count)//没有路径点了
        {
            StopFindPath(EnumFindPathState.Finish);
            return;
        }
        Vector3 targetPos = _listFindPath[_currentIdx];
        float step = _assAttribute.GetValue(DTAttribute.SpeedMove) * Time.deltaTime;
        _assPosition.SetPosition(Vector3.MoveTowards(_assPosition.Position, targetPos, step));
        if (Vector3.Distance(_assPosition.Position, targetPos) < 0.2f)
        {
            _currentIdx = _currentIdx + 1;
        }
        _assDirection.SetValue((targetPos - _assPosition.Position).normalized);
        TryPlayAnimator(EnumAnimator.Run);
    }

    private void TryPlayAnimator(EnumAnimator type)
    {
        if (_assAnimator != null)
        {
            _assAnimator.SetValue(type);
        }
    }
    public bool IsOverFindPath()
    {
        return IsMoveOver(MoveState);
    }

    /// <summary>
    /// 设置当前是否寻路完成。
    /// </summary>
    public void SetMoveState(EnumFindPathState value)
    {
        _moveState = value;
        Owner.NotifyObserver(EnumAssemblyOperate.RoleMoveState, this);
    }
    /// <summary>
    /// 强制停止寻路
    /// </summary>
    public void StopFindPath(EnumFindPathState state = EnumFindPathState.Stop)
    {
        SetMoveState(state);
        TryPlayAnimator(EnumAnimator.Idle);

    }
    /// <summary>
    /// 重置
    /// </summary>
    public void ResetFindPath()
    {
        StopFindPath(EnumFindPathState.None);
    }

    public void UpdateAssembly(EnumAssemblyOperate operate, IAssembly data)
    {
        if (operate == EnumAssemblyOperate.JoystickMove)//摇杆操作，停止寻路
        {
            SetMoveState(EnumFindPathState.Stop);
        }
    }
    protected override void OnRelease()
    {
        RegisterInterfaceManager.UnRegisteUpdate(this);
        Owner.RemoveObserver(this);
        base.OnRelease();
    }

    public static bool IsMoveOver(EnumFindPathState state)
    {
        switch (state)
        {
            case EnumFindPathState.None:
            case EnumFindPathState.Finish:
            case EnumFindPathState.Stop:
                return true;
        }
        return false;
    }
}


public enum EnumFindPathState : byte
{
    /// <summary>
    /// 未开始
    /// </summary>
    None,
    /// <summary>
    /// 初始化
    /// </summary>
    Initial,
    /// <summary>
    /// 开始
    /// </summary>
    Start,
    /// <summary>
    /// 寻找中
    /// </summary>
    Finding,
    /// <summary>
    /// 完成
    /// </summary>
    Finish,
    /// <summary>
    /// 终止
    /// </summary>
    Stop,
}
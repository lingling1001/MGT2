using MFrameWork;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
public class AssemblyRoleMove : AssemblySelfRole, IUpdate, IObserverAssembly
{
    private FindPathList _findPathList = new FindPathList();
    public int Priority => DefinePriority.NORMAL;
    /// <summary>
    /// 目标点
    /// </summary>
    public Vector3 Position { get; private set; }
    /// <summary>
    /// 停止距离
    /// </summary>
    public float Distance { get; private set; }
    /// <summary>
    /// 移动状态
    /// </summary>
    private EnumFindPathState _moveState;
    /// <summary>
    /// 是否 移动到最终的索引
    /// </summary>
    public EnumFindPathState MoveState { get { return _moveState; } }

    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        Owner.RegisterObserver(this);
        RegisterInterfaceManager.RegisteUpdate(this);
    }
    public void SetValue(Vector3 pos, float distance = 2)
    {
        Position = pos;
        Distance = distance;
        SetMoveState(EnumFindPathState.Initial);
        _findPathList.SetIdx(1);
    }
    public void On_Update(float elapseSeconds, float realElapseSeconds)
    {
        if (SelfEntity == null)
        {
            return;
        }
        //没有开始寻路 或者 移动结束
        if (IsOverFindPath())
        {
            return;
        }
        if (!SelfEntity.Owner.ContainsKey(EnumAssemblyType.Position))
        {
            return;
        }
        //检测距离
        float distance = Vector3.Distance(SelfEntity.Position, Position);
        if (distance < Distance)//到达目标点
        {
            StopFindPath(EnumFindPathState.Finish);
            return;
        }
        if (MoveState == EnumFindPathState.Initial)//初始化状态
        {
            _findPathList.Initial(SelfEntity.Position, Position);
            SetMoveState(EnumFindPathState.Start);
            return;
        }
        if (MoveState == EnumFindPathState.Start)//开始移动
        {
            if (_findPathList.IsInitListPath())
            {
                SetMoveState(EnumFindPathState.Finding);
                SelfEntity.AssyAnimator.SetValue(EnumAnimator.Run);
            }
            return;
        }
        // Finding  寻路中 
        if (!_findPathList.HasIndex(_findPathList.Index))//没有路径点了
        {
            StopFindPath();
            return;
        }
        Vector3 targetPos = _findPathList.GetPosition();
        float step = SelfEntity.AssyAttribute.GetValue(DefineAttributeId.SPEED_MOVE) * Time.deltaTime;
        SelfEntity.AssyPosition.SetPosition(Vector3.MoveTowards(SelfEntity.Position, targetPos, step));
        SelfEntity.AssyDirection.SetValue((targetPos - SelfEntity.Position).normalized);
        if (Vector3.Distance(SelfEntity.Position, targetPos) < 0.2f)
        {
            _findPathList.SetIdx(_findPathList.Index + 1);
        }
        SelfEntity.AssyAnimator.SetValue(EnumAnimator.Run);


    }
    private bool IsOverFindPath()
    {
        switch (MoveState)
        {
            case EnumFindPathState.None:
            case EnumFindPathState.Finish:
            case EnumFindPathState.Stop:
                return true;
        }
        return false;
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
        _findPathList.Reset();
        SetMoveState(state);
        Log.Info("  Stop  Move  " + state);
        SelfEntity.AssyAnimator.SetValue(EnumAnimator.Idle);

    }
    public void UpdateAssembly(EnumAssemblyOperate operate, IAssembly data)
    {
        if (operate == EnumAssemblyOperate.JoystickMove)//摇杆操作，停止寻路
        {
            SetMoveState(EnumFindPathState.Stop);
        }

    }
    public override void OnRelease()
    {
        RegisterInterfaceManager.UnRegisteUpdate(this);
        Owner.RemoveObserver(this);
        base.OnRelease();
    }


}

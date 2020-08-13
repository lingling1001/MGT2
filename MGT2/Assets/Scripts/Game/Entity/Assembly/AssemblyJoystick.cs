using UnityEngine;
/// <summary>
/// 摇杆组件
/// </summary>
public class AssemblyJoystick : AssemblyBase
{
    private ETCJoystick _joystick;
    private AssemblyRole _assemblyRole;
    public bool IsEnable { get; private set; }
    public override void OnInit(EnumAssemblyType assemblyType, AssemblyEntityBase owner)
    {
        base.OnInit(assemblyType, owner);
        _assemblyRole = owner.GetData<AssemblyRole>(EnumAssemblyType.Role);
    }
    public void SetValue(ETCJoystick joystick)
    {
        ReleaseJoystick();
        _joystick = joystick;
        if (joystick == null)
        {
            return;
        }
        _joystick.onMove.AddListener(EventJoystickMove);
        _joystick.onMoveEnd.AddListener(EventJoystickMoveEnd);
        SetIsEnable(true);
    }
    public void SetIsEnable(bool enable)
    {
        IsEnable = enable;
        if (!enable)
        {
            if (_assemblyRole.AssyMoveToDirection.IsMove)
            {
                _assemblyRole.AssyMoveToDirection.SetMove(false);
            }
        }
    }

    private void EventJoystickMove(Vector2 pos)
    {
        if (!IsEnable)
        {
            return;
        }
        Vector3 direction = new Vector3(-pos.x, 0, pos.y);
        _assemblyRole.AssyDirection.SetValue(direction.normalized);
        _assemblyRole.AssyMoveToDirection.SetMove(true);
        Owner.NotifyObserver(EnumAssemblyOperate.JoystickMove, this);

    }
    private void EventJoystickMoveEnd()
    {
        if (!IsEnable)
        {
            return;
        }
        _assemblyRole.AssyMoveToDirection.SetMove(false);
        Owner.NotifyObserver(EnumAssemblyOperate.JoystickMove, this);

    }

    private void ReleaseJoystick()
    {
        if (_joystick != null)
        {
            _joystick.onMove.RemoveListener(EventJoystickMove);
            _joystick.onMoveEnd.RemoveListener(EventJoystickMoveEnd);
            _joystick = null;
        }

    }
    public override void OnRelease()
    {

        _joystick = null;
        base.OnRelease();
    }
}
public class AssemblyCameraFollow : AssemblyGetViewBase
{
    public override void ViewLoadFinish()
    {
        base.ViewLoadFinish();
        if (ViewObjIsNull())
        {
            return;
        }
        GameManager.QGetOrAddMgr<CameraManager>().SetFollowTarget(GetView().Trans);
    }

}
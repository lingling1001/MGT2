public class AssemblyCameraFollow : AssemblyGetViewBase
{
    public override void ViewLoadFinish()
    {
        base.ViewLoadFinish();
        if (ViewObjIsNull())
        {
            return;
        }
        
        GameManager<CameraManager>.QGetOrAddMgr().SetFollowTarget(GetView().Trans);
    }

}

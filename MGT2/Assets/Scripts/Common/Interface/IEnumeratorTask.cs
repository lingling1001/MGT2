using System.Collections;



namespace SimpleFramework
{
    public interface IEnumeratorTask
    {
        IEnumerator DoLoadAsync(System.Action finishCallback);
    }


}




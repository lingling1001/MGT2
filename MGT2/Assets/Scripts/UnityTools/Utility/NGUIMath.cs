using UnityEngine;

static public class NGUIMath
{
    /// <summary>
    /// Helper function that converts the widget's pivot enum into a 0-1 range vector.
    /// </summary>

    static public Vector2 GetPivotOffset(UtilityGrid.Pivot pv)
    {
        Vector2 v = Vector2.zero;

        if (pv == UtilityGrid.Pivot.Top || pv == UtilityGrid.Pivot.Center || pv == UtilityGrid.Pivot.Bottom) v.x = 0.5f;
        else if (pv == UtilityGrid.Pivot.TopRight || pv == UtilityGrid.Pivot.Right || pv == UtilityGrid.Pivot.BottomRight) v.x = 1f;
        else v.x = 0f;

        if (pv == UtilityGrid.Pivot.Left || pv == UtilityGrid.Pivot.Center || pv == UtilityGrid.Pivot.Right) v.y = 0.5f;
        else if (pv == UtilityGrid.Pivot.TopLeft || pv == UtilityGrid.Pivot.Top || pv == UtilityGrid.Pivot.TopRight) v.y = 1f;
        else v.y = 0f;

        return v;
    }

    /// <summary>
    /// Ensure that the angle is within -180 to 180 range.
    /// </summary>

    [System.Diagnostics.DebuggerHidden]
    [System.Diagnostics.DebuggerStepThrough]
    static public float WrapAngle(float angle)
    {
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

}
//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2015 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// All children added to the game object with this script will be repositioned to be on a grid of specified dimensions.
/// If you want the cells to automatically set their scale based on the dimensions of their content, take a look at UITable.
/// </summary>

[AddComponentMenu("NGUI/Interaction/Grid")]
public class UtilityGrid : MonoBehaviour
{
    public delegate void OnReposition();

    public enum Arrangement
    {
        Horizontal,
        Vertical,
        CellSnap,
    }

    public enum Sorting
    {
        None,
        Alphabetic,
        Horizontal,
        Vertical,
        Custom,
    }
    public enum Pivot
    {
        TopLeft,
        Top,
        TopRight,
        Left,
        Center,
        Right,
        BottomLeft,
        Bottom,
        BottomRight,
    }
    /// <summary>
    /// Type of arrangement -- vertical, horizontal or cell snap.
    /// </summary>

    public Arrangement arrangement = Arrangement.Horizontal;

    /// <summary>
    /// How to sort the grid's elements.
    /// </summary>

    public Sorting sorting = Sorting.None;

    /// <summary>
    /// Final pivot point for the grid's content.
    /// </summary>

    public Pivot pivot = Pivot.TopLeft;

    /// <summary>
    /// Maximum children per line.
    /// If the arrangement is horizontal, this denotes the number of columns.
    /// If the arrangement is vertical, this stands for the number of rows.
    /// </summary>

    public int maxPerLine = 0;

    /// <summary>
    /// The width of each of the cells.
    /// </summary>

    public float cellWidth = 200f;

    /// <summary>
    /// The height of each of the cells.
    /// </summary>

    public float cellHeight = 200f;

    /// <summary>
    /// Whether the grid will smoothly animate its children into the correct place.
    /// </summary>

    //public bool animateSmoothly = false;

    /// <summary>
    /// Whether to ignore the disabled children or to treat them as being present.
    /// </summary>

    public bool hideInactive = false;




    /// <summary>
    /// Callback triggered when the grid repositions its contents.
    /// </summary>

    public OnReposition onReposition;

    /// <summary>
    /// Custom sort delegate, used when the sorting method is set to 'custom'.
    /// </summary>

    public System.Comparison<Transform> onCustomSort;

    // Use the 'sorting' property instead
    [HideInInspector] [SerializeField] bool sorted = false;

    protected bool mReposition = false;
    protected bool mInitDone = false;

    /// <summary>
    /// Reposition the children on the next Update().
    /// </summary>

    public bool repositionNow { set { if (value) { mReposition = true; enabled = true; } } }

    /// <summary>
    /// Get the current list of the grid's children.
    /// </summary>

    public List<Transform> GetChildList()
    {
        Transform myTrans = transform;
        List<Transform> list = new List<Transform>();

        for (int i = 0; i < myTrans.childCount; ++i)
        {
            Transform t = myTrans.GetChild(i);
            if (!hideInactive || (t && NGUITools.GetActive(t.gameObject)))
                list.Add(t);
        }

        // Sort the list using the desired sorting logic
        if (sorting != Sorting.None && arrangement != Arrangement.CellSnap)
        {
            if (sorting == Sorting.Alphabetic) list.Sort(SortByName);
            else if (sorting == Sorting.Horizontal) list.Sort(SortHorizontal);
            else if (sorting == Sorting.Vertical) list.Sort(SortVertical);
            else if (onCustomSort != null) list.Sort(onCustomSort);
            else Sort(list);
        }
        return list;
    }

    /// <summary>
    /// Convenience method: get the child at the specified index.
    /// Note that if you plan on calling this function more than once, it's faster to get the entire list using GetChildList() instead.
    /// </summary>

    public Transform GetChild(int index)
    {
        List<Transform> list = GetChildList();
        return (index < list.Count) ? list[index] : null;
    }

    /// <summary>
    /// Get the index of the specified item.
    /// </summary>

    public int GetIndex(Transform trans) { return GetChildList().IndexOf(trans); }

    /// <summary>
    /// Convenience method -- add a new child.
    /// </summary>

    public void AddChild(Transform trans) { AddChild(trans, true); }

    public GameObject AddChild(GameObject prefab)
    {
        GameObject go = NGUITools.AddChild(gameObject, prefab);
        Vector3 v = go.transform.localPosition;
        v.z -= GetChildList().Count;
        v.x -= GetChildList().Count;
        go.transform.localPosition = v;
        return go;
    }
    /// <summary>
    /// Convenience method -- add a new child.
    /// Note that if you plan on adding multiple objects, it's faster to GetChildList() and modify that instead.
    /// </summary>

    public void AddChild(Transform trans, bool sort)
    {
        if (trans != null)
        {
            List<Transform> list = GetChildList();
            list.Add(trans);
            ResetPosition(list);
        }
    }

    // NOTE: This functionality is effectively removed until Unity 4.6.
    /*/// <summary>
	/// Convenience method -- add a new child at the specified index.
	/// Note that if you plan on adding multiple objects, it's faster to GetChildList() and modify that instead.
	/// </summary>

	public void AddChild (Transform trans, int index)
	{
		if (trans != null)
		{
			if (sorting != Sorting.None)
				Debug.LogWarning("The Grid has sorting enabled, so AddChild at index may not work as expected.", this);

			BetterList<Transform> list = GetChildList();
			list.Insert(index, trans);
			ResetPosition(list);
		}
	}

	/// <summary>
	/// Convenience method -- remove a child at the specified index.
	/// Note that if you plan on removing multiple objects, it's faster to GetChildList() and modify that instead.
	/// </summary>

	public Transform RemoveChild (int index)
	{
		BetterList<Transform> list = GetChildList();

		if (index < list.Count)
		{
			Transform t = list[index];
			list.RemoveAt(index);
			ResetPosition(list);
			return t;
		}
		return null;
	}*/

    /// <summary>
    /// Remove the specified child from the list.
    /// Note that if you plan on removing multiple objects, it's faster to GetChildList() and modify that instead.
    /// </summary>

    public bool RemoveChild(Transform t)
    {
        List<Transform> list = GetChildList();

        if (list.Remove(t))
        {
            ResetPosition(list);
            return true;
        }
        return false;
    }

    /// <summary>
    /// Initialize the grid. Executed only once.
    /// </summary>

    protected virtual void Init()
    {
        mInitDone = true;
    }

    /// <summary>
    /// Cache everything and reset the initial position of all children.
    /// </summary>

    protected virtual void Start()
    {
        if (!mInitDone) Init();
        Reposition();
        enabled = false;
    }

    /// <summary>
    /// Reset the position if necessary, then disable the component.
    /// </summary>

    protected virtual void Update()
    {
        Reposition();
        enabled = false;
    }

    /// <summary>
    /// Reposition the content on inspector validation.
    /// </summary>

    void OnValidate() { if (!Application.isPlaying && NGUITools.GetActive(this)) Reposition(); }

    // Various generic sorting functions
    static public int SortByName(Transform a, Transform b) { return string.Compare(a.name, b.name); }
    static public int SortHorizontal(Transform a, Transform b) { return a.localPosition.x.CompareTo(b.localPosition.x); }
    static public int SortVertical(Transform a, Transform b) { return b.localPosition.y.CompareTo(a.localPosition.y); }
    static public int SortNumber(Transform a, Transform b) { return int.Parse(a.name).CompareTo(int.Parse(b.name)); }

    /// <summary>
    /// You can override this function, but in most cases it's easier to just set the onCustomSort delegate instead.
    /// </summary>

    protected virtual void Sort(List<Transform> list) { }

    /// <summary>
    /// Recalculate the position of all elements within the grid, sorting them alphabetically if necessary.
    /// </summary>

    [ContextMenu("Execute")]
    public virtual void Reposition()
    {
        if (Application.isPlaying && !mInitDone && NGUITools.GetActive(gameObject)) Init();

        // Legacy functionality
        if (sorted)
        {
            sorted = false;
            if (sorting == Sorting.None)
                sorting = Sorting.Alphabetic;
            NGUITools.SetDirty(this);
        }

        // Get the list of children in their current order
        List<Transform> list = GetChildList();

        // Reset the position and order of all objects in the list
        ResetPosition(list);

        // Constrain everything to be within the panel's bounds
        //if (keepWithinPanel) ConstrainWithinPanel();

        // Notify the listener
        if (onReposition != null)
            onReposition();
    }

    /// <summary>
    /// Constrain the grid's content to be within the panel's bounds.
    /// </summary>

    //public void ConstrainWithinPanel ()
    //{
    //	if (mPanel != null)
    //	{
    //		mPanel.ConstrainTargetToBounds(transform, true);
    //		UIScrollView sv = mPanel.GetComponent<UIScrollView>();
    //		if (sv != null) sv.UpdateScrollbars(true);
    //	}
    //}

    /// <summary>
    /// Reset the position of all child objects based on the order of items in the list.
    /// </summary>

    protected virtual void ResetPosition(List<Transform> list)
    {
        mReposition = false;

        // Epic hack: Unparent all children so that we get to control the order in which they are re-added back in
        // EDIT: Turns out this does nothing.
        //for (int i = 0, imax = list.Count; i < imax; ++i)
        //	list[i].parent = null;

        int x = 0;
        int y = 0;
        int maxX = 0;
        int maxY = 0;
        Transform myTrans = transform;

        // Re-add the children in the same order we have them in and position them accordingly
        for (int i = 0, imax = list.Count; i < imax; ++i)
        {
            Transform t = list[i];
            // See above
            //t.parent = myTrans;

            Vector3 pos = t.localPosition;
            float depth = pos.z;

            if (arrangement == Arrangement.CellSnap)
            {
                if (cellWidth > 0) pos.x = Mathf.Round(pos.x / cellWidth) * cellWidth;
                if (cellHeight > 0) pos.z = Mathf.Round(pos.z / cellHeight) * cellHeight;
            }
            else
            {
                pos = (arrangement == Arrangement.Horizontal) ?
              new Vector3(cellWidth * x,0, -cellHeight * y) :
              new Vector3(cellWidth * y,0, -cellHeight * x);
            }

            //if (animateSmoothly && Application.isPlaying)
            //{
            //	SpringPosition sp = SpringPosition.Begin(t.gameObject, pos, 15f);
            //	sp.updateScrollView = true;
            //	sp.ignoreTimeScale = true;
            //}
            //else t.localPosition = pos;
            t.localPosition = pos;
            maxX = Mathf.Max(maxX, x);
            maxY = Mathf.Max(maxY, y);

            if (++x >= maxPerLine && maxPerLine > 0)
            {
                x = 0;
                ++y;
            }
        }

        // Apply the origin offset
        if (pivot != Pivot.TopLeft)
        {
            Vector2 po = NGUIMath.GetPivotOffset(pivot);

            float fx, fy;

            if (arrangement == Arrangement.Horizontal)
            {
                fx = Mathf.Lerp(0f, maxX * cellWidth, po.x);
                fy = Mathf.Lerp(-maxY * cellHeight, 0f, po.y);
            }
            else
            {
                fx = Mathf.Lerp(0f, maxY * cellWidth, po.x);
                fy = Mathf.Lerp(-maxX * cellHeight, 0f, po.y);
            }

            for (int i = 0; i < myTrans.childCount; ++i)
            {
                Transform t = myTrans.GetChild(i);
                //SpringPosition sp = t.GetComponent<SpringPosition>();

                //if (sp != null)
                //{
                //	sp.target.x -= fx;
                //	sp.target.y -= fy;
                //}
                //else
                //{
                Vector3 pos = t.localPosition;
                pos.x -= fx;
                pos.z -= fy;
                t.localPosition = pos;
                //}
            }
        }
    }
}

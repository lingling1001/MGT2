//----------------------------------------------
//			  NGUI: Next-Gen UI kit
// Copyright © 2011-2015 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Reflection;

[CanEditMultipleObjects]
[CustomEditor(typeof(Transform), true)]
public class NGUITransformInspector : Editor
{
	static public NGUITransformInspector instance;

	SerializedProperty mPos;
	SerializedProperty mRot;
	SerializedProperty mScale;

	void OnEnable()
	{
		instance = this;

		mPos = serializedObject.FindProperty("m_LocalPosition");
		mRot = serializedObject.FindProperty("m_LocalRotation");
		mScale = serializedObject.FindProperty("m_LocalScale");
	}

	void OnDestroy() { instance = null; }

	/// <summary>
	/// Draw the inspector widget.
	/// </summary>

	public override void OnInspectorGUI()
	{
		NGUIEditorTools.SetLabelWidth(15f);

		serializedObject.Update();

		bool widgets = false;

		//foreach (Object obj in serializedObject.targetObjects)
		//{
		//	Transform t = obj as Transform;

		//	if (t.GetComponent<UIWidget>() != null)
		//	{
		//		widgets = true;
		//		break;
		//	}
		//}

		DrawPosition();
		DrawRotation(widgets);
		DrawScale(widgets);
		DrawCopyComponent();
		serializedObject.ApplyModifiedProperties();

		if (NGUISettings.unifiedTransform)
		{
			NGUIEditorTools.SetLabelWidth(80f);

			//if (UIWidgetInspector.instance != null)
			//{
			//	UIWidgetInspector.instance.serializedObject.Update();
			//	UIWidgetInspector.instance.DrawWidgetTransform();
			//	if (NGUISettings.minimalisticLook) GUILayout.Space(-4f);
			//	UIWidgetInspector.instance.serializedObject.ApplyModifiedProperties();
			//}

			//if (UIRectEditor.instance != null)
			//{
			//	UIRectEditor.instance.serializedObject.Update();
			//	UIRectEditor.instance.DrawAnchorTransform();
			//	UIRectEditor.instance.serializedObject.ApplyModifiedProperties();
			//}
		}
	}

	void DrawPosition()
	{
		GUILayout.BeginHorizontal();
		bool reset = GUILayout.Button("P", GUILayout.Width(20f));
		EditorGUILayout.PropertyField(mPos.FindPropertyRelative("x"));
		EditorGUILayout.PropertyField(mPos.FindPropertyRelative("y"));
		EditorGUILayout.PropertyField(mPos.FindPropertyRelative("z"));
		GUILayout.EndHorizontal();

		//GUILayout.BeginHorizontal();
		//reset = GUILayout.Button("W", GUILayout.Width(20f));
		//EditorGUILayout.Vector3Field("", (target as Transform).position);

		if (reset) mPos.vector3Value = Vector3.zero;
		//GUILayout.EndHorizontal();
	}

	void DrawScale(bool isWidget)
	{
		GUILayout.BeginHorizontal();
		{
			bool reset = GUILayout.Button("S", GUILayout.Width(20f));

			if (isWidget) GUI.color = new Color(0.7f, 0.7f, 0.7f);
			EditorGUILayout.PropertyField(mScale.FindPropertyRelative("x"));
			EditorGUILayout.PropertyField(mScale.FindPropertyRelative("y"));
			EditorGUILayout.PropertyField(mScale.FindPropertyRelative("z"));
			if (isWidget) GUI.color = Color.white;

			if (reset) mScale.vector3Value = Vector3.one;
		}
		GUILayout.EndHorizontal();
	}

	#region Rotation is ugly as hell... since there is no native support for quaternion property drawing
	enum Axes : int
	{
		None = 0,
		X = 1,
		Y = 2,
		Z = 4,
		All = 7,
	}

	Axes CheckDifference(Transform t, Vector3 original)
	{
		Vector3 next = t.localEulerAngles;

		Axes axes = Axes.None;

		if (Differs(next.x, original.x)) axes |= Axes.X;
		if (Differs(next.y, original.y)) axes |= Axes.Y;
		if (Differs(next.z, original.z)) axes |= Axes.Z;

		return axes;
	}

	Axes CheckDifference(SerializedProperty property)
	{
		Axes axes = Axes.None;

		if (property.hasMultipleDifferentValues)
		{
			Vector3 original = property.quaternionValue.eulerAngles;

			foreach (Object obj in serializedObject.targetObjects)
			{
				axes |= CheckDifference(obj as Transform, original);
				if (axes == Axes.All) break;
			}
		}
		return axes;
	}

	/// <summary>
	/// Draw an editable float field.
	/// </summary>
	/// <param name="hidden">Whether to replace the value with a dash</param>
	/// <param name="greyedOut">Whether the value should be greyed out or not</param>

	static bool FloatField(string name, ref float value, bool hidden, bool greyedOut, GUILayoutOption opt)
	{
		float newValue = value;
		GUI.changed = false;

		if (!hidden)
		{
			if (greyedOut)
			{
				GUI.color = new Color(0.7f, 0.7f, 0.7f);
				newValue = EditorGUILayout.FloatField(name, newValue, opt);
				GUI.color = Color.white;
			}
			else
			{
				newValue = EditorGUILayout.FloatField(name, newValue, opt);
			}
		}
		else if (greyedOut)
		{
			GUI.color = new Color(0.7f, 0.7f, 0.7f);
			float.TryParse(EditorGUILayout.TextField(name, "--", opt), out newValue);
			GUI.color = Color.white;
		}
		else
		{
			float.TryParse(EditorGUILayout.TextField(name, "--", opt), out newValue);
		}

		if (GUI.changed && Differs(newValue, value))
		{
			value = newValue;
			return true;
		}
		return false;
	}

	/// <summary>
	/// Because Mathf.Approximately is too sensitive.
	/// </summary>

	static bool Differs(float a, float b) { return Mathf.Abs(a - b) > 0.0001f; }

	void DrawRotation(bool isWidget)
	{
		GUILayout.BeginHorizontal();
		{
			bool reset = GUILayout.Button("R", GUILayout.Width(20f));

			Vector3 visible = (serializedObject.targetObject as Transform).localEulerAngles;

			visible.x = NGUIMath.WrapAngle(visible.x);
			visible.y = NGUIMath.WrapAngle(visible.y);
			visible.z = NGUIMath.WrapAngle(visible.z);

			Axes changed = CheckDifference(mRot);
			Axes altered = Axes.None;

			GUILayoutOption opt = GUILayout.MinWidth(30f);

			if (FloatField("X", ref visible.x, (changed & Axes.X) != 0, isWidget, opt)) altered |= Axes.X;
			if (FloatField("Y", ref visible.y, (changed & Axes.Y) != 0, isWidget, opt)) altered |= Axes.Y;
			if (FloatField("Z", ref visible.z, (changed & Axes.Z) != 0, false, opt)) altered |= Axes.Z;

			if (reset)
			{
				mRot.quaternionValue = Quaternion.identity;
			}
			else if (altered != Axes.None)
			{
				NGUIEditorTools.RegisterUndo("Change Rotation", serializedObject.targetObjects);

				foreach (Object obj in serializedObject.targetObjects)
				{
					Transform t = obj as Transform;
					Vector3 v = t.localEulerAngles;

					if ((altered & Axes.X) != 0) v.x = visible.x;
					if ((altered & Axes.Y) != 0) v.y = visible.y;
					if ((altered & Axes.Z) != 0) v.z = visible.z;

					t.localEulerAngles = v;
				}
			}
		}
		GUILayout.EndHorizontal();
	}
	#endregion


	#region TEST

	public enum OperateType
	{
		Copy,
		Auto,
	}
	private OperateType type = OperateType.Copy;
	private System.Collections.Generic.List<System.Type> listCompons = new System.Collections.Generic.List<System.Type>();

	private void DrawCopyComponent()
	{
		EditorGUILayout.BeginHorizontal();
		if (GUILayout.Button("CP"))
		{
			CopyName(serializedObject.targetObject.name);
		}
		//if (GUILayout.Button("LAB"))
		//{
		//    CopyNameComponent("UILabel");
		//}
		if (GUILayout.Button("CP"))
		{
			SetListDatas();
			Transform trans = serializedObject.targetObject as Transform;
			listCompons.Insert(0, trans.gameObject.GetType());
			type = OperateType.Copy;
		}
		if (GUILayout.Button("AU"))
		{
			SetListDatas();
			type = OperateType.Auto;
		}
		EditorGUILayout.EndHorizontal();
		for (int cnt = 0; cnt < listCompons.Count; cnt++)
		{
			if (GUILayout.Button(listCompons[cnt].Name))
			{
				if (type == OperateType.Copy)
				{
					CopyNameComponent(listCompons[cnt].Name);
				}
				else if (type == OperateType.Auto)
				{
					AutoSetFiled(listCompons[cnt]);
				}
				listCompons.Clear();
			}
		}
	}
	private void SetListDatas()
	{
		listCompons.Clear();
		Transform trans = serializedObject.targetObject as Transform;
		Component[] coms = trans.GetComponents<Component>();
		for (int cnt = 0; cnt < coms.Length; cnt++)
		{
			listCompons.Add(coms[cnt].GetType());
		}
	}
	private void CopyNameComponent(string param1)
	{
		CopyName(string.Format("    [SerializeField] private {0} {1};", param1, serializedObject.targetObject.name));
	}
	private void CopyName(string param1)
	{
		TextEditor text = new TextEditor();
		text.text = param1;
		text.OnFocus();
		text.Copy();
		listCompons.Clear();
	}

	private void AutoSetFiled(System.Type type)
	{
		try
		{
			if (serializedObject == null)
			{
				return;
			}
			Transform trans = serializedObject.targetObject as Transform;
			if (trans == null)
			{
				return;
			}
			Component thisComponent = trans.GetComponent(type);
			//获取所有字段
			List<FieldInfo> listFis = new List<FieldInfo>();
			listFis.AddRange(type.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance));
			listFis.AddRange(type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance));
			System.Collections.Generic.List<GameObject> list = new System.Collections.Generic.List<GameObject>();
			for (int cnt = 0; cnt < listFis.Count; cnt++)
			{
				list.Clear();
				list.Add(trans.gameObject);
				NGUITools.FindHideChildGameObject(list, trans, listFis[cnt].Name);
				for (int i = 0; i < list.Count; i++)
				{
					if (listFis[cnt].Name == list[i].name)
					{
						if (listFis[cnt].FieldType.Name == "GameObject")
						{
							listFis[cnt].SetValue(thisComponent, list[i]);
						}
						else
						{
							Component com = list[i].GetComponent(listFis[cnt].FieldType);
							if (com == null)
							{
								continue;
							}
							if (listFis[cnt].FieldType == com.GetType())
							{
								listFis[cnt].SetValue(thisComponent, com);
							}
							else
							{
								Debug.LogError(listFis[cnt].FieldType.Name + "   " + com.GetType().Name);
							}
						}
						continue;
					}
				}
			}
		}
		catch (System.Exception ex)
		{
			Debug.LogError(ex.ToString());
		}
	}
	#endregion
}

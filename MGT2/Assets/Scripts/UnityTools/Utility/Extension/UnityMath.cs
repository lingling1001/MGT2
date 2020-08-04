//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2019 Jiang Yin. All rights reserved.
// Homepage: http://gameframework.cn/
// Feedback: mailto:jiangyin@gameframework.cn
//------------------------------------------------------------

using UnityEngine;

public class UnityMath
{
	/// <summary>
	/// 获取两点之间距离一定百分比的一个点
	/// </summary>
	/// <param name="start">起始点</param>
	/// <param name="end">结束点</param>
	/// <param name="distance">起始点到目标点距离百分比</param>
	/// <returns></returns>
	public static Vector3 GetBetweenPoint(Vector3 start, Vector3 end, float percent)
    {
        Vector3 normal = (end - start).normalized;
        float distance = Vector3.Distance(start, end);
        return normal * (distance * percent) + start;
    }

	public static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
	{
		float u = 1 - t;
		float tt = t * t;
		float uu = u * u;

		Vector3 p = uu * p0;
		p += 2 * u * t * p1;
		p += tt * p2;

		return p;
	}

	public static Vector3[] GetBeizerList(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint, int segmentNum)
	{
		Vector3[] path = new Vector3[segmentNum];
		for (int i = 1; i <= segmentNum; i++)
		{
			float t = i / (float)segmentNum;
			Vector3 pixel = CalculateCubicBezierPoint(t, startPoint,
				controlPoint, endPoint);
			path[i - 1] = pixel;

		}
		return path;
	}
}
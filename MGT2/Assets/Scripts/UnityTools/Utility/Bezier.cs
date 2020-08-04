using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bezier
{
	private static Vector3 CalculateCubicBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
	{
		float u = 1 - t;
		float tt = t * t;
		float uu = u * u;

		Vector3 p = uu * p0;
		p += 2 * u * t * p1;
		p += tt * p2;

		return p;
	}

	public static Vector3 [] GetBeizerList(Vector3 startPoint, Vector3 controlPoint, Vector3 endPoint,int segmentNum)
	{
		Vector3 [] path = new Vector3[segmentNum];
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
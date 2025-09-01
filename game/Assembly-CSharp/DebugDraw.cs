using System;
using UnityEngine;

// Token: 0x02000228 RID: 552
public static class DebugDraw
{
	// Token: 0x0600171D RID: 5917 RVA: 0x0009269C File Offset: 0x0009089C
	public static void Sphere(Vector3 origin, float radius, Color color, float duration = 0f)
	{
		int num = 6;
		Quaternion identity = Quaternion.identity;
		int segments = num * 2;
		float num2 = 180f / (float)num;
		for (int i = 0; i < num; i++)
		{
			DebugDraw.DrawCircle(origin, identity * Quaternion.Euler(0f, num2 * (float)i, 0f), radius, segments, color, duration);
		}
		float num3 = 3.1415927f / (float)num;
		for (int j = 1; j < num; j++)
		{
			float f = num3 * (float)j;
			Vector3 b = identity * Vector3.up * Mathf.Cos(f) * radius;
			float radius2 = Mathf.Sin(f) * radius;
			DebugDraw.DrawCircle(origin + b, identity * Quaternion.Euler(90f, 0f, 0f), radius2, segments, color, duration);
		}
	}

	// Token: 0x0600171E RID: 5918 RVA: 0x0009276C File Offset: 0x0009096C
	public static void Circle(Vector3 origin, Vector3 forward, float radius, Color color, float duration = 0f)
	{
		Quaternion rotation = Quaternion.LookRotation(forward);
		DebugDraw.DrawCircle(origin, rotation, radius, 8 * Mathf.CeilToInt(Mathf.Sqrt(radius)), color, duration);
	}

	// Token: 0x0600171F RID: 5919 RVA: 0x00092798 File Offset: 0x00090998
	private static void DrawCircle(Vector3 position, Quaternion rotation, float radius, int segments, Color color, float duration)
	{
		if (radius <= 0f || segments <= 0)
		{
			return;
		}
		float num = 360f / (float)segments;
		num *= 0.017453292f;
		Vector3 vector = Vector3.zero;
		Vector3 vector2 = Vector3.zero;
		for (int i = 0; i < segments; i++)
		{
			vector.x = Mathf.Cos(num * (float)i);
			vector.y = Mathf.Sin(num * (float)i);
			vector.z = 0f;
			vector2.x = Mathf.Cos(num * (float)(i + 1));
			vector2.y = Mathf.Sin(num * (float)(i + 1));
			vector2.z = 0f;
			vector *= radius;
			vector2 *= radius;
			vector = rotation * vector;
			vector2 = rotation * vector2;
			vector += position;
			vector2 += position;
			Debug.DrawLine(vector, vector2, color, duration);
		}
	}
}

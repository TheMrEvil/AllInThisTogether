using System;
using UnityEngine;

// Token: 0x02000234 RID: 564
public class MathHelper : MonoBehaviour
{
	// Token: 0x06001747 RID: 5959 RVA: 0x0009344B File Offset: 0x0009164B
	private static Transform Instance()
	{
		if (MathHelper.instance == null)
		{
			MathHelper.instance = new GameObject("_math_helper").AddComponent<MathHelper>();
		}
		return MathHelper.instance.transform;
	}

	// Token: 0x06001748 RID: 5960 RVA: 0x00093478 File Offset: 0x00091678
	public static Vector3 GetRelativePoint(Vector3 point, Vector3 origin, Vector3 forward)
	{
		Transform transform = MathHelper.Instance();
		transform.position = origin;
		transform.forward = forward;
		return transform.TransformVector(point);
	}

	// Token: 0x06001749 RID: 5961 RVA: 0x00093493 File Offset: 0x00091693
	public static Vector3 GetRelativeDir(Vector3 dir, Vector3 forward, Vector3 up)
	{
		Transform transform = MathHelper.Instance();
		transform.LookAt(transform.position + forward, up);
		return transform.TransformDirection(dir);
	}

	// Token: 0x0600174A RID: 5962 RVA: 0x000934B3 File Offset: 0x000916B3
	public static Transform GetDirectionTransform(Vector3 dir, Vector3 up)
	{
		Transform transform = MathHelper.Instance();
		transform.LookAt(transform.position + dir, up);
		return transform;
	}

	// Token: 0x0600174B RID: 5963 RVA: 0x000934CD File Offset: 0x000916CD
	public MathHelper()
	{
	}

	// Token: 0x0400170C RID: 5900
	protected static MathHelper instance;
}

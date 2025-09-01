using System;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x02000243 RID: 579
public class TestButton : MonoBehaviour
{
	// Token: 0x06001798 RID: 6040 RVA: 0x00094708 File Offset: 0x00092908
	private void OnDrawGizmos()
	{
		Vector3 position = base.transform.position;
		float radius = 0.05f;
		float radius2 = 3f;
		Gizmos.DrawWireSphere(position, radius2);
		BetterGizmos.DrawSphere(Color.blue, position, radius);
		int count = this.Count;
		float num = 10f;
		for (int i = 0; i < count; i++)
		{
			float f = (float)(9 + 8 * (i + 1));
			int num2 = Mathf.CeilToInt((-3f + Mathf.Sqrt(f)) / 4f);
			int num3 = 2 * (num2 - 1) * (num2 - 1) + 3 * (num2 - 1);
			int num4 = i - num3;
			float num5 = 3.1415927f / (num * (float)num2);
			float num6;
			if (num4 % 2 == 0)
			{
				num6 = num5 * (float)num4;
			}
			else
			{
				num6 = -(num5 * (float)(num4 + 1));
			}
			float d = 0.5f + (float)num2 * 0.3f;
			num6 += 1.5707964f;
			float num7 = Mathf.Cos(num6);
			float y = Mathf.Abs(Mathf.Sin(num6));
			float z = -Mathf.Abs(num7 / 3f) - Mathf.Floor((float)i / (num / 2f)) * 0.1f;
			Vector3 position2 = position + d * new Vector3(num7, y, z);
			BetterGizmos.DrawSphere(Color.green, position2, radius);
		}
		for (int j = 0; j < count; j++)
		{
		}
	}

	// Token: 0x06001799 RID: 6041 RVA: 0x0009485F File Offset: 0x00092A5F
	public TestButton()
	{
	}

	// Token: 0x0400176D RID: 5997
	[Range(1f, 25f)]
	public int Count = 10;
}

using System;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x02000233 RID: 563
public class MapLevelHelper : MonoBehaviour
{
	// Token: 0x06001744 RID: 5956 RVA: 0x00093254 File Offset: 0x00091454
	private void OnDrawGizmos()
	{
		Vector3 origin = base.transform.position + Vector3.up * 5f;
		RaycastHit raycastHit;
		if (!Physics.Raycast(new Ray(origin, Vector3.down), out raycastHit, 25f))
		{
			return;
		}
		Vector3 point = raycastHit.point;
		float num = 1.85f;
		float d = 3.25f;
		BetterGizmos.DrawViewFacingArrow(Color.blue, point, point + Vector3.up * d, 0.5f, BetterGizmos.DownsizeDisplay.Squash, BetterGizmos.UpsizeDisplay.Offset);
		BetterGizmos.DrawCapsule(Color.blue, point, point + Vector3.up * (num - 0.25f), 0.5f);
		Vector3 vector = point + Vector3.up * 1f;
		Color color = Color.green;
		color.a = 0.5f;
		if (this.CheckRadius(vector))
		{
			color = Color.red;
		}
		BetterGizmos.DrawCircle2D(color, vector, Vector3.up, 4f);
		BetterGizmos.DrawCircle2D(Color.blue, vector - Vector3.up * 0.1f, Vector3.up, 2.5f);
	}

	// Token: 0x06001745 RID: 5957 RVA: 0x00093374 File Offset: 0x00091574
	private bool CheckRadius(Vector3 tPt)
	{
		new Ray(tPt, Vector3.down);
		int num = 10;
		Vector3 forward = Vector3.forward;
		for (int i = 0; i < 360; i += num)
		{
			Vector3 direction = Quaternion.Euler(0f, (float)i, 0f) * forward;
			RaycastHit raycastHit;
			if (Physics.Raycast(new Ray(tPt, direction), out raycastHit, 8f))
			{
				Vector3 point = raycastHit.point;
				float maxDistance = 8f - raycastHit.distance;
				if (Physics.Raycast(new Ray(tPt, Quaternion.Euler(0f, (float)(i + 180), 0f) * forward), out raycastHit, maxDistance))
				{
					Debug.DrawLine(tPt, raycastHit.point, Color.red);
					Debug.DrawLine(tPt, point, Color.red);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06001746 RID: 5958 RVA: 0x00093443 File Offset: 0x00091643
	public MapLevelHelper()
	{
	}

	// Token: 0x0400170B RID: 5899
	private const float CORRIDOR_DIST = 8f;
}

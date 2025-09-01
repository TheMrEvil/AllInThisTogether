using System;
using UnityEngine;

// Token: 0x02000034 RID: 52
public class CeilingDetector : MonoBehaviour
{
	// Token: 0x0600018A RID: 394 RVA: 0x0000F487 File Offset: 0x0000D687
	private void Awake()
	{
		this.tr = base.transform;
	}

	// Token: 0x0600018B RID: 395 RVA: 0x0000F495 File Offset: 0x0000D695
	private void OnCollisionEnter(Collision _collision)
	{
		this.CheckCollisionAngles(_collision);
	}

	// Token: 0x0600018C RID: 396 RVA: 0x0000F49E File Offset: 0x0000D69E
	private void OnCollisionStay(Collision _collision)
	{
		this.CheckCollisionAngles(_collision);
	}

	// Token: 0x0600018D RID: 397 RVA: 0x0000F4A8 File Offset: 0x0000D6A8
	private void CheckCollisionAngles(Collision _collision)
	{
		float num = 0f;
		if (this.ceilingDetectionMethod == CeilingDetector.CeilingDetectionMethod.OnlyCheckFirstContact)
		{
			num = Vector3.Angle(-this.tr.up, _collision.contacts[0].normal);
			if (num < this.ceilingAngleLimit)
			{
				this.ceilingWasHit = true;
			}
			if (this.isInDebugMode)
			{
				Debug.DrawRay(_collision.contacts[0].point, _collision.contacts[0].normal, Color.red, this.debugDrawDuration);
			}
		}
		if (this.ceilingDetectionMethod == CeilingDetector.CeilingDetectionMethod.CheckAllContacts)
		{
			for (int i = 0; i < _collision.contacts.Length; i++)
			{
				num = Vector3.Angle(-this.tr.up, _collision.contacts[i].normal);
				if (num < this.ceilingAngleLimit)
				{
					this.ceilingWasHit = true;
				}
				if (this.isInDebugMode)
				{
					Debug.DrawRay(_collision.contacts[i].point, _collision.contacts[i].normal, Color.red, this.debugDrawDuration);
				}
			}
		}
		if (this.ceilingDetectionMethod == CeilingDetector.CeilingDetectionMethod.CheckAverageOfAllContacts)
		{
			for (int j = 0; j < _collision.contacts.Length; j++)
			{
				num += Vector3.Angle(-this.tr.up, _collision.contacts[j].normal);
				if (this.isInDebugMode)
				{
					Debug.DrawRay(_collision.contacts[j].point, _collision.contacts[j].normal, Color.red, this.debugDrawDuration);
				}
			}
			if (num / (float)_collision.contacts.Length < this.ceilingAngleLimit)
			{
				this.ceilingWasHit = true;
			}
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x0000F660 File Offset: 0x0000D860
	public bool HitCeiling()
	{
		return this.ceilingWasHit;
	}

	// Token: 0x0600018F RID: 399 RVA: 0x0000F668 File Offset: 0x0000D868
	public void ResetFlags()
	{
		this.ceilingWasHit = false;
	}

	// Token: 0x06000190 RID: 400 RVA: 0x0000F671 File Offset: 0x0000D871
	public CeilingDetector()
	{
	}

	// Token: 0x040001D3 RID: 467
	private bool ceilingWasHit;

	// Token: 0x040001D4 RID: 468
	public float ceilingAngleLimit = 10f;

	// Token: 0x040001D5 RID: 469
	public CeilingDetector.CeilingDetectionMethod ceilingDetectionMethod;

	// Token: 0x040001D6 RID: 470
	public bool isInDebugMode;

	// Token: 0x040001D7 RID: 471
	private float debugDrawDuration = 2f;

	// Token: 0x040001D8 RID: 472
	private Transform tr;

	// Token: 0x020003FE RID: 1022
	public enum CeilingDetectionMethod
	{
		// Token: 0x0400212C RID: 8492
		OnlyCheckFirstContact,
		// Token: 0x0400212D RID: 8493
		CheckAllContacts,
		// Token: 0x0400212E RID: 8494
		CheckAverageOfAllContacts
	}
}

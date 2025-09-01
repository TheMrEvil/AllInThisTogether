using System;
using UnityEngine;

// Token: 0x02000066 RID: 102
public class PathPoint
{
	// Token: 0x0600039C RID: 924 RVA: 0x0001DF2C File Offset: 0x0001C12C
	public void CalculateInner(System.Random rand, NavVisionPoint nextPt)
	{
		if (nextPt == this.Node)
		{
			this.innerTarget = this.Node.NavPosition;
			return;
		}
		Vector3 a = Vector3.Lerp(this.Node.NavPosition, nextPt.NavPosition, 0.5f);
		float num = Mathf.Min(Mathf.Sqrt(this.Node.ValidRadSqr), Mathf.Sqrt(nextPt.ValidRadSqr));
		float x = (float)rand.Next(-100, 100) / 100f;
		float y = (float)rand.Next(-100, 100) / 100f;
		float z = (float)rand.Next(-100, 100) / 100f;
		Vector3 vector = new Vector3(x, y, z);
		Vector3 p = AIManager.NearestNavPoint(a + vector.normalized * (num / 1.25f) * vector.magnitude, num);
		if (p.IsValid())
		{
			this.innerTarget = p;
			return;
		}
		this.innerTarget = a;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x0001E019 File Offset: 0x0001C219
	public PathPoint()
	{
	}

	// Token: 0x0400036A RID: 874
	public NavVisionPoint Node;

	// Token: 0x0400036B RID: 875
	public Vector3 innerTarget;
}

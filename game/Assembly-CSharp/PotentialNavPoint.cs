using System;
using UnityEngine;
using UnityEngine.AI;

// Token: 0x02000318 RID: 792
public class PotentialNavPoint
{
	// Token: 0x17000194 RID: 404
	// (get) Token: 0x06001B6E RID: 7022 RVA: 0x000A966D File Offset: 0x000A786D
	public bool IsRaw
	{
		get
		{
			return this.flynav == null && this.visionPoint == null;
		}
	}

	// Token: 0x06001B6F RID: 7023 RVA: 0x000A9684 File Offset: 0x000A7884
	public PotentialNavPoint(Vector3 pos, bool fitToNavMesh, bool overrideVisible = false)
	{
		NavMeshHit navMeshHit;
		if (fitToNavMesh && NavMesh.SamplePosition(pos, out navMeshHit, 100f, -1))
		{
			pos = navMeshHit.position;
		}
		this.pos = pos;
		this.visibilityOverride = overrideVisible;
	}

	// Token: 0x06001B70 RID: 7024 RVA: 0x000A96C1 File Offset: 0x000A78C1
	public PotentialNavPoint(NavVisionPoint npt)
	{
		this.pos = npt.NavPosition;
		this.visionPoint = npt;
	}

	// Token: 0x06001B71 RID: 7025 RVA: 0x000A96DC File Offset: 0x000A78DC
	public PotentialNavPoint(FlynavNode nav)
	{
		this.pos = nav.Position;
		this.flynav = nav;
		this.visionPoint = nav.VisionPoint;
	}

	// Token: 0x06001B72 RID: 7026 RVA: 0x000A9703 File Offset: 0x000A7903
	public bool IsEntityVisible(EntityControl e)
	{
		return this.visibilityOverride || (this.visionPoint != null && this.visionPoint.InView.ContainsKey(e));
	}

	// Token: 0x06001B73 RID: 7027 RVA: 0x000A972A File Offset: 0x000A792A
	public float ValidRadSqr()
	{
		if (this.visionPoint != null)
		{
			return this.visionPoint.ValidRadSqr;
		}
		return 1f;
	}

	// Token: 0x04001BE4 RID: 7140
	public Vector3 pos;

	// Token: 0x04001BE5 RID: 7141
	public FlynavNode flynav;

	// Token: 0x04001BE6 RID: 7142
	public NavVisionPoint visionPoint;

	// Token: 0x04001BE7 RID: 7143
	private bool visibilityOverride;
}

using System;
using UnityEngine;

// Token: 0x02000232 RID: 562
public class LookAtTarget : MonoBehaviour
{
	// Token: 0x06001740 RID: 5952 RVA: 0x00093166 File Offset: 0x00091366
	public void Activate()
	{
		this.isActive = true;
		this.lastRot = base.transform.rotation;
	}

	// Token: 0x06001741 RID: 5953 RVA: 0x00093180 File Offset: 0x00091380
	public void Deactivate()
	{
		this.isActive = false;
	}

	// Token: 0x06001742 RID: 5954 RVA: 0x0009318C File Offset: 0x0009138C
	private void LateUpdate()
	{
		if (this.Target == null || !this.isActive)
		{
			return;
		}
		Vector3 position = this.Target.position;
		if (this.Planar)
		{
			position.y = base.transform.position.y;
		}
		Quaternion quaternion = Quaternion.LookRotation((position - base.transform.position).normalized);
		if (this.Speed == 0f)
		{
			base.transform.rotation = quaternion;
			return;
		}
		this.lastRot = Quaternion.Lerp(this.lastRot, quaternion, this.Speed * GameplayManager.deltaTime);
		base.transform.rotation = this.lastRot;
	}

	// Token: 0x06001743 RID: 5955 RVA: 0x00093243 File Offset: 0x00091443
	public LookAtTarget()
	{
	}

	// Token: 0x04001706 RID: 5894
	public Transform Target;

	// Token: 0x04001707 RID: 5895
	public bool Planar;

	// Token: 0x04001708 RID: 5896
	public float Speed;

	// Token: 0x04001709 RID: 5897
	public bool isActive = true;

	// Token: 0x0400170A RID: 5898
	private Quaternion lastRot;
}

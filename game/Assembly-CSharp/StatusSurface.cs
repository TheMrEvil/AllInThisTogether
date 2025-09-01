using System;
using UnityEngine;

// Token: 0x02000035 RID: 53
public class StatusSurface : MonoBehaviour
{
	// Token: 0x06000191 RID: 401 RVA: 0x0000F690 File Offset: 0x0000D890
	public void TickUpdate(EntityControl control)
	{
		if (this.ReapplyRate <= 0f)
		{
			return;
		}
		this.applyTimer += Time.fixedDeltaTime;
		if (this.applyTimer < this.ReapplyRate)
		{
			return;
		}
		this.applyTimer = 0f;
		this.Apply(control);
	}

	// Token: 0x06000192 RID: 402 RVA: 0x0000F6DE File Offset: 0x0000D8DE
	public void LeftSurface()
	{
		this.applyTimer = this.ReapplyRate;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x0000F6EC File Offset: 0x0000D8EC
	public void Apply(EntityControl control)
	{
		control.net.ApplyStatus(this.Status.RootNode.guid.GetHashCode(), control.view.ViewID, this.Duration, 0, false, 0);
	}

	// Token: 0x06000194 RID: 404 RVA: 0x0000F722 File Offset: 0x0000D922
	public StatusSurface()
	{
	}

	// Token: 0x040001D9 RID: 473
	public StatusSurface.ClearType ClearOn;

	// Token: 0x040001DA RID: 474
	public float ReapplyRate = 0.25f;

	// Token: 0x040001DB RID: 475
	public float GracePeriod = 0.33f;

	// Token: 0x040001DC RID: 476
	public float Duration;

	// Token: 0x040001DD RID: 477
	public bool RemoveStatusOnExit = true;

	// Token: 0x040001DE RID: 478
	public StatusTree Status;

	// Token: 0x040001DF RID: 479
	private float applyTimer;

	// Token: 0x020003FF RID: 1023
	public enum ClearType
	{
		// Token: 0x04002130 RID: 8496
		Exit,
		// Token: 0x04002131 RID: 8497
		Land
	}
}

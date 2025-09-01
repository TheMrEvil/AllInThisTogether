using System;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x020000BC RID: 188
public class VignetteTrigger : DiageticOption
{
	// Token: 0x06000881 RID: 2177 RVA: 0x0003AA83 File Offset: 0x00038C83
	public virtual void Start()
	{
		this.Activate();
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0003AA8B File Offset: 0x00038C8B
	public virtual void Update()
	{
		if (this.IsAvailable && !this.CanActivate())
		{
			this.Deactivate();
		}
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0003AAA3 File Offset: 0x00038CA3
	public override void Select()
	{
		StateManager.VignetteAction(this.ActionID);
		if (!this.CanActivate())
		{
			this.Deactivate();
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0003AABE File Offset: 0x00038CBE
	public virtual bool CanActivate()
	{
		if (VignetteControl.instance == null)
		{
			return false;
		}
		VignetteControl instance = VignetteControl.instance;
		string actionID = this.ActionID;
		PlayerControl myInstance = PlayerControl.myInstance;
		return instance.CanActivate(actionID, (myInstance != null) ? myInstance.ViewID : -1);
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x0003AAF0 File Offset: 0x00038CF0
	private void OnDrawGizmos()
	{
		BetterGizmos.DrawSphere(new Color(0.5f, 1f, 0.5f, 0.2f), base.transform.position, this.InteractDistance);
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0003AB21 File Offset: 0x00038D21
	public VignetteTrigger()
	{
	}

	// Token: 0x0400072E RID: 1838
	public string Label;

	// Token: 0x0400072F RID: 1839
	public string ActionID;
}

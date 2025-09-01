using System;

// Token: 0x02000132 RID: 306
public class ActionIndicator : Indicatable
{
	// Token: 0x06000E30 RID: 3632 RVA: 0x0005A39E File Offset: 0x0005859E
	private void Start()
	{
		this.Action = base.GetComponentInParent<ActionEffect>();
		if (this.Action == null)
		{
			this.Action = base.GetComponent<ActionEffect>();
		}
		if (this.Action != null)
		{
			WorldIndicators.Indicate(this);
		}
	}

	// Token: 0x06000E31 RID: 3633 RVA: 0x0005A3DA File Offset: 0x000585DA
	public override bool ShouldIndicate()
	{
		return base.ShouldIndicate() && this.Action != null && !this.Action.isFinished;
	}

	// Token: 0x06000E32 RID: 3634 RVA: 0x0005A404 File Offset: 0x00058604
	private void OnDestroy()
	{
		WorldIndicators.ReleaseIndicator(this);
	}

	// Token: 0x06000E33 RID: 3635 RVA: 0x0005A40C File Offset: 0x0005860C
	public ActionIndicator()
	{
	}

	// Token: 0x04000B9F RID: 2975
	private ActionEffect Action;
}

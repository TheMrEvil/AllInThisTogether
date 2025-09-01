using System;

// Token: 0x020002C0 RID: 704
public class CameraFOVNode : EffectNode
{
	// Token: 0x06001A14 RID: 6676 RVA: 0x000A2881 File Offset: 0x000A0A81
	public override Node.InspectorProps GetInspectorProps()
	{
		return new Node.InspectorProps
		{
			Title = "Camera FOV"
		};
	}

	// Token: 0x06001A15 RID: 6677 RVA: 0x000A2894 File Offset: 0x000A0A94
	internal override void Apply(EffectProperties properties)
	{
		if (!this.ShouldApply(properties, null) || CameraFOV.instance == null)
		{
			return;
		}
		CameraFOV.FOVEffect foveffect = this.Effect.Clone();
		foveffect.UID = this.guid;
		CameraFOV.instance.ModifyFOV(foveffect);
	}

	// Token: 0x06001A16 RID: 6678 RVA: 0x000A28DC File Offset: 0x000A0ADC
	internal override void OnCancel(EffectProperties props)
	{
		if (this.CanCancel && this.ShouldApply(props, null))
		{
			CameraFOV.instance.CancelEffect(this.guid);
		}
	}

	// Token: 0x06001A17 RID: 6679 RVA: 0x000A2900 File Offset: 0x000A0B00
	internal override bool ShouldApply(EffectProperties props, EntityControl applyTo = null)
	{
		return !(PlayerControl.myInstance == null) && (!(props.SourceControl != PlayerControl.myInstance) || !(props.AffectedControl != PlayerControl.myInstance));
	}

	// Token: 0x06001A18 RID: 6680 RVA: 0x000A2938 File Offset: 0x000A0B38
	public CameraFOVNode()
	{
	}

	// Token: 0x04001A8F RID: 6799
	public CameraFOV.FOVEffect Effect;

	// Token: 0x04001A90 RID: 6800
	public bool CanCancel = true;
}

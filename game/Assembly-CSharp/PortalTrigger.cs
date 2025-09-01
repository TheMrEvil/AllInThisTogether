using System;
using UnityEngine;

// Token: 0x020000BA RID: 186
public class PortalTrigger : DiageticOption
{
	// Token: 0x06000876 RID: 2166 RVA: 0x0003A889 File Offset: 0x00038A89
	protected override void Awake()
	{
		base.Awake();
		if (this.portalType == PortalType.Raid)
		{
			this.indicator.IconOverride = ((RaidManager.instance.Difficulty == RaidDB.Difficulty.Hard) ? this.HardIcon : this.DefaultIcon);
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0003A8C0 File Offset: 0x00038AC0
	private void Start()
	{
		this.Activate();
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0003A8C8 File Offset: 0x00038AC8
	public override void Activate()
	{
		base.Activate();
		if (this.SystemBase != null)
		{
			this.SystemBase.Play();
		}
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0003A8E9 File Offset: 0x00038AE9
	public override void Deactivate()
	{
		if (!this.IsAvailable)
		{
			return;
		}
		base.Deactivate();
		if (this.SystemBase != null)
		{
			this.SystemBase.Stop();
		}
		EntityIndicator entityIndicator = this.indicator;
		if (entityIndicator == null)
		{
			return;
		}
		entityIndicator.Deactivate();
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0003A923 File Offset: 0x00038B23
	public override void Select()
	{
		if (RaidManager.IsInRaid)
		{
			RaidManager.instance.PortalInteraction(this.portalType);
			return;
		}
		GameplayManager.instance.PostGame_PortalInteraction(this.portalType);
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0003A94D File Offset: 0x00038B4D
	public PortalTrigger()
	{
	}

	// Token: 0x04000720 RID: 1824
	public PortalType portalType;

	// Token: 0x04000721 RID: 1825
	public Sprite DefaultIcon;

	// Token: 0x04000722 RID: 1826
	public Sprite HardIcon;

	// Token: 0x04000723 RID: 1827
	public EntityIndicator indicator;

	// Token: 0x04000724 RID: 1828
	[Header("Display FX")]
	public ParticleSystem SystemBase;
}

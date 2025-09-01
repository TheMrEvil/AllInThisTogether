using System;

// Token: 0x020000B1 RID: 177
public class FountainInteraction : PositionalInteraction
{
	// Token: 0x170000BA RID: 186
	// (get) Token: 0x060007FC RID: 2044 RVA: 0x00038799 File Offset: 0x00036999
	public static FountainInteraction instance
	{
		get
		{
			Fountain instance = Fountain.instance;
			if (instance == null)
			{
				return null;
			}
			return instance.Interaction;
		}
	}

	// Token: 0x170000BB RID: 187
	// (get) Token: 0x060007FD RID: 2045 RVA: 0x000387AB File Offset: 0x000369AB
	public bool IsPlayerInteracting
	{
		get
		{
			return this.IsInteractable && base.IsInteracting;
		}
	}

	// Token: 0x060007FE RID: 2046 RVA: 0x000387C0 File Offset: 0x000369C0
	internal override void Update()
	{
		bool canInteract = FountainWorldUI.CanInteract;
		FountainWorldUI.CanInteract = this.IsInteractable;
		if (this.PlayerInside && !canInteract && this.IsInteractable)
		{
			this.UpdateInteractionLabel();
		}
		base.Update();
	}

	// Token: 0x060007FF RID: 2047 RVA: 0x000387FD File Offset: 0x000369FD
	internal override void OnEnter()
	{
		this.UpdateInteractionLabel();
		base.OnEnter();
	}

	// Token: 0x06000800 RID: 2048 RVA: 0x0003880B File Offset: 0x00036A0B
	private void UpdateInteractionLabel()
	{
		this.InteractTime = ((GameplayManager.instance.CurrentState == GameState.InWave) ? 0.6f : 0f);
		FountainWorldUI.SetReason("Fountain");
	}

	// Token: 0x06000801 RID: 2049 RVA: 0x00038836 File Offset: 0x00036A36
	internal override void OnInteract()
	{
		if (!this.IsInteractable)
		{
			return;
		}
		AugmentsPanel.TryOpen();
		base.OnInteract();
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x0003884C File Offset: 0x00036A4C
	internal override void OnExit()
	{
		base.OnExit();
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x00038854 File Offset: 0x00036A54
	public FountainInteraction()
	{
	}

	// Token: 0x040006C0 RID: 1728
	public bool IsInteractable;
}

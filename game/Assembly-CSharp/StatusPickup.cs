using System;
using UnityEngine;

// Token: 0x020000D6 RID: 214
public class StatusPickup : DiageticOption
{
	// Token: 0x060009C1 RID: 2497 RVA: 0x00040EA2 File Offset: 0x0003F0A2
	private void Start()
	{
		if (this.AutoActivate)
		{
			this.Activate();
		}
	}

	// Token: 0x060009C2 RID: 2498 RVA: 0x00040EB2 File Offset: 0x0003F0B2
	public void Setup(StatusTree status, int stacks = 1, float duration = 0f)
	{
		this.Status = status;
		this.Stacks = stacks;
		if (status == null)
		{
			this.Deactivate();
			return;
		}
		this.Activate();
	}

	// Token: 0x060009C3 RID: 2499 RVA: 0x00040ED8 File Offset: 0x0003F0D8
	public override void Activate()
	{
		base.Activate();
		this.vfxDisplay.Play();
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChange));
	}

	// Token: 0x060009C4 RID: 2500 RVA: 0x00040F0B File Offset: 0x0003F10B
	public override void Deactivate()
	{
		base.Deactivate();
		this.vfxDisplay.Stop();
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChange));
	}

	// Token: 0x060009C5 RID: 2501 RVA: 0x00040F40 File Offset: 0x0003F140
	public override void Select()
	{
		if (!this.CanCollect)
		{
			return;
		}
		AudioManager.PlayInterfaceSFX(this.ChosenSFX, 1f, 0f);
		PlayerControl.myInstance.net.ApplyStatus(this.Status.HashCode, -1, 0f, this.Stacks, false, 0);
		PlayerControl.myInstance.AugmentAddedFX.Play();
		this.Deactivate();
		UnityEngine.Object.Destroy(base.gameObject, 3f);
		Action onCollect = this.OnCollect;
		if (onCollect == null)
		{
			return;
		}
		onCollect();
	}

	// Token: 0x060009C6 RID: 2502 RVA: 0x00040FC8 File Offset: 0x0003F1C8
	private void OnGameStateChange(GameState from, GameState to)
	{
	}

	// Token: 0x060009C7 RID: 2503 RVA: 0x00040FCA File Offset: 0x0003F1CA
	public StatusPickup()
	{
	}

	// Token: 0x0400081C RID: 2076
	public bool AutoActivate;

	// Token: 0x0400081D RID: 2077
	public AudioClip ChosenSFX;

	// Token: 0x0400081E RID: 2078
	public StatusTree Status;

	// Token: 0x0400081F RID: 2079
	public int Stacks = 1;

	// Token: 0x04000820 RID: 2080
	public bool CanCollect = true;

	// Token: 0x04000821 RID: 2081
	public ParticleSystem vfxDisplay;

	// Token: 0x04000822 RID: 2082
	public Action OnCollect;
}

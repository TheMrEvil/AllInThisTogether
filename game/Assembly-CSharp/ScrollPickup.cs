using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

// Token: 0x020000CA RID: 202
public class ScrollPickup : DiageticOption
{
	// Token: 0x06000968 RID: 2408 RVA: 0x0003F344 File Offset: 0x0003D544
	public void Setup(AugmentTree augment)
	{
		this.Augment = augment;
		if (augment == null)
		{
			this.Deactivate();
		}
		else
		{
			this.scrollDisplay.Setup(this.Augment, true);
			this.Activate();
		}
		base.StartCoroutine(this.CheckAutoPickup());
	}

	// Token: 0x06000969 RID: 2409 RVA: 0x0003F383 File Offset: 0x0003D583
	private IEnumerator CheckAutoPickup()
	{
		yield return true;
		if (GameplayManager.CurState == GameState.Reward_Start)
		{
			this.Select();
		}
		yield break;
	}

	// Token: 0x0600096A RID: 2410 RVA: 0x0003F392 File Offset: 0x0003D592
	public override void Activate()
	{
		base.Activate();
		this.scrollDisplay.Activate();
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Combine(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChange));
	}

	// Token: 0x0600096B RID: 2411 RVA: 0x0003F3C5 File Offset: 0x0003D5C5
	public override void Deactivate()
	{
		base.Deactivate();
		this.scrollDisplay.Deactivate();
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChange));
	}

	// Token: 0x0600096C RID: 2412 RVA: 0x0003F3F8 File Offset: 0x0003D5F8
	public override void Select()
	{
		if (!this.CanCollect)
		{
			return;
		}
		AudioManager.PlayInterfaceSFX(this.ChosenSFX, 1f, 0f);
		Progression.SawAugment(this.Augment.ID);
		PlayerControl.myInstance.AddAugmentExternal(this.Augment);
		PlayerControl.myInstance.Display.BookFollow.BookParticleVortex(this.scrollDisplay.transform.position);
		GameHUD.instance.GotRewardAugment(this.Augment);
		this.Deactivate();
		UnityEngine.Object.Destroy(base.gameObject, 3f);
		Action onCollect = this.OnCollect;
		if (onCollect == null)
		{
			return;
		}
		onCollect();
	}

	// Token: 0x0600096D RID: 2413 RVA: 0x0003F4A0 File Offset: 0x0003D6A0
	private void OnGameStateChange(GameState from, GameState to)
	{
		if (from == GameState.InWave)
		{
			try
			{
				this.Select();
			}
			catch
			{
			}
		}
	}

	// Token: 0x0600096E RID: 2414 RVA: 0x0003F4CC File Offset: 0x0003D6CC
	private void OnDestroy()
	{
		GameplayManager.OnGameStateChanged = (Action<GameState, GameState>)Delegate.Remove(GameplayManager.OnGameStateChanged, new Action<GameState, GameState>(this.OnGameStateChange));
	}

	// Token: 0x0600096F RID: 2415 RVA: 0x0003F4EE File Offset: 0x0003D6EE
	public ScrollPickup()
	{
	}

	// Token: 0x040007C4 RID: 1988
	public AudioClip ChosenSFX;

	// Token: 0x040007C5 RID: 1989
	public ScrollTrigger scrollDisplay;

	// Token: 0x040007C6 RID: 1990
	public AugmentTree Augment;

	// Token: 0x040007C7 RID: 1991
	public bool CanCollect = true;

	// Token: 0x040007C8 RID: 1992
	public Action OnCollect;

	// Token: 0x020004C3 RID: 1219
	[CompilerGenerated]
	private sealed class <CheckAutoPickup>d__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022AC RID: 8876 RVA: 0x000C7718 File Offset: 0x000C5918
		[DebuggerHidden]
		public <CheckAutoPickup>d__6(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000C7727 File Offset: 0x000C5927
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000C772C File Offset: 0x000C592C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ScrollPickup scrollPickup = this;
			if (num == 0)
			{
				this.<>1__state = -1;
				this.<>2__current = true;
				this.<>1__state = 1;
				return true;
			}
			if (num != 1)
			{
				return false;
			}
			this.<>1__state = -1;
			if (GameplayManager.CurState == GameState.Reward_Start)
			{
				scrollPickup.Select();
			}
			return false;
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060022AF RID: 8879 RVA: 0x000C7782 File Offset: 0x000C5982
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000C778A File Offset: 0x000C598A
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x060022B1 RID: 8881 RVA: 0x000C7791 File Offset: 0x000C5991
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002446 RID: 9286
		private int <>1__state;

		// Token: 0x04002447 RID: 9287
		private object <>2__current;

		// Token: 0x04002448 RID: 9288
		public ScrollPickup <>4__this;
	}
}

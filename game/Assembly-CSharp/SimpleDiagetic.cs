using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MiniTools.BetterGizmos;
using UnityEngine;
using UnityEngine.Events;

// Token: 0x020000E4 RID: 228
public class SimpleDiagetic : DiageticOption
{
	// Token: 0x06000A22 RID: 2594 RVA: 0x000425F1 File Offset: 0x000407F1
	private void Start()
	{
		if (this.StartActive)
		{
			this.Activate();
		}
	}

	// Token: 0x06000A23 RID: 2595 RVA: 0x00042601 File Offset: 0x00040801
	internal override void OnEnable()
	{
		base.OnEnable();
		if (this.StartActive)
		{
			base.StartCoroutine(this.AcitvateDelayed());
		}
	}

	// Token: 0x06000A24 RID: 2596 RVA: 0x0004261E File Offset: 0x0004081E
	private IEnumerator AcitvateDelayed()
	{
		yield return true;
		this.Activate();
		yield break;
	}

	// Token: 0x06000A25 RID: 2597 RVA: 0x0004262D File Offset: 0x0004082D
	public override void Select()
	{
		base.Select();
		UnityEvent onActivate = this.OnActivate;
		if (onActivate != null)
		{
			onActivate.Invoke();
		}
		this.Deactivate();
		if (this.Repeat == SimpleDiagetic.RepeatTime.Cooldown)
		{
			base.Invoke("Activate", this.Cooldown);
		}
	}

	// Token: 0x06000A26 RID: 2598 RVA: 0x00042666 File Offset: 0x00040866
	public void SetOnCooldown()
	{
		this.Deactivate();
		base.Invoke("Activate", this.Cooldown);
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0004267F File Offset: 0x0004087F
	internal virtual void OnDrawGizmos()
	{
		BetterGizmos.DrawSphere(new Color(0.8f, 0.7f, 0.5f, 0.2f), base.transform.position, this.InteractDistance);
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x000426B0 File Offset: 0x000408B0
	public SimpleDiagetic()
	{
	}

	// Token: 0x040008A3 RID: 2211
	public string Label;

	// Token: 0x040008A4 RID: 2212
	public bool StartActive = true;

	// Token: 0x040008A5 RID: 2213
	public SimpleDiagetic.RepeatTime Repeat;

	// Token: 0x040008A6 RID: 2214
	public float Cooldown = 2f;

	// Token: 0x040008A7 RID: 2215
	private float icd;

	// Token: 0x040008A8 RID: 2216
	public UnityEvent OnActivate;

	// Token: 0x020004D6 RID: 1238
	public enum RepeatTime
	{
		// Token: 0x04002481 RID: 9345
		Once,
		// Token: 0x04002482 RID: 9346
		Cooldown
	}

	// Token: 0x020004D7 RID: 1239
	[CompilerGenerated]
	private sealed class <AcitvateDelayed>d__9 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002304 RID: 8964 RVA: 0x000C844B File Offset: 0x000C664B
		[DebuggerHidden]
		public <AcitvateDelayed>d__9(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x000C845A File Offset: 0x000C665A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x000C845C File Offset: 0x000C665C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			SimpleDiagetic simpleDiagetic = this;
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
			simpleDiagetic.Activate();
			return false;
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06002307 RID: 8967 RVA: 0x000C84AA File Offset: 0x000C66AA
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x000C84B2 File Offset: 0x000C66B2
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06002309 RID: 8969 RVA: 0x000C84B9 File Offset: 0x000C66B9
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002483 RID: 9347
		private int <>1__state;

		// Token: 0x04002484 RID: 9348
		private object <>2__current;

		// Token: 0x04002485 RID: 9349
		public SimpleDiagetic <>4__this;
	}
}

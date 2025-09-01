using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MagicaCloth2;
using UnityEngine;

// Token: 0x020000AE RID: 174
public class FishingRod : MonoBehaviour
{
	// Token: 0x060007E1 RID: 2017 RVA: 0x000382F0 File Offset: 0x000364F0
	public void Setup(PlayerControl player, Vector3 endPt)
	{
		if (player == null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		this.castReady = false;
		this.isFinished = false;
		this.playerRef = player;
		this.isFinished = false;
		this.initialized = true;
		Transform followRef;
		if (player == null)
		{
			followRef = null;
		}
		else
		{
			PlayerDisplay display = player.Display;
			followRef = ((display != null) ? display.FishingAnchor : null);
		}
		this.FollowRef = followRef;
		this.LineEnd = endPt;
		this.curEnd = this.LineStart.position;
		base.StartCoroutine("CastSequence");
	}

	// Token: 0x060007E2 RID: 2018 RVA: 0x00038377 File Offset: 0x00036577
	private IEnumerator CastSequence()
	{
		foreach (ActionMaterial actionMaterial in this.Meshes)
		{
			actionMaterial.Enter();
		}
		yield return new WaitForSeconds(0.1f);
		PlayerDisplay display = this.playerRef.Display;
		if (display != null)
		{
			display.PlayAbilityAnim("Fishing_Start", 0.25f, PlayerAbilityType.Primary, false);
		}
		yield return new WaitForSeconds(0.6f);
		this.castReady = true;
		yield break;
	}

	// Token: 0x060007E3 RID: 2019 RVA: 0x00038388 File Offset: 0x00036588
	private void LateUpdate()
	{
		if (!this.initialized)
		{
			return;
		}
		if (this.playerRef == null || this.FollowRef == null)
		{
			UnityEngine.Object.Destroy(base.gameObject);
			return;
		}
		base.transform.SetPositionAndRotation(this.FollowRef.position, this.FollowRef.rotation);
		foreach (ActionMaterial actionMaterial in this.Meshes)
		{
			actionMaterial.TickUpdate();
		}
		this.UpdateLine();
	}

	// Token: 0x060007E4 RID: 2020 RVA: 0x00038430 File Offset: 0x00036630
	private void UpdateLine()
	{
		this.Line.SetPosition(0, this.LineStart.position);
		if (this.isFinished)
		{
			this.curEnd = Vector3.Lerp(this.curEnd, this.LineStart.position, Time.deltaTime * 14f);
		}
		else if (this.castReady)
		{
			this.curEnd = Vector3.Lerp(this.curEnd, this.LineEnd, Time.deltaTime * 6f);
		}
		else
		{
			this.curEnd = this.LineStart.position;
		}
		this.Line.SetPosition(1, this.curEnd);
	}

	// Token: 0x060007E5 RID: 2021 RVA: 0x000384D4 File Offset: 0x000366D4
	public void DebugSetup()
	{
		this.Setup(PlayerControl.myInstance, Vector3.zero);
	}

	// Token: 0x060007E6 RID: 2022 RVA: 0x000384E8 File Offset: 0x000366E8
	public void Finish()
	{
		if (this.playerRef == null)
		{
			return;
		}
		this.isFinished = false;
		PlayerDisplay display = this.playerRef.Display;
		if (display != null)
		{
			display.PlayAbilityAnim("Fishing_End", 0.2f, PlayerAbilityType.Primary, false);
		}
		base.StartCoroutine("FinishSequence");
		UnityEngine.Object.Destroy(base.gameObject, 2.5f);
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00038549 File Offset: 0x00036749
	private IEnumerator FinishSequence()
	{
		yield return new WaitForSeconds(0.33f);
		this.isFinished = true;
		yield return new WaitForSeconds(0.66f);
		foreach (ActionMaterial actionMaterial in this.Meshes)
		{
			actionMaterial.Exit();
		}
		this.castReady = false;
		this.isFinished = false;
		yield break;
	}

	// Token: 0x060007E8 RID: 2024 RVA: 0x00038558 File Offset: 0x00036758
	public FishingRod()
	{
	}

	// Token: 0x040006A4 RID: 1700
	private PlayerControl playerRef;

	// Token: 0x040006A5 RID: 1701
	public Transform LineStart;

	// Token: 0x040006A6 RID: 1702
	public LineRenderer Line;

	// Token: 0x040006A7 RID: 1703
	public List<ActionMaterial> Meshes;

	// Token: 0x040006A8 RID: 1704
	private Transform FollowRef;

	// Token: 0x040006A9 RID: 1705
	public MagicaCloth clothRef;

	// Token: 0x040006AA RID: 1706
	private Vector3 curEnd;

	// Token: 0x040006AB RID: 1707
	private Vector3 LineEnd;

	// Token: 0x040006AC RID: 1708
	private bool castReady;

	// Token: 0x040006AD RID: 1709
	private bool isFinished;

	// Token: 0x040006AE RID: 1710
	private bool initialized;

	// Token: 0x020004B2 RID: 1202
	[CompilerGenerated]
	private sealed class <CastSequence>d__12 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002267 RID: 8807 RVA: 0x000C6BE7 File Offset: 0x000C4DE7
		[DebuggerHidden]
		public <CastSequence>d__12(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002268 RID: 8808 RVA: 0x000C6BF6 File Offset: 0x000C4DF6
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002269 RID: 8809 RVA: 0x000C6BF8 File Offset: 0x000C4DF8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			FishingRod fishingRod = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				foreach (ActionMaterial actionMaterial in fishingRod.Meshes)
				{
					actionMaterial.Enter();
				}
				this.<>2__current = new WaitForSeconds(0.1f);
				this.<>1__state = 1;
				return true;
			case 1:
			{
				this.<>1__state = -1;
				PlayerDisplay display = fishingRod.playerRef.Display;
				if (display != null)
				{
					display.PlayAbilityAnim("Fishing_Start", 0.25f, PlayerAbilityType.Primary, false);
				}
				this.<>2__current = new WaitForSeconds(0.6f);
				this.<>1__state = 2;
				return true;
			}
			case 2:
				this.<>1__state = -1;
				fishingRod.castReady = true;
				return false;
			default:
				return false;
			}
		}

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x0600226A RID: 8810 RVA: 0x000C6CDC File Offset: 0x000C4EDC
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600226B RID: 8811 RVA: 0x000C6CE4 File Offset: 0x000C4EE4
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x0600226C RID: 8812 RVA: 0x000C6CEB File Offset: 0x000C4EEB
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400240F RID: 9231
		private int <>1__state;

		// Token: 0x04002410 RID: 9232
		private object <>2__current;

		// Token: 0x04002411 RID: 9233
		public FishingRod <>4__this;
	}

	// Token: 0x020004B3 RID: 1203
	[CompilerGenerated]
	private sealed class <FinishSequence>d__17 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600226D RID: 8813 RVA: 0x000C6CF3 File Offset: 0x000C4EF3
		[DebuggerHidden]
		public <FinishSequence>d__17(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x000C6D02 File Offset: 0x000C4F02
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x000C6D04 File Offset: 0x000C4F04
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			FishingRod fishingRod = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(0.33f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				fishingRod.isFinished = true;
				this.<>2__current = new WaitForSeconds(0.66f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				foreach (ActionMaterial actionMaterial in fishingRod.Meshes)
				{
					actionMaterial.Exit();
				}
				fishingRod.castReady = false;
				fishingRod.isFinished = false;
				return false;
			default:
				return false;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x000C6DD4 File Offset: 0x000C4FD4
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002271 RID: 8817 RVA: 0x000C6DDC File Offset: 0x000C4FDC
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x000C6DE3 File Offset: 0x000C4FE3
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002412 RID: 9234
		private int <>1__state;

		// Token: 0x04002413 RID: 9235
		private object <>2__current;

		// Token: 0x04002414 RID: 9236
		public FishingRod <>4__this;
	}
}

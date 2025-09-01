using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x020000CE RID: 206
public class ScrollSpawner : MonoBehaviour
{
	// Token: 0x06000981 RID: 2433 RVA: 0x0003F9DC File Offset: 0x0003DBDC
	private IEnumerator Start()
	{
		while (GoalManager.instance == null)
		{
			yield return true;
		}
		yield return true;
		if (this.SpawnOnStart)
		{
			if (this.SpawnDelay > 0f)
			{
				yield return new WaitForSeconds(this.SpawnDelay);
			}
			this.CreateScroll();
		}
		yield break;
	}

	// Token: 0x06000982 RID: 2434 RVA: 0x0003F9EC File Offset: 0x0003DBEC
	public void CreateScroll()
	{
		if (this.createdScroll)
		{
			return;
		}
		this.createdScroll = true;
		if (this.PickThree)
		{
			GoalManager.instance.CreatePageSelect(this.Filter, base.transform.position);
			return;
		}
		if (this.Augment != null)
		{
			GoalManager.instance.CreateScroll(this.Augment, base.transform.position);
			return;
		}
		GoalManager.instance.GiveAugmentScroll(this.Filter, base.transform.position);
	}

	// Token: 0x06000983 RID: 2435 RVA: 0x0003FA78 File Offset: 0x0003DC78
	private void OnDrawGizmos()
	{
		Vector3 position = base.transform.position;
		Color color = new Color(1f, 1f, 0.7f);
		BetterGizmos.DrawSphere(color, position, 0.33f);
		BetterGizmos.DrawSphere(color, position + Vector3.up, 0.15f);
		BetterGizmos.DrawSphere(color, position + Vector3.up * 2f, 0.4f);
	}

	// Token: 0x06000984 RID: 2436 RVA: 0x0003FAE6 File Offset: 0x0003DCE6
	public ScrollSpawner()
	{
	}

	// Token: 0x040007E1 RID: 2017
	public bool SpawnOnStart;

	// Token: 0x040007E2 RID: 2018
	public float SpawnDelay;

	// Token: 0x040007E3 RID: 2019
	private bool createdScroll;

	// Token: 0x040007E4 RID: 2020
	public bool PickThree;

	// Token: 0x040007E5 RID: 2021
	public AugmentTree Augment;

	// Token: 0x040007E6 RID: 2022
	public AugmentFilter Filter;

	// Token: 0x020004C8 RID: 1224
	[CompilerGenerated]
	private sealed class <Start>d__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022C4 RID: 8900 RVA: 0x000C7994 File Offset: 0x000C5B94
		[DebuggerHidden]
		public <Start>d__6(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000C79A3 File Offset: 0x000C5BA3
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000C79A8 File Offset: 0x000C5BA8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			ScrollSpawner scrollSpawner = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				if (!scrollSpawner.SpawnOnStart)
				{
					return false;
				}
				if (scrollSpawner.SpawnDelay > 0f)
				{
					this.<>2__current = new WaitForSeconds(scrollSpawner.SpawnDelay);
					this.<>1__state = 3;
					return true;
				}
				goto IL_AA;
			case 3:
				this.<>1__state = -1;
				goto IL_AA;
			default:
				return false;
			}
			if (!(GoalManager.instance == null))
			{
				this.<>2__current = true;
				this.<>1__state = 2;
				return true;
			}
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
			IL_AA:
			scrollSpawner.CreateScroll();
			return false;
		}

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x060022C7 RID: 8903 RVA: 0x000C7A66 File Offset: 0x000C5C66
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000C7A6E File Offset: 0x000C5C6E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x060022C9 RID: 8905 RVA: 0x000C7A75 File Offset: 0x000C5C75
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002452 RID: 9298
		private int <>1__state;

		// Token: 0x04002453 RID: 9299
		private object <>2__current;

		// Token: 0x04002454 RID: 9300
		public ScrollSpawner <>4__this;
	}
}

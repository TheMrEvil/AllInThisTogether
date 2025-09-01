using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using MiniTools.BetterGizmos;
using UnityEngine;

// Token: 0x020000CD RID: 205
public class NookRewardSpawn : MonoBehaviour
{
	// Token: 0x0600097C RID: 2428 RVA: 0x0003F863 File Offset: 0x0003DA63
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
			this.CreatePickup();
		}
		yield break;
	}

	// Token: 0x0600097D RID: 2429 RVA: 0x0003F874 File Offset: 0x0003DA74
	public void CreatePickup()
	{
		Vector3 position = base.transform.position;
		NookDB.NookObject item = NookDB.GetItem(this.NookItemID);
		if (item == null)
		{
			return;
		}
		if (this.SkipIfUnlocked && UnlockManager.IsNookItemUnlocked(item))
		{
			return;
		}
		BossRewardTrigger bossRewardTrigger = GoalManager.instance.CreateBossReward(position, Progression.BossRewardType.NookItem, item);
		BossRewardTrigger bossRewardTrigger2 = bossRewardTrigger;
		bossRewardTrigger2.OnInteract = (Action)Delegate.Combine(bossRewardTrigger2.OnInteract, new Action(delegate()
		{
			UnlockManager.UnlockNookItem(item);
		}));
		if (this.HideIndicator)
		{
			bossRewardTrigger.indicator.gameObject.SetActive(false);
		}
		if (this.HideAudio)
		{
			AudioSource componentInChildren = bossRewardTrigger.gameObject.GetComponentInChildren<AudioSource>();
			if (componentInChildren == null)
			{
				UnityEngine.Debug.Log("NO AUDIO SOURCE FOUND");
				return;
			}
			base.StartCoroutine(this.HideSoundDelayed(componentInChildren));
		}
	}

	// Token: 0x0600097E RID: 2430 RVA: 0x0003F947 File Offset: 0x0003DB47
	private IEnumerator HideSoundDelayed(AudioSource src)
	{
		yield return true;
		src.maxDistance = 20f;
		src.minDistance = 0.5f;
		src.spatialBlend = 1f;
		yield break;
	}

	// Token: 0x0600097F RID: 2431 RVA: 0x0003F958 File Offset: 0x0003DB58
	private void OnDrawGizmos()
	{
		Vector3 position = base.transform.position;
		Color color = new Color(0.3f, 0.6f, 0.9f);
		BetterGizmos.DrawSphere(color, position, 0.33f);
		BetterGizmos.DrawSphere(color, position + Vector3.up, 0.15f);
		BetterGizmos.DrawSphere(color, position + Vector3.up * 2f, 0.4f);
	}

	// Token: 0x06000980 RID: 2432 RVA: 0x0003F9C6 File Offset: 0x0003DBC6
	public NookRewardSpawn()
	{
	}

	// Token: 0x040007DB RID: 2011
	public string NookItemID;

	// Token: 0x040007DC RID: 2012
	public bool SkipIfUnlocked = true;

	// Token: 0x040007DD RID: 2013
	public bool SpawnOnStart = true;

	// Token: 0x040007DE RID: 2014
	public bool HideIndicator;

	// Token: 0x040007DF RID: 2015
	public bool HideAudio;

	// Token: 0x040007E0 RID: 2016
	public float SpawnDelay;

	// Token: 0x020004C5 RID: 1221
	[CompilerGenerated]
	private sealed class <Start>d__6 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022B6 RID: 8886 RVA: 0x000C77FE File Offset: 0x000C59FE
		[DebuggerHidden]
		public <Start>d__6(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022B7 RID: 8887 RVA: 0x000C780D File Offset: 0x000C5A0D
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022B8 RID: 8888 RVA: 0x000C7810 File Offset: 0x000C5A10
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			NookRewardSpawn nookRewardSpawn = this;
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
				if (!nookRewardSpawn.SpawnOnStart)
				{
					return false;
				}
				if (nookRewardSpawn.SpawnDelay > 0f)
				{
					this.<>2__current = new WaitForSeconds(nookRewardSpawn.SpawnDelay);
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
			nookRewardSpawn.CreatePickup();
			return false;
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x000C78CE File Offset: 0x000C5ACE
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022BA RID: 8890 RVA: 0x000C78D6 File Offset: 0x000C5AD6
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x060022BB RID: 8891 RVA: 0x000C78DD File Offset: 0x000C5ADD
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400244B RID: 9291
		private int <>1__state;

		// Token: 0x0400244C RID: 9292
		private object <>2__current;

		// Token: 0x0400244D RID: 9293
		public NookRewardSpawn <>4__this;
	}

	// Token: 0x020004C6 RID: 1222
	[CompilerGenerated]
	private sealed class <>c__DisplayClass7_0
	{
		// Token: 0x060022BC RID: 8892 RVA: 0x000C78E5 File Offset: 0x000C5AE5
		public <>c__DisplayClass7_0()
		{
		}

		// Token: 0x060022BD RID: 8893 RVA: 0x000C78ED File Offset: 0x000C5AED
		internal void <CreatePickup>b__0()
		{
			UnlockManager.UnlockNookItem(this.item);
		}

		// Token: 0x0400244E RID: 9294
		public NookDB.NookObject item;
	}

	// Token: 0x020004C7 RID: 1223
	[CompilerGenerated]
	private sealed class <HideSoundDelayed>d__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060022BE RID: 8894 RVA: 0x000C78FA File Offset: 0x000C5AFA
		[DebuggerHidden]
		public <HideSoundDelayed>d__8(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060022BF RID: 8895 RVA: 0x000C7909 File Offset: 0x000C5B09
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060022C0 RID: 8896 RVA: 0x000C790C File Offset: 0x000C5B0C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
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
			src.maxDistance = 20f;
			src.minDistance = 0.5f;
			src.spatialBlend = 1f;
			return false;
		}

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x060022C1 RID: 8897 RVA: 0x000C797D File Offset: 0x000C5B7D
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000C7985 File Offset: 0x000C5B85
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x060022C3 RID: 8899 RVA: 0x000C798C File Offset: 0x000C5B8C
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400244F RID: 9295
		private int <>1__state;

		// Token: 0x04002450 RID: 9296
		private object <>2__current;

		// Token: 0x04002451 RID: 9297
		public AudioSource src;
	}
}

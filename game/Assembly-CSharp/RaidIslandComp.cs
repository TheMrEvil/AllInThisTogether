using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.AI.Navigation;
using UnityEngine;

// Token: 0x0200025C RID: 604
public class RaidIslandComp : MonoBehaviour
{
	// Token: 0x0600184B RID: 6219 RVA: 0x000980DC File Offset: 0x000962DC
	private void Awake()
	{
		this.IslandColliders = new List<Collider>();
		this.IslandMats = new List<ActionMaterial>();
		this.surface = base.GetComponentInChildren<NavMeshSurface>();
		foreach (GameObject gameObject in this.IslandObjects)
		{
			Collider component = gameObject.GetComponent<Collider>();
			if (component != null)
			{
				this.IslandColliders.Add(component);
			}
			ActionMaterial component2 = gameObject.GetComponent<ActionMaterial>();
			if (component2 != null)
			{
				this.IslandMats.Add(component2);
			}
		}
	}

	// Token: 0x0600184C RID: 6220 RVA: 0x00098180 File Offset: 0x00096380
	public void ResetAll()
	{
		RaidIslandComp[] array = UnityEngine.Object.FindObjectsOfType<RaidIslandComp>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Reset();
		}
	}

	// Token: 0x0600184D RID: 6221 RVA: 0x000981AC File Offset: 0x000963AC
	private void Reset()
	{
		base.StopAllCoroutines();
		this.GlowBlack.InstantOff();
		this.GlowWhite.InstantOff();
		this.AppearWarning.Stop();
		this.DisableWarning.Stop();
		foreach (ActionMaterial actionMaterial in this.IslandMats)
		{
			actionMaterial.InstantOn();
		}
		foreach (GameObject gameObject in this.SpecialObjects)
		{
			gameObject.SetActive(true);
		}
		this.surface.enabled = true;
		foreach (Collider collider in this.IslandColliders)
		{
			collider.enabled = true;
		}
	}

	// Token: 0x0600184E RID: 6222 RVA: 0x000982BC File Offset: 0x000964BC
	public void Activate()
	{
		base.StopAllCoroutines();
		this.GlowBlack.InstantOff();
		this.AppearWarning.Stop();
		this.DisableWarning.Stop();
		base.StartCoroutine("ActivateSequence");
	}

	// Token: 0x0600184F RID: 6223 RVA: 0x000982F1 File Offset: 0x000964F1
	private IEnumerator ActivateSequence()
	{
		this.AppearWarning.Play();
		this.GlowWhite.Enter();
		yield return new WaitForSeconds(1f);
		yield return new WaitForSeconds(1f);
		foreach (ActionMaterial actionMaterial in this.IslandMats)
		{
			actionMaterial.Enter();
		}
		yield return new WaitForSeconds(0.5f);
		this.GlowWhite.Exit();
		this.surface.enabled = true;
		foreach (Collider collider in this.IslandColliders)
		{
			collider.enabled = true;
		}
		foreach (GameObject gameObject in this.SpecialObjects)
		{
			gameObject.SetActive(true);
		}
		yield return new WaitForSeconds(0.5f);
		this.AppearWarning.Stop();
		yield break;
	}

	// Token: 0x06001850 RID: 6224 RVA: 0x00098300 File Offset: 0x00096500
	public void Deactivate()
	{
		base.StopAllCoroutines();
		this.GlowWhite.InstantOff();
		this.AppearWarning.Stop();
		this.DisableWarning.Stop();
		base.StartCoroutine("DeactivateSequence");
	}

	// Token: 0x06001851 RID: 6225 RVA: 0x00098335 File Offset: 0x00096535
	private IEnumerator DeactivateSequence()
	{
		this.DisableWarning.Play();
		this.GlowBlack.Enter();
		yield return new WaitForSeconds(2f);
		this.GlowBlack.Exit();
		foreach (ActionMaterial actionMaterial in this.IslandMats)
		{
			actionMaterial.Exit();
		}
		yield return new WaitForSeconds(0.5f);
		foreach (GameObject gameObject in this.SpecialObjects)
		{
			gameObject.SetActive(false);
		}
		yield return new WaitForSeconds(0.5f);
		this.surface.enabled = false;
		foreach (Collider collider in this.IslandColliders)
		{
			collider.enabled = false;
		}
		this.DisableWarning.Stop();
		yield break;
	}

	// Token: 0x06001852 RID: 6226 RVA: 0x00098344 File Offset: 0x00096544
	public RaidIslandComp()
	{
	}

	// Token: 0x04001820 RID: 6176
	public List<GameObject> IslandObjects;

	// Token: 0x04001821 RID: 6177
	public List<GameObject> SpecialObjects;

	// Token: 0x04001822 RID: 6178
	private List<ActionMaterial> IslandMats = new List<ActionMaterial>();

	// Token: 0x04001823 RID: 6179
	private List<Collider> IslandColliders = new List<Collider>();

	// Token: 0x04001824 RID: 6180
	private NavMeshSurface surface;

	// Token: 0x04001825 RID: 6181
	public ActionMaterial GlowBlack;

	// Token: 0x04001826 RID: 6182
	public ParticleSystem DisableWarning;

	// Token: 0x04001827 RID: 6183
	public ActionMaterial GlowWhite;

	// Token: 0x04001828 RID: 6184
	public ParticleSystem AppearWarning;

	// Token: 0x02000625 RID: 1573
	[CompilerGenerated]
	private sealed class <ActivateSequence>d__13 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002779 RID: 10105 RVA: 0x000D60BA File Offset: 0x000D42BA
		[DebuggerHidden]
		public <ActivateSequence>d__13(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x000D60C9 File Offset: 0x000D42C9
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000D60CC File Offset: 0x000D42CC
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			RaidIslandComp raidIslandComp = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				raidIslandComp.AppearWarning.Play();
				raidIslandComp.GlowWhite.Enter();
				this.<>2__current = new WaitForSeconds(1f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				this.<>2__current = new WaitForSeconds(1f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				foreach (ActionMaterial actionMaterial in raidIslandComp.IslandMats)
				{
					actionMaterial.Enter();
				}
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 3;
				return true;
			case 3:
				this.<>1__state = -1;
				raidIslandComp.GlowWhite.Exit();
				raidIslandComp.surface.enabled = true;
				foreach (Collider collider in raidIslandComp.IslandColliders)
				{
					collider.enabled = true;
				}
				foreach (GameObject gameObject in raidIslandComp.SpecialObjects)
				{
					gameObject.SetActive(true);
				}
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 4;
				return true;
			case 4:
				this.<>1__state = -1;
				raidIslandComp.AppearWarning.Stop();
				return false;
			default:
				return false;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x0600277C RID: 10108 RVA: 0x000D6288 File Offset: 0x000D4488
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x000D6290 File Offset: 0x000D4490
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x0600277E RID: 10110 RVA: 0x000D6297 File Offset: 0x000D4497
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002A04 RID: 10756
		private int <>1__state;

		// Token: 0x04002A05 RID: 10757
		private object <>2__current;

		// Token: 0x04002A06 RID: 10758
		public RaidIslandComp <>4__this;
	}

	// Token: 0x02000626 RID: 1574
	[CompilerGenerated]
	private sealed class <DeactivateSequence>d__15 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600277F RID: 10111 RVA: 0x000D629F File Offset: 0x000D449F
		[DebuggerHidden]
		public <DeactivateSequence>d__15(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000D62AE File Offset: 0x000D44AE
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000D62B0 File Offset: 0x000D44B0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			RaidIslandComp raidIslandComp = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				raidIslandComp.DisableWarning.Play();
				raidIslandComp.GlowBlack.Enter();
				this.<>2__current = new WaitForSeconds(2f);
				this.<>1__state = 1;
				return true;
			case 1:
				this.<>1__state = -1;
				raidIslandComp.GlowBlack.Exit();
				foreach (ActionMaterial actionMaterial in raidIslandComp.IslandMats)
				{
					actionMaterial.Exit();
				}
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 2;
				return true;
			case 2:
				this.<>1__state = -1;
				foreach (GameObject gameObject in raidIslandComp.SpecialObjects)
				{
					gameObject.SetActive(false);
				}
				this.<>2__current = new WaitForSeconds(0.5f);
				this.<>1__state = 3;
				return true;
			case 3:
				this.<>1__state = -1;
				raidIslandComp.surface.enabled = false;
				foreach (Collider collider in raidIslandComp.IslandColliders)
				{
					collider.enabled = false;
				}
				raidIslandComp.DisableWarning.Stop();
				return false;
			default:
				return false;
			}
		}

		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x000D6448 File Offset: 0x000D4648
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x000D6450 File Offset: 0x000D4650
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06002784 RID: 10116 RVA: 0x000D6457 File Offset: 0x000D4657
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002A07 RID: 10759
		private int <>1__state;

		// Token: 0x04002A08 RID: 10760
		private object <>2__current;

		// Token: 0x04002A09 RID: 10761
		public RaidIslandComp <>4__this;
	}
}

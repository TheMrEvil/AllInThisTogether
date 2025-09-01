using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Photon.Pun;
using TMPro;
using UnityEngine;

// Token: 0x0200011E RID: 286
public class Library_TargetPractice : MonoBehaviour
{
	// Token: 0x06000D82 RID: 3458 RVA: 0x00056732 File Offset: 0x00054932
	private void OnEnable()
	{
		Library_TargetPractice.instance = this;
		this.ResetActivity();
	}

	// Token: 0x06000D83 RID: 3459 RVA: 0x00056740 File Offset: 0x00054940
	public void StartEvent()
	{
		MapManager.instance.LibraryTargetWantStart();
	}

	// Token: 0x06000D84 RID: 3460 RVA: 0x0005674C File Offset: 0x0005494C
	public static bool CanStart()
	{
		return Library_TargetPractice.instance != null && Library_TargetPractice.instance.CurState == Library_TargetPractice.State.None;
	}

	// Token: 0x06000D85 RID: 3461 RVA: 0x0005676C File Offset: 0x0005496C
	public void StartEventNetworked(int seed)
	{
		this.StartBurst.Play();
		this.Timer = 0f;
		this.InteractLoop.gameObject.SetActive(false);
		this.canEnd = false;
		this.CurState = Library_TargetPractice.State.Starting;
		AudioManager.PlayClipAtPoint(this.StartSFX, base.transform.position, 1f, 1f, 0.75f, 40f, 250f);
		this.SpawnTargets(seed);
	}

	// Token: 0x06000D86 RID: 3462 RVA: 0x000567E8 File Offset: 0x000549E8
	private void Update()
	{
		this.DPSMeter.UpdateOpacity(this.CurState == Library_TargetPractice.State.None, 4f, true);
		this.TimerGroup.UpdateOpacity(this.CurState > Library_TargetPractice.State.None, 4f, true);
		if (this.CurState == Library_TargetPractice.State.Running)
		{
			this.Timer += Time.deltaTime;
			this.TimerText.text = LibraryRaces.GetTimerText(this.Timer);
			this.CheckTargets();
			if (this.canEnd && this.targets.Count <= 0)
			{
				this.EndEvent();
			}
		}
	}

	// Token: 0x06000D87 RID: 3463 RVA: 0x0005687C File Offset: 0x00054A7C
	private void EndEvent()
	{
		this.EndBurst.Play();
		AudioManager.PlayClipAtPoint(this.EndSFX, this.EndBurst.transform.position, 1f, 1f, 0.8f, 30f, 250f);
		this.CurState = Library_TargetPractice.State.Ended;
		base.Invoke("ResetActivity", 3f);
	}

	// Token: 0x06000D88 RID: 3464 RVA: 0x000568E0 File Offset: 0x00054AE0
	public void ResetActivity()
	{
		this.TargetInteraction.Activate();
		this.InteractLoop.gameObject.SetActive(true);
		this.InteractLoop.Play();
		this.CurState = Library_TargetPractice.State.None;
	}

	// Token: 0x06000D89 RID: 3465 RVA: 0x00056910 File Offset: 0x00054B10
	private void SpawnTargets(int seed)
	{
		System.Random rng = new System.Random(seed);
		base.StartCoroutine(this.SpawnSequence(rng));
	}

	// Token: 0x06000D8A RID: 3466 RVA: 0x00056932 File Offset: 0x00054B32
	private IEnumerator SpawnSequence(System.Random rng)
	{
		List<Transform> spawns = new List<Transform>();
		foreach (Transform item in this.Spawns)
		{
			spawns.Add(item);
		}
		int i = 0;
		while (i < this.TargetCount && spawns.Count > 0)
		{
			int index = rng.Next(0, spawns.Count);
			this.SpawnTarget(spawns[index]);
			spawns.RemoveAt(index);
			yield return new WaitForSeconds(0.15f);
			int num = i;
			i = num + 1;
		}
		this.CurState = Library_TargetPractice.State.Running;
		this.CheckTargets();
		yield break;
	}

	// Token: 0x06000D8B RID: 3467 RVA: 0x00056948 File Offset: 0x00054B48
	private void SpawnTarget(Transform spawn)
	{
		if (PhotonNetwork.InRoom && !PhotonNetwork.IsMasterClient)
		{
			return;
		}
		EntityControl entityControl = AIManager.SpawnAIExplicit("Unique/Lib_CrystalTarget", spawn.position, spawn.forward);
		this.targets.Add(entityControl as AIControl);
	}

	// Token: 0x06000D8C RID: 3468 RVA: 0x0005698C File Offset: 0x00054B8C
	private void CheckTargets()
	{
		for (int i = this.targets.Count - 1; i >= 0; i--)
		{
			if (this.targets[i] == null || this.targets[i].health.isDead)
			{
				this.targets.RemoveAt(i);
			}
		}
		foreach (EntityControl entityControl in AIManager.Enemies)
		{
			if (!entityControl.health.isDead)
			{
				AIControl aicontrol = entityControl as AIControl;
				if (aicontrol != null && !this.targets.Contains(aicontrol) && aicontrol.StatID == "Lib_Crystal")
				{
					this.targets.Add(aicontrol);
				}
			}
		}
		if (!this.canEnd && this.targets.Count >= 0)
		{
			this.canEnd = true;
		}
	}

	// Token: 0x06000D8D RID: 3469 RVA: 0x00056A88 File Offset: 0x00054C88
	private void OnDrawGizmos()
	{
		Gizmos.color = new Color(1f, 0.5f, 0.5f, 0.5f);
		foreach (Transform transform in this.Spawns)
		{
			if (transform != null)
			{
				Gizmos.DrawWireSphere(transform.position, 0.5f);
			}
		}
	}

	// Token: 0x06000D8E RID: 3470 RVA: 0x00056B0C File Offset: 0x00054D0C
	public Library_TargetPractice()
	{
	}

	// Token: 0x04000B15 RID: 2837
	public static Library_TargetPractice instance;

	// Token: 0x04000B16 RID: 2838
	public CanvasGroup DPSMeter;

	// Token: 0x04000B17 RID: 2839
	public CanvasGroup TimerGroup;

	// Token: 0x04000B18 RID: 2840
	public TextMeshProUGUI TimerText;

	// Token: 0x04000B19 RID: 2841
	public ParticleSystem EndBurst;

	// Token: 0x04000B1A RID: 2842
	public SimpleDiagetic TargetInteraction;

	// Token: 0x04000B1B RID: 2843
	public ParticleSystem InteractLoop;

	// Token: 0x04000B1C RID: 2844
	public ParticleSystem StartBurst;

	// Token: 0x04000B1D RID: 2845
	public AudioClip StartSFX;

	// Token: 0x04000B1E RID: 2846
	public AudioClip EndSFX;

	// Token: 0x04000B1F RID: 2847
	public int TargetCount = 10;

	// Token: 0x04000B20 RID: 2848
	public List<Transform> Spawns;

	// Token: 0x04000B21 RID: 2849
	public List<AIControl> targets = new List<AIControl>();

	// Token: 0x04000B22 RID: 2850
	public bool canEnd;

	// Token: 0x04000B23 RID: 2851
	public Library_TargetPractice.State CurState;

	// Token: 0x04000B24 RID: 2852
	private float Timer;

	// Token: 0x0200052A RID: 1322
	public enum State
	{
		// Token: 0x04002624 RID: 9764
		None,
		// Token: 0x04002625 RID: 9765
		Starting,
		// Token: 0x04002626 RID: 9766
		Running,
		// Token: 0x04002627 RID: 9767
		Ended
	}

	// Token: 0x0200052B RID: 1323
	[CompilerGenerated]
	private sealed class <SpawnSequence>d__24 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x060023FA RID: 9210 RVA: 0x000CC8DD File Offset: 0x000CAADD
		[DebuggerHidden]
		public <SpawnSequence>d__24(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x060023FB RID: 9211 RVA: 0x000CC8EC File Offset: 0x000CAAEC
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x060023FC RID: 9212 RVA: 0x000CC8F0 File Offset: 0x000CAAF0
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			Library_TargetPractice library_TargetPractice = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
				int num2 = i;
				i = num2 + 1;
			}
			else
			{
				this.<>1__state = -1;
				spawns = new List<Transform>();
				foreach (Transform item in library_TargetPractice.Spawns)
				{
					spawns.Add(item);
				}
				i = 0;
			}
			if (i < library_TargetPractice.TargetCount && spawns.Count > 0)
			{
				int index = rng.Next(0, spawns.Count);
				library_TargetPractice.SpawnTarget(spawns[index]);
				spawns.RemoveAt(index);
				this.<>2__current = new WaitForSeconds(0.15f);
				this.<>1__state = 1;
				return true;
			}
			library_TargetPractice.CurState = Library_TargetPractice.State.Running;
			library_TargetPractice.CheckTargets();
			return false;
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x060023FD RID: 9213 RVA: 0x000CCA18 File Offset: 0x000CAC18
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060023FE RID: 9214 RVA: 0x000CCA20 File Offset: 0x000CAC20
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x060023FF RID: 9215 RVA: 0x000CCA27 File Offset: 0x000CAC27
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002628 RID: 9768
		private int <>1__state;

		// Token: 0x04002629 RID: 9769
		private object <>2__current;

		// Token: 0x0400262A RID: 9770
		public Library_TargetPractice <>4__this;

		// Token: 0x0400262B RID: 9771
		public System.Random rng;

		// Token: 0x0400262C RID: 9772
		private List<Transform> <spawns>5__2;

		// Token: 0x0400262D RID: 9773
		private int <i>5__3;
	}
}

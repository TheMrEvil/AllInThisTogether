using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FA RID: 506
public class CodexStats_History : MonoBehaviour
{
	// Token: 0x06001592 RID: 5522 RVA: 0x00088134 File Offset: 0x00086334
	private void Awake()
	{
		this.Fader = base.GetComponent<CanvasGroup>();
		this.Fader.HideImmediate();
		this.RunDisplayRef.gameObject.SetActive(false);
		this.EmptyDisplayRef.gameObject.SetActive(false);
	}

	// Token: 0x06001593 RID: 5523 RVA: 0x0008816F File Offset: 0x0008636F
	public void ToggleVisibility(bool isVisible)
	{
		if (this.Fader.alpha == 0f && !isVisible)
		{
			return;
		}
		base.StopAllCoroutines();
		base.StartCoroutine(this.Fade(isVisible));
		if (isVisible)
		{
			this.SetupInfo();
		}
	}

	// Token: 0x06001594 RID: 5524 RVA: 0x000881A4 File Offset: 0x000863A4
	public void LoadRuns()
	{
		this.recentRuns = LocalRunRecord.LoadRecentRuns(10);
	}

	// Token: 0x06001595 RID: 5525 RVA: 0x000881B4 File Offset: 0x000863B4
	private void SetupInfo()
	{
		foreach (Codex_RunEntry codex_RunEntry in this.runDisplays)
		{
			UnityEngine.Object.Destroy(codex_RunEntry.gameObject);
		}
		this.runDisplays.Clear();
		foreach (GameObject obj in this.emptyDisplays)
		{
			UnityEngine.Object.Destroy(obj);
		}
		this.emptyDisplays.Clear();
		if (this.recentRuns.Count == 0)
		{
			this.GlobalStatsDisplay.GlobalTomeTitle.text = "Select a run to display";
			this.GlobalStatsDisplay.StatNavFader.HideImmediate();
			for (int i = 0; i < 3; i++)
			{
				GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.EmptyDisplayRef, this.RunDisplayHolder);
				gameObject.SetActive(true);
				this.emptyDisplays.Add(gameObject);
			}
			return;
		}
		this.SetupRuns();
	}

	// Token: 0x06001596 RID: 5526 RVA: 0x000882C8 File Offset: 0x000864C8
	private void SetupRuns()
	{
		foreach (LocalRunRecord record in this.recentRuns)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.RunDisplayRef, this.RunDisplayHolder);
			gameObject.SetActive(true);
			Codex_RunEntry component = gameObject.GetComponent<Codex_RunEntry>();
			if (component.Setup(record))
			{
				this.runDisplays.Add(component);
			}
			else
			{
				UnityEngine.Object.Destroy(gameObject.gameObject);
			}
		}
		UISelector.SetupVerticalListNav<Codex_RunEntry>(this.runDisplays, null, null, true);
		this.AutoSelect();
	}

	// Token: 0x06001597 RID: 5527 RVA: 0x0008836C File Offset: 0x0008656C
	public void AutoSelect()
	{
		UISelector.SelectSelectable(this.runDisplays[0].GetComponent<Selectable>());
	}

	// Token: 0x06001598 RID: 5528 RVA: 0x00088384 File Offset: 0x00086584
	public void SelectRecord(Codex_RunEntry run)
	{
		if (!run.Record.IsRaid)
		{
			this.GlobalStatsDisplay.Setup(run.Record);
		}
	}

	// Token: 0x06001599 RID: 5529 RVA: 0x000883A4 File Offset: 0x000865A4
	public void OnInputChanged()
	{
		foreach (GameObject gameObject in this.TabControlPrompts)
		{
			gameObject.SetActive(InputManager.IsUsingController);
		}
	}

	// Token: 0x0600159A RID: 5530 RVA: 0x000883FC File Offset: 0x000865FC
	public void NextPage()
	{
		this.GlobalStatsDisplay.NextPage();
	}

	// Token: 0x0600159B RID: 5531 RVA: 0x00088409 File Offset: 0x00086609
	public void PrevPage()
	{
		this.GlobalStatsDisplay.PrevPage();
	}

	// Token: 0x0600159C RID: 5532 RVA: 0x00088416 File Offset: 0x00086616
	private IEnumerator Fade(bool fadingIn)
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.unscaledDeltaTime * 12f;
			this.Fader.alpha = (fadingIn ? t : (1f - t));
			yield return null;
		}
		if (fadingIn)
		{
			this.Fader.ShowImmediate();
		}
		else
		{
			this.Fader.HideImmediate();
		}
		yield break;
	}

	// Token: 0x0600159D RID: 5533 RVA: 0x0008842C File Offset: 0x0008662C
	public CodexStats_History()
	{
	}

	// Token: 0x04001538 RID: 5432
	private CanvasGroup Fader;

	// Token: 0x04001539 RID: 5433
	public TextMeshProUGUI Title;

	// Token: 0x0400153A RID: 5434
	public GameObject RunDisplayRef;

	// Token: 0x0400153B RID: 5435
	public GameObject EmptyDisplayRef;

	// Token: 0x0400153C RID: 5436
	public Transform RunDisplayHolder;

	// Token: 0x0400153D RID: 5437
	public AutoScrollRect AutoScroll;

	// Token: 0x0400153E RID: 5438
	private List<GameObject> emptyDisplays = new List<GameObject>();

	// Token: 0x0400153F RID: 5439
	private List<Codex_RunEntry> runDisplays = new List<Codex_RunEntry>();

	// Token: 0x04001540 RID: 5440
	public Codex_GlobalStatsDisplay GlobalStatsDisplay;

	// Token: 0x04001541 RID: 5441
	public List<GameObject> TabControlPrompts;

	// Token: 0x04001542 RID: 5442
	private List<LocalRunRecord> recentRuns = new List<LocalRunRecord>();

	// Token: 0x020005D8 RID: 1496
	[CompilerGenerated]
	private sealed class <Fade>d__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600264D RID: 9805 RVA: 0x000D34B5 File Offset: 0x000D16B5
		[DebuggerHidden]
		public <Fade>d__21(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x000D34C4 File Offset: 0x000D16C4
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x000D34C8 File Offset: 0x000D16C8
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			CodexStats_History codexStats_History = this;
			if (num != 0)
			{
				if (num != 1)
				{
					return false;
				}
				this.<>1__state = -1;
			}
			else
			{
				this.<>1__state = -1;
				t = 0f;
			}
			if (t >= 1f)
			{
				if (fadingIn)
				{
					codexStats_History.Fader.ShowImmediate();
				}
				else
				{
					codexStats_History.Fader.HideImmediate();
				}
				return false;
			}
			t += Time.unscaledDeltaTime * 12f;
			codexStats_History.Fader.alpha = (fadingIn ? t : (1f - t));
			this.<>2__current = null;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06002650 RID: 9808 RVA: 0x000D3584 File Offset: 0x000D1784
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002651 RID: 9809 RVA: 0x000D358C File Offset: 0x000D178C
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06002652 RID: 9810 RVA: 0x000D3593 File Offset: 0x000D1793
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040028CD RID: 10445
		private int <>1__state;

		// Token: 0x040028CE RID: 10446
		private object <>2__current;

		// Token: 0x040028CF RID: 10447
		public CodexStats_History <>4__this;

		// Token: 0x040028D0 RID: 10448
		public bool fadingIn;

		// Token: 0x040028D1 RID: 10449
		private float <t>5__2;
	}
}

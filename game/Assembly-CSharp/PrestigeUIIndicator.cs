using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x0200020C RID: 524
public class PrestigeUIIndicator : MonoBehaviour
{
	// Token: 0x0600163B RID: 5691 RVA: 0x0008C767 File Offset: 0x0008A967
	private void Awake()
	{
		PrestigeUIIndicator.instance = this;
	}

	// Token: 0x0600163C RID: 5692 RVA: 0x0008C770 File Offset: 0x0008A970
	public void Trigger(int prestigeLevel, Unlockable reward)
	{
		this.Title.text = "Ascendant " + prestigeLevel.ToRomanNumeral();
		this.Icon.sprite = MetaDB.GetPrestigeIcon(prestigeLevel);
		this.SetupRewardDisplay(reward);
		base.StartCoroutine("FadeSequence");
	}

	// Token: 0x0600163D RID: 5693 RVA: 0x0008C7BC File Offset: 0x0008A9BC
	private void SetupRewardDisplay(Unlockable reward)
	{
		this.RewardDisplay.gameObject.SetActive(reward != null);
		if (reward != null)
		{
			this.RewardDisplay.Setup(0, reward);
		}
	}

	// Token: 0x0600163E RID: 5694 RVA: 0x0008C7E2 File Offset: 0x0008A9E2
	private IEnumerator FadeSequence()
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.deltaTime * 0.5f;
			this.Fader.UpdateOpacity(true, 1.5f, true);
			yield return true;
		}
		yield return new WaitForSeconds(2.25f);
		while (t > 0f)
		{
			t -= Time.deltaTime * 1f;
			this.Fader.UpdateOpacity(false, 4f, true);
			yield return true;
		}
		yield break;
	}

	// Token: 0x0600163F RID: 5695 RVA: 0x0008C7F1 File Offset: 0x0008A9F1
	public PrestigeUIIndicator()
	{
	}

	// Token: 0x040015D6 RID: 5590
	public static PrestigeUIIndicator instance;

	// Token: 0x040015D7 RID: 5591
	public CanvasGroup Fader;

	// Token: 0x040015D8 RID: 5592
	public TextMeshProUGUI Title;

	// Token: 0x040015D9 RID: 5593
	public Image Icon;

	// Token: 0x040015DA RID: 5594
	public Scriptorium_PrestigeRewardItem RewardDisplay;

	// Token: 0x020005F4 RID: 1524
	[CompilerGenerated]
	private sealed class <FadeSequence>d__8 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x0600269D RID: 9885 RVA: 0x000D3D2B File Offset: 0x000D1F2B
		[DebuggerHidden]
		public <FadeSequence>d__8(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x000D3D3A File Offset: 0x000D1F3A
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000D3D3C File Offset: 0x000D1F3C
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			PrestigeUIIndicator prestigeUIIndicator = this;
			switch (num)
			{
			case 0:
				this.<>1__state = -1;
				t = 0f;
				break;
			case 1:
				this.<>1__state = -1;
				break;
			case 2:
				this.<>1__state = -1;
				goto IL_F5;
			case 3:
				this.<>1__state = -1;
				goto IL_F5;
			default:
				return false;
			}
			if (t >= 1f)
			{
				this.<>2__current = new WaitForSeconds(2.25f);
				this.<>1__state = 2;
				return true;
			}
			t += Time.deltaTime * 0.5f;
			prestigeUIIndicator.Fader.UpdateOpacity(true, 1.5f, true);
			this.<>2__current = true;
			this.<>1__state = 1;
			return true;
			IL_F5:
			if (t <= 0f)
			{
				return false;
			}
			t -= Time.deltaTime * 1f;
			prestigeUIIndicator.Fader.UpdateOpacity(false, 4f, true);
			this.<>2__current = true;
			this.<>1__state = 3;
			return true;
		}

		// Token: 0x17000370 RID: 880
		// (get) Token: 0x060026A0 RID: 9888 RVA: 0x000D3E4C File Offset: 0x000D204C
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x000D3E54 File Offset: 0x000D2054
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000371 RID: 881
		// (get) Token: 0x060026A2 RID: 9890 RVA: 0x000D3E5B File Offset: 0x000D205B
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x04002950 RID: 10576
		private int <>1__state;

		// Token: 0x04002951 RID: 10577
		private object <>2__current;

		// Token: 0x04002952 RID: 10578
		public PrestigeUIIndicator <>4__this;

		// Token: 0x04002953 RID: 10579
		private float <t>5__2;
	}
}

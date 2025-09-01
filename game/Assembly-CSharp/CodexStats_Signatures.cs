using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FC RID: 508
public class CodexStats_Signatures : MonoBehaviour
{
	// Token: 0x060015A7 RID: 5543 RVA: 0x0008879C File Offset: 0x0008699C
	private void Awake()
	{
		this.Fader = base.GetComponent<CanvasGroup>();
		this.Fader.HideImmediate();
		using (List<CodexStats_Signatures.SignatureSelector>.Enumerator enumerator = this.SignatureSelectors.GetEnumerator())
		{
			while (enumerator.MoveNext())
			{
				CodexStats_Signatures.SignatureSelector v = enumerator.Current;
				v.ButtonRef.onClick.AddListener(delegate()
				{
					this.SetupDetailStats(v.Color);
				});
			}
		}
		this.GenStatItemRef.gameObject.SetActive(false);
	}

	// Token: 0x060015A8 RID: 5544 RVA: 0x00088844 File Offset: 0x00086A44
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

	// Token: 0x060015A9 RID: 5545 RVA: 0x00088879 File Offset: 0x00086A79
	private void SetupInfo()
	{
		this.SetupDisplayBars();
		this.SetupDetailStats(this.favoredColor);
	}

	// Token: 0x060015AA RID: 5546 RVA: 0x00088890 File Offset: 0x00086A90
	private void SetupDisplayBars()
	{
		float num = 0f;
		float num2 = 0f;
		foreach (CodexStats_Signatures.SignatureGraph signatureGraph in this.SignatureGraphs)
		{
			int num3 = (int)GameStats.GetColorStat(signatureGraph.Color, GameStats.SignatureStat.TimePlayed, 0U);
			int num4 = (int)GameStats.GetColorStat(signatureGraph.Color, GameStats.SignatureStat.TomesWon, 0U);
			if ((float)num4 > num2)
			{
				this.favoredColor = signatureGraph.Color;
			}
			num = Mathf.Max(num, (float)num3);
			num2 = Mathf.Max(num2, (float)num4);
		}
		foreach (CodexStats_Signatures.SignatureGraph signatureGraph2 in this.SignatureGraphs)
		{
			signatureGraph2.Icon.sprite = PlayerDB.GetCore(signatureGraph2.Color).MajorIcon;
			int num5 = (int)GameStats.GetColorStat(signatureGraph2.Color, GameStats.SignatureStat.TimePlayed, 0U);
			int num6 = (int)GameStats.GetColorStat(signatureGraph2.Color, GameStats.SignatureStat.TomesWon, 0U);
			signatureGraph2.TimePlayedFill.fillAmount = (float)num5 / num;
			signatureGraph2.TomesFill.fillAmount = (float)num6 / num2;
			signatureGraph2.TomesWon.text = num6.ToCommaSeparatedString();
			TimeSpan timeSpan = TimeSpan.FromSeconds((double)num5);
			signatureGraph2.TimePlayedCounter.text = timeSpan.ToString("hh\\:mm\\:ss");
		}
	}

	// Token: 0x060015AB RID: 5547 RVA: 0x00088A08 File Offset: 0x00086C08
	private void SetupDetailStats(MagicColor color)
	{
		foreach (CodexStats_Signatures.SignatureSelector signatureSelector in this.SignatureSelectors)
		{
			signatureSelector.SelectedDisplay.SetActive(signatureSelector.Color == color);
		}
		this.curStatColor = color;
		this.curIndex = this.colors.IndexOf(color);
		foreach (PostGame_StatEntry postGame_StatEntry in this.statDisplays)
		{
			UnityEngine.Object.Destroy(postGame_StatEntry.gameObject);
		}
		this.statDisplays.Clear();
		this.AddStatBox(GameStats.SignatureStat.ChaptersComplete, "Chapters Completed");
		GameStats.GetColorStat(this.curStatColor, GameStats.SignatureStat.TomesPlayed, 0U);
		GameStats.GetColorStat(this.curStatColor, GameStats.SignatureStat.TomesWon, 0U);
		this.AddStatBox(GameStats.SignatureStat.MaxBinding, "Max Bindings Mended");
		this.AddStatBox(GameStats.SignatureStat.MaxAppendix, "Highest Appendix");
		this.AddStatBox(GameStats.SignatureStat.UltsCast, "Signature Spells Cast");
		this.AddStatBox(GameStats.SignatureStat.ManaSpent, "Mana Spent");
		switch (this.curStatColor)
		{
		case MagicColor.Red:
			this.AddStatBox(GameStats.SignatureStat.TetherDamage, "Tether Damage");
			this.AddStatBox(GameStats.SignatureStat.LeechHeal, "Leech Healing");
			this.AddStatBox(GameStats.SignatureStat.RedUltTethers, "Signature Ability Tethers");
			return;
		case MagicColor.Yellow:
			this.AddStatBox(GameStats.SignatureStat.FlourishDamage, "Flourish Damage");
			this.AddStatBox(GameStats.SignatureStat.FlourishBiggestHit, "Largest Flourish");
			this.AddStatBox(GameStats.SignatureStat.YellowUltCrits, "Signature Ability Flourishes");
			return;
		case MagicColor.Green:
			this.AddStatBox(GameStats.SignatureStat.InklingDamage, "Inkling Damage");
			this.AddStatBox(GameStats.SignatureStat.InklingEvolveSeconds, "Evolved Inkling Time");
			this.AddStatBox(GameStats.SignatureStat.InklingCasts, "Inkling Casts");
			return;
		case MagicColor.Blue:
			this.AddStatBox(GameStats.SignatureStat.BlotDamage, "Blot Damage");
			this.AddStatBox(GameStats.SignatureStat.BlotSpread, "Spread Blot");
			this.AddStatBox(GameStats.SignatureStat.BlutUltBlots, "Signature Ability Blotted");
			return;
		case MagicColor.Purple:
			break;
		case MagicColor.Pink:
			this.AddStatBox(GameStats.SignatureStat.FinalizeDamage, "Finalize Damage");
			this.AddStatBox(GameStats.SignatureStat.DraftsCreated, "Drafts Created");
			this.AddStatBox(GameStats.SignatureStat.DraftsExploded, "Drafts Finalized");
			return;
		case MagicColor.Orange:
			this.AddStatBox(GameStats.SignatureStat.Atlas_Damage, "Atlas Damage");
			this.AddStatBox(GameStats.SignatureStat.Survey_Deposited, "Survey Deposited");
			return;
		case MagicColor.Teal:
			this.AddStatBox(GameStats.SignatureStat.ShardDamage, "Plot Twist Damage");
			this.AddStatBox(GameStats.SignatureStat.ShardsFired, "Plot Twists Launched");
			this.AddStatBox(GameStats.SignatureStat.ArmorCreated, "Plot Armor Woven");
			break;
		default:
			return;
		}
	}

	// Token: 0x060015AC RID: 5548 RVA: 0x00088C98 File Offset: 0x00086E98
	private void AddStatBox(GameStats.SignatureStat s, string label)
	{
		ulong colorStat = GameStats.GetColorStat(this.curStatColor, s, 0U);
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GenStatItemRef, this.GenStatHolder);
		gameObject.SetActive(true);
		PostGame_StatEntry component = gameObject.GetComponent<PostGame_StatEntry>();
		component.SetupBasic(label, colorStat, false);
		this.statDisplays.Add(component);
	}

	// Token: 0x060015AD RID: 5549 RVA: 0x00088CE8 File Offset: 0x00086EE8
	private void AddStatBox(string label, ulong value)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GenStatItemRef, this.GenStatHolder);
		gameObject.SetActive(true);
		PostGame_StatEntry component = gameObject.GetComponent<PostGame_StatEntry>();
		component.SetupBasic(label, value, false);
		this.statDisplays.Add(component);
	}

	// Token: 0x060015AE RID: 5550 RVA: 0x00088D28 File Offset: 0x00086F28
	public void OnInputChanged()
	{
		foreach (GameObject gameObject in this.TabControlPrompts)
		{
			gameObject.SetActive(InputManager.IsUsingController);
		}
	}

	// Token: 0x060015AF RID: 5551 RVA: 0x00088D80 File Offset: 0x00086F80
	public void NextPage()
	{
		this.curIndex++;
		if (this.curIndex >= this.colors.Count)
		{
			this.curIndex = 0;
		}
		this.SetupDetailStats(this.colors[this.curIndex]);
	}

	// Token: 0x060015B0 RID: 5552 RVA: 0x00088DCC File Offset: 0x00086FCC
	public void PrevPage()
	{
		this.curIndex--;
		if (this.curIndex < 0)
		{
			this.curIndex = this.colors.Count - 1;
		}
		this.SetupDetailStats(this.colors[this.curIndex]);
	}

	// Token: 0x060015B1 RID: 5553 RVA: 0x00088E1A File Offset: 0x0008701A
	private IEnumerator Fade(bool fadingIn)
	{
		float t = 0f;
		this.Fader.UpdateOpacity(fadingIn, 1f, false);
		while (t < 1f)
		{
			t += Time.unscaledDeltaTime * 12f;
			this.Fader.alpha = (fadingIn ? t : (1f - t));
			yield return null;
		}
		yield break;
	}

	// Token: 0x060015B2 RID: 5554 RVA: 0x00088E30 File Offset: 0x00087030
	public CodexStats_Signatures()
	{
	}

	// Token: 0x04001553 RID: 5459
	private CanvasGroup Fader;

	// Token: 0x04001554 RID: 5460
	public List<CodexStats_Signatures.SignatureGraph> SignatureGraphs;

	// Token: 0x04001555 RID: 5461
	private MagicColor favoredColor = MagicColor.Blue;

	// Token: 0x04001556 RID: 5462
	public List<CodexStats_Signatures.SignatureSelector> SignatureSelectors;

	// Token: 0x04001557 RID: 5463
	public List<GameObject> TabControlPrompts;

	// Token: 0x04001558 RID: 5464
	public MagicColor curStatColor;

	// Token: 0x04001559 RID: 5465
	public GameObject GenStatItemRef;

	// Token: 0x0400155A RID: 5466
	public Transform GenStatHolder;

	// Token: 0x0400155B RID: 5467
	private List<PostGame_StatEntry> statDisplays = new List<PostGame_StatEntry>();

	// Token: 0x0400155C RID: 5468
	private List<MagicColor> colors = new List<MagicColor>
	{
		MagicColor.Blue,
		MagicColor.Yellow,
		MagicColor.Green,
		MagicColor.Red,
		MagicColor.Pink,
		MagicColor.Orange,
		MagicColor.Teal
	};

	// Token: 0x0400155D RID: 5469
	private int curIndex;

	// Token: 0x020005DC RID: 1500
	[Serializable]
	public class SignatureGraph
	{
		// Token: 0x0600265C RID: 9820 RVA: 0x000D3757 File Offset: 0x000D1957
		public SignatureGraph()
		{
		}

		// Token: 0x040028E1 RID: 10465
		public MagicColor Color;

		// Token: 0x040028E2 RID: 10466
		public Image Icon;

		// Token: 0x040028E3 RID: 10467
		public Image TimePlayedFill;

		// Token: 0x040028E4 RID: 10468
		public Image TomesFill;

		// Token: 0x040028E5 RID: 10469
		public TextMeshProUGUI TomesWon;

		// Token: 0x040028E6 RID: 10470
		public TextMeshProUGUI TimePlayedCounter;
	}

	// Token: 0x020005DD RID: 1501
	[Serializable]
	public class SignatureSelector
	{
		// Token: 0x0600265D RID: 9821 RVA: 0x000D375F File Offset: 0x000D195F
		public SignatureSelector()
		{
		}

		// Token: 0x040028E7 RID: 10471
		public MagicColor Color;

		// Token: 0x040028E8 RID: 10472
		public GameObject SelectedDisplay;

		// Token: 0x040028E9 RID: 10473
		public Button ButtonRef;
	}

	// Token: 0x020005DE RID: 1502
	[CompilerGenerated]
	private sealed class <>c__DisplayClass9_0
	{
		// Token: 0x0600265E RID: 9822 RVA: 0x000D3767 File Offset: 0x000D1967
		public <>c__DisplayClass9_0()
		{
		}

		// Token: 0x0600265F RID: 9823 RVA: 0x000D376F File Offset: 0x000D196F
		internal void <Awake>b__0()
		{
			this.<>4__this.SetupDetailStats(this.v.Color);
		}

		// Token: 0x040028EA RID: 10474
		public CodexStats_Signatures.SignatureSelector v;

		// Token: 0x040028EB RID: 10475
		public CodexStats_Signatures <>4__this;
	}

	// Token: 0x020005DF RID: 1503
	[CompilerGenerated]
	private sealed class <Fade>d__21 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002660 RID: 9824 RVA: 0x000D3787 File Offset: 0x000D1987
		[DebuggerHidden]
		public <Fade>d__21(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002661 RID: 9825 RVA: 0x000D3796 File Offset: 0x000D1996
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002662 RID: 9826 RVA: 0x000D3798 File Offset: 0x000D1998
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			CodexStats_Signatures codexStats_Signatures = this;
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
				codexStats_Signatures.Fader.UpdateOpacity(fadingIn, 1f, false);
			}
			if (t >= 1f)
			{
				return false;
			}
			t += Time.unscaledDeltaTime * 12f;
			codexStats_Signatures.Fader.alpha = (fadingIn ? t : (1f - t));
			this.<>2__current = null;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06002663 RID: 9827 RVA: 0x000D384B File Offset: 0x000D1A4B
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x06002664 RID: 9828 RVA: 0x000D3853 File Offset: 0x000D1A53
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06002665 RID: 9829 RVA: 0x000D385A File Offset: 0x000D1A5A
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040028EC RID: 10476
		private int <>1__state;

		// Token: 0x040028ED RID: 10477
		private object <>2__current;

		// Token: 0x040028EE RID: 10478
		public CodexStats_Signatures <>4__this;

		// Token: 0x040028EF RID: 10479
		public bool fadingIn;

		// Token: 0x040028F0 RID: 10480
		private float <t>5__2;
	}
}

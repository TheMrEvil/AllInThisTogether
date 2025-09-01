using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x020001FB RID: 507
public class CodexStats_Overview : MonoBehaviour
{
	// Token: 0x0600159E RID: 5534 RVA: 0x00088455 File Offset: 0x00086655
	private void Awake()
	{
		this.Fader = base.GetComponent<CanvasGroup>();
		this.Fader.HideImmediate();
		this.GenStatItemRef.gameObject.SetActive(false);
	}

	// Token: 0x0600159F RID: 5535 RVA: 0x0008847F File Offset: 0x0008667F
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

	// Token: 0x060015A0 RID: 5536 RVA: 0x000884B4 File Offset: 0x000866B4
	private void SetupInfo()
	{
		this.SetupNameInfo();
		this.SetupMainStats();
		this.SetupGenralStats();
	}

	// Token: 0x060015A1 RID: 5537 RVA: 0x000884C8 File Offset: 0x000866C8
	private void SetupNameInfo()
	{
		int prestigeCount = Progression.PrestigeCount;
		this.PrestigeDisplay.SetActive(prestigeCount > 0);
		this.PrestigeIcon.sprite = MetaDB.GetPrestigeIcon(prestigeCount);
		this.LevelText.text = Mathf.Max(Progression.InkLevel, 1).ToString();
		this.PlayerName.text = PlayerControl.myInstance.Username;
	}

	// Token: 0x060015A2 RID: 5538 RVA: 0x00088530 File Offset: 0x00086730
	private void SetupMainStats()
	{
		TimeSpan timeSpan = TimeSpan.FromSeconds((double)GameStats.GetTotalTimePlayed());
		this.Timeplayed.text = timeSpan.ToString("hh\\:mm\\:ss");
		this.TomesMended.text = GameStats.GetGlobalStat(GameStats.Stat.TomesWon, 0).ToCommaSeparatedString();
		this.HighestBinding.text = GameStats.GetGlobalStat(GameStats.Stat.MaxBinding, 0).ToString();
		this.QuillmarkTotal.text = (Currency.LCoinSpent + Currency.LoadoutCoin).ToCommaSeparatedString();
		this.GildingTotal.text = (Currency.GildingsSpent + Currency.Gildings).ToCommaSeparatedString();
		MagicColor color = MagicColor.Neutral;
		int num = 0;
		foreach (object obj in Enum.GetValues(typeof(MagicColor)))
		{
			int num2 = (int)GameStats.GetColorStat((MagicColor)obj, GameStats.SignatureStat.TomesPlayed, 0U);
			if (num2 > num)
			{
				num = num2;
				color = (MagicColor)obj;
			}
		}
		this.FaveSignatureIcon.sprite = PlayerDB.GetCore(color).MajorIcon;
		this.FaveSignature.text = color.ToString();
	}

	// Token: 0x060015A3 RID: 5539 RVA: 0x0008866C File Offset: 0x0008686C
	private void SetupGenralStats()
	{
		foreach (PostGame_StatEntry postGame_StatEntry in this.statDisplays)
		{
			UnityEngine.Object.Destroy(postGame_StatEntry.gameObject);
		}
		this.statDisplays.Clear();
		foreach (CodexStats_Overview.StatInfo statInfo in this.Stats)
		{
			ulong value = statInfo.GetValue();
			if (value >= (ulong)((long)statInfo.MinValue))
			{
				this.AddStatBox(statInfo.Title + ":", value);
			}
		}
	}

	// Token: 0x060015A4 RID: 5540 RVA: 0x00088730 File Offset: 0x00086930
	private void AddStatBox(string label, ulong value)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.GenStatItemRef, this.GenStatHolder);
		gameObject.SetActive(true);
		PostGame_StatEntry component = gameObject.GetComponent<PostGame_StatEntry>();
		component.SetupBasic(label, value, false);
		this.statDisplays.Add(component);
	}

	// Token: 0x060015A5 RID: 5541 RVA: 0x00088770 File Offset: 0x00086970
	private IEnumerator Fade(bool fadingIn)
	{
		float t = 0f;
		while (t < 1f)
		{
			t += Time.unscaledDeltaTime * 12f;
			this.Fader.alpha = (fadingIn ? t : (1f - t));
			yield return null;
		}
		yield break;
	}

	// Token: 0x060015A6 RID: 5542 RVA: 0x00088786 File Offset: 0x00086986
	public CodexStats_Overview()
	{
	}

	// Token: 0x04001543 RID: 5443
	private CanvasGroup Fader;

	// Token: 0x04001544 RID: 5444
	public GameObject PrestigeDisplay;

	// Token: 0x04001545 RID: 5445
	public Image PrestigeIcon;

	// Token: 0x04001546 RID: 5446
	public TextMeshProUGUI LevelText;

	// Token: 0x04001547 RID: 5447
	public TextMeshProUGUI PlayerName;

	// Token: 0x04001548 RID: 5448
	public TextMeshProUGUI Timeplayed;

	// Token: 0x04001549 RID: 5449
	public TextMeshProUGUI TomesMended;

	// Token: 0x0400154A RID: 5450
	public TextMeshProUGUI HighestBinding;

	// Token: 0x0400154B RID: 5451
	public TextMeshProUGUI QuillmarkTotal;

	// Token: 0x0400154C RID: 5452
	public TextMeshProUGUI GildingTotal;

	// Token: 0x0400154D RID: 5453
	public Image FaveSignatureIcon;

	// Token: 0x0400154E RID: 5454
	public TextMeshProUGUI FaveSignature;

	// Token: 0x0400154F RID: 5455
	public GameObject GenStatItemRef;

	// Token: 0x04001550 RID: 5456
	public Transform GenStatHolder;

	// Token: 0x04001551 RID: 5457
	private List<PostGame_StatEntry> statDisplays = new List<PostGame_StatEntry>();

	// Token: 0x04001552 RID: 5458
	[Space(10f)]
	public List<CodexStats_Overview.StatInfo> Stats;

	// Token: 0x020005D9 RID: 1497
	[Serializable]
	public class StatInfo
	{
		// Token: 0x06002653 RID: 9811 RVA: 0x000D359C File Offset: 0x000D179C
		public ulong GetValue()
		{
			if (this.Category == CodexStats_Overview.StatType.Gameplay)
			{
				if (this.PlrStat == PlayerStat.TotalDamage)
				{
					return GameStats.GetTotalDamageDone();
				}
				if (this.PlrStat == PlayerStat.TotalHealing)
				{
					return GameStats.GetTotalHealingDone();
				}
			}
			ulong result;
			switch (this.Category)
			{
			case CodexStats_Overview.StatType.Gameplay:
				result = GameStats.GetPlayerStat(this.PlrStat, 0U);
				break;
			case CodexStats_Overview.StatType.Meta:
				result = (ulong)((long)GameStats.GetGlobalStat(this.MetaStat, 0));
				break;
			case CodexStats_Overview.StatType.Special:
				result = (ulong)((long)GameStats.GetSpecialStat(this.SpecialStat));
				break;
			default:
				result = 0UL;
				break;
			}
			return result;
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x000D3620 File Offset: 0x000D1820
		public string GroupName()
		{
			string result;
			switch (this.Category)
			{
			case CodexStats_Overview.StatType.Gameplay:
				result = this.PlrStat.ToString();
				break;
			case CodexStats_Overview.StatType.Meta:
				result = this.MetaStat.ToString();
				break;
			case CodexStats_Overview.StatType.Special:
				result = this.SpecialStat.ToString();
				break;
			default:
				result = "___";
				break;
			}
			return result;
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x000D368B File Offset: 0x000D188B
		public StatInfo()
		{
		}

		// Token: 0x040028D2 RID: 10450
		public CodexStats_Overview.StatType Category;

		// Token: 0x040028D3 RID: 10451
		public PlayerStat PlrStat;

		// Token: 0x040028D4 RID: 10452
		public GameStats.Stat MetaStat;

		// Token: 0x040028D5 RID: 10453
		public GameStats.SpecialStat SpecialStat;

		// Token: 0x040028D6 RID: 10454
		public string Title;

		// Token: 0x040028D7 RID: 10455
		public int MinValue;
	}

	// Token: 0x020005DA RID: 1498
	public enum StatType
	{
		// Token: 0x040028D9 RID: 10457
		Gameplay,
		// Token: 0x040028DA RID: 10458
		Meta,
		// Token: 0x040028DB RID: 10459
		Special
	}

	// Token: 0x020005DB RID: 1499
	[CompilerGenerated]
	private sealed class <Fade>d__23 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002656 RID: 9814 RVA: 0x000D3693 File Offset: 0x000D1893
		[DebuggerHidden]
		public <Fade>d__23(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x06002657 RID: 9815 RVA: 0x000D36A2 File Offset: 0x000D18A2
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x000D36A4 File Offset: 0x000D18A4
		bool IEnumerator.MoveNext()
		{
			int num = this.<>1__state;
			CodexStats_Overview codexStats_Overview = this;
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
				return false;
			}
			t += Time.unscaledDeltaTime * 12f;
			codexStats_Overview.Fader.alpha = (fadingIn ? t : (1f - t));
			this.<>2__current = null;
			this.<>1__state = 1;
			return true;
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06002659 RID: 9817 RVA: 0x000D3740 File Offset: 0x000D1940
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x000D3748 File Offset: 0x000D1948
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000363 RID: 867
		// (get) Token: 0x0600265B RID: 9819 RVA: 0x000D374F File Offset: 0x000D194F
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x040028DC RID: 10460
		private int <>1__state;

		// Token: 0x040028DD RID: 10461
		private object <>2__current;

		// Token: 0x040028DE RID: 10462
		public CodexStats_Overview <>4__this;

		// Token: 0x040028DF RID: 10463
		public bool fadingIn;

		// Token: 0x040028E0 RID: 10464
		private float <t>5__2;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using SimpleJSON;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// Token: 0x02000148 RID: 328
public class Codex_GlobalStatsDisplay : MonoBehaviour
{
	// Token: 0x06000EC7 RID: 3783 RVA: 0x0005D986 File Offset: 0x0005BB86
	private void Awake()
	{
		this.timeBarRef.gameObject.SetActive(false);
		this.SetupPips();
	}

	// Token: 0x06000EC8 RID: 3784 RVA: 0x0005D9A0 File Offset: 0x0005BBA0
	public void Setup(LocalRunRecord run)
	{
		this.currentRun = run;
		GenreTree genre = GraphDB.GetGenre(run.TomeID);
		this.GlobalTomeTitle.text = genre.Root.ShortName;
		this.ShowStats(this.CurrentStat);
	}

	// Token: 0x06000EC9 RID: 3785 RVA: 0x0005D9E4 File Offset: 0x0005BBE4
	private void ShowStats(Codex_GlobalStatsDisplay.ComparisonType type)
	{
		this.CurrentStat = type;
		this.NoDataFader.HideImmediate();
		this.StatNavFader.ShowImmediate();
		TextMeshProUGUI statTypeTitle = this.StatTypeTitle;
		string text;
		switch (type)
		{
		case Codex_GlobalStatsDisplay.ComparisonType.Duration:
			text = "Duration";
			break;
		case Codex_GlobalStatsDisplay.ComparisonType.BindingLevel:
			text = "Binding Level";
			break;
		case Codex_GlobalStatsDisplay.ComparisonType.AppendixReached:
			text = "Appendix Reached";
			break;
		default:
			text = "";
			break;
		}
		statTypeTitle.text = text;
		foreach (KeyValuePair<Codex_GlobalStatsDisplay.ComparisonType, Image> keyValuePair in this.pips)
		{
			keyValuePair.Value.sprite = ((keyValuePair.Key == type) ? this.Pip_Filled : this.Pip_Empty);
		}
		this.CreateBars();
	}

	// Token: 0x06000ECA RID: 3786 RVA: 0x0005DAB8 File Offset: 0x0005BCB8
	private void SetupPips()
	{
		for (int i = 0; i < Enum.GetValues(typeof(Codex_GlobalStatsDisplay.ComparisonType)).Length; i++)
		{
			Codex_GlobalStatsDisplay.ComparisonType stat = (Codex_GlobalStatsDisplay.ComparisonType)i;
			GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.PipRef, this.PipHolder.transform);
			gameObject.SetActive(true);
			gameObject.GetComponent<Button>().onClick.AddListener(delegate()
			{
				this.ShowStats(stat);
			});
			this.pips.Add(stat, gameObject.GetComponent<Image>());
		}
	}

	// Token: 0x06000ECB RID: 3787 RVA: 0x0005DB49 File Offset: 0x0005BD49
	public void NextPage()
	{
		this.CurrentStat++;
		if (this.CurrentStat > Codex_GlobalStatsDisplay.ComparisonType.AppendixReached)
		{
			this.CurrentStat = Codex_GlobalStatsDisplay.ComparisonType.Duration;
		}
		this.ShowStats(this.CurrentStat);
	}

	// Token: 0x06000ECC RID: 3788 RVA: 0x0005DB75 File Offset: 0x0005BD75
	public void PrevPage()
	{
		this.CurrentStat--;
		if (this.CurrentStat < Codex_GlobalStatsDisplay.ComparisonType.Duration)
		{
			this.CurrentStat = Codex_GlobalStatsDisplay.ComparisonType.AppendixReached;
		}
		this.ShowStats(this.CurrentStat);
	}

	// Token: 0x06000ECD RID: 3789 RVA: 0x0005DBA4 File Offset: 0x0005BDA4
	private void CreateBars()
	{
		this.ClearBars();
		if (!this.tomeStats.ContainsKey(this.currentRun.TomeID))
		{
			this.NoDataFader.ShowImmediate();
			this.StatNavFader.HideImmediate();
			return;
		}
		List<Codex_GlobalStatsDisplay.StatBucket> buckets = this.GetBuckets();
		float currentRunStat = this.GetCurrentRunStat();
		float num = 0f;
		foreach (Codex_GlobalStatsDisplay.StatBucket statBucket in buckets)
		{
			num = Mathf.Max(num, (float)statBucket.Count);
		}
		foreach (Codex_GlobalStatsDisplay.StatBucket statBucket2 in buckets)
		{
			bool isSelf = statBucket2.MyValueFits(currentRunStat) && this.currentRun.Won;
			this.CreateBar(statBucket2.Label, Mathf.Sqrt((float)statBucket2.Count), Mathf.Sqrt(num), isSelf);
		}
	}

	// Token: 0x06000ECE RID: 3790 RVA: 0x0005DCB8 File Offset: 0x0005BEB8
	private float GetCurrentRunStat()
	{
		float result;
		switch (this.CurrentStat)
		{
		case Codex_GlobalStatsDisplay.ComparisonType.Duration:
			result = this.currentRun.Timer;
			break;
		case Codex_GlobalStatsDisplay.ComparisonType.BindingLevel:
			result = (float)this.currentRun.BindingLevel;
			break;
		case Codex_GlobalStatsDisplay.ComparisonType.AppendixReached:
			result = (float)this.currentRun.Appendix;
			break;
		default:
			result = 0f;
			break;
		}
		return result;
	}

	// Token: 0x06000ECF RID: 3791 RVA: 0x0005DD14 File Offset: 0x0005BF14
	private List<Codex_GlobalStatsDisplay.StatBucket> GetBuckets()
	{
		List<Codex_GlobalStatsDisplay.StatBucket> buckets = this.GetBuckets(this.CurrentStat);
		Codex_GlobalStatsDisplay.TomeStatData tomeStatData = this.tomeStats[this.currentRun.TomeID];
		Dictionary<int, int> dictionary;
		switch (this.CurrentStat)
		{
		case Codex_GlobalStatsDisplay.ComparisonType.Duration:
			dictionary = tomeStatData.Timers;
			break;
		case Codex_GlobalStatsDisplay.ComparisonType.BindingLevel:
			dictionary = tomeStatData.Bindings;
			break;
		case Codex_GlobalStatsDisplay.ComparisonType.AppendixReached:
			dictionary = tomeStatData.Appendix;
			break;
		default:
			dictionary = null;
			break;
		}
		Dictionary<int, int> dictionary2 = dictionary;
		if (dictionary2 == null)
		{
			return buckets;
		}
		int num = 0;
		foreach (KeyValuePair<int, int> keyValuePair in dictionary2)
		{
			num += keyValuePair.Value;
		}
		if (this.CurrentStat == Codex_GlobalStatsDisplay.ComparisonType.AppendixReached)
		{
			num += tomeStatData.Total + dictionary2[1];
		}
		foreach (Codex_GlobalStatsDisplay.StatBucket statBucket in buckets)
		{
			foreach (KeyValuePair<int, int> keyValuePair2 in dictionary2)
			{
				if (statBucket.ValueFits((float)keyValuePair2.Key))
				{
					statBucket.Count += keyValuePair2.Value;
				}
			}
			statBucket.Max = (float)num;
		}
		if (this.CurrentStat == Codex_GlobalStatsDisplay.ComparisonType.AppendixReached)
		{
			buckets[0].Count = tomeStatData.Total;
		}
		return buckets;
	}

	// Token: 0x06000ED0 RID: 3792 RVA: 0x0005DEA8 File Offset: 0x0005C0A8
	private void CreateBar(string stat, float value, float max, bool isSelf)
	{
		GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(this.timeBarRef, this.timeBarHolder);
		gameObject.SetActive(true);
		Codex_RunStatBar component = gameObject.GetComponent<Codex_RunStatBar>();
		component.Setup(stat, value / max, isSelf);
		this.timeBarObjects.Add(component);
	}

	// Token: 0x06000ED1 RID: 3793 RVA: 0x0005DEEC File Offset: 0x0005C0EC
	private void ClearBars()
	{
		foreach (Codex_RunStatBar codex_RunStatBar in this.timeBarObjects)
		{
			UnityEngine.Object.Destroy(codex_RunStatBar.gameObject);
		}
		this.timeBarObjects.Clear();
	}

	// Token: 0x06000ED2 RID: 3794 RVA: 0x0005DF4C File Offset: 0x0005C14C
	private List<Codex_GlobalStatsDisplay.StatBucket> GetBuckets(Codex_GlobalStatsDisplay.ComparisonType type)
	{
		List<Codex_GlobalStatsDisplay.StatBucket> list = new List<Codex_GlobalStatsDisplay.StatBucket>();
		switch (type)
		{
		case Codex_GlobalStatsDisplay.ComparisonType.Duration:
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("Under 6:00", 0f, 360f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("6:00-8:00", 360f, 480f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("8:00-10:00", 480f, 600f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("10:00-12:00", 600f, 720f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("12:00-14:00", 720f, 840f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("14:00-16:00", 840f, 960f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("16:00-18:00", 960f, 1080f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("18:00-20:00", 1080f, 1200f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("20:00-22:00", 1200f, 1320f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("22:00-24:00", 1320f, 1440f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("24:00-26:00", 1440f, 1560f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("26:00-28:00", 1560f, 1680f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("28:00-30:00", 1680f, 1800f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("30:00-32:00", 1800f, 1920f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("Over 32:00", 1920f, 2.1474836E+09f, -1f, -1f));
			break;
		case Codex_GlobalStatsDisplay.ComparisonType.BindingLevel:
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("0", -1f, 0f, -1f, 0f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("1-2", 1f, 2f, 1f, 2f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("3-4", 3f, 4f, 3f, 4f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("5-6", 5f, 6f, 5f, 6f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("7-8", 7f, 8f, 7f, 8f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("9-10", 9f, 10f, 9f, 10f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("11-12", 11f, 12f, 11f, 12f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("13-14", 13f, 14f, 13f, 14f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("15-16", 15f, 10f, 15f, 16f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("17-18", 17f, 18f, 17f, 18f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("19-20", 19f, 20f, 19f, 20f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("21-30", 21f, 30f, 21f, 30f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("31-40", 31f, 40f, 31f, 40f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("41-50", 41f, 50f, 41f, 50f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("51+", 51f, 999f, 51f, 2.1474836E+09f));
			break;
		case Codex_GlobalStatsDisplay.ComparisonType.AppendixReached:
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("0", -1f, 0f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("1", 1f, 1f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("2", 2f, 2f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("3", 3f, 3f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("4", 4f, 4f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("5", 5f, 5f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("6", 6f, 6f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("7", 7f, 7f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("8", 8f, 8f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("9", 9f, 9f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("10", 10f, 10f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("11", 11f, 11f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("12", 12f, 12f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("13", 13f, 13f, -1f, -1f));
			list.Add(new Codex_GlobalStatsDisplay.StatBucket("14+", 14f, 5000f, -1f, -1f));
			break;
		}
		return list;
	}

	// Token: 0x06000ED3 RID: 3795 RVA: 0x0005E5D5 File Offset: 0x0005C7D5
	public void RefreshTomeStats()
	{
		if (this.isFetching)
		{
			return;
		}
		base.StartCoroutine("FetchTomeStats");
	}

	// Token: 0x06000ED4 RID: 3796 RVA: 0x0005E5EC File Offset: 0x0005C7EC
	private IEnumerator FetchTomeStats()
	{
		if (this.isFetching)
		{
			yield break;
		}
		this.isFetching = true;
		List<UnlockDB.GenreUnlock> genres = UnlockDB.DB.Genres;
		foreach (UnlockDB.GenreUnlock genreUnlock in genres)
		{
			string tomeID = genreUnlock.Genre.ID;
			if (!this.tomeStats.ContainsKey(tomeID))
			{
				Task<JSONNode> task = ParseManager.FetchTomeStats(tomeID);
				float safetyTime = 2.5f;
				while (safetyTime > 0f && !task.IsCompleted)
				{
					yield return true;
				}
				JSONNode result = task.Result;
				if (result != null)
				{
					Codex_GlobalStatsDisplay.TomeStatData value = new Codex_GlobalStatsDisplay.TomeStatData(result);
					this.tomeStats.Add(tomeID, value);
				}
				tomeID = null;
				task = null;
			}
		}
		List<UnlockDB.GenreUnlock>.Enumerator enumerator = default(List<UnlockDB.GenreUnlock>.Enumerator);
		UnityEngine.Debug.Log("Fetched Global Stats for " + this.tomeStats.Count.ToString() + " Tome(s)");
		this.isFetching = false;
		yield break;
		yield break;
	}

	// Token: 0x06000ED5 RID: 3797 RVA: 0x0005E5FB File Offset: 0x0005C7FB
	public Codex_GlobalStatsDisplay()
	{
	}

	// Token: 0x04000C67 RID: 3175
	public TextMeshProUGUI GlobalTomeTitle;

	// Token: 0x04000C68 RID: 3176
	public GameObject timeBarRef;

	// Token: 0x04000C69 RID: 3177
	public Transform timeBarHolder;

	// Token: 0x04000C6A RID: 3178
	private List<Codex_RunStatBar> timeBarObjects = new List<Codex_RunStatBar>();

	// Token: 0x04000C6B RID: 3179
	public CanvasGroup NoDataFader;

	// Token: 0x04000C6C RID: 3180
	[Header("Stat Navigation")]
	public CanvasGroup StatNavFader;

	// Token: 0x04000C6D RID: 3181
	public TextMeshProUGUI StatTypeTitle;

	// Token: 0x04000C6E RID: 3182
	public Codex_GlobalStatsDisplay.ComparisonType CurrentStat;

	// Token: 0x04000C6F RID: 3183
	public GameObject PipHolder;

	// Token: 0x04000C70 RID: 3184
	public GameObject PipRef;

	// Token: 0x04000C71 RID: 3185
	public Sprite Pip_Filled;

	// Token: 0x04000C72 RID: 3186
	public Sprite Pip_Empty;

	// Token: 0x04000C73 RID: 3187
	private Dictionary<Codex_GlobalStatsDisplay.ComparisonType, Image> pips = new Dictionary<Codex_GlobalStatsDisplay.ComparisonType, Image>();

	// Token: 0x04000C74 RID: 3188
	private LocalRunRecord currentRun;

	// Token: 0x04000C75 RID: 3189
	private Dictionary<string, Codex_GlobalStatsDisplay.TomeStatData> tomeStats = new Dictionary<string, Codex_GlobalStatsDisplay.TomeStatData>();

	// Token: 0x04000C76 RID: 3190
	private bool isFetching;

	// Token: 0x02000541 RID: 1345
	public enum ComparisonType
	{
		// Token: 0x0400267C RID: 9852
		Duration,
		// Token: 0x0400267D RID: 9853
		BindingLevel,
		// Token: 0x0400267E RID: 9854
		AppendixReached,
		// Token: 0x0400267F RID: 9855
		ChallengeStat
	}

	// Token: 0x02000542 RID: 1346
	public class StatBucket
	{
		// Token: 0x06002432 RID: 9266 RVA: 0x000CD480 File Offset: 0x000CB680
		public StatBucket(string label, float GlobalMin, float GlobalMax, float myMin = -1f, float myMax = -1f)
		{
			this.Label = label;
			this.Min = GlobalMin;
			this.Max = GlobalMax;
			this.MyMin = GlobalMin;
			this.MyMax = GlobalMax;
			if (myMin > -1f)
			{
				this.MyMin = myMin;
			}
			if (myMax > -1f)
			{
				this.MyMax = myMax;
			}
		}

		// Token: 0x06002433 RID: 9267 RVA: 0x000CD4D8 File Offset: 0x000CB6D8
		public void Add()
		{
			this.Count++;
		}

		// Token: 0x06002434 RID: 9268 RVA: 0x000CD4E8 File Offset: 0x000CB6E8
		public bool ValueFits(float value)
		{
			return value >= this.Min && value <= this.Max;
		}

		// Token: 0x06002435 RID: 9269 RVA: 0x000CD501 File Offset: 0x000CB701
		public bool MyValueFits(float value)
		{
			return value >= this.MyMin && value <= this.MyMax;
		}

		// Token: 0x04002680 RID: 9856
		public string Label;

		// Token: 0x04002681 RID: 9857
		public float Min;

		// Token: 0x04002682 RID: 9858
		public float Max;

		// Token: 0x04002683 RID: 9859
		public float MyMin;

		// Token: 0x04002684 RID: 9860
		public float MyMax;

		// Token: 0x04002685 RID: 9861
		public int Count;
	}

	// Token: 0x02000543 RID: 1347
	private class TomeStatData
	{
		// Token: 0x06002436 RID: 9270 RVA: 0x000CD51C File Offset: 0x000CB71C
		public TomeStatData(JSONNode data)
		{
			this.TomeID = data.GetValueOrDefault("TomeID", "");
			if (data.HasKey("Bindings"))
			{
				JSONNode jsonnode = data["Bindings"];
				List<int> list = new List<int>();
				foreach (KeyValuePair<string, JSONNode> keyValuePair in jsonnode)
				{
					int item;
					int.TryParse(keyValuePair.Key, out item);
					list.Add(item);
				}
				list.Sort();
				foreach (int key in list)
				{
					this.Bindings.Add(key, jsonnode[key.ToString()].AsInt);
				}
			}
			if (data.HasKey("Timers"))
			{
				JSONNode jsonnode2 = data["Timers"];
				List<int> list2 = new List<int>();
				foreach (KeyValuePair<string, JSONNode> keyValuePair2 in jsonnode2)
				{
					int item2;
					int.TryParse(keyValuePair2.Key, out item2);
					if (!list2.Contains(item2))
					{
						list2.Add(item2);
					}
				}
				list2.Sort();
				foreach (int key2 in list2)
				{
					this.Timers.Add(key2, jsonnode2[key2.ToString()].AsInt);
				}
			}
			if (data.HasKey("Appendix"))
			{
				JSONNode jsonnode3 = data["Appendix"];
				List<int> list3 = new List<int>();
				foreach (KeyValuePair<string, JSONNode> keyValuePair3 in jsonnode3)
				{
					int item3;
					int.TryParse(keyValuePair3.Key, out item3);
					list3.Add(item3);
				}
				list3.Sort();
				foreach (int key3 in list3)
				{
					this.Appendix.Add(key3, jsonnode3[key3.ToString()].AsInt);
				}
			}
			foreach (KeyValuePair<int, int> keyValuePair4 in this.Bindings)
			{
				this.Total += keyValuePair4.Value;
			}
		}

		// Token: 0x04002686 RID: 9862
		public string TomeID;

		// Token: 0x04002687 RID: 9863
		public int Total;

		// Token: 0x04002688 RID: 9864
		public Dictionary<int, int> Bindings = new Dictionary<int, int>();

		// Token: 0x04002689 RID: 9865
		public Dictionary<int, int> Timers = new Dictionary<int, int>();

		// Token: 0x0400268A RID: 9866
		public Dictionary<int, int> Appendix = new Dictionary<int, int>();
	}

	// Token: 0x02000544 RID: 1348
	[CompilerGenerated]
	private sealed class <>c__DisplayClass19_0
	{
		// Token: 0x06002437 RID: 9271 RVA: 0x000CD7F0 File Offset: 0x000CB9F0
		public <>c__DisplayClass19_0()
		{
		}

		// Token: 0x06002438 RID: 9272 RVA: 0x000CD7F8 File Offset: 0x000CB9F8
		internal void <SetupPips>b__0()
		{
			this.<>4__this.ShowStats(this.stat);
		}

		// Token: 0x0400268B RID: 9867
		public Codex_GlobalStatsDisplay.ComparisonType stat;

		// Token: 0x0400268C RID: 9868
		public Codex_GlobalStatsDisplay <>4__this;
	}

	// Token: 0x02000545 RID: 1349
	[CompilerGenerated]
	private sealed class <FetchTomeStats>d__29 : IEnumerator<object>, IEnumerator, IDisposable
	{
		// Token: 0x06002439 RID: 9273 RVA: 0x000CD80B File Offset: 0x000CBA0B
		[DebuggerHidden]
		public <FetchTomeStats>d__29(int <>1__state)
		{
			this.<>1__state = <>1__state;
		}

		// Token: 0x0600243A RID: 9274 RVA: 0x000CD81C File Offset: 0x000CBA1C
		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			int num = this.<>1__state;
			if (num == -3 || num == 1)
			{
				try
				{
				}
				finally
				{
					this.<>m__Finally1();
				}
			}
		}

		// Token: 0x0600243B RID: 9275 RVA: 0x000CD854 File Offset: 0x000CBA54
		bool IEnumerator.MoveNext()
		{
			bool result2;
			try
			{
				int num = this.<>1__state;
				Codex_GlobalStatsDisplay codex_GlobalStatsDisplay = this;
				if (num != 0)
				{
					if (num != 1)
					{
						return false;
					}
					this.<>1__state = -3;
					goto IL_D5;
				}
				else
				{
					this.<>1__state = -1;
					if (codex_GlobalStatsDisplay.isFetching)
					{
						return false;
					}
					codex_GlobalStatsDisplay.isFetching = true;
					List<UnlockDB.GenreUnlock> genres = UnlockDB.DB.Genres;
					enumerator = genres.GetEnumerator();
					this.<>1__state = -3;
				}
				IL_130:
				while (enumerator.MoveNext())
				{
					UnlockDB.GenreUnlock genreUnlock = enumerator.Current;
					tomeID = genreUnlock.Genre.ID;
					if (!codex_GlobalStatsDisplay.tomeStats.ContainsKey(tomeID))
					{
						task = ParseManager.FetchTomeStats(tomeID);
						safetyTime = 2.5f;
						goto IL_D5;
					}
				}
				this.<>m__Finally1();
				enumerator = default(List<UnlockDB.GenreUnlock>.Enumerator);
				UnityEngine.Debug.Log("Fetched Global Stats for " + codex_GlobalStatsDisplay.tomeStats.Count.ToString() + " Tome(s)");
				codex_GlobalStatsDisplay.isFetching = false;
				return false;
				IL_D5:
				if (safetyTime <= 0f || task.IsCompleted)
				{
					JSONNode result = task.Result;
					if (result != null)
					{
						Codex_GlobalStatsDisplay.TomeStatData value = new Codex_GlobalStatsDisplay.TomeStatData(result);
						codex_GlobalStatsDisplay.tomeStats.Add(tomeID, value);
					}
					tomeID = null;
					task = null;
					goto IL_130;
				}
				this.<>2__current = true;
				this.<>1__state = 1;
				result2 = true;
			}
			catch
			{
				this.System.IDisposable.Dispose();
				throw;
			}
			return result2;
		}

		// Token: 0x0600243C RID: 9276 RVA: 0x000CDA0C File Offset: 0x000CBC0C
		private void <>m__Finally1()
		{
			this.<>1__state = -1;
			((IDisposable)enumerator).Dispose();
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x000CDA26 File Offset: 0x000CBC26
		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x000CDA2E File Offset: 0x000CBC2E
		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x170002E1 RID: 737
		// (get) Token: 0x0600243F RID: 9279 RVA: 0x000CDA35 File Offset: 0x000CBC35
		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return this.<>2__current;
			}
		}

		// Token: 0x0400268D RID: 9869
		private int <>1__state;

		// Token: 0x0400268E RID: 9870
		private object <>2__current;

		// Token: 0x0400268F RID: 9871
		public Codex_GlobalStatsDisplay <>4__this;

		// Token: 0x04002690 RID: 9872
		private List<UnlockDB.GenreUnlock>.Enumerator <>7__wrap1;

		// Token: 0x04002691 RID: 9873
		private string <tomeID>5__3;

		// Token: 0x04002692 RID: 9874
		private Task<JSONNode> <task>5__4;

		// Token: 0x04002693 RID: 9875
		private float <safetyTime>5__5;
	}
}

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000017 RID: 23
	public class Leaderboard : ILeaderboard
	{
		// Token: 0x060000B8 RID: 184 RVA: 0x000034F0 File Offset: 0x000016F0
		public Leaderboard()
		{
			this.id = "Invalid";
			this.range = new Range(1, 10);
			this.userScope = UserScope.Global;
			this.timeScope = TimeScope.AllTime;
			this.m_Loading = false;
			this.m_LocalUserScore = new Score("Invalid", 0L);
			this.m_MaxRange = 0U;
			IScore[] scores = new Score[0];
			this.m_Scores = scores;
			this.m_Title = "Invalid";
			this.m_UserIDs = new string[0];
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00003575 File Offset: 0x00001775
		public void SetUserFilter(string[] userIDs)
		{
			this.m_UserIDs = userIDs;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00003580 File Offset: 0x00001780
		public override string ToString()
		{
			string[] array = new string[20];
			array[0] = "ID: '";
			array[1] = this.id;
			array[2] = "' Title: '";
			array[3] = this.m_Title;
			array[4] = "' Loading: '";
			array[5] = this.m_Loading.ToString();
			array[6] = "' Range: [";
			int num = 7;
			Range range = this.range;
			array[num] = range.from.ToString();
			array[8] = ",";
			int num2 = 9;
			range = this.range;
			array[num2] = range.count.ToString();
			array[10] = "] MaxRange: '";
			array[11] = this.m_MaxRange.ToString();
			array[12] = "' Scores: '";
			array[13] = this.m_Scores.Length.ToString();
			array[14] = "' UserScope: '";
			array[15] = this.userScope.ToString();
			array[16] = "' TimeScope: '";
			array[17] = this.timeScope.ToString();
			array[18] = "' UserFilter: '";
			array[19] = this.m_UserIDs.Length.ToString();
			return string.Concat(array);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000036A9 File Offset: 0x000018A9
		public void LoadScores(Action<bool> callback)
		{
			ActivePlatform.Instance.LoadScores(this, callback);
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000BC RID: 188 RVA: 0x000036BC File Offset: 0x000018BC
		public bool loading
		{
			get
			{
				return ActivePlatform.Instance.GetLoading(this);
			}
		}

		// Token: 0x060000BD RID: 189 RVA: 0x000036D9 File Offset: 0x000018D9
		public void SetLocalUserScore(IScore score)
		{
			this.m_LocalUserScore = score;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000036E3 File Offset: 0x000018E3
		public void SetMaxRange(uint maxRange)
		{
			this.m_MaxRange = maxRange;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000036ED File Offset: 0x000018ED
		public void SetScores(IScore[] scores)
		{
			this.m_Scores = scores;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000036F7 File Offset: 0x000018F7
		public void SetTitle(string title)
		{
			this.m_Title = title;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00003704 File Offset: 0x00001904
		public string[] GetUserFilter()
		{
			return this.m_UserIDs;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000371C File Offset: 0x0000191C
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003724 File Offset: 0x00001924
		public string id
		{
			[CompilerGenerated]
			get
			{
				return this.<id>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<id>k__BackingField = value;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000372D File Offset: 0x0000192D
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003735 File Offset: 0x00001935
		public UserScope userScope
		{
			[CompilerGenerated]
			get
			{
				return this.<userScope>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<userScope>k__BackingField = value;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x0000373E File Offset: 0x0000193E
		// (set) Token: 0x060000C7 RID: 199 RVA: 0x00003746 File Offset: 0x00001946
		public Range range
		{
			[CompilerGenerated]
			get
			{
				return this.<range>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<range>k__BackingField = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x0000374F File Offset: 0x0000194F
		// (set) Token: 0x060000C9 RID: 201 RVA: 0x00003757 File Offset: 0x00001957
		public TimeScope timeScope
		{
			[CompilerGenerated]
			get
			{
				return this.<timeScope>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<timeScope>k__BackingField = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00003760 File Offset: 0x00001960
		public IScore localUserScore
		{
			get
			{
				return this.m_LocalUserScore;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00003778 File Offset: 0x00001978
		public uint maxRange
		{
			get
			{
				return this.m_MaxRange;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00003790 File Offset: 0x00001990
		public IScore[] scores
		{
			get
			{
				return this.m_Scores;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000CD RID: 205 RVA: 0x000037A8 File Offset: 0x000019A8
		public string title
		{
			get
			{
				return this.m_Title;
			}
		}

		// Token: 0x04000038 RID: 56
		private bool m_Loading;

		// Token: 0x04000039 RID: 57
		private IScore m_LocalUserScore;

		// Token: 0x0400003A RID: 58
		private uint m_MaxRange;

		// Token: 0x0400003B RID: 59
		private IScore[] m_Scores;

		// Token: 0x0400003C RID: 60
		private string m_Title;

		// Token: 0x0400003D RID: 61
		private string[] m_UserIDs;

		// Token: 0x0400003E RID: 62
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <id>k__BackingField;

		// Token: 0x0400003F RID: 63
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private UserScope <userScope>k__BackingField;

		// Token: 0x04000040 RID: 64
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Range <range>k__BackingField;

		// Token: 0x04000041 RID: 65
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TimeScope <timeScope>k__BackingField;
	}
}

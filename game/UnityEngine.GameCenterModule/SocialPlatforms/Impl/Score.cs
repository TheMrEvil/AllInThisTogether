using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000016 RID: 22
	public class Score : IScore
	{
		// Token: 0x060000A7 RID: 167 RVA: 0x0000333C File Offset: 0x0000153C
		public Score() : this("unkown", -1L)
		{
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x0000334D File Offset: 0x0000154D
		public Score(string leaderboardID, long value) : this(leaderboardID, value, "0", DateTime.Now, "", -1)
		{
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00003369 File Offset: 0x00001569
		public Score(string leaderboardID, long value, string userID, DateTime date, string formattedValue, int rank)
		{
			this.leaderboardID = leaderboardID;
			this.value = value;
			this.m_UserID = userID;
			this.m_Date = date;
			this.m_FormattedValue = formattedValue;
			this.m_Rank = rank;
		}

		// Token: 0x060000AA RID: 170 RVA: 0x000033A4 File Offset: 0x000015A4
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"Rank: '",
				this.m_Rank.ToString(),
				"' Value: '",
				this.value.ToString(),
				"' Category: '",
				this.leaderboardID,
				"' PlayerID: '",
				this.m_UserID,
				"' Date: '",
				this.m_Date.ToString()
			});
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000342A File Offset: 0x0000162A
		public void ReportScore(Action<bool> callback)
		{
			ActivePlatform.Instance.ReportScore(this.value, this.leaderboardID, callback);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003445 File Offset: 0x00001645
		public void SetDate(DateTime date)
		{
			this.m_Date = date;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x0000344F File Offset: 0x0000164F
		public void SetFormattedValue(string value)
		{
			this.m_FormattedValue = value;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003459 File Offset: 0x00001659
		public void SetUserID(string userID)
		{
			this.m_UserID = userID;
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003463 File Offset: 0x00001663
		public void SetRank(int rank)
		{
			this.m_Rank = rank;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x0000346D File Offset: 0x0000166D
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00003475 File Offset: 0x00001675
		public string leaderboardID
		{
			[CompilerGenerated]
			get
			{
				return this.<leaderboardID>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<leaderboardID>k__BackingField = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x0000347E File Offset: 0x0000167E
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00003486 File Offset: 0x00001686
		public long value
		{
			[CompilerGenerated]
			get
			{
				return this.<value>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<value>k__BackingField = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00003490 File Offset: 0x00001690
		public DateTime date
		{
			get
			{
				return this.m_Date;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x000034A8 File Offset: 0x000016A8
		public string formattedValue
		{
			get
			{
				return this.m_FormattedValue;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000034C0 File Offset: 0x000016C0
		public string userID
		{
			get
			{
				return this.m_UserID;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x000034D8 File Offset: 0x000016D8
		public int rank
		{
			get
			{
				return this.m_Rank;
			}
		}

		// Token: 0x04000032 RID: 50
		private DateTime m_Date;

		// Token: 0x04000033 RID: 51
		private string m_FormattedValue;

		// Token: 0x04000034 RID: 52
		private string m_UserID;

		// Token: 0x04000035 RID: 53
		private int m_Rank;

		// Token: 0x04000036 RID: 54
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <leaderboardID>k__BackingField;

		// Token: 0x04000037 RID: 55
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private long <value>k__BackingField;
	}
}

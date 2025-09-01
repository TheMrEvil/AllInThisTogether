using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.SocialPlatforms.Impl
{
	// Token: 0x02000014 RID: 20
	public class Achievement : IAchievement
	{
		// Token: 0x0600008D RID: 141 RVA: 0x00003014 File Offset: 0x00001214
		public Achievement(string id, double percentCompleted, bool completed, bool hidden, DateTime lastReportedDate)
		{
			this.id = id;
			this.percentCompleted = percentCompleted;
			this.m_Completed = completed;
			this.m_Hidden = hidden;
			this.m_LastReportedDate = lastReportedDate;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003045 File Offset: 0x00001245
		public Achievement(string id, double percent)
		{
			this.id = id;
			this.percentCompleted = percent;
			this.m_Hidden = false;
			this.m_Completed = false;
			this.m_LastReportedDate = DateTime.MinValue;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003078 File Offset: 0x00001278
		public Achievement() : this("unknown", 0.0)
		{
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003090 File Offset: 0x00001290
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				this.id,
				" - ",
				this.percentCompleted.ToString(),
				" - ",
				this.completed.ToString(),
				" - ",
				this.hidden.ToString(),
				" - ",
				this.lastReportedDate.ToString()
			});
		}

		// Token: 0x06000091 RID: 145 RVA: 0x0000311B File Offset: 0x0000131B
		public void ReportProgress(Action<bool> callback)
		{
			ActivePlatform.Instance.ReportProgress(this.id, this.percentCompleted, callback);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003136 File Offset: 0x00001336
		public void SetCompleted(bool value)
		{
			this.m_Completed = value;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003140 File Offset: 0x00001340
		public void SetHidden(bool value)
		{
			this.m_Hidden = value;
		}

		// Token: 0x06000094 RID: 148 RVA: 0x0000314A File Offset: 0x0000134A
		public void SetLastReportedDate(DateTime date)
		{
			this.m_LastReportedDate = date;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x06000095 RID: 149 RVA: 0x00003154 File Offset: 0x00001354
		// (set) Token: 0x06000096 RID: 150 RVA: 0x0000315C File Offset: 0x0000135C
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

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00003165 File Offset: 0x00001365
		// (set) Token: 0x06000098 RID: 152 RVA: 0x0000316D File Offset: 0x0000136D
		public double percentCompleted
		{
			[CompilerGenerated]
			get
			{
				return this.<percentCompleted>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<percentCompleted>k__BackingField = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00003178 File Offset: 0x00001378
		public bool completed
		{
			get
			{
				return this.m_Completed;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600009A RID: 154 RVA: 0x00003190 File Offset: 0x00001390
		public bool hidden
		{
			get
			{
				return this.m_Hidden;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000031A8 File Offset: 0x000013A8
		public DateTime lastReportedDate
		{
			get
			{
				return this.m_LastReportedDate;
			}
		}

		// Token: 0x04000026 RID: 38
		private bool m_Completed;

		// Token: 0x04000027 RID: 39
		private bool m_Hidden;

		// Token: 0x04000028 RID: 40
		private DateTime m_LastReportedDate;

		// Token: 0x04000029 RID: 41
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <id>k__BackingField;

		// Token: 0x0400002A RID: 42
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private double <percentCompleted>k__BackingField;
	}
}

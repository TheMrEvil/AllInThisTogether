using System;

namespace Steamworks.Data
{
	// Token: 0x020001F4 RID: 500
	public struct DurationControl
	{
		// Token: 0x170002B8 RID: 696
		// (get) Token: 0x06000FC4 RID: 4036 RVA: 0x00019AA0 File Offset: 0x00017CA0
		public AppId Appid
		{
			get
			{
				return this._inner.Appid;
			}
		}

		// Token: 0x170002B9 RID: 697
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00019AAD File Offset: 0x00017CAD
		public bool Applicable
		{
			get
			{
				return this._inner.Applicable;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000FC6 RID: 4038 RVA: 0x00019ABA File Offset: 0x00017CBA
		internal TimeSpan PlaytimeInLastFiveHours
		{
			get
			{
				return TimeSpan.FromSeconds((double)this._inner.CsecsLast5h);
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00019ACD File Offset: 0x00017CCD
		internal TimeSpan PlaytimeToday
		{
			get
			{
				return TimeSpan.FromSeconds((double)this._inner.CsecsLast5h);
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000FC8 RID: 4040 RVA: 0x00019AE0 File Offset: 0x00017CE0
		internal DurationControlProgress Progress
		{
			get
			{
				return this._inner.Progress;
			}
		}

		// Token: 0x04000BF3 RID: 3059
		internal DurationControl_t _inner;
	}
}

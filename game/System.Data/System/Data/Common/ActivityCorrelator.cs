using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Data.Common
{
	// Token: 0x020003C9 RID: 969
	internal static class ActivityCorrelator
	{
		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002F03 RID: 12035 RVA: 0x000C9D93 File Offset: 0x000C7F93
		internal static ActivityCorrelator.ActivityId Current
		{
			get
			{
				if (ActivityCorrelator.t_tlsActivity == null)
				{
					ActivityCorrelator.t_tlsActivity = new ActivityCorrelator.ActivityId();
				}
				return new ActivityCorrelator.ActivityId(ActivityCorrelator.t_tlsActivity);
			}
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000C9DB0 File Offset: 0x000C7FB0
		internal static ActivityCorrelator.ActivityId Next()
		{
			if (ActivityCorrelator.t_tlsActivity == null)
			{
				ActivityCorrelator.t_tlsActivity = new ActivityCorrelator.ActivityId();
			}
			ActivityCorrelator.t_tlsActivity.Increment();
			return new ActivityCorrelator.ActivityId(ActivityCorrelator.t_tlsActivity);
		}

		// Token: 0x04001C03 RID: 7171
		[ThreadStatic]
		private static ActivityCorrelator.ActivityId t_tlsActivity;

		// Token: 0x020003CA RID: 970
		internal class ActivityId
		{
			// Token: 0x170007CB RID: 1995
			// (get) Token: 0x06002F05 RID: 12037 RVA: 0x000C9DD7 File Offset: 0x000C7FD7
			// (set) Token: 0x06002F06 RID: 12038 RVA: 0x000C9DDF File Offset: 0x000C7FDF
			internal Guid Id
			{
				[CompilerGenerated]
				get
				{
					return this.<Id>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Id>k__BackingField = value;
				}
			}

			// Token: 0x170007CC RID: 1996
			// (get) Token: 0x06002F07 RID: 12039 RVA: 0x000C9DE8 File Offset: 0x000C7FE8
			// (set) Token: 0x06002F08 RID: 12040 RVA: 0x000C9DF0 File Offset: 0x000C7FF0
			internal uint Sequence
			{
				[CompilerGenerated]
				get
				{
					return this.<Sequence>k__BackingField;
				}
				[CompilerGenerated]
				private set
				{
					this.<Sequence>k__BackingField = value;
				}
			}

			// Token: 0x06002F09 RID: 12041 RVA: 0x000C9DF9 File Offset: 0x000C7FF9
			internal ActivityId()
			{
				this.Id = Guid.NewGuid();
				this.Sequence = 0U;
			}

			// Token: 0x06002F0A RID: 12042 RVA: 0x000C9E13 File Offset: 0x000C8013
			internal ActivityId(ActivityCorrelator.ActivityId activity)
			{
				this.Id = activity.Id;
				this.Sequence = activity.Sequence;
			}

			// Token: 0x06002F0B RID: 12043 RVA: 0x000C9E34 File Offset: 0x000C8034
			internal void Increment()
			{
				uint sequence = this.Sequence + 1U;
				this.Sequence = sequence;
			}

			// Token: 0x06002F0C RID: 12044 RVA: 0x000C9E51 File Offset: 0x000C8051
			public override string ToString()
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}:{1}", this.Id, this.Sequence);
			}

			// Token: 0x04001C04 RID: 7172
			[CompilerGenerated]
			private Guid <Id>k__BackingField;

			// Token: 0x04001C05 RID: 7173
			[CompilerGenerated]
			private uint <Sequence>k__BackingField;
		}
	}
}

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.SearchService
{
	// Token: 0x020002D4 RID: 724
	[AttributeUsage(AttributeTargets.Field)]
	[Obsolete("ObjectSelectorHandlerWithLabelsAttribute has been deprecated. Use SearchContextAttribute instead.")]
	public class ObjectSelectorHandlerWithLabelsAttribute : Attribute
	{
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00030C5F File Offset: 0x0002EE5F
		public string[] labels
		{
			[CompilerGenerated]
			get
			{
				return this.<labels>k__BackingField;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x06001DF1 RID: 7665 RVA: 0x00030C67 File Offset: 0x0002EE67
		public bool matchAll
		{
			[CompilerGenerated]
			get
			{
				return this.<matchAll>k__BackingField;
			}
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x00030C6F File Offset: 0x0002EE6F
		public ObjectSelectorHandlerWithLabelsAttribute(params string[] labels)
		{
			this.labels = labels;
			this.matchAll = 1;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x00030C87 File Offset: 0x0002EE87
		public ObjectSelectorHandlerWithLabelsAttribute(bool matchAll, params string[] labels)
		{
			this.labels = labels;
			this.matchAll = matchAll;
		}

		// Token: 0x040009CF RID: 2511
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string[] <labels>k__BackingField;

		// Token: 0x040009D0 RID: 2512
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly bool <matchAll>k__BackingField;
	}
}

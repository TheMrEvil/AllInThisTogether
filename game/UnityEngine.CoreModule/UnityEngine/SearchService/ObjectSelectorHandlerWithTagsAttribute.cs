using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.SearchService
{
	// Token: 0x020002D5 RID: 725
	[AttributeUsage(AttributeTargets.Field)]
	[Obsolete("ObjectSelectorHandlerWithTagsAttribute has been deprecated. Use SearchContextAttribute instead.")]
	public class ObjectSelectorHandlerWithTagsAttribute : Attribute
	{
		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x00030C9F File Offset: 0x0002EE9F
		public string[] tags
		{
			[CompilerGenerated]
			get
			{
				return this.<tags>k__BackingField;
			}
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x00030CA7 File Offset: 0x0002EEA7
		public ObjectSelectorHandlerWithTagsAttribute(params string[] tags)
		{
			this.tags = tags;
		}

		// Token: 0x040009D1 RID: 2513
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string[] <tags>k__BackingField;
	}
}

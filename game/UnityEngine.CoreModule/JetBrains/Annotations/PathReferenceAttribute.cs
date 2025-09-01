using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000CA RID: 202
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class PathReferenceAttribute : Attribute
	{
		// Token: 0x06000368 RID: 872 RVA: 0x00002059 File Offset: 0x00000259
		public PathReferenceAttribute()
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00005E1C File Offset: 0x0000401C
		public PathReferenceAttribute([NotNull] [PathReference] string basePath)
		{
			this.BasePath = basePath;
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600036A RID: 874 RVA: 0x00005E2D File Offset: 0x0000402D
		[CanBeNull]
		public string BasePath
		{
			[CompilerGenerated]
			get
			{
				return this.<BasePath>k__BackingField;
			}
		}

		// Token: 0x0400025B RID: 603
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private readonly string <BasePath>k__BackingField;
	}
}

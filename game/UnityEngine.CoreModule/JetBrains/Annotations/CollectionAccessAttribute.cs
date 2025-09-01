using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000CD RID: 205
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class CollectionAccessAttribute : Attribute
	{
		// Token: 0x06000373 RID: 883 RVA: 0x00005E68 File Offset: 0x00004068
		public CollectionAccessAttribute(CollectionAccessType collectionAccessType)
		{
			this.CollectionAccessType = collectionAccessType;
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00005E79 File Offset: 0x00004079
		public CollectionAccessType CollectionAccessType
		{
			[CompilerGenerated]
			get
			{
				return this.<CollectionAccessType>k__BackingField;
			}
		}

		// Token: 0x0400025F RID: 607
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly CollectionAccessType <CollectionAccessType>k__BackingField;
	}
}

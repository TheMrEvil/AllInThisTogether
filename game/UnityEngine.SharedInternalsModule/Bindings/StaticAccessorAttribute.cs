using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000023 RID: 35
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property)]
	[VisibleToOtherModules]
	internal class StaticAccessorAttribute : Attribute, IBindingsAttribute
	{
		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000068 RID: 104 RVA: 0x00002545 File Offset: 0x00000745
		// (set) Token: 0x06000069 RID: 105 RVA: 0x0000254D File Offset: 0x0000074D
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600006A RID: 106 RVA: 0x00002556 File Offset: 0x00000756
		// (set) Token: 0x0600006B RID: 107 RVA: 0x0000255E File Offset: 0x0000075E
		public StaticAccessorType Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Type>k__BackingField = value;
			}
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00002078 File Offset: 0x00000278
		public StaticAccessorAttribute()
		{
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00002567 File Offset: 0x00000767
		[VisibleToOtherModules]
		internal StaticAccessorAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00002579 File Offset: 0x00000779
		public StaticAccessorAttribute(StaticAccessorType type)
		{
			this.Type = type;
		}

		// Token: 0x0600006F RID: 111 RVA: 0x0000258B File Offset: 0x0000078B
		public StaticAccessorAttribute(string name, StaticAccessorType type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x04000023 RID: 35
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <Name>k__BackingField;

		// Token: 0x04000024 RID: 36
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private StaticAccessorType <Type>k__BackingField;
	}
}

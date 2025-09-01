using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace JetBrains.Annotations
{
	// Token: 0x020000D0 RID: 208
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class AssertionConditionAttribute : Attribute
	{
		// Token: 0x06000376 RID: 886 RVA: 0x00005E81 File Offset: 0x00004081
		public AssertionConditionAttribute(AssertionConditionType conditionType)
		{
			this.ConditionType = conditionType;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000377 RID: 887 RVA: 0x00005E92 File Offset: 0x00004092
		public AssertionConditionType ConditionType
		{
			[CompilerGenerated]
			get
			{
				return this.<ConditionType>k__BackingField;
			}
		}

		// Token: 0x04000265 RID: 613
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private readonly AssertionConditionType <ConditionType>k__BackingField;
	}
}

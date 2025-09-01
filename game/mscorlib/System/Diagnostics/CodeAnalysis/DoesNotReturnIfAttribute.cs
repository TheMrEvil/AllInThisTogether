using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A0B RID: 2571
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class DoesNotReturnIfAttribute : Attribute
	{
		// Token: 0x06005B64 RID: 23396 RVA: 0x001349D4 File Offset: 0x00132BD4
		public DoesNotReturnIfAttribute(bool parameterValue)
		{
			this.ParameterValue = parameterValue;
		}

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06005B65 RID: 23397 RVA: 0x001349E3 File Offset: 0x00132BE3
		public bool ParameterValue
		{
			[CompilerGenerated]
			get
			{
				return this.<ParameterValue>k__BackingField;
			}
		}

		// Token: 0x04003855 RID: 14421
		[CompilerGenerated]
		private readonly bool <ParameterValue>k__BackingField;
	}
}

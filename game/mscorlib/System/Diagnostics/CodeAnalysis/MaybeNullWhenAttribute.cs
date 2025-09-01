using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A07 RID: 2567
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x06005B5D RID: 23389 RVA: 0x0013498F File Offset: 0x00132B8F
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000FB7 RID: 4023
		// (get) Token: 0x06005B5E RID: 23390 RVA: 0x0013499E File Offset: 0x00132B9E
		public bool ReturnValue
		{
			[CompilerGenerated]
			get
			{
				return this.<ReturnValue>k__BackingField;
			}
		}

		// Token: 0x04003852 RID: 14418
		[CompilerGenerated]
		private readonly bool <ReturnValue>k__BackingField;
	}
}

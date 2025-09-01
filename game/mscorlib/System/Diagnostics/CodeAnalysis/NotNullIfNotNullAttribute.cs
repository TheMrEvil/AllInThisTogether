using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x02000A09 RID: 2569
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true, Inherited = false)]
	public sealed class NotNullIfNotNullAttribute : Attribute
	{
		// Token: 0x06005B61 RID: 23393 RVA: 0x001349BD File Offset: 0x00132BBD
		public NotNullIfNotNullAttribute(string parameterName)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06005B62 RID: 23394 RVA: 0x001349CC File Offset: 0x00132BCC
		public string ParameterName
		{
			[CompilerGenerated]
			get
			{
				return this.<ParameterName>k__BackingField;
			}
		}

		// Token: 0x04003854 RID: 14420
		[CompilerGenerated]
		private readonly string <ParameterName>k__BackingField;
	}
}

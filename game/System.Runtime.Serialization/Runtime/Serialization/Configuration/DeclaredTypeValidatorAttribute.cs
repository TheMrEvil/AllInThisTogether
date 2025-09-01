using System;
using System.Configuration;

namespace System.Runtime.Serialization.Configuration
{
	// Token: 0x020001A4 RID: 420
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class DeclaredTypeValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0005413D File Offset: 0x0005233D
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				return new DeclaredTypeValidator();
			}
		}

		// Token: 0x06001538 RID: 5432 RVA: 0x00054144 File Offset: 0x00052344
		public DeclaredTypeValidatorAttribute()
		{
		}
	}
}

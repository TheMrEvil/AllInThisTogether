using System;

namespace Unity.Burst.CompilerServices
{
	// Token: 0x02000024 RID: 36
	[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
	public class AssumeRangeAttribute : Attribute
	{
		// Token: 0x06000133 RID: 307 RVA: 0x00007996 File Offset: 0x00005B96
		public AssumeRangeAttribute(long min, long max)
		{
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000799E File Offset: 0x00005B9E
		public AssumeRangeAttribute(ulong min, ulong max)
		{
		}
	}
}

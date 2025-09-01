using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020000A8 RID: 168
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class PreserveDependencyAttribute : Attribute
	{
		// Token: 0x06000900 RID: 2304 RVA: 0x000254FF File Offset: 0x000236FF
		public PreserveDependencyAttribute(string memberSignature)
		{
		}

		// Token: 0x06000901 RID: 2305 RVA: 0x000254FF File Offset: 0x000236FF
		public PreserveDependencyAttribute(string memberSignature, string typeName)
		{
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x000254FF File Offset: 0x000236FF
		public PreserveDependencyAttribute(string memberSignature, string typeName, string assembly)
		{
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00025507 File Offset: 0x00023707
		// (set) Token: 0x06000904 RID: 2308 RVA: 0x0002550F File Offset: 0x0002370F
		public string Condition
		{
			[CompilerGenerated]
			get
			{
				return this.<Condition>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Condition>k__BackingField = value;
			}
		}

		// Token: 0x040003E6 RID: 998
		[CompilerGenerated]
		private string <Condition>k__BackingField;
	}
}

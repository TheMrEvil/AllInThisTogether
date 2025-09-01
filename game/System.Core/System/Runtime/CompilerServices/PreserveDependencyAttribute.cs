using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020002EA RID: 746
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class PreserveDependencyAttribute : Attribute
	{
		// Token: 0x060016AA RID: 5802 RVA: 0x000023F5 File Offset: 0x000005F5
		public PreserveDependencyAttribute(string memberSignature)
		{
		}

		// Token: 0x060016AB RID: 5803 RVA: 0x000023F5 File Offset: 0x000005F5
		public PreserveDependencyAttribute(string memberSignature, string typeName)
		{
		}

		// Token: 0x060016AC RID: 5804 RVA: 0x000023F5 File Offset: 0x000005F5
		public PreserveDependencyAttribute(string memberSignature, string typeName, string assembly)
		{
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0004C7CA File Offset: 0x0004A9CA
		// (set) Token: 0x060016AE RID: 5806 RVA: 0x0004C7D2 File Offset: 0x0004A9D2
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

		// Token: 0x04000B62 RID: 2914
		[CompilerGenerated]
		private string <Condition>k__BackingField;
	}
}

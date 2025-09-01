using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000851 RID: 2129
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class PreserveDependencyAttribute : Attribute
	{
		// Token: 0x060046EC RID: 18156 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveDependencyAttribute(string memberSignature)
		{
		}

		// Token: 0x060046ED RID: 18157 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveDependencyAttribute(string memberSignature, string typeName)
		{
		}

		// Token: 0x060046EE RID: 18158 RVA: 0x00002050 File Offset: 0x00000250
		public PreserveDependencyAttribute(string memberSignature, string typeName, string assembly)
		{
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x060046EF RID: 18159 RVA: 0x000E7D96 File Offset: 0x000E5F96
		// (set) Token: 0x060046F0 RID: 18160 RVA: 0x000E7D9E File Offset: 0x000E5F9E
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

		// Token: 0x04002D9F RID: 11679
		[CompilerGenerated]
		private string <Condition>k__BackingField;
	}
}

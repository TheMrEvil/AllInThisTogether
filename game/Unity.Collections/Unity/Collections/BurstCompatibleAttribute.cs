using System;
using System.Runtime.CompilerServices;

namespace Unity.Collections
{
	// Token: 0x0200003A RID: 58
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = true)]
	public class BurstCompatibleAttribute : Attribute
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000041CE File Offset: 0x000023CE
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000041D6 File Offset: 0x000023D6
		public Type[] GenericTypeArguments
		{
			[CompilerGenerated]
			get
			{
				return this.<GenericTypeArguments>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<GenericTypeArguments>k__BackingField = value;
			}
		}

		// Token: 0x0600010D RID: 269 RVA: 0x00002050 File Offset: 0x00000250
		public BurstCompatibleAttribute()
		{
		}

		// Token: 0x04000072 RID: 114
		[CompilerGenerated]
		private Type[] <GenericTypeArguments>k__BackingField;

		// Token: 0x04000073 RID: 115
		public string RequiredUnityDefine;

		// Token: 0x04000074 RID: 116
		public BurstCompatibleAttribute.BurstCompatibleCompileTarget CompileTarget;

		// Token: 0x0200003B RID: 59
		public enum BurstCompatibleCompileTarget
		{
			// Token: 0x04000076 RID: 118
			Player,
			// Token: 0x04000077 RID: 119
			Editor,
			// Token: 0x04000078 RID: 120
			PlayerAndEditor
		}
	}
}

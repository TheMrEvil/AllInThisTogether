using System;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;

namespace Mono.CSharp
{
	// Token: 0x02000135 RID: 309
	public class ConditionalAccessContext
	{
		// Token: 0x06000FBC RID: 4028 RVA: 0x000408E0 File Offset: 0x0003EAE0
		public ConditionalAccessContext(TypeSpec type, Label endLabel)
		{
			this.Type = type;
			this.EndLabel = endLabel;
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x000408F6 File Offset: 0x0003EAF6
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x000408FE File Offset: 0x0003EAFE
		public bool Statement
		{
			[CompilerGenerated]
			get
			{
				return this.<Statement>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Statement>k__BackingField = value;
			}
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00040907 File Offset: 0x0003EB07
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x0004090F File Offset: 0x0003EB0F
		public Label EndLabel
		{
			[CompilerGenerated]
			get
			{
				return this.<EndLabel>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<EndLabel>k__BackingField = value;
			}
		}

		// Token: 0x17000460 RID: 1120
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00040918 File Offset: 0x0003EB18
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00040920 File Offset: 0x0003EB20
		public TypeSpec Type
		{
			[CompilerGenerated]
			get
			{
				return this.<Type>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Type>k__BackingField = value;
			}
		}

		// Token: 0x04000720 RID: 1824
		[CompilerGenerated]
		private bool <Statement>k__BackingField;

		// Token: 0x04000721 RID: 1825
		[CompilerGenerated]
		private Label <EndLabel>k__BackingField;

		// Token: 0x04000722 RID: 1826
		[CompilerGenerated]
		private TypeSpec <Type>k__BackingField;
	}
}

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000007 RID: 7
	[IncludeMyAttributes]
	[ShowInInspector]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class ButtonGroupAttribute : PropertyGroupAttribute
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002403 File Offset: 0x00000603
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000240B File Offset: 0x0000060B
		public IconAlignment IconAlignment
		{
			get
			{
				return this.buttonIconAlignment;
			}
			set
			{
				this.buttonIconAlignment = value;
				this.HasDefinedButtonIconAlignment = true;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600002E RID: 46 RVA: 0x0000241B File Offset: 0x0000061B
		// (set) Token: 0x0600002F RID: 47 RVA: 0x00002423 File Offset: 0x00000623
		public int ButtonAlignment
		{
			get
			{
				return this.buttonAlignment;
			}
			set
			{
				this.buttonAlignment = value;
				this.HasDefinedButtonAlignment = true;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002433 File Offset: 0x00000633
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000243B File Offset: 0x0000063B
		public bool Stretch
		{
			get
			{
				return this.stretch;
			}
			set
			{
				this.stretch = value;
				this.HasDefinedStretch = true;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000032 RID: 50 RVA: 0x0000244B File Offset: 0x0000064B
		// (set) Token: 0x06000033 RID: 51 RVA: 0x00002453 File Offset: 0x00000653
		public bool HasDefinedButtonIconAlignment
		{
			[CompilerGenerated]
			get
			{
				return this.<HasDefinedButtonIconAlignment>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasDefinedButtonIconAlignment>k__BackingField = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000245C File Offset: 0x0000065C
		// (set) Token: 0x06000035 RID: 53 RVA: 0x00002464 File Offset: 0x00000664
		public bool HasDefinedButtonAlignment
		{
			[CompilerGenerated]
			get
			{
				return this.<HasDefinedButtonAlignment>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasDefinedButtonAlignment>k__BackingField = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000036 RID: 54 RVA: 0x0000246D File Offset: 0x0000066D
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002475 File Offset: 0x00000675
		public bool HasDefinedStretch
		{
			[CompilerGenerated]
			get
			{
				return this.<HasDefinedStretch>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasDefinedStretch>k__BackingField = value;
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x0000247E File Offset: 0x0000067E
		public ButtonGroupAttribute(string group = "_DefaultGroup", float order = 0f) : base(group, order)
		{
		}

		// Token: 0x04000025 RID: 37
		public int ButtonHeight;

		// Token: 0x04000026 RID: 38
		[CompilerGenerated]
		private bool <HasDefinedButtonIconAlignment>k__BackingField;

		// Token: 0x04000027 RID: 39
		[CompilerGenerated]
		private bool <HasDefinedButtonAlignment>k__BackingField;

		// Token: 0x04000028 RID: 40
		[CompilerGenerated]
		private bool <HasDefinedStretch>k__BackingField;

		// Token: 0x04000029 RID: 41
		private IconAlignment buttonIconAlignment;

		// Token: 0x0400002A RID: 42
		private int buttonAlignment;

		// Token: 0x0400002B RID: 43
		private bool stretch;
	}
}

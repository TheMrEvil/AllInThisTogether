using System;
using System.Runtime.CompilerServices;

namespace Parse
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
	public sealed class ParseClassNameAttribute : Attribute
	{
		// Token: 0x06000013 RID: 19 RVA: 0x0000229E File Offset: 0x0000049E
		public ParseClassNameAttribute(string className)
		{
			this.ClassName = className;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000014 RID: 20 RVA: 0x000022AD File Offset: 0x000004AD
		// (set) Token: 0x06000015 RID: 21 RVA: 0x000022B5 File Offset: 0x000004B5
		public string ClassName
		{
			[CompilerGenerated]
			get
			{
				return this.<ClassName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<ClassName>k__BackingField = value;
			}
		}

		// Token: 0x04000005 RID: 5
		[CompilerGenerated]
		private string <ClassName>k__BackingField;
	}
}

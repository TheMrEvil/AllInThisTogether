using System;
using System.Runtime.CompilerServices;

namespace Parse
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
	public sealed class ParseFieldNameAttribute : Attribute
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000022BE File Offset: 0x000004BE
		public ParseFieldNameAttribute(string fieldName)
		{
			this.FieldName = fieldName;
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022CD File Offset: 0x000004CD
		// (set) Token: 0x06000018 RID: 24 RVA: 0x000022D5 File Offset: 0x000004D5
		public string FieldName
		{
			[CompilerGenerated]
			get
			{
				return this.<FieldName>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<FieldName>k__BackingField = value;
			}
		}

		// Token: 0x04000006 RID: 6
		[CompilerGenerated]
		private string <FieldName>k__BackingField;
	}
}

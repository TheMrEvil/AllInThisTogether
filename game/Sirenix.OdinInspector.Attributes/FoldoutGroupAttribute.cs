using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000028 RID: 40
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class FoldoutGroupAttribute : PropertyGroupAttribute
	{
		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000072 RID: 114 RVA: 0x0000276A File Offset: 0x0000096A
		// (set) Token: 0x06000073 RID: 115 RVA: 0x00002772 File Offset: 0x00000972
		public bool Expanded
		{
			get
			{
				return this.expanded;
			}
			set
			{
				this.expanded = value;
				this.HasDefinedExpanded = true;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00002782 File Offset: 0x00000982
		// (set) Token: 0x06000075 RID: 117 RVA: 0x0000278A File Offset: 0x0000098A
		public bool HasDefinedExpanded
		{
			[CompilerGenerated]
			get
			{
				return this.<HasDefinedExpanded>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<HasDefinedExpanded>k__BackingField = value;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x0000247E File Offset: 0x0000067E
		public FoldoutGroupAttribute(string groupName, float order = 0f) : base(groupName, order)
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00002793 File Offset: 0x00000993
		public FoldoutGroupAttribute(string groupName, bool expanded, float order = 0f) : base(groupName, order)
		{
			this.expanded = expanded;
			this.HasDefinedExpanded = true;
		}

		// Token: 0x06000078 RID: 120 RVA: 0x000027AC File Offset: 0x000009AC
		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			FoldoutGroupAttribute foldoutGroupAttribute = other as FoldoutGroupAttribute;
			if (foldoutGroupAttribute.HasDefinedExpanded)
			{
				this.HasDefinedExpanded = true;
				this.Expanded = foldoutGroupAttribute.Expanded;
			}
			if (this.HasDefinedExpanded)
			{
				foldoutGroupAttribute.HasDefinedExpanded = true;
				foldoutGroupAttribute.Expanded = this.Expanded;
			}
		}

		// Token: 0x0400005D RID: 93
		private bool expanded;

		// Token: 0x0400005E RID: 94
		[CompilerGenerated]
		private bool <HasDefinedExpanded>k__BackingField;
	}
}

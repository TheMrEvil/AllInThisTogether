using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000072 RID: 114
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class ToggleGroupAttribute : PropertyGroupAttribute
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00003CBB File Offset: 0x00001EBB
		public ToggleGroupAttribute(string toggleMemberName, float order = 0f, string groupTitle = null) : base(toggleMemberName, order)
		{
			this.ToggleGroupTitle = groupTitle;
			this.CollapseOthersOnExpand = true;
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00003CD3 File Offset: 0x00001ED3
		public ToggleGroupAttribute(string toggleMemberName, string groupTitle) : this(toggleMemberName, 0f, groupTitle)
		{
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00003CBB File Offset: 0x00001EBB
		[Obsolete("Use [ToggleGroup(\"toggleMemberName\", groupTitle: \"$titleStringMemberName\")] instead")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ToggleGroupAttribute(string toggleMemberName, float order, string groupTitle, string titleStringMemberName) : base(toggleMemberName, order)
		{
			this.ToggleGroupTitle = groupTitle;
			this.CollapseOthersOnExpand = true;
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00003CE2 File Offset: 0x00001EE2
		public string ToggleMemberName
		{
			get
			{
				return this.GroupName;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600017D RID: 381 RVA: 0x00003CEA File Offset: 0x00001EEA
		// (set) Token: 0x0600017E RID: 382 RVA: 0x00003CF2 File Offset: 0x00001EF2
		[Obsolete("Add a $ infront of group title instead, i.e: \"$MyStringMember\".")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string TitleStringMemberName
		{
			[CompilerGenerated]
			get
			{
				return this.<TitleStringMemberName>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TitleStringMemberName>k__BackingField = value;
			}
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00003CFC File Offset: 0x00001EFC
		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			ToggleGroupAttribute toggleGroupAttribute = other as ToggleGroupAttribute;
			if (this.ToggleGroupTitle == null)
			{
				this.ToggleGroupTitle = toggleGroupAttribute.ToggleGroupTitle;
			}
			else if (toggleGroupAttribute.ToggleGroupTitle == null)
			{
				toggleGroupAttribute.ToggleGroupTitle = this.ToggleGroupTitle;
			}
			this.CollapseOthersOnExpand = (this.CollapseOthersOnExpand && toggleGroupAttribute.CollapseOthersOnExpand);
			toggleGroupAttribute.CollapseOthersOnExpand = this.CollapseOthersOnExpand;
		}

		// Token: 0x04000146 RID: 326
		public string ToggleGroupTitle;

		// Token: 0x04000147 RID: 327
		public bool CollapseOthersOnExpand;

		// Token: 0x04000148 RID: 328
		[CompilerGenerated]
		private string <TitleStringMemberName>k__BackingField;
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200007C RID: 124
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class ValueDropdownAttribute : Attribute
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00004028 File Offset: 0x00002228
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00004030 File Offset: 0x00002230
		[Obsolete("Use the ValuesGetter member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MemberName
		{
			get
			{
				return this.ValuesGetter;
			}
			set
			{
				this.ValuesGetter = value;
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x00004039 File Offset: 0x00002239
		public ValueDropdownAttribute(string valuesGetter)
		{
			this.NumberOfItemsBeforeEnablingSearch = 10;
			this.ValuesGetter = valuesGetter;
			this.DrawDropdownForListElements = true;
		}

		// Token: 0x0400024B RID: 587
		public string ValuesGetter;

		// Token: 0x0400024C RID: 588
		public int NumberOfItemsBeforeEnablingSearch;

		// Token: 0x0400024D RID: 589
		public bool IsUniqueList;

		// Token: 0x0400024E RID: 590
		public bool DrawDropdownForListElements;

		// Token: 0x0400024F RID: 591
		public bool DisableListAddButtonBehaviour;

		// Token: 0x04000250 RID: 592
		public bool ExcludeExistingValuesInList;

		// Token: 0x04000251 RID: 593
		public bool ExpandAllMenuItems;

		// Token: 0x04000252 RID: 594
		public bool AppendNextDrawer;

		// Token: 0x04000253 RID: 595
		public bool DisableGUIInAppendedDrawer;

		// Token: 0x04000254 RID: 596
		public bool DoubleClickToConfirm;

		// Token: 0x04000255 RID: 597
		public bool FlattenTreeView;

		// Token: 0x04000256 RID: 598
		public int DropdownWidth;

		// Token: 0x04000257 RID: 599
		public int DropdownHeight;

		// Token: 0x04000258 RID: 600
		public string DropdownTitle;

		// Token: 0x04000259 RID: 601
		public bool SortDropdownItems;

		// Token: 0x0400025A RID: 602
		public bool HideChildProperties;

		// Token: 0x0400025B RID: 603
		public bool CopyValues = true;

		// Token: 0x0400025C RID: 604
		public bool OnlyChangeValueOnConfirm;
	}
}

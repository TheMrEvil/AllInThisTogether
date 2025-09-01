using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000042 RID: 66
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	[DontApplyToListElements]
	public sealed class ListDrawerSettingsAttribute : Attribute
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002C1F File Offset: 0x00000E1F
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00002C27 File Offset: 0x00000E27
		public bool ShowPaging
		{
			get
			{
				return this.paging;
			}
			set
			{
				this.paging = value;
				this.pagingHasValue = true;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002C37 File Offset: 0x00000E37
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00002C3F File Offset: 0x00000E3F
		public bool DraggableItems
		{
			get
			{
				return this.draggable;
			}
			set
			{
				this.draggable = value;
				this.draggableHasValue = true;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002C4F File Offset: 0x00000E4F
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x00002C57 File Offset: 0x00000E57
		public int NumberOfItemsPerPage
		{
			get
			{
				return this.numberOfItemsPerPage;
			}
			set
			{
				this.numberOfItemsPerPage = value;
				this.numberOfItemsPerPageHasValue = true;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002C67 File Offset: 0x00000E67
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00002C6F File Offset: 0x00000E6F
		public bool IsReadOnly
		{
			get
			{
				return this.isReadOnly;
			}
			set
			{
				this.isReadOnly = value;
				this.isReadOnlyHasValue = true;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002C7F File Offset: 0x00000E7F
		// (set) Token: 0x060000BB RID: 187 RVA: 0x00002C87 File Offset: 0x00000E87
		public bool ShowItemCount
		{
			get
			{
				return this.showItemCount;
			}
			set
			{
				this.showItemCount = value;
				this.showItemCountHasValue = true;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002C97 File Offset: 0x00000E97
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00002CA2 File Offset: 0x00000EA2
		[Obsolete("Use ShowFoldout instead, which is what Expanded has always done. If you want to control the default expanded state, use DefaultExpandedState. Expanded has been implemented wrong for a long time.", false)]
		public bool Expanded
		{
			get
			{
				return !this.ShowFoldout;
			}
			set
			{
				this.ShowFoldout = !value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002CAE File Offset: 0x00000EAE
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00002CB6 File Offset: 0x00000EB6
		public bool DefaultExpandedState
		{
			get
			{
				return this.defaultExpandedState;
			}
			set
			{
				this.defaultExpandedStateHasValue = true;
				this.defaultExpandedState = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002CC6 File Offset: 0x00000EC6
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00002CCE File Offset: 0x00000ECE
		public bool ShowIndexLabels
		{
			get
			{
				return this.showIndexLabels;
			}
			set
			{
				this.showIndexLabels = value;
				this.showIndexLabelsHasValue = true;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002CDE File Offset: 0x00000EDE
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002CE6 File Offset: 0x00000EE6
		public string OnTitleBarGUI
		{
			get
			{
				return this.onTitleBarGUI;
			}
			set
			{
				this.onTitleBarGUI = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002CEF File Offset: 0x00000EEF
		public bool PagingHasValue
		{
			get
			{
				return this.pagingHasValue;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002CF7 File Offset: 0x00000EF7
		public bool ShowItemCountHasValue
		{
			get
			{
				return this.showItemCountHasValue;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002CFF File Offset: 0x00000EFF
		public bool NumberOfItemsPerPageHasValue
		{
			get
			{
				return this.numberOfItemsPerPageHasValue;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002D07 File Offset: 0x00000F07
		public bool DraggableHasValue
		{
			get
			{
				return this.draggableHasValue;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002D0F File Offset: 0x00000F0F
		public bool IsReadOnlyHasValue
		{
			get
			{
				return this.isReadOnlyHasValue;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002D17 File Offset: 0x00000F17
		public bool ShowIndexLabelsHasValue
		{
			get
			{
				return this.showIndexLabelsHasValue;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002D1F File Offset: 0x00000F1F
		public bool DefaultExpandedStateHasValue
		{
			get
			{
				return this.defaultExpandedStateHasValue;
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002D27 File Offset: 0x00000F27
		public ListDrawerSettingsAttribute()
		{
		}

		// Token: 0x04000093 RID: 147
		public bool HideAddButton;

		// Token: 0x04000094 RID: 148
		public bool HideRemoveButton;

		// Token: 0x04000095 RID: 149
		public string ListElementLabelName;

		// Token: 0x04000096 RID: 150
		public string CustomAddFunction;

		// Token: 0x04000097 RID: 151
		public string CustomRemoveIndexFunction;

		// Token: 0x04000098 RID: 152
		public string CustomRemoveElementFunction;

		// Token: 0x04000099 RID: 153
		public string OnBeginListElementGUI;

		// Token: 0x0400009A RID: 154
		public string OnEndListElementGUI;

		// Token: 0x0400009B RID: 155
		public bool AlwaysAddDefaultValue;

		// Token: 0x0400009C RID: 156
		public bool AddCopiesLastElement;

		// Token: 0x0400009D RID: 157
		public string ElementColor;

		// Token: 0x0400009E RID: 158
		private string onTitleBarGUI;

		// Token: 0x0400009F RID: 159
		private int numberOfItemsPerPage;

		// Token: 0x040000A0 RID: 160
		private bool paging;

		// Token: 0x040000A1 RID: 161
		private bool draggable;

		// Token: 0x040000A2 RID: 162
		private bool isReadOnly;

		// Token: 0x040000A3 RID: 163
		private bool showItemCount;

		// Token: 0x040000A4 RID: 164
		private bool pagingHasValue;

		// Token: 0x040000A5 RID: 165
		private bool draggableHasValue;

		// Token: 0x040000A6 RID: 166
		private bool isReadOnlyHasValue;

		// Token: 0x040000A7 RID: 167
		private bool showItemCountHasValue;

		// Token: 0x040000A8 RID: 168
		private bool numberOfItemsPerPageHasValue;

		// Token: 0x040000A9 RID: 169
		private bool showIndexLabels;

		// Token: 0x040000AA RID: 170
		private bool showIndexLabelsHasValue;

		// Token: 0x040000AB RID: 171
		private bool defaultExpandedStateHasValue;

		// Token: 0x040000AC RID: 172
		private bool defaultExpandedState;

		// Token: 0x040000AD RID: 173
		public bool ShowFoldout = true;
	}
}

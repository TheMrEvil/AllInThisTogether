using System;
using System.Diagnostics;
using UnityEngine;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200006D RID: 109
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false)]
	[Conditional("UNITY_EDITOR")]
	public class TableListAttribute : Attribute
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600016E RID: 366 RVA: 0x00003AEF File Offset: 0x00001CEF
		// (set) Token: 0x0600016F RID: 367 RVA: 0x00003AF7 File Offset: 0x00001CF7
		public bool ShowPaging
		{
			get
			{
				return this.showPaging;
			}
			set
			{
				this.showPaging = value;
				this.showPagingHasValue = true;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00003B07 File Offset: 0x00001D07
		public bool ShowPagingHasValue
		{
			get
			{
				return this.showPagingHasValue;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00003B0F File Offset: 0x00001D0F
		// (set) Token: 0x06000172 RID: 370 RVA: 0x00003B24 File Offset: 0x00001D24
		public int ScrollViewHeight
		{
			get
			{
				return Math.Min(this.MinScrollViewHeight, this.MaxScrollViewHeight);
			}
			set
			{
				this.MaxScrollViewHeight = value;
				this.MinScrollViewHeight = value;
			}
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00003B41 File Offset: 0x00001D41
		public TableListAttribute()
		{
		}

		// Token: 0x04000122 RID: 290
		public int NumberOfItemsPerPage;

		// Token: 0x04000123 RID: 291
		public bool IsReadOnly;

		// Token: 0x04000124 RID: 292
		public int DefaultMinColumnWidth = 40;

		// Token: 0x04000125 RID: 293
		public bool ShowIndexLabels;

		// Token: 0x04000126 RID: 294
		public bool DrawScrollView = true;

		// Token: 0x04000127 RID: 295
		public int MinScrollViewHeight = 350;

		// Token: 0x04000128 RID: 296
		public int MaxScrollViewHeight;

		// Token: 0x04000129 RID: 297
		public bool AlwaysExpanded;

		// Token: 0x0400012A RID: 298
		public bool HideToolbar;

		// Token: 0x0400012B RID: 299
		public int CellPadding = 2;

		// Token: 0x0400012C RID: 300
		[SerializeField]
		[HideInInspector]
		private bool showPagingHasValue;

		// Token: 0x0400012D RID: 301
		[SerializeField]
		[HideInInspector]
		private bool showPaging;
	}
}

using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.UIElements
{
	// Token: 0x020002F2 RID: 754
	public class UxmlValueBounds : UxmlTypeRestriction
	{
		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x060018F5 RID: 6389 RVA: 0x00065D88 File Offset: 0x00063F88
		// (set) Token: 0x060018F6 RID: 6390 RVA: 0x00065D90 File Offset: 0x00063F90
		public string min
		{
			[CompilerGenerated]
			get
			{
				return this.<min>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<min>k__BackingField = value;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x00065D99 File Offset: 0x00063F99
		// (set) Token: 0x060018F8 RID: 6392 RVA: 0x00065DA1 File Offset: 0x00063FA1
		public string max
		{
			[CompilerGenerated]
			get
			{
				return this.<max>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<max>k__BackingField = value;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060018F9 RID: 6393 RVA: 0x00065DAA File Offset: 0x00063FAA
		// (set) Token: 0x060018FA RID: 6394 RVA: 0x00065DB2 File Offset: 0x00063FB2
		public bool excludeMin
		{
			[CompilerGenerated]
			get
			{
				return this.<excludeMin>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<excludeMin>k__BackingField = value;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060018FB RID: 6395 RVA: 0x00065DBB File Offset: 0x00063FBB
		// (set) Token: 0x060018FC RID: 6396 RVA: 0x00065DC3 File Offset: 0x00063FC3
		public bool excludeMax
		{
			[CompilerGenerated]
			get
			{
				return this.<excludeMax>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<excludeMax>k__BackingField = value;
			}
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x00065DCC File Offset: 0x00063FCC
		public override bool Equals(UxmlTypeRestriction other)
		{
			UxmlValueBounds uxmlValueBounds = other as UxmlValueBounds;
			bool flag = uxmlValueBounds == null;
			return !flag && (this.min == uxmlValueBounds.min && this.max == uxmlValueBounds.max && this.excludeMin == uxmlValueBounds.excludeMin) && this.excludeMax == uxmlValueBounds.excludeMax;
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x00065D7F File Offset: 0x00063F7F
		public UxmlValueBounds()
		{
		}

		// Token: 0x04000AB4 RID: 2740
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <min>k__BackingField;

		// Token: 0x04000AB5 RID: 2741
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <max>k__BackingField;

		// Token: 0x04000AB6 RID: 2742
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <excludeMin>k__BackingField;

		// Token: 0x04000AB7 RID: 2743
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <excludeMax>k__BackingField;
	}
}

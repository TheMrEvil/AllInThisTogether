using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x0200002C RID: 44
	[Conditional("UNITY_EDITOR")]
	public class HideIfGroupAttribute : PropertyGroupAttribute
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000080 RID: 128 RVA: 0x00002861 File Offset: 0x00000A61
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002869 File Offset: 0x00000A69
		public bool Animate
		{
			get
			{
				return this.AnimateVisibility;
			}
			set
			{
				this.AnimateVisibility = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000082 RID: 130 RVA: 0x00002872 File Offset: 0x00000A72
		// (set) Token: 0x06000083 RID: 131 RVA: 0x0000287A File Offset: 0x00000A7A
		[Obsolete("Use the Condition member instead.", false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public string MemberName
		{
			get
			{
				return this.Condition;
			}
			set
			{
				this.Condition = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000084 RID: 132 RVA: 0x00002883 File Offset: 0x00000A83
		// (set) Token: 0x06000085 RID: 133 RVA: 0x0000289F File Offset: 0x00000A9F
		public string Condition
		{
			get
			{
				if (!string.IsNullOrEmpty(this.VisibleIf))
				{
					return this.VisibleIf;
				}
				return this.GroupName;
			}
			set
			{
				this.VisibleIf = value;
			}
		}

		// Token: 0x06000086 RID: 134 RVA: 0x000028A8 File Offset: 0x00000AA8
		public HideIfGroupAttribute(string path, bool animate = true) : base(path)
		{
			this.Animate = animate;
		}

		// Token: 0x06000087 RID: 135 RVA: 0x000028B8 File Offset: 0x00000AB8
		public HideIfGroupAttribute(string path, object value, bool animate = true) : base(path)
		{
			this.Value = value;
			this.Animate = animate;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000028D0 File Offset: 0x00000AD0
		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			HideIfGroupAttribute hideIfGroupAttribute = other as HideIfGroupAttribute;
			if (this.Value != null)
			{
				hideIfGroupAttribute.Value = this.Value;
			}
		}

		// Token: 0x04000064 RID: 100
		public object Value;
	}
}

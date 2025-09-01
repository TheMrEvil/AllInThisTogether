using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000062 RID: 98
	[Conditional("UNITY_EDITOR")]
	public class ShowIfGroupAttribute : PropertyGroupAttribute
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00002861 File Offset: 0x00000A61
		// (set) Token: 0x06000152 RID: 338 RVA: 0x00002869 File Offset: 0x00000A69
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

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000153 RID: 339 RVA: 0x000037B6 File Offset: 0x000019B6
		// (set) Token: 0x06000154 RID: 340 RVA: 0x000037BE File Offset: 0x000019BE
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

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000155 RID: 341 RVA: 0x00002883 File Offset: 0x00000A83
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000289F File Offset: 0x00000A9F
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

		// Token: 0x06000157 RID: 343 RVA: 0x000037C7 File Offset: 0x000019C7
		public ShowIfGroupAttribute(string path, bool animate = true) : base(path)
		{
			this.Animate = animate;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x000037D7 File Offset: 0x000019D7
		public ShowIfGroupAttribute(string path, object value, bool animate = true) : base(path)
		{
			this.Value = value;
			this.Animate = animate;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x000037F0 File Offset: 0x000019F0
		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			ShowIfGroupAttribute showIfGroupAttribute = other as ShowIfGroupAttribute;
			if (this.Value != null)
			{
				showIfGroupAttribute.Value = this.Value;
			}
		}

		// Token: 0x0400010C RID: 268
		public object Value;
	}
}

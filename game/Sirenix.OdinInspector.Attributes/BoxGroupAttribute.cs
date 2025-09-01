using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000005 RID: 5
	[AttributeUsage(AttributeTargets.All, AllowMultiple = true, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public class BoxGroupAttribute : PropertyGroupAttribute
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000210A File Offset: 0x0000030A
		public BoxGroupAttribute(string group, bool showLabel = true, bool centerLabel = false, float order = 0f) : base(group, order)
		{
			this.ShowLabel = showLabel;
			this.CenterLabel = centerLabel;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002123 File Offset: 0x00000323
		public BoxGroupAttribute() : this("_DefaultBoxGroup", false, false, 0f)
		{
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002138 File Offset: 0x00000338
		protected override void CombineValuesWith(PropertyGroupAttribute other)
		{
			BoxGroupAttribute boxGroupAttribute = other as BoxGroupAttribute;
			if (!this.ShowLabel || !boxGroupAttribute.ShowLabel)
			{
				this.ShowLabel = false;
				boxGroupAttribute.ShowLabel = false;
			}
			this.CenterLabel |= boxGroupAttribute.CenterLabel;
		}

		// Token: 0x04000012 RID: 18
		public bool ShowLabel;

		// Token: 0x04000013 RID: 19
		public bool CenterLabel;

		// Token: 0x04000014 RID: 20
		public string LabelText;
	}
}

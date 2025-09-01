using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000082 RID: 130
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class WrapAttribute : Attribute
	{
		// Token: 0x060001B0 RID: 432 RVA: 0x00004195 File Offset: 0x00002395
		public WrapAttribute(double min, double max)
		{
			this.Min = ((min < max) ? min : max);
			this.Max = ((max > min) ? max : min);
		}

		// Token: 0x04000263 RID: 611
		public double Min;

		// Token: 0x04000264 RID: 612
		public double Max;
	}
}

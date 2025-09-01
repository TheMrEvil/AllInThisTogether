using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000043 RID: 67
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class MaxValueAttribute : Attribute
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00002D36 File Offset: 0x00000F36
		public MaxValueAttribute(double maxValue)
		{
			this.MaxValue = maxValue;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002D45 File Offset: 0x00000F45
		public MaxValueAttribute(string expression)
		{
			this.Expression = expression;
		}

		// Token: 0x040000AE RID: 174
		public double MaxValue;

		// Token: 0x040000AF RID: 175
		public string Expression;
	}
}

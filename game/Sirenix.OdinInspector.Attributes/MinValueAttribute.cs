using System;
using System.Diagnostics;

namespace Sirenix.OdinInspector
{
	// Token: 0x02000045 RID: 69
	[AttributeUsage(AttributeTargets.All, AllowMultiple = false, Inherited = true)]
	[Conditional("UNITY_EDITOR")]
	public sealed class MinValueAttribute : Attribute
	{
		// Token: 0x060000D9 RID: 217 RVA: 0x00002E11 File Offset: 0x00001011
		public MinValueAttribute(double minValue)
		{
			this.MinValue = minValue;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00002E20 File Offset: 0x00001020
		public MinValueAttribute(string expression)
		{
			this.Expression = expression;
		}

		// Token: 0x040000B6 RID: 182
		public double MinValue;

		// Token: 0x040000B7 RID: 183
		public string Expression;
	}
}

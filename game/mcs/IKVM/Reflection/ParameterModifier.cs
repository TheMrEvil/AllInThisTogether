using System;

namespace IKVM.Reflection
{
	// Token: 0x02000052 RID: 82
	public struct ParameterModifier
	{
		// Token: 0x060003E3 RID: 995 RVA: 0x0000B15A File Offset: 0x0000935A
		public ParameterModifier(int parameterCount)
		{
			this.values = new bool[parameterCount];
		}

		// Token: 0x1700016D RID: 365
		public bool this[int index]
		{
			get
			{
				return this.values[index];
			}
			set
			{
				this.values[index] = value;
			}
		}

		// Token: 0x040001B6 RID: 438
		private readonly bool[] values;
	}
}

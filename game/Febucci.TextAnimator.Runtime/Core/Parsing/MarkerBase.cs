using System;
using System.Text;

namespace Febucci.UI.Core.Parsing
{
	// Token: 0x02000049 RID: 73
	public abstract class MarkerBase : IComparable<MarkerBase>
	{
		// Token: 0x0600017C RID: 380 RVA: 0x000071AC File Offset: 0x000053AC
		public MarkerBase(string name, int index, int internalOrder, string[] parameters)
		{
			this.name = name;
			this.index = index;
			this.internalOrder = internalOrder;
			this.parameters = parameters;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x000071D4 File Offset: 0x000053D4
		public int CompareTo(MarkerBase other)
		{
			return this.internalOrder.CompareTo(other.internalOrder);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x000071F8 File Offset: 0x000053F8
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.name);
			stringBuilder.Append(" internal order:");
			stringBuilder.Append(this.internalOrder);
			stringBuilder.Append(" index:");
			stringBuilder.Append(this.index);
			stringBuilder.Append('\n');
			for (int i = 0; i < this.parameters.Length; i++)
			{
				stringBuilder.Append(this.parameters[i]);
				stringBuilder.Append('\n');
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000104 RID: 260
		public readonly string name;

		// Token: 0x04000105 RID: 261
		public readonly int index;

		// Token: 0x04000106 RID: 262
		internal readonly int internalOrder;

		// Token: 0x04000107 RID: 263
		public string[] parameters;
	}
}

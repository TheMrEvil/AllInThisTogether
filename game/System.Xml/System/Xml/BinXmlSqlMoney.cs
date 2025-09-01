using System;
using System.Globalization;

namespace System.Xml
{
	// Token: 0x0200001F RID: 31
	internal struct BinXmlSqlMoney
	{
		// Token: 0x06000072 RID: 114 RVA: 0x0000459E File Offset: 0x0000279E
		public BinXmlSqlMoney(int v)
		{
			this.data = (long)v;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000045A8 File Offset: 0x000027A8
		public BinXmlSqlMoney(long v)
		{
			this.data = v;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000045B4 File Offset: 0x000027B4
		public decimal ToDecimal()
		{
			bool isNegative;
			ulong num;
			if (this.data < 0L)
			{
				isNegative = true;
				num = (ulong)(-(ulong)this.data);
			}
			else
			{
				isNegative = false;
				num = (ulong)this.data;
			}
			return new decimal((int)num, (int)(num >> 32), 0, isNegative, 4);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000045F0 File Offset: 0x000027F0
		public override string ToString()
		{
			return this.ToDecimal().ToString("#0.00##", CultureInfo.InvariantCulture);
		}

		// Token: 0x04000569 RID: 1385
		private long data;
	}
}

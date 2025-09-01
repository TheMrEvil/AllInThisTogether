using System;

namespace QFSW.QC
{
	// Token: 0x0200001E RID: 30
	public class ParserException : Exception
	{
		// Token: 0x06000068 RID: 104 RVA: 0x0000329C File Offset: 0x0000149C
		public ParserException(string message) : base(message)
		{
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000032A5 File Offset: 0x000014A5
		public ParserException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

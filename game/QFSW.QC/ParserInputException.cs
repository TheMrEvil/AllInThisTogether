using System;

namespace QFSW.QC
{
	// Token: 0x0200001F RID: 31
	public class ParserInputException : ParserException
	{
		// Token: 0x0600006A RID: 106 RVA: 0x000032AF File Offset: 0x000014AF
		public ParserInputException(string message) : base(message)
		{
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000032B8 File Offset: 0x000014B8
		public ParserInputException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200030B RID: 779
	public class MonoSymbolFileException : Exception
	{
		// Token: 0x060024C1 RID: 9409 RVA: 0x00002050 File Offset: 0x00000250
		public MonoSymbolFileException()
		{
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x0009BD8F File Offset: 0x00099F8F
		public MonoSymbolFileException(string message, params object[] args) : base(string.Format(message, args))
		{
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x00002061 File Offset: 0x00000261
		public MonoSymbolFileException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

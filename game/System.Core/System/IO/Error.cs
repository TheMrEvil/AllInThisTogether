using System;

namespace System.IO
{
	// Token: 0x02000332 RID: 818
	internal static class Error
	{
		// Token: 0x060018D0 RID: 6352 RVA: 0x000539A0 File Offset: 0x00051BA0
		internal static Exception GetEndOfFile()
		{
			return new EndOfStreamException("Unable to read beyond the end of the stream.");
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x000539AC File Offset: 0x00051BAC
		internal static Exception GetPipeNotOpen()
		{
			return new ObjectDisposedException(null, "Cannot access a closed pipe.");
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x000539B9 File Offset: 0x00051BB9
		internal static Exception GetReadNotSupported()
		{
			return new NotSupportedException("Stream does not support reading.");
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000539C5 File Offset: 0x00051BC5
		internal static Exception GetSeekNotSupported()
		{
			return new NotSupportedException("Stream does not support seeking.");
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x000539D1 File Offset: 0x00051BD1
		internal static Exception GetWriteNotSupported()
		{
			return new NotSupportedException("Stream does not support writing.");
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x000539DD File Offset: 0x00051BDD
		internal static Exception GetOperationAborted()
		{
			return new IOException("IO operation was aborted unexpectedly.");
		}
	}
}

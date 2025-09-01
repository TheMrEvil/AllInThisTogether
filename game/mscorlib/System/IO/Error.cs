using System;

namespace System.IO
{
	// Token: 0x02000B04 RID: 2820
	internal static class Error
	{
		// Token: 0x060064C0 RID: 25792 RVA: 0x0015675B File Offset: 0x0015495B
		internal static Exception GetStreamIsClosed()
		{
			return new ObjectDisposedException(null, "Cannot access a closed Stream.");
		}

		// Token: 0x060064C1 RID: 25793 RVA: 0x00156768 File Offset: 0x00154968
		internal static Exception GetEndOfFile()
		{
			return new EndOfStreamException("Unable to read beyond the end of the stream.");
		}

		// Token: 0x060064C2 RID: 25794 RVA: 0x00156774 File Offset: 0x00154974
		internal static Exception GetFileNotOpen()
		{
			return new ObjectDisposedException(null, "Cannot access a closed file.");
		}

		// Token: 0x060064C3 RID: 25795 RVA: 0x00156781 File Offset: 0x00154981
		internal static Exception GetReadNotSupported()
		{
			return new NotSupportedException("Stream does not support reading.");
		}

		// Token: 0x060064C4 RID: 25796 RVA: 0x0015678D File Offset: 0x0015498D
		internal static Exception GetSeekNotSupported()
		{
			return new NotSupportedException("Stream does not support seeking.");
		}

		// Token: 0x060064C5 RID: 25797 RVA: 0x00156799 File Offset: 0x00154999
		internal static Exception GetWriteNotSupported()
		{
			return new NotSupportedException("Stream does not support writing.");
		}
	}
}

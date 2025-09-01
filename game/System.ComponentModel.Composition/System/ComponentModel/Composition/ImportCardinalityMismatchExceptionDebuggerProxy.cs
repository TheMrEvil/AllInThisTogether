using System;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	// Token: 0x02000046 RID: 70
	internal class ImportCardinalityMismatchExceptionDebuggerProxy
	{
		// Token: 0x060001F8 RID: 504 RVA: 0x00005E40 File Offset: 0x00004040
		public ImportCardinalityMismatchExceptionDebuggerProxy(ImportCardinalityMismatchException exception)
		{
			Requires.NotNull<ImportCardinalityMismatchException>(exception, "exception");
			this._exception = exception;
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x00005E5A File Offset: 0x0000405A
		public Exception InnerException
		{
			get
			{
				return this._exception.InnerException;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001FA RID: 506 RVA: 0x00005E67 File Offset: 0x00004067
		public string Message
		{
			get
			{
				return this._exception.Message;
			}
		}

		// Token: 0x040000CB RID: 203
		private readonly ImportCardinalityMismatchException _exception;
	}
}

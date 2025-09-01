using System;
using Microsoft.Internal;

namespace System.ComponentModel.Composition.Primitives
{
	// Token: 0x0200008E RID: 142
	internal class ComposablePartExceptionDebuggerProxy
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000ADEB File Offset: 0x00008FEB
		public ComposablePartExceptionDebuggerProxy(ComposablePartException exception)
		{
			Requires.NotNull<ComposablePartException>(exception, "exception");
			this._exception = exception;
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060003C4 RID: 964 RVA: 0x0000AE05 File Offset: 0x00009005
		public ICompositionElement Element
		{
			get
			{
				return this._exception.Element;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0000AE12 File Offset: 0x00009012
		public Exception InnerException
		{
			get
			{
				return this._exception.InnerException;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x0000AE1F File Offset: 0x0000901F
		public string Message
		{
			get
			{
				return this._exception.Message;
			}
		}

		// Token: 0x04000178 RID: 376
		private readonly ComposablePartException _exception;
	}
}

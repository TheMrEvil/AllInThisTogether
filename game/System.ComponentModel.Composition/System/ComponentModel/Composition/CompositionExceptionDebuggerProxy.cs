using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Internal;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition
{
	// Token: 0x0200002D RID: 45
	internal class CompositionExceptionDebuggerProxy
	{
		// Token: 0x06000161 RID: 353 RVA: 0x000049C1 File Offset: 0x00002BC1
		public CompositionExceptionDebuggerProxy(CompositionException exception)
		{
			Requires.NotNull<CompositionException>(exception, "exception");
			this._exception = exception;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000162 RID: 354 RVA: 0x000049DC File Offset: 0x00002BDC
		public ReadOnlyCollection<Exception> Exceptions
		{
			get
			{
				List<Exception> list = new List<Exception>();
				foreach (CompositionError compositionError in this._exception.Errors)
				{
					if (compositionError.Exception != null)
					{
						list.Add(compositionError.Exception);
					}
				}
				return list.ToReadOnlyCollection<Exception>();
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00004A48 File Offset: 0x00002C48
		public string Message
		{
			get
			{
				return this._exception.Message;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000164 RID: 356 RVA: 0x00004A58 File Offset: 0x00002C58
		public ReadOnlyCollection<Exception> RootCauses
		{
			get
			{
				List<Exception> list = new List<Exception>();
				foreach (CompositionError compositionError in this._exception.Errors)
				{
					if (compositionError.Exception != null)
					{
						CompositionException ex = compositionError.Exception as CompositionException;
						if (ex != null)
						{
							CompositionExceptionDebuggerProxy compositionExceptionDebuggerProxy = new CompositionExceptionDebuggerProxy(ex);
							if (compositionExceptionDebuggerProxy.RootCauses.Count > 0)
							{
								list.AddRange(compositionExceptionDebuggerProxy.RootCauses);
								continue;
							}
						}
						list.Add(compositionError.Exception);
					}
				}
				return list.ToReadOnlyCollection<Exception>();
			}
		}

		// Token: 0x0400008F RID: 143
		private readonly CompositionException _exception;
	}
}

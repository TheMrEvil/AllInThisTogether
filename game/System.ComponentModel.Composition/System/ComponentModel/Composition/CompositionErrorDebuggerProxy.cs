using System;
using System.ComponentModel.Composition.Primitives;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	// Token: 0x02000026 RID: 38
	internal class CompositionErrorDebuggerProxy
	{
		// Token: 0x06000143 RID: 323 RVA: 0x0000449C File Offset: 0x0000269C
		public CompositionErrorDebuggerProxy(CompositionError error)
		{
			Requires.NotNull<CompositionError>(error, "error");
			this._error = error;
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000144 RID: 324 RVA: 0x000044B6 File Offset: 0x000026B6
		public string Description
		{
			get
			{
				return this._error.Description;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000145 RID: 325 RVA: 0x000044C3 File Offset: 0x000026C3
		public Exception Exception
		{
			get
			{
				return this._error.Exception;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000146 RID: 326 RVA: 0x000044D0 File Offset: 0x000026D0
		public ICompositionElement Element
		{
			get
			{
				return this._error.Element;
			}
		}

		// Token: 0x04000070 RID: 112
		private readonly CompositionError _error;
	}
}

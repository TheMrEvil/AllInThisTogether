using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition
{
	// Token: 0x0200002F RID: 47
	internal struct CompositionResult<T>
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00004BBE File Offset: 0x00002DBE
		public CompositionResult(T value)
		{
			this = new CompositionResult<T>(value, null);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00004BC8 File Offset: 0x00002DC8
		public CompositionResult(params CompositionError[] errors)
		{
			this = new CompositionResult<T>(default(T), errors);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00004BE8 File Offset: 0x00002DE8
		public CompositionResult(IEnumerable<CompositionError> errors)
		{
			this = new CompositionResult<T>(default(T), errors);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00004C05 File Offset: 0x00002E05
		internal CompositionResult(T value, IEnumerable<CompositionError> errors)
		{
			this._errors = errors;
			this._value = value;
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00004C15 File Offset: 0x00002E15
		public bool Succeeded
		{
			get
			{
				return this._errors == null || !this._errors.FastAny<CompositionError>();
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000175 RID: 373 RVA: 0x00004C2F File Offset: 0x00002E2F
		public IEnumerable<CompositionError> Errors
		{
			get
			{
				return this._errors ?? Enumerable.Empty<CompositionError>();
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00004C40 File Offset: 0x00002E40
		public T Value
		{
			get
			{
				this.ThrowOnErrors();
				return this._value;
			}
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00004C4E File Offset: 0x00002E4E
		internal CompositionResult<TValue> ToResult<TValue>()
		{
			return new CompositionResult<TValue>(this._errors);
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00004C5B File Offset: 0x00002E5B
		internal CompositionResult ToResult()
		{
			return new CompositionResult(this._errors);
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00004C68 File Offset: 0x00002E68
		private void ThrowOnErrors()
		{
			if (!this.Succeeded)
			{
				throw new CompositionException(this._errors);
			}
		}

		// Token: 0x04000092 RID: 146
		private readonly IEnumerable<CompositionError> _errors;

		// Token: 0x04000093 RID: 147
		private readonly T _value;
	}
}

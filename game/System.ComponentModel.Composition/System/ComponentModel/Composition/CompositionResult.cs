using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.Linq;
using Microsoft.Internal.Collections;

namespace System.ComponentModel.Composition
{
	// Token: 0x0200002E RID: 46
	internal struct CompositionResult
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00004AF8 File Offset: 0x00002CF8
		public CompositionResult(params CompositionError[] errors)
		{
			this = new CompositionResult(errors);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00004B01 File Offset: 0x00002D01
		public CompositionResult(IEnumerable<CompositionError> errors)
		{
			this._errors = errors;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00004B0A File Offset: 0x00002D0A
		public bool Succeeded
		{
			get
			{
				return this._errors == null || !this._errors.FastAny<CompositionError>();
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00004B24 File Offset: 0x00002D24
		public IEnumerable<CompositionError> Errors
		{
			get
			{
				return this._errors ?? Enumerable.Empty<CompositionError>();
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00004B35 File Offset: 0x00002D35
		public CompositionResult MergeResult(CompositionResult result)
		{
			if (this.Succeeded)
			{
				return result;
			}
			if (result.Succeeded)
			{
				return this;
			}
			return this.MergeErrors(result._errors);
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00004B5D File Offset: 0x00002D5D
		public CompositionResult MergeError(CompositionError error)
		{
			return this.MergeErrors(new CompositionError[]
			{
				error
			});
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00004B6F File Offset: 0x00002D6F
		public CompositionResult MergeErrors(IEnumerable<CompositionError> errors)
		{
			return new CompositionResult(this._errors.ConcatAllowingNull(errors));
		}

		// Token: 0x0600016C RID: 364 RVA: 0x00004B82 File Offset: 0x00002D82
		public CompositionResult<T> ToResult<T>(T value)
		{
			return new CompositionResult<T>(value, this._errors);
		}

		// Token: 0x0600016D RID: 365 RVA: 0x00004B90 File Offset: 0x00002D90
		public void ThrowOnErrors()
		{
			this.ThrowOnErrors(null);
		}

		// Token: 0x0600016E RID: 366 RVA: 0x00004B99 File Offset: 0x00002D99
		public void ThrowOnErrors(AtomicComposition atomicComposition)
		{
			if (this.Succeeded)
			{
				return;
			}
			if (atomicComposition == null)
			{
				throw new CompositionException(this._errors);
			}
			throw new ChangeRejectedException(this._errors);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000028FF File Offset: 0x00000AFF
		// Note: this type is marked as 'beforefieldinit'.
		static CompositionResult()
		{
		}

		// Token: 0x04000090 RID: 144
		public static readonly CompositionResult SucceededResult;

		// Token: 0x04000091 RID: 145
		private readonly IEnumerable<CompositionError> _errors;
	}
}

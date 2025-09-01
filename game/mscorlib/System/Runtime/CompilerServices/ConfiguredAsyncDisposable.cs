using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020007E4 RID: 2020
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredAsyncDisposable
	{
		// Token: 0x060045D9 RID: 17881 RVA: 0x000E535A File Offset: 0x000E355A
		internal ConfiguredAsyncDisposable(IAsyncDisposable source, bool continueOnCapturedContext)
		{
			this._source = source;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x060045DA RID: 17882 RVA: 0x000E536C File Offset: 0x000E356C
		public ConfiguredValueTaskAwaitable DisposeAsync()
		{
			return this._source.DisposeAsync().ConfigureAwait(this._continueOnCapturedContext);
		}

		// Token: 0x04002D2B RID: 11563
		private readonly IAsyncDisposable _source;

		// Token: 0x04002D2C RID: 11564
		private readonly bool _continueOnCapturedContext;
	}
}

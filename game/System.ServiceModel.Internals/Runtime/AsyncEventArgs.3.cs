using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x0200000B RID: 11
	internal class AsyncEventArgs<TArgument, TResult> : AsyncEventArgs<TArgument>
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000021 RID: 33 RVA: 0x000022C5 File Offset: 0x000004C5
		// (set) Token: 0x06000022 RID: 34 RVA: 0x000022CD File Offset: 0x000004CD
		public TResult Result
		{
			[CompilerGenerated]
			get
			{
				return this.<Result>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Result>k__BackingField = value;
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000022D6 File Offset: 0x000004D6
		public AsyncEventArgs()
		{
		}

		// Token: 0x04000043 RID: 67
		[CompilerGenerated]
		private TResult <Result>k__BackingField;
	}
}

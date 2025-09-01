using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x0200000A RID: 10
	internal class AsyncEventArgs<TArgument> : AsyncEventArgs
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600001D RID: 29 RVA: 0x0000229B File Offset: 0x0000049B
		// (set) Token: 0x0600001E RID: 30 RVA: 0x000022A3 File Offset: 0x000004A3
		public TArgument Arguments
		{
			[CompilerGenerated]
			get
			{
				return this.<Arguments>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<Arguments>k__BackingField = value;
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000022AC File Offset: 0x000004AC
		public virtual void Set(AsyncEventArgsCallback callback, TArgument arguments, object state)
		{
			base.SetAsyncState(callback, state);
			this.Arguments = arguments;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000022BD File Offset: 0x000004BD
		public AsyncEventArgs()
		{
		}

		// Token: 0x04000042 RID: 66
		[CompilerGenerated]
		private TArgument <Arguments>k__BackingField;
	}
}

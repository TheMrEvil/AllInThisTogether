using System;

namespace System.Runtime
{
	// Token: 0x0200002B RID: 43
	internal class SignalGate<T> : SignalGate
	{
		// Token: 0x06000143 RID: 323 RVA: 0x00005982 File Offset: 0x00003B82
		public SignalGate()
		{
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000598A File Offset: 0x00003B8A
		public bool Signal(T result)
		{
			this.result = result;
			return base.Signal();
		}

		// Token: 0x06000145 RID: 325 RVA: 0x00005999 File Offset: 0x00003B99
		public bool Unlock(out T result)
		{
			if (base.Unlock())
			{
				result = this.result;
				return true;
			}
			result = default(T);
			return false;
		}

		// Token: 0x040000CB RID: 203
		private T result;
	}
}

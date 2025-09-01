using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x02000389 RID: 905
	public interface IValueTaskSource
	{
		// Token: 0x06002556 RID: 9558
		ValueTaskSourceStatus GetStatus(short token);

		// Token: 0x06002557 RID: 9559
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags);

		// Token: 0x06002558 RID: 9560
		void GetResult(short token);
	}
}

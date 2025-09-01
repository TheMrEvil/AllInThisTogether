using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x0200038A RID: 906
	public interface IValueTaskSource<out TResult>
	{
		// Token: 0x06002559 RID: 9561
		ValueTaskSourceStatus GetStatus(short token);

		// Token: 0x0600255A RID: 9562
		void OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags);

		// Token: 0x0600255B RID: 9563
		TResult GetResult(short token);
	}
}

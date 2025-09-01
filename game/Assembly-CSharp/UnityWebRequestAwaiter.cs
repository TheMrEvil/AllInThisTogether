using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Networking;

// Token: 0x02000227 RID: 551
public class UnityWebRequestAwaiter : INotifyCompletion
{
	// Token: 0x06001718 RID: 5912 RVA: 0x00092654 File Offset: 0x00090854
	public UnityWebRequestAwaiter(UnityWebRequestAsyncOperation asyncOp)
	{
		this.asyncOp = asyncOp;
		asyncOp.completed += this.OnRequestCompleted;
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x06001719 RID: 5913 RVA: 0x00092675 File Offset: 0x00090875
	public bool IsCompleted
	{
		get
		{
			return this.asyncOp.isDone;
		}
	}

	// Token: 0x0600171A RID: 5914 RVA: 0x00092682 File Offset: 0x00090882
	public void GetResult()
	{
	}

	// Token: 0x0600171B RID: 5915 RVA: 0x00092684 File Offset: 0x00090884
	public void OnCompleted(Action continuation)
	{
		this.continuation = continuation;
	}

	// Token: 0x0600171C RID: 5916 RVA: 0x0009268D File Offset: 0x0009088D
	private void OnRequestCompleted(AsyncOperation obj)
	{
		this.continuation();
	}

	// Token: 0x040016E9 RID: 5865
	private readonly UnityWebRequestAsyncOperation asyncOp;

	// Token: 0x040016EA RID: 5866
	private Action continuation;
}

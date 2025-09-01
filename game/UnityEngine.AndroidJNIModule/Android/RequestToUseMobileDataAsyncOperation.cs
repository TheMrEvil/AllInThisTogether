using System;

namespace UnityEngine.Android
{
	// Token: 0x02000017 RID: 23
	public class RequestToUseMobileDataAsyncOperation : CustomYieldInstruction
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00008378 File Offset: 0x00006578
		public override bool keepWaiting
		{
			get
			{
				object operationLock = this.m_OperationLock;
				bool result;
				lock (operationLock)
				{
					result = (this.m_RequestResult == null);
				}
				return result;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00007EB4 File Offset: 0x000060B4
		public bool isDone
		{
			get
			{
				return !this.keepWaiting;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000083BC File Offset: 0x000065BC
		public AndroidAssetPackUseMobileDataRequestResult result
		{
			get
			{
				object operationLock = this.m_OperationLock;
				AndroidAssetPackUseMobileDataRequestResult requestResult;
				lock (operationLock)
				{
					requestResult = this.m_RequestResult;
				}
				return requestResult;
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000083FC File Offset: 0x000065FC
		internal RequestToUseMobileDataAsyncOperation()
		{
			this.m_OperationLock = new object();
		}

		// Token: 0x060001AB RID: 427 RVA: 0x00008414 File Offset: 0x00006614
		internal void OnResult(AndroidAssetPackUseMobileDataRequestResult result)
		{
			object operationLock = this.m_OperationLock;
			lock (operationLock)
			{
				this.m_RequestResult = result;
			}
		}

		// Token: 0x04000047 RID: 71
		private AndroidAssetPackUseMobileDataRequestResult m_RequestResult;

		// Token: 0x04000048 RID: 72
		private readonly object m_OperationLock;
	}
}

using System;

namespace UnityEngine.Android
{
	// Token: 0x02000016 RID: 22
	public class GetAssetPackStateAsyncOperation : CustomYieldInstruction
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00008254 File Offset: 0x00006454
		public override bool keepWaiting
		{
			get
			{
				object operationLock = this.m_OperationLock;
				bool result;
				lock (operationLock)
				{
					result = (this.m_States == null);
				}
				return result;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00007EB4 File Offset: 0x000060B4
		public bool isDone
		{
			get
			{
				return !this.keepWaiting;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00008298 File Offset: 0x00006498
		public ulong size
		{
			get
			{
				object operationLock = this.m_OperationLock;
				ulong size;
				lock (operationLock)
				{
					size = this.m_Size;
				}
				return size;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x000082D8 File Offset: 0x000064D8
		public AndroidAssetPackState[] states
		{
			get
			{
				object operationLock = this.m_OperationLock;
				AndroidAssetPackState[] states;
				lock (operationLock)
				{
					states = this.m_States;
				}
				return states;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00008318 File Offset: 0x00006518
		internal GetAssetPackStateAsyncOperation()
		{
			this.m_OperationLock = new object();
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00008330 File Offset: 0x00006530
		internal void OnResult(ulong size, AndroidAssetPackState[] states)
		{
			object operationLock = this.m_OperationLock;
			lock (operationLock)
			{
				this.m_Size = size;
				this.m_States = states;
			}
		}

		// Token: 0x04000044 RID: 68
		private ulong m_Size;

		// Token: 0x04000045 RID: 69
		private AndroidAssetPackState[] m_States;

		// Token: 0x04000046 RID: 70
		private readonly object m_OperationLock;
	}
}

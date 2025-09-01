using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001EC RID: 492
	[RequiredByNativeCode]
	[NativeHeader("Runtime/Export/Scripting/AsyncOperation.bindings.h")]
	[NativeHeader("Runtime/Misc/AsyncOperation.h")]
	[StructLayout(LayoutKind.Sequential)]
	public class AsyncOperation : YieldInstruction
	{
		// Token: 0x06001645 RID: 5701
		[NativeMethod(IsThreadSafe = true)]
		[StaticAccessor("AsyncOperationBindings", StaticAccessorType.DoubleColon)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void InternalDestroy(IntPtr ptr);

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001646 RID: 5702
		public extern bool isDone { [NativeMethod("IsDone")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001647 RID: 5703
		public extern float progress { [NativeMethod("GetProgress")] [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001648 RID: 5704
		// (set) Token: 0x06001649 RID: 5705
		public extern int priority { [NativeMethod("GetPriority")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetPriority")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600164A RID: 5706
		// (set) Token: 0x0600164B RID: 5707
		public extern bool allowSceneActivation { [NativeMethod("GetAllowSceneActivation")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeMethod("SetAllowSceneActivation")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x0600164C RID: 5708 RVA: 0x00023AD4 File Offset: 0x00021CD4
		~AsyncOperation()
		{
			AsyncOperation.InternalDestroy(this.m_Ptr);
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x00023B0C File Offset: 0x00021D0C
		[RequiredByNativeCode]
		internal void InvokeCompletionEvent()
		{
			bool flag = this.m_completeCallback != null;
			if (flag)
			{
				this.m_completeCallback(this);
				this.m_completeCallback = null;
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x0600164E RID: 5710 RVA: 0x00023B40 File Offset: 0x00021D40
		// (remove) Token: 0x0600164F RID: 5711 RVA: 0x00023B7D File Offset: 0x00021D7D
		public event Action<AsyncOperation> completed
		{
			add
			{
				bool isDone = this.isDone;
				if (isDone)
				{
					value(this);
				}
				else
				{
					this.m_completeCallback = (Action<AsyncOperation>)Delegate.Combine(this.m_completeCallback, value);
				}
			}
			remove
			{
				this.m_completeCallback = (Action<AsyncOperation>)Delegate.Remove(this.m_completeCallback, value);
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x00023B97 File Offset: 0x00021D97
		public AsyncOperation()
		{
		}

		// Token: 0x040007CD RID: 1997
		internal IntPtr m_Ptr;

		// Token: 0x040007CE RID: 1998
		private Action<AsyncOperation> m_completeCallback;
	}
}

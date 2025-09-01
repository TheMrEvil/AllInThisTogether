using System;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020002B6 RID: 694
	internal struct Win32ThreadPoolNativeOverlapped
	{
		// Token: 0x06001E63 RID: 7779 RVA: 0x000175B9 File Offset: 0x000157B9
		static Win32ThreadPoolNativeOverlapped()
		{
			if (!Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06001E64 RID: 7780 RVA: 0x00070890 File Offset: 0x0006EA90
		internal Win32ThreadPoolNativeOverlapped.OverlappedData Data
		{
			get
			{
				return Win32ThreadPoolNativeOverlapped.s_dataArray[this._dataIndex];
			}
		}

		// Token: 0x06001E65 RID: 7781 RVA: 0x000708A0 File Offset: 0x0006EAA0
		internal unsafe static Win32ThreadPoolNativeOverlapped* Allocate(IOCompletionCallback callback, object state, object pinData, PreAllocatedOverlapped preAllocated)
		{
			Win32ThreadPoolNativeOverlapped* ptr = Win32ThreadPoolNativeOverlapped.AllocateNew();
			try
			{
				ptr->SetData(callback, state, pinData, preAllocated);
			}
			catch
			{
				Win32ThreadPoolNativeOverlapped.Free(ptr);
				throw;
			}
			return ptr;
		}

		// Token: 0x06001E66 RID: 7782 RVA: 0x000708DC File Offset: 0x0006EADC
		private unsafe static Win32ThreadPoolNativeOverlapped* AllocateNew()
		{
			IntPtr intPtr;
			Win32ThreadPoolNativeOverlapped* ptr;
			while ((intPtr = Volatile.Read(ref Win32ThreadPoolNativeOverlapped.s_freeList)) != IntPtr.Zero)
			{
				ptr = (Win32ThreadPoolNativeOverlapped*)((void*)intPtr);
				if (!(Interlocked.CompareExchange(ref Win32ThreadPoolNativeOverlapped.s_freeList, ptr->_nextFree, intPtr) != intPtr))
				{
					ptr->_nextFree = IntPtr.Zero;
					return ptr;
				}
			}
			ptr = (Win32ThreadPoolNativeOverlapped*)((void*)Interop.MemAlloc((UIntPtr)((ulong)((long)sizeof(Win32ThreadPoolNativeOverlapped)))));
			*ptr = default(Win32ThreadPoolNativeOverlapped);
			Win32ThreadPoolNativeOverlapped.OverlappedData value = new Win32ThreadPoolNativeOverlapped.OverlappedData();
			int num = Interlocked.Increment(ref Win32ThreadPoolNativeOverlapped.s_dataCount) - 1;
			if (num < 0)
			{
				Environment.FailFast("Too many outstanding Win32ThreadPoolNativeOverlapped instances");
			}
			for (;;)
			{
				Win32ThreadPoolNativeOverlapped.OverlappedData[] array = Volatile.Read<Win32ThreadPoolNativeOverlapped.OverlappedData[]>(ref Win32ThreadPoolNativeOverlapped.s_dataArray);
				int num2 = (array == null) ? 0 : array.Length;
				if (num2 <= num)
				{
					int i = num2;
					if (i == 0)
					{
						i = 128;
					}
					while (i <= num)
					{
						i = i * 3 / 2;
					}
					Win32ThreadPoolNativeOverlapped.OverlappedData[] array2 = array;
					Array.Resize<Win32ThreadPoolNativeOverlapped.OverlappedData>(ref array2, i);
					if (Interlocked.CompareExchange<Win32ThreadPoolNativeOverlapped.OverlappedData[]>(ref Win32ThreadPoolNativeOverlapped.s_dataArray, array2, array) != array)
					{
						continue;
					}
					array = array2;
				}
				if (Win32ThreadPoolNativeOverlapped.s_dataArray[num] != null)
				{
					break;
				}
				Interlocked.Exchange<Win32ThreadPoolNativeOverlapped.OverlappedData>(ref array[num], value);
			}
			ptr->_dataIndex = num;
			return ptr;
		}

		// Token: 0x06001E67 RID: 7783 RVA: 0x000709F0 File Offset: 0x0006EBF0
		private void SetData(IOCompletionCallback callback, object state, object pinData, PreAllocatedOverlapped preAllocated)
		{
			Win32ThreadPoolNativeOverlapped.OverlappedData data = this.Data;
			data._callback = callback;
			data._state = state;
			data._executionContext = ExecutionContext.Capture();
			data._preAllocated = preAllocated;
			if (pinData != null)
			{
				object[] array = pinData as object[];
				if (array != null && array.GetType() == typeof(object[]))
				{
					if (data._pinnedData == null || data._pinnedData.Length < array.Length)
					{
						Array.Resize<GCHandle>(ref data._pinnedData, array.Length);
					}
					for (int i = 0; i < array.Length; i++)
					{
						if (!data._pinnedData[i].IsAllocated)
						{
							data._pinnedData[i] = GCHandle.Alloc(array[i], GCHandleType.Pinned);
						}
						else
						{
							data._pinnedData[i].Target = array[i];
						}
					}
					return;
				}
				if (data._pinnedData == null)
				{
					data._pinnedData = new GCHandle[1];
				}
				if (!data._pinnedData[0].IsAllocated)
				{
					data._pinnedData[0] = GCHandle.Alloc(pinData, GCHandleType.Pinned);
					return;
				}
				data._pinnedData[0].Target = pinData;
			}
		}

		// Token: 0x06001E68 RID: 7784 RVA: 0x00070B0C File Offset: 0x0006ED0C
		internal unsafe static void Free(Win32ThreadPoolNativeOverlapped* overlapped)
		{
			overlapped->Data.Reset();
			overlapped->_overlapped = default(NativeOverlapped);
			IntPtr intPtr;
			do
			{
				intPtr = Volatile.Read(ref Win32ThreadPoolNativeOverlapped.s_freeList);
				overlapped->_nextFree = intPtr;
			}
			while (!(Interlocked.CompareExchange(ref Win32ThreadPoolNativeOverlapped.s_freeList, (IntPtr)((void*)overlapped), intPtr) == intPtr));
		}

		// Token: 0x06001E69 RID: 7785 RVA: 0x0000270D File Offset: 0x0000090D
		internal unsafe static NativeOverlapped* ToNativeOverlapped(Win32ThreadPoolNativeOverlapped* overlapped)
		{
			return (NativeOverlapped*)overlapped;
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x0000270D File Offset: 0x0000090D
		internal unsafe static Win32ThreadPoolNativeOverlapped* FromNativeOverlapped(NativeOverlapped* overlapped)
		{
			return (Win32ThreadPoolNativeOverlapped*)overlapped;
		}

		// Token: 0x06001E6B RID: 7787 RVA: 0x00070B5C File Offset: 0x0006ED5C
		internal unsafe static void CompleteWithCallback(uint errorCode, uint bytesWritten, Win32ThreadPoolNativeOverlapped* overlapped)
		{
			Win32ThreadPoolNativeOverlapped.OverlappedData data = overlapped->Data;
			data._completed = true;
			if (data._executionContext == null)
			{
				data._callback(errorCode, bytesWritten, Win32ThreadPoolNativeOverlapped.ToNativeOverlapped(overlapped));
				return;
			}
			ContextCallback contextCallback = Win32ThreadPoolNativeOverlapped.s_executionContextCallback;
			if (contextCallback == null)
			{
				contextCallback = (Win32ThreadPoolNativeOverlapped.s_executionContextCallback = new ContextCallback(Win32ThreadPoolNativeOverlapped.OnExecutionContextCallback));
			}
			Win32ThreadPoolNativeOverlapped.ExecutionContextCallbackArgs executionContextCallbackArgs = Win32ThreadPoolNativeOverlapped.t_executionContextCallbackArgs;
			if (executionContextCallbackArgs == null)
			{
				executionContextCallbackArgs = new Win32ThreadPoolNativeOverlapped.ExecutionContextCallbackArgs();
			}
			Win32ThreadPoolNativeOverlapped.t_executionContextCallbackArgs = null;
			executionContextCallbackArgs._errorCode = errorCode;
			executionContextCallbackArgs._bytesWritten = bytesWritten;
			executionContextCallbackArgs._overlapped = overlapped;
			executionContextCallbackArgs._data = data;
			ExecutionContext.Run(data._executionContext, contextCallback, executionContextCallbackArgs);
		}

		// Token: 0x06001E6C RID: 7788 RVA: 0x00070BF0 File Offset: 0x0006EDF0
		private unsafe static void OnExecutionContextCallback(object state)
		{
			Win32ThreadPoolNativeOverlapped.ExecutionContextCallbackArgs executionContextCallbackArgs = (Win32ThreadPoolNativeOverlapped.ExecutionContextCallbackArgs)state;
			uint errorCode = executionContextCallbackArgs._errorCode;
			uint bytesWritten = executionContextCallbackArgs._bytesWritten;
			Win32ThreadPoolNativeOverlapped* overlapped = executionContextCallbackArgs._overlapped;
			Win32ThreadPoolNativeOverlapped.OverlappedData data = executionContextCallbackArgs._data;
			executionContextCallbackArgs._data = null;
			Win32ThreadPoolNativeOverlapped.t_executionContextCallbackArgs = executionContextCallbackArgs;
			data._callback(errorCode, bytesWritten, Win32ThreadPoolNativeOverlapped.ToNativeOverlapped(overlapped));
		}

		// Token: 0x04001AA3 RID: 6819
		[ThreadStatic]
		private static Win32ThreadPoolNativeOverlapped.ExecutionContextCallbackArgs t_executionContextCallbackArgs;

		// Token: 0x04001AA4 RID: 6820
		private static ContextCallback s_executionContextCallback;

		// Token: 0x04001AA5 RID: 6821
		private static Win32ThreadPoolNativeOverlapped.OverlappedData[] s_dataArray;

		// Token: 0x04001AA6 RID: 6822
		private static int s_dataCount;

		// Token: 0x04001AA7 RID: 6823
		private static IntPtr s_freeList;

		// Token: 0x04001AA8 RID: 6824
		private NativeOverlapped _overlapped;

		// Token: 0x04001AA9 RID: 6825
		private IntPtr _nextFree;

		// Token: 0x04001AAA RID: 6826
		private int _dataIndex;

		// Token: 0x020002B7 RID: 695
		private class ExecutionContextCallbackArgs
		{
			// Token: 0x06001E6D RID: 7789 RVA: 0x0000259F File Offset: 0x0000079F
			public ExecutionContextCallbackArgs()
			{
			}

			// Token: 0x04001AAB RID: 6827
			internal uint _errorCode;

			// Token: 0x04001AAC RID: 6828
			internal uint _bytesWritten;

			// Token: 0x04001AAD RID: 6829
			internal unsafe Win32ThreadPoolNativeOverlapped* _overlapped;

			// Token: 0x04001AAE RID: 6830
			internal Win32ThreadPoolNativeOverlapped.OverlappedData _data;
		}

		// Token: 0x020002B8 RID: 696
		internal class OverlappedData
		{
			// Token: 0x06001E6E RID: 7790 RVA: 0x00070C40 File Offset: 0x0006EE40
			internal void Reset()
			{
				if (this._pinnedData != null)
				{
					for (int i = 0; i < this._pinnedData.Length; i++)
					{
						if (this._pinnedData[i].IsAllocated && this._pinnedData[i].Target != null)
						{
							this._pinnedData[i].Target = null;
						}
					}
				}
				this._callback = null;
				this._state = null;
				this._executionContext = null;
				this._completed = false;
				this._preAllocated = null;
			}

			// Token: 0x06001E6F RID: 7791 RVA: 0x0000259F File Offset: 0x0000079F
			public OverlappedData()
			{
			}

			// Token: 0x04001AAF RID: 6831
			internal GCHandle[] _pinnedData;

			// Token: 0x04001AB0 RID: 6832
			internal IOCompletionCallback _callback;

			// Token: 0x04001AB1 RID: 6833
			internal object _state;

			// Token: 0x04001AB2 RID: 6834
			internal ExecutionContext _executionContext;

			// Token: 0x04001AB3 RID: 6835
			internal ThreadPoolBoundHandle _boundHandle;

			// Token: 0x04001AB4 RID: 6836
			internal PreAllocatedOverlapped _preAllocated;

			// Token: 0x04001AB5 RID: 6837
			internal bool _completed;
		}
	}
}

using System;
using System.Runtime.InteropServices;
using Unity.Baselib.LowLevel;

namespace Unity.Baselib
{
	// Token: 0x02000014 RID: 20
	internal struct ErrorState
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00002140 File Offset: 0x00000340
		public void ThrowIfFailed()
		{
			bool flag = this.ErrorCode > Binding.Baselib_ErrorCode.Success;
			if (flag)
			{
				throw new BaselibException(this);
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002167 File Offset: 0x00000367
		public Binding.Baselib_ErrorCode ErrorCode
		{
			get
			{
				return this.nativeErrorState.code;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000024 RID: 36 RVA: 0x00002174 File Offset: 0x00000374
		public unsafe Binding.Baselib_ErrorState* NativeErrorStatePtr
		{
			get
			{
				fixed (Binding.Baselib_ErrorState* ptr = &this.nativeErrorState)
				{
					return ptr;
				}
			}
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002194 File Offset: 0x00000394
		public unsafe string Explain(Binding.Baselib_ErrorState_ExplainVerbosity verbosity = Binding.Baselib_ErrorState_ExplainVerbosity.ErrorType_SourceLocation_Explanation)
		{
			fixed (Binding.Baselib_ErrorState* ptr = &this.nativeErrorState)
			{
				Binding.Baselib_ErrorState* errorState = ptr;
				uint num = Binding.Baselib_ErrorState_Explain(errorState, null, 0U, verbosity) + 1U;
				IntPtr intPtr = Binding.Baselib_Memory_Allocate(new UIntPtr(num));
				string result;
				try
				{
					Binding.Baselib_ErrorState_Explain(errorState, (byte*)((void*)intPtr), num, verbosity);
					result = Marshal.PtrToStringAnsi(intPtr);
				}
				finally
				{
					Binding.Baselib_Memory_Free(intPtr);
				}
				return result;
			}
		}

		// Token: 0x04000025 RID: 37
		private Binding.Baselib_ErrorState nativeErrorState;
	}
}

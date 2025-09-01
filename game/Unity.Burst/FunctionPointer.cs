using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Unity.Collections.LowLevel.Unsafe;

namespace Unity.Burst
{
	// Token: 0x02000014 RID: 20
	public readonly struct FunctionPointer<T> : IFunctionPointer
	{
		// Token: 0x060000A5 RID: 165 RVA: 0x00005415 File Offset: 0x00003615
		public FunctionPointer(IntPtr ptr)
		{
			this._ptr = ptr;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x0000541E File Offset: 0x0000361E
		public IntPtr Value
		{
			get
			{
				return this._ptr;
			}
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00005426 File Offset: 0x00003626
		[Conditional("ENABLE_UNITY_COLLECTIONS_CHECKS")]
		private void CheckIsCreated()
		{
			if (!this.IsCreated)
			{
				throw new NullReferenceException("Object reference not set to an instance of an object");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000543B File Offset: 0x0000363B
		public T Invoke
		{
			get
			{
				return Marshal.GetDelegateForFunctionPointer<T>(this._ptr);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00005448 File Offset: 0x00003648
		public bool IsCreated
		{
			get
			{
				return this._ptr != IntPtr.Zero;
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000545A File Offset: 0x0000365A
		IFunctionPointer IFunctionPointer.FromIntPtr(IntPtr ptr)
		{
			return new FunctionPointer<T>(ptr);
		}

		// Token: 0x04000143 RID: 323
		[NativeDisableUnsafePtrRestriction]
		private readonly IntPtr _ptr;
	}
}

using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x0200004D RID: 77
	internal struct RuntimeGPtrArrayHandle
	{
		// Token: 0x06000119 RID: 281 RVA: 0x00004960 File Offset: 0x00002B60
		internal unsafe RuntimeGPtrArrayHandle(RuntimeStructs.GPtrArray* value)
		{
			this.value = value;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00004969 File Offset: 0x00002B69
		internal unsafe RuntimeGPtrArrayHandle(IntPtr ptr)
		{
			this.value = (RuntimeStructs.GPtrArray*)((void*)ptr);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00004977 File Offset: 0x00002B77
		internal unsafe int Length
		{
			get
			{
				return this.value->len;
			}
		}

		// Token: 0x1700000F RID: 15
		internal IntPtr this[int i]
		{
			get
			{
				return this.Lookup(i);
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000498D File Offset: 0x00002B8D
		internal unsafe IntPtr Lookup(int i)
		{
			if (i >= 0 && i < this.Length)
			{
				return this.value->data[i];
			}
			throw new IndexOutOfRangeException();
		}

		// Token: 0x0600011E RID: 286
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void GPtrArrayFree(RuntimeStructs.GPtrArray* value);

		// Token: 0x0600011F RID: 287 RVA: 0x000049B8 File Offset: 0x00002BB8
		internal static void DestroyAndFree(ref RuntimeGPtrArrayHandle h)
		{
			RuntimeGPtrArrayHandle.GPtrArrayFree(h.value);
			h.value = null;
		}

		// Token: 0x04000DE4 RID: 3556
		private unsafe RuntimeStructs.GPtrArray* value;
	}
}

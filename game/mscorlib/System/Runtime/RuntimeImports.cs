using System;
using System.Runtime.CompilerServices;

namespace System.Runtime
{
	// Token: 0x02000554 RID: 1364
	public static class RuntimeImports
	{
		// Token: 0x060035BB RID: 13755 RVA: 0x000C2028 File Offset: 0x000C0228
		internal unsafe static void RhZeroMemory(ref byte b, ulong byteLength)
		{
			fixed (byte* ptr = &b)
			{
				RuntimeImports.ZeroMemory((void*)ptr, (uint)byteLength);
			}
		}

		// Token: 0x060035BC RID: 13756 RVA: 0x000C2043 File Offset: 0x000C0243
		internal unsafe static void RhZeroMemory(IntPtr p, UIntPtr byteLength)
		{
			RuntimeImports.ZeroMemory((void*)p, (uint)byteLength);
		}

		// Token: 0x060035BD RID: 13757
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void ZeroMemory(void* p, uint byteLength);

		// Token: 0x060035BE RID: 13758
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void Memmove(byte* dest, byte* src, uint len);

		// Token: 0x060035BF RID: 13759
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void Memmove_wbarrier(byte* dest, byte* src, uint len, IntPtr type_handle);

		// Token: 0x060035C0 RID: 13760
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void _ecvt_s(byte* buffer, int sizeInBytes, double value, int count, int* dec, int* sign);
	}
}

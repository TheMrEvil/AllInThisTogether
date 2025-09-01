using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x0200094B RID: 2379
	internal class ConstantHelper
	{
		// Token: 0x06005369 RID: 21353 RVA: 0x00105DB0 File Offset: 0x00103FB0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static byte GetByteWithAllBitsSet()
		{
			byte result = 0;
			*(&result) = byte.MaxValue;
			return result;
		}

		// Token: 0x0600536A RID: 21354 RVA: 0x00105DCC File Offset: 0x00103FCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static sbyte GetSByteWithAllBitsSet()
		{
			sbyte result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x0600536B RID: 21355 RVA: 0x00105DE4 File Offset: 0x00103FE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort GetUInt16WithAllBitsSet()
		{
			ushort result = 0;
			*(&result) = ushort.MaxValue;
			return result;
		}

		// Token: 0x0600536C RID: 21356 RVA: 0x00105E00 File Offset: 0x00104000
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static short GetInt16WithAllBitsSet()
		{
			short result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x0600536D RID: 21357 RVA: 0x00105E18 File Offset: 0x00104018
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint GetUInt32WithAllBitsSet()
		{
			uint result = 0U;
			*(&result) = uint.MaxValue;
			return result;
		}

		// Token: 0x0600536E RID: 21358 RVA: 0x00105E30 File Offset: 0x00104030
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetInt32WithAllBitsSet()
		{
			int result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x0600536F RID: 21359 RVA: 0x00105E48 File Offset: 0x00104048
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong GetUInt64WithAllBitsSet()
		{
			ulong result = 0UL;
			*(&result) = ulong.MaxValue;
			return result;
		}

		// Token: 0x06005370 RID: 21360 RVA: 0x00105E60 File Offset: 0x00104060
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long GetInt64WithAllBitsSet()
		{
			long result = 0L;
			*(&result) = -1L;
			return result;
		}

		// Token: 0x06005371 RID: 21361 RVA: 0x00105E78 File Offset: 0x00104078
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float GetSingleWithAllBitsSet()
		{
			float result = 0f;
			*(int*)(&result) = -1;
			return result;
		}

		// Token: 0x06005372 RID: 21362 RVA: 0x00105E94 File Offset: 0x00104094
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double GetDoubleWithAllBitsSet()
		{
			double result = 0.0;
			*(long*)(&result) = -1L;
			return result;
		}

		// Token: 0x06005373 RID: 21363 RVA: 0x0000259F File Offset: 0x0000079F
		public ConstantHelper()
		{
		}
	}
}

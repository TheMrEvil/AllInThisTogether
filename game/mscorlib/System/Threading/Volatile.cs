using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System.Threading
{
	/// <summary>Contains methods for performing volatile memory operations.</summary>
	// Token: 0x020002FD RID: 765
	public static class Volatile
	{
		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x0600214B RID: 8523 RVA: 0x00078302 File Offset: 0x00076502
		[Intrinsic]
		public static bool Read(ref bool location)
		{
			return Unsafe.As<bool, Volatile.VolatileBoolean>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600214C RID: 8524 RVA: 0x00078311 File Offset: 0x00076511
		[Intrinsic]
		public static void Write(ref bool location, bool value)
		{
			Unsafe.As<bool, Volatile.VolatileBoolean>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x0600214D RID: 8525 RVA: 0x00078321 File Offset: 0x00076521
		[Intrinsic]
		public static byte Read(ref byte location)
		{
			return Unsafe.As<byte, Volatile.VolatileByte>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600214E RID: 8526 RVA: 0x00078330 File Offset: 0x00076530
		[Intrinsic]
		public static void Write(ref byte location, byte value)
		{
			Unsafe.As<byte, Volatile.VolatileByte>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x0600214F RID: 8527 RVA: 0x00078340 File Offset: 0x00076540
		[Intrinsic]
		public static short Read(ref short location)
		{
			return Unsafe.As<short, Volatile.VolatileInt16>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06002150 RID: 8528 RVA: 0x0007834F File Offset: 0x0007654F
		[Intrinsic]
		public static void Write(ref short location, short value)
		{
			Unsafe.As<short, Volatile.VolatileInt16>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06002151 RID: 8529 RVA: 0x0007835F File Offset: 0x0007655F
		[Intrinsic]
		public static int Read(ref int location)
		{
			return Unsafe.As<int, Volatile.VolatileInt32>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06002152 RID: 8530 RVA: 0x0007836E File Offset: 0x0007656E
		[Intrinsic]
		public static void Write(ref int location, int value)
		{
			Unsafe.As<int, Volatile.VolatileInt32>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06002153 RID: 8531 RVA: 0x0007837E File Offset: 0x0007657E
		[Intrinsic]
		public static IntPtr Read(ref IntPtr location)
		{
			return Unsafe.As<IntPtr, Volatile.VolatileIntPtr>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06002154 RID: 8532 RVA: 0x0007838D File Offset: 0x0007658D
		[Intrinsic]
		public static void Write(ref IntPtr location, IntPtr value)
		{
			Unsafe.As<IntPtr, Volatile.VolatileIntPtr>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06002155 RID: 8533 RVA: 0x0007839D File Offset: 0x0007659D
		[Intrinsic]
		[CLSCompliant(false)]
		public static sbyte Read(ref sbyte location)
		{
			return Unsafe.As<sbyte, Volatile.VolatileSByte>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06002156 RID: 8534 RVA: 0x000783AC File Offset: 0x000765AC
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref sbyte location, sbyte value)
		{
			Unsafe.As<sbyte, Volatile.VolatileSByte>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06002157 RID: 8535 RVA: 0x000783BC File Offset: 0x000765BC
		[Intrinsic]
		public static float Read(ref float location)
		{
			return Unsafe.As<float, Volatile.VolatileSingle>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06002158 RID: 8536 RVA: 0x000783CB File Offset: 0x000765CB
		[Intrinsic]
		public static void Write(ref float location, float value)
		{
			Unsafe.As<float, Volatile.VolatileSingle>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06002159 RID: 8537 RVA: 0x000783DB File Offset: 0x000765DB
		[CLSCompliant(false)]
		[Intrinsic]
		public static ushort Read(ref ushort location)
		{
			return Unsafe.As<ushort, Volatile.VolatileUInt16>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600215A RID: 8538 RVA: 0x000783EA File Offset: 0x000765EA
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref ushort location, ushort value)
		{
			Unsafe.As<ushort, Volatile.VolatileUInt16>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x0600215B RID: 8539 RVA: 0x000783FA File Offset: 0x000765FA
		[CLSCompliant(false)]
		[Intrinsic]
		public static uint Read(ref uint location)
		{
			return Unsafe.As<uint, Volatile.VolatileUInt32>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600215C RID: 8540 RVA: 0x00078409 File Offset: 0x00076609
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref uint location, uint value)
		{
			Unsafe.As<uint, Volatile.VolatileUInt32>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x0600215D RID: 8541 RVA: 0x00078419 File Offset: 0x00076619
		[CLSCompliant(false)]
		[Intrinsic]
		public static UIntPtr Read(ref UIntPtr location)
		{
			return Unsafe.As<UIntPtr, Volatile.VolatileUIntPtr>(ref location).Value;
		}

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x0600215E RID: 8542 RVA: 0x00078428 File Offset: 0x00076628
		[CLSCompliant(false)]
		[Intrinsic]
		public static void Write(ref UIntPtr location, UIntPtr value)
		{
			Unsafe.As<UIntPtr, Volatile.VolatileUIntPtr>(ref location).Value = value;
		}

		/// <summary>Reads the object reference from the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <typeparam name="T">The type of field to read. This must be a reference type, not a value type.</typeparam>
		/// <returns>The reference to <paramref name="T" /> that was read. This reference is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x0600215F RID: 8543 RVA: 0x00078438 File Offset: 0x00076638
		[Intrinsic]
		public static T Read<T>(ref T location) where T : class
		{
			return Unsafe.As<T>(Unsafe.As<T, Volatile.VolatileObject>(ref location).Value);
		}

		/// <summary>Writes the specified object reference to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the object reference is written.</param>
		/// <param name="value">The object reference to write. The reference is written immediately so that it is visible to all processors in the computer.</param>
		/// <typeparam name="T">The type of field to write. This must be a reference type, not a value type.</typeparam>
		// Token: 0x06002160 RID: 8544 RVA: 0x0007844C File Offset: 0x0007664C
		[Intrinsic]
		public static void Write<T>(ref T location, T value) where T : class
		{
			Unsafe.As<T, Volatile.VolatileObject>(ref location).Value = value;
		}

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06002161 RID: 8545
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Read(ref long location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06002162 RID: 8546
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern ulong Read(ref ulong location);

		/// <summary>Reads the value of the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears after this method in the code, the processor cannot move it before this method.</summary>
		/// <param name="location">The field to read.</param>
		/// <returns>The value that was read. This value is the latest written by any processor in the computer, regardless of the number of processors or the state of processor cache.</returns>
		// Token: 0x06002163 RID: 8547
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Read(ref double location);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a memory operation appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06002164 RID: 8548
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref long location, long value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06002165 RID: 8549
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref ulong location, ulong value);

		/// <summary>Writes the specified value to the specified field. On systems that require it, inserts a memory barrier that prevents the processor from reordering memory operations as follows: If a read or write appears before this method in the code, the processor cannot move it after this method.</summary>
		/// <param name="location">The field where the value is written.</param>
		/// <param name="value">The value to write. The value is written immediately so that it is visible to all processors in the computer.</param>
		// Token: 0x06002166 RID: 8550
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Write(ref double location, double value);

		// Token: 0x020002FE RID: 766
		private struct VolatileBoolean
		{
			// Token: 0x04001BBC RID: 7100
			public volatile bool Value;
		}

		// Token: 0x020002FF RID: 767
		private struct VolatileByte
		{
			// Token: 0x04001BBD RID: 7101
			public volatile byte Value;
		}

		// Token: 0x02000300 RID: 768
		private struct VolatileInt16
		{
			// Token: 0x04001BBE RID: 7102
			public volatile short Value;
		}

		// Token: 0x02000301 RID: 769
		private struct VolatileInt32
		{
			// Token: 0x04001BBF RID: 7103
			public volatile int Value;
		}

		// Token: 0x02000302 RID: 770
		private struct VolatileIntPtr
		{
			// Token: 0x04001BC0 RID: 7104
			public volatile IntPtr Value;
		}

		// Token: 0x02000303 RID: 771
		private struct VolatileSByte
		{
			// Token: 0x04001BC1 RID: 7105
			public volatile sbyte Value;
		}

		// Token: 0x02000304 RID: 772
		private struct VolatileSingle
		{
			// Token: 0x04001BC2 RID: 7106
			public volatile float Value;
		}

		// Token: 0x02000305 RID: 773
		private struct VolatileUInt16
		{
			// Token: 0x04001BC3 RID: 7107
			public volatile ushort Value;
		}

		// Token: 0x02000306 RID: 774
		private struct VolatileUInt32
		{
			// Token: 0x04001BC4 RID: 7108
			public volatile uint Value;
		}

		// Token: 0x02000307 RID: 775
		private struct VolatileUIntPtr
		{
			// Token: 0x04001BC5 RID: 7109
			public volatile UIntPtr Value;
		}

		// Token: 0x02000308 RID: 776
		private struct VolatileObject
		{
			// Token: 0x04001BC6 RID: 7110
			public volatile object Value;
		}
	}
}

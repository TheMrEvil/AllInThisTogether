using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using Unity;

namespace System.Threading
{
	/// <summary>Provides atomic operations for variables that are shared by multiple threads.</summary>
	// Token: 0x020002F0 RID: 752
	public static class Interlocked
	{
		/// <summary>Compares two 32-bit signed integers for equality and, if they are equal, replaces the first value.</summary>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020B3 RID: 8371
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CompareExchange(ref int location1, int value, int comparand);

		// Token: 0x060020B4 RID: 8372
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int CompareExchange(ref int location1, int value, int comparand, ref bool succeeded);

		// Token: 0x060020B5 RID: 8373
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CompareExchange(ref object location1, ref object value, ref object comparand, ref object result);

		/// <summary>Compares two objects for reference equality and, if they are equal, replaces the first object.</summary>
		/// <param name="location1">The destination object that is compared by reference with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The object that replaces the destination object if the reference comparison results in equality.</param>
		/// <param name="comparand">The object that is compared by reference to the object at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020B6 RID: 8374 RVA: 0x000769C8 File Offset: 0x00074BC8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static object CompareExchange(ref object location1, object value, object comparand)
		{
			object result = null;
			Interlocked.CompareExchange(ref location1, ref value, ref comparand, ref result);
			return result;
		}

		/// <summary>Compares two single-precision floating point numbers for equality and, if they are equal, replaces the first value.</summary>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020B7 RID: 8375
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CompareExchange(ref float location1, float value, float comparand);

		/// <summary>Decrements a specified variable and stores the result, as an atomic operation.</summary>
		/// <param name="location">The variable whose value is to be decremented.</param>
		/// <returns>The decremented value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location" /> is a null pointer.</exception>
		// Token: 0x060020B8 RID: 8376
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Decrement(ref int location);

		/// <summary>Decrements the specified variable and stores the result, as an atomic operation.</summary>
		/// <param name="location">The variable whose value is to be decremented.</param>
		/// <returns>The decremented value.</returns>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location" /> is a null pointer.</exception>
		// Token: 0x060020B9 RID: 8377
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Decrement(ref long location);

		/// <summary>Increments a specified variable and stores the result, as an atomic operation.</summary>
		/// <param name="location">The variable whose value is to be incremented.</param>
		/// <returns>The incremented value.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location" /> is a null pointer.</exception>
		// Token: 0x060020BA RID: 8378
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Increment(ref int location);

		/// <summary>Increments a specified variable and stores the result, as an atomic operation.</summary>
		/// <param name="location">The variable whose value is to be incremented.</param>
		/// <returns>The incremented value.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location" /> is a null pointer.</exception>
		// Token: 0x060020BB RID: 8379
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Increment(ref long location);

		/// <summary>Sets a 32-bit signed integer to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020BC RID: 8380
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Exchange(ref int location1, int value);

		// Token: 0x060020BD RID: 8381
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Exchange(ref object location1, ref object value, ref object result);

		/// <summary>Sets an object to a specified value and returns a reference to the original object, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020BE RID: 8382 RVA: 0x000769E4 File Offset: 0x00074BE4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static object Exchange(ref object location1, object value)
		{
			object result = null;
			Interlocked.Exchange(ref location1, ref value, ref result);
			return result;
		}

		/// <summary>Sets a single-precision floating point number to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020BF RID: 8383
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Exchange(ref float location1, float value);

		/// <summary>Compares two 64-bit signed integers for equality and, if they are equal, replaces the first value.</summary>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020C0 RID: 8384
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CompareExchange(ref long location1, long value, long comparand);

		/// <summary>Compares two platform-specific handles or pointers for equality and, if they are equal, replaces the first one.</summary>
		/// <param name="location1">The destination <see cref="T:System.IntPtr" />, whose value is compared with the value of <paramref name="comparand" /> and possibly replaced by <paramref name="value" />.</param>
		/// <param name="value">The <see cref="T:System.IntPtr" /> that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The <see cref="T:System.IntPtr" /> that is compared to the value at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020C1 RID: 8385
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CompareExchange(ref IntPtr location1, IntPtr value, IntPtr comparand);

		/// <summary>Compares two double-precision floating point numbers for equality and, if they are equal, replaces the first value.</summary>
		/// <param name="location1">The destination, whose value is compared with <paramref name="comparand" /> and possibly replaced.</param>
		/// <param name="value">The value that replaces the destination value if the comparison results in equality.</param>
		/// <param name="comparand">The value that is compared to the value at <paramref name="location1" />.</param>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020C2 RID: 8386
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CompareExchange(ref double location1, double value, double comparand);

		/// <summary>Compares two instances of the specified reference type <paramref name="T" /> for reference equality and, if they are equal, replaces the first one.</summary>
		/// <param name="location1">The destination, whose value is compared by reference with <paramref name="comparand" /> and possibly replaced. This is a reference parameter (<see langword="ref" /> in C#, <see langword="ByRef" /> in Visual Basic).</param>
		/// <param name="value">The value that replaces the destination value if the comparison by reference results in equality.</param>
		/// <param name="comparand">The value that is compared by reference to the value at <paramref name="location1" />.</param>
		/// <typeparam name="T">The type to be used for <paramref name="location1" />, <paramref name="value" />, and <paramref name="comparand" />. This type must be a reference type.</typeparam>
		/// <returns>The original value in <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020C3 RID: 8387 RVA: 0x00076A00 File Offset: 0x00074C00
		[ComVisible(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[Intrinsic]
		public static T CompareExchange<T>(ref T location1, T value, T comparand) where T : class
		{
			if (Unsafe.AsPointer<T>(ref location1) == null)
			{
				throw new NullReferenceException();
			}
			T result = default(T);
			Interlocked.CompareExchange(Unsafe.As<T, object>(ref location1), Unsafe.As<T, object>(ref value), Unsafe.As<T, object>(ref comparand), Unsafe.As<T, object>(ref result));
			return result;
		}

		/// <summary>Sets a 64-bit signed integer to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020C4 RID: 8388
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Exchange(ref long location1, long value);

		/// <summary>Sets a platform-specific handle or pointer to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020C5 RID: 8389
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Exchange(ref IntPtr location1, IntPtr value);

		/// <summary>Sets a double-precision floating point number to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value.</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020C6 RID: 8390
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exchange(ref double location1, double value);

		/// <summary>Sets a variable of the specified type <paramref name="T" /> to a specified value and returns the original value, as an atomic operation.</summary>
		/// <param name="location1">The variable to set to the specified value. This is a reference parameter (<see langword="ref" /> in C#, <see langword="ByRef" /> in Visual Basic).</param>
		/// <param name="value">The value to which the <paramref name="location1" /> parameter is set.</param>
		/// <typeparam name="T">The type to be used for <paramref name="location1" /> and <paramref name="value" />. This type must be a reference type.</typeparam>
		/// <returns>The original value of <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020C7 RID: 8391 RVA: 0x00076A48 File Offset: 0x00074C48
		[ComVisible(false)]
		[Intrinsic]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static T Exchange<T>(ref T location1, T value) where T : class
		{
			if (Unsafe.AsPointer<T>(ref location1) == null)
			{
				throw new NullReferenceException();
			}
			T result = default(T);
			Interlocked.Exchange(Unsafe.As<T, object>(ref location1), Unsafe.As<T, object>(ref value), Unsafe.As<T, object>(ref result));
			return result;
		}

		/// <summary>Returns a 64-bit value, loaded as an atomic operation.</summary>
		/// <param name="location">The 64-bit value to be loaded.</param>
		/// <returns>The loaded value.</returns>
		// Token: 0x060020C8 RID: 8392
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Read(ref long location);

		/// <summary>Adds two 32-bit integers and replaces the first integer with the sum, as an atomic operation.</summary>
		/// <param name="location1">A variable containing the first value to be added. The sum of the two values is stored in <paramref name="location1" />.</param>
		/// <param name="value">The value to be added to the integer at <paramref name="location1" />.</param>
		/// <returns>The new value stored at <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020C9 RID: 8393
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Add(ref int location1, int value);

		/// <summary>Adds two 64-bit integers and replaces the first integer with the sum, as an atomic operation.</summary>
		/// <param name="location1">A variable containing the first value to be added. The sum of the two values is stored in <paramref name="location1" />.</param>
		/// <param name="value">The value to be added to the integer at <paramref name="location1" />.</param>
		/// <returns>The new value stored at <paramref name="location1" />.</returns>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="location1" /> is a null pointer.</exception>
		// Token: 0x060020CA RID: 8394
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Add(ref long location1, long value);

		/// <summary>Synchronizes memory access as follows: The processor that executes the current thread cannot reorder instructions in such a way that memory accesses before the call to <see cref="M:System.Threading.Interlocked.MemoryBarrier" /> execute after memory accesses that follow the call to <see cref="M:System.Threading.Interlocked.MemoryBarrier" />.</summary>
		// Token: 0x060020CB RID: 8395 RVA: 0x00076A87 File Offset: 0x00074C87
		public static void MemoryBarrier()
		{
			Thread.MemoryBarrier();
		}

		// Token: 0x060020CC RID: 8396
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MemoryBarrierProcessWide();

		/// <summary>Defines a memory fence that blocks speculative execution past this point until pending reads and writes are complete.</summary>      
		// Token: 0x060020CD RID: 8397 RVA: 0x000173AD File Offset: 0x000155AD
		[SecuritySafeCritical]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static void SpeculationBarrier()
		{
			ThrowStub.ThrowNotSupportedException();
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices.ComTypes;
using System.Runtime.Remoting;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using Mono.Interop;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides a collection of methods for allocating unmanaged memory, copying unmanaged memory blocks, and converting managed to unmanaged types, as well as other miscellaneous methods used when interacting with unmanaged code.</summary>
	// Token: 0x02000748 RID: 1864
	public static class Marshal
	{
		// Token: 0x0600414E RID: 16718
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int AddRefInternal(IntPtr pUnk);

		/// <summary>Increments the reference count on the specified interface.</summary>
		/// <param name="pUnk">The interface reference count to increment.</param>
		/// <returns>The new value of the reference count on the <paramref name="pUnk" /> parameter.</returns>
		// Token: 0x0600414F RID: 16719 RVA: 0x000E1FE2 File Offset: 0x000E01E2
		public static int AddRef(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentNullException("pUnk");
			}
			return Marshal.AddRefInternal(pUnk);
		}

		/// <summary>Indicates whether runtime callable wrappers (RCWs) from any context are available for cleanup.</summary>
		/// <returns>
		///   <see langword="true" /> if there are any RCWs available for cleanup; otherwise, <see langword="false" />.</returns>
		// Token: 0x06004150 RID: 16720 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		public static bool AreComObjectsAvailableForCleanup()
		{
			return false;
		}

		/// <summary>Notifies the runtime to clean up all Runtime Callable Wrappers (RCWs) allocated in the current context.</summary>
		// Token: 0x06004151 RID: 16721 RVA: 0x000E2002 File Offset: 0x000E0202
		public static void CleanupUnusedObjectsInCurrentContext()
		{
			if (Environment.IsRunningOnWindows)
			{
				throw new PlatformNotSupportedException();
			}
		}

		/// <summary>Allocates a block of memory of specified size from the COM task memory allocator.</summary>
		/// <param name="cb">The size of the block of memory to be allocated.</param>
		/// <returns>An integer representing the address of the block of memory allocated. This memory must be released with <see cref="M:System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr)" />.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request.</exception>
		// Token: 0x06004152 RID: 16722
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocCoTaskMem(int cb);

		// Token: 0x06004153 RID: 16723
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr AllocCoTaskMemSize(UIntPtr sizet);

		/// <summary>Allocates memory from the unmanaged memory of the process by using the pointer to the specified number of bytes.</summary>
		/// <param name="cb">The required number of bytes in memory.</param>
		/// <returns>A pointer to the newly allocated memory. This memory must be released using the <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> method.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request.</exception>
		// Token: 0x06004154 RID: 16724
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr AllocHGlobal(IntPtr cb);

		/// <summary>Allocates memory from the unmanaged memory of the process by using the specified number of bytes.</summary>
		/// <param name="cb">The required number of bytes in memory.</param>
		/// <returns>A pointer to the newly allocated memory. This memory must be released using the <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" /> method.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request.</exception>
		// Token: 0x06004155 RID: 16725 RVA: 0x000E2011 File Offset: 0x000E0211
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		public static IntPtr AllocHGlobal(int cb)
		{
			return Marshal.AllocHGlobal((IntPtr)cb);
		}

		/// <summary>Gets an interface pointer identified by the specified moniker.</summary>
		/// <param name="monikerName">The moniker corresponding to the desired interface pointer.</param>
		/// <returns>An object containing a reference to the interface pointer identified by the <paramref name="monikerName" /> parameter. A moniker is a name, and in this case, the moniker is defined by an interface.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">An unrecognized HRESULT was returned by the unmanaged <see langword="BindToMoniker" /> method.</exception>
		// Token: 0x06004156 RID: 16726 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static object BindToMoniker(string monikerName)
		{
			throw new NotImplementedException();
		}

		/// <summary>Changes the strength of an object's COM Callable Wrapper (CCW) handle.</summary>
		/// <param name="otp">The object whose CCW holds a reference counted handle. The handle is strong if the reference count on the CCW is greater than zero; otherwise, it is weak.</param>
		/// <param name="fIsWeak">
		///   <see langword="true" /> to change the strength of the handle on the <paramref name="otp" /> parameter to weak, regardless of its reference count; <see langword="false" /> to reset the handle strength on <paramref name="otp" /> to be reference counted.</param>
		// Token: 0x06004157 RID: 16727 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static void ChangeWrapperHandleStrength(object otp, bool fIsWeak)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004158 RID: 16728 RVA: 0x000E201E File Offset: 0x000E021E
		internal static void copy_to_unmanaged(Array source, int startIndex, IntPtr destination, int length)
		{
			Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, null);
		}

		// Token: 0x06004159 RID: 16729
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void copy_to_unmanaged_fixed(Array source, int startIndex, IntPtr destination, int length, void* fixed_source_element);

		// Token: 0x0600415A RID: 16730 RVA: 0x000E202B File Offset: 0x000E022B
		private static bool skip_fixed(Array array, int startIndex)
		{
			return startIndex < 0 || startIndex >= array.Length;
		}

		// Token: 0x0600415B RID: 16731 RVA: 0x000E2040 File Offset: 0x000E0240
		internal unsafe static void copy_to_unmanaged(byte[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, null);
				return;
			}
			fixed (byte* ptr = &source[startIndex])
			{
				void* fixed_source_element = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, fixed_source_element);
			}
		}

		// Token: 0x0600415C RID: 16732 RVA: 0x000E207C File Offset: 0x000E027C
		internal unsafe static void copy_to_unmanaged(char[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, null);
				return;
			}
			fixed (char* ptr = &source[startIndex])
			{
				void* fixed_source_element = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, fixed_source_element);
			}
		}

		/// <summary>Copies data from a one-dimensional, managed 8-bit unsigned integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x0600415D RID: 16733 RVA: 0x000E20B8 File Offset: 0x000E02B8
		public unsafe static void Copy(byte[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (byte* ptr = &source[startIndex])
			{
				void* fixed_source_element = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, fixed_source_element);
			}
		}

		/// <summary>Copies data from a one-dimensional, managed character array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x0600415E RID: 16734 RVA: 0x000E20F0 File Offset: 0x000E02F0
		public unsafe static void Copy(char[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (char* ptr = &source[startIndex])
			{
				void* fixed_source_element = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, fixed_source_element);
			}
		}

		/// <summary>Copies data from a one-dimensional, managed 16-bit signed integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x0600415F RID: 16735 RVA: 0x000E2128 File Offset: 0x000E0328
		public unsafe static void Copy(short[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (short* ptr = &source[startIndex])
			{
				void* fixed_source_element = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, fixed_source_element);
			}
		}

		/// <summary>Copies data from a one-dimensional, managed 32-bit signed integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="startIndex" /> or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x06004160 RID: 16736 RVA: 0x000E2160 File Offset: 0x000E0360
		public unsafe static void Copy(int[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (int* ptr = &source[startIndex])
			{
				void* fixed_source_element = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, fixed_source_element);
			}
		}

		/// <summary>Copies data from a one-dimensional, managed 64-bit signed integer array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x06004161 RID: 16737 RVA: 0x000E2198 File Offset: 0x000E0398
		public unsafe static void Copy(long[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (long* ptr = &source[startIndex])
			{
				void* fixed_source_element = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, fixed_source_element);
			}
		}

		/// <summary>Copies data from a one-dimensional, managed single-precision floating-point number array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x06004162 RID: 16738 RVA: 0x000E21D0 File Offset: 0x000E03D0
		public unsafe static void Copy(float[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (float* ptr = &source[startIndex])
			{
				void* fixed_source_element = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, fixed_source_element);
			}
		}

		/// <summary>Copies data from a one-dimensional, managed double-precision floating-point number array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="startIndex" /> and <paramref name="length" /> are not valid.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="startIndex" />, <paramref name="destination" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x06004163 RID: 16739 RVA: 0x000E2208 File Offset: 0x000E0408
		public unsafe static void Copy(double[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (double* ptr = &source[startIndex])
			{
				void* fixed_source_element = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, fixed_source_element);
			}
		}

		/// <summary>Copies data from a one-dimensional, managed <see cref="T:System.IntPtr" /> array to an unmanaged memory pointer.</summary>
		/// <param name="source">The one-dimensional array to copy from.</param>
		/// <param name="startIndex">The zero-based index in the source array where copying should start.</param>
		/// <param name="destination">The memory pointer to copy to.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x06004164 RID: 16740 RVA: 0x000E2240 File Offset: 0x000E0440
		public unsafe static void Copy(IntPtr[] source, int startIndex, IntPtr destination, int length)
		{
			if (Marshal.skip_fixed(source, startIndex))
			{
				Marshal.copy_to_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (IntPtr* ptr = &source[startIndex])
			{
				void* fixed_source_element = (void*)ptr;
				Marshal.copy_to_unmanaged_fixed(source, startIndex, destination, length, fixed_source_element);
			}
		}

		// Token: 0x06004165 RID: 16741 RVA: 0x000E2278 File Offset: 0x000E0478
		internal static void copy_from_unmanaged(IntPtr source, int startIndex, Array destination, int length)
		{
			Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, null);
		}

		// Token: 0x06004166 RID: 16742
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void copy_from_unmanaged_fixed(IntPtr source, int startIndex, Array destination, int length, void* fixed_destination_element);

		/// <summary>Copies data from an unmanaged memory pointer to a managed 8-bit unsigned integer array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x06004167 RID: 16743 RVA: 0x000E2288 File Offset: 0x000E0488
		public unsafe static void Copy(IntPtr source, byte[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (byte* ptr = &destination[startIndex])
			{
				void* fixed_destination_element = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, fixed_destination_element);
			}
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed character array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x06004168 RID: 16744 RVA: 0x000E22C0 File Offset: 0x000E04C0
		public unsafe static void Copy(IntPtr source, char[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (char* ptr = &destination[startIndex])
			{
				void* fixed_destination_element = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, fixed_destination_element);
			}
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 16-bit signed integer array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x06004169 RID: 16745 RVA: 0x000E22F8 File Offset: 0x000E04F8
		public unsafe static void Copy(IntPtr source, short[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (short* ptr = &destination[startIndex])
			{
				void* fixed_destination_element = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, fixed_destination_element);
			}
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 32-bit signed integer array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x0600416A RID: 16746 RVA: 0x000E2330 File Offset: 0x000E0530
		public unsafe static void Copy(IntPtr source, int[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (int* ptr = &destination[startIndex])
			{
				void* fixed_destination_element = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, fixed_destination_element);
			}
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed 64-bit signed integer array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x0600416B RID: 16747 RVA: 0x000E2368 File Offset: 0x000E0568
		public unsafe static void Copy(IntPtr source, long[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (long* ptr = &destination[startIndex])
			{
				void* fixed_destination_element = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, fixed_destination_element);
			}
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed single-precision floating-point number array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x0600416C RID: 16748 RVA: 0x000E23A0 File Offset: 0x000E05A0
		public unsafe static void Copy(IntPtr source, float[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (float* ptr = &destination[startIndex])
			{
				void* fixed_destination_element = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, fixed_destination_element);
			}
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed double-precision floating-point number array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x0600416D RID: 16749 RVA: 0x000E23D8 File Offset: 0x000E05D8
		public unsafe static void Copy(IntPtr source, double[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (double* ptr = &destination[startIndex])
			{
				void* fixed_destination_element = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, fixed_destination_element);
			}
		}

		/// <summary>Copies data from an unmanaged memory pointer to a managed <see cref="T:System.IntPtr" /> array.</summary>
		/// <param name="source">The memory pointer to copy from.</param>
		/// <param name="destination">The array to copy to.</param>
		/// <param name="startIndex">The zero-based index in the destination array where copying should start.</param>
		/// <param name="length">The number of array elements to copy.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" />, <paramref name="destination" />, <paramref name="startIndex" />, or <paramref name="length" /> is <see langword="null" />.</exception>
		// Token: 0x0600416E RID: 16750 RVA: 0x000E2410 File Offset: 0x000E0610
		public unsafe static void Copy(IntPtr source, IntPtr[] destination, int startIndex, int length)
		{
			if (Marshal.skip_fixed(destination, startIndex))
			{
				Marshal.copy_from_unmanaged(source, startIndex, destination, length);
				return;
			}
			fixed (IntPtr* ptr = &destination[startIndex])
			{
				void* fixed_destination_element = (void*)ptr;
				Marshal.copy_from_unmanaged_fixed(source, startIndex, destination, length, fixed_destination_element);
			}
		}

		/// <summary>Aggregates a managed object with the specified COM object.</summary>
		/// <param name="pOuter">The outer <see langword="IUnknown" /> pointer.</param>
		/// <param name="o">An object to aggregate.</param>
		/// <returns>The inner <see langword="IUnknown" /> pointer of the managed object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> is a Windows Runtime object.</exception>
		// Token: 0x0600416F RID: 16751 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static IntPtr CreateAggregatedObject(IntPtr pOuter, object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Aggregates a managed object of the specified type with the specified COM object.</summary>
		/// <param name="pOuter">The outer IUnknown pointer.</param>
		/// <param name="o">The managed object to aggregate.</param>
		/// <typeparam name="T">The type of the managed object to aggregate.</typeparam>
		/// <returns>The inner IUnknown pointer of the managed object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> is a Windows Runtime object.</exception>
		// Token: 0x06004170 RID: 16752 RVA: 0x000E2448 File Offset: 0x000E0648
		public static IntPtr CreateAggregatedObject<T>(IntPtr pOuter, T o)
		{
			return Marshal.CreateAggregatedObject(pOuter, o);
		}

		/// <summary>Wraps the specified COM object in an object of the specified type.</summary>
		/// <param name="o">The object to be wrapped.</param>
		/// <param name="t">The type of wrapper to create.</param>
		/// <returns>The newly wrapped object that is an instance of the desired type.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> must derive from <see langword="__ComObject" />.  
		/// -or-  
		/// <paramref name="t" /> is a Windows Runtime type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="t" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> cannot be converted to the destination type because it does not support all required interfaces.</exception>
		// Token: 0x06004171 RID: 16753 RVA: 0x000E2458 File Offset: 0x000E0658
		public static object CreateWrapperOfType(object o, Type t)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new ArgumentException("o must derive from __ComObject", "o");
			}
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			foreach (Type type in o.GetType().GetInterfaces())
			{
				if (type.IsImport && _ComObject.GetInterface(type) == IntPtr.Zero)
				{
					throw new InvalidCastException();
				}
			}
			return ComInteropProxy.GetProxy(_ComObject.IUnknown, t).GetTransparentProxy();
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Wraps the specified COM object in an object of the specified type.</summary>
		/// <param name="o">The object to be wrapped.</param>
		/// <typeparam name="T">The type of object to wrap.</typeparam>
		/// <typeparam name="TWrapper">The type of object to return.</typeparam>
		/// <returns>The newly wrapped object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <typeparamref name="T" /> must derive from <see langword="__ComObject" />.  
		/// -or-  
		/// <typeparamref name="T" /> is a Windows Runtime type.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> cannot be converted to the <paramref name="TWrapper" /> because it does not support all required interfaces.</exception>
		// Token: 0x06004172 RID: 16754 RVA: 0x000E24E3 File Offset: 0x000E06E3
		public static TWrapper CreateWrapperOfType<T, TWrapper>(T o)
		{
			return (TWrapper)((object)Marshal.CreateWrapperOfType(o, typeof(TWrapper)));
		}

		/// <summary>Frees all substructures that the specified unmanaged memory block points to.</summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory.</param>
		/// <param name="structuretype">Type of a formatted class. This provides the layout information necessary to delete the buffer in the <paramref name="ptr" /> parameter.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="structureType" /> has an automatic layout. Use sequential or explicit instead.</exception>
		// Token: 0x06004173 RID: 16755
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void DestroyStructure(IntPtr ptr, Type structuretype);

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Frees all substructures of a specified type that the specified unmanaged memory block points to.</summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory.</param>
		/// <typeparam name="T">The type of the formatted structure. This provides the layout information necessary to delete the buffer in the <paramref name="ptr" /> parameter.</typeparam>
		/// <exception cref="T:System.ArgumentException">
		///   <typeparamref name="T" /> has an automatic layout. Use sequential or explicit instead.</exception>
		// Token: 0x06004174 RID: 16756 RVA: 0x000E24FF File Offset: 0x000E06FF
		public static void DestroyStructure<T>(IntPtr ptr)
		{
			Marshal.DestroyStructure(ptr, typeof(T));
		}

		/// <summary>Frees a <see langword="BSTR" /> using the COM SysFreeString function.</summary>
		/// <param name="ptr">The address of the BSTR to be freed.</param>
		// Token: 0x06004175 RID: 16757
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeBSTR(IntPtr ptr);

		/// <summary>Frees a block of memory allocated by the unmanaged COM task memory allocator.</summary>
		/// <param name="ptr">The address of the memory to be freed.</param>
		// Token: 0x06004176 RID: 16758
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeCoTaskMem(IntPtr ptr);

		/// <summary>Frees memory previously allocated from the unmanaged memory of the process.</summary>
		/// <param name="hglobal">The handle returned by the original matching call to <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />.</param>
		// Token: 0x06004177 RID: 16759
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FreeHGlobal(IntPtr hglobal);

		// Token: 0x06004178 RID: 16760 RVA: 0x000E2514 File Offset: 0x000E0714
		private static void ClearBSTR(IntPtr ptr)
		{
			int num = Marshal.ReadInt32(ptr, -4);
			for (int i = 0; i < num; i++)
			{
				Marshal.WriteByte(ptr, i, 0);
			}
		}

		/// <summary>Frees a BSTR pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToBSTR(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the <see langword="BSTR" /> to free.</param>
		// Token: 0x06004179 RID: 16761 RVA: 0x000E253E File Offset: 0x000E073E
		public static void ZeroFreeBSTR(IntPtr s)
		{
			Marshal.ClearBSTR(s);
			Marshal.FreeBSTR(s);
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000E254C File Offset: 0x000E074C
		private static void ClearAnsi(IntPtr ptr)
		{
			int num = 0;
			while (Marshal.ReadByte(ptr, num) != 0)
			{
				Marshal.WriteByte(ptr, num, 0);
				num++;
			}
		}

		// Token: 0x0600417B RID: 16763 RVA: 0x000E2574 File Offset: 0x000E0774
		private static void ClearUnicode(IntPtr ptr)
		{
			int num = 0;
			while (Marshal.ReadInt16(ptr, num) != 0)
			{
				Marshal.WriteInt16(ptr, num, 0);
				num += 2;
			}
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemAnsi(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		// Token: 0x0600417C RID: 16764 RVA: 0x000E259A File Offset: 0x000E079A
		public static void ZeroFreeCoTaskMemAnsi(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeCoTaskMem(s);
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToCoTaskMemUnicode(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		// Token: 0x0600417D RID: 16765 RVA: 0x000E25A8 File Offset: 0x000E07A8
		public static void ZeroFreeCoTaskMemUnicode(IntPtr s)
		{
			Marshal.ClearUnicode(s);
			Marshal.FreeCoTaskMem(s);
		}

		// Token: 0x0600417E RID: 16766 RVA: 0x000E259A File Offset: 0x000E079A
		public static void ZeroFreeCoTaskMemUTF8(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeCoTaskMem(s);
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocAnsi(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		// Token: 0x0600417F RID: 16767 RVA: 0x000E25B6 File Offset: 0x000E07B6
		public static void ZeroFreeGlobalAllocAnsi(IntPtr s)
		{
			Marshal.ClearAnsi(s);
			Marshal.FreeHGlobal(s);
		}

		/// <summary>Frees an unmanaged string pointer that was allocated using the <see cref="M:System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(System.Security.SecureString)" /> method.</summary>
		/// <param name="s">The address of the unmanaged string to free.</param>
		// Token: 0x06004180 RID: 16768 RVA: 0x000E25C4 File Offset: 0x000E07C4
		public static void ZeroFreeGlobalAllocUnicode(IntPtr s)
		{
			Marshal.ClearUnicode(s);
			Marshal.FreeHGlobal(s);
		}

		/// <summary>Returns the globally unique identifier (GUID) for the specified type, or generates a GUID using the algorithm used by the Type Library Exporter (Tlbexp.exe).</summary>
		/// <param name="type">The type to generate a GUID for.</param>
		/// <returns>An identifier for the specified type.</returns>
		// Token: 0x06004181 RID: 16769 RVA: 0x000E25D2 File Offset: 0x000E07D2
		public static Guid GenerateGuidForType(Type type)
		{
			return type.GUID;
		}

		/// <summary>Returns a programmatic identifier (ProgID) for the specified type.</summary>
		/// <param name="type">The type to get a ProgID for.</param>
		/// <returns>The ProgID of the specified type.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="type" /> parameter is not a class that can be create by COM. The class must be public, have a public default constructor, and be COM visible.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="type" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004182 RID: 16770 RVA: 0x000E25DC File Offset: 0x000E07DC
		public static string GenerateProgIdForType(Type type)
		{
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(type))
			{
				if (customAttributeData.Constructor.DeclaringType.Name == "ProgIdAttribute")
				{
					IList<CustomAttributeTypedArgument> constructorArguments = customAttributeData.ConstructorArguments;
					string text = customAttributeData.ConstructorArguments[0].Value as string;
					if (text == null)
					{
						text = string.Empty;
					}
					return text;
				}
			}
			return type.FullName;
		}

		/// <summary>Obtains a running instance of the specified object from the running object table (ROT).</summary>
		/// <param name="progID">The programmatic identifier (ProgID) of the object that was requested.</param>
		/// <returns>The object that was requested; otherwise <see langword="null" />. You can cast this object to any COM interface that it supports.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">The object was not found.</exception>
		// Token: 0x06004183 RID: 16771 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static object GetActiveObject(string progID)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004184 RID: 16772
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetCCW(object o, Type T);

		// Token: 0x06004185 RID: 16773 RVA: 0x000E2678 File Offset: 0x000E0878
		private static IntPtr GetComInterfaceForObjectInternal(object o, Type T)
		{
			if (Marshal.IsComObject(o))
			{
				return ((__ComObject)o).GetInterface(T);
			}
			return Marshal.GetCCW(o, T);
		}

		/// <summary>Returns a pointer to an IUnknown interface that represents the specified interface on the specified object. Custom query interface access is enabled by default.</summary>
		/// <param name="o">The object that provides the interface.</param>
		/// <param name="T">The type of interface that is requested.</param>
		/// <returns>The interface pointer that represents the specified interface for the object.</returns>
		/// <exception cref="T:System.ArgumentException">The <typeparamref name="T" /> parameter is not an interface.  
		///  -or-  
		///  The type is not visible to COM.  
		///  -or-  
		///  The <typeparamref name="T" /> parameter is a generic type definition.</exception>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="o" /> parameter does not support the requested interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="o" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <typeparamref name="T" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004186 RID: 16774 RVA: 0x000E2696 File Offset: 0x000E0896
		public static IntPtr GetComInterfaceForObject(object o, Type T)
		{
			IntPtr comInterfaceForObjectInternal = Marshal.GetComInterfaceForObjectInternal(o, T);
			Marshal.AddRef(comInterfaceForObjectInternal);
			return comInterfaceForObjectInternal;
		}

		/// <summary>Returns a pointer to an IUnknown interface that represents the specified interface on the specified object. Custom query interface access is controlled by the specified customization mode.</summary>
		/// <param name="o">The object that provides the interface.</param>
		/// <param name="T">The type of interface that is requested.</param>
		/// <param name="mode">One of the enumeration values that indicates whether to apply an <see langword="IUnknown::QueryInterface" /> customization that is supplied by an <see cref="T:System.Runtime.InteropServices.ICustomQueryInterface" />.</param>
		/// <returns>The interface pointer that represents the interface for the object.</returns>
		/// <exception cref="T:System.ArgumentException">The <typeparamref name="T" /> parameter is not an interface.  
		///  -or-  
		///  The type is not visible to COM.  
		///  -or-  
		///  The <typeparamref name="T" /> parameter is a generic type definition.</exception>
		/// <exception cref="T:System.InvalidCastException">The object <paramref name="o" /> does not support the requested interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="o" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <typeparamref name="T" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004187 RID: 16775 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static IntPtr GetComInterfaceForObject(object o, Type T, CustomQueryInterfaceMode mode)
		{
			throw new NotImplementedException();
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Returns a pointer to an IUnknown interface that represents the specified interface on an object of the specified type. Custom query interface access is enabled by default.</summary>
		/// <param name="o">The object that provides the interface.</param>
		/// <typeparam name="T">The type of <paramref name="o" />.</typeparam>
		/// <typeparam name="TInterface">The type of interface to return.</typeparam>
		/// <returns>The interface pointer that represents the <paramref name="TInterface" /> interface.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="TInterface" /> parameter is not an interface.  
		///  -or-  
		///  The type is not visible to COM.  
		///  -or-  
		///  The <typeparamref name="T" /> parameter is an open generic type.</exception>
		/// <exception cref="T:System.InvalidCastException">The <paramref name="o" /> parameter does not support the <paramref name="TInterface" /> interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="o" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004188 RID: 16776 RVA: 0x000E26A6 File Offset: 0x000E08A6
		public static IntPtr GetComInterfaceForObject<T, TInterface>(T o)
		{
			return Marshal.GetComInterfaceForObject(o, typeof(T));
		}

		/// <summary>Returns an interface pointer that represents the specified interface for an object, if the caller is in the same context as that object.</summary>
		/// <param name="o">The object that provides the interface.</param>
		/// <param name="t">The type of interface that is requested.</param>
		/// <returns>The interface pointer specified by <paramref name="t" /> that represents the interface for the specified object, or <see langword="null" /> if the caller is not in the same context as the object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not an interface.  
		/// -or-  
		/// The type is not visible to COM.</exception>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> does not support the requested interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="t" /> is <see langword="null" />.</exception>
		// Token: 0x06004189 RID: 16777 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static IntPtr GetComInterfaceForObjectInContext(object o, Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves data that is referenced by the specified key from the specified COM object.</summary>
		/// <param name="obj">The COM object that contains the data that you want.</param>
		/// <param name="key">The key in the internal hash table of <paramref name="obj" /> to retrieve the data from.</param>
		/// <returns>The data represented by the <paramref name="key" /> parameter in the internal hash table of the <paramref name="obj" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a COM object.  
		/// -or-  
		/// <paramref name="obj" /> is a Windows Runtime object.</exception>
		// Token: 0x0600418A RID: 16778 RVA: 0x000E26BD File Offset: 0x000E08BD
		public static object GetComObjectData(object obj, object key)
		{
			throw new NotSupportedException("MSDN states user code should never need to call this method.");
		}

		// Token: 0x0600418B RID: 16779
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetComSlotForMethodInfoInternal(MemberInfo m);

		/// <summary>Retrieves the virtual function table (v-table or VTBL) slot for a specified <see cref="T:System.Reflection.MemberInfo" /> type when that type is exposed to COM.</summary>
		/// <param name="m">An object that represents an interface method.</param>
		/// <returns>The VTBL slot <paramref name="m" /> identifier when it is exposed to COM.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="m" /> parameter is not a <see cref="T:System.Reflection.MemberInfo" /> object.  
		///  -or-  
		///  The <paramref name="m" /> parameter is not an interface method.</exception>
		// Token: 0x0600418C RID: 16780 RVA: 0x000E26CC File Offset: 0x000E08CC
		public static int GetComSlotForMethodInfo(MemberInfo m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			if (!(m is MethodInfo))
			{
				throw new ArgumentException("The MemberInfo must be an interface method.", "m");
			}
			if (!m.DeclaringType.IsInterface)
			{
				throw new ArgumentException("The MemberInfo must be an interface method.", "m");
			}
			return Marshal.GetComSlotForMethodInfoInternal(m);
		}

		/// <summary>Retrieves the last slot in the virtual function table (v-table or VTBL) of a type when exposed to COM.</summary>
		/// <param name="t">A type that represents an interface or class.</param>
		/// <returns>The last VTBL slot of the interface when exposed to COM. If the <paramref name="t" /> parameter is a class, the returned VTBL slot is the last slot in the interface that is generated from the class.</returns>
		// Token: 0x0600418D RID: 16781 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static int GetEndComSlot(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a computer-independent description of an exception, and information about the state that existed for the thread when the exception occurred.</summary>
		/// <returns>A pointer to an EXCEPTION_POINTERS structure.</returns>
		// Token: 0x0600418E RID: 16782 RVA: 0x000479F8 File Offset: 0x00045BF8
		[ComVisible(true)]
		public static IntPtr GetExceptionPointers()
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the instance handle (HINSTANCE) for the specified module.</summary>
		/// <param name="m">The module whose HINSTANCE is desired.</param>
		/// <returns>The HINSTANCE for <paramref name="m" />; or -1 if the module does not have an HINSTANCE.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600418F RID: 16783 RVA: 0x000E2728 File Offset: 0x000E0928
		public static IntPtr GetHINSTANCE(Module m)
		{
			if (m == null)
			{
				throw new ArgumentNullException("m");
			}
			RuntimeModule runtimeModule = m as RuntimeModule;
			if (runtimeModule != null)
			{
				return RuntimeModule.GetHINSTANCE(runtimeModule.MonoModule);
			}
			return (IntPtr)(-1);
		}

		/// <summary>Retrieves a code that identifies the type of the exception that occurred.</summary>
		/// <returns>The type of the exception.</returns>
		// Token: 0x06004190 RID: 16784 RVA: 0x0001B98B File Offset: 0x00019B8B
		public static int GetExceptionCode()
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Converts the specified exception to an HRESULT.</summary>
		/// <param name="e">The exception to convert to an HRESULT.</param>
		/// <returns>The HRESULT mapped to the supplied exception.</returns>
		// Token: 0x06004191 RID: 16785 RVA: 0x000E2768 File Offset: 0x000E0968
		public static int GetHRForException(Exception e)
		{
			if (e == null)
			{
				return 0;
			}
			ManagedErrorInfo errorInfo = new ManagedErrorInfo(e);
			Marshal.SetErrorInfo(0, errorInfo);
			return e._HResult;
		}

		/// <summary>Returns the HRESULT corresponding to the last error incurred by Win32 code executed using <see cref="T:System.Runtime.InteropServices.Marshal" />.</summary>
		/// <returns>The HRESULT corresponding to the last Win32 error code.</returns>
		// Token: 0x06004192 RID: 16786 RVA: 0x000479F8 File Offset: 0x00045BF8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int GetHRForLastWin32Error()
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004193 RID: 16787
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIDispatchForObjectInternal(object o);

		/// <summary>Returns an IDispatch interface from a managed object.</summary>
		/// <param name="o">The object whose <see langword="IDispatch" /> interface is requested.</param>
		/// <returns>The <see langword="IDispatch" /> pointer for the <paramref name="o" /> parameter.</returns>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> does not support the requested interface.</exception>
		// Token: 0x06004194 RID: 16788 RVA: 0x000E278F File Offset: 0x000E098F
		public static IntPtr GetIDispatchForObject(object o)
		{
			IntPtr idispatchForObjectInternal = Marshal.GetIDispatchForObjectInternal(o);
			Marshal.AddRef(idispatchForObjectInternal);
			return idispatchForObjectInternal;
		}

		/// <summary>Returns an IDispatch interface pointer from a managed object, if the caller is in the same context as that object.</summary>
		/// <param name="o">The object whose <see langword="IDispatch" /> interface is requested.</param>
		/// <returns>The <see langword="IDispatch" /> interface pointer for the specified object, or <see langword="null" /> if the caller is not in the same context as the specified object.</returns>
		/// <exception cref="T:System.InvalidCastException">
		///   <paramref name="o" /> does not support the requested interface.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is <see langword="null" />.</exception>
		// Token: 0x06004195 RID: 16789 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static IntPtr GetIDispatchForObjectInContext(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a <see cref="T:System.Runtime.InteropServices.ComTypes.ITypeInfo" /> interface from a managed type.</summary>
		/// <param name="t">The type whose <see langword="ITypeInfo" /> interface is being requested.</param>
		/// <returns>A pointer to the <see langword="ITypeInfo" /> interface for the <paramref name="t" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not a visible type to COM.  
		/// -or-  
		/// <paramref name="t" /> is a Windows Runtime type.</exception>
		/// <exception cref="T:System.Runtime.InteropServices.COMException">A type library is registered for the assembly that contains the type, but the type definition cannot be found.</exception>
		// Token: 0x06004196 RID: 16790 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static IntPtr GetITypeInfoForType(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns an IUnknown interface from a managed object, if the caller is in the same context as that object.</summary>
		/// <param name="o">The object whose <see langword="IUnknown" /> interface is requested.</param>
		/// <returns>The <see langword="IUnknown" /> pointer for the specified object, or <see langword="null" /> if the caller is not in the same context as the specified object.</returns>
		// Token: 0x06004197 RID: 16791 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static IntPtr GetIUnknownForObjectInContext(object o)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a pointer to a runtime-generated function that marshals a call from managed to unmanaged code.</summary>
		/// <param name="pfnMethodToWrap">A pointer to the method to marshal.</param>
		/// <param name="pbSignature">A pointer to the method signature.</param>
		/// <param name="cbSignature">The number of bytes in <paramref name="pbSignature" />.</param>
		/// <returns>A pointer to the function that will marshal a call from the <paramref name="pfnMethodToWrap" /> parameter to unmanaged code.</returns>
		// Token: 0x06004198 RID: 16792 RVA: 0x000479F8 File Offset: 0x00045BF8
		[Obsolete("This method has been deprecated")]
		public static IntPtr GetManagedThunkForUnmanagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves a <see cref="T:System.Reflection.MemberInfo" /> object for the specified virtual function table (v-table or VTBL) slot.</summary>
		/// <param name="t">The type for which the <see cref="T:System.Reflection.MemberInfo" /> is to be retrieved.</param>
		/// <param name="slot">The VTBL slot.</param>
		/// <param name="memberType">On successful return, one of the enumeration values that specifies the type of the member.</param>
		/// <returns>The object that represents the member at the specified VTBL slot.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not visible from COM.</exception>
		// Token: 0x06004199 RID: 16793 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static MemberInfo GetMethodInfoForComSlot(Type t, int slot, ref ComMemberType memberType)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600419A RID: 16794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetIUnknownForObjectInternal(object o);

		/// <summary>Returns an IUnknown interface from a managed object.</summary>
		/// <param name="o">The object whose <see langword="IUnknown" /> interface is requested.</param>
		/// <returns>The <see langword="IUnknown" /> pointer for the <paramref name="o" /> parameter.</returns>
		// Token: 0x0600419B RID: 16795 RVA: 0x000E279E File Offset: 0x000E099E
		public static IntPtr GetIUnknownForObject(object o)
		{
			IntPtr iunknownForObjectInternal = Marshal.GetIUnknownForObjectInternal(o);
			Marshal.AddRef(iunknownForObjectInternal);
			return iunknownForObjectInternal;
		}

		/// <summary>Converts an object to a COM VARIANT.</summary>
		/// <param name="obj">The object for which to get a COM VARIANT.</param>
		/// <param name="pDstNativeVariant">A pointer to receive the VARIANT that corresponds to the <paramref name="obj" /> parameter.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="obj" /> parameter is an instance of a generic type.</exception>
		// Token: 0x0600419C RID: 16796 RVA: 0x000E27B0 File Offset: 0x000E09B0
		public static void GetNativeVariantForObject(object obj, IntPtr pDstNativeVariant)
		{
			Variant structure = default(Variant);
			structure.SetValue(obj);
			Marshal.StructureToPtr<Variant>(structure, pDstNativeVariant, false);
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Converts an object of a specified type to a COM VARIANT.</summary>
		/// <param name="obj">The object for which to get a COM VARIANT.</param>
		/// <param name="pDstNativeVariant">A pointer to receive the VARIANT that corresponds to the <paramref name="obj" /> parameter.</param>
		/// <typeparam name="T">The type of the object to convert.</typeparam>
		// Token: 0x0600419D RID: 16797 RVA: 0x000E27D5 File Offset: 0x000E09D5
		public static void GetNativeVariantForObject<T>(T obj, IntPtr pDstNativeVariant)
		{
			Marshal.GetNativeVariantForObject(obj, pDstNativeVariant);
		}

		// Token: 0x0600419E RID: 16798
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object GetObjectForCCW(IntPtr pUnk);

		/// <summary>Returns an instance of a type that represents a COM object by a pointer to its IUnknown interface.</summary>
		/// <param name="pUnk">A pointer to the <see langword="IUnknown" /> interface.</param>
		/// <returns>An object that represents the specified unmanaged COM object.</returns>
		// Token: 0x0600419F RID: 16799 RVA: 0x000E27E4 File Offset: 0x000E09E4
		public static object GetObjectForIUnknown(IntPtr pUnk)
		{
			object obj = Marshal.GetObjectForCCW(pUnk);
			if (obj == null)
			{
				obj = ComInteropProxy.GetProxy(pUnk, typeof(__ComObject)).GetTransparentProxy();
			}
			return obj;
		}

		/// <summary>Converts a COM VARIANT to an object.</summary>
		/// <param name="pSrcNativeVariant">A pointer to a COM VARIANT.</param>
		/// <returns>An object that corresponds to the <paramref name="pSrcNativeVariant" /> parameter.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException">
		///   <paramref name="pSrcNativeVariant" /> is not a valid VARIANT type.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="pSrcNativeVariant" /> has an unsupported type.</exception>
		// Token: 0x060041A0 RID: 16800 RVA: 0x000E2814 File Offset: 0x000E0A14
		public static object GetObjectForNativeVariant(IntPtr pSrcNativeVariant)
		{
			return ((Variant)Marshal.PtrToStructure(pSrcNativeVariant, typeof(Variant))).GetValue();
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Converts a COM VARIANT to an object of a specified type.</summary>
		/// <param name="pSrcNativeVariant">A pointer to a COM VARIANT.</param>
		/// <typeparam name="T">The type to which to convert the COM VARIANT.</typeparam>
		/// <returns>An object of the specified type that corresponds to the <paramref name="pSrcNativeVariant" /> parameter.</returns>
		/// <exception cref="T:System.Runtime.InteropServices.InvalidOleVariantTypeException">
		///   <paramref name="pSrcNativeVariant" /> is not a valid VARIANT type.</exception>
		/// <exception cref="T:System.NotSupportedException">
		///   <paramref name="pSrcNativeVariant" /> has an unsupported type.</exception>
		// Token: 0x060041A1 RID: 16801 RVA: 0x000E2840 File Offset: 0x000E0A40
		public static T GetObjectForNativeVariant<T>(IntPtr pSrcNativeVariant)
		{
			return (T)((object)((Variant)Marshal.PtrToStructure(pSrcNativeVariant, typeof(Variant))).GetValue());
		}

		/// <summary>Converts an array of COM VARIANTs to an array of objects.</summary>
		/// <param name="aSrcNativeVariant">A pointer to the first element of an array of COM VARIANTs.</param>
		/// <param name="cVars">The count of COM VARIANTs in <paramref name="aSrcNativeVariant" />.</param>
		/// <returns>An object array that corresponds to <paramref name="aSrcNativeVariant" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cVars" /> is a negative number.</exception>
		// Token: 0x060041A2 RID: 16802 RVA: 0x000E2870 File Offset: 0x000E0A70
		public static object[] GetObjectsForNativeVariants(IntPtr aSrcNativeVariant, int cVars)
		{
			if (cVars < 0)
			{
				throw new ArgumentOutOfRangeException("cVars", "cVars cannot be a negative number.");
			}
			object[] array = new object[cVars];
			for (int i = 0; i < cVars; i++)
			{
				array[i] = Marshal.GetObjectForNativeVariant((IntPtr)(aSrcNativeVariant.ToInt64() + (long)(i * Marshal.SizeOf(typeof(Variant)))));
			}
			return array;
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Converts an array of COM VARIANTs to an array of a specified type.</summary>
		/// <param name="aSrcNativeVariant">A pointer to the first element of an array of COM VARIANTs.</param>
		/// <param name="cVars">The count of COM VARIANTs in <paramref name="aSrcNativeVariant" />.</param>
		/// <typeparam name="T">The type of the array to return.</typeparam>
		/// <returns>An array of <typeparamref name="T" /> objects that corresponds to <paramref name="aSrcNativeVariant" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="cVars" /> is a negative number.</exception>
		// Token: 0x060041A3 RID: 16803 RVA: 0x000E28CC File Offset: 0x000E0ACC
		public static T[] GetObjectsForNativeVariants<T>(IntPtr aSrcNativeVariant, int cVars)
		{
			if (cVars < 0)
			{
				throw new ArgumentOutOfRangeException("cVars", "cVars cannot be a negative number.");
			}
			T[] array = new T[cVars];
			for (int i = 0; i < cVars; i++)
			{
				array[i] = Marshal.GetObjectForNativeVariant<T>((IntPtr)(aSrcNativeVariant.ToInt64() + (long)(i * Marshal.SizeOf(typeof(Variant)))));
			}
			return array;
		}

		/// <summary>Gets the first slot in the virtual function table (v-table or VTBL) that contains user-defined methods.</summary>
		/// <param name="t">A type that represents an interface.</param>
		/// <returns>The first VTBL slot that contains user-defined methods. The first slot is 3 if the interface is based on IUnknown, and 7 if the interface is based on IDispatch.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not visible from COM.</exception>
		// Token: 0x060041A4 RID: 16804 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static int GetStartComSlot(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Converts a fiber cookie into the corresponding <see cref="T:System.Threading.Thread" /> instance.</summary>
		/// <param name="cookie">An integer that represents a fiber cookie.</param>
		/// <returns>A thread that corresponds to the <paramref name="cookie" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="cookie" /> parameter is 0.</exception>
		// Token: 0x060041A5 RID: 16805 RVA: 0x000479F8 File Offset: 0x00045BF8
		[Obsolete("This method has been deprecated")]
		public static Thread GetThreadFromFiberCookie(int cookie)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns a managed object of a specified type that represents a COM object.</summary>
		/// <param name="pUnk">A pointer to the <see langword="IUnknown" /> interface of the unmanaged object.</param>
		/// <param name="t">The type of the requested managed class.</param>
		/// <returns>An instance of the class corresponding to the <see cref="T:System.Type" /> object that represents the requested unmanaged COM object.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="t" /> is not attributed with <see cref="T:System.Runtime.InteropServices.ComImportAttribute" />.  
		/// -or-  
		/// <paramref name="t" /> is a Windows Runtime type.</exception>
		// Token: 0x060041A6 RID: 16806 RVA: 0x000E292C File Offset: 0x000E0B2C
		public static object GetTypedObjectForIUnknown(IntPtr pUnk, Type t)
		{
			__ComObject _ComObject = (__ComObject)new ComInteropProxy(pUnk, t).GetTransparentProxy();
			foreach (Type type in t.GetInterfaces())
			{
				if ((type.Attributes & TypeAttributes.Import) == TypeAttributes.Import && _ComObject.GetInterface(type) == IntPtr.Zero)
				{
					return null;
				}
			}
			return _ComObject;
		}

		/// <summary>Converts an unmanaged ITypeInfo object into a managed <see cref="T:System.Type" /> object.</summary>
		/// <param name="piTypeInfo">The <see langword="ITypeInfo" /> interface to marshal.</param>
		/// <returns>A managed type that represents the unmanaged <see langword="ITypeInfo" /> object.</returns>
		// Token: 0x060041A7 RID: 16807 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static Type GetTypeForITypeInfo(IntPtr piTypeInfo)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the name of the type represented by an ITypeInfo object.</summary>
		/// <param name="pTI">An object that represents an <see langword="ITypeInfo" /> pointer.</param>
		/// <returns>The name of the type that the <paramref name="pTI" /> parameter points to.</returns>
		// Token: 0x060041A8 RID: 16808 RVA: 0x000479F8 File Offset: 0x00045BF8
		[Obsolete]
		public static string GetTypeInfoName(UCOMITypeInfo pTI)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the library identifier (LIBID) of a type library.</summary>
		/// <param name="pTLB">The type library whose LIBID is to be retrieved.</param>
		/// <returns>The LIBID of the type library that the <paramref name="pTLB" /> parameter points to.</returns>
		// Token: 0x060041A9 RID: 16809 RVA: 0x000479F8 File Offset: 0x00045BF8
		[Obsolete]
		public static Guid GetTypeLibGuid(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the library identifier (LIBID) of a type library.</summary>
		/// <param name="typelib">The type library whose LIBID is to be retrieved.</param>
		/// <returns>The LIBID of the specified type library.</returns>
		// Token: 0x060041AA RID: 16810 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static Guid GetTypeLibGuid(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the library identifier (LIBID) that is assigned to a type library when it was exported from the specified assembly.</summary>
		/// <param name="asm">The assembly from which the type library was exported.</param>
		/// <returns>The LIBID that is assigned to a type library when it is exported from the specified assembly.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="asm" /> is <see langword="null" />.</exception>
		// Token: 0x060041AB RID: 16811 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static Guid GetTypeLibGuidForAssembly(Assembly asm)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the LCID of a type library.</summary>
		/// <param name="pTLB">The type library whose LCID is to be retrieved.</param>
		/// <returns>The LCID of the type library that the <paramref name="pTLB" /> parameter points to.</returns>
		// Token: 0x060041AC RID: 16812 RVA: 0x000479F8 File Offset: 0x00045BF8
		[Obsolete]
		public static int GetTypeLibLcid(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the LCID of a type library.</summary>
		/// <param name="typelib">The type library whose LCID is to be retrieved.</param>
		/// <returns>The LCID of the type library that the <paramref name="typelib" /> parameter points to.</returns>
		// Token: 0x060041AD RID: 16813 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static int GetTypeLibLcid(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the name of a type library.</summary>
		/// <param name="pTLB">The type library whose name is to be retrieved.</param>
		/// <returns>The name of the type library that the <paramref name="pTLB" /> parameter points to.</returns>
		// Token: 0x060041AE RID: 16814 RVA: 0x000479F8 File Offset: 0x00045BF8
		[Obsolete]
		public static string GetTypeLibName(UCOMITypeLib pTLB)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the name of a type library.</summary>
		/// <param name="typelib">The type library whose name is to be retrieved.</param>
		/// <returns>The name of the type library that the <paramref name="typelib" /> parameter points to.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="typelib" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060041AF RID: 16815 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static string GetTypeLibName(ITypeLib typelib)
		{
			throw new NotImplementedException();
		}

		/// <summary>Retrieves the version number of a type library that will be exported from the specified assembly.</summary>
		/// <param name="inputAssembly">A managed assembly.</param>
		/// <param name="majorVersion">The major version number.</param>
		/// <param name="minorVersion">The minor version number.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="inputAssembly" /> is <see langword="null" />.</exception>
		// Token: 0x060041B0 RID: 16816 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static void GetTypeLibVersionForAssembly(Assembly inputAssembly, out int majorVersion, out int minorVersion)
		{
			throw new NotImplementedException();
		}

		/// <summary>Gets a pointer to a runtime-generated function that marshals a call from unmanaged to managed code.</summary>
		/// <param name="pfnMethodToWrap">A pointer to the method to marshal.</param>
		/// <param name="pbSignature">A pointer to the method signature.</param>
		/// <param name="cbSignature">The number of bytes in <paramref name="pbSignature" />.</param>
		/// <returns>A pointer to a function that will marshal a call from <paramref name="pfnMethodToWrap" /> to managed code.</returns>
		// Token: 0x060041B1 RID: 16817 RVA: 0x000479F8 File Offset: 0x00045BF8
		[Obsolete("This method has been deprecated")]
		public static IntPtr GetUnmanagedThunkForManagedMethodPtr(IntPtr pfnMethodToWrap, IntPtr pbSignature, int cbSignature)
		{
			throw new NotImplementedException();
		}

		/// <summary>Indicates whether a type is visible to COM clients.</summary>
		/// <param name="t">The type to check for COM visibility.</param>
		/// <returns>
		///   <see langword="true" /> if the type is visible to COM; otherwise, <see langword="false" />.</returns>
		// Token: 0x060041B2 RID: 16818 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static bool IsTypeVisibleFromCom(Type t)
		{
			throw new NotImplementedException();
		}

		/// <summary>Calculates the number of bytes in unmanaged memory that are required to hold the parameters for the specified method.</summary>
		/// <param name="m">The method to be checked.</param>
		/// <returns>The number of bytes required to represent the method parameters in unmanaged memory.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="m" /> parameter is not a <see cref="T:System.Reflection.MethodInfo" /> object.</exception>
		// Token: 0x060041B3 RID: 16819 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static int NumParamBytes(MethodInfo m)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the type associated with the specified class identifier (CLSID).</summary>
		/// <param name="clsid">The CLSID of the type to return.</param>
		/// <returns>
		///   <see langword="System.__ComObject" /> regardless of whether the CLSID is valid.</returns>
		// Token: 0x060041B4 RID: 16820 RVA: 0x0001B98B File Offset: 0x00019B8B
		public static Type GetTypeFromCLSID(Guid clsid)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Retrieves the name of the type represented by an ITypeInfo object.</summary>
		/// <param name="typeInfo">An object that represents an <see langword="ITypeInfo" /> pointer.</param>
		/// <returns>The name of the type that the <paramref name="typeInfo" /> parameter points to.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="typeInfo" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060041B5 RID: 16821 RVA: 0x0001B98B File Offset: 0x00019B8B
		public static string GetTypeInfoName(ITypeInfo typeInfo)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Creates a unique Runtime Callable Wrapper (RCW) object for a given IUnknown interface.</summary>
		/// <param name="unknown">A managed pointer to an <see langword="IUnknown" /> interface.</param>
		/// <returns>A unique RCW for the specified <see langword="IUnknown" /> interface.</returns>
		// Token: 0x060041B6 RID: 16822 RVA: 0x0001B98B File Offset: 0x00019B8B
		public static object GetUniqueObjectForIUnknown(IntPtr unknown)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Indicates whether a specified object represents a COM object.</summary>
		/// <param name="o">The object to check.</param>
		/// <returns>
		///   <see langword="true" /> if the <paramref name="o" /> parameter is a COM type; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is <see langword="null" />.</exception>
		// Token: 0x060041B7 RID: 16823
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsComObject(object o);

		/// <summary>Returns the error code returned by the last unmanaged function that was called using platform invoke that has the <see cref="F:System.Runtime.InteropServices.DllImportAttribute.SetLastError" /> flag set.</summary>
		/// <returns>The last error code set by a call to the Win32 SetLastError function.</returns>
		// Token: 0x060041B8 RID: 16824
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int GetLastWin32Error();

		/// <summary>Returns the field offset of the unmanaged form of the managed class.</summary>
		/// <param name="t">A value type or formatted reference type that specifies the managed class. You must apply the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> to the class.</param>
		/// <param name="fieldName">The field within the <paramref name="t" /> parameter.</param>
		/// <returns>The offset, in bytes, for the <paramref name="fieldName" /> parameter within the specified class that is declared by platform invoke.</returns>
		/// <exception cref="T:System.ArgumentException">The class cannot be exported as a structure or the field is nonpublic. Beginning with the .NET Framework version 2.0, the field may be private.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="t" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060041B9 RID: 16825
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr OffsetOf(Type t, string fieldName);

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Returns the field offset of the unmanaged form of a specified managed class.</summary>
		/// <param name="fieldName">The name of the field in the <paramref name="T" /> type.</param>
		/// <typeparam name="T">A managed value type or formatted reference type. You must apply the <see cref="T:System.Runtime.InteropServices.StructLayoutAttribute" /> attribute to the class.</typeparam>
		/// <returns>The offset, in bytes, for the <paramref name="fieldName" /> parameter within the specified class that is declared by platform invoke.</returns>
		// Token: 0x060041BA RID: 16826 RVA: 0x000E298D File Offset: 0x000E0B8D
		public static IntPtr OffsetOf<T>(string fieldName)
		{
			return Marshal.OffsetOf(typeof(T), fieldName);
		}

		/// <summary>Executes one-time method setup tasks without calling the method.</summary>
		/// <param name="m">The method to be checked.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="m" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="m" /> parameter is not a <see cref="T:System.Reflection.MethodInfo" /> object.</exception>
		// Token: 0x060041BB RID: 16827
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Prelink(MethodInfo m);

		/// <summary>Performs a pre-link check for all methods on a class.</summary>
		/// <param name="c">The class whose methods are to be checked.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="c" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060041BC RID: 16828
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PrelinkAll(Type c);

		/// <summary>Copies all characters up to the first null character from an unmanaged ANSI string to a managed <see cref="T:System.String" />, and widens each ANSI character to Unicode.</summary>
		/// <param name="ptr">The address of the first character of the unmanaged string.</param>
		/// <returns>A managed string that holds a copy of the unmanaged ANSI string. If <paramref name="ptr" /> is <see langword="null" />, the method returns a null string.</returns>
		// Token: 0x060041BD RID: 16829
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringAnsi(IntPtr ptr);

		/// <summary>Allocates a managed <see cref="T:System.String" />, copies a specified number of characters from an unmanaged ANSI string into it, and widens each ANSI character to Unicode.</summary>
		/// <param name="ptr">The address of the first character of the unmanaged string.</param>
		/// <param name="len">The byte count of the input string to copy.</param>
		/// <returns>A managed string that holds a copy of the native ANSI string if the value of the <paramref name="ptr" /> parameter is not <see langword="null" />; otherwise, this method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="len" /> is less than zero.</exception>
		// Token: 0x060041BE RID: 16830
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringAnsi(IntPtr ptr, int len);

		// Token: 0x060041BF RID: 16831 RVA: 0x000E299F File Offset: 0x000E0B9F
		public static string PtrToStringUTF8(IntPtr ptr)
		{
			return Marshal.PtrToStringAnsi(ptr);
		}

		// Token: 0x060041C0 RID: 16832 RVA: 0x000E29A7 File Offset: 0x000E0BA7
		public static string PtrToStringUTF8(IntPtr ptr, int byteLen)
		{
			return Marshal.PtrToStringAnsi(ptr, byteLen);
		}

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies all characters up to the first null character from a string stored in unmanaged memory into it.</summary>
		/// <param name="ptr">For Unicode platforms, the address of the first Unicode character.  
		///  -or-  
		///  For ANSI plaforms, the address of the first ANSI character.</param>
		/// <returns>A managed string that holds a copy of the unmanaged string if the value of the <paramref name="ptr" /> parameter is not <see langword="null" />; otherwise, this method returns <see langword="null" />.</returns>
		// Token: 0x060041C1 RID: 16833 RVA: 0x000E29B0 File Offset: 0x000E0BB0
		public static string PtrToStringAuto(IntPtr ptr)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.PtrToStringAnsi(ptr);
			}
			return Marshal.PtrToStringUni(ptr);
		}

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies the specified number of characters from a string stored in unmanaged memory into it.</summary>
		/// <param name="ptr">For Unicode platforms, the address of the first Unicode character.  
		///  -or-  
		///  For ANSI plaforms, the address of the first ANSI character.</param>
		/// <param name="len">The number of characters to copy.</param>
		/// <returns>A managed string that holds a copy of the native string if the value of the <paramref name="ptr" /> parameter is not <see langword="null" />; otherwise, this method returns <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="len" /> is less than zero.</exception>
		// Token: 0x060041C2 RID: 16834 RVA: 0x000E29C7 File Offset: 0x000E0BC7
		public static string PtrToStringAuto(IntPtr ptr, int len)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.PtrToStringAnsi(ptr, len);
			}
			return Marshal.PtrToStringUni(ptr, len);
		}

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies all characters up to the first null character from an unmanaged Unicode string into it.</summary>
		/// <param name="ptr">The address of the first character of the unmanaged string.</param>
		/// <returns>A managed string that holds a copy of the unmanaged string if the value of the <paramref name="ptr" /> parameter is not <see langword="null" />; otherwise, this method returns <see langword="null" />.</returns>
		// Token: 0x060041C3 RID: 16835
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringUni(IntPtr ptr);

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies a specified number of characters from an unmanaged Unicode string into it.</summary>
		/// <param name="ptr">The address of the first character of the unmanaged string.</param>
		/// <param name="len">The number of Unicode characters to copy.</param>
		/// <returns>A managed string that holds a copy of the unmanaged string if the value of the <paramref name="ptr" /> parameter is not <see langword="null" />; otherwise, this method returns <see langword="null" />.</returns>
		// Token: 0x060041C4 RID: 16836
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringUni(IntPtr ptr, int len);

		/// <summary>Allocates a managed <see cref="T:System.String" /> and copies a binary string (BSTR) stored in unmanaged memory into it.</summary>
		/// <param name="ptr">The address of the first character of the unmanaged string.</param>
		/// <returns>A managed string that holds a copy of the unmanaged string.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ptr" /> equals <see cref="F:System.IntPtr.Zero" />.</exception>
		// Token: 0x060041C5 RID: 16837
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string PtrToStringBSTR(IntPtr ptr);

		/// <summary>Marshals data from an unmanaged block of memory to a managed object.</summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory.</param>
		/// <param name="structure">The object to which the data is to be copied. This must be an instance of a formatted class.</param>
		/// <exception cref="T:System.ArgumentException">Structure layout is not sequential or explicit.  
		///  -or-  
		///  Structure is a boxed value type.</exception>
		// Token: 0x060041C6 RID: 16838
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void PtrToStructure(IntPtr ptr, object structure);

		/// <summary>Marshals data from an unmanaged block of memory to a newly allocated managed object of the specified type.</summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory.</param>
		/// <param name="structureType">The type of object to be created. This object must represent a formatted class or a structure.</param>
		/// <returns>A managed object containing the data pointed to by the <paramref name="ptr" /> parameter.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="structureType" /> parameter layout is not sequential or explicit.  
		///  -or-  
		///  The <paramref name="structureType" /> parameter is a generic type definition.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="structureType" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.MissingMethodException">The class specified by <paramref name="structureType" /> does not have an accessible default constructor.</exception>
		// Token: 0x060041C7 RID: 16839
		[ComVisible(true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern object PtrToStructure(IntPtr ptr, Type structureType);

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Marshals data from an unmanaged block of memory to a managed object of the specified type.</summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory.</param>
		/// <param name="structure">The object to which the data is to be copied.</param>
		/// <typeparam name="T">The type of <paramref name="structure" />. This must be a formatted class.</typeparam>
		/// <exception cref="T:System.ArgumentException">Structure layout is not sequential or explicit.</exception>
		// Token: 0x060041C8 RID: 16840 RVA: 0x000E29E0 File Offset: 0x000E0BE0
		public static void PtrToStructure<T>(IntPtr ptr, T structure)
		{
			Marshal.PtrToStructure(ptr, structure);
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Marshals data from an unmanaged block of memory to a newly allocated managed object of the type specified by a generic type parameter.</summary>
		/// <param name="ptr">A pointer to an unmanaged block of memory.</param>
		/// <typeparam name="T">The type of the object to which the data is to be copied. This must be a formatted class or a structure.</typeparam>
		/// <returns>A managed object that contains the data that the <paramref name="ptr" /> parameter points to.</returns>
		/// <exception cref="T:System.ArgumentException">The layout of <typeparamref name="T" /> is not sequential or explicit.</exception>
		/// <exception cref="T:System.MissingMethodException">The class specified by <typeparamref name="T" /> does not have an accessible default constructor.</exception>
		// Token: 0x060041C9 RID: 16841 RVA: 0x000E29EE File Offset: 0x000E0BEE
		public static T PtrToStructure<T>(IntPtr ptr)
		{
			return (T)((object)Marshal.PtrToStructure(ptr, typeof(T)));
		}

		// Token: 0x060041CA RID: 16842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int QueryInterfaceInternal(IntPtr pUnk, ref Guid iid, out IntPtr ppv);

		/// <summary>Requests a pointer to a specified interface from a COM object.</summary>
		/// <param name="pUnk">The interface to be queried.</param>
		/// <param name="iid">The interface identifier (IID) of the requested interface.</param>
		/// <param name="ppv">When this method returns, contains a reference to the returned interface.</param>
		/// <returns>An HRESULT that indicates the success or failure of the call.</returns>
		// Token: 0x060041CB RID: 16843 RVA: 0x000E2A05 File Offset: 0x000E0C05
		public static int QueryInterface(IntPtr pUnk, ref Guid iid, out IntPtr ppv)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentNullException("pUnk");
			}
			return Marshal.QueryInterfaceInternal(pUnk, ref iid, out ppv);
		}

		/// <summary>Reads a single byte from unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory from which to read.</param>
		/// <returns>The byte read from unmanaged memory.</returns>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x060041CC RID: 16844 RVA: 0x000E2A27 File Offset: 0x000E0C27
		public unsafe static byte ReadByte(IntPtr ptr)
		{
			return *(byte*)((void*)ptr);
		}

		/// <summary>Reads a single byte at a given offset (or index) from unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <returns>The byte read from unmanaged memory at the given offset.</returns>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x060041CD RID: 16845 RVA: 0x000E2A30 File Offset: 0x000E0C30
		public unsafe static byte ReadByte(IntPtr ptr, int ofs)
		{
			return ((byte*)((void*)ptr))[ofs];
		}

		/// <summary>Reads a single byte at a given offset (or index) from unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the source object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <returns>The byte read from unmanaged memory at the given offset.</returns>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x060041CE RID: 16846 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SuppressUnmanagedCodeSecurity]
		public static byte ReadByte([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a 16-bit signed integer from unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory from which to read.</param>
		/// <returns>The 16-bit signed integer read from unmanaged memory.</returns>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x060041CF RID: 16847 RVA: 0x000E2A3C File Offset: 0x000E0C3C
		public unsafe static short ReadInt16(IntPtr ptr)
		{
			byte* ptr2 = (byte*)((void*)ptr);
			if ((ptr2 & 1U) == 0U)
			{
				return *(short*)ptr2;
			}
			short result;
			Buffer.Memcpy((byte*)(&result), (byte*)((void*)ptr), 2, true);
			return result;
		}

		/// <summary>Reads a 16-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <returns>The 16-bit signed integer read from unmanaged memory at the given offset.</returns>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x060041D0 RID: 16848 RVA: 0x000E2A6C File Offset: 0x000E0C6C
		public unsafe static short ReadInt16(IntPtr ptr, int ofs)
		{
			byte* ptr2 = (byte*)((void*)ptr) + ofs;
			if ((ptr2 & 1U) == 0U)
			{
				return *(short*)ptr2;
			}
			short result;
			Buffer.Memcpy((byte*)(&result), ptr2, 2, true);
			return result;
		}

		/// <summary>Reads a 16-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the source object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <returns>The 16-bit signed integer read from unmanaged memory at the given offset.</returns>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x060041D1 RID: 16849 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SuppressUnmanagedCodeSecurity]
		public static short ReadInt16([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a 32-bit signed integer from unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory from which to read.</param>
		/// <returns>The 32-bit signed integer read from unmanaged memory.</returns>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x060041D2 RID: 16850 RVA: 0x000E2A98 File Offset: 0x000E0C98
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static int ReadInt32(IntPtr ptr)
		{
			byte* ptr2 = (byte*)((void*)ptr);
			if ((ptr2 & 3U) == 0U)
			{
				return *(int*)ptr2;
			}
			int result;
			Buffer.Memcpy((byte*)(&result), ptr2, 4, true);
			return result;
		}

		/// <summary>Reads a 32-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <returns>The 32-bit signed integer read from unmanaged memory.</returns>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x060041D3 RID: 16851 RVA: 0x000E2AC4 File Offset: 0x000E0CC4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static int ReadInt32(IntPtr ptr, int ofs)
		{
			byte* ptr2 = (byte*)((void*)ptr) + ofs;
			if ((ptr2 & 3) == 0)
			{
				return *(int*)ptr2;
			}
			int result;
			Buffer.Memcpy((byte*)(&result), ptr2, 4, true);
			return result;
		}

		/// <summary>Reads a 32-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the source object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <returns>The 32-bit signed integer read from unmanaged memory at the given offset.</returns>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x060041D4 RID: 16852 RVA: 0x000479F8 File Offset: 0x00045BF8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		public static int ReadInt32([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a 64-bit signed integer from unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory from which to read.</param>
		/// <returns>The 64-bit signed integer read from unmanaged memory.</returns>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x060041D5 RID: 16853 RVA: 0x000E2AF0 File Offset: 0x000E0CF0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public unsafe static long ReadInt64(IntPtr ptr)
		{
			byte* ptr2 = (byte*)((void*)ptr);
			if ((ptr2 & 7U) == 0U)
			{
				return *(long*)((void*)ptr);
			}
			long result;
			Buffer.Memcpy((byte*)(&result), ptr2, 8, true);
			return result;
		}

		/// <summary>Reads a 64-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <returns>The 64-bit signed integer read from unmanaged memory at the given offset.</returns>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x060041D6 RID: 16854 RVA: 0x000E2B20 File Offset: 0x000E0D20
		public unsafe static long ReadInt64(IntPtr ptr, int ofs)
		{
			byte* ptr2 = (byte*)((void*)ptr) + ofs;
			if ((ptr2 & 7U) == 0U)
			{
				return *(long*)ptr2;
			}
			long result;
			Buffer.Memcpy((byte*)(&result), ptr2, 8, true);
			return result;
		}

		/// <summary>Reads a 64-bit signed integer at a given offset from unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the source object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <returns>The 64-bit signed integer read from unmanaged memory at the given offset.</returns>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x060041D7 RID: 16855 RVA: 0x000479F8 File Offset: 0x00045BF8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[SuppressUnmanagedCodeSecurity]
		public static long ReadInt64([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Reads a processor native-sized integer from unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory from which to read.</param>
		/// <returns>The integer read from unmanaged memory. A 32 bit integer is returned on 32 bit machines and a 64 bit integer is returned on 64 bit machines.</returns>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x060041D8 RID: 16856 RVA: 0x000E2B4B File Offset: 0x000E0D4B
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr(IntPtr ptr)
		{
			if (IntPtr.Size == 4)
			{
				return (IntPtr)Marshal.ReadInt32(ptr);
			}
			return (IntPtr)Marshal.ReadInt64(ptr);
		}

		/// <summary>Reads a processor native sized integer at a given offset from unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory from which to read.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <returns>The integer read from unmanaged memory at the given offset.</returns>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x060041D9 RID: 16857 RVA: 0x000E2B6C File Offset: 0x000E0D6C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr(IntPtr ptr, int ofs)
		{
			if (IntPtr.Size == 4)
			{
				return (IntPtr)Marshal.ReadInt32(ptr, ofs);
			}
			return (IntPtr)Marshal.ReadInt64(ptr, ofs);
		}

		/// <summary>Reads a processor native sized integer from unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the source object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before reading.</param>
		/// <returns>The integer read from unmanaged memory at the given offset.</returns>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x060041DA RID: 16858 RVA: 0x000479F8 File Offset: 0x00045BF8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static IntPtr ReadIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] object ptr, int ofs)
		{
			throw new NotImplementedException();
		}

		/// <summary>Resizes a block of memory previously allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" />.</summary>
		/// <param name="pv">A pointer to memory allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocCoTaskMem(System.Int32)" />.</param>
		/// <param name="cb">The new size of the allocated block.</param>
		/// <returns>An integer representing the address of the reallocated block of memory. This memory must be released with <see cref="M:System.Runtime.InteropServices.Marshal.FreeCoTaskMem(System.IntPtr)" />.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request.</exception>
		// Token: 0x060041DB RID: 16859
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ReAllocCoTaskMem(IntPtr pv, int cb);

		/// <summary>Resizes a block of memory previously allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />.</summary>
		/// <param name="pv">A pointer to memory allocated with <see cref="M:System.Runtime.InteropServices.Marshal.AllocHGlobal(System.IntPtr)" />.</param>
		/// <param name="cb">The new size of the allocated block. This is not a pointer; it is the byte count you are requesting, cast to type <see cref="T:System.IntPtr" />. If you pass a pointer, it is treated as a size.</param>
		/// <returns>A pointer to the reallocated memory. This memory must be released using <see cref="M:System.Runtime.InteropServices.Marshal.FreeHGlobal(System.IntPtr)" />.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to satisfy the request.</exception>
		// Token: 0x060041DC RID: 16860
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr ReAllocHGlobal(IntPtr pv, IntPtr cb);

		// Token: 0x060041DD RID: 16861
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ReleaseInternal(IntPtr pUnk);

		/// <summary>Decrements the reference count on the specified interface.</summary>
		/// <param name="pUnk">The interface to release.</param>
		/// <returns>The new value of the reference count on the interface specified by the <paramref name="pUnk" /> parameter.</returns>
		// Token: 0x060041DE RID: 16862 RVA: 0x000E2B8F File Offset: 0x000E0D8F
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Release(IntPtr pUnk)
		{
			if (pUnk == IntPtr.Zero)
			{
				throw new ArgumentNullException("pUnk");
			}
			return Marshal.ReleaseInternal(pUnk);
		}

		// Token: 0x060041DF RID: 16863
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ReleaseComObjectInternal(object co);

		/// <summary>Decrements the reference count of the Runtime Callable Wrapper (RCW) associated with the specified COM object.</summary>
		/// <param name="o">The COM object to release.</param>
		/// <returns>The new value of the reference count of the RCW associated with <paramref name="o" />. This value is typically zero since the RCW keeps just one reference to the wrapped COM object regardless of the number of managed clients calling it.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> is not a valid COM object.</exception>
		/// <exception cref="T:System.NullReferenceException">
		///   <paramref name="o" /> is <see langword="null" />.</exception>
		// Token: 0x060041E0 RID: 16864 RVA: 0x000E2BAF File Offset: 0x000E0DAF
		public static int ReleaseComObject(object o)
		{
			if (o == null)
			{
				throw new ArgumentException("Value cannot be null.", "o");
			}
			if (!Marshal.IsComObject(o))
			{
				throw new ArgumentException("Value must be a Com object.", "o");
			}
			return Marshal.ReleaseComObjectInternal(o);
		}

		/// <summary>Releases the thread cache.</summary>
		// Token: 0x060041E1 RID: 16865 RVA: 0x000479F8 File Offset: 0x00045BF8
		[Obsolete]
		public static void ReleaseThreadCache()
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets data referenced by the specified key in the specified COM object.</summary>
		/// <param name="obj">The COM object in which to store the data.</param>
		/// <param name="key">The key in the internal hash table of the COM object in which to store the data.</param>
		/// <param name="data">The data to set.</param>
		/// <returns>
		///   <see langword="true" /> if the data was set successfully; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="obj" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="key" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a COM object.  
		/// -or-  
		/// <paramref name="obj" /> is a Windows Runtime object.</exception>
		// Token: 0x060041E2 RID: 16866 RVA: 0x000E26BD File Offset: 0x000E08BD
		public static bool SetComObjectData(object obj, object key, object data)
		{
			throw new NotSupportedException("MSDN states user code should never need to call this method.");
		}

		/// <summary>Returns the unmanaged size of an object in bytes.</summary>
		/// <param name="structure">The object whose size is to be returned.</param>
		/// <returns>The size of the specified object in unmanaged code.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="structure" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060041E3 RID: 16867 RVA: 0x000E2BE2 File Offset: 0x000E0DE2
		[ComVisible(true)]
		public static int SizeOf(object structure)
		{
			return Marshal.SizeOf(structure.GetType());
		}

		/// <summary>Returns the size of an unmanaged type in bytes.</summary>
		/// <param name="t">The type whose size is to be returned.</param>
		/// <returns>The size of the specified type in unmanaged code.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="t" /> parameter is a generic type definition.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="t" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060041E4 RID: 16868
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int SizeOf(Type t);

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Returns the size of an unmanaged type in bytes.</summary>
		/// <typeparam name="T">The type whose size is to be returned.</typeparam>
		/// <returns>The size, in bytes, of the type that is specified by the <typeparamref name="T" /> generic type parameter.</returns>
		// Token: 0x060041E5 RID: 16869 RVA: 0x000E2BEF File Offset: 0x000E0DEF
		public static int SizeOf<T>()
		{
			return Marshal.SizeOf(typeof(T));
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Returns the unmanaged size of an object of a specified type in bytes.</summary>
		/// <param name="structure">The object whose size is to be returned.</param>
		/// <typeparam name="T">The type of the <paramref name="structure" /> parameter.</typeparam>
		/// <returns>The size, in bytes, of the specified object in unmanaged code.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="structure" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060041E6 RID: 16870 RVA: 0x000E2C00 File Offset: 0x000E0E00
		public static int SizeOf<T>(T structure)
		{
			return Marshal.SizeOf(structure.GetType());
		}

		// Token: 0x060041E7 RID: 16871 RVA: 0x000E2C14 File Offset: 0x000E0E14
		internal static uint SizeOfType(Type type)
		{
			return (uint)Marshal.SizeOf(type);
		}

		// Token: 0x060041E8 RID: 16872 RVA: 0x000E2C1C File Offset: 0x000E0E1C
		internal static uint AlignedSizeOf<T>() where T : struct
		{
			uint num = Marshal.SizeOfType(typeof(T));
			if (num == 1U || num == 2U)
			{
				return num;
			}
			if (IntPtr.Size == 8 && num == 4U)
			{
				return num;
			}
			return num + 3U & 4294967292U;
		}

		/// <summary>Allocates a BSTR and copies the contents of a managed <see cref="T:System.String" /> into it.</summary>
		/// <param name="s">The managed string to be copied.</param>
		/// <returns>An unmanaged pointer to the <see langword="BSTR" />, or 0 if <paramref name="s" /> is null.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length for <paramref name="s" /> is out of range.</exception>
		// Token: 0x060041E9 RID: 16873 RVA: 0x000E2C58 File Offset: 0x000E0E58
		public unsafe static IntPtr StringToBSTR(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Marshal.BufferToBSTR(ptr, s.Length);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <param name="s">A managed string to be copied.</param>
		/// <returns>An integer representing a pointer to the block of memory allocated for the string, or 0 if <paramref name="s" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		// Token: 0x060041EA RID: 16874 RVA: 0x000E2C8A File Offset: 0x000E0E8A
		public static IntPtr StringToCoTaskMemAnsi(string s)
		{
			return Marshal.StringToAllocatedMemoryUTF8(s);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <param name="s">A managed string to be copied.</param>
		/// <returns>The allocated memory block, or 0 if <paramref name="s" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The length for <paramref name="s" /> is out of range.</exception>
		// Token: 0x060041EB RID: 16875 RVA: 0x000E2C92 File Offset: 0x000E0E92
		public static IntPtr StringToCoTaskMemAuto(string s)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.StringToCoTaskMemAnsi(s);
			}
			return Marshal.StringToCoTaskMemUni(s);
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <param name="s">A managed string to be copied.</param>
		/// <returns>An integer representing a pointer to the block of memory allocated for the string, or 0 if s is <see langword="null" />.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		// Token: 0x060041EC RID: 16876 RVA: 0x000E2CAC File Offset: 0x000E0EAC
		public static IntPtr StringToCoTaskMemUni(string s)
		{
			int num = s.Length + 1;
			IntPtr intPtr = Marshal.AllocCoTaskMem(num * 2);
			char[] array = new char[num];
			s.CopyTo(0, array, 0, s.Length);
			array[s.Length] = '\0';
			Marshal.copy_to_unmanaged(array, 0, intPtr, num);
			return intPtr;
		}

		// Token: 0x060041ED RID: 16877
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr StringToHGlobalAnsi(char* s, int length);

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory, converting into ANSI format as it copies.</summary>
		/// <param name="s">A managed string to be copied.</param>
		/// <returns>The address, in unmanaged memory, to where <paramref name="s" /> was copied, or 0 if <paramref name="s" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		// Token: 0x060041EE RID: 16878 RVA: 0x000E2CF4 File Offset: 0x000E0EF4
		public unsafe static IntPtr StringToHGlobalAnsi(string s)
		{
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Marshal.StringToHGlobalAnsi(ptr, (s != null) ? s.Length : 0);
		}

		// Token: 0x060041EF RID: 16879 RVA: 0x000E2D24 File Offset: 0x000E0F24
		public unsafe static IntPtr StringToAllocatedMemoryUTF8(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int num = (s.Length + 1) * 3;
			if (num < s.Length)
			{
				throw new ArgumentOutOfRangeException("s");
			}
			IntPtr intPtr = Marshal.AllocCoTaskMemSize(new UIntPtr((uint)(num + 1)));
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			byte* ptr = (byte*)((void*)intPtr);
			fixed (string text = s)
			{
				char* ptr2 = text;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				int bytes = Encoding.UTF8.GetBytes(ptr2, s.Length, ptr, num);
				ptr[bytes] = 0;
			}
			return intPtr;
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory, converting into ANSI format if required.</summary>
		/// <param name="s">A managed string to be copied.</param>
		/// <returns>The address, in unmanaged memory, to where the string was copied, or 0 if <paramref name="s" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		// Token: 0x060041F0 RID: 16880 RVA: 0x000E2DAD File Offset: 0x000E0FAD
		public static IntPtr StringToHGlobalAuto(string s)
		{
			if (Marshal.SystemDefaultCharSize != 2)
			{
				return Marshal.StringToHGlobalAnsi(s);
			}
			return Marshal.StringToHGlobalUni(s);
		}

		// Token: 0x060041F1 RID: 16881
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr StringToHGlobalUni(char* s, int length);

		/// <summary>Copies the contents of a managed <see cref="T:System.String" /> into unmanaged memory.</summary>
		/// <param name="s">A managed string to be copied.</param>
		/// <returns>The address, in unmanaged memory, to where the <paramref name="s" /> was copied, or 0 if <paramref name="s" /> is <see langword="null" />.</returns>
		/// <exception cref="T:System.OutOfMemoryException">The method could not allocate enough native heap memory.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="s" /> parameter exceeds the maximum length allowed by the operating system.</exception>
		// Token: 0x060041F2 RID: 16882 RVA: 0x000E2DC4 File Offset: 0x000E0FC4
		public unsafe static IntPtr StringToHGlobalUni(string s)
		{
			char* ptr = s;
			if (ptr != null)
			{
				ptr += RuntimeHelpers.OffsetToStringData / 2;
			}
			return Marshal.StringToHGlobalUni(ptr, (s != null) ? s.Length : 0);
		}

		/// <summary>Allocates an unmanaged binary string (BSTR) and copies the contents of a managed <see cref="T:System.Security.SecureString" /> object into it.</summary>
		/// <param name="s">The managed object to copy.</param>
		/// <returns>The address, in unmanaged memory, where the <paramref name="s" /> parameter was copied to, or 0 if a null object was supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		// Token: 0x060041F3 RID: 16883 RVA: 0x000E2DF4 File Offset: 0x000E0FF4
		public unsafe static IntPtr SecureStringToBSTR(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			byte[] buffer = s.GetBuffer();
			int length = s.Length;
			if (BitConverter.IsLittleEndian)
			{
				for (int i = 0; i < buffer.Length; i += 2)
				{
					byte b = buffer[i];
					buffer[i] = buffer[i + 1];
					buffer[i + 1] = b;
				}
			}
			byte[] array;
			byte* ptr;
			if ((array = buffer) == null || array.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &array[0];
			}
			return Marshal.BufferToBSTR((char*)ptr, length);
		}

		// Token: 0x060041F4 RID: 16884 RVA: 0x000E2E69 File Offset: 0x000E1069
		internal static IntPtr SecureStringCoTaskMemAllocator(int len)
		{
			return Marshal.AllocCoTaskMem(len);
		}

		// Token: 0x060041F5 RID: 16885 RVA: 0x000E2E71 File Offset: 0x000E1071
		internal static IntPtr SecureStringGlobalAllocator(int len)
		{
			return Marshal.AllocHGlobal(len);
		}

		// Token: 0x060041F6 RID: 16886 RVA: 0x000E2E7C File Offset: 0x000E107C
		internal static IntPtr SecureStringToAnsi(SecureString s, Marshal.SecureStringAllocator allocator)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = allocator(length + 1);
			byte[] array = new byte[length + 1];
			try
			{
				byte[] buffer = s.GetBuffer();
				int i = 0;
				int num = 0;
				while (i < length)
				{
					array[i] = buffer[num + 1];
					buffer[num] = 0;
					buffer[num + 1] = 0;
					i++;
					num += 2;
				}
				array[i] = 0;
				Marshal.copy_to_unmanaged(array, 0, intPtr, length + 1);
			}
			finally
			{
				int j = length;
				while (j > 0)
				{
					j--;
					array[j] = 0;
				}
			}
			return intPtr;
		}

		// Token: 0x060041F7 RID: 16887 RVA: 0x000E2F20 File Offset: 0x000E1120
		internal static IntPtr SecureStringToUnicode(SecureString s, Marshal.SecureStringAllocator allocator)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			int length = s.Length;
			IntPtr intPtr = allocator(length * 2 + 2);
			byte[] array = null;
			try
			{
				array = s.GetBuffer();
				for (int i = 0; i < length; i++)
				{
					Marshal.WriteInt16(intPtr, i * 2, (short)((int)array[i * 2] << 8 | (int)array[i * 2 + 1]));
				}
				Marshal.WriteInt16(intPtr, array.Length, 0);
			}
			finally
			{
				if (array != null)
				{
					int j = array.Length;
					while (j > 0)
					{
						j--;
						array[j] = 0;
					}
				}
			}
			return intPtr;
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> object to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <param name="s">The managed object to copy.</param>
		/// <returns>The address, in unmanaged memory, where the <paramref name="s" /> parameter was copied to, or 0 if a null object was supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		// Token: 0x060041F8 RID: 16888 RVA: 0x000E2FB4 File Offset: 0x000E11B4
		public static IntPtr SecureStringToCoTaskMemAnsi(SecureString s)
		{
			return Marshal.SecureStringToAnsi(s, new Marshal.SecureStringAllocator(Marshal.SecureStringCoTaskMemAllocator));
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> object to a block of memory allocated from the unmanaged COM task allocator.</summary>
		/// <param name="s">The managed object to copy.</param>
		/// <returns>The address, in unmanaged memory, where the <paramref name="s" /> parameter was copied to, or 0 if a null object was supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		// Token: 0x060041F9 RID: 16889 RVA: 0x000E2FC8 File Offset: 0x000E11C8
		public static IntPtr SecureStringToCoTaskMemUnicode(SecureString s)
		{
			return Marshal.SecureStringToUnicode(s, new Marshal.SecureStringAllocator(Marshal.SecureStringCoTaskMemAllocator));
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> into unmanaged memory, converting into ANSI format as it copies.</summary>
		/// <param name="s">The managed object to copy.</param>
		/// <returns>The address, in unmanaged memory, to where the <paramref name="s" /> parameter was copied, or 0 if a null object was supplied.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		// Token: 0x060041FA RID: 16890 RVA: 0x000E2FDC File Offset: 0x000E11DC
		public static IntPtr SecureStringToGlobalAllocAnsi(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Marshal.SecureStringToAnsi(s, new Marshal.SecureStringAllocator(Marshal.SecureStringGlobalAllocator));
		}

		/// <summary>Copies the contents of a managed <see cref="T:System.Security.SecureString" /> object into unmanaged memory.</summary>
		/// <param name="s">The managed object to copy.</param>
		/// <returns>The address, in unmanaged memory, where <paramref name="s" /> was copied, or 0 if <paramref name="s" /> is a <see cref="T:System.Security.SecureString" /> object whose length is 0.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="s" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.NotSupportedException">The current computer is not running Windows 2000 Service Pack 3 or later.</exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory available.</exception>
		// Token: 0x060041FB RID: 16891 RVA: 0x000E2FFE File Offset: 0x000E11FE
		public static IntPtr SecureStringToGlobalAllocUnicode(SecureString s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Marshal.SecureStringToUnicode(s, new Marshal.SecureStringAllocator(Marshal.SecureStringGlobalAllocator));
		}

		/// <summary>Marshals data from a managed object to an unmanaged block of memory.</summary>
		/// <param name="structure">A managed object that holds the data to be marshaled. This object must be a structure or an instance of a formatted class.</param>
		/// <param name="ptr">A pointer to an unmanaged block of memory, which must be allocated before this method is called.</param>
		/// <param name="fDeleteOld">
		///   <see langword="true" /> to call the <see cref="M:System.Runtime.InteropServices.Marshal.DestroyStructure(System.IntPtr,System.Type)" /> method on the <paramref name="ptr" /> parameter before this method copies the data. The block must contain valid data. Note that passing <see langword="false" /> when the memory block already contains data can lead to a memory leak.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="structure" /> is a reference type that is not a formatted class.  
		/// -or-  
		/// <paramref name="structure" /> is an instance of a generic type (in the .NET Framework 4.5 and earlier versions only).</exception>
		// Token: 0x060041FC RID: 16892
		[ComVisible(true)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void StructureToPtr(object structure, IntPtr ptr, bool fDeleteOld);

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Marshals data from a managed object of a specified type to an unmanaged block of memory.</summary>
		/// <param name="structure">A managed object that holds the data to be marshaled. The object must be a structure or an instance of a formatted class.</param>
		/// <param name="ptr">A pointer to an unmanaged block of memory, which must be allocated before this method is called.</param>
		/// <param name="fDeleteOld">
		///   <see langword="true" /> to call the <see cref="M:System.Runtime.InteropServices.Marshal.DestroyStructure``1(System.IntPtr)" /> method on the <paramref name="ptr" /> parameter before this method copies the data. The block must contain valid data. Note that passing <see langword="false" /> when the memory block already contains data can lead to a memory leak.</param>
		/// <typeparam name="T">The type of the managed object.</typeparam>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="structure" /> is a reference type that is not a formatted class.</exception>
		// Token: 0x060041FD RID: 16893 RVA: 0x000E3020 File Offset: 0x000E1220
		public static void StructureToPtr<T>(T structure, IntPtr ptr, bool fDeleteOld)
		{
			Marshal.StructureToPtr(structure, ptr, fDeleteOld);
		}

		/// <summary>Throws an exception with a specific failure HRESULT value.</summary>
		/// <param name="errorCode">The HRESULT corresponding to the desired exception.</param>
		// Token: 0x060041FE RID: 16894 RVA: 0x000E3030 File Offset: 0x000E1230
		public static void ThrowExceptionForHR(int errorCode)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode);
			if (exceptionForHR != null)
			{
				throw exceptionForHR;
			}
		}

		/// <summary>Throws an exception with a specific failure HRESULT, based on the specified IErrorInfo.aspx) interface.</summary>
		/// <param name="errorCode">The HRESULT corresponding to the desired exception.</param>
		/// <param name="errorInfo">A pointer to the IErrorInfo interface that provides more information about the error. You can specify IntPtr(0) to use the current IErrorInfo interface, or IntPtr(-1) to ignore the current IErrorInfo interface and construct the exception just from the error code.</param>
		// Token: 0x060041FF RID: 16895 RVA: 0x000E304C File Offset: 0x000E124C
		public static void ThrowExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			Exception exceptionForHR = Marshal.GetExceptionForHR(errorCode, errorInfo);
			if (exceptionForHR != null)
			{
				throw exceptionForHR;
			}
		}

		// Token: 0x06004200 RID: 16896
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern IntPtr BufferToBSTR(char* ptr, int slen);

		/// <summary>Gets the address of the element at the specified index inside the specified array.</summary>
		/// <param name="arr">The array that contains the desired element.</param>
		/// <param name="index">The index in the <paramref name="arr" /> parameter of the desired element.</param>
		/// <returns>The address of <paramref name="index" /> inside <paramref name="arr" />.</returns>
		// Token: 0x06004201 RID: 16897
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr UnsafeAddrOfPinnedArrayElement(Array arr, int index);

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Gets the address of the element at the specified index in an array of a specified type.</summary>
		/// <param name="arr">The array that contains the desired element.</param>
		/// <param name="index">The index of the desired element in the <paramref name="arr" /> array.</param>
		/// <typeparam name="T">The type of the array.</typeparam>
		/// <returns>The address of <paramref name="index" /> in <paramref name="arr" />.</returns>
		// Token: 0x06004202 RID: 16898 RVA: 0x000E3066 File Offset: 0x000E1266
		public static IntPtr UnsafeAddrOfPinnedArrayElement<T>(T[] arr, int index)
		{
			return Marshal.UnsafeAddrOfPinnedArrayElement(arr, index);
		}

		/// <summary>Writes a single byte value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x06004203 RID: 16899 RVA: 0x000E306F File Offset: 0x000E126F
		public unsafe static void WriteByte(IntPtr ptr, byte val)
		{
			*(byte*)((void*)ptr) = val;
		}

		/// <summary>Writes a single byte value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write to.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x06004204 RID: 16900 RVA: 0x000E3079 File Offset: 0x000E1279
		public unsafe static void WriteByte(IntPtr ptr, int ofs, byte val)
		{
			*(byte*)((void*)IntPtr.Add(ptr, ofs)) = val;
		}

		/// <summary>Writes a single byte value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x06004205 RID: 16901 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SuppressUnmanagedCodeSecurity]
		public static void WriteByte([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, byte val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a 16-bit integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x06004206 RID: 16902 RVA: 0x000E308C File Offset: 0x000E128C
		public unsafe static void WriteInt16(IntPtr ptr, short val)
		{
			byte* ptr2 = (byte*)((void*)ptr);
			if ((ptr2 & 1U) == 0U)
			{
				*(short*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 2, true);
		}

		/// <summary>Writes a 16-bit signed integer value into unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write to.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x06004207 RID: 16903 RVA: 0x000E30B8 File Offset: 0x000E12B8
		public unsafe static void WriteInt16(IntPtr ptr, int ofs, short val)
		{
			byte* ptr2 = (byte*)((void*)ptr) + ofs;
			if ((ptr2 & 1U) == 0U)
			{
				*(short*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 2, true);
		}

		/// <summary>Writes a 16-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x06004208 RID: 16904 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SuppressUnmanagedCodeSecurity]
		public static void WriteInt16([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, short val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a character as a 16-bit integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x06004209 RID: 16905 RVA: 0x000E30E3 File Offset: 0x000E12E3
		public static void WriteInt16(IntPtr ptr, char val)
		{
			Marshal.WriteInt16(ptr, 0, (short)val);
		}

		/// <summary>Writes a 16-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in the native heap to write to.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x0600420A RID: 16906 RVA: 0x000E30EE File Offset: 0x000E12EE
		public static void WriteInt16(IntPtr ptr, int ofs, char val)
		{
			Marshal.WriteInt16(ptr, ofs, (short)val);
		}

		/// <summary>Writes a 16-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x0600420B RID: 16907 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static void WriteInt16([In] [Out] object ptr, int ofs, char val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a 32-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x0600420C RID: 16908 RVA: 0x000E30FC File Offset: 0x000E12FC
		public unsafe static void WriteInt32(IntPtr ptr, int val)
		{
			byte* ptr2 = (byte*)((void*)ptr);
			if ((ptr2 & 3U) == 0U)
			{
				*(int*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 4, true);
		}

		/// <summary>Writes a 32-bit signed integer value into unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write to.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x0600420D RID: 16909 RVA: 0x000E3128 File Offset: 0x000E1328
		public unsafe static void WriteInt32(IntPtr ptr, int ofs, int val)
		{
			byte* ptr2 = (byte*)((void*)ptr) + ofs;
			if ((ptr2 & 3U) == 0U)
			{
				*(int*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 4, true);
		}

		/// <summary>Writes a 32-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x0600420E RID: 16910 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SuppressUnmanagedCodeSecurity]
		public static void WriteInt32([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, int val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a 64-bit signed integer value to unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x0600420F RID: 16911 RVA: 0x000E3154 File Offset: 0x000E1354
		public unsafe static void WriteInt64(IntPtr ptr, long val)
		{
			byte* ptr2 = (byte*)((void*)ptr);
			if ((ptr2 & 7U) == 0U)
			{
				*(long*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 8, true);
		}

		/// <summary>Writes a 64-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x06004210 RID: 16912 RVA: 0x000E3180 File Offset: 0x000E1380
		public unsafe static void WriteInt64(IntPtr ptr, int ofs, long val)
		{
			byte* ptr2 = (byte*)((void*)ptr) + ofs;
			if ((ptr2 & 7U) == 0U)
			{
				*(long*)ptr2 = val;
				return;
			}
			Buffer.Memcpy(ptr2, (byte*)(&val), 8, true);
		}

		/// <summary>Writes a 64-bit signed integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x06004211 RID: 16913 RVA: 0x000479F8 File Offset: 0x00045BF8
		[SuppressUnmanagedCodeSecurity]
		public static void WriteInt64([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, long val)
		{
			throw new NotImplementedException();
		}

		/// <summary>Writes a processor native sized integer value into unmanaged memory.</summary>
		/// <param name="ptr">The address in unmanaged memory to write to.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">
		///   <paramref name="ptr" /> is not a recognized format.  
		/// -or-  
		/// <paramref name="ptr" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="ptr" /> is invalid.</exception>
		// Token: 0x06004212 RID: 16914 RVA: 0x000E31AB File Offset: 0x000E13AB
		public static void WriteIntPtr(IntPtr ptr, IntPtr val)
		{
			if (IntPtr.Size == 4)
			{
				Marshal.WriteInt32(ptr, (int)val);
				return;
			}
			Marshal.WriteInt64(ptr, (long)val);
		}

		/// <summary>Writes a processor native-sized integer value to unmanaged memory at a specified offset.</summary>
		/// <param name="ptr">The base address in unmanaged memory to write to.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		// Token: 0x06004213 RID: 16915 RVA: 0x000E31CE File Offset: 0x000E13CE
		public static void WriteIntPtr(IntPtr ptr, int ofs, IntPtr val)
		{
			if (IntPtr.Size == 4)
			{
				Marshal.WriteInt32(ptr, ofs, (int)val);
				return;
			}
			Marshal.WriteInt64(ptr, ofs, (long)val);
		}

		/// <summary>Writes a processor native sized integer value to unmanaged memory.</summary>
		/// <param name="ptr">The base address in unmanaged memory of the target object.</param>
		/// <param name="ofs">An additional byte offset, which is added to the <paramref name="ptr" /> parameter before writing.</param>
		/// <param name="val">The value to write.</param>
		/// <exception cref="T:System.AccessViolationException">Base address (<paramref name="ptr" />) plus offset byte (<paramref name="ofs" />) produces a null or invalid address.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ptr" /> is an <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> object. This method does not accept <see cref="T:System.Runtime.InteropServices.ArrayWithOffset" /> parameters.</exception>
		// Token: 0x06004214 RID: 16916 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static void WriteIntPtr([MarshalAs(UnmanagedType.AsAny)] [In] [Out] object ptr, int ofs, IntPtr val)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06004215 RID: 16917 RVA: 0x000E31F4 File Offset: 0x000E13F4
		private static Exception ConvertHrToException(int errorCode)
		{
			if (errorCode > -2147024362)
			{
				if (errorCode <= -2146232828)
				{
					if (errorCode <= -2146893792)
					{
						if (errorCode != -2147023895)
						{
							if (errorCode != -2146893792)
							{
								goto IL_3A9;
							}
							return new CryptographicException();
						}
					}
					else
					{
						if (errorCode == -2146234348)
						{
							return new AppDomainUnloadedException();
						}
						switch (errorCode)
						{
						case -2146233088:
							return new Exception();
						case -2146233087:
							return new SystemException();
						case -2146233086:
							return new ArgumentOutOfRangeException();
						case -2146233085:
							return new ArrayTypeMismatchException();
						case -2146233084:
							return new ContextMarshalException();
						case -2146233083:
						case -2146233074:
						case -2146233073:
						case -2146233061:
						case -2146233060:
						case -2146233059:
						case -2146233058:
						case -2146233057:
						case -2146233055:
						case -2146233053:
						case -2146233052:
						case -2146233051:
						case -2146233050:
						case -2146233046:
						case -2146233045:
						case -2146233044:
						case -2146233043:
						case -2146233042:
						case -2146233041:
						case -2146233040:
						case -2146233035:
						case -2146233034:
							goto IL_3A9;
						case -2146233082:
							return new ExecutionEngineException();
						case -2146233081:
							return new FieldAccessException();
						case -2146233080:
							return new IndexOutOfRangeException();
						case -2146233079:
							return new InvalidOperationException();
						case -2146233078:
							return new SecurityException();
						case -2146233077:
							return new RemotingException();
						case -2146233076:
							return new SerializationException();
						case -2146233075:
							return new VerificationException();
						case -2146233072:
							return new MethodAccessException();
						case -2146233071:
							return new MissingFieldException();
						case -2146233070:
							return new MissingMemberException();
						case -2146233069:
							return new MissingMethodException();
						case -2146233068:
							return new MulticastNotSupportedException();
						case -2146233067:
							return new NotSupportedException();
						case -2146233066:
							return new OverflowException();
						case -2146233065:
							return new RankException();
						case -2146233064:
							return new SynchronizationLockException();
						case -2146233063:
							return new ThreadInterruptedException();
						case -2146233062:
							return new MemberAccessException();
						case -2146233056:
							return new ThreadStateException();
						case -2146233054:
							return new TypeLoadException();
						case -2146233049:
							return new InvalidComObjectException();
						case -2146233048:
							return new NotFiniteNumberException();
						case -2146233047:
							return new DuplicateWaitObjectException();
						case -2146233039:
							return new InvalidOleVariantTypeException();
						case -2146233038:
							return new MissingManifestResourceException();
						case -2146233037:
							return new SafeArrayTypeMismatchException();
						case -2146233036:
							return new TypeInitializationException("", null);
						case -2146233033:
							return new FormatException();
						default:
							switch (errorCode)
							{
							case -2146232832:
								return new ApplicationException();
							case -2146232831:
								return new InvalidFilterCriteriaException();
							case -2146232830:
								return new ReflectionTypeLoadException(new Type[0], new Exception[0]);
							case -2146232829:
								return new TargetException();
							case -2146232828:
								return new TargetInvocationException(null);
							default:
								goto IL_3A9;
							}
							break;
						}
					}
				}
				else if (errorCode <= 3)
				{
					if (errorCode == -2146232800)
					{
						return new IOException();
					}
					if (errorCode == 2)
					{
						goto IL_2A6;
					}
					if (errorCode != 3)
					{
						goto IL_3A9;
					}
					goto IL_27C;
				}
				else
				{
					if (errorCode == 11)
					{
						goto IL_26A;
					}
					if (errorCode == 206)
					{
						goto IL_32A;
					}
					if (errorCode != 1001)
					{
						goto IL_3A9;
					}
				}
				return new StackOverflowException();
			}
			if (errorCode <= -2147024893)
			{
				if (errorCode <= -2147352562)
				{
					switch (errorCode)
					{
					case -2147467263:
						return new NotImplementedException();
					case -2147467262:
						return new InvalidCastException();
					case -2147467261:
						return new NullReferenceException();
					default:
						if (errorCode != -2147352562)
						{
							goto IL_3A9;
						}
						return new TargetParameterCountException();
					}
				}
				else
				{
					if (errorCode == -2147352558)
					{
						return new DivideByZeroException();
					}
					if (errorCode == -2147024894)
					{
						goto IL_2A6;
					}
					if (errorCode != -2147024893)
					{
						goto IL_3A9;
					}
					goto IL_27C;
				}
			}
			else if (errorCode <= -2147024858)
			{
				if (errorCode != -2147024885)
				{
					if (errorCode == -2147024882)
					{
						return new OutOfMemoryException();
					}
					if (errorCode != -2147024858)
					{
						goto IL_3A9;
					}
					return new EndOfStreamException();
				}
			}
			else
			{
				if (errorCode == -2147024809)
				{
					return new ArgumentException();
				}
				if (errorCode == -2147024690)
				{
					goto IL_32A;
				}
				if (errorCode != -2147024362)
				{
					goto IL_3A9;
				}
				return new ArithmeticException();
			}
			IL_26A:
			return new BadImageFormatException();
			IL_27C:
			return new DirectoryNotFoundException();
			IL_2A6:
			return new FileNotFoundException();
			IL_32A:
			return new PathTooLongException();
			IL_3A9:
			if (errorCode < 0)
			{
				return new COMException("", errorCode);
			}
			return null;
		}

		// Token: 0x06004216 RID: 16918
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, EntryPoint = "SetErrorInfo")]
		private static extern int _SetErrorInfo(int dwReserved, [MarshalAs(UnmanagedType.Interface)] IErrorInfo pIErrorInfo);

		// Token: 0x06004217 RID: 16919
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetErrorInfo")]
		private static extern int _GetErrorInfo(int dwReserved, [MarshalAs(UnmanagedType.Interface)] out IErrorInfo ppIErrorInfo);

		// Token: 0x06004218 RID: 16920 RVA: 0x000E35BC File Offset: 0x000E17BC
		internal static int SetErrorInfo(int dwReserved, IErrorInfo errorInfo)
		{
			int result = 0;
			errorInfo = null;
			if (Marshal.SetErrorInfoNotAvailable)
			{
				return -1;
			}
			try
			{
				result = Marshal._SetErrorInfo(dwReserved, errorInfo);
			}
			catch (Exception)
			{
				Marshal.SetErrorInfoNotAvailable = true;
			}
			return result;
		}

		// Token: 0x06004219 RID: 16921 RVA: 0x000E35FC File Offset: 0x000E17FC
		internal static int GetErrorInfo(int dwReserved, out IErrorInfo errorInfo)
		{
			int result = 0;
			errorInfo = null;
			if (Marshal.GetErrorInfoNotAvailable)
			{
				return -1;
			}
			try
			{
				result = Marshal._GetErrorInfo(dwReserved, out errorInfo);
			}
			catch (Exception)
			{
				Marshal.GetErrorInfoNotAvailable = true;
			}
			return result;
		}

		/// <summary>Converts the specified HRESULT error code to a corresponding <see cref="T:System.Exception" /> object.</summary>
		/// <param name="errorCode">The HRESULT to be converted.</param>
		/// <returns>An object that represents the converted HRESULT.</returns>
		// Token: 0x0600421A RID: 16922 RVA: 0x000E363C File Offset: 0x000E183C
		public static Exception GetExceptionForHR(int errorCode)
		{
			return Marshal.GetExceptionForHR(errorCode, IntPtr.Zero);
		}

		/// <summary>Converts the specified HRESULT error code to a corresponding <see cref="T:System.Exception" /> object, with additional error information passed in an IErrorInfo interface for the exception object.</summary>
		/// <param name="errorCode">The HRESULT to be converted.</param>
		/// <param name="errorInfo">A pointer to the <see langword="IErrorInfo" /> interface that provides more information about the error. You can specify IntPtr(0) to use the current <see langword="IErrorInfo" /> interface, or IntPtr(-1) to ignore the current <see langword="IErrorInfo" /> interface and construct the exception just from the error code.</param>
		/// <returns>An object that represents the converted HRESULT and information obtained from <paramref name="errorInfo" />.</returns>
		// Token: 0x0600421B RID: 16923 RVA: 0x000E364C File Offset: 0x000E184C
		public static Exception GetExceptionForHR(int errorCode, IntPtr errorInfo)
		{
			IErrorInfo errorInfo2 = null;
			if (errorInfo != (IntPtr)(-1))
			{
				if (errorInfo == IntPtr.Zero)
				{
					if (Marshal.GetErrorInfo(0, out errorInfo2) != 0)
					{
						errorInfo2 = null;
					}
				}
				else
				{
					errorInfo2 = (Marshal.GetObjectForIUnknown(errorInfo) as IErrorInfo);
				}
			}
			if (errorInfo2 is ManagedErrorInfo && ((ManagedErrorInfo)errorInfo2).Exception._HResult == errorCode)
			{
				return ((ManagedErrorInfo)errorInfo2).Exception;
			}
			Exception ex = Marshal.ConvertHrToException(errorCode);
			if (errorInfo2 != null && ex != null)
			{
				uint num;
				errorInfo2.GetHelpContext(out num);
				string text;
				errorInfo2.GetSource(out text);
				ex.Source = text;
				errorInfo2.GetDescription(out text);
				ex.SetMessage(text);
				errorInfo2.GetHelpFile(out text);
				if (num == 0U)
				{
					ex.HelpLink = text;
				}
				else
				{
					ex.HelpLink = string.Format("{0}#{1}", text, num);
				}
			}
			return ex;
		}

		/// <summary>Releases all references to a Runtime Callable Wrapper (RCW) by setting its reference count to 0.</summary>
		/// <param name="o">The RCW to be released.</param>
		/// <returns>The new value of the reference count of the RCW associated with the <paramref name="o" /> parameter, which is 0 (zero) if the release is successful.</returns>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="o" /> is not a valid COM object.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="o" /> is <see langword="null" />.</exception>
		// Token: 0x0600421C RID: 16924 RVA: 0x000E371A File Offset: 0x000E191A
		public static int FinalReleaseComObject(object o)
		{
			while (Marshal.ReleaseComObject(o) != 0)
			{
			}
			return 0;
		}

		// Token: 0x0600421D RID: 16925
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Delegate GetDelegateForFunctionPointerInternal(IntPtr ptr, Type t);

		/// <summary>Converts an unmanaged function pointer to a delegate.</summary>
		/// <param name="ptr">The unmanaged function pointer to be converted.</param>
		/// <param name="t">The type of the delegate to be returned.</param>
		/// <returns>A delegate instance that can be cast to the appropriate delegate type.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="t" /> parameter is not a delegate or is generic.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ptr" /> parameter is <see langword="null" />.  
		///  -or-  
		///  The <paramref name="t" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600421E RID: 16926 RVA: 0x000E3728 File Offset: 0x000E1928
		public static Delegate GetDelegateForFunctionPointer(IntPtr ptr, Type t)
		{
			if (t == null)
			{
				throw new ArgumentNullException("t");
			}
			if (!t.IsSubclassOf(typeof(MulticastDelegate)) || t == typeof(MulticastDelegate))
			{
				throw new ArgumentException("Type is not a delegate", "t");
			}
			if (t.IsGenericType)
			{
				throw new ArgumentException("The specified Type must not be a generic type definition.");
			}
			if (ptr == IntPtr.Zero)
			{
				throw new ArgumentNullException("ptr");
			}
			return Marshal.GetDelegateForFunctionPointerInternal(ptr, t);
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Converts an unmanaged function pointer to a delegate of a specified type.</summary>
		/// <param name="ptr">The unmanaged function pointer to convert.</param>
		/// <typeparam name="TDelegate">The type of the delegate to return.</typeparam>
		/// <returns>A instance of the specified delegate type.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="TDelegate" /> generic parameter is not a delegate, or it is an open generic type.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="ptr" /> parameter is <see langword="null" />.</exception>
		// Token: 0x0600421F RID: 16927 RVA: 0x000E37AF File Offset: 0x000E19AF
		public static TDelegate GetDelegateForFunctionPointer<TDelegate>(IntPtr ptr)
		{
			return (TDelegate)((object)Marshal.GetDelegateForFunctionPointer(ptr, typeof(TDelegate)));
		}

		// Token: 0x06004220 RID: 16928
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr GetFunctionPointerForDelegateInternal(Delegate d);

		/// <summary>Converts a delegate into a function pointer that is callable from unmanaged code.</summary>
		/// <param name="d">The delegate to be passed to unmanaged code.</param>
		/// <returns>A value that can be passed to unmanaged code, which, in turn, can use it to call the underlying managed delegate.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="d" /> parameter is a generic type definition.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="d" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004221 RID: 16929 RVA: 0x000E37C6 File Offset: 0x000E19C6
		public static IntPtr GetFunctionPointerForDelegate(Delegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			return Marshal.GetFunctionPointerForDelegateInternal(d);
		}

		/// <summary>[Supported in the .NET Framework 4.5.1 and later versions]  
		///  Converts a delegate of a specified type to a function pointer that is callable from unmanaged code.</summary>
		/// <param name="d">The delegate to be passed to unmanaged code.</param>
		/// <typeparam name="TDelegate">The type of delegate to convert.</typeparam>
		/// <returns>A value that can be passed to unmanaged code, which, in turn, can use it to call the underlying managed delegate.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="d" /> parameter is a generic type definition.</exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="d" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06004222 RID: 16930 RVA: 0x000E37DC File Offset: 0x000E19DC
		public static IntPtr GetFunctionPointerForDelegate<TDelegate>(TDelegate d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			return Marshal.GetFunctionPointerForDelegateInternal((Delegate)((object)d));
		}

		// Token: 0x06004223 RID: 16931
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void SetLastWin32Error(int error);

		// Token: 0x06004224 RID: 16932
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern IntPtr GetRawIUnknownForComObjectNoAddRef(object o);

		// Token: 0x06004225 RID: 16933
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetHRForException_WinRT(Exception e);

		// Token: 0x06004226 RID: 16934
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern object GetNativeActivationFactory(Type type);

		// Token: 0x06004227 RID: 16935 RVA: 0x000E3804 File Offset: 0x000E1A04
		internal static ICustomMarshaler GetCustomMarshalerInstance(Type type, string cookie)
		{
			ValueTuple<Type, string> key = new ValueTuple<Type, string>(type, cookie);
			LazyInitializer.EnsureInitialized<Dictionary<ValueTuple<Type, string>, ICustomMarshaler>>(ref Marshal.MarshalerInstanceCache, () => new Dictionary<ValueTuple<Type, string>, ICustomMarshaler>(new Marshal.MarshalerInstanceKeyComparer()));
			object marshalerInstanceCacheLock = Marshal.MarshalerInstanceCacheLock;
			ICustomMarshaler customMarshaler;
			bool flag2;
			lock (marshalerInstanceCacheLock)
			{
				flag2 = Marshal.MarshalerInstanceCache.TryGetValue(key, out customMarshaler);
			}
			if (!flag2)
			{
				RuntimeMethodInfo runtimeMethodInfo;
				try
				{
					runtimeMethodInfo = (RuntimeMethodInfo)type.GetMethod("GetInstance", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, new Type[]
					{
						typeof(string)
					}, null);
				}
				catch (AmbiguousMatchException)
				{
					throw new ApplicationException("Custom marshaler '" + type.FullName + "' implements multiple static GetInstance methods that take a single string parameter.");
				}
				if (runtimeMethodInfo == null || runtimeMethodInfo.ReturnType != typeof(ICustomMarshaler))
				{
					throw new ApplicationException("Custom marshaler '" + type.FullName + "' does not implement a static GetInstance method that takes a single string parameter and returns an ICustomMarshaler.");
				}
				Exception ex;
				try
				{
					customMarshaler = (ICustomMarshaler)runtimeMethodInfo.InternalInvoke(null, new object[]
					{
						cookie
					}, out ex);
				}
				catch (Exception ex)
				{
					customMarshaler = null;
				}
				if (ex != null)
				{
					ExceptionDispatchInfo.Capture(ex).Throw();
				}
				if (customMarshaler == null)
				{
					throw new ApplicationException("A call to GetInstance() for custom marshaler '" + type.FullName + "' returned null, which is not allowed.");
				}
				marshalerInstanceCacheLock = Marshal.MarshalerInstanceCacheLock;
				lock (marshalerInstanceCacheLock)
				{
					Marshal.MarshalerInstanceCache[key] = customMarshaler;
				}
			}
			return customMarshaler;
		}

		// Token: 0x06004228 RID: 16936 RVA: 0x000E39B0 File Offset: 0x000E1BB0
		public unsafe static IntPtr StringToCoTaskMemUTF8(string s)
		{
			if (s == null)
			{
				return IntPtr.Zero;
			}
			int maxByteCount = Encoding.UTF8.GetMaxByteCount(s.Length);
			IntPtr intPtr = Marshal.AllocCoTaskMem(maxByteCount + 1);
			byte* ptr = (byte*)((void*)intPtr);
			int bytes;
			fixed (string text = s)
			{
				char* ptr2 = text;
				if (ptr2 != null)
				{
					ptr2 += RuntimeHelpers.OffsetToStringData / 2;
				}
				bytes = Encoding.UTF8.GetBytes(ptr2, s.Length, ptr, maxByteCount);
			}
			ptr[bytes] = 0;
			return intPtr;
		}

		// Token: 0x06004229 RID: 16937 RVA: 0x000E3A14 File Offset: 0x000E1C14
		// Note: this type is marked as 'beforefieldinit'.
		static Marshal()
		{
		}

		/// <summary>Represents the maximum size of a double byte character set (DBCS) size, in bytes, for the current operating system. This field is read-only.</summary>
		// Token: 0x04002BCC RID: 11212
		public static readonly int SystemMaxDBCSCharSize = 2;

		/// <summary>Represents the default character size on the system; the default is 2 for Unicode systems and 1 for ANSI systems. This field is read-only.</summary>
		// Token: 0x04002BCD RID: 11213
		public static readonly int SystemDefaultCharSize = Environment.IsRunningOnWindows ? 2 : 1;

		// Token: 0x04002BCE RID: 11214
		private static bool SetErrorInfoNotAvailable;

		// Token: 0x04002BCF RID: 11215
		private static bool GetErrorInfoNotAvailable;

		// Token: 0x04002BD0 RID: 11216
		internal static Dictionary<ValueTuple<Type, string>, ICustomMarshaler> MarshalerInstanceCache;

		// Token: 0x04002BD1 RID: 11217
		internal static readonly object MarshalerInstanceCacheLock = new object();

		// Token: 0x02000749 RID: 1865
		// (Invoke) Token: 0x0600422B RID: 16939
		internal delegate IntPtr SecureStringAllocator(int len);

		// Token: 0x0200074A RID: 1866
		internal class MarshalerInstanceKeyComparer : IEqualityComparer<ValueTuple<Type, string>>
		{
			// Token: 0x0600422E RID: 16942 RVA: 0x000E3A36 File Offset: 0x000E1C36
			public bool Equals(ValueTuple<Type, string> lhs, ValueTuple<Type, string> rhs)
			{
				return lhs.CompareTo(rhs) == 0;
			}

			// Token: 0x0600422F RID: 16943 RVA: 0x000E3A43 File Offset: 0x000E1C43
			public int GetHashCode(ValueTuple<Type, string> key)
			{
				return key.GetHashCode();
			}

			// Token: 0x06004230 RID: 16944 RVA: 0x0000259F File Offset: 0x0000079F
			public MarshalerInstanceKeyComparer()
			{
			}
		}

		// Token: 0x0200074B RID: 1867
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06004231 RID: 16945 RVA: 0x000E3A52 File Offset: 0x000E1C52
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x06004232 RID: 16946 RVA: 0x0000259F File Offset: 0x0000079F
			public <>c()
			{
			}

			// Token: 0x06004233 RID: 16947 RVA: 0x000E3A5E File Offset: 0x000E1C5E
			internal Dictionary<ValueTuple<Type, string>, ICustomMarshaler> <GetCustomMarshalerInstance>b__225_0()
			{
				return new Dictionary<ValueTuple<Type, string>, ICustomMarshaler>(new Marshal.MarshalerInstanceKeyComparer());
			}

			// Token: 0x04002BD2 RID: 11218
			public static readonly Marshal.<>c <>9 = new Marshal.<>c();

			// Token: 0x04002BD3 RID: 11219
			public static Func<Dictionary<ValueTuple<Type, string>, ICustomMarshaler>> <>9__225_0;
		}
	}
}

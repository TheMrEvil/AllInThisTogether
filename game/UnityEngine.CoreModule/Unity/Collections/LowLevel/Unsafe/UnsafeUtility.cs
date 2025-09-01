using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Unity.Burst;
using UnityEngine.Bindings;

namespace Unity.Collections.LowLevel.Unsafe
{
	// Token: 0x020000AC RID: 172
	[StaticAccessor("UnsafeUtility", StaticAccessorType.DoubleColon)]
	[NativeHeader("Runtime/Export/Unsafe/UnsafeUtility.bindings.h")]
	public static class UnsafeUtility
	{
		// Token: 0x060002EF RID: 751
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFieldOffsetInStruct(FieldInfo field);

		// Token: 0x060002F0 RID: 752
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetFieldOffsetInClass(FieldInfo field);

		// Token: 0x060002F1 RID: 753 RVA: 0x0000582C File Offset: 0x00003A2C
		public static int GetFieldOffset(FieldInfo field)
		{
			bool isValueType = field.DeclaringType.IsValueType;
			int result;
			if (isValueType)
			{
				result = UnsafeUtility.GetFieldOffsetInStruct(field);
			}
			else
			{
				bool isClass = field.DeclaringType.IsClass;
				if (isClass)
				{
					result = UnsafeUtility.GetFieldOffsetInClass(field);
				}
				else
				{
					result = -1;
				}
			}
			return result;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x00005870 File Offset: 0x00003A70
		public unsafe static void* PinGCObjectAndGetAddress(object target, out ulong gcHandle)
		{
			return UnsafeUtility.PinSystemObjectAndGetAddress(target, out gcHandle);
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0000588C File Offset: 0x00003A8C
		public unsafe static void* PinGCArrayAndGetDataAddress(Array target, out ulong gcHandle)
		{
			return UnsafeUtility.PinSystemArrayAndGetAddress(target, out gcHandle);
		}

		// Token: 0x060002F4 RID: 756
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* PinSystemArrayAndGetAddress(object target, out ulong gcHandle);

		// Token: 0x060002F5 RID: 757
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void* PinSystemObjectAndGetAddress(object target, out ulong gcHandle);

		// Token: 0x060002F6 RID: 758
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void ReleaseGCObject(ulong gcHandle);

		// Token: 0x060002F7 RID: 759
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void CopyObjectAddressToPtr(object target, void* dstPtr);

		// Token: 0x060002F8 RID: 760 RVA: 0x000058A8 File Offset: 0x00003AA8
		public static bool IsBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable(typeof(T));
		}

		// Token: 0x060002F9 RID: 761
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CheckForLeaks();

		// Token: 0x060002FA RID: 762
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ForgiveLeaks();

		// Token: 0x060002FB RID: 763
		[BurstAuthorizedExternalMethod]
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern NativeLeakDetectionMode GetLeakDetectionMode();

		// Token: 0x060002FC RID: 764
		[BurstAuthorizedExternalMethod]
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void SetLeakDetectionMode(NativeLeakDetectionMode value);

		// Token: 0x060002FD RID: 765
		[BurstAuthorizedExternalMethod]
		[ThreadSafe(ThrowsException = false)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LeakRecord(IntPtr handle, LeakCategory category, int callstacksToSkip);

		// Token: 0x060002FE RID: 766
		[ThreadSafe(ThrowsException = false)]
		[BurstAuthorizedExternalMethod]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int LeakErase(IntPtr handle, LeakCategory category);

		// Token: 0x060002FF RID: 767
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* MallocTracked(long size, int alignment, Allocator allocator, int callstacksToSkip);

		// Token: 0x06000300 RID: 768
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void FreeTracked(void* memory, Allocator allocator);

		// Token: 0x06000301 RID: 769
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void* Malloc(long size, int alignment, Allocator allocator);

		// Token: 0x06000302 RID: 770
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void Free(void* memory, Allocator allocator);

		// Token: 0x06000303 RID: 771 RVA: 0x000058CC File Offset: 0x00003ACC
		public static bool IsValidAllocator(Allocator allocator)
		{
			return allocator > Allocator.None;
		}

		// Token: 0x06000304 RID: 772
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpy(void* destination, void* source, long size);

		// Token: 0x06000305 RID: 773
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpyReplicate(void* destination, void* source, int size, int count);

		// Token: 0x06000306 RID: 774
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemCpyStride(void* destination, int destinationStride, void* source, int sourceStride, int elementSize, int count);

		// Token: 0x06000307 RID: 775
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemMove(void* destination, void* source, long size);

		// Token: 0x06000308 RID: 776
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern void MemSet(void* destination, byte value, long size);

		// Token: 0x06000309 RID: 777 RVA: 0x000058E2 File Offset: 0x00003AE2
		public unsafe static void MemClear(void* destination, long size)
		{
			UnsafeUtility.MemSet(destination, 0, size);
		}

		// Token: 0x0600030A RID: 778
		[ThreadSafe(ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public unsafe static extern int MemCmp(void* ptr1, void* ptr2, long size);

		// Token: 0x0600030B RID: 779
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int SizeOf(Type type);

		// Token: 0x0600030C RID: 780
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsBlittable(Type type);

		// Token: 0x0600030D RID: 781
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsUnmanaged(Type type);

		// Token: 0x0600030E RID: 782
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsValidNativeContainerElementType(Type type);

		// Token: 0x0600030F RID: 783
		[ThreadSafe]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void LogError(string msg, string filename, int linenumber);

		// Token: 0x06000310 RID: 784 RVA: 0x000058F0 File Offset: 0x00003AF0
		private static bool IsBlittableValueType(Type t)
		{
			return t.IsValueType && UnsafeUtility.IsBlittable(t);
		}

		// Token: 0x06000311 RID: 785 RVA: 0x00005914 File Offset: 0x00003B14
		private static string GetReasonForTypeNonBlittableImpl(Type t, string name)
		{
			bool flag = !t.IsValueType;
			string result;
			if (flag)
			{
				result = string.Format("{0} is not blittable because it is not of value type ({1})\n", name, t);
			}
			else
			{
				bool isPrimitive = t.IsPrimitive;
				if (isPrimitive)
				{
					result = string.Format("{0} is not blittable ({1})\n", name, t);
				}
				else
				{
					string text = "";
					foreach (FieldInfo fieldInfo in t.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
					{
						bool flag2 = !UnsafeUtility.IsBlittableValueType(fieldInfo.FieldType);
						if (flag2)
						{
							text += UnsafeUtility.GetReasonForTypeNonBlittableImpl(fieldInfo.FieldType, string.Format("{0}.{1}", name, fieldInfo.Name));
						}
					}
					result = text;
				}
			}
			return result;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000059C8 File Offset: 0x00003BC8
		internal static bool IsArrayBlittable(Array arr)
		{
			return UnsafeUtility.IsBlittableValueType(arr.GetType().GetElementType());
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000059EC File Offset: 0x00003BEC
		internal static bool IsGenericListBlittable<T>() where T : struct
		{
			return UnsafeUtility.IsBlittable<T>();
		}

		// Token: 0x06000314 RID: 788 RVA: 0x00005A04 File Offset: 0x00003C04
		internal static string GetReasonForArrayNonBlittable(Array arr)
		{
			Type elementType = arr.GetType().GetElementType();
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(elementType, elementType.Name);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x00005A30 File Offset: 0x00003C30
		internal static string GetReasonForGenericListNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x00005A5C File Offset: 0x00003C5C
		internal static string GetReasonForTypeNonBlittable(Type t)
		{
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(t, t.Name);
		}

		// Token: 0x06000317 RID: 791 RVA: 0x00005A7C File Offset: 0x00003C7C
		internal static string GetReasonForValueTypeNonBlittable<T>() where T : struct
		{
			Type typeFromHandle = typeof(T);
			return UnsafeUtility.GetReasonForTypeNonBlittableImpl(typeFromHandle, typeFromHandle.Name);
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00005AA8 File Offset: 0x00003CA8
		public static bool IsUnmanaged<T>()
		{
			int num = UnsafeUtility.IsUnmanagedCache<T>.value;
			bool flag = num == 1;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = num == 0;
				if (flag2)
				{
					num = (UnsafeUtility.IsUnmanagedCache<T>.value = (UnsafeUtility.IsUnmanaged(typeof(T)) ? 1 : -1));
				}
				result = (num == 1);
			}
			return result;
		}

		// Token: 0x06000319 RID: 793 RVA: 0x00005AF4 File Offset: 0x00003CF4
		public static bool IsValidNativeContainerElementType<T>()
		{
			int num = UnsafeUtility.IsValidNativeContainerElementTypeCache<T>.value;
			bool flag = num == -1;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = num == 0;
				if (flag2)
				{
					num = (UnsafeUtility.IsValidNativeContainerElementTypeCache<T>.value = (UnsafeUtility.IsValidNativeContainerElementType(typeof(T)) ? 1 : -1));
				}
				result = (num == 1);
			}
			return result;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x00005B40 File Offset: 0x00003D40
		public static int AlignOf<T>() where T : struct
		{
			return UnsafeUtility.SizeOf<UnsafeUtility.AlignOfHelper<T>>() - UnsafeUtility.SizeOf<T>();
		}

		// Token: 0x0600031B RID: 795 RVA: 0x00005B5D File Offset: 0x00003D5D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			UnsafeUtility.InternalCopyPtrToStructure<T>(ptr, out output);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x00005B68 File Offset: 0x00003D68
		private unsafe static void InternalCopyPtrToStructure<T>(void* ptr, out T output) where T : struct
		{
			output = *(T*)ptr;
		}

		// Token: 0x0600031D RID: 797 RVA: 0x00005B76 File Offset: 0x00003D76
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void CopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			UnsafeUtility.InternalCopyStructureToPtr<T>(ref input, ptr);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x00005B68 File Offset: 0x00003D68
		private unsafe static void InternalCopyStructureToPtr<T>(ref T input, void* ptr) where T : struct
		{
			*(T*)ptr = input;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x00005B81 File Offset: 0x00003D81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static T ReadArrayElement<T>(void* source, int index)
		{
			return *(T*)((byte*)source + (long)index * (long)sizeof(T));
		}

		// Token: 0x06000320 RID: 800 RVA: 0x00005B95 File Offset: 0x00003D95
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static T ReadArrayElementWithStride<T>(void* source, int index, int stride)
		{
			return *(T*)((byte*)source + (long)index * (long)stride);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x00005BA4 File Offset: 0x00003DA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteArrayElement<T>(void* destination, int index, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)sizeof(T)) = value;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x00005BB9 File Offset: 0x00003DB9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void WriteArrayElementWithStride<T>(void* destination, int index, int stride, T value)
		{
			*(T*)((byte*)destination + (long)index * (long)stride) = value;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x00005BC9 File Offset: 0x00003DC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static void* AddressOf<T>(ref T output) where T : struct
		{
			return (void*)(&output);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x00005BCC File Offset: 0x00003DCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SizeOf<T>() where T : struct
		{
			return sizeof(T);
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00005BC9 File Offset: 0x00003DC9
		public static ref T As<U, T>(ref U from)
		{
			return ref from;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00005BC9 File Offset: 0x00003DC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ref T AsRef<T>(void* ptr) where T : struct
		{
			return ref *(T*)ptr;
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00005BD4 File Offset: 0x00003DD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ref T ArrayElementAsRef<T>(void* ptr, int index) where T : struct
		{
			return ref *(T*)((byte*)ptr + (long)index * (long)sizeof(T));
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00005BE4 File Offset: 0x00003DE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int EnumToInt<T>(T enumValue) where T : struct, IConvertible
		{
			int result = 0;
			UnsafeUtility.InternalEnumToInt<T>(ref enumValue, ref result);
			return result;
		}

		// Token: 0x06000329 RID: 809 RVA: 0x00005C03 File Offset: 0x00003E03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void InternalEnumToInt<T>(ref T enumValue, ref int intValue)
		{
			intValue = enumValue;
		}

		// Token: 0x0600032A RID: 810 RVA: 0x00005C09 File Offset: 0x00003E09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EnumEquals<T>(T lhs, T rhs) where T : struct, IConvertible
		{
			return lhs == rhs;
		}

		// Token: 0x020000AD RID: 173
		internal struct IsUnmanagedCache<T>
		{
			// Token: 0x0400023B RID: 571
			internal static int value;
		}

		// Token: 0x020000AE RID: 174
		internal struct IsValidNativeContainerElementTypeCache<T>
		{
			// Token: 0x0400023C RID: 572
			internal static int value;
		}

		// Token: 0x020000AF RID: 175
		private struct AlignOfHelper<T> where T : struct
		{
			// Token: 0x0400023D RID: 573
			public byte dummy;

			// Token: 0x0400023E RID: 574
			public T data;
		}
	}
}

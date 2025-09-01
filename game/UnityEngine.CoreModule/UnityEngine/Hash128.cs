using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AE RID: 430
	[NativeHeader("Runtime/Export/Hashing/Hash128.bindings.h")]
	[NativeHeader("Runtime/Utilities/Hash128.h")]
	[UsedByNativeCode]
	[Serializable]
	public struct Hash128 : IComparable, IComparable<Hash128>, IEquatable<Hash128>
	{
		// Token: 0x060012F6 RID: 4854 RVA: 0x00019791 File Offset: 0x00017991
		public Hash128(uint u32_0, uint u32_1, uint u32_2, uint u32_3)
		{
			this.u64_0 = ((ulong)u32_1 << 32 | (ulong)u32_0);
			this.u64_1 = ((ulong)u32_3 << 32 | (ulong)u32_2);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000197B1 File Offset: 0x000179B1
		public Hash128(ulong u64_0, ulong u64_1)
		{
			this.u64_0 = u64_0;
			this.u64_1 = u64_1;
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x060012F8 RID: 4856 RVA: 0x000197C2 File Offset: 0x000179C2
		public bool isValid
		{
			get
			{
				return this.u64_0 != 0UL || this.u64_1 > 0UL;
			}
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000197DC File Offset: 0x000179DC
		public int CompareTo(Hash128 rhs)
		{
			bool flag = this < rhs;
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				bool flag2 = this > rhs;
				if (flag2)
				{
					result = 1;
				}
				else
				{
					result = 0;
				}
			}
			return result;
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x00019818 File Offset: 0x00017A18
		public override string ToString()
		{
			return Hash128.Hash128ToStringImpl(this);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x00019838 File Offset: 0x00017A38
		[FreeFunction("StringToHash128", IsThreadSafe = true)]
		public static Hash128 Parse(string hashString)
		{
			Hash128 result;
			Hash128.Parse_Injected(hashString, out result);
			return result;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0001984E File Offset: 0x00017A4E
		[FreeFunction("Hash128ToString", IsThreadSafe = true)]
		private static string Hash128ToStringImpl(Hash128 hash)
		{
			return Hash128.Hash128ToStringImpl_Injected(ref hash);
		}

		// Token: 0x060012FD RID: 4861
		[FreeFunction("ComputeHash128FromScriptString", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ComputeFromString(string data, ref Hash128 hash);

		// Token: 0x060012FE RID: 4862
		[FreeFunction("ComputeHash128FromScriptPointer", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ComputeFromPtr(IntPtr data, int start, int count, int elemSize, ref Hash128 hash);

		// Token: 0x060012FF RID: 4863
		[FreeFunction("ComputeHash128FromScriptArray", IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ComputeFromArray(Array data, int start, int count, int elemSize, ref Hash128 hash);

		// Token: 0x06001300 RID: 4864 RVA: 0x00019858 File Offset: 0x00017A58
		public static Hash128 Compute(string data)
		{
			Hash128 result = default(Hash128);
			Hash128.ComputeFromString(data, ref result);
			return result;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x0001987C File Offset: 0x00017A7C
		public static Hash128 Compute<T>(NativeArray<T> data) where T : struct
		{
			Hash128 result = default(Hash128);
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, data.Length, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x000198B8 File Offset: 0x00017AB8
		public static Hash128 Compute<T>(NativeArray<T> data, int start, int count) where T : struct
		{
			bool flag = start < 0 || count < 0 || start + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128 result = default(Hash128);
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), start, count, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00019924 File Offset: 0x00017B24
		public static Hash128 Compute<T>(T[] data) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Compute must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			Hash128 result = default(Hash128);
			Hash128.ComputeFromArray(data, 0, data.Length, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00019974 File Offset: 0x00017B74
		public static Hash128 Compute<T>(T[] data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Compute must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128 result = default(Hash128);
			Hash128.ComputeFromArray(data, start, count, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x000199F8 File Offset: 0x00017BF8
		public static Hash128 Compute<T>(List<T> data) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Compute", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			Hash128 result = default(Hash128);
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), 0, data.Count, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00019A60 File Offset: 0x00017C60
		public static Hash128 Compute<T>(List<T> data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Compute", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128 result = default(Hash128);
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), start, count, UnsafeUtility.SizeOf<T>(), ref result);
			return result;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00019AF8 File Offset: 0x00017CF8
		public unsafe static Hash128 Compute<[IsUnmanaged] T>(ref T val) where T : struct, ValueType
		{
			fixed (T* ptr = &val)
			{
				void* value = (void*)ptr;
				Hash128 result = default(Hash128);
				Hash128.ComputeFromPtr((IntPtr)value, 0, 1, UnsafeUtility.SizeOf<T>(), ref result);
				return result;
			}
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00019B30 File Offset: 0x00017D30
		public static Hash128 Compute(int val)
		{
			Hash128 result = default(Hash128);
			result.Append(val);
			return result;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00019B54 File Offset: 0x00017D54
		public static Hash128 Compute(float val)
		{
			Hash128 result = default(Hash128);
			result.Append(val);
			return result;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00019B78 File Offset: 0x00017D78
		public unsafe static Hash128 Compute(void* data, ulong size)
		{
			Hash128 result = default(Hash128);
			Hash128.ComputeFromPtr(new IntPtr(data), 0, (int)size, 1, ref result);
			return result;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00019BA5 File Offset: 0x00017DA5
		public void Append(string data)
		{
			Hash128.ComputeFromString(data, ref this);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00019BB0 File Offset: 0x00017DB0
		public void Append<T>(NativeArray<T> data) where T : struct
		{
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), 0, data.Length, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00019BD4 File Offset: 0x00017DD4
		public void Append<T>(NativeArray<T> data, int start, int count) where T : struct
		{
			bool flag = start < 0 || count < 0 || start + count > data.Length;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128.ComputeFromPtr((IntPtr)data.GetUnsafeReadOnlyPtr<T>(), start, count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00019C34 File Offset: 0x00017E34
		public void Append<T>(T[] data) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Append must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			Hash128.ComputeFromArray(data, 0, data.Length, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00019C78 File Offset: 0x00017E78
		public void Append<T>(T[] data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsArrayBlittable(data);
			if (flag)
			{
				throw new ArgumentException("Array passed to Append must be blittable.\n" + UnsafeUtility.GetReasonForArrayNonBlittable(data));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Length;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128.ComputeFromArray(data, start, count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00019CEC File Offset: 0x00017EEC
		public void Append<T>(List<T> data) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Append", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), 0, data.Count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00019D44 File Offset: 0x00017F44
		public void Append<T>(List<T> data, int start, int count) where T : struct
		{
			bool flag = !UnsafeUtility.IsGenericListBlittable<T>();
			if (flag)
			{
				throw new ArgumentException(string.Format("List<{0}> passed to {1} must be blittable.\n{2}", typeof(T), "Append", UnsafeUtility.GetReasonForGenericListNonBlittable<T>()));
			}
			bool flag2 = start < 0 || count < 0 || start + count > data.Count;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException(string.Format("Bad start/count arguments (start:{0} count:{1})", start, count));
			}
			Hash128.ComputeFromArray(NoAllocHelpers.ExtractArrayFromList(data), start, count, UnsafeUtility.SizeOf<T>(), ref this);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00019DCC File Offset: 0x00017FCC
		public unsafe void Append<[IsUnmanaged] T>(ref T val) where T : struct, ValueType
		{
			fixed (T* ptr = &val)
			{
				void* value = (void*)ptr;
				Hash128.ComputeFromPtr((IntPtr)value, 0, 1, UnsafeUtility.SizeOf<T>(), ref this);
			}
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00019DF8 File Offset: 0x00017FF8
		public void Append(int val)
		{
			this.ShortHash4((uint)val);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00019E03 File Offset: 0x00018003
		public unsafe void Append(float val)
		{
			this.ShortHash4(*(uint*)(&val));
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00019E11 File Offset: 0x00018011
		public unsafe void Append(void* data, ulong size)
		{
			Hash128.ComputeFromPtr(new IntPtr(data), 0, (int)size, 1, ref this);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00019E28 File Offset: 0x00018028
		public override bool Equals(object obj)
		{
			return obj is Hash128 && this == (Hash128)obj;
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00019E58 File Offset: 0x00018058
		public bool Equals(Hash128 obj)
		{
			return this == obj;
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00019E78 File Offset: 0x00018078
		public override int GetHashCode()
		{
			return this.u64_0.GetHashCode() ^ this.u64_1.GetHashCode();
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00019EA4 File Offset: 0x000180A4
		public int CompareTo(object obj)
		{
			bool flag = obj == null || !(obj is Hash128);
			int result;
			if (flag)
			{
				result = 1;
			}
			else
			{
				Hash128 rhs = (Hash128)obj;
				result = this.CompareTo(rhs);
			}
			return result;
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00019EE0 File Offset: 0x000180E0
		public static bool operator ==(Hash128 hash1, Hash128 hash2)
		{
			return hash1.u64_0 == hash2.u64_0 && hash1.u64_1 == hash2.u64_1;
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00019F14 File Offset: 0x00018114
		public static bool operator !=(Hash128 hash1, Hash128 hash2)
		{
			return !(hash1 == hash2);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00019F30 File Offset: 0x00018130
		public static bool operator <(Hash128 x, Hash128 y)
		{
			bool flag = x.u64_0 != y.u64_0;
			bool result;
			if (flag)
			{
				result = (x.u64_0 < y.u64_0);
			}
			else
			{
				result = (x.u64_1 < y.u64_1);
			}
			return result;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00019F78 File Offset: 0x00018178
		public static bool operator >(Hash128 x, Hash128 y)
		{
			bool flag = x < y;
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				bool flag2 = x == y;
				result = !flag2;
			}
			return result;
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00019FAC File Offset: 0x000181AC
		private void ShortHash4(uint data)
		{
			ulong num = this.u64_0;
			ulong num2 = this.u64_1;
			ulong num3 = 16045690984833335023UL;
			ulong num4 = 16045690984833335023UL;
			num4 += 288230376151711744UL;
			num3 += (ulong)data;
			Hash128.ShortEnd(ref num, ref num2, ref num3, ref num4);
			this.u64_0 = num;
			this.u64_1 = num2;
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x0001A00C File Offset: 0x0001820C
		private static void ShortEnd(ref ulong h0, ref ulong h1, ref ulong h2, ref ulong h3)
		{
			h3 ^= h2;
			Hash128.Rot64(ref h2, 15);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 52);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 26);
			h1 += h0;
			h2 ^= h1;
			Hash128.Rot64(ref h1, 51);
			h2 += h1;
			h3 ^= h2;
			Hash128.Rot64(ref h2, 28);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 9);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 47);
			h1 += h0;
			h2 ^= h1;
			Hash128.Rot64(ref h1, 54);
			h2 += h1;
			h3 ^= h2;
			Hash128.Rot64(ref h2, 32);
			h3 += h2;
			h0 ^= h3;
			Hash128.Rot64(ref h3, 25);
			h0 += h3;
			h1 ^= h0;
			Hash128.Rot64(ref h0, 63);
			h1 += h0;
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x0001A117 File Offset: 0x00018317
		private static void Rot64(ref ulong x, int k)
		{
			x = (x << k | x >> 64 - k);
		}

		// Token: 0x06001321 RID: 4897
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Parse_Injected(string hashString, out Hash128 ret);

		// Token: 0x06001322 RID: 4898
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Hash128ToStringImpl_Injected(ref Hash128 hash);

		// Token: 0x040005D8 RID: 1496
		internal ulong u64_0;

		// Token: 0x040005D9 RID: 1497
		internal ulong u64_1;

		// Token: 0x040005DA RID: 1498
		private const ulong kConst = 16045690984833335023UL;
	}
}

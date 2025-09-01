using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000135 RID: 309
	public struct HashCode
	{
		// Token: 0x06000BCC RID: 3020 RVA: 0x000314D8 File Offset: 0x0002F6D8
		private unsafe static uint GenerateGlobalSeed()
		{
			uint result;
			Interop.GetRandomBytes((byte*)(&result), 4);
			return result;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000314F0 File Offset: 0x0002F6F0
		public static int Combine<T1>(T1 value1)
		{
			uint queuedValue = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.MixEmptyState() + 4U, queuedValue));
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x00031528 File Offset: 0x0002F728
		public static int Combine<T1, T2>(T1 value1, T2 value2)
		{
			uint queuedValue = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint queuedValue2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixEmptyState() + 8U, queuedValue), queuedValue2));
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x00031580 File Offset: 0x0002F780
		public static int Combine<T1, T2, T3>(T1 value1, T2 value2, T3 value3)
		{
			uint queuedValue = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint queuedValue2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint queuedValue3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixEmptyState() + 12U, queuedValue), queuedValue2), queuedValue3));
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000315F8 File Offset: 0x0002F7F8
		public static int Combine<T1, T2, T3, T4>(T1 value1, T2 value2, T3 value3, T4 value4)
		{
			uint input = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint input2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint input3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint input4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num;
			uint num2;
			uint num3;
			uint num4;
			HashCode.Initialize(out num, out num2, out num3, out num4);
			num = HashCode.Round(num, input);
			num2 = HashCode.Round(num2, input2);
			num3 = HashCode.Round(num3, input3);
			num4 = HashCode.Round(num4, input4);
			return (int)HashCode.MixFinal(HashCode.MixState(num, num2, num3, num4) + 16U);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000316B4 File Offset: 0x0002F8B4
		public static int Combine<T1, T2, T3, T4, T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
		{
			uint input = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint input2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint input3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint input4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint queuedValue = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint num;
			uint num2;
			uint num3;
			uint num4;
			HashCode.Initialize(out num, out num2, out num3, out num4);
			num = HashCode.Round(num, input);
			num2 = HashCode.Round(num2, input2);
			num3 = HashCode.Round(num3, input3);
			num4 = HashCode.Round(num4, input4);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.MixState(num, num2, num3, num4) + 20U, queuedValue));
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x00031794 File Offset: 0x0002F994
		public static int Combine<T1, T2, T3, T4, T5, T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
		{
			uint input = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint input2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint input3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint input4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint queuedValue = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint queuedValue2 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint num;
			uint num2;
			uint num3;
			uint num4;
			HashCode.Initialize(out num, out num2, out num3, out num4);
			num = HashCode.Round(num, input);
			num2 = HashCode.Round(num2, input2);
			num3 = HashCode.Round(num3, input3);
			num4 = HashCode.Round(num4, input4);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixState(num, num2, num3, num4) + 24U, queuedValue), queuedValue2));
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x00031894 File Offset: 0x0002FA94
		public static int Combine<T1, T2, T3, T4, T5, T6, T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
		{
			uint input = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint input2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint input3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint input4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint queuedValue = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint queuedValue2 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint queuedValue3 = (uint)((value7 != null) ? value7.GetHashCode() : 0);
			uint num;
			uint num2;
			uint num3;
			uint num4;
			HashCode.Initialize(out num, out num2, out num3, out num4);
			num = HashCode.Round(num, input);
			num2 = HashCode.Round(num2, input2);
			num3 = HashCode.Round(num3, input3);
			num4 = HashCode.Round(num4, input4);
			return (int)HashCode.MixFinal(HashCode.QueueRound(HashCode.QueueRound(HashCode.QueueRound(HashCode.MixState(num, num2, num3, num4) + 28U, queuedValue), queuedValue2), queuedValue3));
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x000319B8 File Offset: 0x0002FBB8
		public static int Combine<T1, T2, T3, T4, T5, T6, T7, T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
		{
			uint input = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint input2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint input3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint input4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint input5 = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint input6 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint input7 = (uint)((value7 != null) ? value7.GetHashCode() : 0);
			uint input8 = (uint)((value8 != null) ? value8.GetHashCode() : 0);
			uint num;
			uint num2;
			uint num3;
			uint num4;
			HashCode.Initialize(out num, out num2, out num3, out num4);
			num = HashCode.Round(num, input);
			num2 = HashCode.Round(num2, input2);
			num3 = HashCode.Round(num3, input3);
			num4 = HashCode.Round(num4, input4);
			num = HashCode.Round(num, input5);
			num2 = HashCode.Round(num2, input6);
			num3 = HashCode.Round(num3, input7);
			num4 = HashCode.Round(num4, input8);
			return (int)HashCode.MixFinal(HashCode.MixState(num, num2, num3, num4) + 32U);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x00031B0B File Offset: 0x0002FD0B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Rol(uint value, int count)
		{
			return value << count | value >> 32 - count;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x00031B1D File Offset: 0x0002FD1D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Initialize(out uint v1, out uint v2, out uint v3, out uint v4)
		{
			v1 = HashCode.s_seed + 2654435761U + 2246822519U;
			v2 = HashCode.s_seed + 2246822519U;
			v3 = HashCode.s_seed;
			v4 = HashCode.s_seed - 2654435761U;
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x00031B53 File Offset: 0x0002FD53
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Round(uint hash, uint input)
		{
			hash += input * 2246822519U;
			hash = HashCode.Rol(hash, 13);
			hash *= 2654435761U;
			return hash;
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x00031B74 File Offset: 0x0002FD74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint QueueRound(uint hash, uint queuedValue)
		{
			hash += queuedValue * 3266489917U;
			return HashCode.Rol(hash, 17) * 668265263U;
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x00031B8F File Offset: 0x0002FD8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixState(uint v1, uint v2, uint v3, uint v4)
		{
			return HashCode.Rol(v1, 1) + HashCode.Rol(v2, 7) + HashCode.Rol(v3, 12) + HashCode.Rol(v4, 18);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x00031BB2 File Offset: 0x0002FDB2
		private static uint MixEmptyState()
		{
			return HashCode.s_seed + 374761393U;
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x00031BBF File Offset: 0x0002FDBF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixFinal(uint hash)
		{
			hash ^= hash >> 15;
			hash *= 2246822519U;
			hash ^= hash >> 13;
			hash *= 3266489917U;
			hash ^= hash >> 16;
			return hash;
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x00031BEC File Offset: 0x0002FDEC
		public void Add<T>(T value)
		{
			this.Add((value != null) ? value.GetHashCode() : 0);
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x00031C0C File Offset: 0x0002FE0C
		public void Add<T>(T value, IEqualityComparer<T> comparer)
		{
			this.Add((comparer != null) ? comparer.GetHashCode(value) : ((value != null) ? value.GetHashCode() : 0));
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x00031C38 File Offset: 0x0002FE38
		private void Add(int value)
		{
			uint length = this._length;
			this._length = length + 1U;
			uint num = length;
			uint num2 = num % 4U;
			if (num2 == 0U)
			{
				this._queue1 = (uint)value;
				return;
			}
			if (num2 == 1U)
			{
				this._queue2 = (uint)value;
				return;
			}
			if (num2 == 2U)
			{
				this._queue3 = (uint)value;
				return;
			}
			if (num == 3U)
			{
				HashCode.Initialize(out this._v1, out this._v2, out this._v3, out this._v4);
			}
			this._v1 = HashCode.Round(this._v1, this._queue1);
			this._v2 = HashCode.Round(this._v2, this._queue2);
			this._v3 = HashCode.Round(this._v3, this._queue3);
			this._v4 = HashCode.Round(this._v4, (uint)value);
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x00031CF8 File Offset: 0x0002FEF8
		public int ToHashCode()
		{
			uint length = this._length;
			uint num = length % 4U;
			uint num2 = (length < 4U) ? HashCode.MixEmptyState() : HashCode.MixState(this._v1, this._v2, this._v3, this._v4);
			num2 += length * 4U;
			if (num > 0U)
			{
				num2 = HashCode.QueueRound(num2, this._queue1);
				if (num > 1U)
				{
					num2 = HashCode.QueueRound(num2, this._queue2);
					if (num > 2U)
					{
						num2 = HashCode.QueueRound(num2, this._queue3);
					}
				}
			}
			return (int)HashCode.MixFinal(num2);
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00031D7A File Offset: 0x0002FF7A
		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.", true)]
		public override int GetHashCode()
		{
			throw new NotSupportedException("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.");
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x00031D86 File Offset: 0x0002FF86
		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes.", true)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException("HashCode is a mutable struct and should not be compared with other HashCodes.");
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x00031D92 File Offset: 0x0002FF92
		// Note: this type is marked as 'beforefieldinit'.
		static HashCode()
		{
		}

		// Token: 0x04001243 RID: 4675
		private static readonly uint s_seed = HashCode.GenerateGlobalSeed();

		// Token: 0x04001244 RID: 4676
		private const uint Prime1 = 2654435761U;

		// Token: 0x04001245 RID: 4677
		private const uint Prime2 = 2246822519U;

		// Token: 0x04001246 RID: 4678
		private const uint Prime3 = 3266489917U;

		// Token: 0x04001247 RID: 4679
		private const uint Prime4 = 668265263U;

		// Token: 0x04001248 RID: 4680
		private const uint Prime5 = 374761393U;

		// Token: 0x04001249 RID: 4681
		private uint _v1;

		// Token: 0x0400124A RID: 4682
		private uint _v2;

		// Token: 0x0400124B RID: 4683
		private uint _v3;

		// Token: 0x0400124C RID: 4684
		private uint _v4;

		// Token: 0x0400124D RID: 4685
		private uint _queue1;

		// Token: 0x0400124E RID: 4686
		private uint _queue2;

		// Token: 0x0400124F RID: 4687
		private uint _queue3;

		// Token: 0x04001250 RID: 4688
		private uint _length;
	}
}

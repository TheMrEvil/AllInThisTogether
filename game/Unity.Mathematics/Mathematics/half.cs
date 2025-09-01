using System;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000028 RID: 40
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct half : IEquatable<half>, IFormattable
	{
		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x0600158F RID: 5519 RVA: 0x0003DF5B File Offset: 0x0003C15B
		public static float MaxValue
		{
			get
			{
				return 65504f;
			}
		}

		// Token: 0x170005C9 RID: 1481
		// (get) Token: 0x06001590 RID: 5520 RVA: 0x0003DF62 File Offset: 0x0003C162
		public static float MinValue
		{
			get
			{
				return -65504f;
			}
		}

		// Token: 0x170005CA RID: 1482
		// (get) Token: 0x06001591 RID: 5521 RVA: 0x0003DF69 File Offset: 0x0003C169
		public static half MaxValueAsHalf
		{
			get
			{
				return new half(half.MaxValue);
			}
		}

		// Token: 0x170005CB RID: 1483
		// (get) Token: 0x06001592 RID: 5522 RVA: 0x0003DF75 File Offset: 0x0003C175
		public static half MinValueAsHalf
		{
			get
			{
				return new half(half.MinValue);
			}
		}

		// Token: 0x06001593 RID: 5523 RVA: 0x0003DF81 File Offset: 0x0003C181
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half(half x)
		{
			this.value = x.value;
		}

		// Token: 0x06001594 RID: 5524 RVA: 0x0003DF8F File Offset: 0x0003C18F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half(float v)
		{
			this.value = (ushort)math.f32tof16(v);
		}

		// Token: 0x06001595 RID: 5525 RVA: 0x0003DF9E File Offset: 0x0003C19E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public half(double v)
		{
			this.value = (ushort)math.f32tof16((float)v);
		}

		// Token: 0x06001596 RID: 5526 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half(float v)
		{
			return new half(v);
		}

		// Token: 0x06001597 RID: 5527 RVA: 0x0003DFB6 File Offset: 0x0003C1B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator half(double v)
		{
			return new half(v);
		}

		// Token: 0x06001598 RID: 5528 RVA: 0x0003DFBE File Offset: 0x0003C1BE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float(half d)
		{
			return math.f16tof32((uint)d.value);
		}

		// Token: 0x06001599 RID: 5529 RVA: 0x0003DFCB File Offset: 0x0003C1CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator double(half d)
		{
			return (double)math.f16tof32((uint)d.value);
		}

		// Token: 0x0600159A RID: 5530 RVA: 0x0003DFD9 File Offset: 0x0003C1D9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(half lhs, half rhs)
		{
			return lhs.value == rhs.value;
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0003DFE9 File Offset: 0x0003C1E9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(half lhs, half rhs)
		{
			return lhs.value != rhs.value;
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0003DFFC File Offset: 0x0003C1FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(half rhs)
		{
			return this.value == rhs.value;
		}

		// Token: 0x0600159D RID: 5533 RVA: 0x0003E00C File Offset: 0x0003C20C
		public override bool Equals(object o)
		{
			if (o is half)
			{
				half rhs = (half)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x0600159E RID: 5534 RVA: 0x0003E031 File Offset: 0x0003C231
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)this.value;
		}

		// Token: 0x0600159F RID: 5535 RVA: 0x0003E03C File Offset: 0x0003C23C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return math.f16tof32((uint)this.value).ToString();
		}

		// Token: 0x060015A0 RID: 5536 RVA: 0x0003E05C File Offset: 0x0003C25C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return math.f16tof32((uint)this.value).ToString(format, formatProvider);
		}

		// Token: 0x060015A1 RID: 5537 RVA: 0x0003E07E File Offset: 0x0003C27E
		// Note: this type is marked as 'beforefieldinit'.
		static half()
		{
		}

		// Token: 0x040000A1 RID: 161
		public ushort value;

		// Token: 0x040000A2 RID: 162
		public static readonly half zero;
	}
}

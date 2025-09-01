using System;
using System.Runtime.CompilerServices;

namespace MagicaCloth2
{
	// Token: 0x02000132 RID: 306
	[Serializable]
	public struct VertexAttribute : IEquatable<VertexAttribute>
	{
		// Token: 0x0600051A RID: 1306 RVA: 0x0002B1C5 File Offset: 0x000293C5
		public VertexAttribute(byte initialValue = 0)
		{
			this.Value = initialValue;
		}

		// Token: 0x0600051B RID: 1307 RVA: 0x0002B1CE File Offset: 0x000293CE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clear()
		{
			this.Value = 0;
		}

		// Token: 0x0600051C RID: 1308 RVA: 0x0002B1D7 File Offset: 0x000293D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFlag(byte flag, bool sw)
		{
			if (sw)
			{
				this.Value |= flag;
				return;
			}
			this.Value &= ~flag;
		}

		// Token: 0x0600051D RID: 1309 RVA: 0x0002B1FC File Offset: 0x000293FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void SetFlag(VertexAttribute attr, bool sw)
		{
			if (sw)
			{
				this.Value |= attr.Value;
				return;
			}
			this.Value &= ~attr.Value;
		}

		// Token: 0x0600051E RID: 1310 RVA: 0x0002B22B File Offset: 0x0002942B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsSet(byte flag)
		{
			return (this.Value & flag) > 0;
		}

		// Token: 0x0600051F RID: 1311 RVA: 0x0002B238 File Offset: 0x00029438
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsInvalid()
		{
			return !this.IsSet(3);
		}

		// Token: 0x06000520 RID: 1312 RVA: 0x0002B244 File Offset: 0x00029444
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsFixed()
		{
			return this.IsSet(1);
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0002B24D File Offset: 0x0002944D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsMove()
		{
			return this.IsSet(2);
		}

		// Token: 0x06000522 RID: 1314 RVA: 0x0002B256 File Offset: 0x00029456
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsDontMove()
		{
			return !this.IsSet(2);
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x0002B262 File Offset: 0x00029462
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool IsMotion()
		{
			return !this.IsSet(8);
		}

		// Token: 0x06000524 RID: 1316 RVA: 0x0002B26E File Offset: 0x0002946E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static VertexAttribute JoinAttribute(VertexAttribute attr1, VertexAttribute attr2)
		{
			if (attr1.Value >= attr2.Value)
			{
				return attr2;
			}
			return attr1;
		}

		// Token: 0x06000525 RID: 1317 RVA: 0x0002B281 File Offset: 0x00029481
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(VertexAttribute other)
		{
			return this.Value == other.Value;
		}

		// Token: 0x06000526 RID: 1318 RVA: 0x0002B294 File Offset: 0x00029494
		public override bool Equals(object obj)
		{
			if (obj is VertexAttribute)
			{
				VertexAttribute other = (VertexAttribute)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000527 RID: 1319 RVA: 0x0002B2B9 File Offset: 0x000294B9
		public override int GetHashCode()
		{
			return (int)this.Value;
		}

		// Token: 0x06000528 RID: 1320 RVA: 0x0002B281 File Offset: 0x00029481
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(VertexAttribute lhs, VertexAttribute rhs)
		{
			return lhs.Value == rhs.Value;
		}

		// Token: 0x06000529 RID: 1321 RVA: 0x0002B2C1 File Offset: 0x000294C1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(VertexAttribute lhs, VertexAttribute rhs)
		{
			return lhs.Value != rhs.Value;
		}

		// Token: 0x0600052A RID: 1322 RVA: 0x0002B2D4 File Offset: 0x000294D4
		// Note: this type is marked as 'beforefieldinit'.
		static VertexAttribute()
		{
		}

		// Token: 0x040007D9 RID: 2009
		public const byte Flag_Fixed = 1;

		// Token: 0x040007DA RID: 2010
		public const byte Flag_Move = 2;

		// Token: 0x040007DB RID: 2011
		public const byte Flag_InvalidMotion = 8;

		// Token: 0x040007DC RID: 2012
		public const byte Flag_Triangle = 128;

		// Token: 0x040007DD RID: 2013
		public static readonly VertexAttribute Invalid = default(VertexAttribute);

		// Token: 0x040007DE RID: 2014
		public static readonly VertexAttribute Fixed = new VertexAttribute(1);

		// Token: 0x040007DF RID: 2015
		public static readonly VertexAttribute Move = new VertexAttribute(2);

		// Token: 0x040007E0 RID: 2016
		public byte Value;
	}
}

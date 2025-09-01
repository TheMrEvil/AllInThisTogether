using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001CB RID: 459
	[NativeType("Runtime/Math/Vector2Int.h")]
	[UsedByNativeCode]
	[Il2CppEagerStaticClassConstruction]
	public struct Vector2Int : IEquatable<Vector2Int>, IFormattable
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001512 RID: 5394 RVA: 0x000212C4 File Offset: 0x0001F4C4
		// (set) Token: 0x06001513 RID: 5395 RVA: 0x000212DC File Offset: 0x0001F4DC
		public int x
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_X;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_X = value;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001514 RID: 5396 RVA: 0x000212E8 File Offset: 0x0001F4E8
		// (set) Token: 0x06001515 RID: 5397 RVA: 0x00021300 File Offset: 0x0001F500
		public int y
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Y;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Y = value;
			}
		}

		// Token: 0x06001516 RID: 5398 RVA: 0x0002130A File Offset: 0x0001F50A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector2Int(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		// Token: 0x06001517 RID: 5399 RVA: 0x0002130A File Offset: 0x0001F50A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
		}

		// Token: 0x17000436 RID: 1078
		public int this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				int result;
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
					}
					result = this.y;
				}
				else
				{
					result = this.x;
				}
				return result;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException(string.Format("Invalid Vector2Int index addressed: {0}!", index));
					}
					this.y = value;
				}
				else
				{
					this.x = value;
				}
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600151A RID: 5402 RVA: 0x000213AC File Offset: 0x0001F5AC
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y));
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x000213E0 File Offset: 0x0001F5E0
		public int sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.x * this.x + this.y * this.y;
			}
		}

		// Token: 0x0600151C RID: 5404 RVA: 0x00021410 File Offset: 0x0001F610
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector2Int a, Vector2Int b)
		{
			float num = (float)(a.x - b.x);
			float num2 = (float)(a.y - b.y);
			return (float)Math.Sqrt((double)(num * num + num2 * num2));
		}

		// Token: 0x0600151D RID: 5405 RVA: 0x00021454 File Offset: 0x0001F654
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Min(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y));
		}

		// Token: 0x0600151E RID: 5406 RVA: 0x00021494 File Offset: 0x0001F694
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Max(Vector2Int lhs, Vector2Int rhs)
		{
			return new Vector2Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y));
		}

		// Token: 0x0600151F RID: 5407 RVA: 0x000214D4 File Offset: 0x0001F6D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int Scale(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		// Token: 0x06001520 RID: 5408 RVA: 0x00021509 File Offset: 0x0001F709
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector2Int scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
		}

		// Token: 0x06001521 RID: 5409 RVA: 0x00021538 File Offset: 0x0001F738
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clamp(Vector2Int min, Vector2Int max)
		{
			this.x = Math.Max(min.x, this.x);
			this.x = Math.Min(max.x, this.x);
			this.y = Math.Max(min.y, this.y);
			this.y = Math.Min(max.y, this.y);
		}

		// Token: 0x06001522 RID: 5410 RVA: 0x000215AC File Offset: 0x0001F7AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector2(Vector2Int v)
		{
			return new Vector2((float)v.x, (float)v.y);
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x000215D4 File Offset: 0x0001F7D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector3Int(Vector2Int v)
		{
			return new Vector3Int(v.x, v.y, 0);
		}

		// Token: 0x06001524 RID: 5412 RVA: 0x000215FC File Offset: 0x0001F7FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int FloorToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y));
		}

		// Token: 0x06001525 RID: 5413 RVA: 0x0002162C File Offset: 0x0001F82C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int CeilToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y));
		}

		// Token: 0x06001526 RID: 5414 RVA: 0x0002165C File Offset: 0x0001F85C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int RoundToInt(Vector2 v)
		{
			return new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));
		}

		// Token: 0x06001527 RID: 5415 RVA: 0x0002168C File Offset: 0x0001F88C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator -(Vector2Int v)
		{
			return new Vector2Int(-v.x, -v.y);
		}

		// Token: 0x06001528 RID: 5416 RVA: 0x000216B4 File Offset: 0x0001F8B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator +(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x + b.x, a.y + b.y);
		}

		// Token: 0x06001529 RID: 5417 RVA: 0x000216EC File Offset: 0x0001F8EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator -(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x - b.x, a.y - b.y);
		}

		// Token: 0x0600152A RID: 5418 RVA: 0x00021724 File Offset: 0x0001F924
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator *(Vector2Int a, Vector2Int b)
		{
			return new Vector2Int(a.x * b.x, a.y * b.y);
		}

		// Token: 0x0600152B RID: 5419 RVA: 0x0002175C File Offset: 0x0001F95C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator *(int a, Vector2Int b)
		{
			return new Vector2Int(a * b.x, a * b.y);
		}

		// Token: 0x0600152C RID: 5420 RVA: 0x00021788 File Offset: 0x0001F988
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator *(Vector2Int a, int b)
		{
			return new Vector2Int(a.x * b, a.y * b);
		}

		// Token: 0x0600152D RID: 5421 RVA: 0x000217B4 File Offset: 0x0001F9B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector2Int operator /(Vector2Int a, int b)
		{
			return new Vector2Int(a.x / b, a.y / b);
		}

		// Token: 0x0600152E RID: 5422 RVA: 0x000217E0 File Offset: 0x0001F9E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector2Int lhs, Vector2Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y;
		}

		// Token: 0x0600152F RID: 5423 RVA: 0x00021818 File Offset: 0x0001FA18
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector2Int lhs, Vector2Int rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001530 RID: 5424 RVA: 0x00021834 File Offset: 0x0001FA34
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector2Int);
			return !flag && this.Equals((Vector2Int)other);
		}

		// Token: 0x06001531 RID: 5425 RVA: 0x00021868 File Offset: 0x0001FA68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector2Int other)
		{
			return this.x == other.x && this.y == other.y;
		}

		// Token: 0x06001532 RID: 5426 RVA: 0x0002189C File Offset: 0x0001FA9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return this.x.GetHashCode() ^ this.y.GetHashCode() << 2;
		}

		// Token: 0x06001533 RID: 5427 RVA: 0x000218D0 File Offset: 0x0001FAD0
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001534 RID: 5428 RVA: 0x000218EC File Offset: 0x0001FAEC
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001535 RID: 5429 RVA: 0x00021908 File Offset: 0x0001FB08
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider)
			});
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001536 RID: 5430 RVA: 0x00021964 File Offset: 0x0001FB64
		public static Vector2Int zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Zero;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x0002197C File Offset: 0x0001FB7C
		public static Vector2Int one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_One;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001538 RID: 5432 RVA: 0x00021994 File Offset: 0x0001FB94
		public static Vector2Int up
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Up;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x000219AC File Offset: 0x0001FBAC
		public static Vector2Int down
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Down;
			}
		}

		// Token: 0x1700043D RID: 1085
		// (get) Token: 0x0600153A RID: 5434 RVA: 0x000219C4 File Offset: 0x0001FBC4
		public static Vector2Int left
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Left;
			}
		}

		// Token: 0x1700043E RID: 1086
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x000219DC File Offset: 0x0001FBDC
		public static Vector2Int right
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector2Int.s_Right;
			}
		}

		// Token: 0x0600153C RID: 5436 RVA: 0x000219F4 File Offset: 0x0001FBF4
		// Note: this type is marked as 'beforefieldinit'.
		static Vector2Int()
		{
		}

		// Token: 0x0400078C RID: 1932
		private int m_X;

		// Token: 0x0400078D RID: 1933
		private int m_Y;

		// Token: 0x0400078E RID: 1934
		private static readonly Vector2Int s_Zero = new Vector2Int(0, 0);

		// Token: 0x0400078F RID: 1935
		private static readonly Vector2Int s_One = new Vector2Int(1, 1);

		// Token: 0x04000790 RID: 1936
		private static readonly Vector2Int s_Up = new Vector2Int(0, 1);

		// Token: 0x04000791 RID: 1937
		private static readonly Vector2Int s_Down = new Vector2Int(0, -1);

		// Token: 0x04000792 RID: 1938
		private static readonly Vector2Int s_Left = new Vector2Int(-1, 0);

		// Token: 0x04000793 RID: 1939
		private static readonly Vector2Int s_Right = new Vector2Int(1, 0);
	}
}

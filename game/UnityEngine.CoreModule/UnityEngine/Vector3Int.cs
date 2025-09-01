using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001CC RID: 460
	[UsedByNativeCode]
	[Il2CppEagerStaticClassConstruction]
	public struct Vector3Int : IEquatable<Vector3Int>, IFormattable
	{
		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x00021A4C File Offset: 0x0001FC4C
		// (set) Token: 0x0600153E RID: 5438 RVA: 0x00021A64 File Offset: 0x0001FC64
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

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00021A70 File Offset: 0x0001FC70
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x00021A88 File Offset: 0x0001FC88
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

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x00021A94 File Offset: 0x0001FC94
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x00021AAC File Offset: 0x0001FCAC
		public int z
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.m_Z;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.m_Z = value;
			}
		}

		// Token: 0x06001543 RID: 5443 RVA: 0x00021AB6 File Offset: 0x0001FCB6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3Int(int x, int y)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = 0;
		}

		// Token: 0x06001544 RID: 5444 RVA: 0x00021ACE File Offset: 0x0001FCCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public Vector3Int(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x06001545 RID: 5445 RVA: 0x00021ACE File Offset: 0x0001FCCE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Set(int x, int y, int z)
		{
			this.m_X = x;
			this.m_Y = y;
			this.m_Z = z;
		}

		// Token: 0x17000442 RID: 1090
		public int this[int index]
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				int result;
				switch (index)
				{
				case 0:
					result = this.x;
					break;
				case 1:
					result = this.y;
					break;
				case 2:
					result = this.z;
					break;
				default:
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", new object[]
					{
						index
					}));
				}
				return result;
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				switch (index)
				{
				case 0:
					this.x = value;
					break;
				case 1:
					this.y = value;
					break;
				case 2:
					this.z = value;
					break;
				default:
					throw new IndexOutOfRangeException(UnityString.Format("Invalid Vector3Int index addressed: {0}!", new object[]
					{
						index
					}));
				}
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06001548 RID: 5448 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		public float magnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Mathf.Sqrt((float)(this.x * this.x + this.y * this.y + this.z * this.z));
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x00021BF4 File Offset: 0x0001FDF4
		public int sqrMagnitude
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return this.x * this.x + this.y * this.y + this.z * this.z;
			}
		}

		// Token: 0x0600154A RID: 5450 RVA: 0x00021C30 File Offset: 0x0001FE30
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Distance(Vector3Int a, Vector3Int b)
		{
			return (a - b).magnitude;
		}

		// Token: 0x0600154B RID: 5451 RVA: 0x00021C54 File Offset: 0x0001FE54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Min(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Min(lhs.x, rhs.x), Mathf.Min(lhs.y, rhs.y), Mathf.Min(lhs.z, rhs.z));
		}

		// Token: 0x0600154C RID: 5452 RVA: 0x00021CA4 File Offset: 0x0001FEA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Max(Vector3Int lhs, Vector3Int rhs)
		{
			return new Vector3Int(Mathf.Max(lhs.x, rhs.x), Mathf.Max(lhs.y, rhs.y), Mathf.Max(lhs.z, rhs.z));
		}

		// Token: 0x0600154D RID: 5453 RVA: 0x00021CF4 File Offset: 0x0001FEF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int Scale(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x0600154E RID: 5454 RVA: 0x00021D38 File Offset: 0x0001FF38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Scale(Vector3Int scale)
		{
			this.x *= scale.x;
			this.y *= scale.y;
			this.z *= scale.z;
		}

		// Token: 0x0600154F RID: 5455 RVA: 0x00021D88 File Offset: 0x0001FF88
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public void Clamp(Vector3Int min, Vector3Int max)
		{
			this.x = Math.Max(min.x, this.x);
			this.x = Math.Min(max.x, this.x);
			this.y = Math.Max(min.y, this.y);
			this.y = Math.Min(max.y, this.y);
			this.z = Math.Max(min.z, this.z);
			this.z = Math.Min(max.z, this.z);
		}

		// Token: 0x06001550 RID: 5456 RVA: 0x00021E2C File Offset: 0x0002002C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator Vector3(Vector3Int v)
		{
			return new Vector3((float)v.x, (float)v.y, (float)v.z);
		}

		// Token: 0x06001551 RID: 5457 RVA: 0x00021E5C File Offset: 0x0002005C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator Vector2Int(Vector3Int v)
		{
			return new Vector2Int(v.x, v.y);
		}

		// Token: 0x06001552 RID: 5458 RVA: 0x00021E84 File Offset: 0x00020084
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int FloorToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.FloorToInt(v.x), Mathf.FloorToInt(v.y), Mathf.FloorToInt(v.z));
		}

		// Token: 0x06001553 RID: 5459 RVA: 0x00021EBC File Offset: 0x000200BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int CeilToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.CeilToInt(v.x), Mathf.CeilToInt(v.y), Mathf.CeilToInt(v.z));
		}

		// Token: 0x06001554 RID: 5460 RVA: 0x00021EF4 File Offset: 0x000200F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int RoundToInt(Vector3 v)
		{
			return new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
		}

		// Token: 0x06001555 RID: 5461 RVA: 0x00021F2C File Offset: 0x0002012C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator +(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x + b.x, a.y + b.y, a.z + b.z);
		}

		// Token: 0x06001556 RID: 5462 RVA: 0x00021F70 File Offset: 0x00020170
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator -(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x - b.x, a.y - b.y, a.z - b.z);
		}

		// Token: 0x06001557 RID: 5463 RVA: 0x00021FB4 File Offset: 0x000201B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator *(Vector3Int a, Vector3Int b)
		{
			return new Vector3Int(a.x * b.x, a.y * b.y, a.z * b.z);
		}

		// Token: 0x06001558 RID: 5464 RVA: 0x00021FF8 File Offset: 0x000201F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator -(Vector3Int a)
		{
			return new Vector3Int(-a.x, -a.y, -a.z);
		}

		// Token: 0x06001559 RID: 5465 RVA: 0x00022028 File Offset: 0x00020228
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator *(Vector3Int a, int b)
		{
			return new Vector3Int(a.x * b, a.y * b, a.z * b);
		}

		// Token: 0x0600155A RID: 5466 RVA: 0x0002205C File Offset: 0x0002025C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator *(int a, Vector3Int b)
		{
			return new Vector3Int(a * b.x, a * b.y, a * b.z);
		}

		// Token: 0x0600155B RID: 5467 RVA: 0x00022090 File Offset: 0x00020290
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector3Int operator /(Vector3Int a, int b)
		{
			return new Vector3Int(a.x / b, a.y / b, a.z / b);
		}

		// Token: 0x0600155C RID: 5468 RVA: 0x000220C4 File Offset: 0x000202C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Vector3Int lhs, Vector3Int rhs)
		{
			return lhs.x == rhs.x && lhs.y == rhs.y && lhs.z == rhs.z;
		}

		// Token: 0x0600155D RID: 5469 RVA: 0x0002210C File Offset: 0x0002030C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator !=(Vector3Int lhs, Vector3Int rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600155E RID: 5470 RVA: 0x00022128 File Offset: 0x00020328
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(object other)
		{
			bool flag = !(other is Vector3Int);
			return !flag && this.Equals((Vector3Int)other);
		}

		// Token: 0x0600155F RID: 5471 RVA: 0x0002215C File Offset: 0x0002035C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(Vector3Int other)
		{
			return this == other;
		}

		// Token: 0x06001560 RID: 5472 RVA: 0x0002217C File Offset: 0x0002037C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			int hashCode = this.y.GetHashCode();
			int hashCode2 = this.z.GetHashCode();
			return this.x.GetHashCode() ^ hashCode << 4 ^ hashCode >> 28 ^ hashCode2 >> 4 ^ hashCode2 << 28;
		}

		// Token: 0x06001561 RID: 5473 RVA: 0x000221CC File Offset: 0x000203CC
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001562 RID: 5474 RVA: 0x000221E8 File Offset: 0x000203E8
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001563 RID: 5475 RVA: 0x00022204 File Offset: 0x00020404
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("({0}, {1}, {2})", new object[]
			{
				this.x.ToString(format, formatProvider),
				this.y.ToString(format, formatProvider),
				this.z.ToString(format, formatProvider)
			});
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001564 RID: 5476 RVA: 0x00022274 File Offset: 0x00020474
		public static Vector3Int zero
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Zero;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x0002228C File Offset: 0x0002048C
		public static Vector3Int one
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_One;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001566 RID: 5478 RVA: 0x000222A4 File Offset: 0x000204A4
		public static Vector3Int up
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Up;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x000222BC File Offset: 0x000204BC
		public static Vector3Int down
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Down;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06001568 RID: 5480 RVA: 0x000222D4 File Offset: 0x000204D4
		public static Vector3Int left
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Left;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x000222EC File Offset: 0x000204EC
		public static Vector3Int right
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Right;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x00022304 File Offset: 0x00020504
		public static Vector3Int forward
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Forward;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x0002231C File Offset: 0x0002051C
		public static Vector3Int back
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return Vector3Int.s_Back;
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00022334 File Offset: 0x00020534
		// Note: this type is marked as 'beforefieldinit'.
		static Vector3Int()
		{
		}

		// Token: 0x04000794 RID: 1940
		private int m_X;

		// Token: 0x04000795 RID: 1941
		private int m_Y;

		// Token: 0x04000796 RID: 1942
		private int m_Z;

		// Token: 0x04000797 RID: 1943
		private static readonly Vector3Int s_Zero = new Vector3Int(0, 0, 0);

		// Token: 0x04000798 RID: 1944
		private static readonly Vector3Int s_One = new Vector3Int(1, 1, 1);

		// Token: 0x04000799 RID: 1945
		private static readonly Vector3Int s_Up = new Vector3Int(0, 1, 0);

		// Token: 0x0400079A RID: 1946
		private static readonly Vector3Int s_Down = new Vector3Int(0, -1, 0);

		// Token: 0x0400079B RID: 1947
		private static readonly Vector3Int s_Left = new Vector3Int(-1, 0, 0);

		// Token: 0x0400079C RID: 1948
		private static readonly Vector3Int s_Right = new Vector3Int(1, 0, 0);

		// Token: 0x0400079D RID: 1949
		private static readonly Vector3Int s_Forward = new Vector3Int(0, 0, 1);

		// Token: 0x0400079E RID: 1950
		private static readonly Vector3Int s_Back = new Vector3Int(0, 0, -1);
	}
}

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Unity.Mathematics
{
	// Token: 0x0200001C RID: 28
	[DebuggerTypeProxy(typeof(float2.DebuggerProxy))]
	[Il2CppEagerStaticClassConstruction]
	[Serializable]
	public struct float2 : IEquatable<float2>, IFormattable
	{
		// Token: 0x06001011 RID: 4113 RVA: 0x0002F3CF File Offset: 0x0002D5CF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(float x, float y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x06001012 RID: 4114 RVA: 0x0002F3DF File Offset: 0x0002D5DF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(float2 xy)
		{
			this.x = xy.x;
			this.y = xy.y;
		}

		// Token: 0x06001013 RID: 4115 RVA: 0x0002F3F9 File Offset: 0x0002D5F9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(float v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06001014 RID: 4116 RVA: 0x0002F409 File Offset: 0x0002D609
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(bool v)
		{
			this.x = (v ? 1f : 0f);
			this.y = (v ? 1f : 0f);
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x0002F435 File Offset: 0x0002D635
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(bool2 v)
		{
			this.x = (v.x ? 1f : 0f);
			this.y = (v.y ? 1f : 0f);
		}

		// Token: 0x06001016 RID: 4118 RVA: 0x0002F46B File Offset: 0x0002D66B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(int v)
		{
			this.x = (float)v;
			this.y = (float)v;
		}

		// Token: 0x06001017 RID: 4119 RVA: 0x0002F47D File Offset: 0x0002D67D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(int2 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
		}

		// Token: 0x06001018 RID: 4120 RVA: 0x0002F499 File Offset: 0x0002D699
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(uint v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x06001019 RID: 4121 RVA: 0x0002F4AD File Offset: 0x0002D6AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(uint2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0002F4CB File Offset: 0x0002D6CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(half v)
		{
			this.x = v;
			this.y = v;
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0002F4E5 File Offset: 0x0002D6E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(half2 v)
		{
			this.x = v.x;
			this.y = v.y;
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0002F509 File Offset: 0x0002D709
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(double v)
		{
			this.x = (float)v;
			this.y = (float)v;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0002F51B File Offset: 0x0002D71B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public float2(double2 v)
		{
			this.x = (float)v.x;
			this.y = (float)v.y;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0002F537 File Offset: 0x0002D737
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(float v)
		{
			return new float2(v);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0002F53F File Offset: 0x0002D73F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2(bool v)
		{
			return new float2(v);
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0002F547 File Offset: 0x0002D747
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2(bool2 v)
		{
			return new float2(v);
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0002F54F File Offset: 0x0002D74F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(int v)
		{
			return new float2(v);
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0002F557 File Offset: 0x0002D757
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(int2 v)
		{
			return new float2(v);
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0002F55F File Offset: 0x0002D75F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(uint v)
		{
			return new float2(v);
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0002F567 File Offset: 0x0002D767
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(uint2 v)
		{
			return new float2(v);
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0002F56F File Offset: 0x0002D76F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(half v)
		{
			return new float2(v);
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0002F577 File Offset: 0x0002D777
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static implicit operator float2(half2 v)
		{
			return new float2(v);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0002F57F File Offset: 0x0002D77F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2(double v)
		{
			return new float2(v);
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0002F587 File Offset: 0x0002D787
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static explicit operator float2(double2 v)
		{
			return new float2(v);
		}

		// Token: 0x06001029 RID: 4137 RVA: 0x0002F58F File Offset: 0x0002D78F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator *(float2 lhs, float2 rhs)
		{
			return new float2(lhs.x * rhs.x, lhs.y * rhs.y);
		}

		// Token: 0x0600102A RID: 4138 RVA: 0x0002F5B0 File Offset: 0x0002D7B0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator *(float2 lhs, float rhs)
		{
			return new float2(lhs.x * rhs, lhs.y * rhs);
		}

		// Token: 0x0600102B RID: 4139 RVA: 0x0002F5C7 File Offset: 0x0002D7C7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator *(float lhs, float2 rhs)
		{
			return new float2(lhs * rhs.x, lhs * rhs.y);
		}

		// Token: 0x0600102C RID: 4140 RVA: 0x0002F5DE File Offset: 0x0002D7DE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator +(float2 lhs, float2 rhs)
		{
			return new float2(lhs.x + rhs.x, lhs.y + rhs.y);
		}

		// Token: 0x0600102D RID: 4141 RVA: 0x0002F5FF File Offset: 0x0002D7FF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator +(float2 lhs, float rhs)
		{
			return new float2(lhs.x + rhs, lhs.y + rhs);
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x0002F616 File Offset: 0x0002D816
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator +(float lhs, float2 rhs)
		{
			return new float2(lhs + rhs.x, lhs + rhs.y);
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0002F62D File Offset: 0x0002D82D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator -(float2 lhs, float2 rhs)
		{
			return new float2(lhs.x - rhs.x, lhs.y - rhs.y);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x0002F64E File Offset: 0x0002D84E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator -(float2 lhs, float rhs)
		{
			return new float2(lhs.x - rhs, lhs.y - rhs);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x0002F665 File Offset: 0x0002D865
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator -(float lhs, float2 rhs)
		{
			return new float2(lhs - rhs.x, lhs - rhs.y);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x0002F67C File Offset: 0x0002D87C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator /(float2 lhs, float2 rhs)
		{
			return new float2(lhs.x / rhs.x, lhs.y / rhs.y);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x0002F69D File Offset: 0x0002D89D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator /(float2 lhs, float rhs)
		{
			return new float2(lhs.x / rhs, lhs.y / rhs);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x0002F6B4 File Offset: 0x0002D8B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator /(float lhs, float2 rhs)
		{
			return new float2(lhs / rhs.x, lhs / rhs.y);
		}

		// Token: 0x06001035 RID: 4149 RVA: 0x0002F6CB File Offset: 0x0002D8CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator %(float2 lhs, float2 rhs)
		{
			return new float2(lhs.x % rhs.x, lhs.y % rhs.y);
		}

		// Token: 0x06001036 RID: 4150 RVA: 0x0002F6EC File Offset: 0x0002D8EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator %(float2 lhs, float rhs)
		{
			return new float2(lhs.x % rhs, lhs.y % rhs);
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0002F703 File Offset: 0x0002D903
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator %(float lhs, float2 rhs)
		{
			return new float2(lhs % rhs.x, lhs % rhs.y);
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0002F71C File Offset: 0x0002D91C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator ++(float2 val)
		{
			float num = val.x + 1f;
			val.x = num;
			float num2 = num;
			num = val.y + 1f;
			val.y = num;
			return new float2(num2, num);
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0002F754 File Offset: 0x0002D954
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator --(float2 val)
		{
			float num = val.x - 1f;
			val.x = num;
			float num2 = num;
			num = val.y - 1f;
			val.y = num;
			return new float2(num2, num);
		}

		// Token: 0x0600103A RID: 4154 RVA: 0x0002F78C File Offset: 0x0002D98C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x < rhs.x, lhs.y < rhs.y);
		}

		// Token: 0x0600103B RID: 4155 RVA: 0x0002F7AF File Offset: 0x0002D9AF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(float2 lhs, float rhs)
		{
			return new bool2(lhs.x < rhs, lhs.y < rhs);
		}

		// Token: 0x0600103C RID: 4156 RVA: 0x0002F7C8 File Offset: 0x0002D9C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <(float lhs, float2 rhs)
		{
			return new bool2(lhs < rhs.x, lhs < rhs.y);
		}

		// Token: 0x0600103D RID: 4157 RVA: 0x0002F7E1 File Offset: 0x0002D9E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x <= rhs.x, lhs.y <= rhs.y);
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0002F80A File Offset: 0x0002DA0A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(float2 lhs, float rhs)
		{
			return new bool2(lhs.x <= rhs, lhs.y <= rhs);
		}

		// Token: 0x0600103F RID: 4159 RVA: 0x0002F829 File Offset: 0x0002DA29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator <=(float lhs, float2 rhs)
		{
			return new bool2(lhs <= rhs.x, lhs <= rhs.y);
		}

		// Token: 0x06001040 RID: 4160 RVA: 0x0002F848 File Offset: 0x0002DA48
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x > rhs.x, lhs.y > rhs.y);
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0002F86B File Offset: 0x0002DA6B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(float2 lhs, float rhs)
		{
			return new bool2(lhs.x > rhs, lhs.y > rhs);
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0002F884 File Offset: 0x0002DA84
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >(float lhs, float2 rhs)
		{
			return new bool2(lhs > rhs.x, lhs > rhs.y);
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002F89D File Offset: 0x0002DA9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x >= rhs.x, lhs.y >= rhs.y);
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0002F8C6 File Offset: 0x0002DAC6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(float2 lhs, float rhs)
		{
			return new bool2(lhs.x >= rhs, lhs.y >= rhs);
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0002F8E5 File Offset: 0x0002DAE5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator >=(float lhs, float2 rhs)
		{
			return new bool2(lhs >= rhs.x, lhs >= rhs.y);
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002F904 File Offset: 0x0002DB04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator -(float2 val)
		{
			return new float2(-val.x, -val.y);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0002F919 File Offset: 0x0002DB19
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float2 operator +(float2 val)
		{
			return new float2(val.x, val.y);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0002F92C File Offset: 0x0002DB2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x == rhs.x, lhs.y == rhs.y);
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0002F94F File Offset: 0x0002DB4F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(float2 lhs, float rhs)
		{
			return new bool2(lhs.x == rhs, lhs.y == rhs);
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0002F968 File Offset: 0x0002DB68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator ==(float lhs, float2 rhs)
		{
			return new bool2(lhs == rhs.x, lhs == rhs.y);
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002F981 File Offset: 0x0002DB81
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(float2 lhs, float2 rhs)
		{
			return new bool2(lhs.x != rhs.x, lhs.y != rhs.y);
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0002F9AA File Offset: 0x0002DBAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(float2 lhs, float rhs)
		{
			return new bool2(lhs.x != rhs, lhs.y != rhs);
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002F9C9 File Offset: 0x0002DBC9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool2 operator !=(float lhs, float2 rhs)
		{
			return new bool2(lhs != rhs.x, lhs != rhs.y);
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x0600104E RID: 4174 RVA: 0x0002F9E8 File Offset: 0x0002DBE8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.x);
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x0600104F RID: 4175 RVA: 0x0002FA07 File Offset: 0x0002DC07
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.x, this.y);
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001050 RID: 4176 RVA: 0x0002FA26 File Offset: 0x0002DC26
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.x);
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001051 RID: 4177 RVA: 0x0002FA45 File Offset: 0x0002DC45
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.x, this.y, this.y);
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001052 RID: 4178 RVA: 0x0002FA64 File Offset: 0x0002DC64
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.x);
			}
		}

		// Token: 0x170003E0 RID: 992
		// (get) Token: 0x06001053 RID: 4179 RVA: 0x0002FA83 File Offset: 0x0002DC83
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.x, this.y);
			}
		}

		// Token: 0x170003E1 RID: 993
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x0002FAA2 File Offset: 0x0002DCA2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.x);
			}
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x0002FAC1 File Offset: 0x0002DCC1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 xyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.x, this.y, this.y, this.y);
			}
		}

		// Token: 0x170003E3 RID: 995
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x0002FAE0 File Offset: 0x0002DCE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.x);
			}
		}

		// Token: 0x170003E4 RID: 996
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x0002FAFF File Offset: 0x0002DCFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.x, this.y);
			}
		}

		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06001058 RID: 4184 RVA: 0x0002FB1E File Offset: 0x0002DD1E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.x);
			}
		}

		// Token: 0x170003E6 RID: 998
		// (get) Token: 0x06001059 RID: 4185 RVA: 0x0002FB3D File Offset: 0x0002DD3D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yxyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.x, this.y, this.y);
			}
		}

		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x0600105A RID: 4186 RVA: 0x0002FB5C File Offset: 0x0002DD5C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.x);
			}
		}

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x0600105B RID: 4187 RVA: 0x0002FB7B File Offset: 0x0002DD7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.x, this.y);
			}
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x0002FB9A File Offset: 0x0002DD9A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.x);
			}
		}

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x0600105D RID: 4189 RVA: 0x0002FBB9 File Offset: 0x0002DDB9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float4 yyyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float4(this.y, this.y, this.y, this.y);
			}
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x0002FBD8 File Offset: 0x0002DDD8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.x);
			}
		}

		// Token: 0x170003EC RID: 1004
		// (get) Token: 0x0600105F RID: 4191 RVA: 0x0002FBF1 File Offset: 0x0002DDF1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.x, this.y);
			}
		}

		// Token: 0x170003ED RID: 1005
		// (get) Token: 0x06001060 RID: 4192 RVA: 0x0002FC0A File Offset: 0x0002DE0A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.x);
			}
		}

		// Token: 0x170003EE RID: 1006
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x0002FC23 File Offset: 0x0002DE23
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 xyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.x, this.y, this.y);
			}
		}

		// Token: 0x170003EF RID: 1007
		// (get) Token: 0x06001062 RID: 4194 RVA: 0x0002FC3C File Offset: 0x0002DE3C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.x);
			}
		}

		// Token: 0x170003F0 RID: 1008
		// (get) Token: 0x06001063 RID: 4195 RVA: 0x0002FC55 File Offset: 0x0002DE55
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yxy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.x, this.y);
			}
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06001064 RID: 4196 RVA: 0x0002FC6E File Offset: 0x0002DE6E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.x);
			}
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06001065 RID: 4197 RVA: 0x0002FC87 File Offset: 0x0002DE87
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float3 yyy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float3(this.y, this.y, this.y);
			}
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06001066 RID: 4198 RVA: 0x0002FCA0 File Offset: 0x0002DEA0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.x);
			}
		}

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06001067 RID: 4199 RVA: 0x0002FCB3 File Offset: 0x0002DEB3
		// (set) Token: 0x06001068 RID: 4200 RVA: 0x0002FCC6 File Offset: 0x0002DEC6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 xy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.x, this.y);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.x = value.x;
				this.y = value.y;
			}
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001069 RID: 4201 RVA: 0x0002FCE0 File Offset: 0x0002DEE0
		// (set) Token: 0x0600106A RID: 4202 RVA: 0x0002FCF3 File Offset: 0x0002DEF3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yx
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.x);
			}
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			set
			{
				this.y = value.x;
				this.x = value.y;
			}
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x0002FD0D File Offset: 0x0002DF0D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public float2 yy
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new float2(this.y, this.y);
			}
		}

		// Token: 0x170003F7 RID: 1015
		public unsafe float this[int index]
		{
			get
			{
				fixed (float2* ptr = &this)
				{
					return ((float*)ptr)[index];
				}
			}
			set
			{
				fixed (float* ptr = &this.x)
				{
					ptr[index] = value;
				}
			}
		}

		// Token: 0x0600106E RID: 4206 RVA: 0x0002FD58 File Offset: 0x0002DF58
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public bool Equals(float2 rhs)
		{
			return this.x == rhs.x && this.y == rhs.y;
		}

		// Token: 0x0600106F RID: 4207 RVA: 0x0002FD78 File Offset: 0x0002DF78
		public override bool Equals(object o)
		{
			if (o is float2)
			{
				float2 rhs = (float2)o;
				return this.Equals(rhs);
			}
			return false;
		}

		// Token: 0x06001070 RID: 4208 RVA: 0x0002FD9D File Offset: 0x0002DF9D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode()
		{
			return (int)math.hash(this);
		}

		// Token: 0x06001071 RID: 4209 RVA: 0x0002FDAA File Offset: 0x0002DFAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override string ToString()
		{
			return string.Format("float2({0}f, {1}f)", this.x, this.y);
		}

		// Token: 0x06001072 RID: 4210 RVA: 0x0002FDCC File Offset: 0x0002DFCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public string ToString(string format, IFormatProvider formatProvider)
		{
			return string.Format("float2({0}f, {1}f)", this.x.ToString(format, formatProvider), this.y.ToString(format, formatProvider));
		}

		// Token: 0x06001073 RID: 4211 RVA: 0x0002FDF2 File Offset: 0x0002DFF2
		public static implicit operator Vector2(float2 v)
		{
			return new Vector2(v.x, v.y);
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x0002FE05 File Offset: 0x0002E005
		public static implicit operator float2(Vector2 v)
		{
			return new float2(v.x, v.y);
		}

		// Token: 0x0400006E RID: 110
		public float x;

		// Token: 0x0400006F RID: 111
		public float y;

		// Token: 0x04000070 RID: 112
		public static readonly float2 zero;

		// Token: 0x02000057 RID: 87
		internal sealed class DebuggerProxy
		{
			// Token: 0x0600246D RID: 9325 RVA: 0x0006758C File Offset: 0x0006578C
			public DebuggerProxy(float2 v)
			{
				this.x = v.x;
				this.y = v.y;
			}

			// Token: 0x04000145 RID: 325
			public float x;

			// Token: 0x04000146 RID: 326
			public float y;
		}
	}
}

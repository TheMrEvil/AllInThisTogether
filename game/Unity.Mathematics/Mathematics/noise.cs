using System;
using Unity.IL2CPP.CompilerServices;

namespace Unity.Mathematics
{
	// Token: 0x02000039 RID: 57
	[Il2CppEagerStaticClassConstruction]
	public static class noise
	{
		// Token: 0x06001DFE RID: 7678 RVA: 0x00051EA0 File Offset: 0x000500A0
		public static float2 cellular(float2 P)
		{
			float2 @float = noise.mod289(math.floor(P));
			float2 float2 = math.frac(P);
			float3 rhs = math.float3(-1f, 0f, 1f);
			float3 rhs2 = math.float3(-0.5f, 0.5f, 1.5f);
			float3 float3 = noise.permute(@float.x + rhs);
			float3 lhs = noise.permute(float3.x + @float.y + rhs);
			float3 rhs3 = math.frac(lhs * 0.14285715f) - 0.42857143f;
			float3 rhs4 = noise.mod7(math.floor(lhs * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 float4 = float2.x + 0.5f + 1f * rhs3;
			float3 float5 = float2.y - rhs2 + 1f * rhs4;
			float3 float6 = float4 * float4 + float5 * float5;
			float3 lhs2 = noise.permute(float3.y + @float.y + rhs);
			rhs3 = math.frac(lhs2 * 0.14285715f) - 0.42857143f;
			rhs4 = noise.mod7(math.floor(lhs2 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float4 = float2.x - 0.5f + 1f * rhs3;
			float5 = float2.y - rhs2 + 1f * rhs4;
			float3 float7 = float4 * float4 + float5 * float5;
			float3 lhs3 = noise.permute(float3.z + @float.y + rhs);
			rhs3 = math.frac(lhs3 * 0.14285715f) - 0.42857143f;
			rhs4 = noise.mod7(math.floor(lhs3 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float4 = float2.x - 1.5f + 1f * rhs3;
			float5 = float2.y - rhs2 + 1f * rhs4;
			float3 y = float4 * float4 + float5 * float5;
			float3 x = math.min(float6, float7);
			float7 = math.max(float6, float7);
			float7 = math.min(float7, y);
			float6 = math.min(x, float7);
			float7 = math.max(x, float7);
			float6.xy = ((float6.x < float6.y) ? float6.xy : float6.yx);
			float6.xz = ((float6.x < float6.z) ? float6.xz : float6.zx);
			float6.yz = math.min(float6.yz, float7.yz);
			float6.y = math.min(float6.y, float6.z);
			float6.y = math.min(float6.y, float7.x);
			return math.sqrt(float6.xy);
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x000521F4 File Offset: 0x000503F4
		public static float2 cellular2x2(float2 P)
		{
			float2 @float = noise.mod289(math.floor(P));
			float2 float2 = math.frac(P);
			float4 lhs = float2.x + math.float4(-0.5f, -1.5f, -0.5f, -1.5f);
			float4 lhs2 = float2.y + math.float4(-0.5f, -0.5f, -1.5f, -1.5f);
			float4 float3 = noise.permute(noise.permute(@float.x + math.float4(0f, 1f, 0f, 1f)) + @float.y + math.float4(0f, 0f, 1f, 1f));
			float4 rhs = noise.mod7(float3) * 0.14285715f + 0.071428575f;
			float4 rhs2 = noise.mod7(math.floor(float3 * 0.14285715f)) * 0.14285715f + 0.071428575f;
			float4 float4 = lhs + 0.8f * rhs;
			float4 float5 = lhs2 + 0.8f * rhs2;
			float4 float6 = float4 * float4 + float5 * float5;
			float6.xy = ((float6.x < float6.y) ? float6.xy : float6.yx);
			float6.xz = ((float6.x < float6.z) ? float6.xz : float6.zx);
			float6.xw = ((float6.x < float6.w) ? float6.xw : float6.wx);
			float6.y = math.min(float6.y, float6.z);
			float6.y = math.min(float6.y, float6.w);
			return math.sqrt(float6.xy);
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x000523E8 File Offset: 0x000505E8
		public static float2 cellular2x2x2(float3 P)
		{
			float3 @float = noise.mod289(math.floor(P));
			float3 float2 = math.frac(P);
			float4 lhs = float2.x + math.float4(0f, -1f, 0f, -1f);
			float4 lhs2 = float2.y + math.float4(0f, 0f, -1f, -1f);
			float4 lhs3 = noise.permute(noise.permute(@float.x + math.float4(0f, 1f, 0f, 1f)) + @float.y + math.float4(0f, 0f, 1f, 1f));
			float4 lhs4 = noise.permute(lhs3 + @float.z);
			float4 lhs5 = noise.permute(lhs3 + @float.z + math.float4(1f, 1f, 1f, 1f));
			float4 rhs = math.frac(lhs4 * 0.14285715f) - 0.42857143f;
			float4 rhs2 = noise.mod7(math.floor(lhs4 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float4 rhs3 = math.floor(lhs4 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float4 rhs4 = math.frac(lhs5 * 0.14285715f) - 0.42857143f;
			float4 rhs5 = noise.mod7(math.floor(lhs5 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float4 rhs6 = math.floor(lhs5 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float4 float3 = lhs + 0.8f * rhs;
			float4 float4 = lhs2 + 0.8f * rhs2;
			float4 float5 = float2.z + 0.8f * rhs3;
			float4 float6 = lhs + 0.8f * rhs4;
			float4 float7 = lhs2 + 0.8f * rhs5;
			float4 float8 = float2.z - 1f + 0.8f * rhs6;
			float4 x = float3 * float3 + float4 * float4 + float5 * float5;
			float4 float9 = float6 * float6 + float7 * float7 + float8 * float8;
			float4 float10 = math.min(x, float9);
			float9 = math.max(x, float9);
			float10.xy = ((float10.x < float10.y) ? float10.xy : float10.yx);
			float10.xz = ((float10.x < float10.z) ? float10.xz : float10.zx);
			float10.xw = ((float10.x < float10.w) ? float10.xw : float10.wx);
			float10.yzw = math.min(float10.yzw, float9.yzw);
			float10.y = math.min(float10.y, float10.z);
			float10.y = math.min(float10.y, float10.w);
			float10.y = math.min(float10.y, float9.x);
			return math.sqrt(float10.xy);
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x00052798 File Offset: 0x00050998
		public static float2 cellular(float3 P)
		{
			float3 @float = noise.mod289(math.floor(P));
			float3 float2 = math.frac(P) - 0.5f;
			float3 lhs = float2.x + math.float3(1f, 0f, -1f);
			float3 float3 = float2.y + math.float3(1f, 0f, -1f);
			float3 float4 = float2.z + math.float3(1f, 0f, -1f);
			float3 lhs2 = noise.permute(@float.x + math.float3(-1f, 0f, 1f));
			float3 lhs3 = noise.permute(lhs2 + @float.y - 1f);
			float3 lhs4 = noise.permute(lhs2 + @float.y);
			float3 lhs5 = noise.permute(lhs2 + @float.y + 1f);
			float3 lhs6 = noise.permute(lhs3 + @float.z - 1f);
			float3 lhs7 = noise.permute(lhs3 + @float.z);
			float3 lhs8 = noise.permute(lhs3 + @float.z + 1f);
			float3 lhs9 = noise.permute(lhs4 + @float.z - 1f);
			float3 lhs10 = noise.permute(lhs4 + @float.z);
			float3 lhs11 = noise.permute(lhs4 + @float.z + 1f);
			float3 lhs12 = noise.permute(lhs5 + @float.z - 1f);
			float3 lhs13 = noise.permute(lhs5 + @float.z);
			float3 lhs14 = noise.permute(lhs5 + @float.z + 1f);
			float3 rhs = math.frac(lhs6 * 0.14285715f) - 0.42857143f;
			float3 rhs2 = noise.mod7(math.floor(lhs6 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 rhs3 = math.floor(lhs6 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 rhs4 = math.frac(lhs7 * 0.14285715f) - 0.42857143f;
			float3 rhs5 = noise.mod7(math.floor(lhs7 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 rhs6 = math.floor(lhs7 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 rhs7 = math.frac(lhs8 * 0.14285715f) - 0.42857143f;
			float3 rhs8 = noise.mod7(math.floor(lhs8 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 rhs9 = math.floor(lhs8 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 rhs10 = math.frac(lhs9 * 0.14285715f) - 0.42857143f;
			float3 rhs11 = noise.mod7(math.floor(lhs9 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 rhs12 = math.floor(lhs9 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 rhs13 = math.frac(lhs10 * 0.14285715f) - 0.42857143f;
			float3 rhs14 = noise.mod7(math.floor(lhs10 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 rhs15 = math.floor(lhs10 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 rhs16 = math.frac(lhs11 * 0.14285715f) - 0.42857143f;
			float3 rhs17 = noise.mod7(math.floor(lhs11 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 rhs18 = math.floor(lhs11 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 rhs19 = math.frac(lhs12 * 0.14285715f) - 0.42857143f;
			float3 rhs20 = noise.mod7(math.floor(lhs12 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 rhs21 = math.floor(lhs12 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 rhs22 = math.frac(lhs13 * 0.14285715f) - 0.42857143f;
			float3 rhs23 = noise.mod7(math.floor(lhs13 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 rhs24 = math.floor(lhs13 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 rhs25 = math.frac(lhs14 * 0.14285715f) - 0.42857143f;
			float3 rhs26 = noise.mod7(math.floor(lhs14 * 0.14285715f)) * 0.14285715f - 0.42857143f;
			float3 rhs27 = math.floor(lhs14 * 0.020408163f) * 0.16666667f - 0.41666666f;
			float3 float5 = lhs + 1f * rhs;
			float3 float6 = float3.x + 1f * rhs2;
			float3 float7 = float4.x + 1f * rhs3;
			float3 float8 = lhs + 1f * rhs4;
			float3 float9 = float3.x + 1f * rhs5;
			float3 float10 = float4.y + 1f * rhs6;
			float3 float11 = lhs + 1f * rhs7;
			float3 float12 = float3.x + 1f * rhs8;
			float3 float13 = float4.z + 1f * rhs9;
			float3 float14 = lhs + 1f * rhs10;
			float3 float15 = float3.y + 1f * rhs11;
			float3 float16 = float4.x + 1f * rhs12;
			float3 float17 = lhs + 1f * rhs13;
			float3 float18 = float3.y + 1f * rhs14;
			float3 float19 = float4.y + 1f * rhs15;
			float3 float20 = lhs + 1f * rhs16;
			float3 float21 = float3.y + 1f * rhs17;
			float3 float22 = float4.z + 1f * rhs18;
			float3 float23 = lhs + 1f * rhs19;
			float3 float24 = float3.z + 1f * rhs20;
			float3 float25 = float4.x + 1f * rhs21;
			float3 float26 = lhs + 1f * rhs22;
			float3 float27 = float3.z + 1f * rhs23;
			float3 float28 = float4.y + 1f * rhs24;
			float3 float29 = lhs + 1f * rhs25;
			float3 float30 = float3.z + 1f * rhs26;
			float3 float31 = float4.z + 1f * rhs27;
			float3 float32 = float5 * float5 + float6 * float6 + float7 * float7;
			float3 float33 = float8 * float8 + float9 * float9 + float10 * float10;
			float3 y = float11 * float11 + float12 * float12 + float13 * float13;
			float3 float34 = float14 * float14 + float15 * float15 + float16 * float16;
			float3 float35 = float17 * float17 + float18 * float18 + float19 * float19;
			float3 y2 = float20 * float20 + float21 * float21 + float22 * float22;
			float3 float36 = float23 * float23 + float24 * float24 + float25 * float25;
			float3 float37 = float26 * float26 + float27 * float27 + float28 * float28;
			float3 y3 = float29 * float29 + float30 * float30 + float31 * float31;
			float3 x = math.min(float32, float33);
			float33 = math.max(float32, float33);
			float32 = math.min(x, y);
			y = math.max(x, y);
			float33 = math.min(float33, y);
			float3 x2 = math.min(float34, float35);
			float35 = math.max(float34, float35);
			float34 = math.min(x2, y2);
			y2 = math.max(x2, y2);
			float35 = math.min(float35, y2);
			float3 x3 = math.min(float36, float37);
			float37 = math.max(float36, float37);
			float36 = math.min(x3, y3);
			y3 = math.max(x3, y3);
			float37 = math.min(float37, y3);
			float3 x4 = math.min(float32, float34);
			float34 = math.max(float32, float34);
			float32 = math.min(x4, float36);
			float36 = math.max(x4, float36);
			float32.xy = ((float32.x < float32.y) ? float32.xy : float32.yx);
			float32.xz = ((float32.x < float32.z) ? float32.xz : float32.zx);
			float33 = math.min(float33, float34);
			float33 = math.min(float33, float35);
			float33 = math.min(float33, float36);
			float33 = math.min(float33, float37);
			float32.yz = math.min(float32.yz, float33.xy);
			float32.y = math.min(float32.y, float33.z);
			float32.y = math.min(float32.y, float32.z);
			return math.sqrt(float32.xy);
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x000532C8 File Offset: 0x000514C8
		public static float cnoise(float2 P)
		{
			float4 x = math.floor(P.xyxy) + math.float4(0f, 0f, 1f, 1f);
			float4 @float = math.frac(P.xyxy) - math.float4(0f, 0f, 1f, 1f);
			x = noise.mod289(x);
			float4 xzxz = x.xzxz;
			float4 yyww = x.yyww;
			float4 xzxz2 = @float.xzxz;
			float4 yyww2 = @float.yyww;
			float4 float2 = math.frac(noise.permute(noise.permute(xzxz) + yyww) * 0.024390243f) * 2f - 1f;
			float4 float3 = math.abs(float2) - 0.5f;
			float4 rhs = math.floor(float2 + 0.5f);
			float4 float4 = float2 - rhs;
			float2 float5 = math.float2(float4.x, float3.x);
			float2 float6 = math.float2(float4.y, float3.y);
			float2 float7 = math.float2(float4.z, float3.z);
			float2 float8 = math.float2(float4.w, float3.w);
			float4 float9 = noise.taylorInvSqrt(math.float4(math.dot(float5, float5), math.dot(float7, float7), math.dot(float6, float6), math.dot(float8, float8)));
			float5 *= float9.x;
			float7 *= float9.y;
			float6 *= float9.z;
			float8 *= float9.w;
			float x2 = math.dot(float5, math.float2(xzxz2.x, yyww2.x));
			float x3 = math.dot(float6, math.float2(xzxz2.y, yyww2.y));
			float y = math.dot(float7, math.float2(xzxz2.z, yyww2.z));
			float y2 = math.dot(float8, math.float2(xzxz2.w, yyww2.w));
			float2 float10 = noise.fade(@float.xy);
			float2 float11 = math.lerp(math.float2(x2, y), math.float2(x3, y2), float10.x);
			float num = math.lerp(float11.x, float11.y, float10.y);
			return 2.3f * num;
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x0005352C File Offset: 0x0005172C
		public static float pnoise(float2 P, float2 rep)
		{
			float4 x = math.floor(P.xyxy) + math.float4(0f, 0f, 1f, 1f);
			float4 @float = math.frac(P.xyxy) - math.float4(0f, 0f, 1f, 1f);
			x = math.fmod(x, rep.xyxy);
			x = noise.mod289(x);
			float4 xzxz = x.xzxz;
			float4 yyww = x.yyww;
			float4 xzxz2 = @float.xzxz;
			float4 yyww2 = @float.yyww;
			float4 float2 = math.frac(noise.permute(noise.permute(xzxz) + yyww) * 0.024390243f) * 2f - 1f;
			float4 float3 = math.abs(float2) - 0.5f;
			float4 rhs = math.floor(float2 + 0.5f);
			float4 float4 = float2 - rhs;
			float2 float5 = math.float2(float4.x, float3.x);
			float2 float6 = math.float2(float4.y, float3.y);
			float2 float7 = math.float2(float4.z, float3.z);
			float2 float8 = math.float2(float4.w, float3.w);
			float4 float9 = noise.taylorInvSqrt(math.float4(math.dot(float5, float5), math.dot(float7, float7), math.dot(float6, float6), math.dot(float8, float8)));
			float5 *= float9.x;
			float7 *= float9.y;
			float6 *= float9.z;
			float8 *= float9.w;
			float x2 = math.dot(float5, math.float2(xzxz2.x, yyww2.x));
			float x3 = math.dot(float6, math.float2(xzxz2.y, yyww2.y));
			float y = math.dot(float7, math.float2(xzxz2.z, yyww2.z));
			float y2 = math.dot(float8, math.float2(xzxz2.w, yyww2.w));
			float2 float10 = noise.fade(@float.xy);
			float2 float11 = math.lerp(math.float2(x2, y), math.float2(x3, y2), float10.x);
			float num = math.lerp(float11.x, float11.y, float10.y);
			return 2.3f * num;
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x0005379C File Offset: 0x0005199C
		public static float cnoise(float3 P)
		{
			float3 @float = math.floor(P);
			float3 float2 = @float + math.float3(1f);
			@float = noise.mod289(@float);
			float2 = noise.mod289(float2);
			float3 float3 = math.frac(P);
			float3 float4 = float3 - math.float3(1f);
			float4 x = math.float4(@float.x, float2.x, @float.x, float2.x);
			float4 rhs = math.float4(@float.yy, float2.yy);
			float4 zzzz = @float.zzzz;
			float4 zzzz2 = float2.zzzz;
			float4 lhs = noise.permute(noise.permute(x) + rhs);
			float4 lhs2 = noise.permute(lhs + zzzz);
			float4 lhs3 = noise.permute(lhs + zzzz2);
			float4 float5 = lhs2 * 0.14285715f;
			float4 float6 = math.frac(math.floor(float5) * 0.14285715f) - 0.5f;
			float5 = math.frac(float5);
			float4 float7 = math.float4(0.5f) - math.abs(float5) - math.abs(float6);
			float4 lhs4 = math.step(float7, math.float4(0f));
			float5 -= lhs4 * (math.step(0f, float5) - 0.5f);
			float6 -= lhs4 * (math.step(0f, float6) - 0.5f);
			float4 float8 = lhs3 * 0.14285715f;
			float4 float9 = math.frac(math.floor(float8) * 0.14285715f) - 0.5f;
			float8 = math.frac(float8);
			float4 float10 = math.float4(0.5f) - math.abs(float8) - math.abs(float9);
			float4 lhs5 = math.step(float10, math.float4(0f));
			float8 -= lhs5 * (math.step(0f, float8) - 0.5f);
			float9 -= lhs5 * (math.step(0f, float9) - 0.5f);
			float3 float11 = math.float3(float5.x, float6.x, float7.x);
			float3 float12 = math.float3(float5.y, float6.y, float7.y);
			float3 float13 = math.float3(float5.z, float6.z, float7.z);
			float3 float14 = math.float3(float5.w, float6.w, float7.w);
			float3 float15 = math.float3(float8.x, float9.x, float10.x);
			float3 float16 = math.float3(float8.y, float9.y, float10.y);
			float3 float17 = math.float3(float8.z, float9.z, float10.z);
			float3 float18 = math.float3(float8.w, float9.w, float10.w);
			float4 float19 = noise.taylorInvSqrt(math.float4(math.dot(float11, float11), math.dot(float13, float13), math.dot(float12, float12), math.dot(float14, float14)));
			float11 *= float19.x;
			float13 *= float19.y;
			float12 *= float19.z;
			float14 *= float19.w;
			float4 float20 = noise.taylorInvSqrt(math.float4(math.dot(float15, float15), math.dot(float17, float17), math.dot(float16, float16), math.dot(float18, float18)));
			float15 *= float20.x;
			float17 *= float20.y;
			float16 *= float20.z;
			float18 *= float20.w;
			float x2 = math.dot(float11, float3);
			float y = math.dot(float12, math.float3(float4.x, float3.yz));
			float z = math.dot(float13, math.float3(float3.x, float4.y, float3.z));
			float w = math.dot(float14, math.float3(float4.xy, float3.z));
			float x3 = math.dot(float15, math.float3(float3.xy, float4.z));
			float y2 = math.dot(float16, math.float3(float4.x, float3.y, float4.z));
			float z2 = math.dot(float17, math.float3(float3.x, float4.yz));
			float w2 = math.dot(float18, float4);
			float3 float21 = noise.fade(float3);
			float4 float22 = math.lerp(math.float4(x2, y, z, w), math.float4(x3, y2, z2, w2), float21.z);
			float2 float23 = math.lerp(float22.xy, float22.zw, float21.y);
			float num = math.lerp(float23.x, float23.y, float21.x);
			return 2.2f * num;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x00053CD4 File Offset: 0x00051ED4
		public static float pnoise(float3 P, float3 rep)
		{
			float3 @float = math.fmod(math.floor(P), rep);
			float3 float2 = math.fmod(@float + math.float3(1f), rep);
			@float = noise.mod289(@float);
			float2 = noise.mod289(float2);
			float3 float3 = math.frac(P);
			float3 float4 = float3 - math.float3(1f);
			float4 x = math.float4(@float.x, float2.x, @float.x, float2.x);
			float4 rhs = math.float4(@float.yy, float2.yy);
			float4 zzzz = @float.zzzz;
			float4 zzzz2 = float2.zzzz;
			float4 lhs = noise.permute(noise.permute(x) + rhs);
			float4 lhs2 = noise.permute(lhs + zzzz);
			float4 lhs3 = noise.permute(lhs + zzzz2);
			float4 float5 = lhs2 * 0.14285715f;
			float4 float6 = math.frac(math.floor(float5) * 0.14285715f) - 0.5f;
			float5 = math.frac(float5);
			float4 float7 = math.float4(0.5f) - math.abs(float5) - math.abs(float6);
			float4 lhs4 = math.step(float7, math.float4(0f));
			float5 -= lhs4 * (math.step(0f, float5) - 0.5f);
			float6 -= lhs4 * (math.step(0f, float6) - 0.5f);
			float4 float8 = lhs3 * 0.14285715f;
			float4 float9 = math.frac(math.floor(float8) * 0.14285715f) - 0.5f;
			float8 = math.frac(float8);
			float4 float10 = math.float4(0.5f) - math.abs(float8) - math.abs(float9);
			float4 lhs5 = math.step(float10, math.float4(0f));
			float8 -= lhs5 * (math.step(0f, float8) - 0.5f);
			float9 -= lhs5 * (math.step(0f, float9) - 0.5f);
			float3 float11 = math.float3(float5.x, float6.x, float7.x);
			float3 float12 = math.float3(float5.y, float6.y, float7.y);
			float3 float13 = math.float3(float5.z, float6.z, float7.z);
			float3 float14 = math.float3(float5.w, float6.w, float7.w);
			float3 float15 = math.float3(float8.x, float9.x, float10.x);
			float3 float16 = math.float3(float8.y, float9.y, float10.y);
			float3 float17 = math.float3(float8.z, float9.z, float10.z);
			float3 float18 = math.float3(float8.w, float9.w, float10.w);
			float4 float19 = noise.taylorInvSqrt(math.float4(math.dot(float11, float11), math.dot(float13, float13), math.dot(float12, float12), math.dot(float14, float14)));
			float11 *= float19.x;
			float13 *= float19.y;
			float12 *= float19.z;
			float14 *= float19.w;
			float4 float20 = noise.taylorInvSqrt(math.float4(math.dot(float15, float15), math.dot(float17, float17), math.dot(float16, float16), math.dot(float18, float18)));
			float15 *= float20.x;
			float17 *= float20.y;
			float16 *= float20.z;
			float18 *= float20.w;
			float x2 = math.dot(float11, float3);
			float y = math.dot(float12, math.float3(float4.x, float3.yz));
			float z = math.dot(float13, math.float3(float3.x, float4.y, float3.z));
			float w = math.dot(float14, math.float3(float4.xy, float3.z));
			float x3 = math.dot(float15, math.float3(float3.xy, float4.z));
			float y2 = math.dot(float16, math.float3(float4.x, float3.y, float4.z));
			float z2 = math.dot(float17, math.float3(float3.x, float4.yz));
			float w2 = math.dot(float18, float4);
			float3 float21 = noise.fade(float3);
			float4 float22 = math.lerp(math.float4(x2, y, z, w), math.float4(x3, y2, z2, w2), float21.z);
			float2 float23 = math.lerp(float22.xy, float22.zw, float21.y);
			float num = math.lerp(float23.x, float23.y, float21.x);
			return 2.2f * num;
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x00054218 File Offset: 0x00052418
		public static float cnoise(float4 P)
		{
			float4 @float = math.floor(P);
			float4 float2 = @float + 1f;
			@float = noise.mod289(@float);
			float2 = noise.mod289(float2);
			float4 float3 = math.frac(P);
			float4 float4 = float3 - 1f;
			float4 x = math.float4(@float.x, float2.x, @float.x, float2.x);
			float4 rhs = math.float4(@float.yy, float2.yy);
			float4 rhs2 = math.float4(@float.zzzz);
			float4 rhs3 = math.float4(float2.zzzz);
			float4 rhs4 = math.float4(@float.wwww);
			float4 rhs5 = math.float4(float2.wwww);
			float4 lhs = noise.permute(noise.permute(x) + rhs);
			float4 lhs2 = noise.permute(lhs + rhs2);
			float4 lhs3 = noise.permute(lhs + rhs3);
			float4 lhs4 = noise.permute(lhs2 + rhs4);
			float4 lhs5 = noise.permute(lhs2 + rhs5);
			float4 lhs6 = noise.permute(lhs3 + rhs4);
			float4 lhs7 = noise.permute(lhs3 + rhs5);
			float4 float5 = lhs4 * 0.14285715f;
			float4 float6 = math.floor(float5) * 0.14285715f;
			float4 float7 = math.floor(float6) * 0.16666667f;
			float5 = math.frac(float5) - 0.5f;
			float6 = math.frac(float6) - 0.5f;
			float7 = math.frac(float7) - 0.5f;
			float4 float8 = math.float4(0.75f) - math.abs(float5) - math.abs(float6) - math.abs(float7);
			float4 lhs8 = math.step(float8, math.float4(0f));
			float5 -= lhs8 * (math.step(0f, float5) - 0.5f);
			float6 -= lhs8 * (math.step(0f, float6) - 0.5f);
			float4 float9 = lhs5 * 0.14285715f;
			float4 float10 = math.floor(float9) * 0.14285715f;
			float4 float11 = math.floor(float10) * 0.16666667f;
			float9 = math.frac(float9) - 0.5f;
			float10 = math.frac(float10) - 0.5f;
			float11 = math.frac(float11) - 0.5f;
			float4 float12 = math.float4(0.75f) - math.abs(float9) - math.abs(float10) - math.abs(float11);
			float4 lhs9 = math.step(float12, math.float4(0f));
			float9 -= lhs9 * (math.step(0f, float9) - 0.5f);
			float10 -= lhs9 * (math.step(0f, float10) - 0.5f);
			float4 float13 = lhs6 * 0.14285715f;
			float4 float14 = math.floor(float13) * 0.14285715f;
			float4 float15 = math.floor(float14) * 0.16666667f;
			float13 = math.frac(float13) - 0.5f;
			float14 = math.frac(float14) - 0.5f;
			float15 = math.frac(float15) - 0.5f;
			float4 float16 = math.float4(0.75f) - math.abs(float13) - math.abs(float14) - math.abs(float15);
			float4 lhs10 = math.step(float16, math.float4(0f));
			float13 -= lhs10 * (math.step(0f, float13) - 0.5f);
			float14 -= lhs10 * (math.step(0f, float14) - 0.5f);
			float4 float17 = lhs7 * 0.14285715f;
			float4 float18 = math.floor(float17) * 0.14285715f;
			float4 float19 = math.floor(float18) * 0.16666667f;
			float17 = math.frac(float17) - 0.5f;
			float18 = math.frac(float18) - 0.5f;
			float19 = math.frac(float19) - 0.5f;
			float4 float20 = math.float4(0.75f) - math.abs(float17) - math.abs(float18) - math.abs(float19);
			float4 lhs11 = math.step(float20, math.float4(0f));
			float17 -= lhs11 * (math.step(0f, float17) - 0.5f);
			float18 -= lhs11 * (math.step(0f, float18) - 0.5f);
			float4 float21 = math.float4(float5.x, float6.x, float7.x, float8.x);
			float4 float22 = math.float4(float5.y, float6.y, float7.y, float8.y);
			float4 float23 = math.float4(float5.z, float6.z, float7.z, float8.z);
			float4 float24 = math.float4(float5.w, float6.w, float7.w, float8.w);
			float4 float25 = math.float4(float13.x, float14.x, float15.x, float16.x);
			float4 float26 = math.float4(float13.y, float14.y, float15.y, float16.y);
			float4 float27 = math.float4(float13.z, float14.z, float15.z, float16.z);
			float4 float28 = math.float4(float13.w, float14.w, float15.w, float16.w);
			float4 float29 = math.float4(float9.x, float10.x, float11.x, float12.x);
			float4 float30 = math.float4(float9.y, float10.y, float11.y, float12.y);
			float4 float31 = math.float4(float9.z, float10.z, float11.z, float12.z);
			float4 float32 = math.float4(float9.w, float10.w, float11.w, float12.w);
			float4 float33 = math.float4(float17.x, float18.x, float19.x, float20.x);
			float4 float34 = math.float4(float17.y, float18.y, float19.y, float20.y);
			float4 float35 = math.float4(float17.z, float18.z, float19.z, float20.z);
			float4 float36 = math.float4(float17.w, float18.w, float19.w, float20.w);
			float4 float37 = noise.taylorInvSqrt(math.float4(math.dot(float21, float21), math.dot(float23, float23), math.dot(float22, float22), math.dot(float24, float24)));
			float21 *= float37.x;
			float23 *= float37.y;
			float22 *= float37.z;
			float24 *= float37.w;
			float4 float38 = noise.taylorInvSqrt(math.float4(math.dot(float29, float29), math.dot(float31, float31), math.dot(float30, float30), math.dot(float32, float32)));
			float4 x2 = float29 * float38.x;
			float31 *= float38.y;
			float30 *= float38.z;
			float32 *= float38.w;
			float4 float39 = noise.taylorInvSqrt(math.float4(math.dot(float25, float25), math.dot(float27, float27), math.dot(float26, float26), math.dot(float28, float28)));
			float25 *= float39.x;
			float27 *= float39.y;
			float26 *= float39.z;
			float28 *= float39.w;
			float4 float40 = noise.taylorInvSqrt(math.float4(math.dot(float33, float33), math.dot(float35, float35), math.dot(float34, float34), math.dot(float36, float36)));
			float33 *= float40.x;
			float35 *= float40.y;
			float34 *= float40.z;
			float36 *= float40.w;
			float x3 = math.dot(float21, float3);
			float y = math.dot(float22, math.float4(float4.x, float3.yzw));
			float z = math.dot(float23, math.float4(float3.x, float4.y, float3.zw));
			float w = math.dot(float24, math.float4(float4.xy, float3.zw));
			float x4 = math.dot(float25, math.float4(float3.xy, float4.z, float3.w));
			float y2 = math.dot(float26, math.float4(float4.x, float3.y, float4.z, float3.w));
			float z2 = math.dot(float27, math.float4(float3.x, float4.yz, float3.w));
			float w2 = math.dot(float28, math.float4(float4.xyz, float3.w));
			float x5 = math.dot(x2, math.float4(float3.xyz, float4.w));
			float y3 = math.dot(float30, math.float4(float4.x, float3.yz, float4.w));
			float z3 = math.dot(float31, math.float4(float3.x, float4.y, float3.z, float4.w));
			float w3 = math.dot(float32, math.float4(float4.xy, float3.z, float4.w));
			float x6 = math.dot(float33, math.float4(float3.xy, float4.zw));
			float y4 = math.dot(float34, math.float4(float4.x, float3.y, float4.zw));
			float z4 = math.dot(float35, math.float4(float3.x, float4.yzw));
			float w4 = math.dot(float36, float4);
			float4 float41 = noise.fade(float3);
			float4 x7 = math.lerp(math.float4(x3, y, z, w), math.float4(x5, y3, z3, w3), float41.w);
			float4 y5 = math.lerp(math.float4(x4, y2, z2, w2), math.float4(x6, y4, z4, w4), float41.w);
			float4 float42 = math.lerp(x7, y5, float41.z);
			float2 float43 = math.lerp(float42.xy, float42.zw, float41.y);
			float num = math.lerp(float43.x, float43.y, float41.x);
			return 2.2f * num;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x00054D98 File Offset: 0x00052F98
		public static float pnoise(float4 P, float4 rep)
		{
			float4 @float = math.fmod(math.floor(P), rep);
			float4 float2 = math.fmod(@float + 1f, rep);
			@float = noise.mod289(@float);
			float2 = noise.mod289(float2);
			float4 float3 = math.frac(P);
			float4 float4 = float3 - 1f;
			float4 x = math.float4(@float.x, float2.x, @float.x, float2.x);
			float4 rhs = math.float4(@float.yy, float2.yy);
			float4 rhs2 = math.float4(@float.zzzz);
			float4 rhs3 = math.float4(float2.zzzz);
			float4 rhs4 = math.float4(@float.wwww);
			float4 rhs5 = math.float4(float2.wwww);
			float4 lhs = noise.permute(noise.permute(x) + rhs);
			float4 lhs2 = noise.permute(lhs + rhs2);
			float4 lhs3 = noise.permute(lhs + rhs3);
			float4 lhs4 = noise.permute(lhs2 + rhs4);
			float4 lhs5 = noise.permute(lhs2 + rhs5);
			float4 lhs6 = noise.permute(lhs3 + rhs4);
			float4 lhs7 = noise.permute(lhs3 + rhs5);
			float4 float5 = lhs4 * 0.14285715f;
			float4 float6 = math.floor(float5) * 0.14285715f;
			float4 float7 = math.floor(float6) * 0.16666667f;
			float5 = math.frac(float5) - 0.5f;
			float6 = math.frac(float6) - 0.5f;
			float7 = math.frac(float7) - 0.5f;
			float4 float8 = math.float4(0.75f) - math.abs(float5) - math.abs(float6) - math.abs(float7);
			float4 lhs8 = math.step(float8, math.float4(0f));
			float5 -= lhs8 * (math.step(0f, float5) - 0.5f);
			float6 -= lhs8 * (math.step(0f, float6) - 0.5f);
			float4 float9 = lhs5 * 0.14285715f;
			float4 float10 = math.floor(float9) * 0.14285715f;
			float4 float11 = math.floor(float10) * 0.16666667f;
			float9 = math.frac(float9) - 0.5f;
			float10 = math.frac(float10) - 0.5f;
			float11 = math.frac(float11) - 0.5f;
			float4 float12 = math.float4(0.75f) - math.abs(float9) - math.abs(float10) - math.abs(float11);
			float4 lhs9 = math.step(float12, math.float4(0f));
			float9 -= lhs9 * (math.step(0f, float9) - 0.5f);
			float10 -= lhs9 * (math.step(0f, float10) - 0.5f);
			float4 float13 = lhs6 * 0.14285715f;
			float4 float14 = math.floor(float13) * 0.14285715f;
			float4 float15 = math.floor(float14) * 0.16666667f;
			float13 = math.frac(float13) - 0.5f;
			float14 = math.frac(float14) - 0.5f;
			float15 = math.frac(float15) - 0.5f;
			float4 float16 = math.float4(0.75f) - math.abs(float13) - math.abs(float14) - math.abs(float15);
			float4 lhs10 = math.step(float16, math.float4(0f));
			float13 -= lhs10 * (math.step(0f, float13) - 0.5f);
			float14 -= lhs10 * (math.step(0f, float14) - 0.5f);
			float4 float17 = lhs7 * 0.14285715f;
			float4 float18 = math.floor(float17) * 0.14285715f;
			float4 float19 = math.floor(float18) * 0.16666667f;
			float17 = math.frac(float17) - 0.5f;
			float18 = math.frac(float18) - 0.5f;
			float19 = math.frac(float19) - 0.5f;
			float4 float20 = math.float4(0.75f) - math.abs(float17) - math.abs(float18) - math.abs(float19);
			float4 lhs11 = math.step(float20, math.float4(0f));
			float17 -= lhs11 * (math.step(0f, float17) - 0.5f);
			float18 -= lhs11 * (math.step(0f, float18) - 0.5f);
			float4 float21 = math.float4(float5.x, float6.x, float7.x, float8.x);
			float4 float22 = math.float4(float5.y, float6.y, float7.y, float8.y);
			float4 float23 = math.float4(float5.z, float6.z, float7.z, float8.z);
			float4 float24 = math.float4(float5.w, float6.w, float7.w, float8.w);
			float4 float25 = math.float4(float13.x, float14.x, float15.x, float16.x);
			float4 float26 = math.float4(float13.y, float14.y, float15.y, float16.y);
			float4 float27 = math.float4(float13.z, float14.z, float15.z, float16.z);
			float4 float28 = math.float4(float13.w, float14.w, float15.w, float16.w);
			float4 float29 = math.float4(float9.x, float10.x, float11.x, float12.x);
			float4 float30 = math.float4(float9.y, float10.y, float11.y, float12.y);
			float4 float31 = math.float4(float9.z, float10.z, float11.z, float12.z);
			float4 float32 = math.float4(float9.w, float10.w, float11.w, float12.w);
			float4 float33 = math.float4(float17.x, float18.x, float19.x, float20.x);
			float4 float34 = math.float4(float17.y, float18.y, float19.y, float20.y);
			float4 float35 = math.float4(float17.z, float18.z, float19.z, float20.z);
			float4 float36 = math.float4(float17.w, float18.w, float19.w, float20.w);
			float4 float37 = noise.taylorInvSqrt(math.float4(math.dot(float21, float21), math.dot(float23, float23), math.dot(float22, float22), math.dot(float24, float24)));
			float21 *= float37.x;
			float23 *= float37.y;
			float22 *= float37.z;
			float24 *= float37.w;
			float4 float38 = noise.taylorInvSqrt(math.float4(math.dot(float29, float29), math.dot(float31, float31), math.dot(float30, float30), math.dot(float32, float32)));
			float4 x2 = float29 * float38.x;
			float31 *= float38.y;
			float30 *= float38.z;
			float32 *= float38.w;
			float4 float39 = noise.taylorInvSqrt(math.float4(math.dot(float25, float25), math.dot(float27, float27), math.dot(float26, float26), math.dot(float28, float28)));
			float25 *= float39.x;
			float27 *= float39.y;
			float26 *= float39.z;
			float28 *= float39.w;
			float4 float40 = noise.taylorInvSqrt(math.float4(math.dot(float33, float33), math.dot(float35, float35), math.dot(float34, float34), math.dot(float36, float36)));
			float33 *= float40.x;
			float35 *= float40.y;
			float34 *= float40.z;
			float36 *= float40.w;
			float x3 = math.dot(float21, float3);
			float y = math.dot(float22, math.float4(float4.x, float3.yzw));
			float z = math.dot(float23, math.float4(float3.x, float4.y, float3.zw));
			float w = math.dot(float24, math.float4(float4.xy, float3.zw));
			float x4 = math.dot(float25, math.float4(float3.xy, float4.z, float3.w));
			float y2 = math.dot(float26, math.float4(float4.x, float3.y, float4.z, float3.w));
			float z2 = math.dot(float27, math.float4(float3.x, float4.yz, float3.w));
			float w2 = math.dot(float28, math.float4(float4.xyz, float3.w));
			float x5 = math.dot(x2, math.float4(float3.xyz, float4.w));
			float y3 = math.dot(float30, math.float4(float4.x, float3.yz, float4.w));
			float z3 = math.dot(float31, math.float4(float3.x, float4.y, float3.z, float4.w));
			float w3 = math.dot(float32, math.float4(float4.xy, float3.z, float4.w));
			float x6 = math.dot(float33, math.float4(float3.xy, float4.zw));
			float y4 = math.dot(float34, math.float4(float4.x, float3.y, float4.zw));
			float z4 = math.dot(float35, math.float4(float3.x, float4.yzw));
			float w4 = math.dot(float36, float4);
			float4 float41 = noise.fade(float3);
			float4 x7 = math.lerp(math.float4(x3, y, z, w), math.float4(x5, y3, z3, w3), float41.w);
			float4 y5 = math.lerp(math.float4(x4, y2, z2, w2), math.float4(x6, y4, z4, w4), float41.w);
			float4 float42 = math.lerp(x7, y5, float41.z);
			float2 float43 = math.lerp(float42.xy, float42.zw, float41.y);
			float num = math.lerp(float43.x, float43.y, float41.x);
			return 2.2f * num;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x00055922 File Offset: 0x00053B22
		private static float mod289(float x)
		{
			return x - math.floor(x * 0.0034602077f) * 289f;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x00055938 File Offset: 0x00053B38
		private static float2 mod289(float2 x)
		{
			return x - math.floor(x * 0.0034602077f) * 289f;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x0005595A File Offset: 0x00053B5A
		private static float3 mod289(float3 x)
		{
			return x - math.floor(x * 0.0034602077f) * 289f;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0005597C File Offset: 0x00053B7C
		private static float4 mod289(float4 x)
		{
			return x - math.floor(x * 0.0034602077f) * 289f;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x0005599E File Offset: 0x00053B9E
		private static float3 mod7(float3 x)
		{
			return x - math.floor(x * 0.14285715f) * 7f;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x000559C0 File Offset: 0x00053BC0
		private static float4 mod7(float4 x)
		{
			return x - math.floor(x * 0.14285715f) * 7f;
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x000559E2 File Offset: 0x00053BE2
		private static float permute(float x)
		{
			return noise.mod289((34f * x + 1f) * x);
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x000559F8 File Offset: 0x00053BF8
		private static float3 permute(float3 x)
		{
			return noise.mod289((34f * x + 1f) * x);
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x00055A1A File Offset: 0x00053C1A
		private static float4 permute(float4 x)
		{
			return noise.mod289((34f * x + 1f) * x);
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x00055A3C File Offset: 0x00053C3C
		private static float taylorInvSqrt(float r)
		{
			return 1.7928429f - 0.85373473f * r;
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x00055A4B File Offset: 0x00053C4B
		private static float4 taylorInvSqrt(float4 r)
		{
			return 1.7928429f - 0.85373473f * r;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x00055A62 File Offset: 0x00053C62
		private static float2 fade(float2 t)
		{
			return t * t * t * (t * (t * 6f - 15f) + 10f);
		}

		// Token: 0x06001E14 RID: 7700 RVA: 0x00055A9B File Offset: 0x00053C9B
		private static float3 fade(float3 t)
		{
			return t * t * t * (t * (t * 6f - 15f) + 10f);
		}

		// Token: 0x06001E15 RID: 7701 RVA: 0x00055AD4 File Offset: 0x00053CD4
		private static float4 fade(float4 t)
		{
			return t * t * t * (t * (t * 6f - 15f) + 10f);
		}

		// Token: 0x06001E16 RID: 7702 RVA: 0x00055B10 File Offset: 0x00053D10
		private static float4 grad4(float j, float4 ip)
		{
			float4 @float = math.float4(1f, 1f, 1f, -1f);
			float3 float2 = math.floor(math.frac(math.float3(j) * ip.xyz) * 7f) * ip.z - 1f;
			float w = 1.5f - math.dot(math.abs(float2), @float.xyz);
			float4 float3 = math.float4(float2, w);
			float4 float4 = math.float4(float3 < 0f);
			float3.xyz += (float4.xyz * 2f - 1f) * float4.www;
			return float3;
		}

		// Token: 0x06001E17 RID: 7703 RVA: 0x00055BE4 File Offset: 0x00053DE4
		private static float2 rgrad2(float2 p, float rot)
		{
			float x = noise.permute(noise.permute(p.x) + p.y) * 0.024390243f + rot;
			x = math.frac(x) * 6.2831855f;
			return math.float2(math.cos(x), math.sin(x));
		}

		// Token: 0x06001E18 RID: 7704 RVA: 0x00055C30 File Offset: 0x00053E30
		public static float snoise(float2 v)
		{
			float4 @float = math.float4(0.21132487f, 0.36602542f, -0.57735026f, 0.024390243f);
			float2 float2 = math.floor(v + math.dot(v, @float.yy));
			float2 float3 = v - float2 + math.dot(float2, @float.xx);
			float2 float4 = (float3.x > float3.y) ? math.float2(1f, 0f) : math.float2(0f, 1f);
			float4 float5 = float3.xyxy + @float.xxzz;
			float5.xy -= float4;
			float2 = noise.mod289(float2);
			float3 lhs = noise.permute(noise.permute(float2.y + math.float3(0f, float4.y, 1f)) + float2.x + math.float3(0f, float4.x, 1f));
			float3 float6 = math.max(0.5f - math.float3(math.dot(float3, float3), math.dot(float5.xy, float5.xy), math.dot(float5.zw, float5.zw)), 0f);
			float6 *= float6;
			float6 *= float6;
			float3 float7 = 2f * math.frac(lhs * @float.www) - 1f;
			float3 float8 = math.abs(float7) - 0.5f;
			float3 rhs = math.floor(float7 + 0.5f);
			float3 float9 = float7 - rhs;
			float6 *= 1.7928429f - 0.85373473f * (float9 * float9 + float8 * float8);
			float x = float9.x * float3.x + float8.x * float3.y;
			float2 yz = float9.yz * float5.xz + float8.yz * float5.yw;
			float3 y = math.float3(x, yz);
			return 130f * math.dot(float6, y);
		}

		// Token: 0x06001E19 RID: 7705 RVA: 0x00055E88 File Offset: 0x00054088
		public static float snoise(float3 v)
		{
			float2 @float = math.float2(0.16666667f, 0.33333334f);
			float4 float2 = math.float4(0f, 0.5f, 1f, 2f);
			float3 float3 = math.floor(v + math.dot(v, @float.yyy));
			float3 float4 = v - float3 + math.dot(float3, @float.xxx);
			float3 rhs = math.step(float4.yzx, float4.xyz);
			float3 float5 = 1f - rhs;
			float3 float6 = math.min(rhs.xyz, float5.zxy);
			float3 float7 = math.max(rhs.xyz, float5.zxy);
			float3 float8 = float4 - float6 + @float.xxx;
			float3 float9 = float4 - float7 + @float.yyy;
			float3 float10 = float4 - float2.yyy;
			float3 = noise.mod289(float3);
			float4 lhs = noise.permute(noise.permute(noise.permute(float3.z + math.float4(0f, float6.z, float7.z, 1f)) + float3.y + math.float4(0f, float6.y, float7.y, 1f)) + float3.x + math.float4(0f, float6.x, float7.x, 1f));
			float3 float11 = 0.14285715f * float2.wyz - float2.xzx;
			float4 lhs2 = lhs - 49f * math.floor(lhs * float11.z * float11.z);
			float4 float12 = math.floor(lhs2 * float11.z);
			float4 lhs3 = math.floor(lhs2 - 7f * float12);
			float4 x = float12 * float11.x + float11.yyyy;
			float4 x2 = lhs3 * float11.x + float11.yyyy;
			float4 float13 = 1f - math.abs(x) - math.abs(x2);
			float4 x3 = math.float4(x.xy, x2.xy);
			float4 x4 = math.float4(x.zw, x2.zw);
			float4 float14 = math.floor(x3) * 2f + 1f;
			float4 float15 = math.floor(x4) * 2f + 1f;
			float4 float16 = -math.step(float13, math.float4(0f));
			float4 float17 = x3.xzyw + float14.xzyw * float16.xxyy;
			float4 float18 = x4.xzyw + float15.xzyw * float16.zzww;
			float3 float19 = math.float3(float17.xy, float13.x);
			float3 float20 = math.float3(float17.zw, float13.y);
			float3 float21 = math.float3(float18.xy, float13.z);
			float3 float22 = math.float3(float18.zw, float13.w);
			float4 float23 = noise.taylorInvSqrt(math.float4(math.dot(float19, float19), math.dot(float20, float20), math.dot(float21, float21), math.dot(float22, float22)));
			float19 *= float23.x;
			float20 *= float23.y;
			float21 *= float23.z;
			float22 *= float23.w;
			float4 float24 = math.max(0.6f - math.float4(math.dot(float4, float4), math.dot(float8, float8), math.dot(float9, float9), math.dot(float10, float10)), 0f);
			float24 *= float24;
			return 42f * math.dot(float24 * float24, math.float4(math.dot(float19, float4), math.dot(float20, float8), math.dot(float21, float9), math.dot(float22, float10)));
		}

		// Token: 0x06001E1A RID: 7706 RVA: 0x000562F0 File Offset: 0x000544F0
		public static float snoise(float3 v, out float3 gradient)
		{
			float2 @float = math.float2(0.16666667f, 0.33333334f);
			float4 float2 = math.float4(0f, 0.5f, 1f, 2f);
			float3 float3 = math.floor(v + math.dot(v, @float.yyy));
			float3 float4 = v - float3 + math.dot(float3, @float.xxx);
			float3 rhs = math.step(float4.yzx, float4.xyz);
			float3 float5 = 1f - rhs;
			float3 float6 = math.min(rhs.xyz, float5.zxy);
			float3 float7 = math.max(rhs.xyz, float5.zxy);
			float3 float8 = float4 - float6 + @float.xxx;
			float3 float9 = float4 - float7 + @float.yyy;
			float3 float10 = float4 - float2.yyy;
			float3 = noise.mod289(float3);
			float4 lhs = noise.permute(noise.permute(noise.permute(float3.z + math.float4(0f, float6.z, float7.z, 1f)) + float3.y + math.float4(0f, float6.y, float7.y, 1f)) + float3.x + math.float4(0f, float6.x, float7.x, 1f));
			float3 float11 = 0.14285715f * float2.wyz - float2.xzx;
			float4 lhs2 = lhs - 49f * math.floor(lhs * float11.z * float11.z);
			float4 float12 = math.floor(lhs2 * float11.z);
			float4 lhs3 = math.floor(lhs2 - 7f * float12);
			float4 x = float12 * float11.x + float11.yyyy;
			float4 x2 = lhs3 * float11.x + float11.yyyy;
			float4 float13 = 1f - math.abs(x) - math.abs(x2);
			float4 x3 = math.float4(x.xy, x2.xy);
			float4 x4 = math.float4(x.zw, x2.zw);
			float4 float14 = math.floor(x3) * 2f + 1f;
			float4 float15 = math.floor(x4) * 2f + 1f;
			float4 float16 = -math.step(float13, math.float4(0f));
			float4 float17 = x3.xzyw + float14.xzyw * float16.xxyy;
			float4 float18 = x4.xzyw + float15.xzyw * float16.zzww;
			float3 float19 = math.float3(float17.xy, float13.x);
			float3 float20 = math.float3(float17.zw, float13.y);
			float3 float21 = math.float3(float18.xy, float13.z);
			float3 float22 = math.float3(float18.zw, float13.w);
			float4 float23 = noise.taylorInvSqrt(math.float4(math.dot(float19, float19), math.dot(float20, float20), math.dot(float21, float21), math.dot(float22, float22)));
			float19 *= float23.x;
			float20 *= float23.y;
			float21 *= float23.z;
			float22 *= float23.w;
			float4 float24 = math.max(0.6f - math.float4(math.dot(float4, float4), math.dot(float8, float8), math.dot(float9, float9), math.dot(float10, float10)), 0f);
			float4 float25 = float24 * float24;
			float4 float26 = float25 * float25;
			float4 float27 = math.float4(math.dot(float19, float4), math.dot(float20, float8), math.dot(float21, float9), math.dot(float22, float10));
			float4 float28 = float25 * float24 * float27;
			gradient = -8f * (float28.x * float4 + float28.y * float8 + float28.z * float9 + float28.w * float10);
			gradient += float26.x * float19 + float26.y * float20 + float26.z * float21 + float26.w * float22;
			gradient *= 42f;
			return 42f * math.dot(float26, float27);
		}

		// Token: 0x06001E1B RID: 7707 RVA: 0x00056830 File Offset: 0x00054A30
		public static float snoise(float4 v)
		{
			float4 @float = math.float4(0.1381966f, 0.2763932f, 0.4145898f, -0.4472136f);
			float4 float2 = math.floor(v + math.dot(v, math.float4(0.309017f)));
			float4 float3 = v - float2 + math.dot(float2, @float.xxxx);
			float4 float4 = math.float4(0f);
			float3 float5 = math.step(float3.yzw, float3.xxx);
			float3 float6 = math.step(float3.zww, float3.yyz);
			float4.x = float5.x + float5.y + float5.z;
			float4.yzw = 1f - float5;
			float4.y += float6.x + float6.y;
			float4.zw += 1f - float6.xy;
			float4.z += float6.z;
			float4.w += 1f - float6.z;
			float4 float7 = math.clamp(float4, 0f, 1f);
			float4 float8 = math.clamp(float4 - 1f, 0f, 1f);
			float4 float9 = math.clamp(float4 - 2f, 0f, 1f);
			float4 float10 = float3 - float9 + @float.xxxx;
			float4 float11 = float3 - float8 + @float.yyyy;
			float4 float12 = float3 - float7 + @float.zzzz;
			float4 float13 = float3 + @float.wwww;
			float2 = noise.mod289(float2);
			float j = noise.permute(noise.permute(noise.permute(noise.permute(float2.w) + float2.z) + float2.y) + float2.x);
			float4 float14 = noise.permute(noise.permute(noise.permute(noise.permute(float2.w + math.float4(float9.w, float8.w, float7.w, 1f)) + float2.z + math.float4(float9.z, float8.z, float7.z, 1f)) + float2.y + math.float4(float9.y, float8.y, float7.y, 1f)) + float2.x + math.float4(float9.x, float8.x, float7.x, 1f));
			float4 ip = math.float4(0.0034013605f, 0.020408163f, 0.14285715f, 0f);
			float4 float15 = noise.grad4(j, ip);
			float4 float16 = noise.grad4(float14.x, ip);
			float4 float17 = noise.grad4(float14.y, ip);
			float4 float18 = noise.grad4(float14.z, ip);
			float4 float19 = noise.grad4(float14.w, ip);
			float4 float20 = noise.taylorInvSqrt(math.float4(math.dot(float15, float15), math.dot(float16, float16), math.dot(float17, float17), math.dot(float18, float18)));
			float15 *= float20.x;
			float16 *= float20.y;
			float17 *= float20.z;
			float18 *= float20.w;
			float19 *= noise.taylorInvSqrt(math.dot(float19, float19));
			float3 float21 = math.max(0.6f - math.float3(math.dot(float3, float3), math.dot(float10, float10), math.dot(float11, float11)), 0f);
			float2 float22 = math.max(0.6f - math.float2(math.dot(float12, float12), math.dot(float13, float13)), 0f);
			float21 *= float21;
			float22 *= float22;
			return 49f * (math.dot(float21 * float21, math.float3(math.dot(float15, float3), math.dot(float16, float10), math.dot(float17, float11))) + math.dot(float22 * float22, math.float2(math.dot(float18, float12), math.dot(float19, float13))));
		}

		// Token: 0x06001E1C RID: 7708 RVA: 0x00056CD8 File Offset: 0x00054ED8
		public static float3 psrdnoise(float2 pos, float2 per, float rot)
		{
			pos.y += 0.01f;
			float2 x = math.float2(pos.x + pos.y * 0.5f, pos.y);
			float2 @float = math.floor(x);
			float2 float2 = math.frac(x);
			float2 float3 = (float2.x > float2.y) ? math.float2(1f, 0f) : math.float2(0f, 1f);
			float2 float4 = math.float2(@float.x - @float.y * 0.5f, @float.y);
			float2 float5 = math.float2(float4.x + float3.x - float3.y * 0.5f, float4.y + float3.y);
			float2 float6 = math.float2(float4.x + 0.5f, float4.y + 1f);
			float2 float7 = pos - float4;
			float2 float8 = pos - float5;
			float2 float9 = pos - float6;
			float3 lhs = math.fmod(math.float3(float4.x, float5.x, float6.x), per.x);
			float3 float10 = math.fmod(math.float3(float4.y, float5.y, float6.y), per.y);
			float3 float11 = lhs + 0.5f * float10;
			float3 float12 = float10;
			float2 float13 = noise.rgrad2(math.float2(float11.x, float12.x), rot);
			float2 float14 = noise.rgrad2(math.float2(float11.y, float12.y), rot);
			float2 float15 = noise.rgrad2(math.float2(float11.z, float12.z), rot);
			float3 float16 = math.float3(math.dot(float13, float7), math.dot(float14, float8), math.dot(float15, float9));
			float3 float17 = 0.8f - math.float3(math.dot(float7, float7), math.dot(float8, float8), math.dot(float9, float9));
			float3 float18 = -2f * math.float3(float7.x, float8.x, float9.x);
			float3 float19 = -2f * math.float3(float7.y, float8.y, float9.y);
			if (float17.x < 0f)
			{
				float18.x = 0f;
				float19.x = 0f;
				float17.x = 0f;
			}
			if (float17.y < 0f)
			{
				float18.y = 0f;
				float19.y = 0f;
				float17.y = 0f;
			}
			if (float17.z < 0f)
			{
				float18.z = 0f;
				float19.z = 0f;
				float17.z = 0f;
			}
			float3 float20 = float17 * float17;
			float3 float21 = float20 * float20;
			float3 float22 = float20 * float17;
			float x2 = math.dot(float21, float16);
			float2 lhs2 = math.float2(float18.x, float19.x) * 4f * float22.x;
			float2 lhs3 = float21.x * float13 + lhs2 * float16.x;
			float2 lhs4 = math.float2(float18.y, float19.y) * 4f * float22.y;
			float2 rhs = float21.y * float14 + lhs4 * float16.y;
			float2 lhs5 = math.float2(float18.z, float19.z) * 4f * float22.z;
			float2 rhs2 = float21.z * float15 + lhs5 * float16.z;
			return 11f * math.float3(x2, lhs3 + rhs + rhs2);
		}

		// Token: 0x06001E1D RID: 7709 RVA: 0x000570EF File Offset: 0x000552EF
		public static float3 psrdnoise(float2 pos, float2 per)
		{
			return noise.psrdnoise(pos, per, 0f);
		}

		// Token: 0x06001E1E RID: 7710 RVA: 0x00057100 File Offset: 0x00055300
		public static float psrnoise(float2 pos, float2 per, float rot)
		{
			pos.y += 0.001f;
			float2 x = math.float2(pos.x + pos.y * 0.5f, pos.y);
			float2 @float = math.floor(x);
			float2 float2 = math.frac(x);
			float2 float3 = (float2.x > float2.y) ? math.float2(1f, 0f) : math.float2(0f, 1f);
			float2 float4 = math.float2(@float.x - @float.y * 0.5f, @float.y);
			float2 float5 = math.float2(float4.x + float3.x - float3.y * 0.5f, float4.y + float3.y);
			float2 float6 = math.float2(float4.x + 0.5f, float4.y + 1f);
			float2 float7 = pos - float4;
			float2 float8 = pos - float5;
			float2 float9 = pos - float6;
			float3 lhs = math.fmod(math.float3(float4.x, float5.x, float6.x), per.x);
			float3 float10 = math.fmod(math.float3(float4.y, float5.y, float6.y), per.y);
			float3 float11 = lhs + 0.5f * float10;
			float3 float12 = float10;
			float2 x2 = noise.rgrad2(math.float2(float11.x, float12.x), rot);
			float2 x3 = noise.rgrad2(math.float2(float11.y, float12.y), rot);
			float2 x4 = noise.rgrad2(math.float2(float11.z, float12.z), rot);
			float3 y = math.float3(math.dot(x2, float7), math.dot(x3, float8), math.dot(x4, float9));
			float3 float13 = math.max(0.8f - math.float3(math.dot(float7, float7), math.dot(float8, float8), math.dot(float9, float9)), 0f);
			float3 float14 = float13 * float13;
			float num = math.dot(float14 * float14, y);
			return 11f * num;
		}

		// Token: 0x06001E1F RID: 7711 RVA: 0x0005732E File Offset: 0x0005552E
		public static float psrnoise(float2 pos, float2 per)
		{
			return noise.psrnoise(pos, per, 0f);
		}

		// Token: 0x06001E20 RID: 7712 RVA: 0x0005733C File Offset: 0x0005553C
		public static float3 srdnoise(float2 pos, float rot)
		{
			pos.y += 0.001f;
			float2 x = math.float2(pos.x + pos.y * 0.5f, pos.y);
			float2 @float = math.floor(x);
			float2 float2 = math.frac(x);
			float2 float3 = (float2.x > float2.y) ? math.float2(1f, 0f) : math.float2(0f, 1f);
			float2 float4 = math.float2(@float.x - @float.y * 0.5f, @float.y);
			float2 float5 = math.float2(float4.x + float3.x - float3.y * 0.5f, float4.y + float3.y);
			float2 float6 = math.float2(float4.x + 0.5f, float4.y + 1f);
			float2 float7 = pos - float4;
			float2 float8 = pos - float5;
			float2 float9 = pos - float6;
			float3 lhs = math.float3(float4.x, float5.x, float6.x);
			float3 float10 = math.float3(float4.y, float5.y, float6.y);
			float3 x2 = lhs + 0.5f * float10;
			float3 float11 = float10;
			float3 float12 = noise.mod289(x2);
			float11 = noise.mod289(float11);
			float2 float13 = noise.rgrad2(math.float2(float12.x, float11.x), rot);
			float2 float14 = noise.rgrad2(math.float2(float12.y, float11.y), rot);
			float2 float15 = noise.rgrad2(math.float2(float12.z, float11.z), rot);
			float3 float16 = math.float3(math.dot(float13, float7), math.dot(float14, float8), math.dot(float15, float9));
			float3 float17 = 0.8f - math.float3(math.dot(float7, float7), math.dot(float8, float8), math.dot(float9, float9));
			float3 float18 = -2f * math.float3(float7.x, float8.x, float9.x);
			float3 float19 = -2f * math.float3(float7.y, float8.y, float9.y);
			if (float17.x < 0f)
			{
				float18.x = 0f;
				float19.x = 0f;
				float17.x = 0f;
			}
			if (float17.y < 0f)
			{
				float18.y = 0f;
				float19.y = 0f;
				float17.y = 0f;
			}
			if (float17.z < 0f)
			{
				float18.z = 0f;
				float19.z = 0f;
				float17.z = 0f;
			}
			float3 float20 = float17 * float17;
			float3 float21 = float20 * float20;
			float3 float22 = float20 * float17;
			float x3 = math.dot(float21, float16);
			float2 lhs2 = math.float2(float18.x, float19.x) * 4f * float22.x;
			float2 lhs3 = float21.x * float13 + lhs2 * float16.x;
			float2 lhs4 = math.float2(float18.y, float19.y) * 4f * float22.y;
			float2 rhs = float21.y * float14 + lhs4 * float16.y;
			float2 lhs5 = math.float2(float18.z, float19.z) * 4f * float22.z;
			float2 rhs2 = float21.z * float15 + lhs5 * float16.z;
			return 11f * math.float3(x3, lhs3 + rhs + rhs2);
		}

		// Token: 0x06001E21 RID: 7713 RVA: 0x00057741 File Offset: 0x00055941
		public static float3 srdnoise(float2 pos)
		{
			return noise.srdnoise(pos, 0f);
		}

		// Token: 0x06001E22 RID: 7714 RVA: 0x00057750 File Offset: 0x00055950
		public static float srnoise(float2 pos, float rot)
		{
			pos.y += 0.001f;
			float2 x = math.float2(pos.x + pos.y * 0.5f, pos.y);
			float2 @float = math.floor(x);
			float2 float2 = math.frac(x);
			float2 float3 = (float2.x > float2.y) ? math.float2(1f, 0f) : math.float2(0f, 1f);
			float2 float4 = math.float2(@float.x - @float.y * 0.5f, @float.y);
			float2 float5 = math.float2(float4.x + float3.x - float3.y * 0.5f, float4.y + float3.y);
			float2 float6 = math.float2(float4.x + 0.5f, float4.y + 1f);
			float2 float7 = pos - float4;
			float2 float8 = pos - float5;
			float2 float9 = pos - float6;
			float3 lhs = math.float3(float4.x, float5.x, float6.x);
			float3 float10 = math.float3(float4.y, float5.y, float6.y);
			float3 x2 = lhs + 0.5f * float10;
			float3 float11 = float10;
			float3 float12 = noise.mod289(x2);
			float11 = noise.mod289(float11);
			float2 x3 = noise.rgrad2(math.float2(float12.x, float11.x), rot);
			float2 x4 = noise.rgrad2(math.float2(float12.y, float11.y), rot);
			float2 x5 = noise.rgrad2(math.float2(float12.z, float11.z), rot);
			float3 y = math.float3(math.dot(x3, float7), math.dot(x4, float8), math.dot(x5, float9));
			float3 float13 = math.max(0.8f - math.float3(math.dot(float7, float7), math.dot(float8, float8), math.dot(float9, float9)), 0f);
			float3 float14 = float13 * float13;
			float num = math.dot(float14 * float14, y);
			return 11f * num;
		}

		// Token: 0x06001E23 RID: 7715 RVA: 0x0005796C File Offset: 0x00055B6C
		public static float srnoise(float2 pos)
		{
			return noise.srnoise(pos, 0f);
		}
	}
}

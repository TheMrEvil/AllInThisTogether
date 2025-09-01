using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;

namespace MagicaCloth2
{
	// Token: 0x020000DE RID: 222
	public static class MathExtensions
	{
		// Token: 0x06000363 RID: 867 RVA: 0x0001EC4C File Offset: 0x0001CE4C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float GetValue(this float4x4 m, int index)
		{
			index = math.clamp(index, 0, 15);
			float4x4 float4x = m;
			return float4x[index / 4][index % 4];
		}

		// Token: 0x06000364 RID: 868 RVA: 0x0001EC7D File Offset: 0x0001CE7D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetValue(this float4x4 m, int index, float value)
		{
			index = math.clamp(index, 0, 15);
			m[index / 4][index % 4] = value;
		}

		// Token: 0x06000365 RID: 869 RVA: 0x0001EC9C File Offset: 0x0001CE9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float EvaluateCurveClamp01(this float4x4 m, float time)
		{
			return math.saturate(DataUtility.EvaluateCurve(m, time));
		}

		// Token: 0x06000366 RID: 870 RVA: 0x0001ECAA File Offset: 0x0001CEAA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float EvaluateCurve(this float4x4 m, float time)
		{
			return DataUtility.EvaluateCurve(m, time);
		}
	}
}

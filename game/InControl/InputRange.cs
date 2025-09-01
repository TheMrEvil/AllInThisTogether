using System;
using UnityEngine;

namespace InControl
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public struct InputRange
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00005B89 File Offset: 0x00003D89
		private InputRange(float value0, float value1, InputRangeType type)
		{
			this.Value0 = value0;
			this.Value1 = value1;
			this.Type = type;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public InputRange(InputRangeType type)
		{
			this.Value0 = InputRange.typeToRange[(int)type].Value0;
			this.Value1 = InputRange.typeToRange[(int)type].Value1;
			this.Type = type;
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00005BD5 File Offset: 0x00003DD5
		public bool Includes(float value)
		{
			return !this.Excludes(value);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00005BE1 File Offset: 0x00003DE1
		private bool Excludes(float value)
		{
			return this.Type == InputRangeType.None || value < Mathf.Min(this.Value0, this.Value1) || value > Mathf.Max(this.Value0, this.Value1);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00005C17 File Offset: 0x00003E17
		public static bool Excludes(InputRangeType rangeType, float value)
		{
			return InputRange.typeToRange[(int)rangeType].Excludes(value);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00005C2C File Offset: 0x00003E2C
		private static float Remap(float value, InputRange sourceRange, InputRange targetRange)
		{
			if (sourceRange.Excludes(value))
			{
				return 0f;
			}
			float t = Mathf.InverseLerp(sourceRange.Value0, sourceRange.Value1, value);
			return Mathf.Lerp(targetRange.Value0, targetRange.Value1, t);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00005C70 File Offset: 0x00003E70
		public static float Remap(float value, InputRangeType sourceRangeType, InputRangeType targetRangeType)
		{
			InputRange sourceRange = InputRange.typeToRange[(int)sourceRangeType];
			InputRange targetRange = InputRange.typeToRange[(int)targetRangeType];
			return InputRange.Remap(value, sourceRange, targetRange);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00005CA0 File Offset: 0x00003EA0
		// Note: this type is marked as 'beforefieldinit'.
		static InputRange()
		{
		}

		// Token: 0x040001F6 RID: 502
		public static readonly InputRange None = new InputRange(0f, 0f, InputRangeType.None);

		// Token: 0x040001F7 RID: 503
		public static readonly InputRange MinusOneToOne = new InputRange(-1f, 1f, InputRangeType.MinusOneToOne);

		// Token: 0x040001F8 RID: 504
		public static readonly InputRange OneToMinusOne = new InputRange(1f, -1f, InputRangeType.OneToMinusOne);

		// Token: 0x040001F9 RID: 505
		public static readonly InputRange ZeroToOne = new InputRange(0f, 1f, InputRangeType.ZeroToOne);

		// Token: 0x040001FA RID: 506
		public static readonly InputRange ZeroToMinusOne = new InputRange(0f, -1f, InputRangeType.ZeroToMinusOne);

		// Token: 0x040001FB RID: 507
		public static readonly InputRange OneToZero = new InputRange(1f, 0f, InputRangeType.OneToZero);

		// Token: 0x040001FC RID: 508
		public static readonly InputRange MinusOneToZero = new InputRange(-1f, 0f, InputRangeType.MinusOneToZero);

		// Token: 0x040001FD RID: 509
		private static readonly InputRange[] typeToRange = new InputRange[]
		{
			InputRange.None,
			InputRange.MinusOneToOne,
			InputRange.OneToMinusOne,
			InputRange.ZeroToOne,
			InputRange.ZeroToMinusOne,
			InputRange.OneToZero,
			InputRange.MinusOneToZero
		};

		// Token: 0x040001FE RID: 510
		public readonly float Value0;

		// Token: 0x040001FF RID: 511
		public readonly float Value1;

		// Token: 0x04000200 RID: 512
		public readonly InputRangeType Type;
	}
}

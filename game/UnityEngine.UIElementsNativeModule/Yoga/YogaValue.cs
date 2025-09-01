using System;

namespace UnityEngine.Yoga
{
	// Token: 0x0200001E RID: 30
	internal struct YogaValue
	{
		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000039E0 File Offset: 0x00001BE0
		public YogaUnit Unit
		{
			get
			{
				return this.unit;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x0600014A RID: 330 RVA: 0x000039F8 File Offset: 0x00001BF8
		public float Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00003A10 File Offset: 0x00001C10
		public static YogaValue Point(float value)
		{
			return new YogaValue
			{
				value = value,
				unit = (YogaConstants.IsUndefined(value) ? YogaUnit.Undefined : YogaUnit.Point)
			};
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00003A48 File Offset: 0x00001C48
		public bool Equals(YogaValue other)
		{
			return this.Unit == other.Unit && (this.Value.Equals(other.Value) || this.Unit == YogaUnit.Undefined);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x00003A90 File Offset: 0x00001C90
		public override bool Equals(object obj)
		{
			bool flag = obj == null;
			return !flag && obj is YogaValue && this.Equals((YogaValue)obj);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00003AC8 File Offset: 0x00001CC8
		public override int GetHashCode()
		{
			return this.Value.GetHashCode() * 397 ^ (int)this.Unit;
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00003AF8 File Offset: 0x00001CF8
		public static YogaValue Undefined()
		{
			return new YogaValue
			{
				value = float.NaN,
				unit = YogaUnit.Undefined
			};
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00003B28 File Offset: 0x00001D28
		public static YogaValue Auto()
		{
			return new YogaValue
			{
				value = float.NaN,
				unit = YogaUnit.Auto
			};
		}

		// Token: 0x06000151 RID: 337 RVA: 0x00003B58 File Offset: 0x00001D58
		public static YogaValue Percent(float value)
		{
			return new YogaValue
			{
				value = value,
				unit = (YogaConstants.IsUndefined(value) ? YogaUnit.Undefined : YogaUnit.Percent)
			};
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00003B90 File Offset: 0x00001D90
		public static implicit operator YogaValue(float pointValue)
		{
			return YogaValue.Point(pointValue);
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00003BA8 File Offset: 0x00001DA8
		internal static YogaValue MarshalValue(YogaValue value)
		{
			return value;
		}

		// Token: 0x04000057 RID: 87
		private float value;

		// Token: 0x04000058 RID: 88
		private YogaUnit unit;
	}
}

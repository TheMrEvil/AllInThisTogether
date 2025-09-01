using System;
using System.Globalization;

namespace UnityEngine.UIElements
{
	// Token: 0x0200027C RID: 636
	public struct Length : IEquatable<Length>
	{
		// Token: 0x06001492 RID: 5266 RVA: 0x0005A0D8 File Offset: 0x000582D8
		public static Length Percent(float value)
		{
			return new Length(value, LengthUnit.Percent);
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0005A0F4 File Offset: 0x000582F4
		internal static Length Auto()
		{
			return new Length(0f, Length.Unit.Auto);
		}

		// Token: 0x06001494 RID: 5268 RVA: 0x0005A114 File Offset: 0x00058314
		internal static Length None()
		{
			return new Length(0f, Length.Unit.None);
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06001495 RID: 5269 RVA: 0x0005A131 File Offset: 0x00058331
		// (set) Token: 0x06001496 RID: 5270 RVA: 0x0005A139 File Offset: 0x00058339
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = Mathf.Clamp(value, -8388608f, 8388608f);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x0005A151 File Offset: 0x00058351
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x0005A159 File Offset: 0x00058359
		public LengthUnit unit
		{
			get
			{
				return (LengthUnit)this.m_Unit;
			}
			set
			{
				this.m_Unit = (Length.Unit)value;
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x0005A162 File Offset: 0x00058362
		internal bool IsAuto()
		{
			return this.m_Unit == Length.Unit.Auto;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x0005A16D File Offset: 0x0005836D
		internal bool IsNone()
		{
			return this.m_Unit == Length.Unit.None;
		}

		// Token: 0x0600149B RID: 5275 RVA: 0x0005A178 File Offset: 0x00058378
		public Length(float value)
		{
			this = new Length(value, Length.Unit.Pixel);
		}

		// Token: 0x0600149C RID: 5276 RVA: 0x0005A184 File Offset: 0x00058384
		public Length(float value, LengthUnit unit)
		{
			this = new Length(value, (Length.Unit)unit);
		}

		// Token: 0x0600149D RID: 5277 RVA: 0x0005A190 File Offset: 0x00058390
		private Length(float value, Length.Unit unit)
		{
			this = default(Length);
			this.value = value;
			this.m_Unit = unit;
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x0005A1AC File Offset: 0x000583AC
		public static implicit operator Length(float value)
		{
			return new Length(value, LengthUnit.Pixel);
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x0005A1C8 File Offset: 0x000583C8
		public static bool operator ==(Length lhs, Length rhs)
		{
			return lhs.m_Value == rhs.m_Value && lhs.m_Unit == rhs.m_Unit;
		}

		// Token: 0x060014A0 RID: 5280 RVA: 0x0005A1FC File Offset: 0x000583FC
		public static bool operator !=(Length lhs, Length rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x060014A1 RID: 5281 RVA: 0x0005A218 File Offset: 0x00058418
		public bool Equals(Length other)
		{
			return other == this;
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x0005A238 File Offset: 0x00058438
		public override bool Equals(object obj)
		{
			bool result;
			if (obj is Length)
			{
				Length other = (Length)obj;
				result = this.Equals(other);
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x0005A264 File Offset: 0x00058464
		public override int GetHashCode()
		{
			return this.m_Value.GetHashCode() * 397 ^ (int)this.m_Unit;
		}

		// Token: 0x060014A4 RID: 5284 RVA: 0x0005A290 File Offset: 0x00058490
		public override string ToString()
		{
			string str = this.value.ToString(CultureInfo.InvariantCulture.NumberFormat);
			string str2 = string.Empty;
			switch (this.m_Unit)
			{
			case Length.Unit.Pixel:
			{
				bool flag = !Mathf.Approximately(0f, this.value);
				if (flag)
				{
					str2 = "px";
				}
				break;
			}
			case Length.Unit.Percent:
				str2 = "%";
				break;
			case Length.Unit.Auto:
				str = "auto";
				break;
			case Length.Unit.None:
				str = "none";
				break;
			}
			return str + str2;
		}

		// Token: 0x04000930 RID: 2352
		private const float k_MaxValue = 8388608f;

		// Token: 0x04000931 RID: 2353
		private float m_Value;

		// Token: 0x04000932 RID: 2354
		private Length.Unit m_Unit;

		// Token: 0x0200027D RID: 637
		private enum Unit
		{
			// Token: 0x04000934 RID: 2356
			Pixel,
			// Token: 0x04000935 RID: 2357
			Percent,
			// Token: 0x04000936 RID: 2358
			Auto,
			// Token: 0x04000937 RID: 2359
			None
		}
	}
}

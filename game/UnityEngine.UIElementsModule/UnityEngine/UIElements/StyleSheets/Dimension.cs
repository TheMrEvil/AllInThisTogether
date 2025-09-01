using System;
using System.Globalization;

namespace UnityEngine.UIElements.StyleSheets
{
	// Token: 0x0200035E RID: 862
	[Serializable]
	internal struct Dimension : IEquatable<Dimension>
	{
		// Token: 0x06001BEC RID: 7148 RVA: 0x00082BA5 File Offset: 0x00080DA5
		public Dimension(float value, Dimension.Unit unit)
		{
			this.unit = unit;
			this.value = value;
		}

		// Token: 0x06001BED RID: 7149 RVA: 0x00082BB8 File Offset: 0x00080DB8
		public Length ToLength()
		{
			LengthUnit lengthUnit = (this.unit == Dimension.Unit.Percent) ? LengthUnit.Percent : LengthUnit.Pixel;
			return new Length(this.value, lengthUnit);
		}

		// Token: 0x06001BEE RID: 7150 RVA: 0x00082BE4 File Offset: 0x00080DE4
		public TimeValue ToTime()
		{
			TimeUnit timeUnit = (this.unit == Dimension.Unit.Millisecond) ? TimeUnit.Millisecond : TimeUnit.Second;
			return new TimeValue(this.value, timeUnit);
		}

		// Token: 0x06001BEF RID: 7151 RVA: 0x00082C10 File Offset: 0x00080E10
		public Angle ToAngle()
		{
			Angle result;
			switch (this.unit)
			{
			case Dimension.Unit.Degree:
				result = new Angle(this.value, AngleUnit.Degree);
				break;
			case Dimension.Unit.Gradian:
				result = new Angle(this.value, AngleUnit.Gradian);
				break;
			case Dimension.Unit.Radian:
				result = new Angle(this.value, AngleUnit.Radian);
				break;
			case Dimension.Unit.Turn:
				result = new Angle(this.value, AngleUnit.Turn);
				break;
			default:
				result = new Angle(this.value, AngleUnit.Degree);
				break;
			}
			return result;
		}

		// Token: 0x06001BF0 RID: 7152 RVA: 0x00082C90 File Offset: 0x00080E90
		public static bool operator ==(Dimension lhs, Dimension rhs)
		{
			return lhs.value == rhs.value && lhs.unit == rhs.unit;
		}

		// Token: 0x06001BF1 RID: 7153 RVA: 0x00082CC4 File Offset: 0x00080EC4
		public static bool operator !=(Dimension lhs, Dimension rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x06001BF2 RID: 7154 RVA: 0x00082CE0 File Offset: 0x00080EE0
		public bool Equals(Dimension other)
		{
			return other == this;
		}

		// Token: 0x06001BF3 RID: 7155 RVA: 0x00082D00 File Offset: 0x00080F00
		public override bool Equals(object obj)
		{
			bool flag = !(obj is Dimension);
			bool result;
			if (flag)
			{
				result = false;
			}
			else
			{
				Dimension lhs = (Dimension)obj;
				result = (lhs == this);
			}
			return result;
		}

		// Token: 0x06001BF4 RID: 7156 RVA: 0x00082D3C File Offset: 0x00080F3C
		public override int GetHashCode()
		{
			int num = -799583767;
			num = num * -1521134295 + this.unit.GetHashCode();
			return num * -1521134295 + this.value.GetHashCode();
		}

		// Token: 0x06001BF5 RID: 7157 RVA: 0x00082D84 File Offset: 0x00080F84
		public override string ToString()
		{
			string str = string.Empty;
			switch (this.unit)
			{
			case Dimension.Unit.Unitless:
				str = string.Empty;
				break;
			case Dimension.Unit.Pixel:
				str = "px";
				break;
			case Dimension.Unit.Percent:
				str = "%";
				break;
			case Dimension.Unit.Second:
				str = "s";
				break;
			case Dimension.Unit.Millisecond:
				str = "ms";
				break;
			case Dimension.Unit.Degree:
				str = "deg";
				break;
			case Dimension.Unit.Gradian:
				str = "grad";
				break;
			case Dimension.Unit.Radian:
				str = "rad";
				break;
			case Dimension.Unit.Turn:
				str = "turn";
				break;
			}
			return this.value.ToString(CultureInfo.InvariantCulture.NumberFormat) + str;
		}

		// Token: 0x04000DE2 RID: 3554
		public Dimension.Unit unit;

		// Token: 0x04000DE3 RID: 3555
		public float value;

		// Token: 0x0200035F RID: 863
		public enum Unit
		{
			// Token: 0x04000DE5 RID: 3557
			Unitless,
			// Token: 0x04000DE6 RID: 3558
			Pixel,
			// Token: 0x04000DE7 RID: 3559
			Percent,
			// Token: 0x04000DE8 RID: 3560
			Second,
			// Token: 0x04000DE9 RID: 3561
			Millisecond,
			// Token: 0x04000DEA RID: 3562
			Degree,
			// Token: 0x04000DEB RID: 3563
			Gradian,
			// Token: 0x04000DEC RID: 3564
			Radian,
			// Token: 0x04000DED RID: 3565
			Turn
		}
	}
}

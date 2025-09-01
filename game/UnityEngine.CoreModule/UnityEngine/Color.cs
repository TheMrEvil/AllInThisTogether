using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001BD RID: 445
	[RequiredByNativeCode(Optional = true, GenerateProxy = true)]
	[NativeHeader("Runtime/Math/Color.h")]
	[NativeClass("ColorRGBAf")]
	public struct Color : IEquatable<Color>, IFormattable
	{
		// Token: 0x0600137E RID: 4990 RVA: 0x0001B47B File Offset: 0x0001967B
		public Color(float r, float g, float b, float a)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x0001B49B File Offset: 0x0001969B
		public Color(float r, float g, float b)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = 1f;
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x0001B4C0 File Offset: 0x000196C0
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x0001B4DC File Offset: 0x000196DC
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x0001B4F8 File Offset: 0x000196F8
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = string.IsNullOrEmpty(format);
			if (flag)
			{
				format = "F3";
			}
			bool flag2 = formatProvider == null;
			if (flag2)
			{
				formatProvider = CultureInfo.InvariantCulture.NumberFormat;
			}
			return UnityString.Format("RGBA({0}, {1}, {2}, {3})", new object[]
			{
				this.r.ToString(format, formatProvider),
				this.g.ToString(format, formatProvider),
				this.b.ToString(format, formatProvider),
				this.a.ToString(format, formatProvider)
			});
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x0001B580 File Offset: 0x00019780
		public override int GetHashCode()
		{
			return this.GetHashCode();
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x0001B5AC File Offset: 0x000197AC
		public override bool Equals(object other)
		{
			bool flag = !(other is Color);
			return !flag && this.Equals((Color)other);
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x0001B5E0 File Offset: 0x000197E0
		public bool Equals(Color other)
		{
			return this.r.Equals(other.r) && this.g.Equals(other.g) && this.b.Equals(other.b) && this.a.Equals(other.a);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x0001B640 File Offset: 0x00019840
		public static Color operator +(Color a, Color b)
		{
			return new Color(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x0001B68C File Offset: 0x0001988C
		public static Color operator -(Color a, Color b)
		{
			return new Color(a.r - b.r, a.g - b.g, a.b - b.b, a.a - b.a);
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x0001B6D8 File Offset: 0x000198D8
		public static Color operator *(Color a, Color b)
		{
			return new Color(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0001B724 File Offset: 0x00019924
		public static Color operator *(Color a, float b)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x0001B75C File Offset: 0x0001995C
		public static Color operator *(float b, Color a)
		{
			return new Color(a.r * b, a.g * b, a.b * b, a.a * b);
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x0001B794 File Offset: 0x00019994
		public static Color operator /(Color a, float b)
		{
			return new Color(a.r / b, a.g / b, a.b / b, a.a / b);
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x0001B7CC File Offset: 0x000199CC
		public static bool operator ==(Color lhs, Color rhs)
		{
			return lhs == rhs;
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x0001B7F0 File Offset: 0x000199F0
		public static bool operator !=(Color lhs, Color rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x0001B80C File Offset: 0x00019A0C
		public static Color Lerp(Color a, Color b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x0001B884 File Offset: 0x00019A84
		public static Color LerpUnclamped(Color a, Color b, float t)
		{
			return new Color(a.r + (b.r - a.r) * t, a.g + (b.g - a.g) * t, a.b + (b.b - a.b) * t, a.a + (b.a - a.a) * t);
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x0001B8F4 File Offset: 0x00019AF4
		internal Color RGBMultiplied(float multiplier)
		{
			return new Color(this.r * multiplier, this.g * multiplier, this.b * multiplier, this.a);
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x0001B92C File Offset: 0x00019B2C
		internal Color AlphaMultiplied(float multiplier)
		{
			return new Color(this.r, this.g, this.b, this.a * multiplier);
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x0001B960 File Offset: 0x00019B60
		internal Color RGBMultiplied(Color multiplier)
		{
			return new Color(this.r * multiplier.r, this.g * multiplier.g, this.b * multiplier.b, this.a);
		}

		// Token: 0x170003F6 RID: 1014
		// (get) Token: 0x06001393 RID: 5011 RVA: 0x0001B9A4 File Offset: 0x00019BA4
		public static Color red
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 0f, 0f, 1f);
			}
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06001394 RID: 5012 RVA: 0x0001B9D0 File Offset: 0x00019BD0
		public static Color green
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 1f, 0f, 1f);
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06001395 RID: 5013 RVA: 0x0001B9FC File Offset: 0x00019BFC
		public static Color blue
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 0f, 1f, 1f);
			}
		}

		// Token: 0x170003F9 RID: 1017
		// (get) Token: 0x06001396 RID: 5014 RVA: 0x0001BA28 File Offset: 0x00019C28
		public static Color white
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 1f, 1f, 1f);
			}
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06001397 RID: 5015 RVA: 0x0001BA54 File Offset: 0x00019C54
		public static Color black
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 0f, 0f, 1f);
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06001398 RID: 5016 RVA: 0x0001BA80 File Offset: 0x00019C80
		public static Color yellow
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 0.92156863f, 0.015686275f, 1f);
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06001399 RID: 5017 RVA: 0x0001BAAC File Offset: 0x00019CAC
		public static Color cyan
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 1f, 1f, 1f);
			}
		}

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x0600139A RID: 5018 RVA: 0x0001BAD8 File Offset: 0x00019CD8
		public static Color magenta
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(1f, 0f, 1f, 1f);
			}
		}

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x0600139B RID: 5019 RVA: 0x0001BB04 File Offset: 0x00019D04
		public static Color gray
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x0600139C RID: 5020 RVA: 0x0001BB30 File Offset: 0x00019D30
		public static Color grey
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0.5f, 0.5f, 0.5f, 1f);
			}
		}

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x0600139D RID: 5021 RVA: 0x0001BB5C File Offset: 0x00019D5C
		public static Color clear
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Color(0f, 0f, 0f, 0f);
			}
		}

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x0600139E RID: 5022 RVA: 0x0001BB88 File Offset: 0x00019D88
		public float grayscale
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return 0.299f * this.r + 0.587f * this.g + 0.114f * this.b;
			}
		}

		// Token: 0x17000402 RID: 1026
		// (get) Token: 0x0600139F RID: 5023 RVA: 0x0001BBC0 File Offset: 0x00019DC0
		public Color linear
		{
			get
			{
				return new Color(Mathf.GammaToLinearSpace(this.r), Mathf.GammaToLinearSpace(this.g), Mathf.GammaToLinearSpace(this.b), this.a);
			}
		}

		// Token: 0x17000403 RID: 1027
		// (get) Token: 0x060013A0 RID: 5024 RVA: 0x0001BC00 File Offset: 0x00019E00
		public Color gamma
		{
			get
			{
				return new Color(Mathf.LinearToGammaSpace(this.r), Mathf.LinearToGammaSpace(this.g), Mathf.LinearToGammaSpace(this.b), this.a);
			}
		}

		// Token: 0x17000404 RID: 1028
		// (get) Token: 0x060013A1 RID: 5025 RVA: 0x0001BC40 File Offset: 0x00019E40
		public float maxColorComponent
		{
			get
			{
				return Mathf.Max(Mathf.Max(this.r, this.g), this.b);
			}
		}

		// Token: 0x060013A2 RID: 5026 RVA: 0x0001BC70 File Offset: 0x00019E70
		public static implicit operator Vector4(Color c)
		{
			return new Vector4(c.r, c.g, c.b, c.a);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0001BCA0 File Offset: 0x00019EA0
		public static implicit operator Color(Vector4 v)
		{
			return new Color(v.x, v.y, v.z, v.w);
		}

		// Token: 0x17000405 RID: 1029
		public float this[int index]
		{
			get
			{
				float result;
				switch (index)
				{
				case 0:
					result = this.r;
					break;
				case 1:
					result = this.g;
					break;
				case 2:
					result = this.b;
					break;
				case 3:
					result = this.a;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color index(" + index.ToString() + ")!");
				}
				return result;
			}
			set
			{
				switch (index)
				{
				case 0:
					this.r = value;
					break;
				case 1:
					this.g = value;
					break;
				case 2:
					this.b = value;
					break;
				case 3:
					this.a = value;
					break;
				default:
					throw new IndexOutOfRangeException("Invalid Color index(" + index.ToString() + ")!");
				}
			}
		}

		// Token: 0x060013A6 RID: 5030 RVA: 0x0001BDA8 File Offset: 0x00019FA8
		public static void RGBToHSV(Color rgbColor, out float H, out float S, out float V)
		{
			bool flag = rgbColor.b > rgbColor.g && rgbColor.b > rgbColor.r;
			if (flag)
			{
				Color.RGBToHSVHelper(4f, rgbColor.b, rgbColor.r, rgbColor.g, out H, out S, out V);
			}
			else
			{
				bool flag2 = rgbColor.g > rgbColor.r;
				if (flag2)
				{
					Color.RGBToHSVHelper(2f, rgbColor.g, rgbColor.b, rgbColor.r, out H, out S, out V);
				}
				else
				{
					Color.RGBToHSVHelper(0f, rgbColor.r, rgbColor.g, rgbColor.b, out H, out S, out V);
				}
			}
		}

		// Token: 0x060013A7 RID: 5031 RVA: 0x0001BE50 File Offset: 0x0001A050
		private static void RGBToHSVHelper(float offset, float dominantcolor, float colorone, float colortwo, out float H, out float S, out float V)
		{
			V = dominantcolor;
			bool flag = V != 0f;
			if (flag)
			{
				bool flag2 = colorone > colortwo;
				float num;
				if (flag2)
				{
					num = colortwo;
				}
				else
				{
					num = colorone;
				}
				float num2 = V - num;
				bool flag3 = num2 != 0f;
				if (flag3)
				{
					S = num2 / V;
					H = offset + (colorone - colortwo) / num2;
				}
				else
				{
					S = 0f;
					H = offset + (colorone - colortwo);
				}
				H /= 6f;
				bool flag4 = H < 0f;
				if (flag4)
				{
					H += 1f;
				}
			}
			else
			{
				S = 0f;
				H = 0f;
			}
		}

		// Token: 0x060013A8 RID: 5032 RVA: 0x0001BF04 File Offset: 0x0001A104
		public static Color HSVToRGB(float H, float S, float V)
		{
			return Color.HSVToRGB(H, S, V, true);
		}

		// Token: 0x060013A9 RID: 5033 RVA: 0x0001BF20 File Offset: 0x0001A120
		public static Color HSVToRGB(float H, float S, float V, bool hdr)
		{
			Color white = Color.white;
			bool flag = S == 0f;
			if (flag)
			{
				white.r = V;
				white.g = V;
				white.b = V;
			}
			else
			{
				bool flag2 = V == 0f;
				if (flag2)
				{
					white.r = 0f;
					white.g = 0f;
					white.b = 0f;
				}
				else
				{
					white.r = 0f;
					white.g = 0f;
					white.b = 0f;
					float num = H * 6f;
					int num2 = (int)Mathf.Floor(num);
					float num3 = num - (float)num2;
					float num4 = V * (1f - S);
					float num5 = V * (1f - S * num3);
					float num6 = V * (1f - S * (1f - num3));
					switch (num2)
					{
					case -1:
						white.r = V;
						white.g = num4;
						white.b = num5;
						break;
					case 0:
						white.r = V;
						white.g = num6;
						white.b = num4;
						break;
					case 1:
						white.r = num5;
						white.g = V;
						white.b = num4;
						break;
					case 2:
						white.r = num4;
						white.g = V;
						white.b = num6;
						break;
					case 3:
						white.r = num4;
						white.g = num5;
						white.b = V;
						break;
					case 4:
						white.r = num6;
						white.g = num4;
						white.b = V;
						break;
					case 5:
						white.r = V;
						white.g = num4;
						white.b = num5;
						break;
					case 6:
						white.r = V;
						white.g = num6;
						white.b = num4;
						break;
					}
					bool flag3 = !hdr;
					if (flag3)
					{
						white.r = Mathf.Clamp(white.r, 0f, 1f);
						white.g = Mathf.Clamp(white.g, 0f, 1f);
						white.b = Mathf.Clamp(white.b, 0f, 1f);
					}
				}
			}
			return white;
		}

		// Token: 0x0400073B RID: 1851
		public float r;

		// Token: 0x0400073C RID: 1852
		public float g;

		// Token: 0x0400073D RID: 1853
		public float b;

		// Token: 0x0400073E RID: 1854
		public float a;
	}
}

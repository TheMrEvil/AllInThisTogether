using System;
using System.Globalization;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001BE RID: 446
	[UsedByNativeCode]
	[StructLayout(LayoutKind.Explicit)]
	public struct Color32 : IFormattable
	{
		// Token: 0x060013AA RID: 5034 RVA: 0x0001C19A File Offset: 0x0001A39A
		public Color32(byte r, byte g, byte b, byte a)
		{
			this.rgba = 0;
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		// Token: 0x060013AB RID: 5035 RVA: 0x0001C1C4 File Offset: 0x0001A3C4
		public static implicit operator Color32(Color c)
		{
			return new Color32((byte)Mathf.Round(Mathf.Clamp01(c.r) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.g) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.b) * 255f), (byte)Mathf.Round(Mathf.Clamp01(c.a) * 255f));
		}

		// Token: 0x060013AC RID: 5036 RVA: 0x0001C238 File Offset: 0x0001A438
		public static implicit operator Color(Color32 c)
		{
			return new Color((float)c.r / 255f, (float)c.g / 255f, (float)c.b / 255f, (float)c.a / 255f);
		}

		// Token: 0x060013AD RID: 5037 RVA: 0x0001C284 File Offset: 0x0001A484
		public static Color32 Lerp(Color32 a, Color32 b, float t)
		{
			t = Mathf.Clamp01(t);
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x060013AE RID: 5038 RVA: 0x0001C308 File Offset: 0x0001A508
		public static Color32 LerpUnclamped(Color32 a, Color32 b, float t)
		{
			return new Color32((byte)((float)a.r + (float)(b.r - a.r) * t), (byte)((float)a.g + (float)(b.g - a.g) * t), (byte)((float)a.b + (float)(b.b - a.b) * t), (byte)((float)a.a + (float)(b.a - a.a) * t));
		}

		// Token: 0x17000406 RID: 1030
		public byte this[int index]
		{
			get
			{
				byte result;
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
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index.ToString() + ")!");
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
					throw new IndexOutOfRangeException("Invalid Color32 index(" + index.ToString() + ")!");
				}
			}
		}

		// Token: 0x060013B1 RID: 5041 RVA: 0x0001C45C File Offset: 0x0001A65C
		[VisibleToOtherModules]
		internal bool InternalEquals(Color32 other)
		{
			return this.rgba == other.rgba;
		}

		// Token: 0x060013B2 RID: 5042 RVA: 0x0001C47C File Offset: 0x0001A67C
		public override string ToString()
		{
			return this.ToString(null, null);
		}

		// Token: 0x060013B3 RID: 5043 RVA: 0x0001C498 File Offset: 0x0001A698
		public string ToString(string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060013B4 RID: 5044 RVA: 0x0001C4B4 File Offset: 0x0001A6B4
		public string ToString(string format, IFormatProvider formatProvider)
		{
			bool flag = formatProvider == null;
			if (flag)
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

		// Token: 0x0400073F RID: 1855
		[Ignore(DoesNotContributeToSize = true)]
		[FieldOffset(0)]
		private int rgba;

		// Token: 0x04000740 RID: 1856
		[FieldOffset(0)]
		public byte r;

		// Token: 0x04000741 RID: 1857
		[FieldOffset(1)]
		public byte g;

		// Token: 0x04000742 RID: 1858
		[FieldOffset(2)]
		public byte b;

		// Token: 0x04000743 RID: 1859
		[FieldOffset(3)]
		public byte a;
	}
}

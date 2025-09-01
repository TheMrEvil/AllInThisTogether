using System;
using System.Collections.Generic;

namespace UnityEngine.ProBuilder
{
	// Token: 0x02000013 RID: 19
	internal static class ColorUtility
	{
		// Token: 0x06000092 RID: 146 RVA: 0x000046B5 File Offset: 0x000028B5
		private static bool approx(float lhs, float rhs)
		{
			return Mathf.Abs(lhs - rhs) < Mathf.Epsilon;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000046C6 File Offset: 0x000028C6
		public static Color GetColor(Vector3 vec)
		{
			vec.Normalize();
			return new Color(Mathf.Abs(vec.x), Mathf.Abs(vec.y), Mathf.Abs(vec.z), 1f);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000046FA File Offset: 0x000028FA
		public static XYZColor RGBToXYZ(Color col)
		{
			return ColorUtility.RGBToXYZ(col.r, col.g, col.b);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00004714 File Offset: 0x00002914
		public static XYZColor RGBToXYZ(float r, float g, float b)
		{
			if (r > 0.04045f)
			{
				r = Mathf.Pow((r + 0.055f) / 1.055f, 2.4f);
			}
			else
			{
				r /= 12.92f;
			}
			if (g > 0.04045f)
			{
				g = Mathf.Pow((g + 0.055f) / 1.055f, 2.4f);
			}
			else
			{
				g /= 12.92f;
			}
			if (b > 0.04045f)
			{
				b = Mathf.Pow((b + 0.055f) / 1.055f, 2.4f);
			}
			else
			{
				b /= 12.92f;
			}
			r *= 100f;
			g *= 100f;
			b *= 100f;
			float x = r * 0.4124f + g * 0.3576f + b * 0.1805f;
			float y = r * 0.2126f + g * 0.7152f + b * 0.0722f;
			float z = r * 0.0193f + g * 0.1192f + b * 0.9505f;
			return new XYZColor(x, y, z);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x00004810 File Offset: 0x00002A10
		public static CIELabColor XYZToCIE_Lab(XYZColor xyz)
		{
			float num = xyz.x / 95.047f;
			float num2 = xyz.y / 100f;
			float num3 = xyz.z / 108.883f;
			if (num > 0.008856f)
			{
				num = Mathf.Pow(num, 0.33333334f);
			}
			else
			{
				num = 7.787f * num + 0.13793103f;
			}
			if (num2 > 0.008856f)
			{
				num2 = Mathf.Pow(num2, 0.33333334f);
			}
			else
			{
				num2 = 7.787f * num2 + 0.13793103f;
			}
			if (num3 > 0.008856f)
			{
				num3 = Mathf.Pow(num3, 0.33333334f);
			}
			else
			{
				num3 = 7.787f * num3 + 0.13793103f;
			}
			float l = 116f * num2 - 16f;
			float a = 500f * (num - num2);
			float b = 200f * (num2 - num3);
			return new CIELabColor(l, a, b);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000048DC File Offset: 0x00002ADC
		public static float DeltaE(CIELabColor lhs, CIELabColor rhs)
		{
			return Mathf.Sqrt(Mathf.Pow(lhs.L - rhs.L, 2f) + Mathf.Pow(lhs.a - rhs.a, 2f) + Mathf.Pow(lhs.b - rhs.b, 2f));
		}

		// Token: 0x06000098 RID: 152 RVA: 0x00004935 File Offset: 0x00002B35
		public static Color HSVtoRGB(HSVColor hsv)
		{
			return ColorUtility.HSVtoRGB(hsv.h, hsv.s, hsv.v);
		}

		// Token: 0x06000099 RID: 153 RVA: 0x00004950 File Offset: 0x00002B50
		public static Color HSVtoRGB(float h, float s, float v)
		{
			if (s == 0f)
			{
				return new Color(v, v, v, 1f);
			}
			h /= 60f;
			int num = (int)Mathf.Floor(h);
			float num2 = h - (float)num;
			float num3 = v * (1f - s);
			float num4 = v * (1f - s * num2);
			float num5 = v * (1f - s * (1f - num2));
			float r;
			float g;
			float b;
			switch (num)
			{
			case 0:
				r = v;
				g = num5;
				b = num3;
				break;
			case 1:
				r = num4;
				g = v;
				b = num3;
				break;
			case 2:
				r = num3;
				g = v;
				b = num5;
				break;
			case 3:
				r = num3;
				g = num4;
				b = v;
				break;
			case 4:
				r = num5;
				g = num3;
				b = v;
				break;
			default:
				r = v;
				g = num3;
				b = num4;
				break;
			}
			return new Color(r, g, b, 1f);
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004A1C File Offset: 0x00002C1C
		public static HSVColor RGBtoHSV(Color color)
		{
			float r = color.r;
			float b = color.b;
			float g = color.g;
			float num = Mathf.Min(Mathf.Min(r, g), b);
			float num2 = Mathf.Max(Mathf.Max(r, g), b);
			float v = num2;
			float num3 = num2 - num;
			float s;
			float num4;
			if (num2 != 0f)
			{
				s = num3 / num2;
				if (ColorUtility.approx(r, num2))
				{
					num4 = (g - b) / num3;
					if (float.IsNaN(num4))
					{
						num4 = 0f;
					}
				}
				else if (ColorUtility.approx(g, num2))
				{
					num4 = 2f + (b - r) / num3;
				}
				else
				{
					num4 = 4f + (r - g) / num3;
				}
				num4 *= 60f;
				if (num4 < 0f)
				{
					num4 += 360f;
				}
				return new HSVColor(num4, s, v);
			}
			s = 0f;
			num4 = 0f;
			return new HSVColor(num4, s, v);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004B00 File Offset: 0x00002D00
		public static string GetColorName(Color InColor)
		{
			CIELabColor lhs = CIELabColor.FromRGB(InColor);
			string result = "Unknown";
			float num = float.PositiveInfinity;
			foreach (KeyValuePair<string, CIELabColor> keyValuePair in ColorUtility.ColorNameLookup)
			{
				float num2 = Mathf.Abs(ColorUtility.DeltaE(lhs, keyValuePair.Value));
				if (num2 < num)
				{
					num = num2;
					result = keyValuePair.Key;
				}
			}
			return result;
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004B84 File Offset: 0x00002D84
		private static CIELabColor CIELabFromRGB(float R, float G, float B, float Scale)
		{
			float num = 1f / Scale;
			return CIELabColor.FromXYZ(XYZColor.FromRGB(R * num, G * num, B * num));
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004BAC File Offset: 0x00002DAC
		// Note: this type is marked as 'beforefieldinit'.
		static ColorUtility()
		{
		}

		// Token: 0x0400004A RID: 74
		private static readonly Dictionary<string, CIELabColor> ColorNameLookup = new Dictionary<string, CIELabColor>
		{
			{
				"Acid Green",
				ColorUtility.CIELabFromRGB(69f, 75f, 10f, 100f)
			},
			{
				"Aero",
				ColorUtility.CIELabFromRGB(49f, 73f, 91f, 100f)
			},
			{
				"Aero Blue",
				ColorUtility.CIELabFromRGB(79f, 100f, 90f, 100f)
			},
			{
				"African Violet",
				ColorUtility.CIELabFromRGB(70f, 52f, 75f, 100f)
			},
			{
				"Air Force Blue (RAF)",
				ColorUtility.CIELabFromRGB(36f, 54f, 66f, 100f)
			},
			{
				"Air Force Blue (USAF)",
				ColorUtility.CIELabFromRGB(0f, 19f, 56f, 100f)
			},
			{
				"Air Superiority Blue",
				ColorUtility.CIELabFromRGB(45f, 63f, 76f, 100f)
			},
			{
				"Alabama Crimson",
				ColorUtility.CIELabFromRGB(69f, 0f, 16f, 100f)
			},
			{
				"Alice Blue",
				ColorUtility.CIELabFromRGB(94f, 97f, 100f, 100f)
			},
			{
				"Alizarin Crimson",
				ColorUtility.CIELabFromRGB(89f, 15f, 21f, 100f)
			},
			{
				"Alloy Orange",
				ColorUtility.CIELabFromRGB(77f, 38f, 6f, 100f)
			},
			{
				"Almond",
				ColorUtility.CIELabFromRGB(94f, 87f, 80f, 100f)
			},
			{
				"Amaranth",
				ColorUtility.CIELabFromRGB(90f, 17f, 31f, 100f)
			},
			{
				"Amaranth Deep Purple",
				ColorUtility.CIELabFromRGB(67f, 15f, 31f, 100f)
			},
			{
				"Amaranth Pink",
				ColorUtility.CIELabFromRGB(95f, 61f, 73f, 100f)
			},
			{
				"Amaranth Purple",
				ColorUtility.CIELabFromRGB(67f, 15f, 31f, 100f)
			},
			{
				"Amaranth Red",
				ColorUtility.CIELabFromRGB(83f, 13f, 18f, 100f)
			},
			{
				"Amazon",
				ColorUtility.CIELabFromRGB(23f, 48f, 34f, 100f)
			},
			{
				"Amber",
				ColorUtility.CIELabFromRGB(100f, 75f, 0f, 100f)
			},
			{
				"Amber (SAE/ECE)",
				ColorUtility.CIELabFromRGB(100f, 49f, 0f, 100f)
			},
			{
				"American Rose",
				ColorUtility.CIELabFromRGB(100f, 1f, 24f, 100f)
			},
			{
				"Amethyst",
				ColorUtility.CIELabFromRGB(60f, 40f, 80f, 100f)
			},
			{
				"Android Green",
				ColorUtility.CIELabFromRGB(64f, 78f, 22f, 100f)
			},
			{
				"Anti-Flash White",
				ColorUtility.CIELabFromRGB(95f, 95f, 96f, 100f)
			},
			{
				"Antique Brass",
				ColorUtility.CIELabFromRGB(80f, 58f, 46f, 100f)
			},
			{
				"Antique Bronze",
				ColorUtility.CIELabFromRGB(40f, 36f, 12f, 100f)
			},
			{
				"Antique Fuchsia",
				ColorUtility.CIELabFromRGB(57f, 36f, 51f, 100f)
			},
			{
				"Antique Ruby",
				ColorUtility.CIELabFromRGB(52f, 11f, 18f, 100f)
			},
			{
				"Antique White",
				ColorUtility.CIELabFromRGB(98f, 92f, 84f, 100f)
			},
			{
				"Ao (English)",
				ColorUtility.CIELabFromRGB(0f, 50f, 0f, 100f)
			},
			{
				"Apple Green",
				ColorUtility.CIELabFromRGB(55f, 71f, 0f, 100f)
			},
			{
				"Apricot",
				ColorUtility.CIELabFromRGB(98f, 81f, 69f, 100f)
			},
			{
				"Aqua",
				ColorUtility.CIELabFromRGB(0f, 100f, 100f, 100f)
			},
			{
				"Aquamarine",
				ColorUtility.CIELabFromRGB(50f, 100f, 83f, 100f)
			},
			{
				"Army Green",
				ColorUtility.CIELabFromRGB(29f, 33f, 13f, 100f)
			},
			{
				"Arsenic",
				ColorUtility.CIELabFromRGB(23f, 27f, 29f, 100f)
			},
			{
				"Artichoke",
				ColorUtility.CIELabFromRGB(56f, 59f, 47f, 100f)
			},
			{
				"Arylide Yellow",
				ColorUtility.CIELabFromRGB(91f, 84f, 42f, 100f)
			},
			{
				"Ash Grey",
				ColorUtility.CIELabFromRGB(70f, 75f, 71f, 100f)
			},
			{
				"Asparagus",
				ColorUtility.CIELabFromRGB(53f, 66f, 42f, 100f)
			},
			{
				"Atomic Tangerine",
				ColorUtility.CIELabFromRGB(100f, 60f, 40f, 100f)
			},
			{
				"Auburn",
				ColorUtility.CIELabFromRGB(65f, 16f, 16f, 100f)
			},
			{
				"Aureolin",
				ColorUtility.CIELabFromRGB(99f, 93f, 0f, 100f)
			},
			{
				"AuroMetalSaurus",
				ColorUtility.CIELabFromRGB(43f, 50f, 50f, 100f)
			},
			{
				"Avocado",
				ColorUtility.CIELabFromRGB(34f, 51f, 1f, 100f)
			},
			{
				"Azure",
				ColorUtility.CIELabFromRGB(0f, 50f, 100f, 100f)
			},
			{
				"Azure (Web Color)",
				ColorUtility.CIELabFromRGB(94f, 100f, 100f, 100f)
			},
			{
				"Azure Mist",
				ColorUtility.CIELabFromRGB(94f, 100f, 100f, 100f)
			},
			{
				"Azureish White",
				ColorUtility.CIELabFromRGB(86f, 91f, 96f, 100f)
			},
			{
				"Baby Blue",
				ColorUtility.CIELabFromRGB(54f, 81f, 94f, 100f)
			},
			{
				"Baby Blue Eyes",
				ColorUtility.CIELabFromRGB(63f, 79f, 95f, 100f)
			},
			{
				"Baby Pink",
				ColorUtility.CIELabFromRGB(96f, 76f, 76f, 100f)
			},
			{
				"Baby Powder",
				ColorUtility.CIELabFromRGB(100f, 100f, 98f, 100f)
			},
			{
				"Baker-Miller Pink",
				ColorUtility.CIELabFromRGB(100f, 57f, 69f, 100f)
			},
			{
				"Ball Blue",
				ColorUtility.CIELabFromRGB(13f, 67f, 80f, 100f)
			},
			{
				"Banana Mania",
				ColorUtility.CIELabFromRGB(98f, 91f, 71f, 100f)
			},
			{
				"Banana Yellow",
				ColorUtility.CIELabFromRGB(100f, 88f, 21f, 100f)
			},
			{
				"Bangladesh Green",
				ColorUtility.CIELabFromRGB(0f, 42f, 31f, 100f)
			},
			{
				"Barbie Pink",
				ColorUtility.CIELabFromRGB(88f, 13f, 54f, 100f)
			},
			{
				"Barn Red",
				ColorUtility.CIELabFromRGB(49f, 4f, 1f, 100f)
			},
			{
				"Battleship Grey",
				ColorUtility.CIELabFromRGB(52f, 52f, 51f, 100f)
			},
			{
				"Bazaar",
				ColorUtility.CIELabFromRGB(60f, 47f, 48f, 100f)
			},
			{
				"Beau Blue",
				ColorUtility.CIELabFromRGB(74f, 83f, 90f, 100f)
			},
			{
				"Beaver",
				ColorUtility.CIELabFromRGB(62f, 51f, 44f, 100f)
			},
			{
				"Beige",
				ColorUtility.CIELabFromRGB(96f, 96f, 86f, 100f)
			},
			{
				"B'dazzled Blue",
				ColorUtility.CIELabFromRGB(18f, 35f, 58f, 100f)
			},
			{
				"Big Dip O’ruby",
				ColorUtility.CIELabFromRGB(61f, 15f, 26f, 100f)
			},
			{
				"Bisque",
				ColorUtility.CIELabFromRGB(100f, 89f, 77f, 100f)
			},
			{
				"Bistre",
				ColorUtility.CIELabFromRGB(24f, 17f, 12f, 100f)
			},
			{
				"Bistre Brown",
				ColorUtility.CIELabFromRGB(59f, 44f, 9f, 100f)
			},
			{
				"Bitter Lemon",
				ColorUtility.CIELabFromRGB(79f, 88f, 5f, 100f)
			},
			{
				"Bitter Lime",
				ColorUtility.CIELabFromRGB(75f, 100f, 0f, 100f)
			},
			{
				"Bittersweet",
				ColorUtility.CIELabFromRGB(100f, 44f, 37f, 100f)
			},
			{
				"Bittersweet Shimmer",
				ColorUtility.CIELabFromRGB(75f, 31f, 32f, 100f)
			},
			{
				"Black",
				ColorUtility.CIELabFromRGB(0f, 0f, 0f, 100f)
			},
			{
				"Black Bean",
				ColorUtility.CIELabFromRGB(24f, 5f, 1f, 100f)
			},
			{
				"Black Leather Jacket",
				ColorUtility.CIELabFromRGB(15f, 21f, 16f, 100f)
			},
			{
				"Black Olive",
				ColorUtility.CIELabFromRGB(23f, 24f, 21f, 100f)
			},
			{
				"Blanched Almond",
				ColorUtility.CIELabFromRGB(100f, 92f, 80f, 100f)
			},
			{
				"Blast-Off Bronze",
				ColorUtility.CIELabFromRGB(65f, 44f, 39f, 100f)
			},
			{
				"Bleu De France",
				ColorUtility.CIELabFromRGB(19f, 55f, 91f, 100f)
			},
			{
				"Blizzard Blue",
				ColorUtility.CIELabFromRGB(67f, 90f, 93f, 100f)
			},
			{
				"Blond",
				ColorUtility.CIELabFromRGB(98f, 94f, 75f, 100f)
			},
			{
				"Blue",
				ColorUtility.CIELabFromRGB(0f, 0f, 100f, 100f)
			},
			{
				"Blue (Crayola)",
				ColorUtility.CIELabFromRGB(12f, 46f, 100f, 100f)
			},
			{
				"Blue (Munsell)",
				ColorUtility.CIELabFromRGB(0f, 58f, 69f, 100f)
			},
			{
				"Blue (NCS)",
				ColorUtility.CIELabFromRGB(0f, 53f, 74f, 100f)
			},
			{
				"Blue (Pantone)",
				ColorUtility.CIELabFromRGB(0f, 9f, 66f, 100f)
			},
			{
				"Blue (Pigment)",
				ColorUtility.CIELabFromRGB(20f, 20f, 60f, 100f)
			},
			{
				"Blue (RYB)",
				ColorUtility.CIELabFromRGB(1f, 28f, 100f, 100f)
			},
			{
				"Blue Bell",
				ColorUtility.CIELabFromRGB(64f, 64f, 82f, 100f)
			},
			{
				"Blue-Gray",
				ColorUtility.CIELabFromRGB(40f, 60f, 80f, 100f)
			},
			{
				"Blue-Green",
				ColorUtility.CIELabFromRGB(5f, 60f, 73f, 100f)
			},
			{
				"Blue Lagoon",
				ColorUtility.CIELabFromRGB(37f, 58f, 63f, 100f)
			},
			{
				"Blue-Magenta Violet",
				ColorUtility.CIELabFromRGB(33f, 21f, 57f, 100f)
			},
			{
				"Blue Sapphire",
				ColorUtility.CIELabFromRGB(7f, 38f, 50f, 100f)
			},
			{
				"Blue-Violet",
				ColorUtility.CIELabFromRGB(54f, 17f, 89f, 100f)
			},
			{
				"Blue Yonder",
				ColorUtility.CIELabFromRGB(31f, 45f, 65f, 100f)
			},
			{
				"Blueberry",
				ColorUtility.CIELabFromRGB(31f, 53f, 97f, 100f)
			},
			{
				"Bluebonnet",
				ColorUtility.CIELabFromRGB(11f, 11f, 94f, 100f)
			},
			{
				"Blush",
				ColorUtility.CIELabFromRGB(87f, 36f, 51f, 100f)
			},
			{
				"Bole",
				ColorUtility.CIELabFromRGB(47f, 27f, 23f, 100f)
			},
			{
				"Bondi Blue",
				ColorUtility.CIELabFromRGB(0f, 58f, 71f, 100f)
			},
			{
				"Bone",
				ColorUtility.CIELabFromRGB(89f, 85f, 79f, 100f)
			},
			{
				"Boston University Red",
				ColorUtility.CIELabFromRGB(80f, 0f, 0f, 100f)
			},
			{
				"Bottle Green",
				ColorUtility.CIELabFromRGB(0f, 42f, 31f, 100f)
			},
			{
				"Boysenberry",
				ColorUtility.CIELabFromRGB(53f, 20f, 38f, 100f)
			},
			{
				"Brandeis Blue",
				ColorUtility.CIELabFromRGB(0f, 44f, 100f, 100f)
			},
			{
				"Brass",
				ColorUtility.CIELabFromRGB(71f, 65f, 26f, 100f)
			},
			{
				"Brick Red",
				ColorUtility.CIELabFromRGB(80f, 25f, 33f, 100f)
			},
			{
				"Bright Cerulean",
				ColorUtility.CIELabFromRGB(11f, 67f, 84f, 100f)
			},
			{
				"Bright Green",
				ColorUtility.CIELabFromRGB(40f, 100f, 0f, 100f)
			},
			{
				"Bright Lavender",
				ColorUtility.CIELabFromRGB(75f, 58f, 89f, 100f)
			},
			{
				"Bright Lilac",
				ColorUtility.CIELabFromRGB(85f, 57f, 94f, 100f)
			},
			{
				"Bright Maroon",
				ColorUtility.CIELabFromRGB(76f, 13f, 28f, 100f)
			},
			{
				"Bright Navy Blue",
				ColorUtility.CIELabFromRGB(10f, 45f, 82f, 100f)
			},
			{
				"Bright Pink",
				ColorUtility.CIELabFromRGB(100f, 0f, 50f, 100f)
			},
			{
				"Bright Turquoise",
				ColorUtility.CIELabFromRGB(3f, 91f, 87f, 100f)
			},
			{
				"Bright Ube",
				ColorUtility.CIELabFromRGB(82f, 62f, 91f, 100f)
			},
			{
				"Brilliant Azure",
				ColorUtility.CIELabFromRGB(20f, 60f, 100f, 100f)
			},
			{
				"Brilliant Lavender",
				ColorUtility.CIELabFromRGB(96f, 73f, 100f, 100f)
			},
			{
				"Brilliant Rose",
				ColorUtility.CIELabFromRGB(100f, 33f, 64f, 100f)
			},
			{
				"Brink Pink",
				ColorUtility.CIELabFromRGB(98f, 38f, 50f, 100f)
			},
			{
				"British Racing Green",
				ColorUtility.CIELabFromRGB(0f, 26f, 15f, 100f)
			},
			{
				"Bronze",
				ColorUtility.CIELabFromRGB(80f, 50f, 20f, 100f)
			},
			{
				"Bronze Yellow",
				ColorUtility.CIELabFromRGB(45f, 44f, 0f, 100f)
			},
			{
				"Brown (Traditional)",
				ColorUtility.CIELabFromRGB(59f, 29f, 0f, 100f)
			},
			{
				"Brown (Web)",
				ColorUtility.CIELabFromRGB(65f, 16f, 16f, 100f)
			},
			{
				"Brown-Nose",
				ColorUtility.CIELabFromRGB(42f, 27f, 14f, 100f)
			},
			{
				"Brown Yellow",
				ColorUtility.CIELabFromRGB(80f, 60f, 40f, 100f)
			},
			{
				"Brunswick Green",
				ColorUtility.CIELabFromRGB(11f, 30f, 24f, 100f)
			},
			{
				"Bubble Gum",
				ColorUtility.CIELabFromRGB(100f, 76f, 80f, 100f)
			},
			{
				"Bubbles",
				ColorUtility.CIELabFromRGB(91f, 100f, 100f, 100f)
			},
			{
				"Buff",
				ColorUtility.CIELabFromRGB(94f, 86f, 51f, 100f)
			},
			{
				"Bud Green",
				ColorUtility.CIELabFromRGB(48f, 71f, 38f, 100f)
			},
			{
				"Bulgarian Rose",
				ColorUtility.CIELabFromRGB(28f, 2f, 3f, 100f)
			},
			{
				"Burgundy",
				ColorUtility.CIELabFromRGB(50f, 0f, 13f, 100f)
			},
			{
				"Burlywood",
				ColorUtility.CIELabFromRGB(87f, 72f, 53f, 100f)
			},
			{
				"Burnt Orange",
				ColorUtility.CIELabFromRGB(80f, 33f, 0f, 100f)
			},
			{
				"Burnt Sienna",
				ColorUtility.CIELabFromRGB(91f, 45f, 32f, 100f)
			},
			{
				"Burnt Umber",
				ColorUtility.CIELabFromRGB(54f, 20f, 14f, 100f)
			},
			{
				"Byzantine",
				ColorUtility.CIELabFromRGB(74f, 20f, 64f, 100f)
			},
			{
				"Byzantium",
				ColorUtility.CIELabFromRGB(44f, 16f, 39f, 100f)
			},
			{
				"Cadet",
				ColorUtility.CIELabFromRGB(33f, 41f, 45f, 100f)
			},
			{
				"Cadet Blue",
				ColorUtility.CIELabFromRGB(37f, 62f, 63f, 100f)
			},
			{
				"Cadet Grey",
				ColorUtility.CIELabFromRGB(57f, 64f, 69f, 100f)
			},
			{
				"Cadmium Green",
				ColorUtility.CIELabFromRGB(0f, 42f, 24f, 100f)
			},
			{
				"Cadmium Orange",
				ColorUtility.CIELabFromRGB(93f, 53f, 18f, 100f)
			},
			{
				"Cadmium Red",
				ColorUtility.CIELabFromRGB(89f, 0f, 13f, 100f)
			},
			{
				"Cadmium Yellow",
				ColorUtility.CIELabFromRGB(100f, 96f, 0f, 100f)
			},
			{
				"Cafe Au Lait",
				ColorUtility.CIELabFromRGB(65f, 48f, 36f, 100f)
			},
			{
				"Cafe Noir",
				ColorUtility.CIELabFromRGB(29f, 21f, 13f, 100f)
			},
			{
				"Cal Poly Green",
				ColorUtility.CIELabFromRGB(12f, 30f, 17f, 100f)
			},
			{
				"Cambridge Blue",
				ColorUtility.CIELabFromRGB(64f, 76f, 68f, 100f)
			},
			{
				"Camel",
				ColorUtility.CIELabFromRGB(76f, 60f, 42f, 100f)
			},
			{
				"Cameo Pink",
				ColorUtility.CIELabFromRGB(94f, 73f, 80f, 100f)
			},
			{
				"Camouflage Green",
				ColorUtility.CIELabFromRGB(47f, 53f, 42f, 100f)
			},
			{
				"Canary Yellow",
				ColorUtility.CIELabFromRGB(100f, 94f, 0f, 100f)
			},
			{
				"Candy Apple Red",
				ColorUtility.CIELabFromRGB(100f, 3f, 0f, 100f)
			},
			{
				"Candy Pink",
				ColorUtility.CIELabFromRGB(89f, 44f, 48f, 100f)
			},
			{
				"Capri",
				ColorUtility.CIELabFromRGB(0f, 75f, 100f, 100f)
			},
			{
				"Caput Mortuum",
				ColorUtility.CIELabFromRGB(35f, 15f, 13f, 100f)
			},
			{
				"Cardinal",
				ColorUtility.CIELabFromRGB(77f, 12f, 23f, 100f)
			},
			{
				"Caribbean Green",
				ColorUtility.CIELabFromRGB(0f, 80f, 60f, 100f)
			},
			{
				"Carmine",
				ColorUtility.CIELabFromRGB(59f, 0f, 9f, 100f)
			},
			{
				"Carmine (M&P)",
				ColorUtility.CIELabFromRGB(84f, 0f, 25f, 100f)
			},
			{
				"Carmine Pink",
				ColorUtility.CIELabFromRGB(92f, 30f, 26f, 100f)
			},
			{
				"Carmine Red",
				ColorUtility.CIELabFromRGB(100f, 0f, 22f, 100f)
			},
			{
				"Carnation Pink",
				ColorUtility.CIELabFromRGB(100f, 65f, 79f, 100f)
			},
			{
				"Carnelian",
				ColorUtility.CIELabFromRGB(70f, 11f, 11f, 100f)
			},
			{
				"Carolina Blue",
				ColorUtility.CIELabFromRGB(34f, 63f, 83f, 100f)
			},
			{
				"Carrot Orange",
				ColorUtility.CIELabFromRGB(93f, 57f, 13f, 100f)
			},
			{
				"Castleton Green",
				ColorUtility.CIELabFromRGB(0f, 34f, 25f, 100f)
			},
			{
				"Catalina Blue",
				ColorUtility.CIELabFromRGB(2f, 16f, 47f, 100f)
			},
			{
				"Catawba",
				ColorUtility.CIELabFromRGB(44f, 21f, 26f, 100f)
			},
			{
				"Cedar Chest",
				ColorUtility.CIELabFromRGB(79f, 35f, 29f, 100f)
			},
			{
				"Ceil",
				ColorUtility.CIELabFromRGB(57f, 63f, 81f, 100f)
			},
			{
				"Celadon",
				ColorUtility.CIELabFromRGB(67f, 88f, 69f, 100f)
			},
			{
				"Celadon Blue",
				ColorUtility.CIELabFromRGB(0f, 48f, 65f, 100f)
			},
			{
				"Celadon Green",
				ColorUtility.CIELabFromRGB(18f, 52f, 49f, 100f)
			},
			{
				"Celeste",
				ColorUtility.CIELabFromRGB(70f, 100f, 100f, 100f)
			},
			{
				"Celestial Blue",
				ColorUtility.CIELabFromRGB(29f, 59f, 82f, 100f)
			},
			{
				"Cerise",
				ColorUtility.CIELabFromRGB(87f, 19f, 39f, 100f)
			},
			{
				"Cerise Pink",
				ColorUtility.CIELabFromRGB(93f, 23f, 51f, 100f)
			},
			{
				"Cerulean",
				ColorUtility.CIELabFromRGB(0f, 48f, 65f, 100f)
			},
			{
				"Cerulean Blue",
				ColorUtility.CIELabFromRGB(16f, 32f, 75f, 100f)
			},
			{
				"Cerulean Frost",
				ColorUtility.CIELabFromRGB(43f, 61f, 76f, 100f)
			},
			{
				"CG Blue",
				ColorUtility.CIELabFromRGB(0f, 48f, 65f, 100f)
			},
			{
				"CG Red",
				ColorUtility.CIELabFromRGB(88f, 24f, 19f, 100f)
			},
			{
				"Chamoisee",
				ColorUtility.CIELabFromRGB(63f, 47f, 35f, 100f)
			},
			{
				"Champagne",
				ColorUtility.CIELabFromRGB(97f, 91f, 81f, 100f)
			},
			{
				"Charcoal",
				ColorUtility.CIELabFromRGB(21f, 27f, 31f, 100f)
			},
			{
				"Charleston Green",
				ColorUtility.CIELabFromRGB(14f, 17f, 17f, 100f)
			},
			{
				"Charm Pink",
				ColorUtility.CIELabFromRGB(90f, 56f, 67f, 100f)
			},
			{
				"Chartreuse (Traditional)",
				ColorUtility.CIELabFromRGB(87f, 100f, 0f, 100f)
			},
			{
				"Chartreuse (Web)",
				ColorUtility.CIELabFromRGB(50f, 100f, 0f, 100f)
			},
			{
				"Cherry",
				ColorUtility.CIELabFromRGB(87f, 19f, 39f, 100f)
			},
			{
				"Cherry Blossom Pink",
				ColorUtility.CIELabFromRGB(100f, 72f, 77f, 100f)
			},
			{
				"Chestnut",
				ColorUtility.CIELabFromRGB(58f, 27f, 21f, 100f)
			},
			{
				"China Pink",
				ColorUtility.CIELabFromRGB(87f, 44f, 63f, 100f)
			},
			{
				"China Rose",
				ColorUtility.CIELabFromRGB(66f, 32f, 43f, 100f)
			},
			{
				"Chinese Red",
				ColorUtility.CIELabFromRGB(67f, 22f, 12f, 100f)
			},
			{
				"Chinese Violet",
				ColorUtility.CIELabFromRGB(52f, 38f, 53f, 100f)
			},
			{
				"Chocolate (Traditional)",
				ColorUtility.CIELabFromRGB(48f, 25f, 0f, 100f)
			},
			{
				"Chocolate (Web)",
				ColorUtility.CIELabFromRGB(82f, 41f, 12f, 100f)
			},
			{
				"Chrome Yellow",
				ColorUtility.CIELabFromRGB(100f, 65f, 0f, 100f)
			},
			{
				"Cinereous",
				ColorUtility.CIELabFromRGB(60f, 51f, 48f, 100f)
			},
			{
				"Cinnabar",
				ColorUtility.CIELabFromRGB(89f, 26f, 20f, 100f)
			},
			{
				"Cinnamon",
				ColorUtility.CIELabFromRGB(82f, 41f, 12f, 100f)
			},
			{
				"Citrine",
				ColorUtility.CIELabFromRGB(89f, 82f, 4f, 100f)
			},
			{
				"Citron",
				ColorUtility.CIELabFromRGB(62f, 66f, 12f, 100f)
			},
			{
				"Claret",
				ColorUtility.CIELabFromRGB(50f, 9f, 20f, 100f)
			},
			{
				"Classic Rose",
				ColorUtility.CIELabFromRGB(98f, 80f, 91f, 100f)
			},
			{
				"Cobalt Blue",
				ColorUtility.CIELabFromRGB(0f, 28f, 67f, 100f)
			},
			{
				"Cocoa Brown",
				ColorUtility.CIELabFromRGB(82f, 41f, 12f, 100f)
			},
			{
				"Coconut",
				ColorUtility.CIELabFromRGB(59f, 35f, 24f, 100f)
			},
			{
				"Coffee",
				ColorUtility.CIELabFromRGB(44f, 31f, 22f, 100f)
			},
			{
				"Columbia Blue",
				ColorUtility.CIELabFromRGB(77f, 85f, 89f, 100f)
			},
			{
				"Congo Pink",
				ColorUtility.CIELabFromRGB(97f, 51f, 47f, 100f)
			},
			{
				"Cool Black",
				ColorUtility.CIELabFromRGB(0f, 18f, 39f, 100f)
			},
			{
				"Cool Grey",
				ColorUtility.CIELabFromRGB(55f, 57f, 67f, 100f)
			},
			{
				"Copper",
				ColorUtility.CIELabFromRGB(72f, 45f, 20f, 100f)
			},
			{
				"Copper (Crayola)",
				ColorUtility.CIELabFromRGB(85f, 54f, 40f, 100f)
			},
			{
				"Copper Penny",
				ColorUtility.CIELabFromRGB(68f, 44f, 41f, 100f)
			},
			{
				"Copper Red",
				ColorUtility.CIELabFromRGB(80f, 43f, 32f, 100f)
			},
			{
				"Copper Rose",
				ColorUtility.CIELabFromRGB(60f, 40f, 40f, 100f)
			},
			{
				"Coquelicot",
				ColorUtility.CIELabFromRGB(100f, 22f, 0f, 100f)
			},
			{
				"Coral",
				ColorUtility.CIELabFromRGB(100f, 50f, 31f, 100f)
			},
			{
				"Coral Pink",
				ColorUtility.CIELabFromRGB(97f, 51f, 47f, 100f)
			},
			{
				"Coral Red",
				ColorUtility.CIELabFromRGB(100f, 25f, 25f, 100f)
			},
			{
				"Cordovan",
				ColorUtility.CIELabFromRGB(54f, 25f, 27f, 100f)
			},
			{
				"Corn",
				ColorUtility.CIELabFromRGB(98f, 93f, 36f, 100f)
			},
			{
				"Cornell Red",
				ColorUtility.CIELabFromRGB(70f, 11f, 11f, 100f)
			},
			{
				"Cornflower Blue",
				ColorUtility.CIELabFromRGB(39f, 58f, 93f, 100f)
			},
			{
				"Cornsilk",
				ColorUtility.CIELabFromRGB(100f, 97f, 86f, 100f)
			},
			{
				"Cosmic Latte",
				ColorUtility.CIELabFromRGB(100f, 97f, 91f, 100f)
			},
			{
				"Coyote Brown",
				ColorUtility.CIELabFromRGB(51f, 38f, 24f, 100f)
			},
			{
				"Cotton Candy",
				ColorUtility.CIELabFromRGB(100f, 74f, 85f, 100f)
			},
			{
				"Cream",
				ColorUtility.CIELabFromRGB(100f, 99f, 82f, 100f)
			},
			{
				"Crimson",
				ColorUtility.CIELabFromRGB(86f, 8f, 24f, 100f)
			},
			{
				"Crimson Glory",
				ColorUtility.CIELabFromRGB(75f, 0f, 20f, 100f)
			},
			{
				"Crimson Red",
				ColorUtility.CIELabFromRGB(60f, 0f, 0f, 100f)
			},
			{
				"Cyan",
				ColorUtility.CIELabFromRGB(0f, 100f, 100f, 100f)
			},
			{
				"Cyan Azure",
				ColorUtility.CIELabFromRGB(31f, 51f, 71f, 100f)
			},
			{
				"Cyan-Blue Azure",
				ColorUtility.CIELabFromRGB(27f, 51f, 75f, 100f)
			},
			{
				"Cyan Cobalt Blue",
				ColorUtility.CIELabFromRGB(16f, 35f, 61f, 100f)
			},
			{
				"Cyan Cornflower Blue",
				ColorUtility.CIELabFromRGB(9f, 55f, 76f, 100f)
			},
			{
				"Cyan (Process)",
				ColorUtility.CIELabFromRGB(0f, 72f, 92f, 100f)
			},
			{
				"Cyber Grape",
				ColorUtility.CIELabFromRGB(35f, 26f, 49f, 100f)
			},
			{
				"Cyber Yellow",
				ColorUtility.CIELabFromRGB(100f, 83f, 0f, 100f)
			},
			{
				"Daffodil",
				ColorUtility.CIELabFromRGB(100f, 100f, 19f, 100f)
			},
			{
				"Dandelion",
				ColorUtility.CIELabFromRGB(94f, 88f, 19f, 100f)
			},
			{
				"Dark Blue",
				ColorUtility.CIELabFromRGB(0f, 0f, 55f, 100f)
			},
			{
				"Dark Blue-Gray",
				ColorUtility.CIELabFromRGB(40f, 40f, 60f, 100f)
			},
			{
				"Dark Brown",
				ColorUtility.CIELabFromRGB(40f, 26f, 13f, 100f)
			},
			{
				"Dark Brown-Tangelo",
				ColorUtility.CIELabFromRGB(53f, 40f, 31f, 100f)
			},
			{
				"Dark Byzantium",
				ColorUtility.CIELabFromRGB(36f, 22f, 33f, 100f)
			},
			{
				"Dark Candy Apple Red",
				ColorUtility.CIELabFromRGB(64f, 0f, 0f, 100f)
			},
			{
				"Dark Cerulean",
				ColorUtility.CIELabFromRGB(3f, 27f, 49f, 100f)
			},
			{
				"Dark Chestnut",
				ColorUtility.CIELabFromRGB(60f, 41f, 38f, 100f)
			},
			{
				"Dark Coral",
				ColorUtility.CIELabFromRGB(80f, 36f, 27f, 100f)
			},
			{
				"Dark Cyan",
				ColorUtility.CIELabFromRGB(0f, 55f, 55f, 100f)
			},
			{
				"Dark Electric Blue",
				ColorUtility.CIELabFromRGB(33f, 41f, 47f, 100f)
			},
			{
				"Dark Goldenrod",
				ColorUtility.CIELabFromRGB(72f, 53f, 4f, 100f)
			},
			{
				"Dark Gray (X11)",
				ColorUtility.CIELabFromRGB(66f, 66f, 66f, 100f)
			},
			{
				"Dark Green",
				ColorUtility.CIELabFromRGB(0f, 20f, 13f, 100f)
			},
			{
				"Dark Green (X11)",
				ColorUtility.CIELabFromRGB(0f, 39f, 0f, 100f)
			},
			{
				"Dark Imperial Blue",
				ColorUtility.CIELabFromRGB(0f, 25f, 42f, 100f)
			},
			{
				"Dark Imperial-er Blue",
				ColorUtility.CIELabFromRGB(0f, 8f, 49f, 100f)
			},
			{
				"Dark Jungle Green",
				ColorUtility.CIELabFromRGB(10f, 14f, 13f, 100f)
			},
			{
				"Dark Khaki",
				ColorUtility.CIELabFromRGB(74f, 72f, 42f, 100f)
			},
			{
				"Dark Lava",
				ColorUtility.CIELabFromRGB(28f, 24f, 20f, 100f)
			},
			{
				"Dark Lavender",
				ColorUtility.CIELabFromRGB(45f, 31f, 59f, 100f)
			},
			{
				"Dark Liver",
				ColorUtility.CIELabFromRGB(33f, 29f, 31f, 100f)
			},
			{
				"Dark Liver (Horses)",
				ColorUtility.CIELabFromRGB(33f, 24f, 22f, 100f)
			},
			{
				"Dark Magenta",
				ColorUtility.CIELabFromRGB(55f, 0f, 55f, 100f)
			},
			{
				"Dark Medium Gray",
				ColorUtility.CIELabFromRGB(66f, 66f, 66f, 100f)
			},
			{
				"Dark Midnight Blue",
				ColorUtility.CIELabFromRGB(0f, 20f, 40f, 100f)
			},
			{
				"Dark Moss Green",
				ColorUtility.CIELabFromRGB(29f, 36f, 14f, 100f)
			},
			{
				"Dark Olive Green",
				ColorUtility.CIELabFromRGB(33f, 42f, 18f, 100f)
			},
			{
				"Dark Orange",
				ColorUtility.CIELabFromRGB(100f, 55f, 0f, 100f)
			},
			{
				"Dark Orchid",
				ColorUtility.CIELabFromRGB(60f, 20f, 80f, 100f)
			},
			{
				"Dark Pastel Blue",
				ColorUtility.CIELabFromRGB(47f, 62f, 80f, 100f)
			},
			{
				"Dark Pastel Green",
				ColorUtility.CIELabFromRGB(1f, 75f, 24f, 100f)
			},
			{
				"Dark Pastel Purple",
				ColorUtility.CIELabFromRGB(59f, 44f, 84f, 100f)
			},
			{
				"Dark Pastel Red",
				ColorUtility.CIELabFromRGB(76f, 23f, 13f, 100f)
			},
			{
				"Dark Pink",
				ColorUtility.CIELabFromRGB(91f, 33f, 50f, 100f)
			},
			{
				"Dark Powder Blue",
				ColorUtility.CIELabFromRGB(0f, 20f, 60f, 100f)
			},
			{
				"Dark Puce",
				ColorUtility.CIELabFromRGB(31f, 23f, 24f, 100f)
			},
			{
				"Dark Purple",
				ColorUtility.CIELabFromRGB(19f, 10f, 20f, 100f)
			},
			{
				"Dark Raspberry",
				ColorUtility.CIELabFromRGB(53f, 15f, 34f, 100f)
			},
			{
				"Dark Red",
				ColorUtility.CIELabFromRGB(55f, 0f, 0f, 100f)
			},
			{
				"Dark Salmon",
				ColorUtility.CIELabFromRGB(91f, 59f, 48f, 100f)
			},
			{
				"Dark Scarlet",
				ColorUtility.CIELabFromRGB(34f, 1f, 10f, 100f)
			},
			{
				"Dark Sea Green",
				ColorUtility.CIELabFromRGB(56f, 74f, 56f, 100f)
			},
			{
				"Dark Sienna",
				ColorUtility.CIELabFromRGB(24f, 8f, 8f, 100f)
			},
			{
				"Dark Sky Blue",
				ColorUtility.CIELabFromRGB(55f, 75f, 84f, 100f)
			},
			{
				"Dark Slate Blue",
				ColorUtility.CIELabFromRGB(28f, 24f, 55f, 100f)
			},
			{
				"Dark Slate Gray",
				ColorUtility.CIELabFromRGB(18f, 31f, 31f, 100f)
			},
			{
				"Dark Spring Green",
				ColorUtility.CIELabFromRGB(9f, 45f, 27f, 100f)
			},
			{
				"Dark Tan",
				ColorUtility.CIELabFromRGB(57f, 51f, 32f, 100f)
			},
			{
				"Dark Tangerine",
				ColorUtility.CIELabFromRGB(100f, 66f, 7f, 100f)
			},
			{
				"Dark Taupe",
				ColorUtility.CIELabFromRGB(28f, 24f, 20f, 100f)
			},
			{
				"Dark Terra Cotta",
				ColorUtility.CIELabFromRGB(80f, 31f, 36f, 100f)
			},
			{
				"Dark Turquoise",
				ColorUtility.CIELabFromRGB(0f, 81f, 82f, 100f)
			},
			{
				"Dark Vanilla",
				ColorUtility.CIELabFromRGB(82f, 75f, 66f, 100f)
			},
			{
				"Dark Violet",
				ColorUtility.CIELabFromRGB(58f, 0f, 83f, 100f)
			},
			{
				"Dark Yellow",
				ColorUtility.CIELabFromRGB(61f, 53f, 5f, 100f)
			},
			{
				"Dartmouth Green",
				ColorUtility.CIELabFromRGB(0f, 44f, 24f, 100f)
			},
			{
				"Davy's Grey",
				ColorUtility.CIELabFromRGB(33f, 33f, 33f, 100f)
			},
			{
				"Debian Red",
				ColorUtility.CIELabFromRGB(84f, 4f, 33f, 100f)
			},
			{
				"Deep Aquamarine",
				ColorUtility.CIELabFromRGB(25f, 51f, 43f, 100f)
			},
			{
				"Deep Carmine",
				ColorUtility.CIELabFromRGB(66f, 13f, 24f, 100f)
			},
			{
				"Deep Carmine Pink",
				ColorUtility.CIELabFromRGB(94f, 19f, 22f, 100f)
			},
			{
				"Deep Carrot Orange",
				ColorUtility.CIELabFromRGB(91f, 41f, 17f, 100f)
			},
			{
				"Deep Cerise",
				ColorUtility.CIELabFromRGB(85f, 20f, 53f, 100f)
			},
			{
				"Deep Champagne",
				ColorUtility.CIELabFromRGB(98f, 84f, 65f, 100f)
			},
			{
				"Deep Chestnut",
				ColorUtility.CIELabFromRGB(73f, 31f, 28f, 100f)
			},
			{
				"Deep Coffee",
				ColorUtility.CIELabFromRGB(44f, 26f, 25f, 100f)
			},
			{
				"Deep Fuchsia",
				ColorUtility.CIELabFromRGB(76f, 33f, 76f, 100f)
			},
			{
				"Deep Green",
				ColorUtility.CIELabFromRGB(2f, 40f, 3f, 100f)
			},
			{
				"Deep Green-Cyan Turquoise",
				ColorUtility.CIELabFromRGB(5f, 49f, 38f, 100f)
			},
			{
				"Deep Jungle Green",
				ColorUtility.CIELabFromRGB(0f, 29f, 29f, 100f)
			},
			{
				"Deep Koamaru",
				ColorUtility.CIELabFromRGB(20f, 20f, 40f, 100f)
			},
			{
				"Deep Lemon",
				ColorUtility.CIELabFromRGB(96f, 78f, 10f, 100f)
			},
			{
				"Deep Lilac",
				ColorUtility.CIELabFromRGB(60f, 33f, 73f, 100f)
			},
			{
				"Deep Magenta",
				ColorUtility.CIELabFromRGB(80f, 0f, 80f, 100f)
			},
			{
				"Deep Maroon",
				ColorUtility.CIELabFromRGB(51f, 0f, 0f, 100f)
			},
			{
				"Deep Mauve",
				ColorUtility.CIELabFromRGB(83f, 45f, 83f, 100f)
			},
			{
				"Deep Moss Green",
				ColorUtility.CIELabFromRGB(21f, 37f, 23f, 100f)
			},
			{
				"Deep Peach",
				ColorUtility.CIELabFromRGB(100f, 80f, 64f, 100f)
			},
			{
				"Deep Pink",
				ColorUtility.CIELabFromRGB(100f, 8f, 58f, 100f)
			},
			{
				"Deep Puce",
				ColorUtility.CIELabFromRGB(66f, 36f, 41f, 100f)
			},
			{
				"Deep Red",
				ColorUtility.CIELabFromRGB(52f, 0f, 0f, 100f)
			},
			{
				"Deep Ruby",
				ColorUtility.CIELabFromRGB(52f, 25f, 36f, 100f)
			},
			{
				"Deep Saffron",
				ColorUtility.CIELabFromRGB(100f, 60f, 20f, 100f)
			},
			{
				"Deep Sky Blue",
				ColorUtility.CIELabFromRGB(0f, 75f, 100f, 100f)
			},
			{
				"Deep Space Sparkle",
				ColorUtility.CIELabFromRGB(29f, 39f, 42f, 100f)
			},
			{
				"Deep Spring Bud",
				ColorUtility.CIELabFromRGB(33f, 42f, 18f, 100f)
			},
			{
				"Deep Taupe",
				ColorUtility.CIELabFromRGB(49f, 37f, 38f, 100f)
			},
			{
				"Deep Tuscan Red",
				ColorUtility.CIELabFromRGB(40f, 26f, 30f, 100f)
			},
			{
				"Deep Violet",
				ColorUtility.CIELabFromRGB(20f, 0f, 40f, 100f)
			},
			{
				"Deer",
				ColorUtility.CIELabFromRGB(73f, 53f, 35f, 100f)
			},
			{
				"Denim",
				ColorUtility.CIELabFromRGB(8f, 38f, 74f, 100f)
			},
			{
				"Desaturated Cyan",
				ColorUtility.CIELabFromRGB(40f, 60f, 60f, 100f)
			},
			{
				"Desert",
				ColorUtility.CIELabFromRGB(76f, 60f, 42f, 100f)
			},
			{
				"Desert Sand",
				ColorUtility.CIELabFromRGB(93f, 79f, 69f, 100f)
			},
			{
				"Desire",
				ColorUtility.CIELabFromRGB(92f, 24f, 33f, 100f)
			},
			{
				"Diamond",
				ColorUtility.CIELabFromRGB(73f, 95f, 100f, 100f)
			},
			{
				"Dim Gray",
				ColorUtility.CIELabFromRGB(41f, 41f, 41f, 100f)
			},
			{
				"Dirt",
				ColorUtility.CIELabFromRGB(61f, 46f, 33f, 100f)
			},
			{
				"Dodger Blue",
				ColorUtility.CIELabFromRGB(12f, 56f, 100f, 100f)
			},
			{
				"Dogwood Rose",
				ColorUtility.CIELabFromRGB(84f, 9f, 41f, 100f)
			},
			{
				"Dollar Bill",
				ColorUtility.CIELabFromRGB(52f, 73f, 40f, 100f)
			},
			{
				"Donkey Brown",
				ColorUtility.CIELabFromRGB(40f, 30f, 16f, 100f)
			},
			{
				"Drab",
				ColorUtility.CIELabFromRGB(59f, 44f, 9f, 100f)
			},
			{
				"Duke Blue",
				ColorUtility.CIELabFromRGB(0f, 0f, 61f, 100f)
			},
			{
				"Dust Storm",
				ColorUtility.CIELabFromRGB(90f, 80f, 79f, 100f)
			},
			{
				"Dutch White",
				ColorUtility.CIELabFromRGB(94f, 87f, 73f, 100f)
			},
			{
				"Earth Yellow",
				ColorUtility.CIELabFromRGB(88f, 66f, 37f, 100f)
			},
			{
				"Ebony",
				ColorUtility.CIELabFromRGB(33f, 36f, 31f, 100f)
			},
			{
				"Ecru",
				ColorUtility.CIELabFromRGB(76f, 70f, 50f, 100f)
			},
			{
				"Eerie Black",
				ColorUtility.CIELabFromRGB(11f, 11f, 11f, 100f)
			},
			{
				"Eggplant",
				ColorUtility.CIELabFromRGB(38f, 25f, 32f, 100f)
			},
			{
				"Eggshell",
				ColorUtility.CIELabFromRGB(94f, 92f, 84f, 100f)
			},
			{
				"Egyptian Blue",
				ColorUtility.CIELabFromRGB(6f, 20f, 65f, 100f)
			},
			{
				"Electric Blue",
				ColorUtility.CIELabFromRGB(49f, 98f, 100f, 100f)
			},
			{
				"Electric Crimson",
				ColorUtility.CIELabFromRGB(100f, 0f, 25f, 100f)
			},
			{
				"Electric Cyan",
				ColorUtility.CIELabFromRGB(0f, 100f, 100f, 100f)
			},
			{
				"Electric Green",
				ColorUtility.CIELabFromRGB(0f, 100f, 0f, 100f)
			},
			{
				"Electric Indigo",
				ColorUtility.CIELabFromRGB(44f, 0f, 100f, 100f)
			},
			{
				"Electric Lavender",
				ColorUtility.CIELabFromRGB(96f, 73f, 100f, 100f)
			},
			{
				"Electric Lime",
				ColorUtility.CIELabFromRGB(80f, 100f, 0f, 100f)
			},
			{
				"Electric Purple",
				ColorUtility.CIELabFromRGB(75f, 0f, 100f, 100f)
			},
			{
				"Electric Ultramarine",
				ColorUtility.CIELabFromRGB(25f, 0f, 100f, 100f)
			},
			{
				"Electric Violet",
				ColorUtility.CIELabFromRGB(56f, 0f, 100f, 100f)
			},
			{
				"Electric Yellow",
				ColorUtility.CIELabFromRGB(100f, 100f, 20f, 100f)
			},
			{
				"Emerald",
				ColorUtility.CIELabFromRGB(31f, 78f, 47f, 100f)
			},
			{
				"Eminence",
				ColorUtility.CIELabFromRGB(42f, 19f, 51f, 100f)
			},
			{
				"English Green",
				ColorUtility.CIELabFromRGB(11f, 30f, 24f, 100f)
			},
			{
				"English Lavender",
				ColorUtility.CIELabFromRGB(71f, 51f, 58f, 100f)
			},
			{
				"English Red",
				ColorUtility.CIELabFromRGB(67f, 29f, 32f, 100f)
			},
			{
				"English Violet",
				ColorUtility.CIELabFromRGB(34f, 24f, 36f, 100f)
			},
			{
				"Eton Blue",
				ColorUtility.CIELabFromRGB(59f, 78f, 64f, 100f)
			},
			{
				"Eucalyptus",
				ColorUtility.CIELabFromRGB(27f, 84f, 66f, 100f)
			},
			{
				"Fallow",
				ColorUtility.CIELabFromRGB(76f, 60f, 42f, 100f)
			},
			{
				"Falu Red",
				ColorUtility.CIELabFromRGB(50f, 9f, 9f, 100f)
			},
			{
				"Fandango",
				ColorUtility.CIELabFromRGB(71f, 20f, 54f, 100f)
			},
			{
				"Fandango Pink",
				ColorUtility.CIELabFromRGB(87f, 32f, 52f, 100f)
			},
			{
				"Fashion Fuchsia",
				ColorUtility.CIELabFromRGB(96f, 0f, 63f, 100f)
			},
			{
				"Fawn",
				ColorUtility.CIELabFromRGB(90f, 67f, 44f, 100f)
			},
			{
				"Feldgrau",
				ColorUtility.CIELabFromRGB(30f, 36f, 33f, 100f)
			},
			{
				"Feldspar",
				ColorUtility.CIELabFromRGB(99f, 84f, 69f, 100f)
			},
			{
				"Fern Green",
				ColorUtility.CIELabFromRGB(31f, 47f, 26f, 100f)
			},
			{
				"Ferrari Red",
				ColorUtility.CIELabFromRGB(100f, 16f, 0f, 100f)
			},
			{
				"Field Drab",
				ColorUtility.CIELabFromRGB(42f, 33f, 12f, 100f)
			},
			{
				"Firebrick",
				ColorUtility.CIELabFromRGB(70f, 13f, 13f, 100f)
			},
			{
				"Fire Engine Red",
				ColorUtility.CIELabFromRGB(81f, 13f, 16f, 100f)
			},
			{
				"Flame",
				ColorUtility.CIELabFromRGB(89f, 35f, 13f, 100f)
			},
			{
				"Flamingo Pink",
				ColorUtility.CIELabFromRGB(99f, 56f, 67f, 100f)
			},
			{
				"Flattery",
				ColorUtility.CIELabFromRGB(42f, 27f, 14f, 100f)
			},
			{
				"Flavescent",
				ColorUtility.CIELabFromRGB(97f, 91f, 56f, 100f)
			},
			{
				"Flax",
				ColorUtility.CIELabFromRGB(93f, 86f, 51f, 100f)
			},
			{
				"Flirt",
				ColorUtility.CIELabFromRGB(64f, 0f, 43f, 100f)
			},
			{
				"Floral White",
				ColorUtility.CIELabFromRGB(100f, 98f, 94f, 100f)
			},
			{
				"Fluorescent Orange",
				ColorUtility.CIELabFromRGB(100f, 75f, 0f, 100f)
			},
			{
				"Fluorescent Pink",
				ColorUtility.CIELabFromRGB(100f, 8f, 58f, 100f)
			},
			{
				"Fluorescent Yellow",
				ColorUtility.CIELabFromRGB(80f, 100f, 0f, 100f)
			},
			{
				"Folly",
				ColorUtility.CIELabFromRGB(100f, 0f, 31f, 100f)
			},
			{
				"Forest Green (Traditional)",
				ColorUtility.CIELabFromRGB(0f, 27f, 13f, 100f)
			},
			{
				"Forest Green (Web)",
				ColorUtility.CIELabFromRGB(13f, 55f, 13f, 100f)
			},
			{
				"French Beige",
				ColorUtility.CIELabFromRGB(65f, 48f, 36f, 100f)
			},
			{
				"French Bistre",
				ColorUtility.CIELabFromRGB(52f, 43f, 30f, 100f)
			},
			{
				"French Blue",
				ColorUtility.CIELabFromRGB(0f, 45f, 73f, 100f)
			},
			{
				"French Fuchsia",
				ColorUtility.CIELabFromRGB(99f, 25f, 57f, 100f)
			},
			{
				"French Lilac",
				ColorUtility.CIELabFromRGB(53f, 38f, 56f, 100f)
			},
			{
				"French Lime",
				ColorUtility.CIELabFromRGB(62f, 99f, 22f, 100f)
			},
			{
				"French Mauve",
				ColorUtility.CIELabFromRGB(83f, 45f, 83f, 100f)
			},
			{
				"French Pink",
				ColorUtility.CIELabFromRGB(99f, 42f, 62f, 100f)
			},
			{
				"French Plum",
				ColorUtility.CIELabFromRGB(51f, 8f, 33f, 100f)
			},
			{
				"French Puce",
				ColorUtility.CIELabFromRGB(31f, 9f, 4f, 100f)
			},
			{
				"French Raspberry",
				ColorUtility.CIELabFromRGB(78f, 17f, 28f, 100f)
			},
			{
				"French Rose",
				ColorUtility.CIELabFromRGB(96f, 29f, 54f, 100f)
			},
			{
				"French Sky Blue",
				ColorUtility.CIELabFromRGB(47f, 71f, 100f, 100f)
			},
			{
				"French Violet",
				ColorUtility.CIELabFromRGB(53f, 2f, 81f, 100f)
			},
			{
				"French Wine",
				ColorUtility.CIELabFromRGB(67f, 12f, 27f, 100f)
			},
			{
				"Fresh Air",
				ColorUtility.CIELabFromRGB(65f, 91f, 100f, 100f)
			},
			{
				"Fuchsia",
				ColorUtility.CIELabFromRGB(100f, 0f, 100f, 100f)
			},
			{
				"Fuchsia (Crayola)",
				ColorUtility.CIELabFromRGB(76f, 33f, 76f, 100f)
			},
			{
				"Fuchsia Pink",
				ColorUtility.CIELabFromRGB(100f, 47f, 100f, 100f)
			},
			{
				"Fuchsia Purple",
				ColorUtility.CIELabFromRGB(80f, 22f, 48f, 100f)
			},
			{
				"Fuchsia Rose",
				ColorUtility.CIELabFromRGB(78f, 26f, 46f, 100f)
			},
			{
				"Fulvous",
				ColorUtility.CIELabFromRGB(89f, 52f, 0f, 100f)
			},
			{
				"Fuzzy Wuzzy",
				ColorUtility.CIELabFromRGB(80f, 40f, 40f, 100f)
			},
			{
				"Gainsboro",
				ColorUtility.CIELabFromRGB(86f, 86f, 86f, 100f)
			},
			{
				"Gamboge",
				ColorUtility.CIELabFromRGB(89f, 61f, 6f, 100f)
			},
			{
				"Gamboge Orange (Brown)",
				ColorUtility.CIELabFromRGB(60f, 40f, 0f, 100f)
			},
			{
				"Generic Viridian",
				ColorUtility.CIELabFromRGB(0f, 50f, 40f, 100f)
			},
			{
				"Ghost White",
				ColorUtility.CIELabFromRGB(97f, 97f, 100f, 100f)
			},
			{
				"Giants Orange",
				ColorUtility.CIELabFromRGB(100f, 35f, 11f, 100f)
			},
			{
				"Grussrel",
				ColorUtility.CIELabFromRGB(69f, 40f, 0f, 100f)
			},
			{
				"Glaucous",
				ColorUtility.CIELabFromRGB(38f, 51f, 71f, 100f)
			},
			{
				"Glitter",
				ColorUtility.CIELabFromRGB(90f, 91f, 98f, 100f)
			},
			{
				"GO Green",
				ColorUtility.CIELabFromRGB(0f, 67f, 40f, 100f)
			},
			{
				"Gold (Metallic)",
				ColorUtility.CIELabFromRGB(83f, 69f, 22f, 100f)
			},
			{
				"Gold (Web) (Golden)",
				ColorUtility.CIELabFromRGB(100f, 84f, 0f, 100f)
			},
			{
				"Gold Fusion",
				ColorUtility.CIELabFromRGB(52f, 46f, 31f, 100f)
			},
			{
				"Golden Brown",
				ColorUtility.CIELabFromRGB(60f, 40f, 8f, 100f)
			},
			{
				"Golden Poppy",
				ColorUtility.CIELabFromRGB(99f, 76f, 0f, 100f)
			},
			{
				"Golden Yellow",
				ColorUtility.CIELabFromRGB(100f, 87f, 0f, 100f)
			},
			{
				"Goldenrod",
				ColorUtility.CIELabFromRGB(85f, 65f, 13f, 100f)
			},
			{
				"Granny Smith Apple",
				ColorUtility.CIELabFromRGB(66f, 89f, 63f, 100f)
			},
			{
				"Grape",
				ColorUtility.CIELabFromRGB(44f, 18f, 66f, 100f)
			},
			{
				"Gray",
				ColorUtility.CIELabFromRGB(50f, 50f, 50f, 100f)
			},
			{
				"Gray (HTML/CSS Gray)",
				ColorUtility.CIELabFromRGB(50f, 50f, 50f, 100f)
			},
			{
				"Gray (X11 Gray)",
				ColorUtility.CIELabFromRGB(75f, 75f, 75f, 100f)
			},
			{
				"Gray-Asparagus",
				ColorUtility.CIELabFromRGB(27f, 35f, 27f, 100f)
			},
			{
				"Gray-Blue",
				ColorUtility.CIELabFromRGB(55f, 57f, 67f, 100f)
			},
			{
				"Green (Color Wheel) (X11 Green)",
				ColorUtility.CIELabFromRGB(0f, 100f, 0f, 100f)
			},
			{
				"Green (Crayola)",
				ColorUtility.CIELabFromRGB(11f, 67f, 47f, 100f)
			},
			{
				"Green (HTML/CSS Color)",
				ColorUtility.CIELabFromRGB(0f, 50f, 0f, 100f)
			},
			{
				"Green (Munsell)",
				ColorUtility.CIELabFromRGB(0f, 66f, 47f, 100f)
			},
			{
				"Green (NCS)",
				ColorUtility.CIELabFromRGB(0f, 62f, 42f, 100f)
			},
			{
				"Green (Pantone)",
				ColorUtility.CIELabFromRGB(0f, 68f, 26f, 100f)
			},
			{
				"Green (Pigment)",
				ColorUtility.CIELabFromRGB(0f, 65f, 31f, 100f)
			},
			{
				"Green (RYB)",
				ColorUtility.CIELabFromRGB(40f, 69f, 20f, 100f)
			},
			{
				"Green-Blue",
				ColorUtility.CIELabFromRGB(7f, 39f, 71f, 100f)
			},
			{
				"Green-Cyan",
				ColorUtility.CIELabFromRGB(0f, 60f, 40f, 100f)
			},
			{
				"Green-Yellow",
				ColorUtility.CIELabFromRGB(68f, 100f, 18f, 100f)
			},
			{
				"Grizzly",
				ColorUtility.CIELabFromRGB(53f, 35f, 9f, 100f)
			},
			{
				"Grullo",
				ColorUtility.CIELabFromRGB(66f, 60f, 53f, 100f)
			},
			{
				"Guppie Green",
				ColorUtility.CIELabFromRGB(0f, 100f, 50f, 100f)
			},
			{
				"Halayà Úbe",
				ColorUtility.CIELabFromRGB(40f, 22f, 33f, 100f)
			},
			{
				"Han Blue",
				ColorUtility.CIELabFromRGB(27f, 42f, 81f, 100f)
			},
			{
				"Han Purple",
				ColorUtility.CIELabFromRGB(32f, 9f, 98f, 100f)
			},
			{
				"Hansa Yellow",
				ColorUtility.CIELabFromRGB(91f, 84f, 42f, 100f)
			},
			{
				"Harlequin",
				ColorUtility.CIELabFromRGB(25f, 100f, 0f, 100f)
			},
			{
				"Harlequin Green",
				ColorUtility.CIELabFromRGB(27f, 80f, 9f, 100f)
			},
			{
				"Harvard Crimson",
				ColorUtility.CIELabFromRGB(79f, 0f, 9f, 100f)
			},
			{
				"Harvest Gold",
				ColorUtility.CIELabFromRGB(85f, 57f, 0f, 100f)
			},
			{
				"Heart Gold",
				ColorUtility.CIELabFromRGB(50f, 50f, 0f, 100f)
			},
			{
				"Heliotrope",
				ColorUtility.CIELabFromRGB(87f, 45f, 100f, 100f)
			},
			{
				"Heliotrope Gray",
				ColorUtility.CIELabFromRGB(67f, 60f, 66f, 100f)
			},
			{
				"Heliotrope Magenta",
				ColorUtility.CIELabFromRGB(67f, 0f, 73f, 100f)
			},
			{
				"Hollywood Cerise",
				ColorUtility.CIELabFromRGB(96f, 0f, 63f, 100f)
			},
			{
				"Honeydew",
				ColorUtility.CIELabFromRGB(94f, 100f, 94f, 100f)
			},
			{
				"Honolulu Blue",
				ColorUtility.CIELabFromRGB(0f, 43f, 69f, 100f)
			},
			{
				"Hooker's Green",
				ColorUtility.CIELabFromRGB(29f, 47f, 42f, 100f)
			},
			{
				"Hot Magenta",
				ColorUtility.CIELabFromRGB(100f, 11f, 81f, 100f)
			},
			{
				"Hot Pink",
				ColorUtility.CIELabFromRGB(100f, 41f, 71f, 100f)
			},
			{
				"Hunter Green",
				ColorUtility.CIELabFromRGB(21f, 37f, 23f, 100f)
			},
			{
				"Iceberg",
				ColorUtility.CIELabFromRGB(44f, 65f, 82f, 100f)
			},
			{
				"Icterine",
				ColorUtility.CIELabFromRGB(99f, 97f, 37f, 100f)
			},
			{
				"Illuminating Emerald",
				ColorUtility.CIELabFromRGB(19f, 57f, 47f, 100f)
			},
			{
				"Imperial",
				ColorUtility.CIELabFromRGB(38f, 18f, 42f, 100f)
			},
			{
				"Imperial Blue",
				ColorUtility.CIELabFromRGB(0f, 14f, 58f, 100f)
			},
			{
				"Imperial Purple",
				ColorUtility.CIELabFromRGB(40f, 1f, 24f, 100f)
			},
			{
				"Imperial Red",
				ColorUtility.CIELabFromRGB(93f, 16f, 22f, 100f)
			},
			{
				"Inchworm",
				ColorUtility.CIELabFromRGB(70f, 93f, 36f, 100f)
			},
			{
				"Independence",
				ColorUtility.CIELabFromRGB(30f, 32f, 43f, 100f)
			},
			{
				"India Green",
				ColorUtility.CIELabFromRGB(7f, 53f, 3f, 100f)
			},
			{
				"Indian Red",
				ColorUtility.CIELabFromRGB(80f, 36f, 36f, 100f)
			},
			{
				"Indian Yellow",
				ColorUtility.CIELabFromRGB(89f, 66f, 34f, 100f)
			},
			{
				"Indigo",
				ColorUtility.CIELabFromRGB(44f, 0f, 100f, 100f)
			},
			{
				"Indigo Dye",
				ColorUtility.CIELabFromRGB(4f, 12f, 57f, 100f)
			},
			{
				"Indigo (Web)",
				ColorUtility.CIELabFromRGB(29f, 0f, 51f, 100f)
			},
			{
				"International Klein Blue",
				ColorUtility.CIELabFromRGB(0f, 18f, 65f, 100f)
			},
			{
				"International Orange (Aerospace)",
				ColorUtility.CIELabFromRGB(100f, 31f, 0f, 100f)
			},
			{
				"International Orange (Engineering)",
				ColorUtility.CIELabFromRGB(73f, 9f, 5f, 100f)
			},
			{
				"International Orange (Golden Gate Bridge)",
				ColorUtility.CIELabFromRGB(75f, 21f, 17f, 100f)
			},
			{
				"Iris",
				ColorUtility.CIELabFromRGB(35f, 31f, 81f, 100f)
			},
			{
				"Irresistible",
				ColorUtility.CIELabFromRGB(70f, 27f, 42f, 100f)
			},
			{
				"Isabelline",
				ColorUtility.CIELabFromRGB(96f, 94f, 93f, 100f)
			},
			{
				"Islamic Green",
				ColorUtility.CIELabFromRGB(0f, 56f, 0f, 100f)
			},
			{
				"Italian Sky Blue",
				ColorUtility.CIELabFromRGB(70f, 100f, 100f, 100f)
			},
			{
				"Ivory",
				ColorUtility.CIELabFromRGB(100f, 100f, 94f, 100f)
			},
			{
				"Jade",
				ColorUtility.CIELabFromRGB(0f, 66f, 42f, 100f)
			},
			{
				"Japanese Carmine",
				ColorUtility.CIELabFromRGB(62f, 16f, 20f, 100f)
			},
			{
				"Japanese Indigo",
				ColorUtility.CIELabFromRGB(15f, 26f, 28f, 100f)
			},
			{
				"Japanese Violet",
				ColorUtility.CIELabFromRGB(36f, 20f, 34f, 100f)
			},
			{
				"Jasmine",
				ColorUtility.CIELabFromRGB(97f, 87f, 49f, 100f)
			},
			{
				"Jasper",
				ColorUtility.CIELabFromRGB(84f, 23f, 24f, 100f)
			},
			{
				"Jazzberry Jam",
				ColorUtility.CIELabFromRGB(65f, 4f, 37f, 100f)
			},
			{
				"Jelly Bean",
				ColorUtility.CIELabFromRGB(85f, 38f, 31f, 100f)
			},
			{
				"Jet",
				ColorUtility.CIELabFromRGB(20f, 20f, 20f, 100f)
			},
			{
				"Jonquil",
				ColorUtility.CIELabFromRGB(96f, 79f, 9f, 100f)
			},
			{
				"Jordy Blue",
				ColorUtility.CIELabFromRGB(54f, 73f, 95f, 100f)
			},
			{
				"June Bud",
				ColorUtility.CIELabFromRGB(74f, 85f, 34f, 100f)
			},
			{
				"Jungle Green",
				ColorUtility.CIELabFromRGB(16f, 67f, 53f, 100f)
			},
			{
				"Kelly Green",
				ColorUtility.CIELabFromRGB(30f, 73f, 9f, 100f)
			},
			{
				"Kenyan Copper",
				ColorUtility.CIELabFromRGB(49f, 11f, 2f, 100f)
			},
			{
				"Keppel",
				ColorUtility.CIELabFromRGB(23f, 69f, 62f, 100f)
			},
			{
				"Jawad/Chicken Color (HTML/CSS) (Khaki)",
				ColorUtility.CIELabFromRGB(76f, 69f, 57f, 100f)
			},
			{
				"Khaki (X11) (Light Khaki)",
				ColorUtility.CIELabFromRGB(94f, 90f, 55f, 100f)
			},
			{
				"Kobe",
				ColorUtility.CIELabFromRGB(53f, 18f, 9f, 100f)
			},
			{
				"Kobi",
				ColorUtility.CIELabFromRGB(91f, 62f, 77f, 100f)
			},
			{
				"Kombu Green",
				ColorUtility.CIELabFromRGB(21f, 26f, 19f, 100f)
			},
			{
				"KU Crimson",
				ColorUtility.CIELabFromRGB(91f, 0f, 5f, 100f)
			},
			{
				"La Salle Green",
				ColorUtility.CIELabFromRGB(3f, 47f, 19f, 100f)
			},
			{
				"Languid Lavender",
				ColorUtility.CIELabFromRGB(84f, 79f, 87f, 100f)
			},
			{
				"Lapis Lazuli",
				ColorUtility.CIELabFromRGB(15f, 38f, 61f, 100f)
			},
			{
				"Laser Lemon",
				ColorUtility.CIELabFromRGB(100f, 100f, 40f, 100f)
			},
			{
				"Laurel Green",
				ColorUtility.CIELabFromRGB(66f, 73f, 62f, 100f)
			},
			{
				"Lava",
				ColorUtility.CIELabFromRGB(81f, 6f, 13f, 100f)
			},
			{
				"Lavender (Floral)",
				ColorUtility.CIELabFromRGB(71f, 49f, 86f, 100f)
			},
			{
				"Lavender (Web)",
				ColorUtility.CIELabFromRGB(90f, 90f, 98f, 100f)
			},
			{
				"Lavender Blue",
				ColorUtility.CIELabFromRGB(80f, 80f, 100f, 100f)
			},
			{
				"Lavender Blush",
				ColorUtility.CIELabFromRGB(100f, 94f, 96f, 100f)
			},
			{
				"Lavender Gray",
				ColorUtility.CIELabFromRGB(77f, 76f, 82f, 100f)
			},
			{
				"Lavender Indigo",
				ColorUtility.CIELabFromRGB(58f, 34f, 92f, 100f)
			},
			{
				"Lavender Magenta",
				ColorUtility.CIELabFromRGB(93f, 51f, 93f, 100f)
			},
			{
				"Lavender Mist",
				ColorUtility.CIELabFromRGB(90f, 90f, 98f, 100f)
			},
			{
				"Lavender Pink",
				ColorUtility.CIELabFromRGB(98f, 68f, 82f, 100f)
			},
			{
				"Lavender Purple",
				ColorUtility.CIELabFromRGB(59f, 48f, 71f, 100f)
			},
			{
				"Lavender Rose",
				ColorUtility.CIELabFromRGB(98f, 63f, 89f, 100f)
			},
			{
				"Lawn Green",
				ColorUtility.CIELabFromRGB(49f, 99f, 0f, 100f)
			},
			{
				"Lemon",
				ColorUtility.CIELabFromRGB(100f, 97f, 0f, 100f)
			},
			{
				"Lemon Chiffon",
				ColorUtility.CIELabFromRGB(100f, 98f, 80f, 100f)
			},
			{
				"Lemon Curry",
				ColorUtility.CIELabFromRGB(80f, 63f, 11f, 100f)
			},
			{
				"Lemon Glacier",
				ColorUtility.CIELabFromRGB(99f, 100f, 0f, 100f)
			},
			{
				"Lemon Lime",
				ColorUtility.CIELabFromRGB(89f, 100f, 0f, 100f)
			},
			{
				"Lemon Meringue",
				ColorUtility.CIELabFromRGB(96f, 92f, 75f, 100f)
			},
			{
				"Lemon Yellow",
				ColorUtility.CIELabFromRGB(100f, 96f, 31f, 100f)
			},
			{
				"Lenurple",
				ColorUtility.CIELabFromRGB(73f, 58f, 85f, 100f)
			},
			{
				"Licorice",
				ColorUtility.CIELabFromRGB(10f, 7f, 6f, 100f)
			},
			{
				"Liberty",
				ColorUtility.CIELabFromRGB(33f, 35f, 65f, 100f)
			},
			{
				"Light Apricot",
				ColorUtility.CIELabFromRGB(99f, 84f, 69f, 100f)
			},
			{
				"Light Blue",
				ColorUtility.CIELabFromRGB(68f, 85f, 90f, 100f)
			},
			{
				"Light Brilliant Red",
				ColorUtility.CIELabFromRGB(100f, 18f, 18f, 100f)
			},
			{
				"Light Brown",
				ColorUtility.CIELabFromRGB(71f, 40f, 11f, 100f)
			},
			{
				"Light Carmine Pink",
				ColorUtility.CIELabFromRGB(90f, 40f, 44f, 100f)
			},
			{
				"Light Cobalt Blue",
				ColorUtility.CIELabFromRGB(53f, 67f, 88f, 100f)
			},
			{
				"Light Coral",
				ColorUtility.CIELabFromRGB(94f, 50f, 50f, 100f)
			},
			{
				"Light Cornflower Blue",
				ColorUtility.CIELabFromRGB(58f, 80f, 92f, 100f)
			},
			{
				"Light Crimson",
				ColorUtility.CIELabFromRGB(96f, 41f, 57f, 100f)
			},
			{
				"Light Cyan",
				ColorUtility.CIELabFromRGB(88f, 100f, 100f, 100f)
			},
			{
				"Light Deep Pink",
				ColorUtility.CIELabFromRGB(100f, 36f, 80f, 100f)
			},
			{
				"Light French Beige",
				ColorUtility.CIELabFromRGB(78f, 68f, 50f, 100f)
			},
			{
				"Light Fuchsia Pink",
				ColorUtility.CIELabFromRGB(98f, 52f, 94f, 100f)
			},
			{
				"Light Goldenrod Yellow",
				ColorUtility.CIELabFromRGB(98f, 98f, 82f, 100f)
			},
			{
				"Light Gray",
				ColorUtility.CIELabFromRGB(83f, 83f, 83f, 100f)
			},
			{
				"Light Grayish Magenta",
				ColorUtility.CIELabFromRGB(80f, 60f, 80f, 100f)
			},
			{
				"Light Green",
				ColorUtility.CIELabFromRGB(56f, 93f, 56f, 100f)
			},
			{
				"Light Hot Pink",
				ColorUtility.CIELabFromRGB(100f, 70f, 87f, 100f)
			},
			{
				"Light Khaki",
				ColorUtility.CIELabFromRGB(94f, 90f, 55f, 100f)
			},
			{
				"Light Medium Orchid",
				ColorUtility.CIELabFromRGB(83f, 61f, 80f, 100f)
			},
			{
				"Light Moss Green",
				ColorUtility.CIELabFromRGB(68f, 87f, 68f, 100f)
			},
			{
				"Light Orchid",
				ColorUtility.CIELabFromRGB(90f, 66f, 84f, 100f)
			},
			{
				"Light Pastel Purple",
				ColorUtility.CIELabFromRGB(69f, 61f, 85f, 100f)
			},
			{
				"Light Pink",
				ColorUtility.CIELabFromRGB(100f, 71f, 76f, 100f)
			},
			{
				"Light Red Ochre",
				ColorUtility.CIELabFromRGB(91f, 45f, 32f, 100f)
			},
			{
				"Light Salmon",
				ColorUtility.CIELabFromRGB(100f, 63f, 48f, 100f)
			},
			{
				"Light Salmon Pink",
				ColorUtility.CIELabFromRGB(100f, 60f, 60f, 100f)
			},
			{
				"Light Sea Green",
				ColorUtility.CIELabFromRGB(13f, 70f, 67f, 100f)
			},
			{
				"Light Sky Blue",
				ColorUtility.CIELabFromRGB(53f, 81f, 98f, 100f)
			},
			{
				"Light Slate Gray",
				ColorUtility.CIELabFromRGB(47f, 53f, 60f, 100f)
			},
			{
				"Light Steel Blue",
				ColorUtility.CIELabFromRGB(69f, 77f, 87f, 100f)
			},
			{
				"Light Taupe",
				ColorUtility.CIELabFromRGB(70f, 55f, 43f, 100f)
			},
			{
				"Light Thulian Pink",
				ColorUtility.CIELabFromRGB(90f, 56f, 67f, 100f)
			},
			{
				"Light Yellow",
				ColorUtility.CIELabFromRGB(100f, 100f, 88f, 100f)
			},
			{
				"Lilac",
				ColorUtility.CIELabFromRGB(78f, 64f, 78f, 100f)
			},
			{
				"Lime (Color Wheel)",
				ColorUtility.CIELabFromRGB(75f, 100f, 0f, 100f)
			},
			{
				"Lime (Web) (X11 Green)",
				ColorUtility.CIELabFromRGB(0f, 100f, 0f, 100f)
			},
			{
				"Lime Green",
				ColorUtility.CIELabFromRGB(20f, 80f, 20f, 100f)
			},
			{
				"Limerick",
				ColorUtility.CIELabFromRGB(62f, 76f, 4f, 100f)
			},
			{
				"Lincoln Green",
				ColorUtility.CIELabFromRGB(10f, 35f, 2f, 100f)
			},
			{
				"Linen",
				ColorUtility.CIELabFromRGB(98f, 94f, 90f, 100f)
			},
			{
				"Lion",
				ColorUtility.CIELabFromRGB(76f, 60f, 42f, 100f)
			},
			{
				"Liseran Purple",
				ColorUtility.CIELabFromRGB(87f, 44f, 63f, 100f)
			},
			{
				"Little Boy Blue",
				ColorUtility.CIELabFromRGB(42f, 63f, 86f, 100f)
			},
			{
				"Liver",
				ColorUtility.CIELabFromRGB(40f, 30f, 28f, 100f)
			},
			{
				"Liver (Dogs)",
				ColorUtility.CIELabFromRGB(72f, 43f, 16f, 100f)
			},
			{
				"Liver (Organ)",
				ColorUtility.CIELabFromRGB(42f, 18f, 12f, 100f)
			},
			{
				"Liver Chestnut",
				ColorUtility.CIELabFromRGB(60f, 45f, 34f, 100f)
			},
			{
				"Livid",
				ColorUtility.CIELabFromRGB(40f, 60f, 80f, 100f)
			},
			{
				"Lumber",
				ColorUtility.CIELabFromRGB(100f, 89f, 80f, 100f)
			},
			{
				"Lust",
				ColorUtility.CIELabFromRGB(90f, 13f, 13f, 100f)
			},
			{
				"Magenta",
				ColorUtility.CIELabFromRGB(100f, 0f, 100f, 100f)
			},
			{
				"Magenta (Crayola)",
				ColorUtility.CIELabFromRGB(100f, 33f, 64f, 100f)
			},
			{
				"Magenta (Dye)",
				ColorUtility.CIELabFromRGB(79f, 12f, 48f, 100f)
			},
			{
				"Magenta (Pantone)",
				ColorUtility.CIELabFromRGB(82f, 25f, 49f, 100f)
			},
			{
				"Magenta (Process)",
				ColorUtility.CIELabFromRGB(100f, 0f, 56f, 100f)
			},
			{
				"Magenta Haze",
				ColorUtility.CIELabFromRGB(62f, 27f, 46f, 100f)
			},
			{
				"Magenta-Pink",
				ColorUtility.CIELabFromRGB(80f, 20f, 55f, 100f)
			},
			{
				"Magic Mint",
				ColorUtility.CIELabFromRGB(67f, 94f, 82f, 100f)
			},
			{
				"Magnolia",
				ColorUtility.CIELabFromRGB(97f, 96f, 100f, 100f)
			},
			{
				"Mahogany",
				ColorUtility.CIELabFromRGB(75f, 25f, 0f, 100f)
			},
			{
				"Maize",
				ColorUtility.CIELabFromRGB(98f, 93f, 36f, 100f)
			},
			{
				"Majorelle Blue",
				ColorUtility.CIELabFromRGB(38f, 31f, 86f, 100f)
			},
			{
				"Malachite",
				ColorUtility.CIELabFromRGB(4f, 85f, 32f, 100f)
			},
			{
				"Manatee",
				ColorUtility.CIELabFromRGB(59f, 60f, 67f, 100f)
			},
			{
				"Mango Tango",
				ColorUtility.CIELabFromRGB(100f, 51f, 26f, 100f)
			},
			{
				"Mantis",
				ColorUtility.CIELabFromRGB(45f, 76f, 40f, 100f)
			},
			{
				"Mardi Gras",
				ColorUtility.CIELabFromRGB(53f, 0f, 52f, 100f)
			},
			{
				"Marigold",
				ColorUtility.CIELabFromRGB(92f, 64f, 13f, 100f)
			},
			{
				"Maroon (Crayola)",
				ColorUtility.CIELabFromRGB(76f, 13f, 28f, 100f)
			},
			{
				"Maroon (HTML/CSS)",
				ColorUtility.CIELabFromRGB(50f, 0f, 0f, 100f)
			},
			{
				"Maroon (X11)",
				ColorUtility.CIELabFromRGB(69f, 19f, 38f, 100f)
			},
			{
				"Mauve",
				ColorUtility.CIELabFromRGB(88f, 69f, 100f, 100f)
			},
			{
				"Mauve Taupe",
				ColorUtility.CIELabFromRGB(57f, 37f, 43f, 100f)
			},
			{
				"Mauvelous",
				ColorUtility.CIELabFromRGB(94f, 60f, 67f, 100f)
			},
			{
				"May Green",
				ColorUtility.CIELabFromRGB(30f, 57f, 25f, 100f)
			},
			{
				"Maya Blue",
				ColorUtility.CIELabFromRGB(45f, 76f, 98f, 100f)
			},
			{
				"Meat Brown",
				ColorUtility.CIELabFromRGB(90f, 72f, 23f, 100f)
			},
			{
				"Medium Aquamarine",
				ColorUtility.CIELabFromRGB(40f, 87f, 67f, 100f)
			},
			{
				"Medium Blue",
				ColorUtility.CIELabFromRGB(0f, 0f, 80f, 100f)
			},
			{
				"Medium Candy Apple Red",
				ColorUtility.CIELabFromRGB(89f, 2f, 17f, 100f)
			},
			{
				"Medium Carmine",
				ColorUtility.CIELabFromRGB(69f, 25f, 21f, 100f)
			},
			{
				"Medium Champagne",
				ColorUtility.CIELabFromRGB(95f, 90f, 67f, 100f)
			},
			{
				"Medium Electric Blue",
				ColorUtility.CIELabFromRGB(1f, 31f, 59f, 100f)
			},
			{
				"Medium Jungle Green",
				ColorUtility.CIELabFromRGB(11f, 21f, 18f, 100f)
			},
			{
				"Medium Lavender Magenta",
				ColorUtility.CIELabFromRGB(87f, 63f, 87f, 100f)
			},
			{
				"Medium Orchid",
				ColorUtility.CIELabFromRGB(73f, 33f, 83f, 100f)
			},
			{
				"Medium Persian Blue",
				ColorUtility.CIELabFromRGB(0f, 40f, 65f, 100f)
			},
			{
				"Medium Purple",
				ColorUtility.CIELabFromRGB(58f, 44f, 86f, 100f)
			},
			{
				"Medium Red-Violet",
				ColorUtility.CIELabFromRGB(73f, 20f, 52f, 100f)
			},
			{
				"Medium Ruby",
				ColorUtility.CIELabFromRGB(67f, 25f, 41f, 100f)
			},
			{
				"Medium Sea Green",
				ColorUtility.CIELabFromRGB(24f, 70f, 44f, 100f)
			},
			{
				"Medium Sky Blue",
				ColorUtility.CIELabFromRGB(50f, 85f, 92f, 100f)
			},
			{
				"Medium Slate Blue",
				ColorUtility.CIELabFromRGB(48f, 41f, 93f, 100f)
			},
			{
				"Medium Spring Bud",
				ColorUtility.CIELabFromRGB(79f, 86f, 53f, 100f)
			},
			{
				"Medium Spring Green",
				ColorUtility.CIELabFromRGB(0f, 98f, 60f, 100f)
			},
			{
				"Medium Taupe",
				ColorUtility.CIELabFromRGB(40f, 30f, 28f, 100f)
			},
			{
				"Medium Turquoise",
				ColorUtility.CIELabFromRGB(28f, 82f, 80f, 100f)
			},
			{
				"Medium Tuscan Red",
				ColorUtility.CIELabFromRGB(47f, 27f, 23f, 100f)
			},
			{
				"Medium Vermilion",
				ColorUtility.CIELabFromRGB(85f, 38f, 23f, 100f)
			},
			{
				"Medium Violet-Red",
				ColorUtility.CIELabFromRGB(78f, 8f, 52f, 100f)
			},
			{
				"Mellow Apricot",
				ColorUtility.CIELabFromRGB(97f, 72f, 47f, 100f)
			},
			{
				"Mellow Yellow",
				ColorUtility.CIELabFromRGB(97f, 87f, 49f, 100f)
			},
			{
				"Melon",
				ColorUtility.CIELabFromRGB(99f, 74f, 71f, 100f)
			},
			{
				"Metallic Seaweed",
				ColorUtility.CIELabFromRGB(4f, 49f, 55f, 100f)
			},
			{
				"Metallic Sunburst",
				ColorUtility.CIELabFromRGB(61f, 49f, 22f, 100f)
			},
			{
				"Mexican Pink",
				ColorUtility.CIELabFromRGB(89f, 0f, 49f, 100f)
			},
			{
				"Midnight Blue",
				ColorUtility.CIELabFromRGB(10f, 10f, 44f, 100f)
			},
			{
				"Midnight Green (Eagle Green)",
				ColorUtility.CIELabFromRGB(0f, 29f, 33f, 100f)
			},
			{
				"Mikado Yellow",
				ColorUtility.CIELabFromRGB(100f, 77f, 5f, 100f)
			},
			{
				"Mindaro",
				ColorUtility.CIELabFromRGB(89f, 98f, 53f, 100f)
			},
			{
				"Ming",
				ColorUtility.CIELabFromRGB(21f, 45f, 49f, 100f)
			},
			{
				"Mint",
				ColorUtility.CIELabFromRGB(24f, 71f, 54f, 100f)
			},
			{
				"Mint Cream",
				ColorUtility.CIELabFromRGB(96f, 100f, 98f, 100f)
			},
			{
				"Mint Green",
				ColorUtility.CIELabFromRGB(60f, 100f, 60f, 100f)
			},
			{
				"Misty Rose",
				ColorUtility.CIELabFromRGB(100f, 89f, 88f, 100f)
			},
			{
				"Moccasin",
				ColorUtility.CIELabFromRGB(98f, 92f, 84f, 100f)
			},
			{
				"Mode Beige",
				ColorUtility.CIELabFromRGB(59f, 44f, 9f, 100f)
			},
			{
				"Moonstone Blue",
				ColorUtility.CIELabFromRGB(45f, 66f, 76f, 100f)
			},
			{
				"Mordant Red 19",
				ColorUtility.CIELabFromRGB(68f, 5f, 0f, 100f)
			},
			{
				"Moss Green",
				ColorUtility.CIELabFromRGB(54f, 60f, 36f, 100f)
			},
			{
				"Mountain Meadow",
				ColorUtility.CIELabFromRGB(19f, 73f, 56f, 100f)
			},
			{
				"Mountbatten Pink",
				ColorUtility.CIELabFromRGB(60f, 48f, 55f, 100f)
			},
			{
				"MSU Green",
				ColorUtility.CIELabFromRGB(9f, 27f, 23f, 100f)
			},
			{
				"Mughal Green",
				ColorUtility.CIELabFromRGB(19f, 38f, 19f, 100f)
			},
			{
				"Mulberry",
				ColorUtility.CIELabFromRGB(77f, 29f, 55f, 100f)
			},
			{
				"Mustard",
				ColorUtility.CIELabFromRGB(100f, 86f, 35f, 100f)
			},
			{
				"Myrtle Green",
				ColorUtility.CIELabFromRGB(19f, 47f, 45f, 100f)
			},
			{
				"Nadeshiko Pink",
				ColorUtility.CIELabFromRGB(96f, 68f, 78f, 100f)
			},
			{
				"Napier Green",
				ColorUtility.CIELabFromRGB(16f, 50f, 0f, 100f)
			},
			{
				"Naples Yellow",
				ColorUtility.CIELabFromRGB(98f, 85f, 37f, 100f)
			},
			{
				"Navajo White",
				ColorUtility.CIELabFromRGB(100f, 87f, 68f, 100f)
			},
			{
				"Navy",
				ColorUtility.CIELabFromRGB(0f, 0f, 50f, 100f)
			},
			{
				"Navy Purple",
				ColorUtility.CIELabFromRGB(58f, 34f, 92f, 100f)
			},
			{
				"Neon Carrot",
				ColorUtility.CIELabFromRGB(100f, 64f, 26f, 100f)
			},
			{
				"Neon Fuchsia",
				ColorUtility.CIELabFromRGB(100f, 25f, 39f, 100f)
			},
			{
				"Neon Green",
				ColorUtility.CIELabFromRGB(22f, 100f, 8f, 100f)
			},
			{
				"New Car",
				ColorUtility.CIELabFromRGB(13f, 31f, 78f, 100f)
			},
			{
				"New York Pink",
				ColorUtility.CIELabFromRGB(84f, 51f, 50f, 100f)
			},
			{
				"Non-Photo Blue",
				ColorUtility.CIELabFromRGB(64f, 87f, 93f, 100f)
			},
			{
				"North Texas Green",
				ColorUtility.CIELabFromRGB(2f, 56f, 20f, 100f)
			},
			{
				"Nyanza",
				ColorUtility.CIELabFromRGB(91f, 100f, 86f, 100f)
			},
			{
				"Ocean Boat Blue",
				ColorUtility.CIELabFromRGB(0f, 47f, 75f, 100f)
			},
			{
				"Ochre",
				ColorUtility.CIELabFromRGB(80f, 47f, 13f, 100f)
			},
			{
				"Office Green",
				ColorUtility.CIELabFromRGB(0f, 50f, 0f, 100f)
			},
			{
				"Old Burgundy",
				ColorUtility.CIELabFromRGB(26f, 19f, 18f, 100f)
			},
			{
				"Old Gold",
				ColorUtility.CIELabFromRGB(81f, 71f, 23f, 100f)
			},
			{
				"Old Heliotrope",
				ColorUtility.CIELabFromRGB(34f, 24f, 36f, 100f)
			},
			{
				"Old Lace",
				ColorUtility.CIELabFromRGB(99f, 96f, 90f, 100f)
			},
			{
				"Old Lavender",
				ColorUtility.CIELabFromRGB(47f, 41f, 47f, 100f)
			},
			{
				"Old Mauve",
				ColorUtility.CIELabFromRGB(40f, 19f, 28f, 100f)
			},
			{
				"Old Moss Green",
				ColorUtility.CIELabFromRGB(53f, 49f, 21f, 100f)
			},
			{
				"Old Rose",
				ColorUtility.CIELabFromRGB(75f, 50f, 51f, 100f)
			},
			{
				"Old Silver",
				ColorUtility.CIELabFromRGB(52f, 52f, 51f, 100f)
			},
			{
				"Olive",
				ColorUtility.CIELabFromRGB(50f, 50f, 0f, 100f)
			},
			{
				"Olive Drab",
				ColorUtility.CIELabFromRGB(24f, 20f, 12f, 100f)
			},
			{
				"Olivine",
				ColorUtility.CIELabFromRGB(60f, 73f, 45f, 100f)
			},
			{
				"Onyx",
				ColorUtility.CIELabFromRGB(21f, 22f, 22f, 100f)
			},
			{
				"Opera Mauve",
				ColorUtility.CIELabFromRGB(72f, 52f, 65f, 100f)
			},
			{
				"Orange (Color Wheel)",
				ColorUtility.CIELabFromRGB(100f, 50f, 0f, 100f)
			},
			{
				"Orange (Crayola)",
				ColorUtility.CIELabFromRGB(100f, 46f, 22f, 100f)
			},
			{
				"Orange (Pantone)",
				ColorUtility.CIELabFromRGB(100f, 35f, 0f, 100f)
			},
			{
				"Orange (RYB)",
				ColorUtility.CIELabFromRGB(98f, 60f, 1f, 100f)
			},
			{
				"Orange (Web)",
				ColorUtility.CIELabFromRGB(100f, 65f, 0f, 100f)
			},
			{
				"Orange Peel",
				ColorUtility.CIELabFromRGB(100f, 62f, 0f, 100f)
			},
			{
				"Orange-Red",
				ColorUtility.CIELabFromRGB(100f, 27f, 0f, 100f)
			},
			{
				"Orange-Yellow",
				ColorUtility.CIELabFromRGB(97f, 84f, 41f, 100f)
			},
			{
				"Orchid",
				ColorUtility.CIELabFromRGB(85f, 44f, 84f, 100f)
			},
			{
				"Orchid Pink",
				ColorUtility.CIELabFromRGB(95f, 74f, 80f, 100f)
			},
			{
				"Orioles Orange",
				ColorUtility.CIELabFromRGB(98f, 31f, 8f, 100f)
			},
			{
				"Otter Brown",
				ColorUtility.CIELabFromRGB(40f, 26f, 13f, 100f)
			},
			{
				"Outer Space",
				ColorUtility.CIELabFromRGB(25f, 29f, 30f, 100f)
			},
			{
				"Outrageous Orange",
				ColorUtility.CIELabFromRGB(100f, 43f, 29f, 100f)
			},
			{
				"Oxford Blue",
				ColorUtility.CIELabFromRGB(0f, 13f, 28f, 100f)
			},
			{
				"OU Crimson Red",
				ColorUtility.CIELabFromRGB(60f, 0f, 0f, 100f)
			},
			{
				"Pakistan Green",
				ColorUtility.CIELabFromRGB(0f, 40f, 0f, 100f)
			},
			{
				"Palatinate Blue",
				ColorUtility.CIELabFromRGB(15f, 23f, 89f, 100f)
			},
			{
				"Palatinate Purple",
				ColorUtility.CIELabFromRGB(41f, 16f, 38f, 100f)
			},
			{
				"Pale Aqua",
				ColorUtility.CIELabFromRGB(74f, 83f, 90f, 100f)
			},
			{
				"Pale Blue",
				ColorUtility.CIELabFromRGB(69f, 93f, 93f, 100f)
			},
			{
				"Pale Brown",
				ColorUtility.CIELabFromRGB(60f, 46f, 33f, 100f)
			},
			{
				"Pale Carmine",
				ColorUtility.CIELabFromRGB(69f, 25f, 21f, 100f)
			},
			{
				"Pale Cerulean",
				ColorUtility.CIELabFromRGB(61f, 77f, 89f, 100f)
			},
			{
				"Pale Chestnut",
				ColorUtility.CIELabFromRGB(87f, 68f, 69f, 100f)
			},
			{
				"Pale Copper",
				ColorUtility.CIELabFromRGB(85f, 54f, 40f, 100f)
			},
			{
				"Pale Cornflower Blue",
				ColorUtility.CIELabFromRGB(67f, 80f, 94f, 100f)
			},
			{
				"Pale Cyan",
				ColorUtility.CIELabFromRGB(53f, 83f, 97f, 100f)
			},
			{
				"Pale Gold",
				ColorUtility.CIELabFromRGB(90f, 75f, 54f, 100f)
			},
			{
				"Pale Goldenrod",
				ColorUtility.CIELabFromRGB(93f, 91f, 67f, 100f)
			},
			{
				"Pale Green",
				ColorUtility.CIELabFromRGB(60f, 98f, 60f, 100f)
			},
			{
				"Pale Lavender",
				ColorUtility.CIELabFromRGB(86f, 82f, 100f, 100f)
			},
			{
				"Pale Magenta",
				ColorUtility.CIELabFromRGB(98f, 52f, 90f, 100f)
			},
			{
				"Pale Magenta-Pink",
				ColorUtility.CIELabFromRGB(100f, 60f, 80f, 100f)
			},
			{
				"Pale Pink",
				ColorUtility.CIELabFromRGB(98f, 85f, 87f, 100f)
			},
			{
				"Pale Plum",
				ColorUtility.CIELabFromRGB(87f, 63f, 87f, 100f)
			},
			{
				"Pale Red-Violet",
				ColorUtility.CIELabFromRGB(86f, 44f, 58f, 100f)
			},
			{
				"Pale Robin Egg Blue",
				ColorUtility.CIELabFromRGB(59f, 87f, 82f, 100f)
			},
			{
				"Pale Silver",
				ColorUtility.CIELabFromRGB(79f, 75f, 73f, 100f)
			},
			{
				"Pale Spring Bud",
				ColorUtility.CIELabFromRGB(93f, 92f, 74f, 100f)
			},
			{
				"Pale Taupe",
				ColorUtility.CIELabFromRGB(74f, 60f, 49f, 100f)
			},
			{
				"Pale Turquoise",
				ColorUtility.CIELabFromRGB(69f, 93f, 93f, 100f)
			},
			{
				"Pale Violet",
				ColorUtility.CIELabFromRGB(80f, 60f, 100f, 100f)
			},
			{
				"Pale Violet-Red",
				ColorUtility.CIELabFromRGB(86f, 44f, 58f, 100f)
			},
			{
				"Pansy Purple",
				ColorUtility.CIELabFromRGB(47f, 9f, 29f, 100f)
			},
			{
				"Paolo Veronese Green",
				ColorUtility.CIELabFromRGB(0f, 61f, 49f, 100f)
			},
			{
				"Papaya Whip",
				ColorUtility.CIELabFromRGB(100f, 94f, 84f, 100f)
			},
			{
				"Paradise Pink",
				ColorUtility.CIELabFromRGB(90f, 24f, 38f, 100f)
			},
			{
				"Paris Green",
				ColorUtility.CIELabFromRGB(31f, 78f, 47f, 100f)
			},
			{
				"Pastel Blue",
				ColorUtility.CIELabFromRGB(68f, 78f, 81f, 100f)
			},
			{
				"Pastel Brown",
				ColorUtility.CIELabFromRGB(51f, 41f, 33f, 100f)
			},
			{
				"Pastel Gray",
				ColorUtility.CIELabFromRGB(81f, 81f, 77f, 100f)
			},
			{
				"Pastel Green",
				ColorUtility.CIELabFromRGB(47f, 87f, 47f, 100f)
			},
			{
				"Pastel Magenta",
				ColorUtility.CIELabFromRGB(96f, 60f, 76f, 100f)
			},
			{
				"Pastel Orange",
				ColorUtility.CIELabFromRGB(100f, 70f, 28f, 100f)
			},
			{
				"Pastel Pink",
				ColorUtility.CIELabFromRGB(87f, 65f, 64f, 100f)
			},
			{
				"Pastel Purple",
				ColorUtility.CIELabFromRGB(70f, 62f, 71f, 100f)
			},
			{
				"Pastel Red",
				ColorUtility.CIELabFromRGB(100f, 41f, 38f, 100f)
			},
			{
				"Pastel Violet",
				ColorUtility.CIELabFromRGB(80f, 60f, 79f, 100f)
			},
			{
				"Pastel Yellow",
				ColorUtility.CIELabFromRGB(99f, 99f, 59f, 100f)
			},
			{
				"Patriarch",
				ColorUtility.CIELabFromRGB(50f, 0f, 50f, 100f)
			},
			{
				"Payne's Grey",
				ColorUtility.CIELabFromRGB(33f, 41f, 47f, 100f)
			},
			{
				"Peachier",
				ColorUtility.CIELabFromRGB(100f, 90f, 71f, 100f)
			},
			{
				"Peach",
				ColorUtility.CIELabFromRGB(100f, 80f, 64f, 100f)
			},
			{
				"Peach-Orange",
				ColorUtility.CIELabFromRGB(100f, 80f, 60f, 100f)
			},
			{
				"Peach Puff",
				ColorUtility.CIELabFromRGB(100f, 85f, 73f, 100f)
			},
			{
				"Peach-Yellow",
				ColorUtility.CIELabFromRGB(98f, 87f, 68f, 100f)
			},
			{
				"Pear",
				ColorUtility.CIELabFromRGB(82f, 89f, 19f, 100f)
			},
			{
				"Pearl",
				ColorUtility.CIELabFromRGB(92f, 88f, 78f, 100f)
			},
			{
				"Pearl Aqua",
				ColorUtility.CIELabFromRGB(53f, 85f, 75f, 100f)
			},
			{
				"Pearly Purple",
				ColorUtility.CIELabFromRGB(72f, 41f, 64f, 100f)
			},
			{
				"Peridot",
				ColorUtility.CIELabFromRGB(90f, 89f, 0f, 100f)
			},
			{
				"Periwinkle",
				ColorUtility.CIELabFromRGB(80f, 80f, 100f, 100f)
			},
			{
				"Persian Blue",
				ColorUtility.CIELabFromRGB(11f, 22f, 73f, 100f)
			},
			{
				"Persian Green",
				ColorUtility.CIELabFromRGB(0f, 65f, 58f, 100f)
			},
			{
				"Persian Indigo",
				ColorUtility.CIELabFromRGB(20f, 7f, 48f, 100f)
			},
			{
				"Persian Orange",
				ColorUtility.CIELabFromRGB(85f, 56f, 35f, 100f)
			},
			{
				"Persian Pink",
				ColorUtility.CIELabFromRGB(97f, 50f, 75f, 100f)
			},
			{
				"Persian Plum",
				ColorUtility.CIELabFromRGB(44f, 11f, 11f, 100f)
			},
			{
				"Persian Red",
				ColorUtility.CIELabFromRGB(80f, 20f, 20f, 100f)
			},
			{
				"Persian Rose",
				ColorUtility.CIELabFromRGB(100f, 16f, 64f, 100f)
			},
			{
				"Persimmon",
				ColorUtility.CIELabFromRGB(93f, 35f, 0f, 100f)
			},
			{
				"Peru",
				ColorUtility.CIELabFromRGB(80f, 52f, 25f, 100f)
			},
			{
				"Phlox",
				ColorUtility.CIELabFromRGB(87f, 0f, 100f, 100f)
			},
			{
				"Phthalo Blue",
				ColorUtility.CIELabFromRGB(0f, 6f, 54f, 100f)
			},
			{
				"Phthalo Green",
				ColorUtility.CIELabFromRGB(7f, 21f, 14f, 100f)
			},
			{
				"Picton Blue",
				ColorUtility.CIELabFromRGB(27f, 69f, 91f, 100f)
			},
			{
				"Pictorial Carmine",
				ColorUtility.CIELabFromRGB(76f, 4f, 31f, 100f)
			},
			{
				"Piggy Pink",
				ColorUtility.CIELabFromRGB(99f, 87f, 90f, 100f)
			},
			{
				"Pine Green",
				ColorUtility.CIELabFromRGB(0f, 47f, 44f, 100f)
			},
			{
				"Pineapple",
				ColorUtility.CIELabFromRGB(34f, 24f, 5f, 100f)
			},
			{
				"Pink",
				ColorUtility.CIELabFromRGB(100f, 75f, 80f, 100f)
			},
			{
				"Pink (Pantone)",
				ColorUtility.CIELabFromRGB(84f, 28f, 58f, 100f)
			},
			{
				"Pink Lace",
				ColorUtility.CIELabFromRGB(100f, 87f, 96f, 100f)
			},
			{
				"Pink Lavender",
				ColorUtility.CIELabFromRGB(85f, 70f, 82f, 100f)
			},
			{
				"Pink-Orange",
				ColorUtility.CIELabFromRGB(100f, 60f, 40f, 100f)
			},
			{
				"Pink Pearl",
				ColorUtility.CIELabFromRGB(91f, 67f, 81f, 100f)
			},
			{
				"Pink Raspberry",
				ColorUtility.CIELabFromRGB(60f, 0f, 21f, 100f)
			},
			{
				"Pink Sherbet",
				ColorUtility.CIELabFromRGB(97f, 56f, 65f, 100f)
			},
			{
				"Pistachio",
				ColorUtility.CIELabFromRGB(58f, 77f, 45f, 100f)
			},
			{
				"Platinum",
				ColorUtility.CIELabFromRGB(90f, 89f, 89f, 100f)
			},
			{
				"Plum",
				ColorUtility.CIELabFromRGB(56f, 27f, 52f, 100f)
			},
			{
				"Plum (Web)",
				ColorUtility.CIELabFromRGB(87f, 63f, 87f, 100f)
			},
			{
				"Pomp And Power",
				ColorUtility.CIELabFromRGB(53f, 38f, 56f, 100f)
			},
			{
				"Popstar",
				ColorUtility.CIELabFromRGB(75f, 31f, 38f, 100f)
			},
			{
				"Portland Orange",
				ColorUtility.CIELabFromRGB(100f, 35f, 21f, 100f)
			},
			{
				"Powder Blue",
				ColorUtility.CIELabFromRGB(69f, 88f, 90f, 100f)
			},
			{
				"Princeton Orange",
				ColorUtility.CIELabFromRGB(96f, 50f, 15f, 100f)
			},
			{
				"Prune",
				ColorUtility.CIELabFromRGB(44f, 11f, 11f, 100f)
			},
			{
				"Prussian Blue",
				ColorUtility.CIELabFromRGB(0f, 19f, 33f, 100f)
			},
			{
				"Psychedelic Purple",
				ColorUtility.CIELabFromRGB(87f, 0f, 100f, 100f)
			},
			{
				"Puce",
				ColorUtility.CIELabFromRGB(80f, 53f, 60f, 100f)
			},
			{
				"Puce Red",
				ColorUtility.CIELabFromRGB(45f, 18f, 22f, 100f)
			},
			{
				"Pullman Brown (UPS Brown)",
				ColorUtility.CIELabFromRGB(39f, 25f, 9f, 100f)
			},
			{
				"Pullman Green",
				ColorUtility.CIELabFromRGB(23f, 20f, 11f, 100f)
			},
			{
				"Pumpkin",
				ColorUtility.CIELabFromRGB(100f, 46f, 9f, 100f)
			},
			{
				"Purple (HTML)",
				ColorUtility.CIELabFromRGB(50f, 0f, 50f, 100f)
			},
			{
				"Purple (Munsell)",
				ColorUtility.CIELabFromRGB(62f, 0f, 77f, 100f)
			},
			{
				"Purple (X11)",
				ColorUtility.CIELabFromRGB(63f, 13f, 94f, 100f)
			},
			{
				"Purple Heart",
				ColorUtility.CIELabFromRGB(41f, 21f, 61f, 100f)
			},
			{
				"Purple Mountain Majesty",
				ColorUtility.CIELabFromRGB(59f, 47f, 71f, 100f)
			},
			{
				"Purple Navy",
				ColorUtility.CIELabFromRGB(31f, 32f, 50f, 100f)
			},
			{
				"Purple Pizzazz",
				ColorUtility.CIELabFromRGB(100f, 31f, 85f, 100f)
			},
			{
				"Purple Taupe",
				ColorUtility.CIELabFromRGB(31f, 25f, 30f, 100f)
			},
			{
				"Purpureus",
				ColorUtility.CIELabFromRGB(60f, 31f, 68f, 100f)
			},
			{
				"Quartz",
				ColorUtility.CIELabFromRGB(32f, 28f, 31f, 100f)
			},
			{
				"Queen Blue",
				ColorUtility.CIELabFromRGB(26f, 42f, 58f, 100f)
			},
			{
				"Queen Pink",
				ColorUtility.CIELabFromRGB(91f, 80f, 84f, 100f)
			},
			{
				"Quinacridone Magenta",
				ColorUtility.CIELabFromRGB(56f, 23f, 35f, 100f)
			},
			{
				"Rackley",
				ColorUtility.CIELabFromRGB(36f, 54f, 66f, 100f)
			},
			{
				"Radical Red",
				ColorUtility.CIELabFromRGB(100f, 21f, 37f, 100f)
			},
			{
				"Rajah",
				ColorUtility.CIELabFromRGB(98f, 67f, 38f, 100f)
			},
			{
				"Raspberry",
				ColorUtility.CIELabFromRGB(89f, 4f, 36f, 100f)
			},
			{
				"Raspberry Glace",
				ColorUtility.CIELabFromRGB(57f, 37f, 43f, 100f)
			},
			{
				"Raspberry Pink",
				ColorUtility.CIELabFromRGB(89f, 31f, 60f, 100f)
			},
			{
				"Raspberry Rose",
				ColorUtility.CIELabFromRGB(70f, 27f, 42f, 100f)
			},
			{
				"Raw Umber",
				ColorUtility.CIELabFromRGB(51f, 40f, 27f, 100f)
			},
			{
				"Razzle Dazzle Rose",
				ColorUtility.CIELabFromRGB(100f, 20f, 80f, 100f)
			},
			{
				"Razzmatazz",
				ColorUtility.CIELabFromRGB(89f, 15f, 42f, 100f)
			},
			{
				"Razzmic Berry",
				ColorUtility.CIELabFromRGB(55f, 31f, 52f, 100f)
			},
			{
				"Rebecca Purple",
				ColorUtility.CIELabFromRGB(40f, 20f, 60f, 100f)
			},
			{
				"Red",
				ColorUtility.CIELabFromRGB(100f, 0f, 0f, 100f)
			},
			{
				"Red (Crayola)",
				ColorUtility.CIELabFromRGB(93f, 13f, 30f, 100f)
			},
			{
				"Red (Munsell)",
				ColorUtility.CIELabFromRGB(95f, 0f, 24f, 100f)
			},
			{
				"Red (NCS)",
				ColorUtility.CIELabFromRGB(77f, 1f, 20f, 100f)
			},
			{
				"Red (Pantone)",
				ColorUtility.CIELabFromRGB(93f, 16f, 22f, 100f)
			},
			{
				"Red (Pigment)",
				ColorUtility.CIELabFromRGB(93f, 11f, 14f, 100f)
			},
			{
				"Red (RYB)",
				ColorUtility.CIELabFromRGB(100f, 15f, 7f, 100f)
			},
			{
				"Red-Brown",
				ColorUtility.CIELabFromRGB(65f, 16f, 16f, 100f)
			},
			{
				"Red Devil",
				ColorUtility.CIELabFromRGB(53f, 0f, 7f, 100f)
			},
			{
				"Red-Orange",
				ColorUtility.CIELabFromRGB(100f, 33f, 29f, 100f)
			},
			{
				"Red-Purple",
				ColorUtility.CIELabFromRGB(89f, 0f, 47f, 100f)
			},
			{
				"Red-Violet",
				ColorUtility.CIELabFromRGB(78f, 8f, 52f, 100f)
			},
			{
				"Redwood",
				ColorUtility.CIELabFromRGB(64f, 35f, 32f, 100f)
			},
			{
				"Regalia",
				ColorUtility.CIELabFromRGB(32f, 18f, 50f, 100f)
			},
			{
				"Registration Black",
				ColorUtility.CIELabFromRGB(0f, 0f, 0f, 100f)
			},
			{
				"Resolution Blue",
				ColorUtility.CIELabFromRGB(0f, 14f, 53f, 100f)
			},
			{
				"Rhythm",
				ColorUtility.CIELabFromRGB(47f, 46f, 59f, 100f)
			},
			{
				"Rich Black",
				ColorUtility.CIELabFromRGB(0f, 25f, 25f, 100f)
			},
			{
				"Rich Black (FOGRA29)",
				ColorUtility.CIELabFromRGB(0f, 4f, 7f, 100f)
			},
			{
				"Rich Black (FOGRA39)",
				ColorUtility.CIELabFromRGB(0f, 1f, 1f, 100f)
			},
			{
				"Rich Brilliant Lavender",
				ColorUtility.CIELabFromRGB(95f, 65f, 100f, 100f)
			},
			{
				"Rich Carmine",
				ColorUtility.CIELabFromRGB(84f, 0f, 25f, 100f)
			},
			{
				"Rich Electric Blue",
				ColorUtility.CIELabFromRGB(3f, 57f, 82f, 100f)
			},
			{
				"Rich Lavender",
				ColorUtility.CIELabFromRGB(65f, 42f, 81f, 100f)
			},
			{
				"Rich Lilac",
				ColorUtility.CIELabFromRGB(71f, 40f, 82f, 100f)
			},
			{
				"Rich Maroon",
				ColorUtility.CIELabFromRGB(69f, 19f, 38f, 100f)
			},
			{
				"Rifle Green",
				ColorUtility.CIELabFromRGB(27f, 30f, 22f, 100f)
			},
			{
				"Roast Coffee",
				ColorUtility.CIELabFromRGB(44f, 26f, 25f, 100f)
			},
			{
				"Robin Egg Blue",
				ColorUtility.CIELabFromRGB(0f, 80f, 80f, 100f)
			},
			{
				"Rocket Metallic",
				ColorUtility.CIELabFromRGB(54f, 50f, 50f, 100f)
			},
			{
				"Roman Silver",
				ColorUtility.CIELabFromRGB(51f, 54f, 59f, 100f)
			},
			{
				"Rose",
				ColorUtility.CIELabFromRGB(100f, 0f, 50f, 100f)
			},
			{
				"Rose Bonbon",
				ColorUtility.CIELabFromRGB(98f, 26f, 62f, 100f)
			},
			{
				"Rose Ebony",
				ColorUtility.CIELabFromRGB(40f, 28f, 27f, 100f)
			},
			{
				"Rose Gold",
				ColorUtility.CIELabFromRGB(72f, 43f, 47f, 100f)
			},
			{
				"Rose Madder",
				ColorUtility.CIELabFromRGB(89f, 15f, 21f, 100f)
			},
			{
				"Rose Pink",
				ColorUtility.CIELabFromRGB(100f, 40f, 80f, 100f)
			},
			{
				"Rose Quartz",
				ColorUtility.CIELabFromRGB(67f, 60f, 66f, 100f)
			},
			{
				"Rose Red",
				ColorUtility.CIELabFromRGB(76f, 12f, 34f, 100f)
			},
			{
				"Rose Taupe",
				ColorUtility.CIELabFromRGB(56f, 36f, 36f, 100f)
			},
			{
				"Rose Vale",
				ColorUtility.CIELabFromRGB(67f, 31f, 32f, 100f)
			},
			{
				"Rosewood",
				ColorUtility.CIELabFromRGB(40f, 0f, 4f, 100f)
			},
			{
				"Rosso Corsa",
				ColorUtility.CIELabFromRGB(83f, 0f, 0f, 100f)
			},
			{
				"Rosy Brown",
				ColorUtility.CIELabFromRGB(74f, 56f, 56f, 100f)
			},
			{
				"Royal Azure",
				ColorUtility.CIELabFromRGB(0f, 22f, 66f, 100f)
			},
			{
				"Royal Blue",
				ColorUtility.CIELabFromRGB(0f, 14f, 40f, 100f)
			},
			{
				"Royal Blue 2",
				ColorUtility.CIELabFromRGB(25f, 41f, 88f, 100f)
			},
			{
				"Royal Fuchsia",
				ColorUtility.CIELabFromRGB(79f, 17f, 57f, 100f)
			},
			{
				"Royal Purple",
				ColorUtility.CIELabFromRGB(47f, 32f, 66f, 100f)
			},
			{
				"Royal Yellow",
				ColorUtility.CIELabFromRGB(98f, 85f, 37f, 100f)
			},
			{
				"Ruber",
				ColorUtility.CIELabFromRGB(81f, 27f, 46f, 100f)
			},
			{
				"Rubine Red",
				ColorUtility.CIELabFromRGB(82f, 0f, 34f, 100f)
			},
			{
				"Ruby",
				ColorUtility.CIELabFromRGB(88f, 7f, 37f, 100f)
			},
			{
				"Ruby Red",
				ColorUtility.CIELabFromRGB(61f, 7f, 12f, 100f)
			},
			{
				"Ruddy",
				ColorUtility.CIELabFromRGB(100f, 0f, 16f, 100f)
			},
			{
				"Ruddy Brown",
				ColorUtility.CIELabFromRGB(73f, 40f, 16f, 100f)
			},
			{
				"Ruddy Pink",
				ColorUtility.CIELabFromRGB(88f, 56f, 59f, 100f)
			},
			{
				"Rufous",
				ColorUtility.CIELabFromRGB(66f, 11f, 3f, 100f)
			},
			{
				"Russet",
				ColorUtility.CIELabFromRGB(50f, 27f, 11f, 100f)
			},
			{
				"Russian Green",
				ColorUtility.CIELabFromRGB(40f, 57f, 40f, 100f)
			},
			{
				"Russian Violet",
				ColorUtility.CIELabFromRGB(20f, 9f, 30f, 100f)
			},
			{
				"Rust",
				ColorUtility.CIELabFromRGB(72f, 25f, 5f, 100f)
			},
			{
				"Rusty Red",
				ColorUtility.CIELabFromRGB(85f, 17f, 26f, 100f)
			},
			{
				"Sacramento State Green",
				ColorUtility.CIELabFromRGB(0f, 34f, 25f, 100f)
			},
			{
				"Saddle Brown",
				ColorUtility.CIELabFromRGB(55f, 27f, 7f, 100f)
			},
			{
				"Safety Orange",
				ColorUtility.CIELabFromRGB(100f, 47f, 0f, 100f)
			},
			{
				"Safety Orange (Blaze Orange)",
				ColorUtility.CIELabFromRGB(100f, 40f, 0f, 100f)
			},
			{
				"Safety Yellow",
				ColorUtility.CIELabFromRGB(93f, 82f, 1f, 100f)
			},
			{
				"Saffron",
				ColorUtility.CIELabFromRGB(96f, 77f, 19f, 100f)
			},
			{
				"Sage",
				ColorUtility.CIELabFromRGB(74f, 72f, 54f, 100f)
			},
			{
				"St. Patrick's Blue",
				ColorUtility.CIELabFromRGB(14f, 16f, 48f, 100f)
			},
			{
				"Salmon",
				ColorUtility.CIELabFromRGB(98f, 50f, 45f, 100f)
			},
			{
				"Salmon Pink",
				ColorUtility.CIELabFromRGB(100f, 57f, 64f, 100f)
			},
			{
				"Sand",
				ColorUtility.CIELabFromRGB(76f, 70f, 50f, 100f)
			},
			{
				"Sand Dune",
				ColorUtility.CIELabFromRGB(59f, 44f, 9f, 100f)
			},
			{
				"Sandstorm",
				ColorUtility.CIELabFromRGB(93f, 84f, 25f, 100f)
			},
			{
				"Sandy Brown",
				ColorUtility.CIELabFromRGB(96f, 64f, 38f, 100f)
			},
			{
				"Sandy Taupe",
				ColorUtility.CIELabFromRGB(59f, 44f, 9f, 100f)
			},
			{
				"Sangria",
				ColorUtility.CIELabFromRGB(57f, 0f, 4f, 100f)
			},
			{
				"Sap Green",
				ColorUtility.CIELabFromRGB(31f, 49f, 16f, 100f)
			},
			{
				"Sapphire",
				ColorUtility.CIELabFromRGB(6f, 32f, 73f, 100f)
			},
			{
				"Sapphire Blue",
				ColorUtility.CIELabFromRGB(0f, 40f, 65f, 100f)
			},
			{
				"Satin Sheen Gold",
				ColorUtility.CIELabFromRGB(80f, 63f, 21f, 100f)
			},
			{
				"Scarlet",
				ColorUtility.CIELabFromRGB(100f, 14f, 0f, 100f)
			},
			{
				"Scarlet-ier",
				ColorUtility.CIELabFromRGB(99f, 5f, 21f, 100f)
			},
			{
				"Schauss Pink",
				ColorUtility.CIELabFromRGB(100f, 57f, 69f, 100f)
			},
			{
				"School Bus Yellow",
				ColorUtility.CIELabFromRGB(100f, 85f, 0f, 100f)
			},
			{
				"Screamin' Green",
				ColorUtility.CIELabFromRGB(46f, 100f, 48f, 100f)
			},
			{
				"Sea Blue",
				ColorUtility.CIELabFromRGB(0f, 41f, 58f, 100f)
			},
			{
				"Sea Green",
				ColorUtility.CIELabFromRGB(18f, 55f, 34f, 100f)
			},
			{
				"Seal Brown",
				ColorUtility.CIELabFromRGB(20f, 8f, 8f, 100f)
			},
			{
				"Seashell",
				ColorUtility.CIELabFromRGB(100f, 96f, 93f, 100f)
			},
			{
				"Selective Yellow",
				ColorUtility.CIELabFromRGB(100f, 73f, 0f, 100f)
			},
			{
				"Sepia",
				ColorUtility.CIELabFromRGB(44f, 26f, 8f, 100f)
			},
			{
				"Shadow",
				ColorUtility.CIELabFromRGB(54f, 47f, 36f, 100f)
			},
			{
				"Shadow Blue",
				ColorUtility.CIELabFromRGB(47f, 55f, 65f, 100f)
			},
			{
				"Shampoo",
				ColorUtility.CIELabFromRGB(100f, 81f, 95f, 100f)
			},
			{
				"Shamrock Green",
				ColorUtility.CIELabFromRGB(0f, 62f, 38f, 100f)
			},
			{
				"Sheen Green",
				ColorUtility.CIELabFromRGB(56f, 83f, 0f, 100f)
			},
			{
				"Shimmering Blush",
				ColorUtility.CIELabFromRGB(85f, 53f, 58f, 100f)
			},
			{
				"Shocking Pink",
				ColorUtility.CIELabFromRGB(99f, 6f, 75f, 100f)
			},
			{
				"Shocking Pink (Crayola)",
				ColorUtility.CIELabFromRGB(100f, 44f, 100f, 100f)
			},
			{
				"Sienna",
				ColorUtility.CIELabFromRGB(53f, 18f, 9f, 100f)
			},
			{
				"Silver",
				ColorUtility.CIELabFromRGB(75f, 75f, 75f, 100f)
			},
			{
				"Silver Chalice",
				ColorUtility.CIELabFromRGB(67f, 67f, 67f, 100f)
			},
			{
				"Silver Lake Blue",
				ColorUtility.CIELabFromRGB(36f, 54f, 73f, 100f)
			},
			{
				"Silver Pink",
				ColorUtility.CIELabFromRGB(77f, 68f, 68f, 100f)
			},
			{
				"Silver Sand",
				ColorUtility.CIELabFromRGB(75f, 76f, 76f, 100f)
			},
			{
				"Sinopia",
				ColorUtility.CIELabFromRGB(80f, 25f, 4f, 100f)
			},
			{
				"Skobeloff",
				ColorUtility.CIELabFromRGB(0f, 45f, 45f, 100f)
			},
			{
				"Sky Blue",
				ColorUtility.CIELabFromRGB(53f, 81f, 92f, 100f)
			},
			{
				"Sky Magenta",
				ColorUtility.CIELabFromRGB(81f, 44f, 69f, 100f)
			},
			{
				"Slate Blue",
				ColorUtility.CIELabFromRGB(42f, 35f, 80f, 100f)
			},
			{
				"Slate Gray",
				ColorUtility.CIELabFromRGB(44f, 50f, 56f, 100f)
			},
			{
				"Smalt (Dark Powder Blue)",
				ColorUtility.CIELabFromRGB(0f, 20f, 60f, 100f)
			},
			{
				"Smitten",
				ColorUtility.CIELabFromRGB(78f, 25f, 53f, 100f)
			},
			{
				"Smoke",
				ColorUtility.CIELabFromRGB(45f, 51f, 46f, 100f)
			},
			{
				"Smoky Black",
				ColorUtility.CIELabFromRGB(6f, 5f, 3f, 100f)
			},
			{
				"Smoky Topaz",
				ColorUtility.CIELabFromRGB(58f, 24f, 25f, 100f)
			},
			{
				"Snow",
				ColorUtility.CIELabFromRGB(100f, 98f, 98f, 100f)
			},
			{
				"Soap",
				ColorUtility.CIELabFromRGB(81f, 78f, 94f, 100f)
			},
			{
				"Solid Pink",
				ColorUtility.CIELabFromRGB(54f, 22f, 26f, 100f)
			},
			{
				"Sonic Silver",
				ColorUtility.CIELabFromRGB(46f, 46f, 46f, 100f)
			},
			{
				"Spartan Crimson",
				ColorUtility.CIELabFromRGB(62f, 7f, 9f, 100f)
			},
			{
				"Space Cadet",
				ColorUtility.CIELabFromRGB(11f, 16f, 32f, 100f)
			},
			{
				"Spanish Bistre",
				ColorUtility.CIELabFromRGB(50f, 46f, 20f, 100f)
			},
			{
				"Spanish Blue",
				ColorUtility.CIELabFromRGB(0f, 44f, 72f, 100f)
			},
			{
				"Spanish Carmine",
				ColorUtility.CIELabFromRGB(82f, 0f, 28f, 100f)
			},
			{
				"Spanish Crimson",
				ColorUtility.CIELabFromRGB(90f, 10f, 30f, 100f)
			},
			{
				"Spanish Gray",
				ColorUtility.CIELabFromRGB(60f, 60f, 60f, 100f)
			},
			{
				"Spanish Green",
				ColorUtility.CIELabFromRGB(0f, 57f, 31f, 100f)
			},
			{
				"Spanish Orange",
				ColorUtility.CIELabFromRGB(91f, 38f, 0f, 100f)
			},
			{
				"Spanish Pink",
				ColorUtility.CIELabFromRGB(97f, 75f, 75f, 100f)
			},
			{
				"Spanish Red",
				ColorUtility.CIELabFromRGB(90f, 0f, 15f, 100f)
			},
			{
				"Spanish Sky Blue",
				ColorUtility.CIELabFromRGB(0f, 100f, 100f, 100f)
			},
			{
				"Spanish Violet",
				ColorUtility.CIELabFromRGB(30f, 16f, 51f, 100f)
			},
			{
				"Spanish Viridian",
				ColorUtility.CIELabFromRGB(0f, 50f, 36f, 100f)
			},
			{
				"Spicy Mix",
				ColorUtility.CIELabFromRGB(55f, 37f, 30f, 100f)
			},
			{
				"Spiro Disco Ball",
				ColorUtility.CIELabFromRGB(6f, 75f, 99f, 100f)
			},
			{
				"Spring Bud",
				ColorUtility.CIELabFromRGB(65f, 99f, 0f, 100f)
			},
			{
				"Spring Green",
				ColorUtility.CIELabFromRGB(0f, 100f, 50f, 100f)
			},
			{
				"Star Command Blue",
				ColorUtility.CIELabFromRGB(0f, 48f, 72f, 100f)
			},
			{
				"Steel Blue",
				ColorUtility.CIELabFromRGB(27f, 51f, 71f, 100f)
			},
			{
				"Steel Pink",
				ColorUtility.CIELabFromRGB(80f, 20f, 80f, 100f)
			},
			{
				"Stil De Grain Yellow",
				ColorUtility.CIELabFromRGB(98f, 85f, 37f, 100f)
			},
			{
				"Stizza",
				ColorUtility.CIELabFromRGB(60f, 0f, 0f, 100f)
			},
			{
				"Stormcloud",
				ColorUtility.CIELabFromRGB(31f, 40f, 42f, 100f)
			},
			{
				"Thistle",
				ColorUtility.CIELabFromRGB(85f, 75f, 85f, 100f)
			},
			{
				"Straw",
				ColorUtility.CIELabFromRGB(89f, 85f, 44f, 100f)
			},
			{
				"Strawberry",
				ColorUtility.CIELabFromRGB(99f, 35f, 55f, 100f)
			},
			{
				"Sunglow",
				ColorUtility.CIELabFromRGB(100f, 80f, 20f, 100f)
			},
			{
				"Sunray",
				ColorUtility.CIELabFromRGB(89f, 67f, 34f, 100f)
			},
			{
				"Sunset",
				ColorUtility.CIELabFromRGB(98f, 84f, 65f, 100f)
			},
			{
				"Sunset Orange",
				ColorUtility.CIELabFromRGB(99f, 37f, 33f, 100f)
			},
			{
				"Super Pink",
				ColorUtility.CIELabFromRGB(81f, 42f, 66f, 100f)
			},
			{
				"Tan",
				ColorUtility.CIELabFromRGB(82f, 71f, 55f, 100f)
			},
			{
				"Tangelo",
				ColorUtility.CIELabFromRGB(98f, 30f, 0f, 100f)
			},
			{
				"Tangerine",
				ColorUtility.CIELabFromRGB(95f, 52f, 0f, 100f)
			},
			{
				"Tangerine Yellow",
				ColorUtility.CIELabFromRGB(100f, 80f, 0f, 100f)
			},
			{
				"Tango Pink",
				ColorUtility.CIELabFromRGB(89f, 44f, 48f, 100f)
			},
			{
				"Taupe",
				ColorUtility.CIELabFromRGB(28f, 24f, 20f, 100f)
			},
			{
				"Taupe Gray",
				ColorUtility.CIELabFromRGB(55f, 52f, 54f, 100f)
			},
			{
				"Tea Green",
				ColorUtility.CIELabFromRGB(82f, 94f, 75f, 100f)
			},
			{
				"Tea Rose",
				ColorUtility.CIELabFromRGB(97f, 51f, 47f, 100f)
			},
			{
				"Tea Rosier",
				ColorUtility.CIELabFromRGB(96f, 76f, 76f, 100f)
			},
			{
				"Teal",
				ColorUtility.CIELabFromRGB(0f, 50f, 50f, 100f)
			},
			{
				"Teal Blue",
				ColorUtility.CIELabFromRGB(21f, 46f, 53f, 100f)
			},
			{
				"Teal Deer",
				ColorUtility.CIELabFromRGB(60f, 90f, 70f, 100f)
			},
			{
				"Teal Green",
				ColorUtility.CIELabFromRGB(0f, 51f, 50f, 100f)
			},
			{
				"Telemagenta",
				ColorUtility.CIELabFromRGB(81f, 20f, 46f, 100f)
			},
			{
				"Tenné",
				ColorUtility.CIELabFromRGB(80f, 34f, 0f, 100f)
			},
			{
				"Terra Cotta",
				ColorUtility.CIELabFromRGB(89f, 45f, 36f, 100f)
			},
			{
				"Thulian Pink",
				ColorUtility.CIELabFromRGB(87f, 44f, 63f, 100f)
			},
			{
				"Tickle Me Pink",
				ColorUtility.CIELabFromRGB(99f, 54f, 67f, 100f)
			},
			{
				"Tiffany Blue",
				ColorUtility.CIELabFromRGB(4f, 73f, 71f, 100f)
			},
			{
				"Tiger's Eye",
				ColorUtility.CIELabFromRGB(88f, 55f, 24f, 100f)
			},
			{
				"Timberwolf",
				ColorUtility.CIELabFromRGB(86f, 84f, 82f, 100f)
			},
			{
				"Titanium Yellow",
				ColorUtility.CIELabFromRGB(93f, 90f, 0f, 100f)
			},
			{
				"Tomato",
				ColorUtility.CIELabFromRGB(100f, 39f, 28f, 100f)
			},
			{
				"Toolbox",
				ColorUtility.CIELabFromRGB(45f, 42f, 75f, 100f)
			},
			{
				"Topaz",
				ColorUtility.CIELabFromRGB(100f, 78f, 49f, 100f)
			},
			{
				"Tractor Red",
				ColorUtility.CIELabFromRGB(99f, 5f, 21f, 100f)
			},
			{
				"Trolley Grey",
				ColorUtility.CIELabFromRGB(50f, 50f, 50f, 100f)
			},
			{
				"Tropical Rain Forest",
				ColorUtility.CIELabFromRGB(0f, 46f, 37f, 100f)
			},
			{
				"True Blue",
				ColorUtility.CIELabFromRGB(0f, 45f, 81f, 100f)
			},
			{
				"Tufts Blue",
				ColorUtility.CIELabFromRGB(25f, 49f, 76f, 100f)
			},
			{
				"Tulip",
				ColorUtility.CIELabFromRGB(100f, 53f, 55f, 100f)
			},
			{
				"Tumbleweed",
				ColorUtility.CIELabFromRGB(87f, 67f, 53f, 100f)
			},
			{
				"Turkish Rose",
				ColorUtility.CIELabFromRGB(71f, 45f, 51f, 100f)
			},
			{
				"Turquoise",
				ColorUtility.CIELabFromRGB(25f, 88f, 82f, 100f)
			},
			{
				"Turquoise Blue",
				ColorUtility.CIELabFromRGB(0f, 100f, 94f, 100f)
			},
			{
				"Turquoise Green",
				ColorUtility.CIELabFromRGB(63f, 84f, 71f, 100f)
			},
			{
				"Tuscan",
				ColorUtility.CIELabFromRGB(98f, 84f, 65f, 100f)
			},
			{
				"Tuscan Brown",
				ColorUtility.CIELabFromRGB(44f, 31f, 22f, 100f)
			},
			{
				"Tuscan Red",
				ColorUtility.CIELabFromRGB(49f, 28f, 28f, 100f)
			},
			{
				"Tuscan Tan",
				ColorUtility.CIELabFromRGB(65f, 48f, 36f, 100f)
			},
			{
				"Tuscany",
				ColorUtility.CIELabFromRGB(75f, 60f, 60f, 100f)
			},
			{
				"Twilight Lavender",
				ColorUtility.CIELabFromRGB(54f, 29f, 42f, 100f)
			},
			{
				"Tyrian Purple",
				ColorUtility.CIELabFromRGB(40f, 1f, 24f, 100f)
			},
			{
				"UA Blue",
				ColorUtility.CIELabFromRGB(0f, 20f, 67f, 100f)
			},
			{
				"UA Red",
				ColorUtility.CIELabFromRGB(85f, 0f, 30f, 100f)
			},
			{
				"Ube",
				ColorUtility.CIELabFromRGB(53f, 47f, 76f, 100f)
			},
			{
				"UCLA Blue",
				ColorUtility.CIELabFromRGB(33f, 41f, 58f, 100f)
			},
			{
				"UCLA Gold",
				ColorUtility.CIELabFromRGB(100f, 70f, 0f, 100f)
			},
			{
				"UFO Green",
				ColorUtility.CIELabFromRGB(24f, 82f, 44f, 100f)
			},
			{
				"Ultramarine",
				ColorUtility.CIELabFromRGB(7f, 4f, 56f, 100f)
			},
			{
				"Ultramarine Blue",
				ColorUtility.CIELabFromRGB(25f, 40f, 96f, 100f)
			},
			{
				"Ultra Pink",
				ColorUtility.CIELabFromRGB(100f, 44f, 100f, 100f)
			},
			{
				"Ultra Red",
				ColorUtility.CIELabFromRGB(99f, 42f, 52f, 100f)
			},
			{
				"Umber",
				ColorUtility.CIELabFromRGB(39f, 32f, 28f, 100f)
			},
			{
				"Unbleached Silk",
				ColorUtility.CIELabFromRGB(100f, 87f, 79f, 100f)
			},
			{
				"United Nations Blue",
				ColorUtility.CIELabFromRGB(36f, 57f, 90f, 100f)
			},
			{
				"University Of California Gold",
				ColorUtility.CIELabFromRGB(72f, 53f, 15f, 100f)
			},
			{
				"Unmellow Yellow",
				ColorUtility.CIELabFromRGB(100f, 100f, 40f, 100f)
			},
			{
				"UP Forest Green",
				ColorUtility.CIELabFromRGB(0f, 27f, 13f, 100f)
			},
			{
				"UP Maroon",
				ColorUtility.CIELabFromRGB(48f, 7f, 7f, 100f)
			},
			{
				"Upsdell Red",
				ColorUtility.CIELabFromRGB(68f, 13f, 16f, 100f)
			},
			{
				"Urobilin",
				ColorUtility.CIELabFromRGB(88f, 68f, 13f, 100f)
			},
			{
				"USAFA Blue",
				ColorUtility.CIELabFromRGB(0f, 31f, 60f, 100f)
			},
			{
				"USC Cardinal",
				ColorUtility.CIELabFromRGB(60f, 0f, 0f, 100f)
			},
			{
				"USC Gold",
				ColorUtility.CIELabFromRGB(100f, 80f, 0f, 100f)
			},
			{
				"University Of Tennessee Orange",
				ColorUtility.CIELabFromRGB(97f, 50f, 0f, 100f)
			},
			{
				"Utah Crimson",
				ColorUtility.CIELabFromRGB(83f, 0f, 25f, 100f)
			},
			{
				"Vanilla",
				ColorUtility.CIELabFromRGB(95f, 90f, 67f, 100f)
			},
			{
				"Vanilla Ice",
				ColorUtility.CIELabFromRGB(95f, 56f, 66f, 100f)
			},
			{
				"Vegas Gold",
				ColorUtility.CIELabFromRGB(77f, 70f, 35f, 100f)
			},
			{
				"Venetian Red",
				ColorUtility.CIELabFromRGB(78f, 3f, 8f, 100f)
			},
			{
				"Verdigris",
				ColorUtility.CIELabFromRGB(26f, 70f, 68f, 100f)
			},
			{
				"Vermilion",
				ColorUtility.CIELabFromRGB(89f, 26f, 20f, 100f)
			},
			{
				"Vermilion 2",
				ColorUtility.CIELabFromRGB(85f, 22f, 12f, 100f)
			},
			{
				"Veronica",
				ColorUtility.CIELabFromRGB(63f, 13f, 94f, 100f)
			},
			{
				"Very Light Azure",
				ColorUtility.CIELabFromRGB(45f, 73f, 98f, 100f)
			},
			{
				"Very Light Blue",
				ColorUtility.CIELabFromRGB(40f, 40f, 100f, 100f)
			},
			{
				"Very Light Malachite Green",
				ColorUtility.CIELabFromRGB(39f, 91f, 53f, 100f)
			},
			{
				"Very Light Tangelo",
				ColorUtility.CIELabFromRGB(100f, 69f, 47f, 100f)
			},
			{
				"Very Pale Orange",
				ColorUtility.CIELabFromRGB(100f, 87f, 75f, 100f)
			},
			{
				"Very Pale Yellow",
				ColorUtility.CIELabFromRGB(100f, 100f, 75f, 100f)
			},
			{
				"Violet",
				ColorUtility.CIELabFromRGB(56f, 0f, 100f, 100f)
			},
			{
				"Violet (Color Wheel)",
				ColorUtility.CIELabFromRGB(50f, 0f, 100f, 100f)
			},
			{
				"Violet (RYB)",
				ColorUtility.CIELabFromRGB(53f, 0f, 69f, 100f)
			},
			{
				"Violet (Web)",
				ColorUtility.CIELabFromRGB(93f, 51f, 93f, 100f)
			},
			{
				"Violet-Blue",
				ColorUtility.CIELabFromRGB(20f, 29f, 70f, 100f)
			},
			{
				"Violet-Red",
				ColorUtility.CIELabFromRGB(97f, 33f, 58f, 100f)
			},
			{
				"Viridian",
				ColorUtility.CIELabFromRGB(25f, 51f, 43f, 100f)
			},
			{
				"Viridian Green",
				ColorUtility.CIELabFromRGB(0f, 59f, 60f, 100f)
			},
			{
				"Vista Blue",
				ColorUtility.CIELabFromRGB(49f, 62f, 85f, 100f)
			},
			{
				"Vivid Amber",
				ColorUtility.CIELabFromRGB(80f, 60f, 0f, 100f)
			},
			{
				"Vivid Auburn",
				ColorUtility.CIELabFromRGB(57f, 15f, 14f, 100f)
			},
			{
				"Vivid Burgundy",
				ColorUtility.CIELabFromRGB(62f, 11f, 21f, 100f)
			},
			{
				"Vivid Cerise",
				ColorUtility.CIELabFromRGB(85f, 11f, 51f, 100f)
			},
			{
				"Vivid Cerulean",
				ColorUtility.CIELabFromRGB(0f, 67f, 93f, 100f)
			},
			{
				"Vivid Crimson",
				ColorUtility.CIELabFromRGB(80f, 0f, 20f, 100f)
			},
			{
				"Vivid Gamboge",
				ColorUtility.CIELabFromRGB(100f, 60f, 0f, 100f)
			},
			{
				"Vivid Lime Green",
				ColorUtility.CIELabFromRGB(65f, 84f, 3f, 100f)
			},
			{
				"Vivid Malachite",
				ColorUtility.CIELabFromRGB(0f, 80f, 20f, 100f)
			},
			{
				"Vivid Mulberry",
				ColorUtility.CIELabFromRGB(72f, 5f, 89f, 100f)
			},
			{
				"Vivid Orange",
				ColorUtility.CIELabFromRGB(100f, 37f, 0f, 100f)
			},
			{
				"Vivid Orange Peel",
				ColorUtility.CIELabFromRGB(100f, 63f, 0f, 100f)
			},
			{
				"Vivid Orchid",
				ColorUtility.CIELabFromRGB(80f, 0f, 100f, 100f)
			},
			{
				"Vivid Raspberry",
				ColorUtility.CIELabFromRGB(100f, 0f, 42f, 100f)
			},
			{
				"Vivid Red",
				ColorUtility.CIELabFromRGB(97f, 5f, 10f, 100f)
			},
			{
				"Vivid Red-Tangelo",
				ColorUtility.CIELabFromRGB(87f, 38f, 14f, 100f)
			},
			{
				"Vivid Sky Blue",
				ColorUtility.CIELabFromRGB(0f, 80f, 100f, 100f)
			},
			{
				"Vivid Tangelo",
				ColorUtility.CIELabFromRGB(94f, 45f, 15f, 100f)
			},
			{
				"Vivid Tangerine",
				ColorUtility.CIELabFromRGB(100f, 63f, 54f, 100f)
			},
			{
				"Vivid Vermilion",
				ColorUtility.CIELabFromRGB(90f, 38f, 14f, 100f)
			},
			{
				"Vivid Violet",
				ColorUtility.CIELabFromRGB(62f, 0f, 100f, 100f)
			},
			{
				"Vivid Yellow",
				ColorUtility.CIELabFromRGB(100f, 89f, 1f, 100f)
			},
			{
				"Warm Black",
				ColorUtility.CIELabFromRGB(0f, 26f, 26f, 100f)
			},
			{
				"Waterspout",
				ColorUtility.CIELabFromRGB(64f, 96f, 98f, 100f)
			},
			{
				"Wenge",
				ColorUtility.CIELabFromRGB(39f, 33f, 32f, 100f)
			},
			{
				"Wheat",
				ColorUtility.CIELabFromRGB(96f, 87f, 70f, 100f)
			},
			{
				"White",
				ColorUtility.CIELabFromRGB(100f, 100f, 100f, 100f)
			},
			{
				"White Smoke",
				ColorUtility.CIELabFromRGB(96f, 96f, 96f, 100f)
			},
			{
				"Wild Blue Yonder",
				ColorUtility.CIELabFromRGB(64f, 68f, 82f, 100f)
			},
			{
				"Wild Orchid",
				ColorUtility.CIELabFromRGB(83f, 44f, 64f, 100f)
			},
			{
				"Wild Strawberry",
				ColorUtility.CIELabFromRGB(100f, 26f, 64f, 100f)
			},
			{
				"Wild Watermelon",
				ColorUtility.CIELabFromRGB(99f, 42f, 52f, 100f)
			},
			{
				"Willpower Orange",
				ColorUtility.CIELabFromRGB(99f, 35f, 0f, 100f)
			},
			{
				"Windsor Tan",
				ColorUtility.CIELabFromRGB(65f, 33f, 1f, 100f)
			},
			{
				"Wine",
				ColorUtility.CIELabFromRGB(45f, 18f, 22f, 100f)
			},
			{
				"Wine Dregs",
				ColorUtility.CIELabFromRGB(40f, 19f, 28f, 100f)
			},
			{
				"Wisteria",
				ColorUtility.CIELabFromRGB(79f, 63f, 86f, 100f)
			},
			{
				"Wood Brown",
				ColorUtility.CIELabFromRGB(76f, 60f, 42f, 100f)
			},
			{
				"Xanadu",
				ColorUtility.CIELabFromRGB(45f, 53f, 47f, 100f)
			},
			{
				"Yale Blue",
				ColorUtility.CIELabFromRGB(6f, 30f, 57f, 100f)
			},
			{
				"Yankees Blue",
				ColorUtility.CIELabFromRGB(11f, 16f, 25f, 100f)
			},
			{
				"Yellow",
				ColorUtility.CIELabFromRGB(100f, 100f, 0f, 100f)
			},
			{
				"Yellow (Crayola)",
				ColorUtility.CIELabFromRGB(99f, 91f, 51f, 100f)
			},
			{
				"Yellow (Munsell)",
				ColorUtility.CIELabFromRGB(94f, 80f, 0f, 100f)
			},
			{
				"Yellow (NCS)",
				ColorUtility.CIELabFromRGB(100f, 83f, 0f, 100f)
			},
			{
				"Yellow (Pantone)",
				ColorUtility.CIELabFromRGB(100f, 87f, 0f, 100f)
			},
			{
				"Yellow (Process)",
				ColorUtility.CIELabFromRGB(100f, 94f, 0f, 100f)
			},
			{
				"Yellow (RYB)",
				ColorUtility.CIELabFromRGB(100f, 100f, 20f, 100f)
			},
			{
				"Yellow-Green",
				ColorUtility.CIELabFromRGB(60f, 80f, 20f, 100f)
			},
			{
				"Yellow Orange",
				ColorUtility.CIELabFromRGB(100f, 68f, 26f, 100f)
			},
			{
				"Yellow Rose",
				ColorUtility.CIELabFromRGB(100f, 94f, 0f, 100f)
			},
			{
				"Zaffre",
				ColorUtility.CIELabFromRGB(0f, 8f, 66f, 100f)
			},
			{
				"Zinnwaldite Brown",
				ColorUtility.CIELabFromRGB(17f, 9f, 3f, 100f)
			},
			{
				"Zomp",
				ColorUtility.CIELabFromRGB(22f, 65f, 56f, 100f)
			}
		};
	}
}

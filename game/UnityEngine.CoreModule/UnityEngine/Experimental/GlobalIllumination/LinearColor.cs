using System;
using UnityEngine.Rendering;

namespace UnityEngine.Experimental.GlobalIllumination
{
	// Token: 0x0200045A RID: 1114
	public struct LinearColor
	{
		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x060027C1 RID: 10177 RVA: 0x00041880 File Offset: 0x0003FA80
		// (set) Token: 0x060027C2 RID: 10178 RVA: 0x00041898 File Offset: 0x0003FA98
		public float red
		{
			get
			{
				return this.m_red;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Red color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_red = value;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x060027C3 RID: 10179 RVA: 0x000418E0 File Offset: 0x0003FAE0
		// (set) Token: 0x060027C4 RID: 10180 RVA: 0x000418F8 File Offset: 0x0003FAF8
		public float green
		{
			get
			{
				return this.m_green;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Green color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_green = value;
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x060027C5 RID: 10181 RVA: 0x00041940 File Offset: 0x0003FB40
		// (set) Token: 0x060027C6 RID: 10182 RVA: 0x00041958 File Offset: 0x0003FB58
		public float blue
		{
			get
			{
				return this.m_blue;
			}
			set
			{
				bool flag = value < 0f || value > 1f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Blue color (" + value.ToString() + ") must be in range [0;1].");
				}
				this.m_blue = value;
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x060027C7 RID: 10183 RVA: 0x000419A0 File Offset: 0x0003FBA0
		// (set) Token: 0x060027C8 RID: 10184 RVA: 0x000419B8 File Offset: 0x0003FBB8
		public float intensity
		{
			get
			{
				return this.m_intensity;
			}
			set
			{
				bool flag = value < 0f;
				if (flag)
				{
					throw new ArgumentOutOfRangeException("Intensity (" + value.ToString() + ") must be positive.");
				}
				this.m_intensity = value;
			}
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x000419F8 File Offset: 0x0003FBF8
		public static LinearColor Convert(Color color, float intensity)
		{
			Color color2 = GraphicsSettings.lightsUseLinearIntensity ? color.linear.RGBMultiplied(intensity) : color.RGBMultiplied(intensity).linear;
			float maxColorComponent = color2.maxColorComponent;
			bool flag = color2.r < 0f || color2.g < 0f || color2.b < 0f;
			if (flag)
			{
				throw new ArgumentOutOfRangeException(string.Concat(new string[]
				{
					"The input color to be converted must not contain negative values (red: ",
					color2.r.ToString(),
					", green: ",
					color2.g.ToString(),
					", blue: ",
					color2.b.ToString(),
					")."
				}));
			}
			bool flag2 = maxColorComponent <= 1E-20f;
			LinearColor result;
			if (flag2)
			{
				result = LinearColor.Black();
			}
			else
			{
				float num = 1f / color2.maxColorComponent;
				LinearColor linearColor;
				linearColor.m_red = color2.r * num;
				linearColor.m_green = color2.g * num;
				linearColor.m_blue = color2.b * num;
				linearColor.m_intensity = maxColorComponent;
				result = linearColor;
			}
			return result;
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x00041B2C File Offset: 0x0003FD2C
		public static LinearColor Black()
		{
			LinearColor result;
			result.m_red = (result.m_green = (result.m_blue = (result.m_intensity = 0f)));
			return result;
		}

		// Token: 0x04000E62 RID: 3682
		private float m_red;

		// Token: 0x04000E63 RID: 3683
		private float m_green;

		// Token: 0x04000E64 RID: 3684
		private float m_blue;

		// Token: 0x04000E65 RID: 3685
		private float m_intensity;
	}
}

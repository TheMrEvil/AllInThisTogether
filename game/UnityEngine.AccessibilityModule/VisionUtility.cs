using System;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine.Accessibility
{
	// Token: 0x02000002 RID: 2
	[UsedByNativeCode]
	public static class VisionUtility
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		internal static float ComputePerceivedLuminance(Color color)
		{
			color = color.linear;
			return Mathf.LinearToGammaSpace(0.2126f * color.r + 0.7152f * color.g + 0.0722f * color.b);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002098 File Offset: 0x00000298
		internal static void GetLuminanceValuesForPalette(Color[] palette, ref float[] outLuminanceValues)
		{
			Debug.Assert(palette != null && outLuminanceValues != null, "Passed in arrays can't be null.");
			Debug.Assert(palette.Length == outLuminanceValues.Length, "Passed in arrays need to be of the same length.");
			for (int i = 0; i < palette.Length; i++)
			{
				outLuminanceValues[i] = VisionUtility.ComputePerceivedLuminance(palette[i]);
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F4 File Offset: 0x000002F4
		public unsafe static int GetColorBlindSafePalette(Color[] palette, float minimumLuminance, float maximumLuminance)
		{
			bool flag = palette == null;
			if (flag)
			{
				throw new ArgumentNullException("palette");
			}
			Color* palette2;
			if (palette == null || palette.Length == 0)
			{
				palette2 = null;
			}
			else
			{
				palette2 = &palette[0];
			}
			return VisionUtility.GetColorBlindSafePaletteInternal((void*)palette2, palette.Length, minimumLuminance, maximumLuminance, false);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002140 File Offset: 0x00000340
		internal unsafe static int GetColorBlindSafePalette(Color32[] palette, float minimumLuminance, float maximumLuminance)
		{
			bool flag = palette == null;
			if (flag)
			{
				throw new ArgumentNullException("palette");
			}
			Color32* palette2;
			if (palette == null || palette.Length == 0)
			{
				palette2 = null;
			}
			else
			{
				palette2 = &palette[0];
			}
			return VisionUtility.GetColorBlindSafePaletteInternal((void*)palette2, palette.Length, minimumLuminance, maximumLuminance, true);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000218C File Offset: 0x0000038C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static int GetColorBlindSafePaletteInternal(void* palette, int paletteLength, float minimumLuminance, float maximumLuminance, bool useColor32)
		{
			bool flag = palette == null;
			if (flag)
			{
				throw new ArgumentNullException("palette");
			}
			Color[] array = (from i in Enumerable.Range(0, VisionUtility.s_ColorBlindSafePalette.Length)
			where VisionUtility.s_ColorBlindSafePaletteLuminanceValues[i] >= minimumLuminance && VisionUtility.s_ColorBlindSafePaletteLuminanceValues[i] <= maximumLuminance
			select VisionUtility.s_ColorBlindSafePalette[i]).ToArray<Color>();
			int num = Mathf.Min(paletteLength, array.Length);
			bool flag2 = num > 0;
			if (flag2)
			{
				for (int k = 0; k < paletteLength; k++)
				{
					if (useColor32)
					{
						*(Color32*)((byte*)palette + (IntPtr)k * (IntPtr)sizeof(Color32)) = array[k % num];
					}
					else
					{
						*(Color*)((byte*)palette + (IntPtr)k * (IntPtr)sizeof(Color)) = array[k % num];
					}
				}
			}
			else
			{
				for (int j = 0; j < paletteLength; j++)
				{
					if (useColor32)
					{
						*(Color32*)((byte*)palette + (IntPtr)j * (IntPtr)sizeof(Color32)) = default(Color32);
					}
					else
					{
						*(Color*)((byte*)palette + (IntPtr)j * (IntPtr)sizeof(Color)) = default(Color);
					}
				}
			}
			return num;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000022D8 File Offset: 0x000004D8
		// Note: this type is marked as 'beforefieldinit'.
		static VisionUtility()
		{
		}

		// Token: 0x04000001 RID: 1
		private static readonly Color[] s_ColorBlindSafePalette = new Color[]
		{
			new Color32(0, 0, 0, byte.MaxValue),
			new Color32(73, 0, 146, byte.MaxValue),
			new Color32(7, 71, 81, byte.MaxValue),
			new Color32(0, 146, 146, byte.MaxValue),
			new Color32(182, 109, byte.MaxValue, byte.MaxValue),
			new Color32(byte.MaxValue, 109, 182, byte.MaxValue),
			new Color32(109, 182, byte.MaxValue, byte.MaxValue),
			new Color32(36, byte.MaxValue, 36, byte.MaxValue),
			new Color32(byte.MaxValue, 182, 219, byte.MaxValue),
			new Color32(182, 219, byte.MaxValue, byte.MaxValue),
			new Color32(byte.MaxValue, byte.MaxValue, 109, byte.MaxValue),
			new Color32(30, 92, 92, byte.MaxValue),
			new Color32(74, 154, 87, byte.MaxValue),
			new Color32(113, 66, 183, byte.MaxValue),
			new Color32(162, 66, 183, byte.MaxValue),
			new Color32(178, 92, 25, byte.MaxValue),
			new Color32(100, 100, 100, byte.MaxValue),
			new Color32(80, 203, 181, byte.MaxValue),
			new Color32(82, 205, 242, byte.MaxValue)
		};

		// Token: 0x04000002 RID: 2
		private static readonly float[] s_ColorBlindSafePaletteLuminanceValues = (from c in VisionUtility.s_ColorBlindSafePalette
		select VisionUtility.ComputePerceivedLuminance(c)).ToArray<float>();

		// Token: 0x02000003 RID: 3
		[CompilerGenerated]
		private sealed class <>c__DisplayClass6_0
		{
			// Token: 0x06000007 RID: 7 RVA: 0x0000257E File Offset: 0x0000077E
			public <>c__DisplayClass6_0()
			{
			}

			// Token: 0x06000008 RID: 8 RVA: 0x00002587 File Offset: 0x00000787
			internal bool <GetColorBlindSafePaletteInternal>b__0(int i)
			{
				return VisionUtility.s_ColorBlindSafePaletteLuminanceValues[i] >= this.minimumLuminance && VisionUtility.s_ColorBlindSafePaletteLuminanceValues[i] <= this.maximumLuminance;
			}

			// Token: 0x04000003 RID: 3
			public float minimumLuminance;

			// Token: 0x04000004 RID: 4
			public float maximumLuminance;
		}

		// Token: 0x02000004 RID: 4
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x06000009 RID: 9 RVA: 0x000025AD File Offset: 0x000007AD
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x0600000A RID: 10 RVA: 0x0000257E File Offset: 0x0000077E
			public <>c()
			{
			}

			// Token: 0x0600000B RID: 11 RVA: 0x000025B9 File Offset: 0x000007B9
			internal Color <GetColorBlindSafePaletteInternal>b__6_1(int i)
			{
				return VisionUtility.s_ColorBlindSafePalette[i];
			}

			// Token: 0x0600000C RID: 12 RVA: 0x000025C6 File Offset: 0x000007C6
			internal float <.cctor>b__7_0(Color c)
			{
				return VisionUtility.ComputePerceivedLuminance(c);
			}

			// Token: 0x04000005 RID: 5
			public static readonly VisionUtility.<>c <>9 = new VisionUtility.<>c();

			// Token: 0x04000006 RID: 6
			public static Func<int, Color> <>9__6_1;
		}
	}
}

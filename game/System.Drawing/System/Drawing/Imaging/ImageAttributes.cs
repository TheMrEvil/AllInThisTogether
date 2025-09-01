using System;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Contains information about how bitmap and metafile colors are manipulated during rendering.</summary>
	// Token: 0x02000107 RID: 263
	[StructLayout(LayoutKind.Sequential)]
	public sealed class ImageAttributes : ICloneable, IDisposable
	{
		// Token: 0x06000C7C RID: 3196 RVA: 0x0001D270 File Offset: 0x0001B470
		internal void SetNativeImageAttributes(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			this.nativeImageAttributes = handle;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Drawing.Imaging.ImageAttributes" /> class.</summary>
		// Token: 0x06000C7D RID: 3197 RVA: 0x0001D294 File Offset: 0x0001B494
		public ImageAttributes()
		{
			IntPtr zero = IntPtr.Zero;
			int num = GDIPlus.GdipCreateImageAttributes(out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			this.SetNativeImageAttributes(zero);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0001D2C6 File Offset: 0x0001B4C6
		internal ImageAttributes(IntPtr newNativeImageAttributes)
		{
			this.SetNativeImageAttributes(newNativeImageAttributes);
		}

		/// <summary>Releases all resources used by this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		// Token: 0x06000C7F RID: 3199 RVA: 0x0001D2D5 File Offset: 0x0001B4D5
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0001D2E4 File Offset: 0x0001B4E4
		private void Dispose(bool disposing)
		{
			if (this.nativeImageAttributes != IntPtr.Zero)
			{
				try
				{
					GDIPlus.GdipDisposeImageAttributes(new HandleRef(this, this.nativeImageAttributes));
				}
				catch (Exception ex)
				{
					if (ClientUtils.IsSecurityOrCriticalException(ex))
					{
						throw;
					}
				}
				finally
				{
					this.nativeImageAttributes = IntPtr.Zero;
				}
			}
		}

		/// <summary>Allows an object to try to free resources and perform other cleanup operations before it is reclaimed by garbage collection.</summary>
		// Token: 0x06000C81 RID: 3201 RVA: 0x0001D34C File Offset: 0x0001B54C
		~ImageAttributes()
		{
			this.Dispose(false);
		}

		/// <summary>Creates an exact copy of this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		/// <returns>The <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object this class creates, cast as an object.</returns>
		// Token: 0x06000C82 RID: 3202 RVA: 0x0001D37C File Offset: 0x0001B57C
		public object Clone()
		{
			IntPtr zero = IntPtr.Zero;
			int num = GDIPlus.GdipCloneImageAttributes(new HandleRef(this, this.nativeImageAttributes), out zero);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			return new ImageAttributes(zero);
		}

		/// <summary>Sets the color-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		// Token: 0x06000C83 RID: 3203 RVA: 0x0001D3B3 File Offset: 0x0001B5B3
		public void SetColorMatrix(ColorMatrix newColorMatrix)
		{
			this.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment matrix.</param>
		// Token: 0x06000C84 RID: 3204 RVA: 0x0001D3BE File Offset: 0x0001B5BE
		public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag flags)
		{
			this.SetColorMatrix(newColorMatrix, flags, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix for a specified category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment matrix.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment matrix is set.</param>
		// Token: 0x06000C85 RID: 3205 RVA: 0x0001D3CC File Offset: 0x0001B5CC
		public void SetColorMatrix(ColorMatrix newColorMatrix, ColorMatrixFlag mode, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesColorMatrix(new HandleRef(this, this.nativeImageAttributes), type, true, newColorMatrix, null, mode);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the color-adjustment matrix for the default category.</summary>
		// Token: 0x06000C86 RID: 3206 RVA: 0x0001D3FA File Offset: 0x0001B5FA
		public void ClearColorMatrix()
		{
			this.ClearColorMatrix(ColorAdjustType.Default);
		}

		/// <summary>Clears the color-adjustment matrix for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment matrix is cleared.</param>
		// Token: 0x06000C87 RID: 3207 RVA: 0x0001D404 File Offset: 0x0001B604
		public void ClearColorMatrix(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesColorMatrix(new HandleRef(this, this.nativeImageAttributes), type, false, null, null, ColorMatrixFlag.Default);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		/// <param name="grayMatrix">The grayscale-adjustment matrix.</param>
		// Token: 0x06000C88 RID: 3208 RVA: 0x0001D432 File Offset: 0x0001B632
		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix)
		{
			this.SetColorMatrices(newColorMatrix, grayMatrix, ColorMatrixFlag.Default, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for the default category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		/// <param name="grayMatrix">The grayscale-adjustment matrix.</param>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment and grayscale-adjustment matrices.</param>
		// Token: 0x06000C89 RID: 3209 RVA: 0x0001D43E File Offset: 0x0001B63E
		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag flags)
		{
			this.SetColorMatrices(newColorMatrix, grayMatrix, flags, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-adjustment matrix and the grayscale-adjustment matrix for a specified category.</summary>
		/// <param name="newColorMatrix">The color-adjustment matrix.</param>
		/// <param name="grayMatrix">The grayscale-adjustment matrix.</param>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Imaging.ColorMatrixFlag" /> that specifies the type of image and color that will be affected by the color-adjustment and grayscale-adjustment matrices.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-adjustment and grayscale-adjustment matrices are set.</param>
		// Token: 0x06000C8A RID: 3210 RVA: 0x0001D44C File Offset: 0x0001B64C
		public void SetColorMatrices(ColorMatrix newColorMatrix, ColorMatrix grayMatrix, ColorMatrixFlag mode, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesColorMatrix(new HandleRef(this, this.nativeImageAttributes), type, true, newColorMatrix, grayMatrix, mode);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the threshold (transparency range) for the default category.</summary>
		/// <param name="threshold">A real number that specifies the threshold value.</param>
		// Token: 0x06000C8B RID: 3211 RVA: 0x0001D47B File Offset: 0x0001B67B
		public void SetThreshold(float threshold)
		{
			this.SetThreshold(threshold, ColorAdjustType.Default);
		}

		/// <summary>Sets the threshold (transparency range) for a specified category.</summary>
		/// <param name="threshold">A threshold value from 0.0 to 1.0 that is used as a breakpoint to sort colors that will be mapped to either a maximum or a minimum value.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color threshold is set.</param>
		// Token: 0x06000C8C RID: 3212 RVA: 0x0001D488 File Offset: 0x0001B688
		public void SetThreshold(float threshold, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesThreshold(new HandleRef(this, this.nativeImageAttributes), type, true, threshold);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the threshold value for the default category.</summary>
		// Token: 0x06000C8D RID: 3213 RVA: 0x0001D4B4 File Offset: 0x0001B6B4
		public void ClearThreshold()
		{
			this.ClearThreshold(ColorAdjustType.Default);
		}

		/// <summary>Clears the threshold value for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the threshold is cleared.</param>
		// Token: 0x06000C8E RID: 3214 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		public void ClearThreshold(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesThreshold(new HandleRef(this, this.nativeImageAttributes), type, false, 0f);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the gamma value for the default category.</summary>
		/// <param name="gamma">The gamma correction value.</param>
		// Token: 0x06000C8F RID: 3215 RVA: 0x0001D4F0 File Offset: 0x0001B6F0
		public void SetGamma(float gamma)
		{
			this.SetGamma(gamma, ColorAdjustType.Default);
		}

		/// <summary>Sets the gamma value for a specified category.</summary>
		/// <param name="gamma">The gamma correction value.</param>
		/// <param name="type">An element of the <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> enumeration that specifies the category for which the gamma value is set.</param>
		// Token: 0x06000C90 RID: 3216 RVA: 0x0001D4FC File Offset: 0x0001B6FC
		public void SetGamma(float gamma, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesGamma(new HandleRef(this, this.nativeImageAttributes), type, true, gamma);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Disables gamma correction for the default category.</summary>
		// Token: 0x06000C91 RID: 3217 RVA: 0x0001D528 File Offset: 0x0001B728
		public void ClearGamma()
		{
			this.ClearGamma(ColorAdjustType.Default);
		}

		/// <summary>Disables gamma correction for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which gamma correction is disabled.</param>
		// Token: 0x06000C92 RID: 3218 RVA: 0x0001D534 File Offset: 0x0001B734
		public void ClearGamma(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesGamma(new HandleRef(this, this.nativeImageAttributes), type, false, 0f);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Turns off color adjustment for the default category. You can call the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.ClearNoOp" /> method to reinstate the color-adjustment settings that were in place before the call to the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetNoOp" /> method.</summary>
		// Token: 0x06000C93 RID: 3219 RVA: 0x0001D564 File Offset: 0x0001B764
		public void SetNoOp()
		{
			this.SetNoOp(ColorAdjustType.Default);
		}

		/// <summary>Turns off color adjustment for a specified category. You can call the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.ClearNoOp" /> method to reinstate the color-adjustment settings that were in place before the call to the <see cref="Overload:System.Drawing.Imaging.ImageAttributes.SetNoOp" /> method.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which color correction is turned off.</param>
		// Token: 0x06000C94 RID: 3220 RVA: 0x0001D570 File Offset: 0x0001B770
		public void SetNoOp(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesNoOp(new HandleRef(this, this.nativeImageAttributes), type, true);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the <see langword="NoOp" /> setting for the default category.</summary>
		// Token: 0x06000C95 RID: 3221 RVA: 0x0001D59B File Offset: 0x0001B79B
		public void ClearNoOp()
		{
			this.ClearNoOp(ColorAdjustType.Default);
		}

		/// <summary>Clears the <see langword="NoOp" /> setting for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the <see langword="NoOp" /> setting is cleared.</param>
		// Token: 0x06000C96 RID: 3222 RVA: 0x0001D5A4 File Offset: 0x0001B7A4
		public void ClearNoOp(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesNoOp(new HandleRef(this, this.nativeImageAttributes), type, false);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color key for the default category.</summary>
		/// <param name="colorLow">The low color-key value.</param>
		/// <param name="colorHigh">The high color-key value.</param>
		// Token: 0x06000C97 RID: 3223 RVA: 0x0001D5CF File Offset: 0x0001B7CF
		public void SetColorKey(Color colorLow, Color colorHigh)
		{
			this.SetColorKey(colorLow, colorHigh, ColorAdjustType.Default);
		}

		/// <summary>Sets the color key (transparency range) for a specified category.</summary>
		/// <param name="colorLow">The low color-key value.</param>
		/// <param name="colorHigh">The high color-key value.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color key is set.</param>
		// Token: 0x06000C98 RID: 3224 RVA: 0x0001D5DC File Offset: 0x0001B7DC
		public void SetColorKey(Color colorLow, Color colorHigh, ColorAdjustType type)
		{
			int colorLow2 = colorLow.ToArgb();
			int colorHigh2 = colorHigh.ToArgb();
			int num = GDIPlus.GdipSetImageAttributesColorKeys(new HandleRef(this, this.nativeImageAttributes), type, true, colorLow2, colorHigh2);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the color key (transparency range) for the default category.</summary>
		// Token: 0x06000C99 RID: 3225 RVA: 0x0001D619 File Offset: 0x0001B819
		public void ClearColorKey()
		{
			this.ClearColorKey(ColorAdjustType.Default);
		}

		/// <summary>Clears the color key (transparency range) for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color key is cleared.</param>
		// Token: 0x06000C9A RID: 3226 RVA: 0x0001D624 File Offset: 0x0001B824
		public void ClearColorKey(ColorAdjustType type)
		{
			int num = 0;
			int num2 = GDIPlus.GdipSetImageAttributesColorKeys(new HandleRef(this, this.nativeImageAttributes), type, false, num, num);
			if (num2 != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num2);
			}
		}

		/// <summary>Sets the CMYK (cyan-magenta-yellow-black) output channel for the default category.</summary>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorChannelFlag" /> that specifies the output channel.</param>
		// Token: 0x06000C9B RID: 3227 RVA: 0x0001D653 File Offset: 0x0001B853
		public void SetOutputChannel(ColorChannelFlag flags)
		{
			this.SetOutputChannel(flags, ColorAdjustType.Default);
		}

		/// <summary>Sets the CMYK (cyan-magenta-yellow-black) output channel for a specified category.</summary>
		/// <param name="flags">An element of <see cref="T:System.Drawing.Imaging.ColorChannelFlag" /> that specifies the output channel.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel is set.</param>
		// Token: 0x06000C9C RID: 3228 RVA: 0x0001D660 File Offset: 0x0001B860
		public void SetOutputChannel(ColorChannelFlag flags, ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesOutputChannel(new HandleRef(this, this.nativeImageAttributes), type, true, flags);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the CMYK (cyan-magenta-yellow-black) output channel setting for the default category.</summary>
		// Token: 0x06000C9D RID: 3229 RVA: 0x0001D68C File Offset: 0x0001B88C
		public void ClearOutputChannel()
		{
			this.ClearOutputChannel(ColorAdjustType.Default);
		}

		/// <summary>Clears the (cyan-magenta-yellow-black) output channel setting for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel setting is cleared.</param>
		// Token: 0x06000C9E RID: 3230 RVA: 0x0001D698 File Offset: 0x0001B898
		public void ClearOutputChannel(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesOutputChannel(new HandleRef(this, this.nativeImageAttributes), type, false, ColorChannelFlag.ColorChannelLast);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the output channel color-profile file for the default category.</summary>
		/// <param name="colorProfileFilename">The path name of a color-profile file. If the color-profile file is in the %SystemRoot%\System32\Spool\Drivers\Color directory, this parameter can be the file name. Otherwise, this parameter must be the fully qualified path name.</param>
		// Token: 0x06000C9F RID: 3231 RVA: 0x0001D6C4 File Offset: 0x0001B8C4
		public void SetOutputChannelColorProfile(string colorProfileFilename)
		{
			this.SetOutputChannelColorProfile(colorProfileFilename, ColorAdjustType.Default);
		}

		/// <summary>Sets the output channel color-profile file for a specified category.</summary>
		/// <param name="colorProfileFilename">The path name of a color-profile file. If the color-profile file is in the %SystemRoot%\System32\Spool\Drivers\Color directory, this parameter can be the file name. Otherwise, this parameter must be the fully qualified path name.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel color-profile file is set.</param>
		// Token: 0x06000CA0 RID: 3232 RVA: 0x0001D6D0 File Offset: 0x0001B8D0
		public void SetOutputChannelColorProfile(string colorProfileFilename, ColorAdjustType type)
		{
			Path.GetFullPath(colorProfileFilename);
			int num = GDIPlus.GdipSetImageAttributesOutputChannelColorProfile(new HandleRef(this, this.nativeImageAttributes), type, true, colorProfileFilename);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Clears the output channel color profile setting for the default category.</summary>
		// Token: 0x06000CA1 RID: 3233 RVA: 0x0001D68C File Offset: 0x0001B88C
		public void ClearOutputChannelColorProfile()
		{
			this.ClearOutputChannel(ColorAdjustType.Default);
		}

		/// <summary>Clears the output channel color profile setting for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the output channel profile setting is cleared.</param>
		// Token: 0x06000CA2 RID: 3234 RVA: 0x0001D704 File Offset: 0x0001B904
		public void ClearOutputChannelColorProfile(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesOutputChannel(new HandleRef(this, this.nativeImageAttributes), type, false, ColorChannelFlag.ColorChannelLast);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color-remap table for the default category.</summary>
		/// <param name="map">An array of color pairs of type <see cref="T:System.Drawing.Imaging.ColorMap" />. Each color pair contains an existing color (the first value) and the color that it will be mapped to (the second value).</param>
		// Token: 0x06000CA3 RID: 3235 RVA: 0x0001D730 File Offset: 0x0001B930
		public void SetRemapTable(ColorMap[] map)
		{
			this.SetRemapTable(map, ColorAdjustType.Default);
		}

		/// <summary>Sets the color-remap table for a specified category.</summary>
		/// <param name="map">An array of color pairs of type <see cref="T:System.Drawing.Imaging.ColorMap" />. Each color pair contains an existing color (the first value) and the color that it will be mapped to (the second value).</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the color-remap table is set.</param>
		// Token: 0x06000CA4 RID: 3236 RVA: 0x0001D73C File Offset: 0x0001B93C
		public void SetRemapTable(ColorMap[] map, ColorAdjustType type)
		{
			int num = map.Length;
			int num2 = 4;
			IntPtr intPtr = Marshal.AllocHGlobal(checked(num * num2 * 2));
			try
			{
				for (int i = 0; i < num; i++)
				{
					Marshal.StructureToPtr<int>(map[i].OldColor.ToArgb(), (IntPtr)((long)intPtr + (long)(i * num2 * 2)), false);
					Marshal.StructureToPtr<int>(map[i].NewColor.ToArgb(), (IntPtr)((long)intPtr + (long)(i * num2 * 2) + (long)num2), false);
				}
				int num3 = GDIPlus.GdipSetImageAttributesRemapTable(new HandleRef(this, this.nativeImageAttributes), type, true, num, new HandleRef(null, intPtr));
				if (num3 != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num3);
				}
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
		}

		/// <summary>Clears the color-remap table for the default category.</summary>
		// Token: 0x06000CA5 RID: 3237 RVA: 0x0001D800 File Offset: 0x0001BA00
		public void ClearRemapTable()
		{
			this.ClearRemapTable(ColorAdjustType.Default);
		}

		/// <summary>Clears the color-remap table for a specified category.</summary>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category for which the remap table is cleared.</param>
		// Token: 0x06000CA6 RID: 3238 RVA: 0x0001D80C File Offset: 0x0001BA0C
		public void ClearRemapTable(ColorAdjustType type)
		{
			int num = GDIPlus.GdipSetImageAttributesRemapTable(new HandleRef(this, this.nativeImageAttributes), type, false, 0, NativeMethods.NullHandleRef);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Sets the color-remap table for the brush category.</summary>
		/// <param name="map">An array of <see cref="T:System.Drawing.Imaging.ColorMap" /> objects.</param>
		// Token: 0x06000CA7 RID: 3239 RVA: 0x0001D83D File Offset: 0x0001BA3D
		public void SetBrushRemapTable(ColorMap[] map)
		{
			this.SetRemapTable(map, ColorAdjustType.Brush);
		}

		/// <summary>Clears the brush color-remap table of this <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object.</summary>
		// Token: 0x06000CA8 RID: 3240 RVA: 0x0001D847 File Offset: 0x0001BA47
		public void ClearBrushRemapTable()
		{
			this.ClearRemapTable(ColorAdjustType.Brush);
		}

		/// <summary>Sets the wrap mode that is used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area.</param>
		// Token: 0x06000CA9 RID: 3241 RVA: 0x0001D850 File Offset: 0x0001BA50
		public void SetWrapMode(WrapMode mode)
		{
			this.SetWrapMode(mode, default(Color), false);
		}

		/// <summary>Sets the wrap mode and color used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area.</param>
		/// <param name="color">An <see cref="T:System.Drawing.Imaging.ImageAttributes" /> object that specifies the color of pixels outside of a rendered image. This color is visible if the mode parameter is set to <see cref="F:System.Drawing.Drawing2D.WrapMode.Clamp" /> and the source rectangle passed to <see cref="Overload:System.Drawing.Graphics.DrawImage" /> is larger than the image itself.</param>
		// Token: 0x06000CAA RID: 3242 RVA: 0x0001D86E File Offset: 0x0001BA6E
		public void SetWrapMode(WrapMode mode, Color color)
		{
			this.SetWrapMode(mode, color, false);
		}

		/// <summary>Sets the wrap mode and color used to decide how to tile a texture across a shape, or at shape boundaries. A texture is tiled across a shape to fill it in when the texture is smaller than the shape it is filling.</summary>
		/// <param name="mode">An element of <see cref="T:System.Drawing.Drawing2D.WrapMode" /> that specifies how repeated copies of an image are used to tile an area.</param>
		/// <param name="color">A color object that specifies the color of pixels outside of a rendered image. This color is visible if the mode parameter is set to <see cref="F:System.Drawing.Drawing2D.WrapMode.Clamp" /> and the source rectangle passed to <see cref="Overload:System.Drawing.Graphics.DrawImage" /> is larger than the image itself.</param>
		/// <param name="clamp">This parameter has no effect. Set it to <see langword="false" />.</param>
		// Token: 0x06000CAB RID: 3243 RVA: 0x0001D87C File Offset: 0x0001BA7C
		public void SetWrapMode(WrapMode mode, Color color, bool clamp)
		{
			int num = GDIPlus.GdipSetImageAttributesWrapMode(new HandleRef(this, this.nativeImageAttributes), (int)mode, color.ToArgb(), clamp);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
		}

		/// <summary>Adjusts the colors in a palette according to the adjustment settings of a specified category.</summary>
		/// <param name="palette">A <see cref="T:System.Drawing.Imaging.ColorPalette" /> that on input contains the palette to be adjusted, and on output contains the adjusted palette.</param>
		/// <param name="type">An element of <see cref="T:System.Drawing.Imaging.ColorAdjustType" /> that specifies the category whose adjustment settings will be applied to the palette.</param>
		// Token: 0x06000CAC RID: 3244 RVA: 0x0001D8B0 File Offset: 0x0001BAB0
		public void GetAdjustedPalette(ColorPalette palette, ColorAdjustType type)
		{
			IntPtr intPtr = palette.ConvertToMemory();
			try
			{
				int num = GDIPlus.GdipGetImageAttributesAdjustedPalette(new HandleRef(this, this.nativeImageAttributes), new HandleRef(null, intPtr), type);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				palette.ConvertFromMemory(intPtr);
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
		}

		// Token: 0x040009AA RID: 2474
		internal IntPtr nativeImageAttributes;
	}
}

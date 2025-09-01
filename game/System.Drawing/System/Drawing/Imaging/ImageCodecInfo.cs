using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>The <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> class provides the necessary storage members and methods to retrieve all pertinent information about the installed image encoders and decoders (called codecs). Not inheritable.</summary>
	// Token: 0x02000109 RID: 265
	public sealed class ImageCodecInfo
	{
		// Token: 0x06000CAD RID: 3245 RVA: 0x00002050 File Offset: 0x00000250
		internal ImageCodecInfo()
		{
		}

		/// <summary>Gets or sets a <see cref="T:System.Guid" /> structure that contains a GUID that identifies a specific codec.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that contains a GUID that identifies a specific codec.</returns>
		// Token: 0x17000362 RID: 866
		// (get) Token: 0x06000CAE RID: 3246 RVA: 0x0001D918 File Offset: 0x0001BB18
		// (set) Token: 0x06000CAF RID: 3247 RVA: 0x0001D920 File Offset: 0x0001BB20
		public Guid Clsid
		{
			get
			{
				return this._clsid;
			}
			set
			{
				this._clsid = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Guid" /> structure that contains a GUID that identifies the codec's format.</summary>
		/// <returns>A <see cref="T:System.Guid" /> structure that contains a GUID that identifies the codec's format.</returns>
		// Token: 0x17000363 RID: 867
		// (get) Token: 0x06000CB0 RID: 3248 RVA: 0x0001D929 File Offset: 0x0001BB29
		// (set) Token: 0x06000CB1 RID: 3249 RVA: 0x0001D931 File Offset: 0x0001BB31
		public Guid FormatID
		{
			get
			{
				return this._formatID;
			}
			set
			{
				this._formatID = value;
			}
		}

		/// <summary>Gets or sets a string that contains the name of the codec.</summary>
		/// <returns>A string that contains the name of the codec.</returns>
		// Token: 0x17000364 RID: 868
		// (get) Token: 0x06000CB2 RID: 3250 RVA: 0x0001D93A File Offset: 0x0001BB3A
		// (set) Token: 0x06000CB3 RID: 3251 RVA: 0x0001D942 File Offset: 0x0001BB42
		public string CodecName
		{
			get
			{
				return this._codecName;
			}
			set
			{
				this._codecName = value;
			}
		}

		/// <summary>Gets or sets string that contains the path name of the DLL that holds the codec. If the codec is not in a DLL, this pointer is <see langword="null" />.</summary>
		/// <returns>A string that contains the path name of the DLL that holds the codec.</returns>
		// Token: 0x17000365 RID: 869
		// (get) Token: 0x06000CB4 RID: 3252 RVA: 0x0001D94B File Offset: 0x0001BB4B
		// (set) Token: 0x06000CB5 RID: 3253 RVA: 0x0001D953 File Offset: 0x0001BB53
		public string DllName
		{
			get
			{
				return this._dllName;
			}
			set
			{
				this._dllName = value;
			}
		}

		/// <summary>Gets or sets a string that describes the codec's file format.</summary>
		/// <returns>A string that describes the codec's file format.</returns>
		// Token: 0x17000366 RID: 870
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0001D95C File Offset: 0x0001BB5C
		// (set) Token: 0x06000CB7 RID: 3255 RVA: 0x0001D964 File Offset: 0x0001BB64
		public string FormatDescription
		{
			get
			{
				return this._formatDescription;
			}
			set
			{
				this._formatDescription = value;
			}
		}

		/// <summary>Gets or sets string that contains the file name extension(s) used in the codec. The extensions are separated by semicolons.</summary>
		/// <returns>A string that contains the file name extension(s) used in the codec.</returns>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000CB8 RID: 3256 RVA: 0x0001D96D File Offset: 0x0001BB6D
		// (set) Token: 0x06000CB9 RID: 3257 RVA: 0x0001D975 File Offset: 0x0001BB75
		public string FilenameExtension
		{
			get
			{
				return this._filenameExtension;
			}
			set
			{
				this._filenameExtension = value;
			}
		}

		/// <summary>Gets or sets a string that contains the codec's Multipurpose Internet Mail Extensions (MIME) type.</summary>
		/// <returns>A string that contains the codec's Multipurpose Internet Mail Extensions (MIME) type.</returns>
		// Token: 0x17000368 RID: 872
		// (get) Token: 0x06000CBA RID: 3258 RVA: 0x0001D97E File Offset: 0x0001BB7E
		// (set) Token: 0x06000CBB RID: 3259 RVA: 0x0001D986 File Offset: 0x0001BB86
		public string MimeType
		{
			get
			{
				return this._mimeType;
			}
			set
			{
				this._mimeType = value;
			}
		}

		/// <summary>Gets or sets 32-bit value used to store additional information about the codec. This property returns a combination of flags from the <see cref="T:System.Drawing.Imaging.ImageCodecFlags" /> enumeration.</summary>
		/// <returns>A 32-bit value used to store additional information about the codec.</returns>
		// Token: 0x17000369 RID: 873
		// (get) Token: 0x06000CBC RID: 3260 RVA: 0x0001D98F File Offset: 0x0001BB8F
		// (set) Token: 0x06000CBD RID: 3261 RVA: 0x0001D997 File Offset: 0x0001BB97
		public ImageCodecFlags Flags
		{
			get
			{
				return this._flags;
			}
			set
			{
				this._flags = value;
			}
		}

		/// <summary>Gets or sets the version number of the codec.</summary>
		/// <returns>The version number of the codec.</returns>
		// Token: 0x1700036A RID: 874
		// (get) Token: 0x06000CBE RID: 3262 RVA: 0x0001D9A0 File Offset: 0x0001BBA0
		// (set) Token: 0x06000CBF RID: 3263 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		public int Version
		{
			get
			{
				return this._version;
			}
			set
			{
				this._version = value;
			}
		}

		/// <summary>Gets or sets a two dimensional array of bytes that represents the signature of the codec.</summary>
		/// <returns>A two dimensional array of bytes that represents the signature of the codec.</returns>
		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06000CC0 RID: 3264 RVA: 0x0001D9B1 File Offset: 0x0001BBB1
		// (set) Token: 0x06000CC1 RID: 3265 RVA: 0x0001D9B9 File Offset: 0x0001BBB9
		[CLSCompliant(false)]
		public byte[][] SignaturePatterns
		{
			get
			{
				return this._signaturePatterns;
			}
			set
			{
				this._signaturePatterns = value;
			}
		}

		/// <summary>Gets or sets a two dimensional array of bytes that can be used as a filter.</summary>
		/// <returns>A two dimensional array of bytes that can be used as a filter.</returns>
		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x0001D9C2 File Offset: 0x0001BBC2
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x0001D9CA File Offset: 0x0001BBCA
		[CLSCompliant(false)]
		public byte[][] SignatureMasks
		{
			get
			{
				return this._signatureMasks;
			}
			set
			{
				this._signatureMasks = value;
			}
		}

		/// <summary>Returns an array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects that contain information about the image decoders built into GDI+.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects. Each <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> object in the array contains information about one of the built-in image decoders.</returns>
		// Token: 0x06000CC4 RID: 3268 RVA: 0x0001D9D4 File Offset: 0x0001BBD4
		public static ImageCodecInfo[] GetImageDecoders()
		{
			int num2;
			int num3;
			int num = GDIPlus.GdipGetImageDecodersSize(out num2, out num3);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			IntPtr intPtr = Marshal.AllocHGlobal(num3);
			ImageCodecInfo[] result;
			try
			{
				num = GDIPlus.GdipGetImageDecoders(num2, num3, intPtr);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				result = ImageCodecInfo.ConvertFromMemory(intPtr, num2);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		/// <summary>Returns an array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects that contain information about the image encoders built into GDI+.</summary>
		/// <returns>An array of <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> objects. Each <see cref="T:System.Drawing.Imaging.ImageCodecInfo" /> object in the array contains information about one of the built-in image encoders.</returns>
		// Token: 0x06000CC5 RID: 3269 RVA: 0x0001DA38 File Offset: 0x0001BC38
		public static ImageCodecInfo[] GetImageEncoders()
		{
			int num2;
			int num3;
			int num = GDIPlus.GdipGetImageEncodersSize(out num2, out num3);
			if (num != 0)
			{
				throw SafeNativeMethods.Gdip.StatusException(num);
			}
			IntPtr intPtr = Marshal.AllocHGlobal(num3);
			ImageCodecInfo[] result;
			try
			{
				num = GDIPlus.GdipGetImageEncoders(num2, num3, intPtr);
				if (num != 0)
				{
					throw SafeNativeMethods.Gdip.StatusException(num);
				}
				result = ImageCodecInfo.ConvertFromMemory(intPtr, num2);
			}
			finally
			{
				Marshal.FreeHGlobal(intPtr);
			}
			return result;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x0001DA9C File Offset: 0x0001BC9C
		private static ImageCodecInfo[] ConvertFromMemory(IntPtr memoryStart, int numCodecs)
		{
			ImageCodecInfo[] array = new ImageCodecInfo[numCodecs];
			for (int i = 0; i < numCodecs; i++)
			{
				IntPtr ptr = (IntPtr)((long)memoryStart + (long)(Marshal.SizeOf(typeof(ImageCodecInfoPrivate)) * i));
				ImageCodecInfoPrivate imageCodecInfoPrivate = new ImageCodecInfoPrivate();
				Marshal.PtrToStructure<ImageCodecInfoPrivate>(ptr, imageCodecInfoPrivate);
				array[i] = new ImageCodecInfo();
				array[i].Clsid = imageCodecInfoPrivate.Clsid;
				array[i].FormatID = imageCodecInfoPrivate.FormatID;
				array[i].CodecName = Marshal.PtrToStringUni(imageCodecInfoPrivate.CodecName);
				array[i].DllName = Marshal.PtrToStringUni(imageCodecInfoPrivate.DllName);
				array[i].FormatDescription = Marshal.PtrToStringUni(imageCodecInfoPrivate.FormatDescription);
				array[i].FilenameExtension = Marshal.PtrToStringUni(imageCodecInfoPrivate.FilenameExtension);
				array[i].MimeType = Marshal.PtrToStringUni(imageCodecInfoPrivate.MimeType);
				array[i].Flags = (ImageCodecFlags)imageCodecInfoPrivate.Flags;
				array[i].Version = imageCodecInfoPrivate.Version;
				array[i].SignaturePatterns = new byte[imageCodecInfoPrivate.SigCount][];
				array[i].SignatureMasks = new byte[imageCodecInfoPrivate.SigCount][];
				for (int j = 0; j < imageCodecInfoPrivate.SigCount; j++)
				{
					array[i].SignaturePatterns[j] = new byte[imageCodecInfoPrivate.SigSize];
					array[i].SignatureMasks[j] = new byte[imageCodecInfoPrivate.SigSize];
					Marshal.Copy((IntPtr)((long)imageCodecInfoPrivate.SigMask + (long)(j * imageCodecInfoPrivate.SigSize)), array[i].SignatureMasks[j], 0, imageCodecInfoPrivate.SigSize);
					Marshal.Copy((IntPtr)((long)imageCodecInfoPrivate.SigPattern + (long)(j * imageCodecInfoPrivate.SigSize)), array[i].SignaturePatterns[j], 0, imageCodecInfoPrivate.SigSize);
				}
			}
			return array;
		}

		// Token: 0x040009B5 RID: 2485
		private Guid _clsid;

		// Token: 0x040009B6 RID: 2486
		private Guid _formatID;

		// Token: 0x040009B7 RID: 2487
		private string _codecName;

		// Token: 0x040009B8 RID: 2488
		private string _dllName;

		// Token: 0x040009B9 RID: 2489
		private string _formatDescription;

		// Token: 0x040009BA RID: 2490
		private string _filenameExtension;

		// Token: 0x040009BB RID: 2491
		private string _mimeType;

		// Token: 0x040009BC RID: 2492
		private ImageCodecFlags _flags;

		// Token: 0x040009BD RID: 2493
		private int _version;

		// Token: 0x040009BE RID: 2494
		private byte[][] _signaturePatterns;

		// Token: 0x040009BF RID: 2495
		private byte[][] _signatureMasks;
	}
}

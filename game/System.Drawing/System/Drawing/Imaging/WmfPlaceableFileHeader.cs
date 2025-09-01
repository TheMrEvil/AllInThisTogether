using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Defines a placeable metafile. Not inheritable.</summary>
	// Token: 0x02000115 RID: 277
	[StructLayout(LayoutKind.Sequential)]
	public sealed class WmfPlaceableFileHeader
	{
		/// <summary>Gets or sets a value indicating the presence of a placeable metafile header.</summary>
		/// <returns>A value indicating presence of a placeable metafile header.</returns>
		// Token: 0x17000371 RID: 881
		// (get) Token: 0x06000CD7 RID: 3287 RVA: 0x0001DD20 File Offset: 0x0001BF20
		// (set) Token: 0x06000CD8 RID: 3288 RVA: 0x0001DD28 File Offset: 0x0001BF28
		public int Key
		{
			get
			{
				return this._key;
			}
			set
			{
				this._key = value;
			}
		}

		/// <summary>Gets or sets the handle of the metafile in memory.</summary>
		/// <returns>The handle of the metafile in memory.</returns>
		// Token: 0x17000372 RID: 882
		// (get) Token: 0x06000CD9 RID: 3289 RVA: 0x0001DD31 File Offset: 0x0001BF31
		// (set) Token: 0x06000CDA RID: 3290 RVA: 0x0001DD39 File Offset: 0x0001BF39
		public short Hmf
		{
			get
			{
				return this._hmf;
			}
			set
			{
				this._hmf = value;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</summary>
		/// <returns>The x-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</returns>
		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000CDB RID: 3291 RVA: 0x0001DD42 File Offset: 0x0001BF42
		// (set) Token: 0x06000CDC RID: 3292 RVA: 0x0001DD4A File Offset: 0x0001BF4A
		public short BboxLeft
		{
			get
			{
				return this._bboxLeft;
			}
			set
			{
				this._bboxLeft = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</summary>
		/// <returns>The y-coordinate of the upper-left corner of the bounding rectangle of the metafile image on the output device.</returns>
		// Token: 0x17000374 RID: 884
		// (get) Token: 0x06000CDD RID: 3293 RVA: 0x0001DD53 File Offset: 0x0001BF53
		// (set) Token: 0x06000CDE RID: 3294 RVA: 0x0001DD5B File Offset: 0x0001BF5B
		public short BboxTop
		{
			get
			{
				return this._bboxTop;
			}
			set
			{
				this._bboxTop = value;
			}
		}

		/// <summary>Gets or sets the x-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</summary>
		/// <returns>The x-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</returns>
		// Token: 0x17000375 RID: 885
		// (get) Token: 0x06000CDF RID: 3295 RVA: 0x0001DD64 File Offset: 0x0001BF64
		// (set) Token: 0x06000CE0 RID: 3296 RVA: 0x0001DD6C File Offset: 0x0001BF6C
		public short BboxRight
		{
			get
			{
				return this._bboxRight;
			}
			set
			{
				this._bboxRight = value;
			}
		}

		/// <summary>Gets or sets the y-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</summary>
		/// <returns>The y-coordinate of the lower-right corner of the bounding rectangle of the metafile image on the output device.</returns>
		// Token: 0x17000376 RID: 886
		// (get) Token: 0x06000CE1 RID: 3297 RVA: 0x0001DD75 File Offset: 0x0001BF75
		// (set) Token: 0x06000CE2 RID: 3298 RVA: 0x0001DD7D File Offset: 0x0001BF7D
		public short BboxBottom
		{
			get
			{
				return this._bboxBottom;
			}
			set
			{
				this._bboxBottom = value;
			}
		}

		/// <summary>Gets or sets the number of twips per inch.</summary>
		/// <returns>The number of twips per inch.</returns>
		// Token: 0x17000377 RID: 887
		// (get) Token: 0x06000CE3 RID: 3299 RVA: 0x0001DD86 File Offset: 0x0001BF86
		// (set) Token: 0x06000CE4 RID: 3300 RVA: 0x0001DD8E File Offset: 0x0001BF8E
		public short Inch
		{
			get
			{
				return this._inch;
			}
			set
			{
				this._inch = value;
			}
		}

		/// <summary>Reserved. Do not use.</summary>
		/// <returns>Reserved. Do not use.</returns>
		// Token: 0x17000378 RID: 888
		// (get) Token: 0x06000CE5 RID: 3301 RVA: 0x0001DD97 File Offset: 0x0001BF97
		// (set) Token: 0x06000CE6 RID: 3302 RVA: 0x0001DD9F File Offset: 0x0001BF9F
		public int Reserved
		{
			get
			{
				return this._reserved;
			}
			set
			{
				this._reserved = value;
			}
		}

		/// <summary>Gets or sets the checksum value for the previous ten <see langword="WORD" /> s in the header.</summary>
		/// <returns>The checksum value for the previous ten <see langword="WORD" /> s in the header.</returns>
		// Token: 0x17000379 RID: 889
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
		// (set) Token: 0x06000CE8 RID: 3304 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		public short Checksum
		{
			get
			{
				return this._checksum;
			}
			set
			{
				this._checksum = value;
			}
		}

		/// <summary>Initializes a new instance of the <see langword="WmfPlaceableFileHeader" /> class.</summary>
		// Token: 0x06000CE9 RID: 3305 RVA: 0x0001DDB9 File Offset: 0x0001BFB9
		public WmfPlaceableFileHeader()
		{
		}

		// Token: 0x04000A3B RID: 2619
		private int _key = -1698247209;

		// Token: 0x04000A3C RID: 2620
		private short _hmf;

		// Token: 0x04000A3D RID: 2621
		private short _bboxLeft;

		// Token: 0x04000A3E RID: 2622
		private short _bboxTop;

		// Token: 0x04000A3F RID: 2623
		private short _bboxRight;

		// Token: 0x04000A40 RID: 2624
		private short _bboxBottom;

		// Token: 0x04000A41 RID: 2625
		private short _inch;

		// Token: 0x04000A42 RID: 2626
		private int _reserved;

		// Token: 0x04000A43 RID: 2627
		private short _checksum;
	}
}

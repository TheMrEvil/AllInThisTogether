using System;
using System.Runtime.InteropServices;

namespace System.Drawing.Imaging
{
	/// <summary>Contains information about a windows-format (WMF) metafile.</summary>
	// Token: 0x0200011A RID: 282
	[StructLayout(LayoutKind.Sequential)]
	public sealed class MetaHeader
	{
		/// <summary>Initializes a new instance of the <see langword="MetaHeader" /> class.</summary>
		// Token: 0x06000D0F RID: 3343 RVA: 0x00002050 File Offset: 0x00000250
		public MetaHeader()
		{
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0001E45C File Offset: 0x0001C65C
		internal MetaHeader(WmfMetaHeader header)
		{
			this.wmf.file_type = header.file_type;
			this.wmf.header_size = header.header_size;
			this.wmf.version = header.version;
			this.wmf.file_size_low = header.file_size_low;
			this.wmf.file_size_high = header.file_size_high;
			this.wmf.num_of_objects = header.num_of_objects;
			this.wmf.max_record_size = header.max_record_size;
			this.wmf.num_of_params = header.num_of_params;
		}

		/// <summary>Gets or sets the size, in bytes, of the header file.</summary>
		/// <returns>The size, in bytes, of the header file.</returns>
		// Token: 0x1700038C RID: 908
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x0001E4F7 File Offset: 0x0001C6F7
		// (set) Token: 0x06000D12 RID: 3346 RVA: 0x0001E504 File Offset: 0x0001C704
		public short HeaderSize
		{
			get
			{
				return this.wmf.header_size;
			}
			set
			{
				this.wmf.header_size = value;
			}
		}

		/// <summary>Gets or sets the size, in bytes, of the largest record in the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
		/// <returns>The size, in bytes, of the largest record in the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
		// Token: 0x1700038D RID: 909
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x0001E512 File Offset: 0x0001C712
		// (set) Token: 0x06000D14 RID: 3348 RVA: 0x0001E51F File Offset: 0x0001C71F
		public int MaxRecord
		{
			get
			{
				return this.wmf.max_record_size;
			}
			set
			{
				this.wmf.max_record_size = value;
			}
		}

		/// <summary>Gets or sets the maximum number of objects that exist in the <see cref="T:System.Drawing.Imaging.Metafile" /> object at the same time.</summary>
		/// <returns>The maximum number of objects that exist in the <see cref="T:System.Drawing.Imaging.Metafile" /> object at the same time.</returns>
		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x0001E52D File Offset: 0x0001C72D
		// (set) Token: 0x06000D16 RID: 3350 RVA: 0x0001E53A File Offset: 0x0001C73A
		public short NoObjects
		{
			get
			{
				return this.wmf.num_of_objects;
			}
			set
			{
				this.wmf.num_of_objects = value;
			}
		}

		/// <summary>Not used. Always returns 0.</summary>
		/// <returns>Always 0.</returns>
		// Token: 0x1700038F RID: 911
		// (get) Token: 0x06000D17 RID: 3351 RVA: 0x0001E548 File Offset: 0x0001C748
		// (set) Token: 0x06000D18 RID: 3352 RVA: 0x0001E555 File Offset: 0x0001C755
		public short NoParameters
		{
			get
			{
				return this.wmf.num_of_params;
			}
			set
			{
				this.wmf.num_of_params = value;
			}
		}

		/// <summary>Gets or sets the size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
		/// <returns>The size, in bytes, of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x0001E563 File Offset: 0x0001C763
		// (set) Token: 0x06000D1A RID: 3354 RVA: 0x0001E5A4 File Offset: 0x0001C7A4
		public int Size
		{
			get
			{
				if (BitConverter.IsLittleEndian)
				{
					return (int)this.wmf.file_size_high << 16 | (int)this.wmf.file_size_low;
				}
				return (int)this.wmf.file_size_low << 16 | (int)this.wmf.file_size_high;
			}
			set
			{
				if (BitConverter.IsLittleEndian)
				{
					this.wmf.file_size_high = (ushort)(value >> 16);
					this.wmf.file_size_low = (ushort)value;
					return;
				}
				this.wmf.file_size_high = (ushort)value;
				this.wmf.file_size_low = (ushort)(value >> 16);
			}
		}

		/// <summary>Gets or sets the type of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</summary>
		/// <returns>The type of the associated <see cref="T:System.Drawing.Imaging.Metafile" /> object.</returns>
		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000D1B RID: 3355 RVA: 0x0001E5F3 File Offset: 0x0001C7F3
		// (set) Token: 0x06000D1C RID: 3356 RVA: 0x0001E600 File Offset: 0x0001C800
		public short Type
		{
			get
			{
				return this.wmf.file_type;
			}
			set
			{
				this.wmf.file_type = value;
			}
		}

		/// <summary>Gets or sets the version number of the header format.</summary>
		/// <returns>The version number of the header format.</returns>
		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000D1D RID: 3357 RVA: 0x0001E60E File Offset: 0x0001C80E
		// (set) Token: 0x06000D1E RID: 3358 RVA: 0x0001E61B File Offset: 0x0001C81B
		public short Version
		{
			get
			{
				return this.wmf.version;
			}
			set
			{
				this.wmf.version = value;
			}
		}

		// Token: 0x04000A78 RID: 2680
		private WmfMetaHeader wmf;
	}
}

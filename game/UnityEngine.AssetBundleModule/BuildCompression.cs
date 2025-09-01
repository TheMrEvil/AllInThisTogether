using System;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	[UsedByNativeCode]
	[Serializable]
	public struct BuildCompression
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002874 File Offset: 0x00000A74
		// (set) Token: 0x06000065 RID: 101 RVA: 0x0000288C File Offset: 0x00000A8C
		public CompressionType compression
		{
			get
			{
				return this._compression;
			}
			private set
			{
				this._compression = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002898 File Offset: 0x00000A98
		// (set) Token: 0x06000067 RID: 103 RVA: 0x000028B0 File Offset: 0x00000AB0
		public CompressionLevel level
		{
			get
			{
				return this._level;
			}
			private set
			{
				this._level = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000068 RID: 104 RVA: 0x000028BC File Offset: 0x00000ABC
		// (set) Token: 0x06000069 RID: 105 RVA: 0x000028D4 File Offset: 0x00000AD4
		public uint blockSize
		{
			get
			{
				return this._blockSize;
			}
			private set
			{
				this._blockSize = value;
			}
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000028DE File Offset: 0x00000ADE
		private BuildCompression(CompressionType in_compression, CompressionLevel in_level, uint in_blockSize)
		{
			this = default(BuildCompression);
			this.compression = in_compression;
			this.level = in_level;
			this.blockSize = in_blockSize;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00002900 File Offset: 0x00000B00
		// Note: this type is marked as 'beforefieldinit'.
		static BuildCompression()
		{
		}

		// Token: 0x0400001E RID: 30
		public static readonly BuildCompression Uncompressed = new BuildCompression(CompressionType.None, CompressionLevel.Maximum, 131072U);

		// Token: 0x0400001F RID: 31
		public static readonly BuildCompression LZ4 = new BuildCompression(CompressionType.Lz4HC, CompressionLevel.Maximum, 131072U);

		// Token: 0x04000020 RID: 32
		public static readonly BuildCompression LZMA = new BuildCompression(CompressionType.Lzma, CompressionLevel.Maximum, 131072U);

		// Token: 0x04000021 RID: 33
		public static readonly BuildCompression UncompressedRuntime = BuildCompression.Uncompressed;

		// Token: 0x04000022 RID: 34
		public static readonly BuildCompression LZ4Runtime = new BuildCompression(CompressionType.Lz4, CompressionLevel.Maximum, 131072U);

		// Token: 0x04000023 RID: 35
		[NativeName("compression")]
		private CompressionType _compression;

		// Token: 0x04000024 RID: 36
		[NativeName("level")]
		private CompressionLevel _level;

		// Token: 0x04000025 RID: 37
		[NativeName("blockSize")]
		private uint _blockSize;
	}
}

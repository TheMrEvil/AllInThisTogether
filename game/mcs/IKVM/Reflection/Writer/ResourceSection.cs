using System;
using System.Collections.Generic;
using System.IO;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection.Writer
{
	// Token: 0x0200008B RID: 139
	internal sealed class ResourceSection
	{
		// Token: 0x0600071D RID: 1821 RVA: 0x0001683F File Offset: 0x00014A3F
		internal void AddVersionInfo(ByteBuffer versionInfo)
		{
			this.root[new OrdinalOrName(16)][new OrdinalOrName(1)][new OrdinalOrName(0)].Data = versionInfo;
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00016870 File Offset: 0x00014A70
		internal void AddIcon(byte[] iconFile)
		{
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(iconFile));
			ushort num = binaryReader.ReadUInt16();
			ushort num2 = binaryReader.ReadUInt16();
			ushort num3 = binaryReader.ReadUInt16();
			if (num != 0 || num2 != 1)
			{
				throw new ArgumentException("The supplied byte array is not a valid .ico file.");
			}
			ByteBuffer byteBuffer = new ByteBuffer((int)(6 + 14 * num3));
			byteBuffer.Write(num);
			byteBuffer.Write(num2);
			byteBuffer.Write(num3);
			for (int i = 0; i < (int)num3; i++)
			{
				byte value = binaryReader.ReadByte();
				byte value2 = binaryReader.ReadByte();
				byte value3 = binaryReader.ReadByte();
				byte value4 = binaryReader.ReadByte();
				ushort value5 = binaryReader.ReadUInt16();
				ushort value6 = binaryReader.ReadUInt16();
				uint num4 = binaryReader.ReadUInt32();
				uint srcOffset = binaryReader.ReadUInt32();
				ushort value7 = (ushort)(2 + i);
				byteBuffer.Write(value);
				byteBuffer.Write(value2);
				byteBuffer.Write(value3);
				byteBuffer.Write(value4);
				byteBuffer.Write(value5);
				byteBuffer.Write(value6);
				byteBuffer.Write(num4);
				byteBuffer.Write(value7);
				byte[] array = new byte[num4];
				Buffer.BlockCopy(iconFile, (int)srcOffset, array, 0, array.Length);
				this.root[new OrdinalOrName(3)][new OrdinalOrName(value7)][new OrdinalOrName(0)].Data = ByteBuffer.Wrap(array);
			}
			this.root[new OrdinalOrName(14)][new OrdinalOrName(32512)][new OrdinalOrName(0)].Data = byteBuffer;
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x000169F9 File Offset: 0x00014BF9
		internal void AddManifest(byte[] manifest, ushort resourceID)
		{
			this.root[new OrdinalOrName(24)][new OrdinalOrName(resourceID)][new OrdinalOrName(0)].Data = ByteBuffer.Wrap(manifest);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00016A30 File Offset: 0x00014C30
		internal void ExtractResources(byte[] buf)
		{
			ByteReader byteReader = new ByteReader(buf, 0, buf.Length);
			while (byteReader.Length >= 32)
			{
				byteReader.Align(4);
				RESOURCEHEADER resourceheader = new RESOURCEHEADER(byteReader);
				if (resourceheader.DataSize != 0)
				{
					this.root[resourceheader.TYPE][resourceheader.NAME][new OrdinalOrName(resourceheader.LanguageId)].Data = ByteBuffer.Wrap(byteReader.ReadBytes(resourceheader.DataSize));
				}
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00016AB0 File Offset: 0x00014CB0
		internal void Finish()
		{
			if (this.bb != null)
			{
				throw new InvalidOperationException();
			}
			this.bb = new ByteBuffer(1024);
			this.linkOffsets = new List<int>();
			this.root.Write(this.bb, this.linkOffsets);
			this.root = null;
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000722 RID: 1826 RVA: 0x00016B04 File Offset: 0x00014D04
		internal int Length
		{
			get
			{
				return this.bb.Length;
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x00016B14 File Offset: 0x00014D14
		internal void Write(MetadataWriter mw, uint rva)
		{
			foreach (int position in this.linkOffsets)
			{
				this.bb.Position = position;
				this.bb.Write(this.bb.GetInt32AtCurrentPosition() + (int)rva);
			}
			mw.Write(this.bb);
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00016B90 File Offset: 0x00014D90
		public ResourceSection()
		{
		}

		// Token: 0x040002D9 RID: 729
		private const int RT_ICON = 3;

		// Token: 0x040002DA RID: 730
		private const int RT_GROUP_ICON = 14;

		// Token: 0x040002DB RID: 731
		private const int RT_VERSION = 16;

		// Token: 0x040002DC RID: 732
		private const int RT_MANIFEST = 24;

		// Token: 0x040002DD RID: 733
		private ResourceDirectoryEntry root = new ResourceDirectoryEntry(new OrdinalOrName("root"));

		// Token: 0x040002DE RID: 734
		private ByteBuffer bb;

		// Token: 0x040002DF RID: 735
		private List<int> linkOffsets;
	}
}

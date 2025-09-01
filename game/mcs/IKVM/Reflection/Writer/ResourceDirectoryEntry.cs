using System;
using System.Collections.Generic;

namespace IKVM.Reflection.Writer
{
	// Token: 0x0200008C RID: 140
	internal sealed class ResourceDirectoryEntry
	{
		// Token: 0x06000725 RID: 1829 RVA: 0x00016BAD File Offset: 0x00014DAD
		internal ResourceDirectoryEntry(OrdinalOrName id)
		{
			this.OrdinalOrName = id;
		}

		// Token: 0x17000267 RID: 615
		internal ResourceDirectoryEntry this[OrdinalOrName id]
		{
			get
			{
				foreach (ResourceDirectoryEntry resourceDirectoryEntry in this.entries)
				{
					if (resourceDirectoryEntry.OrdinalOrName.IsEqual(id))
					{
						return resourceDirectoryEntry;
					}
				}
				ResourceDirectoryEntry resourceDirectoryEntry2 = new ResourceDirectoryEntry(id);
				if (id.Name == null)
				{
					for (int i = this.namedEntries; i < this.entries.Count; i++)
					{
						if (this.entries[i].OrdinalOrName.IsGreaterThan(id))
						{
							this.entries.Insert(i, resourceDirectoryEntry2);
							return resourceDirectoryEntry2;
						}
					}
					this.entries.Add(resourceDirectoryEntry2);
					return resourceDirectoryEntry2;
				}
				for (int j = 0; j < this.namedEntries; j++)
				{
					if (this.entries[j].OrdinalOrName.IsGreaterThan(id))
					{
						this.entries.Insert(j, resourceDirectoryEntry2);
						this.namedEntries++;
						return resourceDirectoryEntry2;
					}
				}
				List<ResourceDirectoryEntry> list = this.entries;
				int num = this.namedEntries;
				this.namedEntries = num + 1;
				list.Insert(num, resourceDirectoryEntry2);
				return resourceDirectoryEntry2;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x00016D0C File Offset: 0x00014F0C
		private int DirectoryLength
		{
			get
			{
				if (this.Data != null)
				{
					return 16;
				}
				int num = 16 + this.entries.Count * 8;
				foreach (ResourceDirectoryEntry resourceDirectoryEntry in this.entries)
				{
					num += resourceDirectoryEntry.DirectoryLength;
				}
				return num;
			}
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x00016D80 File Offset: 0x00014F80
		internal void Write(ByteBuffer bb, List<int> linkOffsets)
		{
			if (this.entries.Count != 0)
			{
				int directoryLength = this.DirectoryLength;
				Dictionary<string, int> strings = new Dictionary<string, int>();
				ByteBuffer byteBuffer = new ByteBuffer(16);
				int num = 16 + this.entries.Count * 8;
				for (int i = 0; i < 3; i++)
				{
					this.Write(bb, i, 0, ref num, strings, ref directoryLength, byteBuffer);
				}
				byteBuffer.Align(4);
				num += byteBuffer.Length;
				this.WriteResourceDataEntries(bb, linkOffsets, ref num);
				bb.Write(byteBuffer);
				this.WriteData(bb);
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x00016E08 File Offset: 0x00015008
		private void WriteResourceDataEntries(ByteBuffer bb, List<int> linkOffsets, ref int offset)
		{
			foreach (ResourceDirectoryEntry resourceDirectoryEntry in this.entries)
			{
				if (resourceDirectoryEntry.Data != null)
				{
					linkOffsets.Add(bb.Position);
					bb.Write(offset);
					bb.Write(resourceDirectoryEntry.Data.Length);
					bb.Write(0);
					bb.Write(0);
					offset += (resourceDirectoryEntry.Data.Length + 3 & -4);
				}
				else
				{
					resourceDirectoryEntry.WriteResourceDataEntries(bb, linkOffsets, ref offset);
				}
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x00016EB0 File Offset: 0x000150B0
		private void WriteData(ByteBuffer bb)
		{
			foreach (ResourceDirectoryEntry resourceDirectoryEntry in this.entries)
			{
				if (resourceDirectoryEntry.Data != null)
				{
					bb.Write(resourceDirectoryEntry.Data);
					bb.Align(4);
				}
				else
				{
					resourceDirectoryEntry.WriteData(bb);
				}
			}
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x00016F20 File Offset: 0x00015120
		private void Write(ByteBuffer bb, int writeDepth, int currentDepth, ref int offset, Dictionary<string, int> strings, ref int stringTableOffset, ByteBuffer stringTable)
		{
			if (currentDepth == writeDepth)
			{
				bb.Write(0);
				bb.Write(0);
				bb.Write(0);
				bb.Write((ushort)this.namedEntries);
				bb.Write((ushort)(this.entries.Count - this.namedEntries));
			}
			foreach (ResourceDirectoryEntry resourceDirectoryEntry in this.entries)
			{
				if (currentDepth == writeDepth)
				{
					resourceDirectoryEntry.WriteEntry(bb, ref offset, strings, ref stringTableOffset, stringTable);
				}
				else
				{
					resourceDirectoryEntry.Write(bb, writeDepth, currentDepth + 1, ref offset, strings, ref stringTableOffset, stringTable);
				}
			}
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x00016FD4 File Offset: 0x000151D4
		private void WriteEntry(ByteBuffer bb, ref int offset, Dictionary<string, int> strings, ref int stringTableOffset, ByteBuffer stringTable)
		{
			ResourceDirectoryEntry.WriteNameOrOrdinal(bb, this.OrdinalOrName, strings, ref stringTableOffset, stringTable);
			if (this.Data == null)
			{
				bb.Write((uint)(int.MinValue | offset));
			}
			else
			{
				bb.Write(offset);
			}
			offset += 16 + this.entries.Count * 8;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x00017028 File Offset: 0x00015228
		private static void WriteNameOrOrdinal(ByteBuffer bb, OrdinalOrName id, Dictionary<string, int> strings, ref int stringTableOffset, ByteBuffer stringTable)
		{
			if (id.Name == null)
			{
				bb.Write((int)id.Ordinal);
				return;
			}
			int num;
			if (!strings.TryGetValue(id.Name, out num))
			{
				num = stringTableOffset;
				strings.Add(id.Name, num);
				stringTableOffset += id.Name.Length * 2 + 2;
				stringTable.Write((ushort)id.Name.Length);
				foreach (char c in id.Name)
				{
					stringTable.Write((short)c);
				}
			}
			bb.Write((uint)(int.MinValue | num));
		}

		// Token: 0x040002E0 RID: 736
		internal readonly OrdinalOrName OrdinalOrName;

		// Token: 0x040002E1 RID: 737
		internal ByteBuffer Data;

		// Token: 0x040002E2 RID: 738
		private int namedEntries;

		// Token: 0x040002E3 RID: 739
		private readonly List<ResourceDirectoryEntry> entries = new List<ResourceDirectoryEntry>();
	}
}

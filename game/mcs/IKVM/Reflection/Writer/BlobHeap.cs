using System;
using IKVM.Reflection.Reader;

namespace IKVM.Reflection.Writer
{
	// Token: 0x02000083 RID: 131
	internal sealed class BlobHeap : SimpleHeap
	{
		// Token: 0x060006D8 RID: 1752 RVA: 0x00014AB4 File Offset: 0x00012CB4
		internal BlobHeap()
		{
			this.buf.Write(0);
		}

		// Token: 0x060006D9 RID: 1753 RVA: 0x00014AE8 File Offset: 0x00012CE8
		internal int Add(ByteBuffer bb)
		{
			int length = bb.Length;
			if (length == 0)
			{
				return 0;
			}
			int compressedUIntLength = MetadataWriter.GetCompressedUIntLength(length);
			int num = bb.Hash();
			int num2 = (num & int.MaxValue) % this.map.Length;
			BlobHeap.Key[] next = this.map;
			int num3 = num2;
			while (next[num2].offset != 0)
			{
				if (next[num2].hash == num && next[num2].len == length && this.buf.Match(next[num2].offset + compressedUIntLength, bb, 0, length))
				{
					return next[num2].offset;
				}
				if (num2 == num3)
				{
					if (next[num2].next == null)
					{
						next[num2].next = new BlobHeap.Key[4];
						next = next[num2].next;
						num2 = 0;
						break;
					}
					next = next[num2].next;
					num2 = -1;
					num3 = next.Length - 1;
				}
				num2++;
			}
			int position = this.buf.Position;
			this.buf.WriteCompressedUInt(length);
			this.buf.Write(bb);
			next[num2].len = length;
			next[num2].hash = num;
			next[num2].offset = position;
			return position;
		}

		// Token: 0x060006DA RID: 1754 RVA: 0x00014C38 File Offset: 0x00012E38
		protected override int GetLength()
		{
			return this.buf.Position;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x00014C45 File Offset: 0x00012E45
		protected override void WriteImpl(MetadataWriter mw)
		{
			mw.Write(this.buf);
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x00014C53 File Offset: 0x00012E53
		internal bool IsEmpty
		{
			get
			{
				return this.buf.Position == 1;
			}
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00014C63 File Offset: 0x00012E63
		internal ByteReader GetBlob(int blobIndex)
		{
			return this.buf.GetBlob(blobIndex);
		}

		// Token: 0x0400028D RID: 653
		private BlobHeap.Key[] map = new BlobHeap.Key[8179];

		// Token: 0x0400028E RID: 654
		private readonly ByteBuffer buf = new ByteBuffer(32);

		// Token: 0x02000339 RID: 825
		private struct Key
		{
			// Token: 0x04000E76 RID: 3702
			internal BlobHeap.Key[] next;

			// Token: 0x04000E77 RID: 3703
			internal int len;

			// Token: 0x04000E78 RID: 3704
			internal int hash;

			// Token: 0x04000E79 RID: 3705
			internal int offset;
		}
	}
}

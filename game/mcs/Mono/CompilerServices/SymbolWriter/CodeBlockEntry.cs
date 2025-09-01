using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000314 RID: 788
	public class CodeBlockEntry
	{
		// Token: 0x060024FE RID: 9470 RVA: 0x000B0E6D File Offset: 0x000AF06D
		public CodeBlockEntry(int index, int parent, CodeBlockEntry.Type type, int start_offset)
		{
			this.Index = index;
			this.Parent = parent;
			this.BlockType = type;
			this.StartOffset = start_offset;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x000B0E94 File Offset: 0x000AF094
		internal CodeBlockEntry(int index, MyBinaryReader reader)
		{
			this.Index = index;
			int num = reader.ReadLeb128();
			this.BlockType = (CodeBlockEntry.Type)(num & 63);
			this.Parent = reader.ReadLeb128();
			this.StartOffset = reader.ReadLeb128();
			this.EndOffset = reader.ReadLeb128();
			if ((num & 64) != 0)
			{
				int num2 = (int)reader.ReadInt16();
				reader.BaseStream.Position += (long)num2;
			}
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x000B0F04 File Offset: 0x000AF104
		public void Close(int end_offset)
		{
			this.EndOffset = end_offset;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x000B0F0D File Offset: 0x000AF10D
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128((int)this.BlockType);
			bw.WriteLeb128(this.Parent);
			bw.WriteLeb128(this.StartOffset);
			bw.WriteLeb128(this.EndOffset);
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x000B0F40 File Offset: 0x000AF140
		public override string ToString()
		{
			return string.Format("[CodeBlock {0}:{1}:{2}:{3}:{4}]", new object[]
			{
				this.Index,
				this.Parent,
				this.BlockType,
				this.StartOffset,
				this.EndOffset
			});
		}

		// Token: 0x04000DD4 RID: 3540
		public int Index;

		// Token: 0x04000DD5 RID: 3541
		public int Parent;

		// Token: 0x04000DD6 RID: 3542
		public CodeBlockEntry.Type BlockType;

		// Token: 0x04000DD7 RID: 3543
		public int StartOffset;

		// Token: 0x04000DD8 RID: 3544
		public int EndOffset;

		// Token: 0x02000412 RID: 1042
		public enum Type
		{
			// Token: 0x04001199 RID: 4505
			Lexical = 1,
			// Token: 0x0400119A RID: 4506
			CompilerGenerated,
			// Token: 0x0400119B RID: 4507
			IteratorBody,
			// Token: 0x0400119C RID: 4508
			IteratorDispatcher
		}
	}
}

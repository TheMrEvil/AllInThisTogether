using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000315 RID: 789
	public struct LocalVariableEntry
	{
		// Token: 0x06002503 RID: 9475 RVA: 0x000B0FA3 File Offset: 0x000AF1A3
		public LocalVariableEntry(int index, string name, int block)
		{
			this.Index = index;
			this.Name = name;
			this.BlockIndex = block;
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x000B0FBA File Offset: 0x000AF1BA
		internal LocalVariableEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			this.Index = reader.ReadLeb128();
			this.Name = reader.ReadString();
			this.BlockIndex = reader.ReadLeb128();
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x000B0FE0 File Offset: 0x000AF1E0
		internal void Write(MonoSymbolFile file, MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.Index);
			bw.Write(this.Name);
			bw.WriteLeb128(this.BlockIndex);
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x000B1006 File Offset: 0x000AF206
		public override string ToString()
		{
			return string.Format("[LocalVariable {0}:{1}:{2}]", this.Name, this.Index, this.BlockIndex - 1);
		}

		// Token: 0x04000DD9 RID: 3545
		public readonly int Index;

		// Token: 0x04000DDA RID: 3546
		public readonly string Name;

		// Token: 0x04000DDB RID: 3547
		public readonly int BlockIndex;
	}
}

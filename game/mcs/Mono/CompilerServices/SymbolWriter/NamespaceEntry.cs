using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x0200031E RID: 798
	public struct NamespaceEntry
	{
		// Token: 0x0600254C RID: 9548 RVA: 0x000B28B0 File Offset: 0x000B0AB0
		public NamespaceEntry(string name, int index, string[] using_clauses, int parent)
		{
			this.Name = name;
			this.Index = index;
			this.Parent = parent;
			this.UsingClauses = ((using_clauses != null) ? using_clauses : new string[0]);
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x000B28DC File Offset: 0x000B0ADC
		internal NamespaceEntry(MonoSymbolFile file, MyBinaryReader reader)
		{
			this.Name = reader.ReadString();
			this.Index = reader.ReadLeb128();
			this.Parent = reader.ReadLeb128();
			int num = reader.ReadLeb128();
			this.UsingClauses = new string[num];
			for (int i = 0; i < num; i++)
			{
				this.UsingClauses[i] = reader.ReadString();
			}
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x000B293C File Offset: 0x000B0B3C
		internal void Write(MonoSymbolFile file, MyBinaryWriter bw)
		{
			bw.Write(this.Name);
			bw.WriteLeb128(this.Index);
			bw.WriteLeb128(this.Parent);
			bw.WriteLeb128(this.UsingClauses.Length);
			foreach (string value in this.UsingClauses)
			{
				bw.Write(value);
			}
		}

		// Token: 0x0600254F RID: 9551 RVA: 0x000B299B File Offset: 0x000B0B9B
		public override string ToString()
		{
			return string.Format("[Namespace {0}:{1}:{2}]", this.Name, this.Index, this.Parent);
		}

		// Token: 0x04000E19 RID: 3609
		public readonly string Name;

		// Token: 0x04000E1A RID: 3610
		public readonly int Index;

		// Token: 0x04000E1B RID: 3611
		public readonly int Parent;

		// Token: 0x04000E1C RID: 3612
		public readonly string[] UsingClauses;
	}
}

using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000318 RID: 792
	public struct ScopeVariable
	{
		// Token: 0x0600250F RID: 9487 RVA: 0x000B1117 File Offset: 0x000AF317
		public ScopeVariable(int scope, int index)
		{
			this.Scope = scope;
			this.Index = index;
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x000B1127 File Offset: 0x000AF327
		internal ScopeVariable(MyBinaryReader reader)
		{
			this.Scope = reader.ReadLeb128();
			this.Index = reader.ReadLeb128();
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000B1141 File Offset: 0x000AF341
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.Scope);
			bw.WriteLeb128(this.Index);
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000B115B File Offset: 0x000AF35B
		public override string ToString()
		{
			return string.Format("[ScopeVariable {0}:{1}]", this.Scope, this.Index);
		}

		// Token: 0x04000DE1 RID: 3553
		public readonly int Scope;

		// Token: 0x04000DE2 RID: 3554
		public readonly int Index;
	}
}

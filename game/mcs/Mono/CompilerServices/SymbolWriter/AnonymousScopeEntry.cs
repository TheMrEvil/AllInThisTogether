using System;
using System.Collections.Generic;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000319 RID: 793
	public class AnonymousScopeEntry
	{
		// Token: 0x06002513 RID: 9491 RVA: 0x000B117D File Offset: 0x000AF37D
		public AnonymousScopeEntry(int id)
		{
			this.ID = id;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000B11A4 File Offset: 0x000AF3A4
		internal AnonymousScopeEntry(MyBinaryReader reader)
		{
			this.ID = reader.ReadLeb128();
			int num = reader.ReadLeb128();
			for (int i = 0; i < num; i++)
			{
				this.captured_vars.Add(new CapturedVariable(reader));
			}
			int num2 = reader.ReadLeb128();
			for (int j = 0; j < num2; j++)
			{
				this.captured_scopes.Add(new CapturedScope(reader));
			}
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000B1221 File Offset: 0x000AF421
		internal void AddCapturedVariable(string name, string captured_name, CapturedVariable.CapturedKind kind)
		{
			this.captured_vars.Add(new CapturedVariable(name, captured_name, kind));
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06002516 RID: 9494 RVA: 0x000B1238 File Offset: 0x000AF438
		public CapturedVariable[] CapturedVariables
		{
			get
			{
				CapturedVariable[] array = new CapturedVariable[this.captured_vars.Count];
				this.captured_vars.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000B1264 File Offset: 0x000AF464
		internal void AddCapturedScope(int scope, string captured_name)
		{
			this.captured_scopes.Add(new CapturedScope(scope, captured_name));
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002518 RID: 9496 RVA: 0x000B1278 File Offset: 0x000AF478
		public CapturedScope[] CapturedScopes
		{
			get
			{
				CapturedScope[] array = new CapturedScope[this.captured_scopes.Count];
				this.captured_scopes.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x000B12A4 File Offset: 0x000AF4A4
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.ID);
			bw.WriteLeb128(this.captured_vars.Count);
			foreach (CapturedVariable capturedVariable in this.captured_vars)
			{
				capturedVariable.Write(bw);
			}
			bw.WriteLeb128(this.captured_scopes.Count);
			foreach (CapturedScope capturedScope in this.captured_scopes)
			{
				capturedScope.Write(bw);
			}
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x000B136C File Offset: 0x000AF56C
		public override string ToString()
		{
			return string.Format("[AnonymousScope {0}]", this.ID);
		}

		// Token: 0x04000DE3 RID: 3555
		public readonly int ID;

		// Token: 0x04000DE4 RID: 3556
		private List<CapturedVariable> captured_vars = new List<CapturedVariable>();

		// Token: 0x04000DE5 RID: 3557
		private List<CapturedScope> captured_scopes = new List<CapturedScope>();
	}
}

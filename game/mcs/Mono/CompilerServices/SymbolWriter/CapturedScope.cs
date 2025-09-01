using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000317 RID: 791
	public struct CapturedScope
	{
		// Token: 0x0600250B RID: 9483 RVA: 0x000B10B6 File Offset: 0x000AF2B6
		public CapturedScope(int scope, string captured_name)
		{
			this.Scope = scope;
			this.CapturedName = captured_name;
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x000B10C6 File Offset: 0x000AF2C6
		internal CapturedScope(MyBinaryReader reader)
		{
			this.Scope = reader.ReadLeb128();
			this.CapturedName = reader.ReadString();
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x000B10E0 File Offset: 0x000AF2E0
		internal void Write(MyBinaryWriter bw)
		{
			bw.WriteLeb128(this.Scope);
			bw.Write(this.CapturedName);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x000B10FA File Offset: 0x000AF2FA
		public override string ToString()
		{
			return string.Format("[CapturedScope {0}:{1}]", this.Scope, this.CapturedName);
		}

		// Token: 0x04000DDF RID: 3551
		public readonly int Scope;

		// Token: 0x04000DE0 RID: 3552
		public readonly string CapturedName;
	}
}

using System;

namespace Mono.CompilerServices.SymbolWriter
{
	// Token: 0x02000316 RID: 790
	public struct CapturedVariable
	{
		// Token: 0x06002507 RID: 9479 RVA: 0x000B1030 File Offset: 0x000AF230
		public CapturedVariable(string name, string captured_name, CapturedVariable.CapturedKind kind)
		{
			this.Name = name;
			this.CapturedName = captured_name;
			this.Kind = kind;
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x000B1047 File Offset: 0x000AF247
		internal CapturedVariable(MyBinaryReader reader)
		{
			this.Name = reader.ReadString();
			this.CapturedName = reader.ReadString();
			this.Kind = (CapturedVariable.CapturedKind)reader.ReadByte();
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x000B106D File Offset: 0x000AF26D
		internal void Write(MyBinaryWriter bw)
		{
			bw.Write(this.Name);
			bw.Write(this.CapturedName);
			bw.Write((byte)this.Kind);
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x000B1093 File Offset: 0x000AF293
		public override string ToString()
		{
			return string.Format("[CapturedVariable {0}:{1}:{2}]", this.Name, this.CapturedName, this.Kind);
		}

		// Token: 0x04000DDC RID: 3548
		public readonly string Name;

		// Token: 0x04000DDD RID: 3549
		public readonly string CapturedName;

		// Token: 0x04000DDE RID: 3550
		public readonly CapturedVariable.CapturedKind Kind;

		// Token: 0x02000413 RID: 1043
		public enum CapturedKind : byte
		{
			// Token: 0x0400119E RID: 4510
			Local,
			// Token: 0x0400119F RID: 4511
			Parameter,
			// Token: 0x040011A0 RID: 4512
			This
		}
	}
}

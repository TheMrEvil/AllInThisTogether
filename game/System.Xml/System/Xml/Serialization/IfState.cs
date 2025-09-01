using System;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x0200026D RID: 621
	internal class IfState
	{
		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0008B31B File Offset: 0x0008951B
		// (set) Token: 0x060017AC RID: 6060 RVA: 0x0008B323 File Offset: 0x00089523
		internal Label EndIf
		{
			get
			{
				return this.endIf;
			}
			set
			{
				this.endIf = value;
			}
		}

		// Token: 0x1700044F RID: 1103
		// (get) Token: 0x060017AD RID: 6061 RVA: 0x0008B32C File Offset: 0x0008952C
		// (set) Token: 0x060017AE RID: 6062 RVA: 0x0008B334 File Offset: 0x00089534
		internal Label ElseBegin
		{
			get
			{
				return this.elseBegin;
			}
			set
			{
				this.elseBegin = value;
			}
		}

		// Token: 0x060017AF RID: 6063 RVA: 0x0000216B File Offset: 0x0000036B
		public IfState()
		{
		}

		// Token: 0x04001879 RID: 6265
		private Label elseBegin;

		// Token: 0x0400187A RID: 6266
		private Label endIf;
	}
}

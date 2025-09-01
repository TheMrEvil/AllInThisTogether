using System;
using System.Reflection.Emit;

namespace System.Xml.Serialization
{
	// Token: 0x0200026B RID: 619
	internal class ForState
	{
		// Token: 0x060017A6 RID: 6054 RVA: 0x0008B2D6 File Offset: 0x000894D6
		internal ForState(LocalBuilder indexVar, Label beginLabel, Label testLabel, object end)
		{
			this.indexVar = indexVar;
			this.beginLabel = beginLabel;
			this.testLabel = testLabel;
			this.end = end;
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060017A7 RID: 6055 RVA: 0x0008B2FB File Offset: 0x000894FB
		internal LocalBuilder Index
		{
			get
			{
				return this.indexVar;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060017A8 RID: 6056 RVA: 0x0008B303 File Offset: 0x00089503
		internal Label BeginLabel
		{
			get
			{
				return this.beginLabel;
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060017A9 RID: 6057 RVA: 0x0008B30B File Offset: 0x0008950B
		internal Label TestLabel
		{
			get
			{
				return this.testLabel;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0008B313 File Offset: 0x00089513
		internal object End
		{
			get
			{
				return this.end;
			}
		}

		// Token: 0x0400186E RID: 6254
		private LocalBuilder indexVar;

		// Token: 0x0400186F RID: 6255
		private Label beginLabel;

		// Token: 0x04001870 RID: 6256
		private Label testLabel;

		// Token: 0x04001871 RID: 6257
		private object end;
	}
}

using System;

namespace System.Xml.Xsl.XsltOld
{
	// Token: 0x020003B0 RID: 944
	internal abstract class TemplateBaseAction : ContainerAction
	{
		// Token: 0x0600269D RID: 9885 RVA: 0x000E7E2D File Offset: 0x000E602D
		public int AllocateVariableSlot()
		{
			int result = this.variableFreeSlot;
			this.variableFreeSlot++;
			if (this.variableCount < this.variableFreeSlot)
			{
				this.variableCount = this.variableFreeSlot;
			}
			return result;
		}

		// Token: 0x0600269E RID: 9886 RVA: 0x0000B528 File Offset: 0x00009728
		public void ReleaseVariableSlots(int n)
		{
		}

		// Token: 0x0600269F RID: 9887 RVA: 0x000DB75C File Offset: 0x000D995C
		protected TemplateBaseAction()
		{
		}

		// Token: 0x04001E60 RID: 7776
		protected int variableCount;

		// Token: 0x04001E61 RID: 7777
		private int variableFreeSlot;
	}
}

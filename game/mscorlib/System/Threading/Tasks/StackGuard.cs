using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000364 RID: 868
	internal class StackGuard
	{
		// Token: 0x06002464 RID: 9316 RVA: 0x00082555 File Offset: 0x00080755
		internal bool TryBeginInliningScope()
		{
			if (this.m_inliningDepth < 20 || RuntimeHelpers.TryEnsureSufficientExecutionStack())
			{
				this.m_inliningDepth++;
				return true;
			}
			return false;
		}

		// Token: 0x06002465 RID: 9317 RVA: 0x00082579 File Offset: 0x00080779
		internal void EndInliningScope()
		{
			this.m_inliningDepth--;
			if (this.m_inliningDepth < 0)
			{
				this.m_inliningDepth = 0;
			}
		}

		// Token: 0x06002466 RID: 9318 RVA: 0x0000259F File Offset: 0x0000079F
		public StackGuard()
		{
		}

		// Token: 0x04001D25 RID: 7461
		private int m_inliningDepth;

		// Token: 0x04001D26 RID: 7462
		private const int MAX_UNCHECKED_INLINING_DEPTH = 20;
	}
}

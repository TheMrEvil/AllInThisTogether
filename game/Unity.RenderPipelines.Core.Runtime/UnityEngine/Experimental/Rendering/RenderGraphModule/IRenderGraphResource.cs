using System;

namespace UnityEngine.Experimental.Rendering.RenderGraphModule
{
	// Token: 0x02000037 RID: 55
	internal class IRenderGraphResource
	{
		// Token: 0x06000216 RID: 534 RVA: 0x0000BC60 File Offset: 0x00009E60
		public virtual void Reset(IRenderGraphResourcePool pool)
		{
			this.imported = false;
			this.shared = false;
			this.sharedExplicitRelease = false;
			this.cachedHash = -1;
			this.transientPassIndex = -1;
			this.sharedResourceLastFrameUsed = -1;
			this.requestFallBack = false;
			this.writeCount = 0U;
			this.m_Pool = pool;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x0000BCAC File Offset: 0x00009EAC
		public virtual string GetName()
		{
			return "";
		}

		// Token: 0x06000218 RID: 536 RVA: 0x0000BCB3 File Offset: 0x00009EB3
		public virtual bool IsCreated()
		{
			return false;
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000BCB6 File Offset: 0x00009EB6
		public virtual void IncrementWriteCount()
		{
			this.writeCount += 1U;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x0000BCC6 File Offset: 0x00009EC6
		public virtual bool NeedsFallBack()
		{
			return this.requestFallBack && this.writeCount == 0U;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000BCDB File Offset: 0x00009EDB
		public virtual void CreatePooledGraphicsResource()
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000BCDD File Offset: 0x00009EDD
		public virtual void CreateGraphicsResource(string name = "")
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000BCDF File Offset: 0x00009EDF
		public virtual void ReleasePooledGraphicsResource(int frameIndex)
		{
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000BCE1 File Offset: 0x00009EE1
		public virtual void ReleaseGraphicsResource()
		{
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000BCE3 File Offset: 0x00009EE3
		public virtual void LogCreation(RenderGraphLogger logger)
		{
		}

		// Token: 0x06000220 RID: 544 RVA: 0x0000BCE5 File Offset: 0x00009EE5
		public virtual void LogRelease(RenderGraphLogger logger)
		{
		}

		// Token: 0x06000221 RID: 545 RVA: 0x0000BCE7 File Offset: 0x00009EE7
		public virtual int GetSortIndex()
		{
			return 0;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x0000BCEA File Offset: 0x00009EEA
		public IRenderGraphResource()
		{
		}

		// Token: 0x04000153 RID: 339
		public bool imported;

		// Token: 0x04000154 RID: 340
		public bool shared;

		// Token: 0x04000155 RID: 341
		public bool sharedExplicitRelease;

		// Token: 0x04000156 RID: 342
		public bool requestFallBack;

		// Token: 0x04000157 RID: 343
		public uint writeCount;

		// Token: 0x04000158 RID: 344
		public int cachedHash;

		// Token: 0x04000159 RID: 345
		public int transientPassIndex;

		// Token: 0x0400015A RID: 346
		public int sharedResourceLastFrameUsed;

		// Token: 0x0400015B RID: 347
		protected IRenderGraphResourcePool m_Pool;
	}
}

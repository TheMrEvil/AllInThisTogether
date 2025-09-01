using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x020002DD RID: 733
	internal abstract class X509ChainImpl : IDisposable
	{
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600171F RID: 5919
		public abstract bool IsValid { get; }

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001720 RID: 5920
		public abstract IntPtr Handle { get; }

		// Token: 0x06001721 RID: 5921 RVA: 0x0005B7F1 File Offset: 0x000599F1
		protected void ThrowIfContextInvalid()
		{
			if (!this.IsValid)
			{
				throw X509Helper2.GetInvalidChainContextException();
			}
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001722 RID: 5922
		public abstract X509ChainElementCollection ChainElements { get; }

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001723 RID: 5923
		// (set) Token: 0x06001724 RID: 5924
		public abstract X509ChainPolicy ChainPolicy { get; set; }

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001725 RID: 5925
		public abstract X509ChainStatus[] ChainStatus { get; }

		// Token: 0x06001726 RID: 5926
		public abstract bool Build(X509Certificate2 certificate);

		// Token: 0x06001727 RID: 5927
		public abstract void AddStatus(X509ChainStatusFlags errorCode);

		// Token: 0x06001728 RID: 5928
		public abstract void Reset();

		// Token: 0x06001729 RID: 5929 RVA: 0x0005B801 File Offset: 0x00059A01
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600172A RID: 5930 RVA: 0x00003917 File Offset: 0x00001B17
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600172B RID: 5931 RVA: 0x0005B810 File Offset: 0x00059A10
		~X509ChainImpl()
		{
			this.Dispose(false);
		}

		// Token: 0x0600172C RID: 5932 RVA: 0x0000219B File Offset: 0x0000039B
		protected X509ChainImpl()
		{
		}
	}
}

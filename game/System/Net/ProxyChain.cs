using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Net
{
	// Token: 0x02000633 RID: 1587
	internal abstract class ProxyChain : IEnumerable<Uri>, IEnumerable, IDisposable
	{
		// Token: 0x06003222 RID: 12834 RVA: 0x000AD840 File Offset: 0x000ABA40
		protected ProxyChain(Uri destination)
		{
			this.m_Destination = destination;
		}

		// Token: 0x06003223 RID: 12835 RVA: 0x000AD85C File Offset: 0x000ABA5C
		public IEnumerator<Uri> GetEnumerator()
		{
			ProxyChain.ProxyEnumerator proxyEnumerator = new ProxyChain.ProxyEnumerator(this);
			if (this.m_MainEnumerator == null)
			{
				this.m_MainEnumerator = proxyEnumerator;
			}
			return proxyEnumerator;
		}

		// Token: 0x06003224 RID: 12836 RVA: 0x000AD880 File Offset: 0x000ABA80
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06003225 RID: 12837 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual void Dispose()
		{
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06003226 RID: 12838 RVA: 0x000AD888 File Offset: 0x000ABA88
		internal IEnumerator<Uri> Enumerator
		{
			get
			{
				if (this.m_MainEnumerator != null)
				{
					return this.m_MainEnumerator;
				}
				return this.GetEnumerator();
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06003227 RID: 12839 RVA: 0x000AD8AC File Offset: 0x000ABAAC
		internal Uri Destination
		{
			get
			{
				return this.m_Destination;
			}
		}

		// Token: 0x06003228 RID: 12840 RVA: 0x00003917 File Offset: 0x00001B17
		internal virtual void Abort()
		{
		}

		// Token: 0x06003229 RID: 12841 RVA: 0x000AD8B4 File Offset: 0x000ABAB4
		internal bool HttpAbort(HttpWebRequest request, WebException webException)
		{
			this.Abort();
			return true;
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x0600322A RID: 12842 RVA: 0x000AD8BD File Offset: 0x000ABABD
		internal HttpAbortDelegate HttpAbortDelegate
		{
			get
			{
				if (this.m_HttpAbortDelegate == null)
				{
					this.m_HttpAbortDelegate = new HttpAbortDelegate(this.HttpAbort);
				}
				return this.m_HttpAbortDelegate;
			}
		}

		// Token: 0x0600322B RID: 12843
		protected abstract bool GetNextProxy(out Uri proxy);

		// Token: 0x04001D4C RID: 7500
		private List<Uri> m_Cache = new List<Uri>();

		// Token: 0x04001D4D RID: 7501
		private bool m_CacheComplete;

		// Token: 0x04001D4E RID: 7502
		private ProxyChain.ProxyEnumerator m_MainEnumerator;

		// Token: 0x04001D4F RID: 7503
		private Uri m_Destination;

		// Token: 0x04001D50 RID: 7504
		private HttpAbortDelegate m_HttpAbortDelegate;

		// Token: 0x02000634 RID: 1588
		private class ProxyEnumerator : IEnumerator<Uri>, IDisposable, IEnumerator
		{
			// Token: 0x0600322C RID: 12844 RVA: 0x000AD8DF File Offset: 0x000ABADF
			internal ProxyEnumerator(ProxyChain chain)
			{
				this.m_Chain = chain;
			}

			// Token: 0x17000A11 RID: 2577
			// (get) Token: 0x0600322D RID: 12845 RVA: 0x000AD8F5 File Offset: 0x000ABAF5
			public Uri Current
			{
				get
				{
					if (this.m_Finished || this.m_CurrentIndex < 0)
					{
						throw new InvalidOperationException(SR.GetString("Enumeration has either not started or has already finished."));
					}
					return this.m_Chain.m_Cache[this.m_CurrentIndex];
				}
			}

			// Token: 0x17000A12 RID: 2578
			// (get) Token: 0x0600322E RID: 12846 RVA: 0x000AD92E File Offset: 0x000ABB2E
			object IEnumerator.Current
			{
				get
				{
					return this.Current;
				}
			}

			// Token: 0x0600322F RID: 12847 RVA: 0x000AD938 File Offset: 0x000ABB38
			public bool MoveNext()
			{
				if (this.m_Finished)
				{
					return false;
				}
				checked
				{
					this.m_CurrentIndex++;
					if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
					{
						return true;
					}
					if (this.m_Chain.m_CacheComplete)
					{
						this.m_Finished = true;
						return false;
					}
					List<Uri> cache = this.m_Chain.m_Cache;
					bool result;
					lock (cache)
					{
						if (this.m_Chain.m_Cache.Count > this.m_CurrentIndex)
						{
							result = true;
						}
						else if (this.m_Chain.m_CacheComplete)
						{
							this.m_Finished = true;
							result = false;
						}
						else
						{
							Uri uri;
							while (this.m_Chain.GetNextProxy(out uri))
							{
								if (uri == null)
								{
									if (this.m_TriedDirect)
									{
										continue;
									}
									this.m_TriedDirect = true;
								}
								this.m_Chain.m_Cache.Add(uri);
								return true;
							}
							this.m_Finished = true;
							this.m_Chain.m_CacheComplete = true;
							result = false;
						}
					}
					return result;
				}
			}

			// Token: 0x06003230 RID: 12848 RVA: 0x000ADA48 File Offset: 0x000ABC48
			public void Reset()
			{
				this.m_Finished = false;
				this.m_CurrentIndex = -1;
			}

			// Token: 0x06003231 RID: 12849 RVA: 0x00003917 File Offset: 0x00001B17
			public void Dispose()
			{
			}

			// Token: 0x04001D51 RID: 7505
			private ProxyChain m_Chain;

			// Token: 0x04001D52 RID: 7506
			private bool m_Finished;

			// Token: 0x04001D53 RID: 7507
			private int m_CurrentIndex = -1;

			// Token: 0x04001D54 RID: 7508
			private bool m_TriedDirect;
		}
	}
}

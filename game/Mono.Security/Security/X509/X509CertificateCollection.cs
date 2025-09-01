using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x02000015 RID: 21
	[Serializable]
	public class X509CertificateCollection : CollectionBase, IEnumerable
	{
		// Token: 0x06000110 RID: 272 RVA: 0x00009E2A File Offset: 0x0000802A
		public X509CertificateCollection()
		{
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00009E32 File Offset: 0x00008032
		public X509CertificateCollection(X509Certificate[] value)
		{
			this.AddRange(value);
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00009E41 File Offset: 0x00008041
		public X509CertificateCollection(X509CertificateCollection value)
		{
			this.AddRange(value);
		}

		// Token: 0x17000041 RID: 65
		public X509Certificate this[int index]
		{
			get
			{
				return (X509Certificate)base.InnerList[index];
			}
			set
			{
				base.InnerList[index] = value;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00009E72 File Offset: 0x00008072
		public int Add(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return base.InnerList.Add(value);
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00009E90 File Offset: 0x00008090
		public void AddRange(X509Certificate[] value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.Length; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00009EC8 File Offset: 0x000080C8
		public void AddRange(X509CertificateCollection value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			for (int i = 0; i < value.InnerList.Count; i++)
			{
				base.InnerList.Add(value[i]);
			}
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009F0C File Offset: 0x0000810C
		public bool Contains(X509Certificate value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00009F1B File Offset: 0x0000811B
		public void CopyTo(X509Certificate[] array, int index)
		{
			base.InnerList.CopyTo(array, index);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00009F2A File Offset: 0x0000812A
		public new X509CertificateCollection.X509CertificateEnumerator GetEnumerator()
		{
			return new X509CertificateCollection.X509CertificateEnumerator(this);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00009F32 File Offset: 0x00008132
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00009F3F File Offset: 0x0000813F
		public override int GetHashCode()
		{
			return base.InnerList.GetHashCode();
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009F4C File Offset: 0x0000814C
		public int IndexOf(X509Certificate value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			byte[] hash = value.Hash;
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Certificate x509Certificate = (X509Certificate)base.InnerList[i];
				if (this.Compare(x509Certificate.Hash, hash))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00009FA8 File Offset: 0x000081A8
		public void Insert(int index, X509Certificate value)
		{
			base.InnerList.Insert(index, value);
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009FB7 File Offset: 0x000081B7
		public void Remove(X509Certificate value)
		{
			base.InnerList.Remove(value);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009FC8 File Offset: 0x000081C8
		private bool Compare(byte[] array1, byte[] array2)
		{
			if (array1 == null && array2 == null)
			{
				return true;
			}
			if (array1 == null || array2 == null)
			{
				return false;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x02000087 RID: 135
		public class X509CertificateEnumerator : IEnumerator
		{
			// Token: 0x06000532 RID: 1330 RVA: 0x00019871 File Offset: 0x00017A71
			public X509CertificateEnumerator(X509CertificateCollection mappings)
			{
				this.enumerator = ((IEnumerable)mappings).GetEnumerator();
			}

			// Token: 0x17000166 RID: 358
			// (get) Token: 0x06000533 RID: 1331 RVA: 0x00019885 File Offset: 0x00017A85
			public X509Certificate Current
			{
				get
				{
					return (X509Certificate)this.enumerator.Current;
				}
			}

			// Token: 0x17000167 RID: 359
			// (get) Token: 0x06000534 RID: 1332 RVA: 0x00019897 File Offset: 0x00017A97
			object IEnumerator.Current
			{
				get
				{
					return this.enumerator.Current;
				}
			}

			// Token: 0x06000535 RID: 1333 RVA: 0x000198A4 File Offset: 0x00017AA4
			bool IEnumerator.MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06000536 RID: 1334 RVA: 0x000198B1 File Offset: 0x00017AB1
			void IEnumerator.Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x06000537 RID: 1335 RVA: 0x000198BE File Offset: 0x00017ABE
			public bool MoveNext()
			{
				return this.enumerator.MoveNext();
			}

			// Token: 0x06000538 RID: 1336 RVA: 0x000198CB File Offset: 0x00017ACB
			public void Reset()
			{
				this.enumerator.Reset();
			}

			// Token: 0x040003D0 RID: 976
			private IEnumerator enumerator;
		}
	}
}

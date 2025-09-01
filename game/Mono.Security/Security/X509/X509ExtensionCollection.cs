using System;
using System.Collections;

namespace Mono.Security.X509
{
	// Token: 0x02000019 RID: 25
	public sealed class X509ExtensionCollection : CollectionBase, IEnumerable
	{
		// Token: 0x06000142 RID: 322 RVA: 0x0000A877 File Offset: 0x00008A77
		public X509ExtensionCollection()
		{
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000A880 File Offset: 0x00008A80
		public X509ExtensionCollection(ASN1 asn1) : this()
		{
			this.readOnly = true;
			if (asn1 == null)
			{
				return;
			}
			if (asn1.Tag != 48)
			{
				throw new Exception("Invalid extensions format");
			}
			for (int i = 0; i < asn1.Count; i++)
			{
				X509Extension value = new X509Extension(asn1[i]);
				base.InnerList.Add(value);
			}
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000A8DE File Offset: 0x00008ADE
		public int Add(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (this.readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			return base.InnerList.Add(extension);
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000A910 File Offset: 0x00008B10
		public void AddRange(X509Extension[] extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			if (this.readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			for (int i = 0; i < extension.Length; i++)
			{
				base.InnerList.Add(extension[i]);
			}
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000A95C File Offset: 0x00008B5C
		public void AddRange(X509ExtensionCollection collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			if (this.readOnly)
			{
				throw new NotSupportedException("Extensions are read only");
			}
			for (int i = 0; i < collection.InnerList.Count; i++)
			{
				base.InnerList.Add(collection[i]);
			}
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000A9B3 File Offset: 0x00008BB3
		public bool Contains(X509Extension extension)
		{
			return this.IndexOf(extension) != -1;
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000A9C2 File Offset: 0x00008BC2
		public bool Contains(string oid)
		{
			return this.IndexOf(oid) != -1;
		}

		// Token: 0x06000149 RID: 329 RVA: 0x0000A9D1 File Offset: 0x00008BD1
		public void CopyTo(X509Extension[] extensions, int index)
		{
			if (extensions == null)
			{
				throw new ArgumentNullException("extensions");
			}
			base.InnerList.CopyTo(extensions, index);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		public int IndexOf(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (((X509Extension)base.InnerList[i]).Equals(extension))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x0000AA40 File Offset: 0x00008C40
		public int IndexOf(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				if (((X509Extension)base.InnerList[i]).Oid == oid)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000AA92 File Offset: 0x00008C92
		public void Insert(int index, X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Insert(index, extension);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000AAAF File Offset: 0x00008CAF
		public void Remove(X509Extension extension)
		{
			if (extension == null)
			{
				throw new ArgumentNullException("extension");
			}
			base.InnerList.Remove(extension);
		}

		// Token: 0x0600014E RID: 334 RVA: 0x0000AACC File Offset: 0x00008CCC
		public void Remove(string oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			int num = this.IndexOf(oid);
			if (num != -1)
			{
				base.InnerList.RemoveAt(num);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000AAFF File Offset: 0x00008CFF
		IEnumerator IEnumerable.GetEnumerator()
		{
			return base.InnerList.GetEnumerator();
		}

		// Token: 0x1700004B RID: 75
		public X509Extension this[int index]
		{
			get
			{
				return (X509Extension)base.InnerList[index];
			}
		}

		// Token: 0x1700004C RID: 76
		public X509Extension this[string oid]
		{
			get
			{
				int num = this.IndexOf(oid);
				if (num == -1)
				{
					return null;
				}
				return (X509Extension)base.InnerList[num];
			}
		}

		// Token: 0x06000152 RID: 338 RVA: 0x0000AB4C File Offset: 0x00008D4C
		public byte[] GetBytes()
		{
			if (base.InnerList.Count < 1)
			{
				return null;
			}
			ASN1 asn = new ASN1(48);
			for (int i = 0; i < base.InnerList.Count; i++)
			{
				X509Extension x509Extension = (X509Extension)base.InnerList[i];
				asn.Add(x509Extension.ASN1);
			}
			return asn.GetBytes();
		}

		// Token: 0x040000C1 RID: 193
		private bool readOnly;
	}
}

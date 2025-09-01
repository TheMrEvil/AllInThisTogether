using System;
using System.Net;
using System.Security.Permissions;
using Mono.Security.X509.Extensions;

namespace Mono.Security.X509
{
	// Token: 0x02000016 RID: 22
	public class X509Chain
	{
		// Token: 0x06000121 RID: 289 RVA: 0x0000A008 File Offset: 0x00008208
		public X509Chain()
		{
			this.certs = new X509CertificateCollection();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000A01B File Offset: 0x0000821B
		public X509Chain(X509CertificateCollection chain) : this()
		{
			this._chain = new X509CertificateCollection();
			this._chain.AddRange(chain);
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000123 RID: 291 RVA: 0x0000A03A File Offset: 0x0000823A
		public X509CertificateCollection Chain
		{
			get
			{
				return this._chain;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000124 RID: 292 RVA: 0x0000A042 File Offset: 0x00008242
		public X509Certificate Root
		{
			get
			{
				return this._root;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000125 RID: 293 RVA: 0x0000A04A File Offset: 0x0000824A
		public X509ChainStatusFlags Status
		{
			get
			{
				return this._status;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000126 RID: 294 RVA: 0x0000A052 File Offset: 0x00008252
		// (set) Token: 0x06000127 RID: 295 RVA: 0x0000A084 File Offset: 0x00008284
		public X509CertificateCollection TrustAnchors
		{
			get
			{
				if (this.roots == null)
				{
					this.roots = new X509CertificateCollection();
					this.roots.AddRange(X509StoreManager.TrustedRootCertificates);
					return this.roots;
				}
				return this.roots;
			}
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.ControlPolicy)]
			set
			{
				this.roots = value;
			}
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000A08D File Offset: 0x0000828D
		public void LoadCertificate(X509Certificate x509)
		{
			this.certs.Add(x509);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000A09C File Offset: 0x0000829C
		public void LoadCertificates(X509CertificateCollection collection)
		{
			this.certs.AddRange(collection);
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000A0AC File Offset: 0x000082AC
		public X509Certificate FindByIssuerName(string issuerName)
		{
			foreach (X509Certificate x509Certificate in this.certs)
			{
				if (x509Certificate.IssuerName == issuerName)
				{
					return x509Certificate;
				}
			}
			return null;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A110 File Offset: 0x00008310
		public bool Build(X509Certificate leaf)
		{
			this._status = X509ChainStatusFlags.NoError;
			if (this._chain == null)
			{
				this._chain = new X509CertificateCollection();
				X509Certificate x509Certificate = leaf;
				X509Certificate potentialRoot = x509Certificate;
				while (x509Certificate != null && !x509Certificate.IsSelfSigned)
				{
					potentialRoot = x509Certificate;
					this._chain.Add(x509Certificate);
					x509Certificate = this.FindCertificateParent(x509Certificate);
				}
				this._root = this.FindCertificateRoot(potentialRoot);
			}
			else
			{
				int count = this._chain.Count;
				if (count > 0)
				{
					if (this.IsParent(leaf, this._chain[0]))
					{
						int num = 1;
						while (num < count && this.IsParent(this._chain[num - 1], this._chain[num]))
						{
							num++;
						}
						if (num == count)
						{
							this._root = this.FindCertificateRoot(this._chain[count - 1]);
						}
					}
				}
				else
				{
					this._root = this.FindCertificateRoot(leaf);
				}
			}
			if (this._chain != null && this._status == X509ChainStatusFlags.NoError)
			{
				foreach (X509Certificate cert in this._chain)
				{
					if (!this.IsValid(cert))
					{
						return false;
					}
				}
				if (!this.IsValid(leaf))
				{
					if (this._status == X509ChainStatusFlags.NotTimeNested)
					{
						this._status = X509ChainStatusFlags.NotTimeValid;
					}
					return false;
				}
				if (this._root != null && !this.IsValid(this._root))
				{
					return false;
				}
			}
			return this._status == X509ChainStatusFlags.NoError;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A29C File Offset: 0x0000849C
		public void Reset()
		{
			this._status = X509ChainStatusFlags.NoError;
			this.roots = null;
			this.certs.Clear();
			if (this._chain != null)
			{
				this._chain.Clear();
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A2CA File Offset: 0x000084CA
		private bool IsValid(X509Certificate cert)
		{
			if (!cert.IsCurrent)
			{
				this._status = X509ChainStatusFlags.NotTimeNested;
				return false;
			}
			bool checkCertificateRevocationList = ServicePointManager.CheckCertificateRevocationList;
			return true;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A2E4 File Offset: 0x000084E4
		private X509Certificate FindCertificateParent(X509Certificate child)
		{
			foreach (X509Certificate x509Certificate in this.certs)
			{
				if (this.IsParent(child, x509Certificate))
				{
					return x509Certificate;
				}
			}
			return null;
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000A344 File Offset: 0x00008544
		private X509Certificate FindCertificateRoot(X509Certificate potentialRoot)
		{
			if (potentialRoot == null)
			{
				this._status = X509ChainStatusFlags.PartialChain;
				return null;
			}
			if (this.IsTrusted(potentialRoot))
			{
				return potentialRoot;
			}
			foreach (X509Certificate x509Certificate in this.TrustAnchors)
			{
				if (this.IsParent(potentialRoot, x509Certificate))
				{
					return x509Certificate;
				}
			}
			if (potentialRoot.IsSelfSigned)
			{
				this._status = X509ChainStatusFlags.UntrustedRoot;
				return potentialRoot;
			}
			this._status = X509ChainStatusFlags.PartialChain;
			return null;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A3DC File Offset: 0x000085DC
		private bool IsTrusted(X509Certificate potentialTrusted)
		{
			return this.TrustAnchors.Contains(potentialTrusted);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A3EC File Offset: 0x000085EC
		private bool IsParent(X509Certificate child, X509Certificate parent)
		{
			if (child.IssuerName != parent.SubjectName)
			{
				return false;
			}
			if (parent.Version > 2 && !this.IsTrusted(parent))
			{
				X509Extension x509Extension = parent.Extensions["2.5.29.19"];
				if (x509Extension != null)
				{
					if (!new BasicConstraintsExtension(x509Extension).CertificateAuthority)
					{
						this._status = X509ChainStatusFlags.InvalidBasicConstraints;
					}
				}
				else
				{
					this._status = X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			if (!child.VerifySignature(parent.RSA))
			{
				this._status = X509ChainStatusFlags.NotSignatureValid;
				return false;
			}
			return true;
		}

		// Token: 0x040000B1 RID: 177
		private X509CertificateCollection roots;

		// Token: 0x040000B2 RID: 178
		private X509CertificateCollection certs;

		// Token: 0x040000B3 RID: 179
		private X509Certificate _root;

		// Token: 0x040000B4 RID: 180
		private X509CertificateCollection _chain;

		// Token: 0x040000B5 RID: 181
		private X509ChainStatusFlags _status;
	}
}

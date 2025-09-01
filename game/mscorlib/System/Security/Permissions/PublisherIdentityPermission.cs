using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.Cryptography;

namespace System.Security.Permissions
{
	/// <summary>Represents the identity of a software publisher. This class cannot be inherited.</summary>
	// Token: 0x0200044F RID: 1103
	[ComVisible(true)]
	[Serializable]
	public sealed class PublisherIdentityPermission : CodeAccessPermission, IBuiltInPermission
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> class with the specified <see cref="T:System.Security.Permissions.PermissionState" />.</summary>
		/// <param name="state">One of the <see cref="T:System.Security.Permissions.PermissionState" /> values.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="state" /> parameter is not a valid value of <see cref="T:System.Security.Permissions.PermissionState" />.</exception>
		// Token: 0x06002CBA RID: 11450 RVA: 0x0009EC34 File Offset: 0x0009CE34
		public PublisherIdentityPermission(PermissionState state)
		{
			CodeAccessPermission.CheckPermissionState(state, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> class with the specified Authenticode X.509v3 certificate.</summary>
		/// <param name="certificate">An X.509 certificate representing the software publisher's identity.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="certificate" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="certificate" /> parameter is not a valid certificate.</exception>
		// Token: 0x06002CBB RID: 11451 RVA: 0x000A0955 File Offset: 0x0009EB55
		public PublisherIdentityPermission(X509Certificate certificate)
		{
			this.Certificate = certificate;
		}

		/// <summary>Gets or sets an Authenticode X.509v3 certificate that represents the identity of the software publisher.</summary>
		/// <returns>An X.509 certificate representing the identity of the software publisher.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Permissions.PublisherIdentityPermission.Certificate" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <see cref="P:System.Security.Permissions.PublisherIdentityPermission.Certificate" /> is not a valid certificate.</exception>
		/// <exception cref="T:System.NotSupportedException">The property cannot be set because the identity is ambiguous.</exception>
		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06002CBC RID: 11452 RVA: 0x000A0964 File Offset: 0x0009EB64
		// (set) Token: 0x06002CBD RID: 11453 RVA: 0x000A096C File Offset: 0x0009EB6C
		public X509Certificate Certificate
		{
			get
			{
				return this.x509;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("X509Certificate");
				}
				this.x509 = value;
			}
		}

		/// <summary>Creates and returns an identical copy of the current permission.</summary>
		/// <returns>A copy of the current permission.</returns>
		// Token: 0x06002CBE RID: 11454 RVA: 0x000A0984 File Offset: 0x0009EB84
		public override IPermission Copy()
		{
			PublisherIdentityPermission publisherIdentityPermission = new PublisherIdentityPermission(PermissionState.None);
			if (this.x509 != null)
			{
				publisherIdentityPermission.Certificate = this.x509;
			}
			return publisherIdentityPermission;
		}

		/// <summary>Reconstructs a permission with a specified state from an XML encoding.</summary>
		/// <param name="esd">The XML encoding to use to reconstruct the permission.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="esd" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="esd" /> parameter is not a valid permission element.  
		///  -or-  
		///  The <paramref name="esd" /> parameter's version number is not valid.</exception>
		// Token: 0x06002CBF RID: 11455 RVA: 0x000A09B0 File Offset: 0x0009EBB0
		public override void FromXml(SecurityElement esd)
		{
			CodeAccessPermission.CheckSecurityElement(esd, "esd", 1, 1);
			string text = esd.Attributes["X509v3Certificate"] as string;
			if (text != null)
			{
				byte[] data = CryptoConvert.FromHex(text);
				this.x509 = new X509Certificate(data);
			}
		}

		/// <summary>Creates and returns a permission that is the intersection of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to intersect with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the intersection of the current permission and the specified permission. This new permission is <see langword="null" /> if the intersection is empty.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002CC0 RID: 11456 RVA: 0x000A09F8 File Offset: 0x0009EBF8
		public override IPermission Intersect(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			if (publisherIdentityPermission == null)
			{
				return null;
			}
			if (this.x509 != null && publisherIdentityPermission.x509 != null && this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString())
			{
				return new PublisherIdentityPermission(publisherIdentityPermission.x509);
			}
			return null;
		}

		/// <summary>Determines whether the current permission is a subset of the specified permission.</summary>
		/// <param name="target">A permission that is to be tested for the subset relationship. This permission must be of the same type as the current permission.</param>
		/// <returns>
		///   <see langword="true" /> if the current permission is a subset of the specified permission; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.</exception>
		// Token: 0x06002CC1 RID: 11457 RVA: 0x000A0A4C File Offset: 0x0009EC4C
		public override bool IsSubsetOf(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			return publisherIdentityPermission != null && (this.x509 == null || (publisherIdentityPermission.x509 != null && this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString()));
		}

		/// <summary>Creates an XML encoding of the permission and its current state.</summary>
		/// <returns>An XML encoding of the permission, including any state information.</returns>
		// Token: 0x06002CC2 RID: 11458 RVA: 0x000A0A98 File Offset: 0x0009EC98
		public override SecurityElement ToXml()
		{
			SecurityElement securityElement = base.Element(1);
			if (this.x509 != null)
			{
				securityElement.AddAttribute("X509v3Certificate", this.x509.GetRawCertDataString());
			}
			return securityElement;
		}

		/// <summary>Creates a permission that is the union of the current permission and the specified permission.</summary>
		/// <param name="target">A permission to combine with the current permission. It must be of the same type as the current permission.</param>
		/// <returns>A new permission that represents the union of the current permission and the specified permission.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="target" /> parameter is not <see langword="null" /> and is not of the same type as the current permission.  
		///  -or-  
		///  The two permissions are not equal.</exception>
		// Token: 0x06002CC3 RID: 11459 RVA: 0x000A0ACC File Offset: 0x0009ECCC
		public override IPermission Union(IPermission target)
		{
			PublisherIdentityPermission publisherIdentityPermission = this.Cast(target);
			if (publisherIdentityPermission == null)
			{
				return this.Copy();
			}
			if (this.x509 != null && publisherIdentityPermission.x509 != null)
			{
				if (this.x509.GetRawCertDataString() == publisherIdentityPermission.x509.GetRawCertDataString())
				{
					return new PublisherIdentityPermission(this.x509);
				}
			}
			else
			{
				if (this.x509 == null && publisherIdentityPermission.x509 != null)
				{
					return new PublisherIdentityPermission(publisherIdentityPermission.x509);
				}
				if (this.x509 != null && publisherIdentityPermission.x509 == null)
				{
					return new PublisherIdentityPermission(this.x509);
				}
			}
			return null;
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x000481C2 File Offset: 0x000463C2
		int IBuiltInPermission.GetTokenIndex()
		{
			return 10;
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x000A0B5D File Offset: 0x0009ED5D
		private PublisherIdentityPermission Cast(IPermission target)
		{
			if (target == null)
			{
				return null;
			}
			PublisherIdentityPermission publisherIdentityPermission = target as PublisherIdentityPermission;
			if (publisherIdentityPermission == null)
			{
				CodeAccessPermission.ThrowInvalidPermission(target, typeof(PublisherIdentityPermission));
			}
			return publisherIdentityPermission;
		}

		// Token: 0x04002074 RID: 8308
		private const int version = 1;

		// Token: 0x04002075 RID: 8309
		private X509Certificate x509;
	}
}

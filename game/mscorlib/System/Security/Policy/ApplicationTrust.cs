using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Permissions;
using Mono.Security.Cryptography;

namespace System.Security.Policy
{
	/// <summary>Encapsulates security decisions about an application. This class cannot be inherited.</summary>
	// Token: 0x02000403 RID: 1027
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationTrust : EvidenceBase, ISecurityEncodable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class.</summary>
		// Token: 0x060029F0 RID: 10736 RVA: 0x000981E2 File Offset: 0x000963E2
		public ApplicationTrust()
		{
			this.fullTrustAssemblies = new List<StrongName>(0);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class with an <see cref="T:System.ApplicationIdentity" />.</summary>
		/// <param name="applicationIdentity">An <see cref="T:System.ApplicationIdentity" /> that uniquely identifies an application.</param>
		// Token: 0x060029F1 RID: 10737 RVA: 0x000981F6 File Offset: 0x000963F6
		public ApplicationTrust(ApplicationIdentity applicationIdentity) : this()
		{
			if (applicationIdentity == null)
			{
				throw new ArgumentNullException("applicationIdentity");
			}
			this._appid = applicationIdentity;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.ApplicationTrust" /> class using the provided grant set and collection of full-trust assemblies.</summary>
		/// <param name="defaultGrantSet">A default permission set that is granted to all assemblies that do not have specific grants.</param>
		/// <param name="fullTrustAssemblies">An array of strong names that represent assemblies that should be considered fully trusted in an application domain.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="fullTrustAssemblies" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="fullTrustAssemblies" /> contains an assembly that does not have a <see cref="T:System.Security.Policy.StrongName" />.</exception>
		// Token: 0x060029F2 RID: 10738 RVA: 0x00098214 File Offset: 0x00096414
		public ApplicationTrust(PermissionSet defaultGrantSet, IEnumerable<StrongName> fullTrustAssemblies)
		{
			if (defaultGrantSet == null)
			{
				throw new ArgumentNullException("defaultGrantSet");
			}
			this._defaultPolicy = new PolicyStatement(defaultGrantSet);
			if (fullTrustAssemblies == null)
			{
				throw new ArgumentNullException("fullTrustAssemblies");
			}
			this.fullTrustAssemblies = new List<StrongName>();
			foreach (StrongName strongName in fullTrustAssemblies)
			{
				if (strongName == null)
				{
					throw new ArgumentException("fullTrustAssemblies contains an assembly that does not have a StrongName");
				}
				this.fullTrustAssemblies.Add((StrongName)strongName.Copy());
			}
		}

		/// <summary>Gets or sets the application identity for the application trust object.</summary>
		/// <returns>An <see cref="T:System.ApplicationIdentity" /> for the application trust object.</returns>
		/// <exception cref="T:System.ArgumentNullException">
		///   <see cref="T:System.ApplicationIdentity" /> cannot be set because it has a value of <see langword="null" />.</exception>
		// Token: 0x17000526 RID: 1318
		// (get) Token: 0x060029F3 RID: 10739 RVA: 0x000982B4 File Offset: 0x000964B4
		// (set) Token: 0x060029F4 RID: 10740 RVA: 0x000982BC File Offset: 0x000964BC
		public ApplicationIdentity ApplicationIdentity
		{
			get
			{
				return this._appid;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("ApplicationIdentity");
				}
				this._appid = value;
			}
		}

		/// <summary>Gets or sets the policy statement defining the default grant set.</summary>
		/// <returns>A <see cref="T:System.Security.Policy.PolicyStatement" /> describing the default grants.</returns>
		// Token: 0x17000527 RID: 1319
		// (get) Token: 0x060029F5 RID: 10741 RVA: 0x000982D3 File Offset: 0x000964D3
		// (set) Token: 0x060029F6 RID: 10742 RVA: 0x000982EF File Offset: 0x000964EF
		public PolicyStatement DefaultGrantSet
		{
			get
			{
				if (this._defaultPolicy == null)
				{
					this._defaultPolicy = this.GetDefaultGrantSet();
				}
				return this._defaultPolicy;
			}
			set
			{
				this._defaultPolicy = value;
			}
		}

		/// <summary>Gets or sets extra security information about the application.</summary>
		/// <returns>An object containing additional security information about the application.</returns>
		// Token: 0x17000528 RID: 1320
		// (get) Token: 0x060029F7 RID: 10743 RVA: 0x000982F8 File Offset: 0x000964F8
		// (set) Token: 0x060029F8 RID: 10744 RVA: 0x00098300 File Offset: 0x00096500
		public object ExtraInfo
		{
			get
			{
				return this._xtranfo;
			}
			set
			{
				this._xtranfo = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the application has the required permission grants and is trusted to run.</summary>
		/// <returns>
		///   <see langword="true" /> if the application is trusted to run; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x17000529 RID: 1321
		// (get) Token: 0x060029F9 RID: 10745 RVA: 0x00098309 File Offset: 0x00096509
		// (set) Token: 0x060029FA RID: 10746 RVA: 0x00098311 File Offset: 0x00096511
		public bool IsApplicationTrustedToRun
		{
			get
			{
				return this._trustrun;
			}
			set
			{
				this._trustrun = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether application trust information is persisted.</summary>
		/// <returns>
		///   <see langword="true" /> if application trust information is persisted; otherwise, <see langword="false" />. The default is <see langword="false" />.</returns>
		// Token: 0x1700052A RID: 1322
		// (get) Token: 0x060029FB RID: 10747 RVA: 0x0009831A File Offset: 0x0009651A
		// (set) Token: 0x060029FC RID: 10748 RVA: 0x00098322 File Offset: 0x00096522
		public bool Persist
		{
			get
			{
				return this._persist;
			}
			set
			{
				this._persist = value;
			}
		}

		/// <summary>Reconstructs an <see cref="T:System.Security.Policy.ApplicationTrust" /> object with a given state from an XML encoding.</summary>
		/// <param name="element">The XML encoding to use to reconstruct the <see cref="T:System.Security.Policy.ApplicationTrust" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="element" /> is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The XML encoding used for <paramref name="element" /> is invalid.</exception>
		// Token: 0x060029FD RID: 10749 RVA: 0x0009832C File Offset: 0x0009652C
		public void FromXml(SecurityElement element)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Tag != "ApplicationTrust")
			{
				throw new ArgumentException("element");
			}
			string text = element.Attribute("FullName");
			if (text != null)
			{
				this._appid = new ApplicationIdentity(text);
			}
			else
			{
				this._appid = null;
			}
			this._defaultPolicy = null;
			SecurityElement securityElement = element.SearchForChildByTag("DefaultGrant");
			if (securityElement != null)
			{
				for (int i = 0; i < securityElement.Children.Count; i++)
				{
					SecurityElement securityElement2 = securityElement.Children[i] as SecurityElement;
					if (securityElement2.Tag == "PolicyStatement")
					{
						this.DefaultGrantSet.FromXml(securityElement2, null);
						break;
					}
				}
			}
			if (!bool.TryParse(element.Attribute("TrustedToRun"), out this._trustrun))
			{
				this._trustrun = false;
			}
			if (!bool.TryParse(element.Attribute("Persist"), out this._persist))
			{
				this._persist = false;
			}
			this._xtranfo = null;
			SecurityElement securityElement3 = element.SearchForChildByTag("ExtraInfo");
			if (securityElement3 != null)
			{
				text = securityElement3.Attribute("Data");
				if (text != null)
				{
					using (MemoryStream memoryStream = new MemoryStream(CryptoConvert.FromHex(text)))
					{
						BinaryFormatter binaryFormatter = new BinaryFormatter();
						this._xtranfo = binaryFormatter.Deserialize(memoryStream);
					}
				}
			}
		}

		/// <summary>Creates an XML encoding of the <see cref="T:System.Security.Policy.ApplicationTrust" /> object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x060029FE RID: 10750 RVA: 0x00098490 File Offset: 0x00096690
		public SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("ApplicationTrust");
			securityElement.AddAttribute("version", "1");
			if (this._appid != null)
			{
				securityElement.AddAttribute("FullName", this._appid.FullName);
			}
			if (this._trustrun)
			{
				securityElement.AddAttribute("TrustedToRun", "true");
			}
			if (this._persist)
			{
				securityElement.AddAttribute("Persist", "true");
			}
			SecurityElement securityElement2 = new SecurityElement("DefaultGrant");
			securityElement2.AddChild(this.DefaultGrantSet.ToXml());
			securityElement.AddChild(securityElement2);
			if (this._xtranfo != null)
			{
				byte[] input = null;
				using (MemoryStream memoryStream = new MemoryStream())
				{
					new BinaryFormatter().Serialize(memoryStream, this._xtranfo);
					input = memoryStream.ToArray();
				}
				SecurityElement securityElement3 = new SecurityElement("ExtraInfo");
				securityElement3.AddAttribute("Data", CryptoConvert.ToHex(input));
				securityElement.AddChild(securityElement3);
			}
			return securityElement;
		}

		/// <summary>Gets the list of full-trust assemblies for this application trust.</summary>
		/// <returns>A list of full-trust assemblies.</returns>
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x060029FF RID: 10751 RVA: 0x00098598 File Offset: 0x00096798
		public IList<StrongName> FullTrustAssemblies
		{
			get
			{
				return this.fullTrustAssemblies;
			}
		}

		// Token: 0x06002A00 RID: 10752 RVA: 0x000985A0 File Offset: 0x000967A0
		private PolicyStatement GetDefaultGrantSet()
		{
			return new PolicyStatement(new PermissionSet(PermissionState.None));
		}

		// Token: 0x04001F5B RID: 8027
		private ApplicationIdentity _appid;

		// Token: 0x04001F5C RID: 8028
		private PolicyStatement _defaultPolicy;

		// Token: 0x04001F5D RID: 8029
		private object _xtranfo;

		// Token: 0x04001F5E RID: 8030
		private bool _trustrun;

		// Token: 0x04001F5F RID: 8031
		private bool _persist;

		// Token: 0x04001F60 RID: 8032
		private IList<StrongName> fullTrustAssemblies;
	}
}

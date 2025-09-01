using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the URL from which a code assembly originates as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000425 RID: 1061
	[ComVisible(true)]
	[Serializable]
	public sealed class Url : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Url" /> class with the URL from which a code assembly originates.</summary>
		/// <param name="name">The URL of origin for the associated code assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B66 RID: 11110 RVA: 0x0009CDB8 File Offset: 0x0009AFB8
		public Url(string name) : this(name, false)
		{
		}

		// Token: 0x06002B67 RID: 11111 RVA: 0x0009CDC2 File Offset: 0x0009AFC2
		internal Url(string name, bool validated)
		{
			this.origin_url = (validated ? name : this.Prepare(name));
		}

		/// <summary>Creates a new copy of the evidence object.</summary>
		/// <returns>A new, identical copy of the evidence object.</returns>
		// Token: 0x06002B68 RID: 11112 RVA: 0x0009CDDD File Offset: 0x0009AFDD
		public object Copy()
		{
			return new Url(this.origin_url, true);
		}

		/// <summary>Creates an identity permission corresponding to the current instance of the <see cref="T:System.Security.Policy.Url" /> evidence class.</summary>
		/// <param name="evidence">The evidence set from which to construct the identity permission.</param>
		/// <returns>A <see cref="T:System.Security.Permissions.UrlIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Url" /> evidence.</returns>
		// Token: 0x06002B69 RID: 11113 RVA: 0x0009CDEB File Offset: 0x0009AFEB
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new UrlIdentityPermission(this.origin_url);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Url" /> evidence object to the specified object for equivalence.</summary>
		/// <param name="o">The <see cref="T:System.Security.Policy.Url" /> evidence object to test for equivalence with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Security.Policy.Url" /> objects are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B6A RID: 11114 RVA: 0x0009CDF8 File Offset: 0x0009AFF8
		public override bool Equals(object o)
		{
			Url url = o as Url;
			if (url == null)
			{
				return false;
			}
			string text = url.Value;
			string text2 = this.origin_url;
			if (text.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text = "file://" + text;
			}
			if (text2.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text2 = "file://" + text2;
			}
			return string.Compare(text, text2, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Gets the hash code of the current URL.</summary>
		/// <returns>The hash code of the current URL.</returns>
		// Token: 0x06002B6B RID: 11115 RVA: 0x0009CE64 File Offset: 0x0009B064
		public override int GetHashCode()
		{
			string text = this.origin_url;
			if (text.IndexOf(Uri.SchemeDelimiter) < 0)
			{
				text = "file://" + text;
			}
			return text.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Url" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Url" />.</returns>
		// Token: 0x06002B6C RID: 11116 RVA: 0x0009CE98 File Offset: 0x0009B098
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Url");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Url", this.origin_url));
			return securityElement.ToString();
		}

		/// <summary>Gets the URL from which the code assembly originates.</summary>
		/// <returns>The URL from which the code assembly originates.</returns>
		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06002B6D RID: 11117 RVA: 0x0009CECF File Offset: 0x0009B0CF
		public string Value
		{
			get
			{
				return this.origin_url;
			}
		}

		// Token: 0x06002B6E RID: 11118 RVA: 0x0009CED7 File Offset: 0x0009B0D7
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 3 : 1) + this.origin_url.Length;
		}

		// Token: 0x06002B6F RID: 11119 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06002B70 RID: 11120 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x0009CEEC File Offset: 0x0009B0EC
		private string Prepare(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("Url");
			}
			if (url == string.Empty)
			{
				throw new FormatException(Locale.GetText("Invalid (empty) Url"));
			}
			if (url.IndexOf(Uri.SchemeDelimiter) > 0)
			{
				if (url.StartsWith("file://"))
				{
					url = "file://" + url.Substring(7);
				}
				url = new Uri(url, false, false).ToString();
			}
			int num = url.Length - 1;
			if (url[num] == '/')
			{
				url = url.Substring(0, num);
			}
			return url;
		}

		// Token: 0x04001FC6 RID: 8134
		private string origin_url;
	}
}

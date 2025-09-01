using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the Web site from which a code assembly originates as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200041E RID: 1054
	[ComVisible(true)]
	[Serializable]
	public sealed class Site : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Site" /> class with the website from which a code assembly originates.</summary>
		/// <param name="name">The website of origin for the associated code assembly.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B19 RID: 11033 RVA: 0x0009C1F4 File Offset: 0x0009A3F4
		public Site(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("url");
			}
			if (!Site.IsValid(name))
			{
				throw new ArgumentException(Locale.GetText("name is not valid"));
			}
			this.origin_site = name;
		}

		/// <summary>Creates a new <see cref="T:System.Security.Policy.Site" /> object from the specified URL.</summary>
		/// <param name="url">The URL from which to create the new <see cref="T:System.Security.Policy.Site" /> object.</param>
		/// <returns>A new site object.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="url" /> parameter is not a valid URL.  
		///  -or-  
		///  The <paramref name="url" /> parameter is a file name.</exception>
		// Token: 0x06002B1A RID: 11034 RVA: 0x0009C22C File Offset: 0x0009A42C
		public static Site CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			if (url.Length == 0)
			{
				throw new FormatException(Locale.GetText("Empty URL."));
			}
			string text = Site.UrlToSite(url);
			if (text == null)
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid URL '{0}'."), url), "url");
			}
			return new Site(text);
		}

		/// <summary>Creates an equivalent copy of the <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <returns>A new object that is identical to the current <see cref="T:System.Security.Policy.Site" /> object.</returns>
		// Token: 0x06002B1B RID: 11035 RVA: 0x0009C288 File Offset: 0x0009A488
		public object Copy()
		{
			return new Site(this.origin_site);
		}

		/// <summary>Creates an identity permission that corresponds to the current <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <param name="evidence">The evidence from which to construct the identity permission.</param>
		/// <returns>A site identity permission for the current <see cref="T:System.Security.Policy.Site" /> object.</returns>
		// Token: 0x06002B1C RID: 11036 RVA: 0x0009C295 File Offset: 0x0009A495
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new SiteIdentityPermission(this.origin_site);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Site" /> to the specified object for equivalence.</summary>
		/// <param name="o">The object to test for equivalence with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the two instances of the <see cref="T:System.Security.Policy.Site" /> class are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06002B1D RID: 11037 RVA: 0x0009C2A4 File Offset: 0x0009A4A4
		public override bool Equals(object o)
		{
			Site site = o as Site;
			return site != null && string.Compare(site.Name, this.origin_site, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Returns the hash code of the current website name.</summary>
		/// <returns>The hash code of the current website name.</returns>
		// Token: 0x06002B1E RID: 11038 RVA: 0x0009C2D7 File Offset: 0x0009A4D7
		public override int GetHashCode()
		{
			return this.origin_site.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Site" /> object.</summary>
		/// <returns>A representation of the current site.</returns>
		// Token: 0x06002B1F RID: 11039 RVA: 0x0009C2E4 File Offset: 0x0009A4E4
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Site");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Name", this.origin_site));
			return securityElement.ToString();
		}

		/// <summary>Gets the website from which the code assembly originates.</summary>
		/// <returns>The name of the website from which the code assembly originates.</returns>
		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x06002B20 RID: 11040 RVA: 0x0009C31B File Offset: 0x0009A51B
		public string Name
		{
			get
			{
				return this.origin_site;
			}
		}

		// Token: 0x06002B21 RID: 11041 RVA: 0x0009C323 File Offset: 0x0009A523
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return (verbose ? 3 : 1) + this.origin_site.Length;
		}

		// Token: 0x06002B22 RID: 11042 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06002B23 RID: 11043 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		// Token: 0x06002B24 RID: 11044 RVA: 0x0009C338 File Offset: 0x0009A538
		internal static bool IsValid(string name)
		{
			if (name == string.Empty)
			{
				return false;
			}
			if (name.Length == 1 && name == ".")
			{
				return false;
			}
			string[] array = name.Split('.', StringSplitOptions.None);
			for (int i = 0; i < array.Length; i++)
			{
				string text = array[i];
				if (i != 0 || !(text == "*"))
				{
					string text2 = text;
					for (int j = 0; j < text2.Length; j++)
					{
						int num = Convert.ToInt32(text2[j]);
						if (num != 33 && num != 45 && (num < 35 || num > 41) && (num < 48 || num > 57) && (num < 64 || num > 90) && (num < 94 || num > 95) && (num < 97 || num > 123) && (num < 125 || num > 126))
						{
							return false;
						}
					}
				}
			}
			return true;
		}

		// Token: 0x06002B25 RID: 11045 RVA: 0x0009C42C File Offset: 0x0009A62C
		internal static string UrlToSite(string url)
		{
			if (url == null)
			{
				return null;
			}
			Uri uri = new Uri(url);
			if (uri.Scheme == Uri.UriSchemeFile)
			{
				return null;
			}
			string host = uri.Host;
			if (!Site.IsValid(host))
			{
				return null;
			}
			return host;
		}

		// Token: 0x04001FB2 RID: 8114
		internal string origin_site;
	}
}

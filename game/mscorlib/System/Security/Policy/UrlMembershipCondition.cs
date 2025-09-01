using System;
using System.Collections;
using System.Globalization;
using System.Runtime.InteropServices;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Determines whether an assembly belongs to a code group by testing its URL. This class cannot be inherited.</summary>
	// Token: 0x02000426 RID: 1062
	[ComVisible(true)]
	[Serializable]
	public sealed class UrlMembershipCondition : IMembershipCondition, ISecurityEncodable, ISecurityPolicyEncodable, IConstantMembershipCondition
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.UrlMembershipCondition" /> class with the URL that determines membership.</summary>
		/// <param name="url">The URL for which to test.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="url" /> must be an absolute URL.</exception>
		// Token: 0x06002B72 RID: 11122 RVA: 0x0009CF80 File Offset: 0x0009B180
		public UrlMembershipCondition(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			this.CheckUrl(url);
			this.userUrl = url;
			this.url = new Url(url);
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x0009CFB7 File Offset: 0x0009B1B7
		internal UrlMembershipCondition(Url url, string userUrl)
		{
			this.url = (Url)url.Copy();
			this.userUrl = userUrl;
		}

		/// <summary>Gets or sets the URL for which the membership condition tests.</summary>
		/// <returns>The URL for which the membership condition tests.</returns>
		/// <exception cref="T:System.ArgumentNullException">An attempt is made to set <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> to <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">Value is not an absolute URL.</exception>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06002B74 RID: 11124 RVA: 0x0009CFDE File Offset: 0x0009B1DE
		// (set) Token: 0x06002B75 RID: 11125 RVA: 0x0009CFFF File Offset: 0x0009B1FF
		public string Url
		{
			get
			{
				if (this.userUrl == null)
				{
					this.userUrl = this.url.Value;
				}
				return this.userUrl;
			}
			set
			{
				this.url = new Url(value);
			}
		}

		/// <summary>Determines whether the specified evidence satisfies the membership condition.</summary>
		/// <param name="evidence">The evidence set against which to make the test.</param>
		/// <returns>
		///   <see langword="true" /> if the specified evidence satisfies the membership condition; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property is <see langword="null" />.</exception>
		// Token: 0x06002B76 RID: 11126 RVA: 0x0009D010 File Offset: 0x0009B210
		public bool Check(Evidence evidence)
		{
			if (evidence == null)
			{
				return false;
			}
			string value = this.url.Value;
			int num = value.LastIndexOf("*");
			if (num == -1)
			{
				num = value.Length;
			}
			IEnumerator hostEnumerator = evidence.GetHostEnumerator();
			while (hostEnumerator.MoveNext())
			{
				if (hostEnumerator.Current is Url && string.Compare(value, 0, (hostEnumerator.Current as Url).Value, 0, num, true, CultureInfo.InvariantCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>Creates an equivalent copy of the membership condition.</summary>
		/// <returns>A new, identical copy of the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property is <see langword="null" />.</exception>
		// Token: 0x06002B77 RID: 11127 RVA: 0x0009D087 File Offset: 0x0009B287
		public IMembershipCondition Copy()
		{
			return new UrlMembershipCondition(this.url, this.userUrl);
		}

		/// <summary>Determines whether the URL from the specified object is equivalent to the URL contained in the current <see cref="T:System.Security.Policy.UrlMembershipCondition" />.</summary>
		/// <param name="o">The object to compare to the current <see cref="T:System.Security.Policy.UrlMembershipCondition" />.</param>
		/// <returns>
		///   <see langword="true" /> if the URL from the specified object is equivalent to the URL contained in the current <see cref="T:System.Security.Policy.UrlMembershipCondition" />; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property of the current object or the specified object is <see langword="null" />.</exception>
		// Token: 0x06002B78 RID: 11128 RVA: 0x0009D09C File Offset: 0x0009B29C
		public override bool Equals(object o)
		{
			UrlMembershipCondition urlMembershipCondition = o as UrlMembershipCondition;
			if (o == null)
			{
				return false;
			}
			string value = this.url.Value;
			int num = value.Length;
			if (value[num - 1] == '*')
			{
				num--;
				if (value[num - 1] == '/')
				{
					num--;
				}
			}
			return string.Compare(value, 0, urlMembershipCondition.Url, 0, num, true, CultureInfo.InvariantCulture) == 0;
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x06002B79 RID: 11129 RVA: 0x0009D102 File Offset: 0x0009B302
		public void FromXml(SecurityElement e)
		{
			this.FromXml(e, null);
		}

		/// <summary>Reconstructs a security object with a specified state from an XML encoding.</summary>
		/// <param name="e">The XML encoding to use to reconstruct the security object.</param>
		/// <param name="level">The policy level context, used to resolve named permission set references.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="e" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">The <paramref name="e" /> parameter is not a valid membership condition element.</exception>
		// Token: 0x06002B7A RID: 11130 RVA: 0x0009D10C File Offset: 0x0009B30C
		public void FromXml(SecurityElement e, PolicyLevel level)
		{
			MembershipConditionHelper.CheckSecurityElement(e, "e", this.version, this.version);
			string text = e.Attribute("Url");
			if (text != null)
			{
				this.CheckUrl(text);
				this.url = new Url(text);
			}
			else
			{
				this.url = null;
			}
			this.userUrl = text;
		}

		/// <summary>Gets the hash code for the current membership condition.</summary>
		/// <returns>The hash code for the current membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property is <see langword="null" />.</exception>
		// Token: 0x06002B7B RID: 11131 RVA: 0x0009D163 File Offset: 0x0009B363
		public override int GetHashCode()
		{
			return this.url.GetHashCode();
		}

		/// <summary>Creates and returns a string representation of the membership condition.</summary>
		/// <returns>A string representation of the state of the membership condition.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property is <see langword="null" />.</exception>
		// Token: 0x06002B7C RID: 11132 RVA: 0x0009D170 File Offset: 0x0009B370
		public override string ToString()
		{
			return "Url - " + this.Url;
		}

		/// <summary>Creates an XML encoding of the security object and its current state.</summary>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		// Token: 0x06002B7D RID: 11133 RVA: 0x0009D182 File Offset: 0x0009B382
		public SecurityElement ToXml()
		{
			return this.ToXml(null);
		}

		/// <summary>Creates an XML encoding of the security object and its current state with the specified <see cref="T:System.Security.Policy.PolicyLevel" />.</summary>
		/// <param name="level">The policy level context for resolving named permission set references.</param>
		/// <returns>An XML encoding of the security object, including any state information.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <see cref="P:System.Security.Policy.UrlMembershipCondition.Url" /> property is <see langword="null" />.</exception>
		// Token: 0x06002B7E RID: 11134 RVA: 0x0009D18B File Offset: 0x0009B38B
		public SecurityElement ToXml(PolicyLevel level)
		{
			SecurityElement securityElement = MembershipConditionHelper.Element(typeof(UrlMembershipCondition), this.version);
			securityElement.AddAttribute("Url", this.userUrl);
			return securityElement;
		}

		// Token: 0x06002B7F RID: 11135 RVA: 0x0009D1B4 File Offset: 0x0009B3B4
		internal void CheckUrl(string url)
		{
			if (new Uri((url.IndexOf(Uri.SchemeDelimiter) < 0) ? ("file://" + url) : url, false, false).Host.IndexOf('*') >= 1)
			{
				throw new ArgumentException(Locale.GetText("Invalid * character in url"), "name");
			}
		}

		// Token: 0x04001FC7 RID: 8135
		private readonly int version = 1;

		// Token: 0x04001FC8 RID: 8136
		private Url url;

		// Token: 0x04001FC9 RID: 8137
		private string userUrl;
	}
}

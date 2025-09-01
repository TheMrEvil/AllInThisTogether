using System;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Mono.Security;

namespace System.Security.Policy
{
	/// <summary>Provides the security zone of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x02000427 RID: 1063
	[ComVisible(true)]
	[Serializable]
	public sealed class Zone : EvidenceBase, IIdentityPermissionFactory, IBuiltInEvidence
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Zone" /> class with the zone from which a code assembly originates.</summary>
		/// <param name="zone">The zone of origin for the associated code assembly.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="zone" /> parameter is not a valid <see cref="T:System.Security.SecurityZone" />.</exception>
		// Token: 0x06002B80 RID: 11136 RVA: 0x0009D208 File Offset: 0x0009B408
		public Zone(SecurityZone zone)
		{
			if (!Enum.IsDefined(typeof(SecurityZone), zone))
			{
				throw new ArgumentException(string.Format(Locale.GetText("Invalid zone {0}."), zone), "zone");
			}
			this.zone = zone;
		}

		/// <summary>Gets the zone from which the code assembly originates.</summary>
		/// <returns>The zone from which the code assembly originates.</returns>
		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06002B81 RID: 11137 RVA: 0x0009D259 File Offset: 0x0009B459
		public SecurityZone SecurityZone
		{
			get
			{
				return this.zone;
			}
		}

		/// <summary>Creates an equivalent copy of the evidence object.</summary>
		/// <returns>A new, identical copy of the evidence object.</returns>
		// Token: 0x06002B82 RID: 11138 RVA: 0x0009D261 File Offset: 0x0009B461
		public object Copy()
		{
			return new Zone(this.zone);
		}

		/// <summary>Creates an identity permission that corresponds to the current instance of the <see cref="T:System.Security.Policy.Zone" /> evidence class.</summary>
		/// <param name="evidence">The evidence set from which to construct the identity permission.</param>
		/// <returns>A <see cref="T:System.Security.Permissions.ZoneIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Zone" /> evidence.</returns>
		// Token: 0x06002B83 RID: 11139 RVA: 0x0009D26E File Offset: 0x0009B46E
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new ZoneIdentityPermission(this.zone);
		}

		/// <summary>Creates a new zone with the specified URL.</summary>
		/// <param name="url">The URL from which to create the zone.</param>
		/// <returns>A new zone with the specified URL.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="url" /> parameter is <see langword="null" />.</exception>
		// Token: 0x06002B84 RID: 11140 RVA: 0x0009D27C File Offset: 0x0009B47C
		[MonoTODO("Not user configurable yet")]
		public static Zone CreateFromUrl(string url)
		{
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			SecurityZone securityZone = SecurityZone.NoZone;
			if (url.Length == 0)
			{
				return new Zone(securityZone);
			}
			Uri uri = null;
			try
			{
				uri = new Uri(url);
			}
			catch
			{
				return new Zone(securityZone);
			}
			if (securityZone == SecurityZone.NoZone)
			{
				if (uri.IsFile)
				{
					if (File.Exists(uri.LocalPath))
					{
						securityZone = SecurityZone.MyComputer;
					}
					else if (string.Compare("FILE://", 0, url, 0, 7, true, CultureInfo.InvariantCulture) == 0)
					{
						securityZone = SecurityZone.Intranet;
					}
					else
					{
						securityZone = SecurityZone.Internet;
					}
				}
				else if (uri.IsLoopback)
				{
					securityZone = SecurityZone.Intranet;
				}
				else
				{
					securityZone = SecurityZone.Internet;
				}
			}
			return new Zone(securityZone);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Zone" /> evidence object to the specified object for equivalence.</summary>
		/// <param name="o">The <see cref="T:System.Security.Policy.Zone" /> evidence object to test for equivalence with the current object.</param>
		/// <returns>
		///   <see langword="true" /> if the two <see cref="T:System.Security.Policy.Zone" /> objects are equal; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.ArgumentException">The <paramref name="o" /> parameter is not a <see cref="T:System.Security.Policy.Zone" /> object.</exception>
		// Token: 0x06002B85 RID: 11141 RVA: 0x0009D320 File Offset: 0x0009B520
		public override bool Equals(object o)
		{
			Zone zone = o as Zone;
			return zone != null && zone.zone == this.zone;
		}

		/// <summary>Gets the hash code of the current zone.</summary>
		/// <returns>The hash code of the current zone.</returns>
		// Token: 0x06002B86 RID: 11142 RVA: 0x0009D259 File Offset: 0x0009B459
		public override int GetHashCode()
		{
			return (int)this.zone;
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Zone" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Zone" />.</returns>
		// Token: 0x06002B87 RID: 11143 RVA: 0x0009D348 File Offset: 0x0009B548
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Zone");
			securityElement.AddAttribute("version", "1");
			securityElement.AddChild(new SecurityElement("Zone", this.zone.ToString()));
			return securityElement.ToString();
		}

		// Token: 0x06002B88 RID: 11144 RVA: 0x000221D2 File Offset: 0x000203D2
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return 3;
		}

		// Token: 0x06002B89 RID: 11145 RVA: 0x0009D395 File Offset: 0x0009B595
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			char c = buffer[position++];
			char c2 = buffer[position++];
			return position;
		}

		// Token: 0x06002B8A RID: 11146 RVA: 0x0009D3AA File Offset: 0x0009B5AA
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			buffer[position++] = '\u0003';
			buffer[position++] = (char)(this.zone >> 16);
			buffer[position++] = (char)(this.zone & (SecurityZone)65535);
			return position;
		}

		// Token: 0x04001FCA RID: 8138
		private SecurityZone zone;
	}
}

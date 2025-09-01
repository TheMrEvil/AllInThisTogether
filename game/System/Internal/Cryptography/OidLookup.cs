using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Internal.Cryptography
{
	// Token: 0x02000117 RID: 279
	internal static class OidLookup
	{
		// Token: 0x060006AC RID: 1708 RVA: 0x000125AF File Offset: 0x000107AF
		private static bool ShouldUseCache(OidGroup oidGroup)
		{
			return oidGroup == OidGroup.All;
		}

		// Token: 0x060006AD RID: 1709 RVA: 0x000125B8 File Offset: 0x000107B8
		private static string NativeOidToFriendlyName(string oid, OidGroup oidGroup, bool fallBackToAllGroups)
		{
			return Interop.Crypt32.FindOidInfo(Interop.Crypt32.CryptOidInfoKeyType.CRYPT_OID_INFO_OID_KEY, oid, oidGroup, fallBackToAllGroups).Name;
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x000125D8 File Offset: 0x000107D8
		private static string NativeFriendlyNameToOid(string friendlyName, OidGroup oidGroup, bool fallBackToAllGroups)
		{
			return Interop.Crypt32.FindOidInfo(Interop.Crypt32.CryptOidInfoKeyType.CRYPT_OID_INFO_NAME_KEY, friendlyName, oidGroup, fallBackToAllGroups).OID;
		}

		// Token: 0x060006AF RID: 1711 RVA: 0x000125F8 File Offset: 0x000107F8
		public static string ToFriendlyName(string oid, OidGroup oidGroup, bool fallBackToAllGroups)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			bool flag = OidLookup.ShouldUseCache(oidGroup);
			string text;
			if (flag && (OidLookup.s_oidToFriendlyName.TryGetValue(oid, out text) || OidLookup.s_compatOids.TryGetValue(oid, out text) || OidLookup.s_lateBoundOidToFriendlyName.TryGetValue(oid, out text)))
			{
				return text;
			}
			text = OidLookup.NativeOidToFriendlyName(oid, oidGroup, fallBackToAllGroups);
			if (flag && text != null)
			{
				OidLookup.s_lateBoundOidToFriendlyName.TryAdd(oid, text);
			}
			return text;
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001266C File Offset: 0x0001086C
		public static string ToOid(string friendlyName, OidGroup oidGroup, bool fallBackToAllGroups)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			if (friendlyName.Length == 0)
			{
				return null;
			}
			bool flag = OidLookup.ShouldUseCache(oidGroup);
			string text;
			if (flag && (OidLookup.s_friendlyNameToOid.TryGetValue(friendlyName, out text) || OidLookup.s_lateBoundFriendlyNameToOid.TryGetValue(friendlyName, out text)))
			{
				return text;
			}
			text = OidLookup.NativeFriendlyNameToOid(friendlyName, oidGroup, fallBackToAllGroups);
			if (flag && text != null)
			{
				OidLookup.s_lateBoundFriendlyNameToOid.TryAdd(friendlyName, text);
			}
			return text;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x000126D8 File Offset: 0x000108D8
		// Note: this type is marked as 'beforefieldinit'.
		static OidLookup()
		{
		}

		// Token: 0x0400047A RID: 1146
		private static readonly ConcurrentDictionary<string, string> s_lateBoundOidToFriendlyName = new ConcurrentDictionary<string, string>();

		// Token: 0x0400047B RID: 1147
		private static readonly ConcurrentDictionary<string, string> s_lateBoundFriendlyNameToOid = new ConcurrentDictionary<string, string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400047C RID: 1148
		private static readonly Dictionary<string, string> s_friendlyNameToOid = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"3des",
				"1.2.840.113549.3.7"
			},
			{
				"aes128",
				"2.16.840.1.101.3.4.1.2"
			},
			{
				"aes128wrap",
				"2.16.840.1.101.3.4.1.5"
			},
			{
				"aes192",
				"2.16.840.1.101.3.4.1.22"
			},
			{
				"aes192wrap",
				"2.16.840.1.101.3.4.1.25"
			},
			{
				"aes256",
				"2.16.840.1.101.3.4.1.42"
			},
			{
				"aes256wrap",
				"2.16.840.1.101.3.4.1.45"
			},
			{
				"brainpoolP160r1",
				"1.3.36.3.3.2.8.1.1.1"
			},
			{
				"brainpoolP160t1",
				"1.3.36.3.3.2.8.1.1.2"
			},
			{
				"brainpoolP192r1",
				"1.3.36.3.3.2.8.1.1.3"
			},
			{
				"brainpoolP192t1",
				"1.3.36.3.3.2.8.1.1.4"
			},
			{
				"brainpoolP224r1",
				"1.3.36.3.3.2.8.1.1.5"
			},
			{
				"brainpoolP224t1",
				"1.3.36.3.3.2.8.1.1.6"
			},
			{
				"brainpoolP256r1",
				"1.3.36.3.3.2.8.1.1.7"
			},
			{
				"brainpoolP256t1",
				"1.3.36.3.3.2.8.1.1.8"
			},
			{
				"brainpoolP320r1",
				"1.3.36.3.3.2.8.1.1.9"
			},
			{
				"brainpoolP320t1",
				"1.3.36.3.3.2.8.1.1.10"
			},
			{
				"brainpoolP384r1",
				"1.3.36.3.3.2.8.1.1.11"
			},
			{
				"brainpoolP384t1",
				"1.3.36.3.3.2.8.1.1.12"
			},
			{
				"brainpoolP512r1",
				"1.3.36.3.3.2.8.1.1.13"
			},
			{
				"brainpoolP512t1",
				"1.3.36.3.3.2.8.1.1.14"
			},
			{
				"C",
				"2.5.4.6"
			},
			{
				"CMS3DESwrap",
				"1.2.840.113549.1.9.16.3.6"
			},
			{
				"CMSRC2wrap",
				"1.2.840.113549.1.9.16.3.7"
			},
			{
				"CN",
				"2.5.4.3"
			},
			{
				"CPS",
				"1.3.6.1.5.5.7.2.1"
			},
			{
				"DC",
				"0.9.2342.19200300.100.1.25"
			},
			{
				"des",
				"1.3.14.3.2.7"
			},
			{
				"Description",
				"2.5.4.13"
			},
			{
				"DH",
				"1.2.840.10046.2.1"
			},
			{
				"dnQualifier",
				"2.5.4.46"
			},
			{
				"DSA",
				"1.2.840.10040.4.1"
			},
			{
				"dsaSHA1",
				"1.3.14.3.2.27"
			},
			{
				"E",
				"1.2.840.113549.1.9.1"
			},
			{
				"ec192wapi",
				"1.2.156.11235.1.1.2.1"
			},
			{
				"ECC",
				"1.2.840.10045.2.1"
			},
			{
				"ECDH_STD_SHA1_KDF",
				"1.3.133.16.840.63.0.2"
			},
			{
				"ECDH_STD_SHA256_KDF",
				"1.3.132.1.11.1"
			},
			{
				"ECDH_STD_SHA384_KDF",
				"1.3.132.1.11.2"
			},
			{
				"ECDSA_P256",
				"1.2.840.10045.3.1.7"
			},
			{
				"ECDSA_P384",
				"1.3.132.0.34"
			},
			{
				"ECDSA_P521",
				"1.3.132.0.35"
			},
			{
				"ESDH",
				"1.2.840.113549.1.9.16.3.5"
			},
			{
				"G",
				"2.5.4.42"
			},
			{
				"I",
				"2.5.4.43"
			},
			{
				"L",
				"2.5.4.7"
			},
			{
				"md2",
				"1.2.840.113549.2.2"
			},
			{
				"md2RSA",
				"1.2.840.113549.1.1.2"
			},
			{
				"md4",
				"1.2.840.113549.2.4"
			},
			{
				"md4RSA",
				"1.2.840.113549.1.1.3"
			},
			{
				"md5",
				"1.2.840.113549.2.5"
			},
			{
				"md5RSA",
				"1.2.840.113549.1.1.4"
			},
			{
				"mgf1",
				"1.2.840.113549.1.1.8"
			},
			{
				"mosaicKMandUpdSig",
				"2.16.840.1.101.2.1.1.20"
			},
			{
				"mosaicUpdatedSig",
				"2.16.840.1.101.2.1.1.19"
			},
			{
				"nistP192",
				"1.2.840.10045.3.1.1"
			},
			{
				"nistP224",
				"1.3.132.0.33"
			},
			{
				"NO_SIGN",
				"1.3.6.1.5.5.7.6.2"
			},
			{
				"O",
				"2.5.4.10"
			},
			{
				"OU",
				"2.5.4.11"
			},
			{
				"Phone",
				"2.5.4.20"
			},
			{
				"POBox",
				"2.5.4.18"
			},
			{
				"PostalCode",
				"2.5.4.17"
			},
			{
				"rc2",
				"1.2.840.113549.3.2"
			},
			{
				"rc4",
				"1.2.840.113549.3.4"
			},
			{
				"RSA",
				"1.2.840.113549.1.1.1"
			},
			{
				"RSAES_OAEP",
				"1.2.840.113549.1.1.7"
			},
			{
				"RSASSA-PSS",
				"1.2.840.113549.1.1.10"
			},
			{
				"S",
				"2.5.4.8"
			},
			{
				"secP160k1",
				"1.3.132.0.9"
			},
			{
				"secP160r1",
				"1.3.132.0.8"
			},
			{
				"secP160r2",
				"1.3.132.0.30"
			},
			{
				"secP192k1",
				"1.3.132.0.31"
			},
			{
				"secP224k1",
				"1.3.132.0.32"
			},
			{
				"secP256k1",
				"1.3.132.0.10"
			},
			{
				"SERIALNUMBER",
				"2.5.4.5"
			},
			{
				"sha1",
				"1.3.14.3.2.26"
			},
			{
				"sha1DSA",
				"1.2.840.10040.4.3"
			},
			{
				"sha1ECDSA",
				"1.2.840.10045.4.1"
			},
			{
				"sha1RSA",
				"1.2.840.113549.1.1.5"
			},
			{
				"sha256",
				"2.16.840.1.101.3.4.2.1"
			},
			{
				"sha256ECDSA",
				"1.2.840.10045.4.3.2"
			},
			{
				"sha256RSA",
				"1.2.840.113549.1.1.11"
			},
			{
				"sha384",
				"2.16.840.1.101.3.4.2.2"
			},
			{
				"sha384ECDSA",
				"1.2.840.10045.4.3.3"
			},
			{
				"sha384RSA",
				"1.2.840.113549.1.1.12"
			},
			{
				"sha512",
				"2.16.840.1.101.3.4.2.3"
			},
			{
				"sha512ECDSA",
				"1.2.840.10045.4.3.4"
			},
			{
				"sha512RSA",
				"1.2.840.113549.1.1.13"
			},
			{
				"SN",
				"2.5.4.4"
			},
			{
				"specifiedECDSA",
				"1.2.840.10045.4.3"
			},
			{
				"STREET",
				"2.5.4.9"
			},
			{
				"T",
				"2.5.4.12"
			},
			{
				"wtls9",
				"2.23.43.1.4.9"
			},
			{
				"X21Address",
				"2.5.4.24"
			},
			{
				"x962P192v2",
				"1.2.840.10045.3.1.2"
			},
			{
				"x962P192v3",
				"1.2.840.10045.3.1.3"
			},
			{
				"x962P239v1",
				"1.2.840.10045.3.1.4"
			},
			{
				"x962P239v2",
				"1.2.840.10045.3.1.5"
			},
			{
				"x962P239v3",
				"1.2.840.10045.3.1.6"
			}
		};

		// Token: 0x0400047D RID: 1149
		private static readonly Dictionary<string, string> s_oidToFriendlyName = OidLookup.s_friendlyNameToOid.ToDictionary((KeyValuePair<string, string> kvp) => kvp.Value, (KeyValuePair<string, string> kvp) => kvp.Key);

		// Token: 0x0400047E RID: 1150
		private static readonly Dictionary<string, string> s_compatOids = new Dictionary<string, string>
		{
			{
				"1.2.840.113549.1.3.1",
				"DH"
			},
			{
				"1.3.14.3.2.12",
				"DSA"
			},
			{
				"1.3.14.3.2.13",
				"sha1DSA"
			},
			{
				"1.3.14.3.2.15",
				"shaRSA"
			},
			{
				"1.3.14.3.2.18",
				"sha"
			},
			{
				"1.3.14.3.2.2",
				"md4RSA"
			},
			{
				"1.3.14.3.2.22",
				"RSA_KEYX"
			},
			{
				"1.3.14.3.2.29",
				"sha1RSA"
			},
			{
				"1.3.14.3.2.3",
				"md5RSA"
			},
			{
				"1.3.14.3.2.4",
				"md4RSA"
			},
			{
				"1.3.14.7.2.3.1",
				"md2RSA"
			}
		};

		// Token: 0x02000118 RID: 280
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060006B2 RID: 1714 RVA: 0x00012E36 File Offset: 0x00011036
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060006B3 RID: 1715 RVA: 0x0000219B File Offset: 0x0000039B
			public <>c()
			{
			}

			// Token: 0x060006B4 RID: 1716 RVA: 0x00012E42 File Offset: 0x00011042
			internal string <.cctor>b__10_0(KeyValuePair<string, string> kvp)
			{
				return kvp.Value;
			}

			// Token: 0x060006B5 RID: 1717 RVA: 0x00012E4B File Offset: 0x0001104B
			internal string <.cctor>b__10_1(KeyValuePair<string, string> kvp)
			{
				return kvp.Key;
			}

			// Token: 0x0400047F RID: 1151
			public static readonly OidLookup.<>c <>9 = new OidLookup.<>c();
		}
	}
}

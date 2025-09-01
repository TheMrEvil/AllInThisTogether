using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Accesses the cryptography configuration information.</summary>
	// Token: 0x020004C6 RID: 1222
	[ComVisible(true)]
	public class CryptoConfig
	{
		/// <summary>Adds a set of names to object identifier (OID) mappings to be used for the current application domain.</summary>
		/// <param name="oid">The object identifier (OID) to map to.</param>
		/// <param name="names">An array of names to map to the OID.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="oid" /> or <paramref name="names" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">One of the entries in the <paramref name="names" /> parameter is empty or <see langword="null" />.</exception>
		// Token: 0x060030D9 RID: 12505 RVA: 0x0001B98B File Offset: 0x00019B8B
		public static void AddOID(string oid, params string[] names)
		{
			throw new PlatformNotSupportedException();
		}

		/// <summary>Creates a new instance of the specified cryptographic object.</summary>
		/// <param name="name">The simple name of the cryptographic object of which to create an instance.</param>
		/// <returns>A new instance of the specified cryptographic object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="name" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x060030DA RID: 12506 RVA: 0x000B1A38 File Offset: 0x000AFC38
		public static object CreateFromName(string name)
		{
			return CryptoConfig.CreateFromName(name, null);
		}

		/// <summary>Creates a new instance of the specified cryptographic object with the specified arguments.</summary>
		/// <param name="name">The simple name of the cryptographic object of which to create an instance.</param>
		/// <param name="args">The arguments used to create the specified cryptographic object.</param>
		/// <returns>A new instance of the specified cryptographic object.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Reflection.TargetInvocationException">The algorithm described by the <paramref name="name" /> parameter was used with Federal Information Processing Standards (FIPS) mode enabled, but is not FIPS compatible.</exception>
		// Token: 0x060030DB RID: 12507 RVA: 0x000B1A44 File Offset: 0x000AFC44
		[PreserveDependency(".ctor()", "System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension", "System")]
		[PreserveDependency(".ctor()", "System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension", "System")]
		[PreserveDependency(".ctor()", "System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension", "System")]
		[PreserveDependency(".ctor()", "System.Security.Cryptography.X509Certificates.X509KeyUsageExtension", "System")]
		[PreserveDependency(".ctor()", "System.Security.Cryptography.X509Certificates.X509Chain", "System")]
		[PreserveDependency(".ctor()", "System.Security.Cryptography.AesCryptoServiceProvider", "System.Core")]
		public static object CreateFromName(string name, params object[] args)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			Type type = null;
			string text = name.ToLowerInvariant();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2442086578U)
			{
				if (num <= 1318943838U)
				{
					if (num <= 589079309U)
					{
						if (num <= 401798778U)
						{
							if (num <= 294650258U)
							{
								if (num != 97835172U)
								{
									if (num != 289646596U)
									{
										if (num != 294650258U)
										{
											goto IL_FA9;
										}
										if (!(text == "3des"))
										{
											goto IL_FA9;
										}
										goto IL_F57;
									}
									else
									{
										if (!(text == "system.security.cryptography.sha1cryptoserviceprovider"))
										{
											goto IL_FA9;
										}
										goto IL_F39;
									}
								}
								else
								{
									if (!(text == "http://www.w3.org/2001/04/xmldsig-more#sha384"))
									{
										goto IL_FA9;
									}
									goto IL_F4B;
								}
							}
							else if (num != 373238979U)
							{
								if (num != 381964475U)
								{
									if (num != 401798778U)
									{
										goto IL_FA9;
									}
									if (!(text == "system.security.cryptography.dsasignaturedescription"))
									{
										goto IL_FA9;
									}
								}
								else
								{
									if (!(text == "hmacsha1"))
									{
										goto IL_FA9;
									}
									goto IL_ED3;
								}
							}
							else
							{
								if (!(text == "system.security.cryptography.sha1cng"))
								{
									goto IL_FA9;
								}
								goto IL_F39;
							}
						}
						else if (num <= 550229268U)
						{
							if (num != 418711287U)
							{
								if (num != 524624695U)
								{
									if (num != 550229268U)
									{
										goto IL_FA9;
									}
									if (!(text == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256"))
									{
										goto IL_FA9;
									}
									goto IL_ED9;
								}
								else
								{
									if (!(text == "system.security.cryptography.hashalgorithm"))
									{
										goto IL_FA9;
									}
									goto IL_F39;
								}
							}
							else
							{
								if (!(text == "system.security.cryptography.mactripledes"))
								{
									goto IL_FA9;
								}
								goto IL_EEB;
							}
						}
						else if (num != 572088812U)
						{
							if (num != 585245684U)
							{
								if (num != 589079309U)
								{
									goto IL_FA9;
								}
								if (!(text == "system.security.cryptography.hmac"))
								{
									goto IL_FA9;
								}
								goto IL_ED3;
							}
							else
							{
								if (!(text == "system.security.cryptography.sha1managed"))
								{
									goto IL_FA9;
								}
								return new SHA1Managed();
							}
						}
						else
						{
							if (!(text == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha384"))
							{
								goto IL_FA9;
							}
							goto IL_EDF;
						}
					}
					else if (num <= 900295094U)
					{
						if (num <= 734683829U)
						{
							if (num != 699966473U)
							{
								if (num != 708523592U)
								{
									if (num != 734683829U)
									{
										goto IL_FA9;
									}
									if (!(text == "hmacsha256"))
									{
										goto IL_FA9;
									}
									goto IL_ED9;
								}
								else
								{
									if (!(text == "aes"))
									{
										goto IL_FA9;
									}
									type = Type.GetType("System.Security.Cryptography.AesCryptoServiceProvider, System.Core");
									goto IL_FA9;
								}
							}
							else if (!(text == "http://www.w3.org/2000/09/xmldsig#dsa-sha1"))
							{
								goto IL_FA9;
							}
						}
						else if (num != 853553133U)
						{
							if (num != 877368883U)
							{
								if (num != 900295094U)
								{
									goto IL_FA9;
								}
								if (!(text == "system.security.cryptography.dsasignatureformatter"))
								{
									goto IL_FA9;
								}
								return new DSASignatureFormatter();
							}
							else
							{
								if (!(text == "http://www.w3.org/2000/09/xmldsig#rsa-sha1"))
								{
									goto IL_FA9;
								}
								goto IL_F21;
							}
						}
						else
						{
							if (!(text == "system.security.cryptography.rsapkcs1sha384signaturedescription"))
							{
								goto IL_FA9;
							}
							goto IL_F2D;
						}
					}
					else if (num <= 1104969097U)
					{
						if (num != 965923590U)
						{
							if (num != 999454301U)
							{
								if (num != 1104969097U)
								{
									goto IL_FA9;
								}
								if (!(text == "rsa"))
								{
									goto IL_FA9;
								}
								goto IL_F0F;
							}
							else
							{
								if (!(text == "system.security.cryptography.rsapkcs1sha256signaturedescription"))
								{
									goto IL_FA9;
								}
								goto IL_F27;
							}
						}
						else
						{
							if (!(text == "http://www.w3.org/2000/09/xmldsig#sha1"))
							{
								goto IL_FA9;
							}
							goto IL_F39;
						}
					}
					else if (num <= 1168228931U)
					{
						if (num != 1147401626U)
						{
							if (num != 1168228931U)
							{
								goto IL_FA9;
							}
							if (!(text == "system.security.cryptography.ripemd160managed"))
							{
								goto IL_FA9;
							}
							goto IL_F03;
						}
						else
						{
							if (!(text == "system.security.cryptography.rsapkcs1signatureformatter"))
							{
								goto IL_FA9;
							}
							return new RSAPKCS1SignatureFormatter();
						}
					}
					else if (num != 1279198866U)
					{
						if (num != 1318943838U)
						{
							goto IL_FA9;
						}
						if (!(text == "http://www.w3.org/2000/09/xmldsig#hmac-sha1"))
						{
							goto IL_FA9;
						}
						goto IL_ED3;
					}
					else
					{
						if (!(text == "system.security.cryptography.tripledes"))
						{
							goto IL_FA9;
						}
						goto IL_F57;
					}
					return new DSASignatureDescription();
				}
				if (num <= 1862521808U)
				{
					if (num <= 1664836558U)
					{
						if (num <= 1604759256U)
						{
							if (num != 1495151835U)
							{
								if (num != 1600607069U)
								{
									if (num != 1604759256U)
									{
										goto IL_FA9;
									}
									if (!(text == "sha-512"))
									{
										goto IL_FA9;
									}
									goto IL_F51;
								}
								else
								{
									if (!(text == "system.security.cryptography.sha384cng"))
									{
										goto IL_FA9;
									}
									goto IL_F4B;
								}
							}
							else
							{
								if (!(text == "system.security.cryptography.descryptoserviceprovider"))
								{
									goto IL_FA9;
								}
								goto IL_EC1;
							}
						}
						else if (num != 1610008198U)
						{
							if (num != 1629735498U)
							{
								if (num != 1664836558U)
								{
									goto IL_FA9;
								}
								if (!(text == "system.security.cryptography.sha256cryptoserviceprovider"))
								{
									goto IL_FA9;
								}
								goto IL_F45;
							}
							else
							{
								if (!(text == "system.security.cryptography.rc2cryptoserviceprovider"))
								{
									goto IL_FA9;
								}
								goto IL_EF7;
							}
						}
						else
						{
							if (!(text == "system.security.cryptography.rijndaelmanaged"))
							{
								goto IL_FA9;
							}
							goto IL_EFD;
						}
					}
					else if (num <= 1720406050U)
					{
						if (num != 1686995390U)
						{
							if (num != 1688024611U)
							{
								if (num != 1720406050U)
								{
									goto IL_FA9;
								}
								if (!(text == "x509chain"))
								{
									goto IL_FA9;
								}
								type = Type.GetType("System.Security.Cryptography.X509Certificates.X509Chain, System");
								goto IL_FA9;
							}
							else
							{
								if (!(text == "http://www.w3.org/2001/04/xmlenc#sha256"))
								{
									goto IL_FA9;
								}
								goto IL_F45;
							}
						}
						else if (!(text == "system.security.cryptography.md5"))
						{
							goto IL_FA9;
						}
					}
					else if (num != 1778503857U)
					{
						if (num != 1820576144U)
						{
							if (num != 1862521808U)
							{
								goto IL_FA9;
							}
							if (!(text == "rc2"))
							{
								goto IL_FA9;
							}
							goto IL_EF7;
						}
						else
						{
							if (!(text == "hmacsha512"))
							{
								goto IL_FA9;
							}
							goto IL_EE5;
						}
					}
					else
					{
						if (!(text == "system.security.cryptography.randomnumbergenerator"))
						{
							goto IL_FA9;
						}
						goto IL_F09;
					}
				}
				else if (num <= 2246759783U)
				{
					if (num <= 2120664437U)
					{
						if (num != 2070555668U)
						{
							if (num != 2102584679U)
							{
								if (num != 2120664437U)
								{
									goto IL_FA9;
								}
								if (!(text == "sha-384"))
								{
									goto IL_FA9;
								}
								goto IL_F4B;
							}
							else
							{
								if (!(text == "ripemd160"))
								{
									goto IL_FA9;
								}
								goto IL_F03;
							}
						}
						else
						{
							if (!(text == "sha1"))
							{
								goto IL_FA9;
							}
							goto IL_F39;
						}
					}
					else if (num != 2131651891U)
					{
						if (num != 2214607313U)
						{
							if (num != 2246759783U)
							{
								goto IL_FA9;
							}
							if (!(text == "system.security.cryptography.dsasignaturedeformatter"))
							{
								goto IL_FA9;
							}
							return new DSASignatureDeformatter();
						}
						else
						{
							if (!(text == "system.security.cryptography.sha512cryptoserviceprovider"))
							{
								goto IL_FA9;
							}
							goto IL_F51;
						}
					}
					else
					{
						if (!(text == "tripledes"))
						{
							goto IL_FA9;
						}
						goto IL_F57;
					}
				}
				else if (num <= 2346491937U)
				{
					if (num != 2269936011U)
					{
						if (num != 2340547105U)
						{
							if (num != 2346491937U)
							{
								goto IL_FA9;
							}
							if (!(text == "system.security.cryptography.sha256"))
							{
								goto IL_FA9;
							}
							goto IL_F45;
						}
						else
						{
							if (!(text == "system.security.cryptography.keyedhashalgorithm"))
							{
								goto IL_FA9;
							}
							goto IL_ED3;
						}
					}
					else if (!(text == "system.security.cryptography.md5cryptoserviceprovider"))
					{
						goto IL_FA9;
					}
				}
				else if (num <= 2394616414U)
				{
					if (num != 2393554675U)
					{
						if (num != 2394616414U)
						{
							goto IL_FA9;
						}
						if (!(text == "system.security.cryptography.sha384cryptoserviceprovider"))
						{
							goto IL_FA9;
						}
						goto IL_F4B;
					}
					else if (!(text == "md5"))
					{
						goto IL_FA9;
					}
				}
				else if (num != 2415328530U)
				{
					if (num != 2442086578U)
					{
						goto IL_FA9;
					}
					if (!(text == "hmacripemd160"))
					{
						goto IL_FA9;
					}
					goto IL_ECD;
				}
				else
				{
					if (!(text == "system.security.cryptography.hmacsha256"))
					{
						goto IL_FA9;
					}
					goto IL_ED9;
				}
				return new MD5CryptoServiceProvider();
				IL_ED9:
				return new HMACSHA256();
			}
			if (num <= 3339968437U)
			{
				if (num <= 2920802226U)
				{
					if (num <= 2685283101U)
					{
						if (num <= 2484875538U)
						{
							if (num != 2451404838U)
							{
								if (num != 2478224771U)
								{
									if (num != 2484875538U)
									{
										goto IL_FA9;
									}
									if (!(text == "system.security.cryptography.sha256managed"))
									{
										goto IL_FA9;
									}
									goto IL_F45;
								}
								else
								{
									if (!(text == "system.security.cryptography.hmacsha512"))
									{
										goto IL_FA9;
									}
									goto IL_EE5;
								}
							}
							else
							{
								if (!(text == "system.security.cryptography.ripemd160"))
								{
									goto IL_FA9;
								}
								goto IL_F03;
							}
						}
						else if (num != 2631153146U)
						{
							if (num != 2661179293U)
							{
								if (num != 2685283101U)
								{
									goto IL_FA9;
								}
								if (!(text == "system.security.cryptography.rc2"))
								{
									goto IL_FA9;
								}
								goto IL_EF7;
							}
							else
							{
								if (!(text == "sha-256"))
								{
									goto IL_FA9;
								}
								goto IL_F45;
							}
						}
						else
						{
							if (!(text == "sha256"))
							{
								goto IL_FA9;
							}
							goto IL_F45;
						}
					}
					else if (num <= 2803157229U)
					{
						if (num != 2694049387U)
						{
							if (num != 2700614742U)
							{
								if (num != 2803157229U)
								{
									goto IL_FA9;
								}
								if (!(text == "system.security.cryptography.sha256cng"))
								{
									goto IL_FA9;
								}
								goto IL_F45;
							}
							else
							{
								if (!(text == "sha384"))
								{
									goto IL_FA9;
								}
								goto IL_F4B;
							}
						}
						else
						{
							if (!(text == "sha512"))
							{
								goto IL_FA9;
							}
							goto IL_F51;
						}
					}
					else if (num != 2824063256U)
					{
						if (num != 2855136637U)
						{
							if (num != 2920802226U)
							{
								goto IL_FA9;
							}
							if (!(text == "rijndael"))
							{
								goto IL_FA9;
							}
							goto IL_EFD;
						}
						else
						{
							if (!(text == "hmacsha384"))
							{
								goto IL_FA9;
							}
							goto IL_EDF;
						}
					}
					else if (!(text == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha512"))
					{
						goto IL_FA9;
					}
				}
				else if (num <= 3106619289U)
				{
					if (num <= 3024233790U)
					{
						if (num != 2930374873U)
						{
							if (num != 2930817943U)
							{
								if (num != 3024233790U)
								{
									goto IL_FA9;
								}
								if (!(text == "system.security.cryptography.hmacsha384"))
								{
									goto IL_FA9;
								}
								goto IL_EDF;
							}
							else
							{
								if (!(text == "system.security.cryptography.sha1"))
								{
									goto IL_FA9;
								}
								goto IL_F39;
							}
						}
						else
						{
							if (!(text == "http://www.w3.org/2001/04/xmldsig-more#hmac-sha512"))
							{
								goto IL_FA9;
							}
							goto IL_EE5;
						}
					}
					else if (num != 3071220272U)
					{
						if (num != 3091284687U)
						{
							if (num != 3106619289U)
							{
								goto IL_FA9;
							}
							if (!(text == "http://www.w3.org/2001/04/xmldsig-more#hmac-ripemd160"))
							{
								goto IL_FA9;
							}
							goto IL_ECD;
						}
						else
						{
							if (!(text == "system.security.cryptography.rsapkcs1sha1signaturedescription"))
							{
								goto IL_FA9;
							}
							goto IL_F21;
						}
					}
					else if (!(text == "system.security.cryptography.rsapkcs1sha512signaturedescription"))
					{
						goto IL_FA9;
					}
				}
				else if (num <= 3193284448U)
				{
					if (num != 3155186700U)
					{
						if (num != 3177008669U)
						{
							if (num != 3193284448U)
							{
								goto IL_FA9;
							}
							if (!(text == "system.security.cryptography.rngcryptoserviceprovider"))
							{
								goto IL_FA9;
							}
							goto IL_F09;
						}
						else
						{
							if (!(text == "triple des"))
							{
								goto IL_FA9;
							}
							goto IL_F57;
						}
					}
					else
					{
						if (!(text == "system.security.cryptography.hmacsha1"))
						{
							goto IL_FA9;
						}
						goto IL_ED3;
					}
				}
				else if (num <= 3271835900U)
				{
					if (num != 3223542963U)
					{
						if (num != 3271835900U)
						{
							goto IL_FA9;
						}
						if (!(text == "randomnumbergenerator"))
						{
							goto IL_FA9;
						}
						goto IL_F09;
					}
					else
					{
						if (!(text == "dsa"))
						{
							goto IL_FA9;
						}
						goto IL_EA9;
					}
				}
				else if (num != 3295766687U)
				{
					if (num != 3339968437U)
					{
						goto IL_FA9;
					}
					if (!(text == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha384"))
					{
						goto IL_FA9;
					}
					goto IL_F2D;
				}
				else
				{
					if (!(text == "system.security.cryptography.hmacripemd160"))
					{
						goto IL_FA9;
					}
					goto IL_ECD;
				}
				return new RSAPKCS1SHA512SignatureDescription();
			}
			if (num <= 3841402386U)
			{
				if (num <= 3529085699U)
				{
					if (num <= 3457114317U)
					{
						if (num != 3416134926U)
						{
							if (num != 3442835812U)
							{
								if (num != 3457114317U)
								{
									goto IL_FA9;
								}
								if (!(text == "system.security.cryptography.sha512managed"))
								{
									goto IL_FA9;
								}
								goto IL_F51;
							}
							else
							{
								if (!(text == "system.security.cryptography.rsa"))
								{
									goto IL_FA9;
								}
								goto IL_F0F;
							}
						}
						else
						{
							if (!(text == "system.security.cryptography.des"))
							{
								goto IL_FA9;
							}
							goto IL_EC1;
						}
					}
					else if (num != 3506813397U)
					{
						if (num != 3523885206U)
						{
							if (num != 3529085699U)
							{
								goto IL_FA9;
							}
							if (!(text == "des"))
							{
								goto IL_FA9;
							}
							goto IL_EC1;
						}
						else
						{
							if (!(text == "2.5.29.14"))
							{
								goto IL_FA9;
							}
							type = Type.GetType("System.Security.Cryptography.X509Certificates.X509SubjectKeyIdentifierExtension, System");
							goto IL_FA9;
						}
					}
					else
					{
						if (!(text == "2.5.29.37"))
						{
							goto IL_FA9;
						}
						type = Type.GetType("System.Security.Cryptography.X509Certificates.X509EnhancedKeyUsageExtension, System");
						goto IL_FA9;
					}
				}
				else if (num <= 3708241362U)
				{
					if (num != 3540662825U)
					{
						if (num != 3569803917U)
						{
							if (num != 3708241362U)
							{
								goto IL_FA9;
							}
							if (!(text == "system.security.cryptography.sha512cng"))
							{
								goto IL_FA9;
							}
							goto IL_F51;
						}
						else
						{
							if (!(text == "system.security.cryptography.asymmetricalgorithm"))
							{
								goto IL_FA9;
							}
							goto IL_F0F;
						}
					}
					else
					{
						if (!(text == "2.5.29.15"))
						{
							goto IL_FA9;
						}
						type = Type.GetType("System.Security.Cryptography.X509Certificates.X509KeyUsageExtension, System");
						goto IL_FA9;
					}
				}
				else if (num <= 3741994253U)
				{
					if (num != 3711531707U)
					{
						if (num != 3741994253U)
						{
							goto IL_FA9;
						}
						if (!(text == "2.5.29.19"))
						{
							goto IL_FA9;
						}
						type = Type.GetType("System.Security.Cryptography.X509Certificates.X509BasicConstraintsExtension, System");
						goto IL_FA9;
					}
					else if (!(text == "system.security.cryptography.hmacmd5"))
					{
						goto IL_FA9;
					}
				}
				else if (num != 3772560434U)
				{
					if (num != 3841402386U)
					{
						goto IL_FA9;
					}
					if (!(text == "ripemd-160"))
					{
						goto IL_FA9;
					}
					goto IL_F03;
				}
				else
				{
					if (!(text == "http://www.w3.org/2001/04/xmlenc#sha512"))
					{
						goto IL_FA9;
					}
					goto IL_F51;
				}
			}
			else if (num <= 4070407701U)
			{
				if (num <= 3979214893U)
				{
					if (num != 3849984186U)
					{
						if (num != 3880483293U)
						{
							if (num != 3979214893U)
							{
								goto IL_FA9;
							}
							if (!(text == "sha"))
							{
								goto IL_FA9;
							}
							goto IL_F39;
						}
						else
						{
							if (!(text == "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"))
							{
								goto IL_FA9;
							}
							goto IL_F27;
						}
					}
					else
					{
						if (!(text == "system.security.cryptography.dsa"))
						{
							goto IL_FA9;
						}
						goto IL_EA9;
					}
				}
				else if (num != 3991591796U)
				{
					if (num != 4003337351U)
					{
						if (num != 4070407701U)
						{
							goto IL_FA9;
						}
						if (!(text == "system.security.cryptography.rijndael"))
						{
							goto IL_FA9;
						}
						goto IL_EFD;
					}
					else
					{
						if (!(text == "system.security.cryptography.dsacryptoserviceprovider"))
						{
							goto IL_FA9;
						}
						goto IL_EA9;
					}
				}
				else
				{
					if (!(text == "system.security.cryptography.sha512"))
					{
						goto IL_FA9;
					}
					goto IL_F51;
				}
			}
			else if (num <= 4109837759U)
			{
				if (num != 4092224578U)
				{
					if (num != 4106266752U)
					{
						if (num != 4109837759U)
						{
							goto IL_FA9;
						}
						if (!(text == "system.security.cryptography.tripledescryptoserviceprovider"))
						{
							goto IL_FA9;
						}
						goto IL_F57;
					}
					else
					{
						if (!(text == "mactripledes"))
						{
							goto IL_FA9;
						}
						goto IL_EEB;
					}
				}
				else if (!(text == "hmacmd5"))
				{
					goto IL_FA9;
				}
			}
			else if (num <= 4181060418U)
			{
				if (num != 4120168715U)
				{
					if (num != 4181060418U)
					{
						goto IL_FA9;
					}
					if (!(text == "system.security.cryptography.sha384managed"))
					{
						goto IL_FA9;
					}
					goto IL_F4B;
				}
				else
				{
					if (!(text == "system.security.cryptography.rsapkcs1signaturedeformatter"))
					{
						goto IL_FA9;
					}
					return new RSAPKCS1SignatureDeformatter();
				}
			}
			else if (num != 4199782769U)
			{
				if (num != 4265317454U)
				{
					goto IL_FA9;
				}
				if (!(text == "system.security.cryptography.symmetricalgorithm"))
				{
					goto IL_FA9;
				}
				goto IL_EFD;
			}
			else
			{
				if (!(text == "system.security.cryptography.sha384"))
				{
					goto IL_FA9;
				}
				goto IL_F4B;
			}
			return new HMACMD5();
			IL_EA9:
			return new DSACryptoServiceProvider();
			IL_EC1:
			return new DESCryptoServiceProvider();
			IL_ECD:
			return new HMACRIPEMD160();
			IL_ED3:
			return new HMACSHA1();
			IL_EDF:
			return new HMACSHA384();
			IL_EE5:
			return new HMACSHA512();
			IL_EEB:
			return new MACTripleDES();
			IL_EF7:
			return new RC2CryptoServiceProvider();
			IL_EFD:
			return new RijndaelManaged();
			IL_F03:
			return new RIPEMD160Managed();
			IL_F09:
			return new RNGCryptoServiceProvider();
			IL_F0F:
			return new RSACryptoServiceProvider();
			IL_F21:
			return new RSAPKCS1SHA1SignatureDescription();
			IL_F27:
			return new RSAPKCS1SHA256SignatureDescription();
			IL_F2D:
			return new RSAPKCS1SHA384SignatureDescription();
			IL_F39:
			return new SHA1CryptoServiceProvider();
			IL_F45:
			return new SHA256Managed();
			IL_F4B:
			return new SHA384Managed();
			IL_F51:
			return new SHA512Managed();
			IL_F57:
			return new TripleDESCryptoServiceProvider();
			IL_FA9:
			if (type == null)
			{
				object obj = CryptoConfig.lockObject;
				lock (obj)
				{
					Dictionary<string, Type> dictionary = CryptoConfig.algorithms;
					if (dictionary != null && dictionary.TryGetValue(name, out type))
					{
						try
						{
							return Activator.CreateInstance(type, args);
						}
						catch
						{
						}
					}
				}
				type = Type.GetType(name);
			}
			object result;
			try
			{
				result = Activator.CreateInstance(type, args);
			}
			catch
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060030DC RID: 12508 RVA: 0x000B2A88 File Offset: 0x000B0C88
		internal static string MapNameToOID(string name, object arg)
		{
			return CryptoConfig.MapNameToOID(name);
		}

		/// <summary>Gets the object identifier (OID) of the algorithm corresponding to the specified simple name.</summary>
		/// <param name="name">The simple name of the algorithm for which to get the OID.</param>
		/// <returns>The OID of the specified algorithm.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="name" /> parameter is <see langword="null" />.</exception>
		// Token: 0x060030DD RID: 12509 RVA: 0x000B2A90 File Offset: 0x000B0C90
		public static string MapNameToOID(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			string text = name.ToLowerInvariant();
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text);
			if (num <= 2393554675U)
			{
				if (num <= 1686995390U)
				{
					if (num <= 585245684U)
					{
						if (num != 289646596U)
						{
							if (num != 373238979U)
							{
								if (num != 585245684U)
								{
									goto IL_4A8;
								}
								if (!(text == "system.security.cryptography.sha1managed"))
								{
									goto IL_4A8;
								}
								goto IL_46C;
							}
							else
							{
								if (!(text == "system.security.cryptography.sha1cng"))
								{
									goto IL_4A8;
								}
								goto IL_46C;
							}
						}
						else
						{
							if (!(text == "system.security.cryptography.sha1cryptoserviceprovider"))
							{
								goto IL_4A8;
							}
							goto IL_46C;
						}
					}
					else if (num <= 1600607069U)
					{
						if (num != 1168228931U)
						{
							if (num != 1600607069U)
							{
								goto IL_4A8;
							}
							if (!(text == "system.security.cryptography.sha384cng"))
							{
								goto IL_4A8;
							}
							goto IL_47E;
						}
						else
						{
							if (!(text == "system.security.cryptography.ripemd160managed"))
							{
								goto IL_4A8;
							}
							goto IL_48A;
						}
					}
					else if (num != 1664836558U)
					{
						if (num != 1686995390U)
						{
							goto IL_4A8;
						}
						if (!(text == "system.security.cryptography.md5"))
						{
							goto IL_4A8;
						}
					}
					else
					{
						if (!(text == "system.security.cryptography.sha256cryptoserviceprovider"))
						{
							goto IL_4A8;
						}
						goto IL_478;
					}
				}
				else if (num <= 2131651891U)
				{
					if (num <= 2070555668U)
					{
						if (num != 1862521808U)
						{
							if (num != 2070555668U)
							{
								goto IL_4A8;
							}
							if (!(text == "sha1"))
							{
								goto IL_4A8;
							}
							goto IL_46C;
						}
						else
						{
							if (!(text == "rc2"))
							{
								goto IL_4A8;
							}
							return "1.2.840.113549.3.2";
						}
					}
					else if (num != 2102584679U)
					{
						if (num != 2131651891U)
						{
							goto IL_4A8;
						}
						if (!(text == "tripledes"))
						{
							goto IL_4A8;
						}
						return "1.2.840.113549.3.7";
					}
					else
					{
						if (!(text == "ripemd160"))
						{
							goto IL_4A8;
						}
						goto IL_48A;
					}
				}
				else if (num <= 2269936011U)
				{
					if (num != 2214607313U)
					{
						if (num != 2269936011U)
						{
							goto IL_4A8;
						}
						if (!(text == "system.security.cryptography.md5cryptoserviceprovider"))
						{
							goto IL_4A8;
						}
					}
					else
					{
						if (!(text == "system.security.cryptography.sha512cryptoserviceprovider"))
						{
							goto IL_4A8;
						}
						goto IL_484;
					}
				}
				else if (num != 2346491937U)
				{
					if (num != 2393554675U)
					{
						goto IL_4A8;
					}
					if (!(text == "md5"))
					{
						goto IL_4A8;
					}
				}
				else
				{
					if (!(text == "system.security.cryptography.sha256"))
					{
						goto IL_4A8;
					}
					goto IL_478;
				}
				return "1.2.840.113549.2.5";
			}
			if (num <= 2700614742U)
			{
				if (num <= 2484875538U)
				{
					if (num != 2394616414U)
					{
						if (num != 2451404838U)
						{
							if (num != 2484875538U)
							{
								goto IL_4A8;
							}
							if (!(text == "system.security.cryptography.sha256managed"))
							{
								goto IL_4A8;
							}
							goto IL_478;
						}
						else
						{
							if (!(text == "system.security.cryptography.ripemd160"))
							{
								goto IL_4A8;
							}
							goto IL_48A;
						}
					}
					else
					{
						if (!(text == "system.security.cryptography.sha384cryptoserviceprovider"))
						{
							goto IL_4A8;
						}
						goto IL_47E;
					}
				}
				else if (num <= 2672027822U)
				{
					if (num != 2631153146U)
					{
						if (num != 2672027822U)
						{
							goto IL_4A8;
						}
						if (!(text == "tripledeskeywrap"))
						{
							goto IL_4A8;
						}
						return "1.2.840.113549.1.9.16.3.6";
					}
					else
					{
						if (!(text == "sha256"))
						{
							goto IL_4A8;
						}
						goto IL_478;
					}
				}
				else if (num != 2694049387U)
				{
					if (num != 2700614742U)
					{
						goto IL_4A8;
					}
					if (!(text == "sha384"))
					{
						goto IL_4A8;
					}
					goto IL_47E;
				}
				else
				{
					if (!(text == "sha512"))
					{
						goto IL_4A8;
					}
					goto IL_484;
				}
			}
			else if (num <= 3529085699U)
			{
				if (num <= 2930817943U)
				{
					if (num != 2803157229U)
					{
						if (num != 2930817943U)
						{
							goto IL_4A8;
						}
						if (!(text == "system.security.cryptography.sha1"))
						{
							goto IL_4A8;
						}
					}
					else
					{
						if (!(text == "system.security.cryptography.sha256cng"))
						{
							goto IL_4A8;
						}
						goto IL_478;
					}
				}
				else if (num != 3457114317U)
				{
					if (num != 3529085699U)
					{
						goto IL_4A8;
					}
					if (!(text == "des"))
					{
						goto IL_4A8;
					}
					return "1.3.14.3.2.7";
				}
				else
				{
					if (!(text == "system.security.cryptography.sha512managed"))
					{
						goto IL_4A8;
					}
					goto IL_484;
				}
			}
			else if (num <= 3991591796U)
			{
				if (num != 3708241362U)
				{
					if (num != 3991591796U)
					{
						goto IL_4A8;
					}
					if (!(text == "system.security.cryptography.sha512"))
					{
						goto IL_4A8;
					}
					goto IL_484;
				}
				else
				{
					if (!(text == "system.security.cryptography.sha512cng"))
					{
						goto IL_4A8;
					}
					goto IL_484;
				}
			}
			else if (num != 4181060418U)
			{
				if (num != 4199782769U)
				{
					goto IL_4A8;
				}
				if (!(text == "system.security.cryptography.sha384"))
				{
					goto IL_4A8;
				}
				goto IL_47E;
			}
			else
			{
				if (!(text == "system.security.cryptography.sha384managed"))
				{
					goto IL_4A8;
				}
				goto IL_47E;
			}
			IL_46C:
			return "1.3.14.3.2.26";
			IL_478:
			return "2.16.840.1.101.3.4.2.1";
			IL_47E:
			return "2.16.840.1.101.3.4.2.2";
			IL_484:
			return "2.16.840.1.101.3.4.2.3";
			IL_48A:
			return "1.3.36.3.2.1";
			IL_4A8:
			return null;
		}

		// Token: 0x060030DE RID: 12510 RVA: 0x000B2F46 File Offset: 0x000B1146
		private static void Initialize()
		{
			CryptoConfig.algorithms = new Dictionary<string, Type>(StringComparer.OrdinalIgnoreCase);
		}

		/// <summary>Adds a set of names to algorithm mappings to be used for the current application domain.</summary>
		/// <param name="algorithm">The algorithm to map to.</param>
		/// <param name="names">An array of names to map to the algorithm.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="algorithm" /> or <paramref name="names" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="algorithm" /> cannot be accessed from outside the assembly.  
		/// -or-  
		/// One of the entries in the <paramref name="names" /> parameter is empty or <see langword="null" />.</exception>
		// Token: 0x060030DF RID: 12511 RVA: 0x000B2F58 File Offset: 0x000B1158
		public static void AddAlgorithm(Type algorithm, params string[] names)
		{
			if (algorithm == null)
			{
				throw new ArgumentNullException("algorithm");
			}
			if (!algorithm.IsVisible)
			{
				throw new ArgumentException("Algorithms added to CryptoConfig must be accessable from outside their assembly.", "algorithm");
			}
			if (names == null)
			{
				throw new ArgumentNullException("names");
			}
			string[] array = new string[names.Length];
			Array.Copy(names, array, array.Length);
			string[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				if (string.IsNullOrEmpty(array2[i]))
				{
					throw new ArgumentException("CryptoConfig cannot add a mapping for a null or empty name.");
				}
			}
			object obj = CryptoConfig.lockObject;
			lock (obj)
			{
				if (CryptoConfig.algorithms == null)
				{
					CryptoConfig.Initialize();
				}
				foreach (string key in array)
				{
					CryptoConfig.algorithms[key] = algorithm;
				}
			}
		}

		/// <summary>Encodes the specified object identifier (OID).</summary>
		/// <param name="str">The OID to encode.</param>
		/// <returns>A byte array containing the encoded OID.</returns>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="str" /> parameter is <see langword="null" />.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicUnexpectedOperationException">An error occurred while encoding the OID.</exception>
		// Token: 0x060030E0 RID: 12512 RVA: 0x000B3034 File Offset: 0x000B1234
		public static byte[] EncodeOID(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			char[] separator = new char[]
			{
				'.'
			};
			string[] array = str.Split(separator);
			if (array.Length < 2)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("OID must have at least two parts"));
			}
			byte[] array2 = new byte[str.Length];
			try
			{
				byte b = Convert.ToByte(array[0]);
				byte b2 = Convert.ToByte(array[1]);
				array2[2] = Convert.ToByte((int)(b * 40 + b2));
			}
			catch
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("Invalid OID"));
			}
			int num = 3;
			for (int i = 2; i < array.Length; i++)
			{
				long num2 = Convert.ToInt64(array[i]);
				if (num2 > 127L)
				{
					byte[] array3 = CryptoConfig.EncodeLongNumber(num2);
					Buffer.BlockCopy(array3, 0, array2, num, array3.Length);
					num += array3.Length;
				}
				else
				{
					array2[num++] = Convert.ToByte(num2);
				}
			}
			int num3 = 2;
			byte[] array4 = new byte[num];
			array4[0] = 6;
			if (num > 127)
			{
				throw new CryptographicUnexpectedOperationException(Locale.GetText("OID > 127 bytes"));
			}
			array4[1] = Convert.ToByte(num - 2);
			Buffer.BlockCopy(array2, num3, array4, num3, num - num3);
			return array4;
		}

		// Token: 0x060030E1 RID: 12513 RVA: 0x000B3164 File Offset: 0x000B1364
		private static byte[] EncodeLongNumber(long x)
		{
			if (x > 2147483647L || x < -2147483648L)
			{
				throw new OverflowException(Locale.GetText("Part of OID doesn't fit in Int32"));
			}
			long num = x;
			int num2 = 1;
			while (num > 127L)
			{
				num >>= 7;
				num2++;
			}
			byte[] array = new byte[num2];
			for (int i = 0; i < num2; i++)
			{
				num = x >> 7 * i;
				num &= 127L;
				if (i != 0)
				{
					num += 128L;
				}
				array[num2 - i - 1] = Convert.ToByte(num);
			}
			return array;
		}

		/// <summary>Indicates whether the runtime should enforce the policy to create only Federal Information Processing Standard (FIPS) certified algorithms.</summary>
		/// <returns>
		///   <see langword="true" /> to enforce the policy; otherwise, <see langword="false" />.</returns>
		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		[MonoLimitation("nothing is FIPS certified so it never make sense to restrict to this (empty) subset")]
		public static bool AllowOnlyFipsAlgorithms
		{
			get
			{
				return false;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.CryptoConfig" /> class.</summary>
		// Token: 0x060030E3 RID: 12515 RVA: 0x0000259F File Offset: 0x0000079F
		public CryptoConfig()
		{
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x000B31E3 File Offset: 0x000B13E3
		// Note: this type is marked as 'beforefieldinit'.
		static CryptoConfig()
		{
		}

		// Token: 0x04002244 RID: 8772
		private static readonly object lockObject = new object();

		// Token: 0x04002245 RID: 8773
		private static Dictionary<string, Type> algorithms;
	}
}

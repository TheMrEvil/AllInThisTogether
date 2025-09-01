using System;
using System.Globalization;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using Mono.Security.Interface;
using Mono.Security.X509;
using Mono.Security.X509.Extensions;

namespace Mono.Net.Security
{
	// Token: 0x020000A9 RID: 169
	internal static class SystemCertificateValidator
	{
		// Token: 0x06000355 RID: 853 RVA: 0x00009E74 File Offset: 0x00008074
		static SystemCertificateValidator()
		{
			SystemCertificateValidator.is_macosx = (Environment.OSVersion.Platform != PlatformID.Win32NT && File.Exists("/System/Library/Frameworks/Security.framework/Security"));
			SystemCertificateValidator.revocation_mode = X509RevocationMode.NoCheck;
			try
			{
				string environmentVariable = Environment.GetEnvironmentVariable("MONO_X509_REVOCATION_MODE");
				if (!string.IsNullOrEmpty(environmentVariable))
				{
					SystemCertificateValidator.revocation_mode = (X509RevocationMode)Enum.Parse(typeof(X509RevocationMode), environmentVariable, true);
				}
			}
			catch
			{
			}
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00009EF8 File Offset: 0x000080F8
		public static System.Security.Cryptography.X509Certificates.X509Chain CreateX509Chain(System.Security.Cryptography.X509Certificates.X509CertificateCollection certs)
		{
			return new System.Security.Cryptography.X509Certificates.X509Chain
			{
				ChainPolicy = new X509ChainPolicy((System.Security.Cryptography.X509Certificates.X509CertificateCollection)certs),
				ChainPolicy = 
				{
					RevocationMode = SystemCertificateValidator.revocation_mode
				}
			};
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009F20 File Offset: 0x00008120
		private static bool BuildX509Chain(System.Security.Cryptography.X509Certificates.X509CertificateCollection certs, System.Security.Cryptography.X509Certificates.X509Chain chain, ref SslPolicyErrors errors, ref int status11)
		{
			if (SystemCertificateValidator.is_macosx)
			{
				return false;
			}
			X509Certificate2 certificate = (X509Certificate2)certs[0];
			bool flag;
			try
			{
				flag = chain.Build(certificate);
				if (!flag)
				{
					errors |= SystemCertificateValidator.GetErrorsFromChain(chain);
				}
			}
			catch (Exception arg)
			{
				Console.Error.WriteLine("ERROR building certificate chain: {0}", arg);
				Console.Error.WriteLine("Please, report this problem to the Mono team");
				errors |= SslPolicyErrors.RemoteCertificateChainErrors;
				flag = false;
			}
			try
			{
				status11 = SystemCertificateValidator.GetStatusFromChain(chain);
			}
			catch
			{
				status11 = -2146762485;
			}
			return flag;
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00009FB8 File Offset: 0x000081B8
		private static bool CheckUsage(System.Security.Cryptography.X509Certificates.X509CertificateCollection certs, string host, ref SslPolicyErrors errors, ref int status11)
		{
			X509Certificate2 x509Certificate = certs[0] as X509Certificate2;
			if (x509Certificate == null)
			{
				x509Certificate = new X509Certificate2(certs[0]);
			}
			if (!SystemCertificateValidator.is_macosx)
			{
				if (!SystemCertificateValidator.CheckCertificateUsage(x509Certificate))
				{
					errors |= SslPolicyErrors.RemoteCertificateChainErrors;
					status11 = -2146762490;
					return false;
				}
				if (!string.IsNullOrEmpty(host) && !SystemCertificateValidator.CheckServerIdentity(x509Certificate, host))
				{
					errors |= SslPolicyErrors.RemoteCertificateNameMismatch;
					status11 = -2146762481;
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000A024 File Offset: 0x00008224
		private static bool EvaluateSystem(System.Security.Cryptography.X509Certificates.X509CertificateCollection certs, System.Security.Cryptography.X509Certificates.X509CertificateCollection anchors, string host, System.Security.Cryptography.X509Certificates.X509Chain chain, ref SslPolicyErrors errors, ref int status11)
		{
			System.Security.Cryptography.X509Certificates.X509Certificate x509Certificate = certs[0];
			bool flag;
			if (SystemCertificateValidator.is_macosx)
			{
				OSX509Certificates.SecTrustResult secTrustResult = OSX509Certificates.SecTrustResult.Deny;
				try
				{
					secTrustResult = OSX509Certificates.TrustEvaluateSsl(certs, anchors, host);
					flag = (secTrustResult == OSX509Certificates.SecTrustResult.Proceed || secTrustResult == OSX509Certificates.SecTrustResult.Unspecified);
				}
				catch
				{
					flag = false;
					errors |= SslPolicyErrors.RemoteCertificateChainErrors;
				}
				if (flag)
				{
					errors = SslPolicyErrors.None;
				}
				else
				{
					status11 = (int)secTrustResult;
					errors |= SslPolicyErrors.RemoteCertificateChainErrors;
				}
			}
			else
			{
				flag = SystemCertificateValidator.BuildX509Chain(certs, chain, ref errors, ref status11);
			}
			return flag;
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000A09C File Offset: 0x0000829C
		public static bool Evaluate(MonoTlsSettings settings, string host, System.Security.Cryptography.X509Certificates.X509CertificateCollection certs, System.Security.Cryptography.X509Certificates.X509Chain chain, ref SslPolicyErrors errors, ref int status11)
		{
			if (!SystemCertificateValidator.CheckUsage(certs, host, ref errors, ref status11))
			{
				return false;
			}
			if (settings != null && settings.SkipSystemValidators)
			{
				return false;
			}
			System.Security.Cryptography.X509Certificates.X509CertificateCollection anchors = (settings != null) ? settings.TrustAnchors : null;
			return SystemCertificateValidator.EvaluateSystem(certs, anchors, host, chain, ref errors, ref status11);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000A0DF File Offset: 0x000082DF
		internal static bool NeedsChain(MonoTlsSettings settings)
		{
			return !SystemCertificateValidator.is_macosx || (CertificateValidationHelper.SupportsX509Chain && (settings == null || !settings.SkipSystemValidators || settings.CallbackNeedsCertificateChain));
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000A108 File Offset: 0x00008308
		private static int GetStatusFromChain(System.Security.Cryptography.X509Certificates.X509Chain chain)
		{
			long num = 0L;
			X509ChainStatus[] chainStatus = chain.ChainStatus;
			int i = 0;
			while (i < chainStatus.Length)
			{
				X509ChainStatus x509ChainStatus = chainStatus[i];
				System.Security.Cryptography.X509Certificates.X509ChainStatusFlags status = x509ChainStatus.Status;
				if (status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
				{
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotTimeValid) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762495);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotTimeNested) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762494);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.Revoked) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762484);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotSignatureValid) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146869244);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NotValidForUsage) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762480);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.UntrustedRoot) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762487);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.RevocationStatusUnknown) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146885614);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.Cyclic) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762486);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidExtension) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762485);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidPolicyConstraints) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762483);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidBasicConstraints) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146869223);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.InvalidNameConstraints) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762476);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasNotSupportedNameConstraint) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762476);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasNotDefinedNameConstraint) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762476);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasNotPermittedNameConstraint) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762476);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.HasExcludedNameConstraint) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762476);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.PartialChain) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762486);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.CtlNotTimeValid) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762495);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.CtlNotSignatureValid) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146869244);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.CtlNotValidForUsage) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762480);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.OfflineRevocation) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146885614);
						break;
					}
					if ((status & System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoIssuanceChainPolicy) != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
					{
						num = (long)((ulong)-2146762489);
						break;
					}
					num = (long)((ulong)-2146762485);
					break;
				}
				else
				{
					i++;
				}
			}
			return (int)num;
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000A310 File Offset: 0x00008510
		private static SslPolicyErrors GetErrorsFromChain(System.Security.Cryptography.X509Certificates.X509Chain chain)
		{
			SslPolicyErrors sslPolicyErrors = SslPolicyErrors.None;
			foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
			{
				if (x509ChainStatus.Status != System.Security.Cryptography.X509Certificates.X509ChainStatusFlags.NoError)
				{
					sslPolicyErrors |= SslPolicyErrors.RemoteCertificateChainErrors;
					break;
				}
			}
			return sslPolicyErrors;
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000A34C File Offset: 0x0000854C
		private static bool CheckCertificateUsage(X509Certificate2 cert)
		{
			bool result;
			try
			{
				if (cert.Version < 3)
				{
					result = true;
				}
				else
				{
					X509KeyUsageExtension x509KeyUsageExtension = cert.Extensions["2.5.29.15"] as X509KeyUsageExtension;
					X509EnhancedKeyUsageExtension x509EnhancedKeyUsageExtension = cert.Extensions["2.5.29.37"] as X509EnhancedKeyUsageExtension;
					if (x509KeyUsageExtension != null && x509EnhancedKeyUsageExtension != null)
					{
						if ((x509KeyUsageExtension.KeyUsages & SystemCertificateValidator.s_flags) == X509KeyUsageFlags.None)
						{
							result = false;
						}
						else
						{
							result = (x509EnhancedKeyUsageExtension.EnhancedKeyUsages["1.3.6.1.5.5.7.3.1"] != null || x509EnhancedKeyUsageExtension.EnhancedKeyUsages["2.16.840.1.113730.4.1"] != null);
						}
					}
					else if (x509KeyUsageExtension != null)
					{
						result = ((x509KeyUsageExtension.KeyUsages & SystemCertificateValidator.s_flags) > X509KeyUsageFlags.None);
					}
					else if (x509EnhancedKeyUsageExtension != null)
					{
						result = (x509EnhancedKeyUsageExtension.EnhancedKeyUsages["1.3.6.1.5.5.7.3.1"] != null || x509EnhancedKeyUsageExtension.EnhancedKeyUsages["2.16.840.1.113730.4.1"] != null);
					}
					else
					{
						System.Security.Cryptography.X509Certificates.X509Extension x509Extension = cert.Extensions["2.16.840.1.113730.1.1"];
						if (x509Extension != null)
						{
							result = (x509Extension.NetscapeCertType(false).IndexOf("SSL Server Authentication", StringComparison.Ordinal) != -1);
						}
						else
						{
							result = true;
						}
					}
				}
			}
			catch (Exception arg)
			{
				Console.Error.WriteLine("ERROR processing certificate: {0}", arg);
				Console.Error.WriteLine("Please, report this problem to the Mono team");
				result = false;
			}
			return result;
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000A490 File Offset: 0x00008690
		private static bool CheckServerIdentity(X509Certificate2 cert, string targetHost)
		{
			bool result;
			try
			{
				Mono.Security.X509.X509Certificate x509Certificate = new Mono.Security.X509.X509Certificate(cert.RawData);
				Mono.Security.X509.X509Extension x509Extension = x509Certificate.Extensions["2.5.29.17"];
				if (x509Extension != null)
				{
					SubjectAltNameExtension subjectAltNameExtension = new SubjectAltNameExtension(x509Extension);
					foreach (string pattern in subjectAltNameExtension.DNSNames)
					{
						if (SystemCertificateValidator.Match(targetHost, pattern))
						{
							return true;
						}
					}
					string[] array = subjectAltNameExtension.IPAddresses;
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] == targetHost)
						{
							return true;
						}
					}
				}
				result = SystemCertificateValidator.CheckDomainName(x509Certificate.SubjectName, targetHost);
			}
			catch (Exception arg)
			{
				Console.Error.WriteLine("ERROR processing certificate: {0}", arg);
				Console.Error.WriteLine("Please, report this problem to the Mono team");
				result = false;
			}
			return result;
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000A568 File Offset: 0x00008768
		private static bool CheckDomainName(string subjectName, string targetHost)
		{
			string pattern = string.Empty;
			MatchCollection matchCollection = new Regex("CN\\s*=\\s*([^,]*)").Matches(subjectName);
			if (matchCollection.Count == 1 && matchCollection[0].Success)
			{
				pattern = matchCollection[0].Groups[1].Value.ToString();
			}
			return SystemCertificateValidator.Match(targetHost, pattern);
		}

		// Token: 0x06000361 RID: 865 RVA: 0x0000A5C8 File Offset: 0x000087C8
		private static bool Match(string hostname, string pattern)
		{
			int num = pattern.IndexOf('*');
			if (num == -1)
			{
				return string.Compare(hostname, pattern, true, CultureInfo.InvariantCulture) == 0;
			}
			if (num != pattern.Length - 1 && pattern[num + 1] != '.')
			{
				return false;
			}
			if (pattern.IndexOf('*', num + 1) != -1)
			{
				return false;
			}
			string text = pattern.Substring(num + 1);
			int num2 = hostname.Length - text.Length;
			if (num2 <= 0)
			{
				return false;
			}
			if (string.Compare(hostname, num2, text, 0, text.Length, true, CultureInfo.InvariantCulture) != 0)
			{
				return false;
			}
			if (num == 0)
			{
				int num3 = hostname.IndexOf('.');
				return num3 == -1 || num3 >= hostname.Length - text.Length;
			}
			string text2 = pattern.Substring(0, num);
			return string.Compare(hostname, 0, text2, 0, text2.Length, true, CultureInfo.InvariantCulture) == 0;
		}

		// Token: 0x0400028F RID: 655
		private static bool is_macosx;

		// Token: 0x04000290 RID: 656
		private static X509RevocationMode revocation_mode;

		// Token: 0x04000291 RID: 657
		private static X509KeyUsageFlags s_flags = X509KeyUsageFlags.KeyAgreement | X509KeyUsageFlags.KeyEncipherment | X509KeyUsageFlags.DigitalSignature;
	}
}

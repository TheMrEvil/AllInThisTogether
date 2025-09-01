using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000058 RID: 88
	internal static class SignedXmlDebugLog
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000296 RID: 662 RVA: 0x0000BD00 File Offset: 0x00009F00
		private static bool InformationLoggingEnabled
		{
			get
			{
				if (!SignedXmlDebugLog.s_haveInformationLogging)
				{
					SignedXmlDebugLog.s_informationLogging = SignedXmlDebugLog.s_traceSource.Switch.ShouldTrace(TraceEventType.Information);
					SignedXmlDebugLog.s_haveInformationLogging = true;
				}
				return SignedXmlDebugLog.s_informationLogging;
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000BD31 File Offset: 0x00009F31
		private static bool VerboseLoggingEnabled
		{
			get
			{
				if (!SignedXmlDebugLog.s_haveVerboseLogging)
				{
					SignedXmlDebugLog.s_verboseLogging = SignedXmlDebugLog.s_traceSource.Switch.ShouldTrace(TraceEventType.Verbose);
					SignedXmlDebugLog.s_haveVerboseLogging = true;
				}
				return SignedXmlDebugLog.s_verboseLogging;
			}
		}

		// Token: 0x06000298 RID: 664 RVA: 0x0000BD64 File Offset: 0x00009F64
		private static string FormatBytes(byte[] bytes)
		{
			if (bytes == null)
			{
				return "(null)";
			}
			StringBuilder stringBuilder = new StringBuilder(bytes.Length * 2);
			foreach (byte b in bytes)
			{
				stringBuilder.Append(b.ToString("x2", CultureInfo.InvariantCulture));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000BDB8 File Offset: 0x00009FB8
		private static string GetKeyName(object key)
		{
			ICspAsymmetricAlgorithm cspAsymmetricAlgorithm = key as ICspAsymmetricAlgorithm;
			X509Certificate x509Certificate = key as X509Certificate;
			X509Certificate2 x509Certificate2 = key as X509Certificate2;
			string arg;
			if (cspAsymmetricAlgorithm != null && cspAsymmetricAlgorithm.CspKeyContainerInfo.KeyContainerName != null)
			{
				arg = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", cspAsymmetricAlgorithm.CspKeyContainerInfo.KeyContainerName);
			}
			else if (x509Certificate2 != null)
			{
				arg = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", x509Certificate2.GetNameInfo(X509NameType.SimpleName, false));
			}
			else if (x509Certificate != null)
			{
				arg = string.Format(CultureInfo.InvariantCulture, "\"{0}\"", x509Certificate.Subject);
			}
			else
			{
				arg = key.GetHashCode().ToString("x8", CultureInfo.InvariantCulture);
			}
			return string.Format(CultureInfo.InvariantCulture, "{0}#{1}", key.GetType().Name, arg);
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000BE78 File Offset: 0x0000A078
		private static string GetObjectId(object o)
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}#{1}", o.GetType().Name, o.GetHashCode().ToString("x8", CultureInfo.InvariantCulture));
		}

		// Token: 0x0600029B RID: 667 RVA: 0x0000BEB8 File Offset: 0x0000A0B8
		private static string GetOidName(Oid oid)
		{
			string text = oid.FriendlyName;
			if (string.IsNullOrEmpty(text))
			{
				text = oid.Value;
			}
			return text;
		}

		// Token: 0x0600029C RID: 668 RVA: 0x0000BEDC File Offset: 0x0000A0DC
		internal static void LogBeginCanonicalization(SignedXml signedXml, Transform canonicalizationTransform)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Beginning canonicalization using \"{0}\" ({1}).", canonicalizationTransform.Algorithm, canonicalizationTransform.GetType().Name);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.BeginCanonicalization, data);
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, "Canonicalization transform is using resolver {0} and base URI \"{1}\".", canonicalizationTransform.Resolver.GetType(), canonicalizationTransform.BaseURI);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.BeginCanonicalization, data2);
			}
		}

		// Token: 0x0600029D RID: 669 RVA: 0x0000BF4C File Offset: 0x0000A14C
		internal static void LogBeginCheckSignatureFormat(SignedXml signedXml, Func<SignedXml, bool> formatValidator)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				MethodInfo method = formatValidator.Method;
				string data = string.Format(CultureInfo.InvariantCulture, "Checking signature format using format validator \"[{0}] {1}.{2}\".", method.Module.Assembly.FullName, method.DeclaringType.FullName, method.Name);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.BeginCheckSignatureFormat, data);
			}
		}

		// Token: 0x0600029E RID: 670 RVA: 0x0000BFA4 File Offset: 0x0000A1A4
		internal static void LogBeginCheckSignedInfo(SignedXml signedXml, SignedInfo signedInfo)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Checking signature on SignedInfo with id \"{0}\".", (signedInfo.Id != null) ? signedInfo.Id : "(null)");
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.BeginCheckSignedInfo, data);
			}
		}

		// Token: 0x0600029F RID: 671 RVA: 0x0000BFE8 File Offset: 0x0000A1E8
		internal static void LogBeginSignatureComputation(SignedXml signedXml, XmlElement context)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.BeginSignatureComputation, "Beginning signature computation.");
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Using context: {0}", (context != null) ? context.OuterXml : "(null)");
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.BeginSignatureComputation, data);
			}
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x0000C03C File Offset: 0x0000A23C
		internal static void LogBeginSignatureVerification(SignedXml signedXml, XmlElement context)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.BeginSignatureVerification, "Beginning signature verification.");
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Using context: {0}", (context != null) ? context.OuterXml : "(null)");
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.BeginSignatureVerification, data);
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000C090 File Offset: 0x0000A290
		internal static void LogCanonicalizedOutput(SignedXml signedXml, Transform canonicalizationTransform)
		{
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				using (StreamReader streamReader = new StreamReader(canonicalizationTransform.GetOutput(typeof(Stream)) as Stream))
				{
					string data = string.Format(CultureInfo.InvariantCulture, "Output of canonicalization transform: {0}", streamReader.ReadToEnd());
					SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.CanonicalizedData, data);
				}
			}
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000C0FC File Offset: 0x0000A2FC
		internal static void LogFormatValidationResult(SignedXml signedXml, bool result)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = result ? "Signature format validation was successful." : "Signature format validation failed.";
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.FormatValidationResult, data);
			}
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x0000C12C File Offset: 0x0000A32C
		internal static void LogUnsafeCanonicalizationMethod(SignedXml signedXml, string algorithm, IEnumerable<string> validAlgorithms)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string arg in validAlgorithms)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendFormat("\"{0}\"", arg);
				}
				string data = string.Format(CultureInfo.InvariantCulture, "Canonicalization method \"{0}\" is not on the safe list. Safe canonicalization methods are: {1}.", algorithm, stringBuilder.ToString());
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.UnsafeCanonicalizationMethod, data);
			}
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x0000C1BC File Offset: 0x0000A3BC
		internal static void LogUnsafeTransformMethod(SignedXml signedXml, string algorithm, IEnumerable<string> validC14nAlgorithms, IEnumerable<string> validTransformAlgorithms)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (string arg in validC14nAlgorithms)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendFormat("\"{0}\"", arg);
				}
				foreach (string arg2 in validTransformAlgorithms)
				{
					if (stringBuilder.Length != 0)
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.AppendFormat("\"{0}\"", arg2);
				}
				string data = string.Format(CultureInfo.InvariantCulture, "Transform method \"{0}\" is not on the safe list. Safe transform methods are: {1}.", algorithm, stringBuilder.ToString());
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.UnsafeTransformMethod, data);
			}
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x0000C2A4 File Offset: 0x0000A4A4
		internal static void LogNamespacePropagation(SignedXml signedXml, XmlNodeList namespaces)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				if (namespaces != null)
				{
					using (IEnumerator enumerator = namespaces.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							object obj = enumerator.Current;
							XmlAttribute xmlAttribute = (XmlAttribute)obj;
							string data = string.Format(CultureInfo.InvariantCulture, "Propagating namespace {0}=\"{1}\".", xmlAttribute.Name, xmlAttribute.Value);
							SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.NamespacePropagation, data);
						}
						return;
					}
				}
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.NamespacePropagation, "No namespaces are being propagated.");
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000C330 File Offset: 0x0000A530
		internal static Stream LogReferenceData(Reference reference, Stream data)
		{
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				MemoryStream memoryStream = new MemoryStream();
				byte[] array = new byte[4096];
				int num;
				do
				{
					num = data.Read(array, 0, array.Length);
					memoryStream.Write(array, 0, num);
				}
				while (num == array.Length);
				string data2 = string.Format(CultureInfo.InvariantCulture, "Transformed reference contents: {0}", Encoding.UTF8.GetString(memoryStream.ToArray()));
				SignedXmlDebugLog.WriteLine(reference, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.ReferenceData, data2);
				memoryStream.Seek(0L, SeekOrigin.Begin);
				return memoryStream;
			}
			return data;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x0000C3AC File Offset: 0x0000A5AC
		internal static void LogSigning(SignedXml signedXml, object key, SignatureDescription signatureDescription, HashAlgorithm hash, AsymmetricSignatureFormatter asymmetricSignatureFormatter)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Calculating signature with key {0} using signature description {1}, hash algorithm {2}, and asymmetric signature formatter {3}.", new object[]
				{
					SignedXmlDebugLog.GetKeyName(key),
					signatureDescription.GetType().Name,
					hash.GetType().Name,
					asymmetricSignatureFormatter.GetType().Name
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.Signing, data);
			}
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x0000C414 File Offset: 0x0000A614
		internal static void LogSigning(SignedXml signedXml, KeyedHashAlgorithm key)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Calculating signature using keyed hash algorithm {0}.", key.GetType().Name);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.Signing, data);
			}
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x0000C450 File Offset: 0x0000A650
		internal static void LogSigningReference(SignedXml signedXml, Reference reference)
		{
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				HashAlgorithm hashAlgorithm = CryptoHelpers.CreateFromName<HashAlgorithm>(reference.DigestMethod);
				string text = (hashAlgorithm == null) ? "null" : hashAlgorithm.GetType().Name;
				string data = string.Format(CultureInfo.InvariantCulture, "Hashing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\" with hash algorithm \"{4}\" ({5}).", new object[]
				{
					SignedXmlDebugLog.GetObjectId(reference),
					reference.Uri,
					reference.Id,
					reference.Type,
					reference.DigestMethod,
					text
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.SigningReference, data);
			}
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000C4D8 File Offset: 0x0000A6D8
		internal static void LogVerificationFailure(SignedXml signedXml, string failureLocation)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Verification failed checking {0}.", failureLocation);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.VerificationFailure, data);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000C508 File Offset: 0x0000A708
		internal static void LogVerificationResult(SignedXml signedXml, object key, bool verified)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string format = verified ? "Verification with key {0} was successful." : "Verification with key {0} was not successful.";
				string data = string.Format(CultureInfo.InvariantCulture, format, SignedXmlDebugLog.GetKeyName(key));
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.SignatureVerificationResult, data);
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000C548 File Offset: 0x0000A748
		internal static void LogVerifyKeyUsage(SignedXml signedXml, X509Certificate certificate, X509KeyUsageExtension keyUsages)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Found key usages \"{0}\" in extension {1} on certificate {2}.", keyUsages.KeyUsages, SignedXmlDebugLog.GetOidName(keyUsages.Oid), SignedXmlDebugLog.GetKeyName(certificate));
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data);
			}
		}

		// Token: 0x060002AD RID: 685 RVA: 0x0000C594 File Offset: 0x0000A794
		internal static void LogVerifyReference(SignedXml signedXml, Reference reference)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Processing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\".", new object[]
				{
					SignedXmlDebugLog.GetObjectId(reference),
					reference.Uri,
					reference.Id,
					reference.Type
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifyReference, data);
			}
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000C5F0 File Offset: 0x0000A7F0
		internal static void LogVerifyReferenceHash(SignedXml signedXml, Reference reference, byte[] actualHash, byte[] expectedHash)
		{
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				HashAlgorithm hashAlgorithm = CryptoHelpers.CreateFromName<HashAlgorithm>(reference.DigestMethod);
				string text = (hashAlgorithm == null) ? "null" : hashAlgorithm.GetType().Name;
				string data = string.Format(CultureInfo.InvariantCulture, "Reference {0} hashed with \"{1}\" ({2}) has hash value {3}, expected hash value {4}.", new object[]
				{
					SignedXmlDebugLog.GetObjectId(reference),
					reference.DigestMethod,
					text,
					SignedXmlDebugLog.FormatBytes(actualHash),
					SignedXmlDebugLog.FormatBytes(expectedHash)
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifyReference, data);
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000C670 File Offset: 0x0000A870
		internal static void LogVerifySignedInfo(SignedXml signedXml, AsymmetricAlgorithm key, SignatureDescription signatureDescription, HashAlgorithm hashAlgorithm, AsymmetricSignatureDeformatter asymmetricSignatureDeformatter, byte[] actualHashValue, byte[] signatureValue)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Verifying SignedInfo using key {0}, signature description {1}, hash algorithm {2}, and asymmetric signature deformatter {3}.", new object[]
				{
					SignedXmlDebugLog.GetKeyName(key),
					signatureDescription.GetType().Name,
					hashAlgorithm.GetType().Name,
					asymmetricSignatureDeformatter.GetType().Name
				});
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data);
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, "Actual hash value: {0}", SignedXmlDebugLog.FormatBytes(actualHashValue));
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data2);
				string data3 = string.Format(CultureInfo.InvariantCulture, "Raw signature: {0}", SignedXmlDebugLog.FormatBytes(signatureValue));
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data3);
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000C724 File Offset: 0x0000A924
		internal static void LogVerifySignedInfo(SignedXml signedXml, KeyedHashAlgorithm mac, byte[] actualHashValue, byte[] signatureValue)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Verifying SignedInfo using keyed hash algorithm {0}.", mac.GetType().Name);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data);
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, "Actual hash value: {0}", SignedXmlDebugLog.FormatBytes(actualHashValue));
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data2);
				string data3 = string.Format(CultureInfo.InvariantCulture, "Raw signature: {0}", SignedXmlDebugLog.FormatBytes(signatureValue));
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data3);
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000C7A8 File Offset: 0x0000A9A8
		internal static void LogVerifyX509Chain(SignedXml signedXml, X509Chain chain, X509Certificate certificate)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				string data = string.Format(CultureInfo.InvariantCulture, "Building and verifying the X509 chain for certificate {0}.", SignedXmlDebugLog.GetKeyName(certificate));
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data);
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				string data2 = string.Format(CultureInfo.InvariantCulture, "Revocation mode for chain building: {0}.", chain.ChainPolicy.RevocationFlag);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data2);
				string data3 = string.Format(CultureInfo.InvariantCulture, "Revocation flag for chain building: {0}.", chain.ChainPolicy.RevocationFlag);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data3);
				string data4 = string.Format(CultureInfo.InvariantCulture, "Verification flags for chain building: {0}.", chain.ChainPolicy.VerificationFlags);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data4);
				string data5 = string.Format(CultureInfo.InvariantCulture, "Verification time for chain building: {0}.", chain.ChainPolicy.VerificationTime);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data5);
				string data6 = string.Format(CultureInfo.InvariantCulture, "URL retrieval timeout for chain building: {0}.", chain.ChainPolicy.UrlRetrievalTimeout);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data6);
			}
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					if (x509ChainStatus.Status != X509ChainStatusFlags.NoError)
					{
						string data7 = string.Format(CultureInfo.InvariantCulture, "Error building X509 chain: {0}: {1}.", x509ChainStatus.Status, x509ChainStatus.StatusInformation);
						SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, data7);
					}
				}
			}
			if (SignedXmlDebugLog.VerboseLoggingEnabled)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("Certificate chain:");
				foreach (X509ChainElement x509ChainElement in chain.ChainElements)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, " {0}", SignedXmlDebugLog.GetKeyName(x509ChainElement.Certificate));
				}
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Verbose, SignedXmlDebugLog.SignedXmlDebugEvent.X509Verification, stringBuilder.ToString());
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000C994 File Offset: 0x0000AB94
		internal static void LogSignedXmlRecursionLimit(SignedXml signedXml, Reference reference)
		{
			if (SignedXmlDebugLog.InformationLoggingEnabled)
			{
				HashAlgorithm hashAlgorithm = CryptoHelpers.CreateFromName<HashAlgorithm>(reference.DigestMethod);
				string arg = (hashAlgorithm == null) ? "null" : hashAlgorithm.GetType().Name;
				string data = string.Format(CultureInfo.InvariantCulture, "Signed xml recursion limit hit while trying to decrypt the key. Reference {0} hashed with \"{1}\" and ({2}).", SignedXmlDebugLog.GetObjectId(reference), reference.DigestMethod, arg);
				SignedXmlDebugLog.WriteLine(signedXml, TraceEventType.Information, SignedXmlDebugLog.SignedXmlDebugEvent.VerifySignedInfo, data);
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000C9F1 File Offset: 0x0000ABF1
		private static void WriteLine(object source, TraceEventType eventType, SignedXmlDebugLog.SignedXmlDebugEvent eventId, string data)
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000C9F3 File Offset: 0x0000ABF3
		// Note: this type is marked as 'beforefieldinit'.
		static SignedXmlDebugLog()
		{
		}

		// Token: 0x0400020A RID: 522
		private const string NullString = "(null)";

		// Token: 0x0400020B RID: 523
		private static TraceSource s_traceSource = new TraceSource("System.Security.Cryptography.Xml.SignedXml");

		// Token: 0x0400020C RID: 524
		private static volatile bool s_haveVerboseLogging;

		// Token: 0x0400020D RID: 525
		private static volatile bool s_verboseLogging;

		// Token: 0x0400020E RID: 526
		private static volatile bool s_haveInformationLogging;

		// Token: 0x0400020F RID: 527
		private static volatile bool s_informationLogging;

		// Token: 0x02000059 RID: 89
		internal enum SignedXmlDebugEvent
		{
			// Token: 0x04000211 RID: 529
			BeginCanonicalization,
			// Token: 0x04000212 RID: 530
			BeginCheckSignatureFormat,
			// Token: 0x04000213 RID: 531
			BeginCheckSignedInfo,
			// Token: 0x04000214 RID: 532
			BeginSignatureComputation,
			// Token: 0x04000215 RID: 533
			BeginSignatureVerification,
			// Token: 0x04000216 RID: 534
			CanonicalizedData,
			// Token: 0x04000217 RID: 535
			FormatValidationResult,
			// Token: 0x04000218 RID: 536
			NamespacePropagation,
			// Token: 0x04000219 RID: 537
			ReferenceData,
			// Token: 0x0400021A RID: 538
			SignatureVerificationResult,
			// Token: 0x0400021B RID: 539
			Signing,
			// Token: 0x0400021C RID: 540
			SigningReference,
			// Token: 0x0400021D RID: 541
			VerificationFailure,
			// Token: 0x0400021E RID: 542
			VerifyReference,
			// Token: 0x0400021F RID: 543
			VerifySignedInfo,
			// Token: 0x04000220 RID: 544
			X509Verification,
			// Token: 0x04000221 RID: 545
			UnsafeCanonicalizationMethod,
			// Token: 0x04000222 RID: 546
			UnsafeTransformMethod
		}
	}
}

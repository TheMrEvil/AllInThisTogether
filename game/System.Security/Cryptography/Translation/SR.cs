using System;

namespace System.Security.Cryptography.Translation
{
	// Token: 0x02000016 RID: 22
	internal class SR
	{
		// Token: 0x0600005B RID: 91 RVA: 0x00002145 File Offset: 0x00000345
		public SR()
		{
		}

		// Token: 0x04000098 RID: 152
		public const string Cryptography_DataProtector_InvalidAppNameOrPurpose = "Invalid application name and/or purpose";

		// Token: 0x04000099 RID: 153
		public const string Cryptography_DataProtector_InvalidPurpose = "Invalid data protection purpose";

		// Token: 0x0400009A RID: 154
		public const string ArgumentOutOfRange_Index = "Index was out of range.  Must be non-negative and less than the size of the collection.";

		// Token: 0x0400009B RID: 155
		public const string Arg_EmptyOrNullString = "String cannot be empty or null.";

		// Token: 0x0400009C RID: 156
		public const string Cryptography_Partial_Chain = "A certificate chain could not be built to a trusted root authority.";

		// Token: 0x0400009D RID: 157
		public const string Cryptography_Xml_BadWrappedKeySize = "Bad wrapped key size.";

		// Token: 0x0400009E RID: 158
		public const string Cryptography_Xml_CipherValueElementRequired = "A Cipher Data element should have either a CipherValue or a CipherReference element.";

		// Token: 0x0400009F RID: 159
		public const string Cryptography_Xml_CreateHashAlgorithmFailed = "Could not create hash algorithm object.";

		// Token: 0x040000A0 RID: 160
		public const string Cryptography_Xml_CreateTransformFailed = "Could not create the XML transformation identified by the URI {0}.";

		// Token: 0x040000A1 RID: 161
		public const string Cryptography_Xml_CreatedKeyFailed = "Failed to create signing key.";

		// Token: 0x040000A2 RID: 162
		public const string Cryptography_Xml_DigestMethodRequired = "A DigestMethod must be specified on a Reference prior to generating XML.";

		// Token: 0x040000A3 RID: 163
		public const string Cryptography_Xml_DigestValueRequired = "A Reference must contain a DigestValue.";

		// Token: 0x040000A4 RID: 164
		public const string Cryptography_Xml_EnvelopedSignatureRequiresContext = "An XmlDocument context is required for enveloped transforms.";

		// Token: 0x040000A5 RID: 165
		public const string Cryptography_Xml_InvalidElement = "Malformed element {0}.";

		// Token: 0x040000A6 RID: 166
		public const string Cryptography_Xml_InvalidEncryptionProperty = "Malformed encryption property element.";

		// Token: 0x040000A7 RID: 167
		public const string Cryptography_Xml_InvalidKeySize = "The key size should be a non negative integer.";

		// Token: 0x040000A8 RID: 168
		public const string Cryptography_Xml_InvalidReference = "Malformed reference element.";

		// Token: 0x040000A9 RID: 169
		public const string Cryptography_Xml_InvalidSignatureLength = "The length of the signature with a MAC should be less than the hash output length.";

		// Token: 0x040000AA RID: 170
		public const string Cryptography_Xml_InvalidSignatureLength2 = "The length in bits of the signature with a MAC should be a multiple of 8.";

		// Token: 0x040000AB RID: 171
		public const string Cryptography_Xml_InvalidX509IssuerSerialNumber = "X509 issuer serial number is invalid.";

		// Token: 0x040000AC RID: 172
		public const string Cryptography_Xml_KeyInfoRequired = "A KeyInfo element is required to check the signature.";

		// Token: 0x040000AD RID: 173
		public const string Cryptography_Xml_KW_BadKeySize = "The length of the encrypted data in Key Wrap is either 32, 40 or 48 bytes.";

		// Token: 0x040000AE RID: 174
		public const string Cryptography_Xml_LoadKeyFailed = "Signing key is not loaded.";

		// Token: 0x040000AF RID: 175
		public const string Cryptography_Xml_MissingAlgorithm = "Symmetric algorithm is not specified.";

		// Token: 0x040000B0 RID: 176
		public const string Cryptography_Xml_MissingCipherData = "Cipher data is not specified.";

		// Token: 0x040000B1 RID: 177
		public const string Cryptography_Xml_MissingDecryptionKey = "Unable to retrieve the decryption key.";

		// Token: 0x040000B2 RID: 178
		public const string Cryptography_Xml_MissingEncryptionKey = "Unable to retrieve the encryption key.";

		// Token: 0x040000B3 RID: 179
		public const string Cryptography_Xml_NotSupportedCryptographicTransform = "The specified cryptographic transform is not supported.";

		// Token: 0x040000B4 RID: 180
		public const string Cryptography_Xml_ReferenceElementRequired = "At least one Reference element is required.";

		// Token: 0x040000B5 RID: 181
		public const string Cryptography_Xml_ReferenceTypeRequired = "The Reference type must be set in an EncryptedReference object.";

		// Token: 0x040000B6 RID: 182
		public const string Cryptography_Xml_SelfReferenceRequiresContext = "An XmlDocument context is required to resolve the Reference Uri {0}.";

		// Token: 0x040000B7 RID: 183
		public const string Cryptography_Xml_SignatureDescriptionNotCreated = "SignatureDescription could not be created for the signature algorithm supplied.";

		// Token: 0x040000B8 RID: 184
		public const string Cryptography_Xml_SignatureMethodKeyMismatch = "The key does not fit the SignatureMethod.";

		// Token: 0x040000B9 RID: 185
		public const string Cryptography_Xml_SignatureMethodRequired = "A signature method is required.";

		// Token: 0x040000BA RID: 186
		public const string Cryptography_Xml_SignatureValueRequired = "Signature requires a SignatureValue.";

		// Token: 0x040000BB RID: 187
		public const string Cryptography_Xml_SignedInfoRequired = "Signature requires a SignedInfo.";

		// Token: 0x040000BC RID: 188
		public const string Cryptography_Xml_TransformIncorrectInputType = "The input type was invalid for this transform.";

		// Token: 0x040000BD RID: 189
		public const string Cryptography_Xml_IncorrectObjectType = "Type of input object is invalid.";

		// Token: 0x040000BE RID: 190
		public const string Cryptography_Xml_UnknownTransform = "Unknown transform has been encountered.";

		// Token: 0x040000BF RID: 191
		public const string Cryptography_Xml_UriNotResolved = "Unable to resolve Uri {0}.";

		// Token: 0x040000C0 RID: 192
		public const string Cryptography_Xml_UriNotSupported = " The specified Uri is not supported.";

		// Token: 0x040000C1 RID: 193
		public const string Cryptography_Xml_UriRequired = "A Uri attribute is required for a CipherReference element.";

		// Token: 0x040000C2 RID: 194
		public const string Cryptography_Xml_XrmlMissingContext = "Null Context property encountered.";

		// Token: 0x040000C3 RID: 195
		public const string Cryptography_Xml_XrmlMissingIRelDecryptor = "IRelDecryptor is required.";

		// Token: 0x040000C4 RID: 196
		public const string Cryptography_Xml_XrmlMissingIssuer = "Issuer node is required.";

		// Token: 0x040000C5 RID: 197
		public const string Cryptography_Xml_XrmlMissingLicence = "License node is required.";

		// Token: 0x040000C6 RID: 198
		public const string Cryptography_Xml_XrmlUnableToDecryptGrant = "Unable to decrypt grant content.";

		// Token: 0x040000C7 RID: 199
		public const string NotSupported_KeyAlgorithm = "The certificate key algorithm is not supported.";

		// Token: 0x040000C8 RID: 200
		public const string Log_ActualHashValue = "Actual hash value: {0}";

		// Token: 0x040000C9 RID: 201
		public const string Log_BeginCanonicalization = "Beginning canonicalization using \"{0}\" ({1}).";

		// Token: 0x040000CA RID: 202
		public const string Log_BeginSignatureComputation = "Beginning signature computation.";

		// Token: 0x040000CB RID: 203
		public const string Log_BeginSignatureVerification = "Beginning signature verification.";

		// Token: 0x040000CC RID: 204
		public const string Log_BuildX509Chain = "Building and verifying the X509 chain for certificate {0}.";

		// Token: 0x040000CD RID: 205
		public const string Log_CanonicalizationSettings = "Canonicalization transform is using resolver {0} and base URI \"{1}\".";

		// Token: 0x040000CE RID: 206
		public const string Log_CanonicalizedOutput = "Output of canonicalization transform: {0}";

		// Token: 0x040000CF RID: 207
		public const string Log_CertificateChain = "Certificate chain:";

		// Token: 0x040000D0 RID: 208
		public const string Log_CheckSignatureFormat = "Checking signature format using format validator \"[{0}] {1}.{2}\".";

		// Token: 0x040000D1 RID: 209
		public const string Log_CheckSignedInfo = "Checking signature on SignedInfo with id \"{0}\".";

		// Token: 0x040000D2 RID: 210
		public const string Log_FormatValidationSuccessful = "Signature format validation was successful.";

		// Token: 0x040000D3 RID: 211
		public const string Log_FormatValidationNotSuccessful = "Signature format validation failed.";

		// Token: 0x040000D4 RID: 212
		public const string Log_KeyUsages = "Found key usages \"{0}\" in extension {1} on certificate {2}.";

		// Token: 0x040000D5 RID: 213
		public const string Log_NoNamespacesPropagated = "No namespaces are being propagated.";

		// Token: 0x040000D6 RID: 214
		public const string Log_PropagatingNamespace = "Propagating namespace {0}=\"{1}\".";

		// Token: 0x040000D7 RID: 215
		public const string Log_RawSignatureValue = "Raw signature: {0}";

		// Token: 0x040000D8 RID: 216
		public const string Log_ReferenceHash = "Reference {0} hashed with \"{1}\" ({2}) has hash value {3}, expected hash value {4}.";

		// Token: 0x040000D9 RID: 217
		public const string Log_RevocationMode = "Revocation mode for chain building: {0}.";

		// Token: 0x040000DA RID: 218
		public const string Log_RevocationFlag = "Revocation flag for chain building: {0}.";

		// Token: 0x040000DB RID: 219
		public const string Log_SigningAsymmetric = "Calculating signature with key {0} using signature description {1}, hash algorithm {2}, and asymmetric signature formatter {3}.";

		// Token: 0x040000DC RID: 220
		public const string Log_SigningHmac = "Calculating signature using keyed hash algorithm {0}.";

		// Token: 0x040000DD RID: 221
		public const string Log_SigningReference = "Hashing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\" with hash algorithm \"{4}\" ({5}).";

		// Token: 0x040000DE RID: 222
		public const string Log_TransformedReferenceContents = "Transformed reference contents: {0}";

		// Token: 0x040000DF RID: 223
		public const string Log_UnsafeCanonicalizationMethod = "Canonicalization method \"{0}\" is not on the safe list. Safe canonicalization methods are: {1}.";

		// Token: 0x040000E0 RID: 224
		public const string Log_UrlTimeout = "URL retrieval timeout for chain building: {0}.";

		// Token: 0x040000E1 RID: 225
		public const string Log_VerificationFailed = "Verification failed checking {0}.";

		// Token: 0x040000E2 RID: 226
		public const string Log_VerificationFailed_References = "references";

		// Token: 0x040000E3 RID: 227
		public const string Log_VerificationFailed_SignedInfo = "SignedInfo";

		// Token: 0x040000E4 RID: 228
		public const string Log_VerificationFailed_X509Chain = "X509 chain verification";

		// Token: 0x040000E5 RID: 229
		public const string Log_VerificationFailed_X509KeyUsage = "X509 key usage verification";

		// Token: 0x040000E6 RID: 230
		public const string Log_VerificationFlag = "Verification flags for chain building: {0}.";

		// Token: 0x040000E7 RID: 231
		public const string Log_VerificationTime = "Verification time for chain building: {0}.";

		// Token: 0x040000E8 RID: 232
		public const string Log_VerificationWithKeySuccessful = "Verification with key {0} was successful.";

		// Token: 0x040000E9 RID: 233
		public const string Log_VerificationWithKeyNotSuccessful = "Verification with key {0} was not successful.";

		// Token: 0x040000EA RID: 234
		public const string Log_VerifyReference = "Processing reference {0}, Uri \"{1}\", Id \"{2}\", Type \"{3}\".";

		// Token: 0x040000EB RID: 235
		public const string Log_VerifySignedInfoAsymmetric = "Verifying SignedInfo using key {0}, signature description {1}, hash algorithm {2}, and asymmetric signature deformatter {3}.";

		// Token: 0x040000EC RID: 236
		public const string Log_VerifySignedInfoHmac = "Verifying SignedInfo using keyed hash algorithm {0}.";

		// Token: 0x040000ED RID: 237
		public const string Log_X509ChainError = "Error building X509 chain: {0}: {1}.";

		// Token: 0x040000EE RID: 238
		public const string Log_XmlContext = "Using context: {0}";

		// Token: 0x040000EF RID: 239
		public const string Log_SignedXmlRecursionLimit = "Signed xml recursion limit hit while trying to decrypt the key. Reference {0} hashed with \"{1}\" and ({2}).";

		// Token: 0x040000F0 RID: 240
		public const string Log_UnsafeTransformMethod = "Transform method \"{0}\" is not on the safe list. Safe transform methods are: {1}.";

		// Token: 0x040000F1 RID: 241
		public const string Arg_RankMultiDimNotSupported = "Only single dimensional arrays are supported for the requested action.";

		// Token: 0x040000F2 RID: 242
		public const string Argument_InvalidOffLen = "Offset and length were out of bounds for the array or count is greater than the number of elements from index to the end of the source collection.";

		// Token: 0x040000F3 RID: 243
		public const string Argument_InvalidOidValue = "The OID value was invalid.";

		// Token: 0x040000F4 RID: 244
		public const string Cryptography_Asn_EnumeratedValueRequiresNonFlagsEnum = "ASN.1 Enumerated values only apply to enum types without the [Flags] attribute.";

		// Token: 0x040000F5 RID: 245
		public const string Cryptography_Asn_NamedBitListRequiresFlagsEnum = "Named bit list operations require an enum with the [Flags] attribute.";

		// Token: 0x040000F6 RID: 246
		public const string Cryptography_Asn_NamedBitListValueTooBig = "The encoded named bit list value is larger than the value size of the '{0}' enum.";

		// Token: 0x040000F7 RID: 247
		public const string Cryptography_Asn_UniversalValueIsFixed = "Tags with TagClass Universal must have the appropriate TagValue value for the data type being read or written.";

		// Token: 0x040000F8 RID: 248
		public const string Cryptography_Asn_UnusedBitCountRange = "Unused bit count must be between 0 and 7, inclusive.";

		// Token: 0x040000F9 RID: 249
		public const string Cryptography_AsnSerializer_AmbiguousFieldType = "Field '{0}' of type '{1}' has ambiguous type '{2}', an attribute derived from AsnTypeAttribute is required.";

		// Token: 0x040000FA RID: 250
		public const string Cryptography_AsnSerializer_Choice_AllowNullNonNullable = "[Choice].AllowNull=true is not valid because type '{0}' cannot have a null value.";

		// Token: 0x040000FB RID: 251
		public const string Cryptography_AsnSerializer_Choice_ConflictingTagMapping = "The tag ({0} {1}) for field '{2}' on type '{3}' already is associated in this context with field '{4}' on type '{5}'.";

		// Token: 0x040000FC RID: 252
		public const string Cryptography_AsnSerializer_Choice_DefaultValueDisallowed = "Field '{0}' on [Choice] type '{1}' has a default value, which is not permitted.";

		// Token: 0x040000FD RID: 253
		public const string Cryptography_AsnSerializer_Choice_NoChoiceWasMade = "An instance of [Choice] type '{0}' has no non-null fields.";

		// Token: 0x040000FE RID: 254
		public const string Cryptography_AsnSerializer_Choice_NonNullableField = "Field '{0}' on [Choice] type '{1}' can not be assigned a null value.";

		// Token: 0x040000FF RID: 255
		public const string Cryptography_AsnSerializer_Choice_TooManyValues = "Fields '{0}' and '{1}' on type '{2}' are both non-null when only one value is permitted.";

		// Token: 0x04000100 RID: 256
		public const string Cryptography_AsnSerializer_Choice_TypeCycle = "Field '{0}' on [Choice] type '{1}' has introduced a type chain cycle.";

		// Token: 0x04000101 RID: 257
		public const string Cryptography_AsnSerializer_MultipleAsnTypeAttributes = "Field '{0}' on type '{1}' has multiple attributes deriving from '{2}' when at most one is permitted.";

		// Token: 0x04000102 RID: 258
		public const string Cryptography_AsnSerializer_NoJaggedArrays = "Type '{0}' cannot be serialized or deserialized because it is an array of arrays.";

		// Token: 0x04000103 RID: 259
		public const string Cryptography_AsnSerializer_NoMultiDimensionalArrays = "Type '{0}' cannot be serialized or deserialized because it is a multi-dimensional array.";

		// Token: 0x04000104 RID: 260
		public const string Cryptography_AsnSerializer_NoOpenTypes = "Type '{0}' cannot be serialized or deserialized because it is not sealed or has unbound generic parameters.";

		// Token: 0x04000105 RID: 261
		public const string Cryptography_AsnSerializer_Optional_NonNullableField = "Field '{0}' on type '{1}' is declared [OptionalValue], but it can not be assigned a null value.";

		// Token: 0x04000106 RID: 262
		public const string Cryptography_AsnSerializer_PopulateFriendlyNameOnString = "Field '{0}' on type '{1}' has [ObjectIdentifier].PopulateFriendlyName set to true, which is not applicable to a string.  Change the field to '{2}' or set PopulateFriendlyName to false.";

		// Token: 0x04000107 RID: 263
		public const string Cryptography_AsnSerializer_SetValueException = "Unable to set field {0} on type {1}.";

		// Token: 0x04000108 RID: 264
		public const string Cryptography_AsnSerializer_SpecificTagChoice = "Field '{0}' on type '{1}' has specified an implicit tag value via [ExpectedTag] for [Choice] type '{2}'. ExplicitTag must be true, or the [ExpectedTag] attribute removed.";

		// Token: 0x04000109 RID: 265
		public const string Cryptography_AsnSerializer_UnexpectedTypeForAttribute = "Field '{0}' of type '{1}' has an effective type of '{2}' when one of ({3}) was expected.";

		// Token: 0x0400010A RID: 266
		public const string Cryptography_AsnSerializer_UtcTimeTwoDigitYearMaxTooSmall = "Field '{0}' on type '{1}' has a [UtcTime] TwoDigitYearMax value ({2}) smaller than the minimum (99).";

		// Token: 0x0400010B RID: 267
		public const string Cryptography_AsnSerializer_UnhandledType = "Could not determine how to serialize or deserialize type '{0}'.";

		// Token: 0x0400010C RID: 268
		public const string Cryptography_AsnWriter_EncodeUnbalancedStack = "Encode cannot be called while a Sequence or SetOf is still open.";

		// Token: 0x0400010D RID: 269
		public const string Cryptography_AsnWriter_PopWrongTag = "Cannot pop the requested tag as it is not currently in progress.";

		// Token: 0x0400010E RID: 270
		public const string Cryptography_BadHashValue = "The hash value is not correct.";

		// Token: 0x0400010F RID: 271
		public const string Cryptography_BadSignature = "Invalid signature.";

		// Token: 0x04000110 RID: 272
		public const string Cryptography_Cms_CannotDetermineSignatureAlgorithm = "Could not determine signature algorithm for the signer certificate.";

		// Token: 0x04000111 RID: 273
		public const string Cryptography_Cms_IncompleteCertChain = "The certificate chain is incomplete, the self-signed root authority could not be determined.";

		// Token: 0x04000112 RID: 274
		public const string Cryptography_Cms_Invalid_Originator_Identifier_Choice = "Invalid originator identifier choice {0} found in decoded CMS.";

		// Token: 0x04000113 RID: 275
		public const string Cryptography_Cms_Invalid_Subject_Identifier_Type = "The subject identifier type {0} is not valid.";

		// Token: 0x04000114 RID: 276
		public const string Cryptography_Cms_InvalidMessageType = "Invalid cryptographic message type.";

		// Token: 0x04000115 RID: 277
		public const string Cryptography_Cms_InvalidSignerHashForSignatureAlg = "SignerInfo digest algorithm '{0}' is not valid for signature algorithm '{1}'.";

		// Token: 0x04000116 RID: 278
		public const string Cryptography_Cms_Key_Agree_Date_Not_Available = "The Date property is not available for none KID key agree recipient.";

		// Token: 0x04000117 RID: 279
		public const string Cryptography_Cms_MessageNotEncrypted = "The CMS message is not encrypted.";

		// Token: 0x04000118 RID: 280
		public const string Cryptography_Cms_MessageNotSigned = "The CMS message is not signed.";

		// Token: 0x04000119 RID: 281
		public const string Cryptography_Cms_MissingAuthenticatedAttribute = "The cryptographic message does not contain an expected authenticated attribute.";

		// Token: 0x0400011A RID: 282
		public const string Cryptography_Cms_NoCounterCounterSigner = "Only one level of counter-signatures are supported on this platform.";

		// Token: 0x0400011B RID: 283
		public const string Cryptography_Cms_NoRecipients = "The recipients collection is empty. You must specify at least one recipient. This platform does not implement the certificate picker UI.";

		// Token: 0x0400011C RID: 284
		public const string Cryptography_Cms_NoSignerCert = "No signer certificate was provided. This platform does not implement the certificate picker UI.";

		// Token: 0x0400011D RID: 285
		public const string Cryptography_Cms_NoSignerAtIndex = "The signed cryptographic message does not have a signer for the specified signer index.";

		// Token: 0x0400011E RID: 286
		public const string Cryptography_Cms_RecipientNotFound = "The enveloped-data message does not contain the specified recipient.";

		// Token: 0x0400011F RID: 287
		public const string Cryptography_Cms_RecipientType_NotSupported = "The recipient type '{0}' is not supported for encryption or decryption on this platform.";

		// Token: 0x04000120 RID: 288
		public const string Cryptography_Cms_Sign_Empty_Content = "Cannot create CMS signature for empty content.";

		// Token: 0x04000121 RID: 289
		public const string Cryptography_Cms_SignerNotFound = "Cannot find the original signer.";

		// Token: 0x04000122 RID: 290
		public const string Cryptography_Cms_Signing_RequiresPrivateKey = "A certificate with a private key is required.";

		// Token: 0x04000123 RID: 291
		public const string Cryptography_Cms_TrustFailure = "Certificate trust could not be established. The first reported error is: {0}";

		// Token: 0x04000124 RID: 292
		public const string Cryptography_Cms_UnknownAlgorithm = "Unknown algorithm '{0}'.";

		// Token: 0x04000125 RID: 293
		public const string Cryptography_Cms_UnknownKeySpec = "Unable to determine the type of key handle from this keyspec {0}.";

		// Token: 0x04000126 RID: 294
		public const string Cryptography_Cms_WrongKeyUsage = "The certificate is not valid for the requested usage.";

		// Token: 0x04000127 RID: 295
		public const string Cryptography_Pkcs_InvalidSignatureParameters = "Invalid signature paramters.";

		// Token: 0x04000128 RID: 296
		public const string Cryptography_Pkcs9_AttributeMismatch = "The parameter should be a PKCS 9 attribute.";

		// Token: 0x04000129 RID: 297
		public const string Cryptography_Pkcs9_MultipleSigningTimeNotAllowed = "Cannot add multiple PKCS 9 signing time attributes.";

		// Token: 0x0400012A RID: 298
		public const string Cryptography_Pkcs_PssParametersMissing = "PSS parameters were not present.";

		// Token: 0x0400012B RID: 299
		public const string Cryptography_Pkcs_PssParametersHashMismatch = "This platform requires that the PSS hash algorithm ({0}) match the data digest algorithm ({1}).";

		// Token: 0x0400012C RID: 300
		public const string Cryptography_Pkcs_PssParametersMgfHashMismatch = "This platform does not support the MGF hash algorithm ({0}) being different from the signature hash algorithm ({1}).";

		// Token: 0x0400012D RID: 301
		public const string Cryptography_Pkcs_PssParametersMgfNotSupported = "Mask generation function '{0}' is not supported by this platform.";

		// Token: 0x0400012E RID: 302
		public const string Cryptography_Pkcs_PssParametersSaltMismatch = "PSS salt size {0} is not supported by this platform with hash algorithm {1}.";

		// Token: 0x0400012F RID: 303
		public const string Cryptography_TimestampReq_BadNonce = "The response from the timestamping server did not match the request nonce.";

		// Token: 0x04000130 RID: 304
		public const string Cryptography_TimestampReq_BadResponse = "The response from the timestamping server was not understood.";

		// Token: 0x04000131 RID: 305
		public const string Cryptography_TimestampReq_Failure = "The timestamping server did not grant the request. The request status is '{0}' with failure info '{1}'.";

		// Token: 0x04000132 RID: 306
		public const string Cryptography_TimestampReq_NoCertFound = "The timestamping request required the TSA certificate in the response, but it was not found.";

		// Token: 0x04000133 RID: 307
		public const string Cryptography_TimestampReq_UnexpectedCertFound = "The timestamping request required the TSA certificate not be included in the response, but certificates were present.";

		// Token: 0x04000134 RID: 308
		public const string InvalidOperation_DuplicateItemNotAllowed = "Duplicate items are not allowed in the collection.";

		// Token: 0x04000135 RID: 309
		public const string InvalidOperation_WrongOidInAsnCollection = "AsnEncodedData element in the collection has wrong Oid value: expected = '{0}', actual = '{1}'.";

		// Token: 0x04000136 RID: 310
		public const string PlatformNotSupported_CryptographyPkcs = "System.Security.Cryptography.Pkcs is only supported on Windows platforms.";

		// Token: 0x04000137 RID: 311
		public const string Cryptography_Der_Invalid_Encoding = "ASN1 corrupted data.";

		// Token: 0x04000138 RID: 312
		public const string Cryptography_Invalid_IA5String = "The string contains a character not in the 7 bit ASCII character set.";

		// Token: 0x04000139 RID: 313
		public const string Cryptography_UnknownHashAlgorithm = "'{0}' is not a known hash algorithm.";

		// Token: 0x0400013A RID: 314
		public const string Cryptography_WriteEncodedValue_OneValueAtATime = "The input to WriteEncodedValue must represent a single encoded value with no trailing data.";
	}
}

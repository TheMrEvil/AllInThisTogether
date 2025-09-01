using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;

namespace System.Net
{
	// Token: 0x0200065F RID: 1631
	internal static class UnsafeNclNativeMethods
	{
		// Token: 0x02000660 RID: 1632
		internal static class HttpApi
		{
			// Token: 0x060033A5 RID: 13221 RVA: 0x000B4054 File Offset: 0x000B2254
			// Note: this type is marked as 'beforefieldinit'.
			static HttpApi()
			{
			}

			// Token: 0x04001E2B RID: 7723
			private const int HttpHeaderRequestMaximum = 41;

			// Token: 0x04001E2C RID: 7724
			private const int HttpHeaderResponseMaximum = 30;

			// Token: 0x04001E2D RID: 7725
			private static string[] m_Strings = new string[]
			{
				"Cache-Control",
				"Connection",
				"Date",
				"Keep-Alive",
				"Pragma",
				"Trailer",
				"Transfer-Encoding",
				"Upgrade",
				"Via",
				"Warning",
				"Allow",
				"Content-Length",
				"Content-Type",
				"Content-Encoding",
				"Content-Language",
				"Content-Location",
				"Content-MD5",
				"Content-Range",
				"Expires",
				"Last-Modified",
				"Accept-Ranges",
				"Age",
				"ETag",
				"Location",
				"Proxy-Authenticate",
				"Retry-After",
				"Server",
				"Set-Cookie",
				"Vary",
				"WWW-Authenticate"
			};

			// Token: 0x02000661 RID: 1633
			internal static class HTTP_REQUEST_HEADER_ID
			{
				// Token: 0x060033A6 RID: 13222 RVA: 0x000B4172 File Offset: 0x000B2372
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.HTTP_REQUEST_HEADER_ID.m_Strings[position];
				}

				// Token: 0x060033A7 RID: 13223 RVA: 0x000B417C File Offset: 0x000B237C
				// Note: this type is marked as 'beforefieldinit'.
				static HTTP_REQUEST_HEADER_ID()
				{
				}

				// Token: 0x04001E2E RID: 7726
				private static string[] m_Strings = new string[]
				{
					"Cache-Control",
					"Connection",
					"Date",
					"Keep-Alive",
					"Pragma",
					"Trailer",
					"Transfer-Encoding",
					"Upgrade",
					"Via",
					"Warning",
					"Allow",
					"Content-Length",
					"Content-Type",
					"Content-Encoding",
					"Content-Language",
					"Content-Location",
					"Content-MD5",
					"Content-Range",
					"Expires",
					"Last-Modified",
					"Accept",
					"Accept-Charset",
					"Accept-Encoding",
					"Accept-Language",
					"Authorization",
					"Cookie",
					"Expect",
					"From",
					"Host",
					"If-Match",
					"If-Modified-Since",
					"If-None-Match",
					"If-Range",
					"If-Unmodified-Since",
					"Max-Forwards",
					"Proxy-Authorization",
					"Referer",
					"Range",
					"Te",
					"Translate",
					"User-Agent"
				};
			}

			// Token: 0x02000662 RID: 1634
			internal static class HTTP_RESPONSE_HEADER_ID
			{
				// Token: 0x060033A8 RID: 13224 RVA: 0x000B4300 File Offset: 0x000B2500
				static HTTP_RESPONSE_HEADER_ID()
				{
					for (int i = 0; i < 30; i++)
					{
						UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable.Add(UnsafeNclNativeMethods.HttpApi.m_Strings[i], i);
					}
				}

				// Token: 0x060033A9 RID: 13225 RVA: 0x000B4340 File Offset: 0x000B2540
				internal static int IndexOfKnownHeader(string HeaderName)
				{
					object obj = UnsafeNclNativeMethods.HttpApi.HTTP_RESPONSE_HEADER_ID.m_Hashtable[HeaderName];
					if (obj != null)
					{
						return (int)obj;
					}
					return -1;
				}

				// Token: 0x060033AA RID: 13226 RVA: 0x000B4364 File Offset: 0x000B2564
				internal static string ToString(int position)
				{
					return UnsafeNclNativeMethods.HttpApi.m_Strings[position];
				}

				// Token: 0x04001E2F RID: 7727
				private static Hashtable m_Hashtable = new Hashtable(30);
			}

			// Token: 0x02000663 RID: 1635
			internal enum Enum
			{
				// Token: 0x04001E31 RID: 7729
				HttpHeaderCacheControl,
				// Token: 0x04001E32 RID: 7730
				HttpHeaderConnection,
				// Token: 0x04001E33 RID: 7731
				HttpHeaderDate,
				// Token: 0x04001E34 RID: 7732
				HttpHeaderKeepAlive,
				// Token: 0x04001E35 RID: 7733
				HttpHeaderPragma,
				// Token: 0x04001E36 RID: 7734
				HttpHeaderTrailer,
				// Token: 0x04001E37 RID: 7735
				HttpHeaderTransferEncoding,
				// Token: 0x04001E38 RID: 7736
				HttpHeaderUpgrade,
				// Token: 0x04001E39 RID: 7737
				HttpHeaderVia,
				// Token: 0x04001E3A RID: 7738
				HttpHeaderWarning,
				// Token: 0x04001E3B RID: 7739
				HttpHeaderAllow,
				// Token: 0x04001E3C RID: 7740
				HttpHeaderContentLength,
				// Token: 0x04001E3D RID: 7741
				HttpHeaderContentType,
				// Token: 0x04001E3E RID: 7742
				HttpHeaderContentEncoding,
				// Token: 0x04001E3F RID: 7743
				HttpHeaderContentLanguage,
				// Token: 0x04001E40 RID: 7744
				HttpHeaderContentLocation,
				// Token: 0x04001E41 RID: 7745
				HttpHeaderContentMd5,
				// Token: 0x04001E42 RID: 7746
				HttpHeaderContentRange,
				// Token: 0x04001E43 RID: 7747
				HttpHeaderExpires,
				// Token: 0x04001E44 RID: 7748
				HttpHeaderLastModified,
				// Token: 0x04001E45 RID: 7749
				HttpHeaderAcceptRanges,
				// Token: 0x04001E46 RID: 7750
				HttpHeaderAge,
				// Token: 0x04001E47 RID: 7751
				HttpHeaderEtag,
				// Token: 0x04001E48 RID: 7752
				HttpHeaderLocation,
				// Token: 0x04001E49 RID: 7753
				HttpHeaderProxyAuthenticate,
				// Token: 0x04001E4A RID: 7754
				HttpHeaderRetryAfter,
				// Token: 0x04001E4B RID: 7755
				HttpHeaderServer,
				// Token: 0x04001E4C RID: 7756
				HttpHeaderSetCookie,
				// Token: 0x04001E4D RID: 7757
				HttpHeaderVary,
				// Token: 0x04001E4E RID: 7758
				HttpHeaderWwwAuthenticate,
				// Token: 0x04001E4F RID: 7759
				HttpHeaderResponseMaximum,
				// Token: 0x04001E50 RID: 7760
				HttpHeaderMaximum = 41
			}
		}

		// Token: 0x02000664 RID: 1636
		internal static class SecureStringHelper
		{
			// Token: 0x060033AB RID: 13227 RVA: 0x000B4370 File Offset: 0x000B2570
			internal static string CreateString(SecureString secureString)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (secureString == null || secureString.Length == 0)
				{
					return string.Empty;
				}
				string result;
				try
				{
					intPtr = Marshal.SecureStringToGlobalAllocUnicode(secureString);
					result = Marshal.PtrToStringUni(intPtr);
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.ZeroFreeGlobalAllocUnicode(intPtr);
					}
				}
				return result;
			}

			// Token: 0x060033AC RID: 13228 RVA: 0x000B43CC File Offset: 0x000B25CC
			internal unsafe static SecureString CreateSecureString(string plainString)
			{
				if (plainString == null || plainString.Length == 0)
				{
					return new SecureString();
				}
				SecureString result;
				fixed (string text = plainString)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					result = new SecureString(ptr, plainString.Length);
				}
				return result;
			}
		}
	}
}

using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Scripting;

namespace UnityEngineInternal
{
	// Token: 0x02000002 RID: 2
	internal static class WebRequestUtils
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		[RequiredByNativeCode]
		internal static string RedirectTo(string baseUri, string redirectUri)
		{
			bool flag = redirectUri[0] == '/';
			Uri uri;
			if (flag)
			{
				uri = new Uri(redirectUri, UriKind.Relative);
			}
			else
			{
				uri = new Uri(redirectUri, UriKind.RelativeOrAbsolute);
			}
			bool isAbsoluteUri = uri.IsAbsoluteUri;
			string absoluteUri;
			if (isAbsoluteUri)
			{
				absoluteUri = uri.AbsoluteUri;
			}
			else
			{
				Uri baseUri2 = new Uri(baseUri, UriKind.Absolute);
				Uri uri2 = new Uri(baseUri2, uri);
				absoluteUri = uri2.AbsoluteUri;
			}
			return absoluteUri;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020B4 File Offset: 0x000002B4
		internal static string MakeInitialUrl(string targetUrl, string localUrl)
		{
			bool flag = string.IsNullOrEmpty(targetUrl);
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				bool prependProtocol = false;
				Uri uri = new Uri(localUrl);
				Uri uri2 = null;
				bool flag2 = targetUrl[0] == '/';
				if (flag2)
				{
					uri2 = new Uri(uri, targetUrl);
					prependProtocol = true;
				}
				bool flag3 = uri2 == null && WebRequestUtils.domainRegex.IsMatch(targetUrl);
				if (flag3)
				{
					targetUrl = uri.Scheme + "://" + targetUrl;
					prependProtocol = true;
				}
				FormatException ex = null;
				try
				{
					bool flag4 = uri2 == null && targetUrl[0] != '.';
					if (flag4)
					{
						uri2 = new Uri(targetUrl);
					}
				}
				catch (FormatException ex2)
				{
					ex = ex2;
				}
				bool flag5 = uri2 == null;
				if (flag5)
				{
					try
					{
						uri2 = new Uri(uri, targetUrl);
						prependProtocol = true;
					}
					catch (FormatException)
					{
						throw ex;
					}
				}
				result = WebRequestUtils.MakeUriString(uri2, targetUrl, prependProtocol);
			}
			return result;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021B8 File Offset: 0x000003B8
		internal static string MakeUriString(Uri targetUri, string targetUrl, bool prependProtocol)
		{
			bool isFile = targetUri.IsFile;
			string result;
			if (isFile)
			{
				bool flag = !targetUri.IsLoopback;
				if (flag)
				{
					result = targetUri.OriginalString;
				}
				else
				{
					string text = targetUri.AbsolutePath;
					bool flag2 = text.Contains("%");
					if (flag2)
					{
						bool flag3 = text.Contains("+");
						if (flag3)
						{
							string originalString = targetUri.OriginalString;
							bool flag4 = !originalString.StartsWith("file:");
							if (flag4)
							{
								return "file:///" + originalString.Replace('\\', '/');
							}
						}
						text = WebRequestUtils.URLDecode(text);
					}
					bool flag5 = text.Length > 0 && text[0] != '/';
					if (flag5)
					{
						text = "/" + text;
					}
					result = "file://" + text;
				}
			}
			else
			{
				string scheme = targetUri.Scheme;
				bool flag6 = !prependProtocol && targetUrl.Length >= scheme.Length + 2 && targetUrl[scheme.Length + 1] != '/';
				if (flag6)
				{
					StringBuilder stringBuilder = new StringBuilder(scheme, targetUrl.Length);
					stringBuilder.Append(':');
					bool flag7 = scheme == "jar";
					if (flag7)
					{
						string text2 = targetUri.AbsolutePath;
						bool flag8 = text2.Contains("%");
						if (flag8)
						{
							text2 = WebRequestUtils.URLDecode(text2);
						}
						bool flag9 = text2.StartsWith("file:/") && text2.Length > 6 && text2[6] != '/';
						if (flag9)
						{
							stringBuilder.Append("file://");
							stringBuilder.Append(text2.Substring(5));
						}
						else
						{
							stringBuilder.Append(text2);
						}
						result = stringBuilder.ToString();
					}
					else
					{
						stringBuilder.Append(targetUri.PathAndQuery);
						stringBuilder.Append(targetUri.Fragment);
						result = stringBuilder.ToString();
					}
				}
				else
				{
					bool flag10 = targetUrl.Contains("%");
					if (flag10)
					{
						result = targetUri.OriginalString;
					}
					else
					{
						result = targetUri.AbsoluteUri;
					}
				}
			}
			return result;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000023DC File Offset: 0x000005DC
		private static string URLDecode(string encoded)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(encoded);
			byte[] bytes2 = WWWTranscoder.URLDecode(bytes);
			return Encoding.UTF8.GetString(bytes2);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000240C File Offset: 0x0000060C
		// Note: this type is marked as 'beforefieldinit'.
		static WebRequestUtils()
		{
		}

		// Token: 0x04000001 RID: 1
		private static Regex domainRegex = new Regex("^\\s*\\w+(?:\\.\\w+)+(\\/.*)?$");
	}
}

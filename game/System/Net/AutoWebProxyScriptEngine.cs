using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace System.Net
{
	// Token: 0x0200065E RID: 1630
	internal class AutoWebProxyScriptEngine
	{
		// Token: 0x06003395 RID: 13205 RVA: 0x0000219B File Offset: 0x0000039B
		public AutoWebProxyScriptEngine(WebProxy proxy, bool useRegistry)
		{
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x06003396 RID: 13206 RVA: 0x000B3C97 File Offset: 0x000B1E97
		// (set) Token: 0x06003397 RID: 13207 RVA: 0x000B3C9F File Offset: 0x000B1E9F
		public Uri AutomaticConfigurationScript
		{
			[CompilerGenerated]
			get
			{
				return this.<AutomaticConfigurationScript>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AutomaticConfigurationScript>k__BackingField = value;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003398 RID: 13208 RVA: 0x000B3CA8 File Offset: 0x000B1EA8
		// (set) Token: 0x06003399 RID: 13209 RVA: 0x000B3CB0 File Offset: 0x000B1EB0
		public bool AutomaticallyDetectSettings
		{
			[CompilerGenerated]
			get
			{
				return this.<AutomaticallyDetectSettings>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<AutomaticallyDetectSettings>k__BackingField = value;
			}
		}

		// Token: 0x0600339A RID: 13210 RVA: 0x000B3CBC File Offset: 0x000B1EBC
		public bool GetProxies(Uri destination, out IList<string> proxyList)
		{
			int num = 0;
			return this.GetProxies(destination, out proxyList, ref num);
		}

		// Token: 0x0600339B RID: 13211 RVA: 0x000B3CD5 File Offset: 0x000B1ED5
		public bool GetProxies(Uri destination, out IList<string> proxyList, ref int syncStatus)
		{
			proxyList = null;
			return false;
		}

		// Token: 0x0600339C RID: 13212 RVA: 0x00003917 File Offset: 0x00001B17
		public void Close()
		{
		}

		// Token: 0x0600339D RID: 13213 RVA: 0x00003917 File Offset: 0x00001B17
		public void Abort(ref int syncStatus)
		{
		}

		// Token: 0x0600339E RID: 13214 RVA: 0x00003917 File Offset: 0x00001B17
		public void CheckForChanges()
		{
		}

		// Token: 0x0600339F RID: 13215 RVA: 0x000B3CDC File Offset: 0x000B1EDC
		public WebProxyData GetWebProxyData()
		{
			WebProxyData webProxyData;
			if (AutoWebProxyScriptEngine.IsWindows())
			{
				webProxyData = this.InitializeRegistryGlobalProxy();
				if (webProxyData != null)
				{
					return webProxyData;
				}
			}
			webProxyData = this.ReadEnvVariables();
			return webProxyData ?? new WebProxyData();
		}

		// Token: 0x060033A0 RID: 13216 RVA: 0x000B3D10 File Offset: 0x000B1F10
		private WebProxyData ReadEnvVariables()
		{
			string text = Environment.GetEnvironmentVariable("http_proxy") ?? Environment.GetEnvironmentVariable("HTTP_PROXY");
			if (text != null)
			{
				try
				{
					if (!text.StartsWith("http://"))
					{
						text = "http://" + text;
					}
					Uri uri = new Uri(text);
					IPAddress obj;
					if (IPAddress.TryParse(uri.Host, out obj))
					{
						if (IPAddress.Any.Equals(obj))
						{
							uri = new UriBuilder(uri)
							{
								Host = "127.0.0.1"
							}.Uri;
						}
						else if (IPAddress.IPv6Any.Equals(obj))
						{
							uri = new UriBuilder(uri)
							{
								Host = "[::1]"
							}.Uri;
						}
					}
					bool bypassOnLocal = false;
					ArrayList arrayList = new ArrayList();
					string text2 = Environment.GetEnvironmentVariable("no_proxy") ?? Environment.GetEnvironmentVariable("NO_PROXY");
					if (text2 != null)
					{
						foreach (string text3 in text2.Split(new char[]
						{
							','
						}, StringSplitOptions.RemoveEmptyEntries))
						{
							if (text3 != "*.local")
							{
								arrayList.Add(text3);
							}
							else
							{
								bypassOnLocal = true;
							}
						}
					}
					return new WebProxyData
					{
						proxyAddress = uri,
						bypassOnLocal = bypassOnLocal,
						bypassList = AutoWebProxyScriptEngine.CreateBypassList(arrayList)
					};
				}
				catch (UriFormatException)
				{
				}
			}
			return null;
		}

		// Token: 0x060033A1 RID: 13217 RVA: 0x000B3E70 File Offset: 0x000B2070
		private static bool IsWindows()
		{
			return Environment.OSVersion.Platform < PlatformID.Unix;
		}

		// Token: 0x060033A2 RID: 13218 RVA: 0x000B3E80 File Offset: 0x000B2080
		private WebProxyData InitializeRegistryGlobalProxy()
		{
			if ((int)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyEnable", 0) <= 0)
			{
				return null;
			}
			string address = "";
			bool bypassOnLocal = false;
			ArrayList arrayList = new ArrayList();
			string text = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyServer", null);
			if (text == null)
			{
				return null;
			}
			string text2 = (string)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Microsoft\\Windows\\CurrentVersion\\Internet Settings", "ProxyOverride", null);
			if (text.Contains("="))
			{
				foreach (string text3 in text.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					if (text3.StartsWith("http="))
					{
						address = text3.Substring(5);
						break;
					}
				}
			}
			else
			{
				address = text;
			}
			if (text2 != null)
			{
				foreach (string text4 in text2.Split(new char[]
				{
					';'
				}, StringSplitOptions.RemoveEmptyEntries))
				{
					if (text4 != "<local>")
					{
						arrayList.Add(text4);
					}
					else
					{
						bypassOnLocal = true;
					}
				}
			}
			return new WebProxyData
			{
				proxyAddress = AutoWebProxyScriptEngine.ToUri(address),
				bypassOnLocal = bypassOnLocal,
				bypassList = AutoWebProxyScriptEngine.CreateBypassList(arrayList)
			};
		}

		// Token: 0x060033A3 RID: 13219 RVA: 0x000B3FB9 File Offset: 0x000B21B9
		private static Uri ToUri(string address)
		{
			if (address == null)
			{
				return null;
			}
			if (address.IndexOf("://", StringComparison.Ordinal) == -1)
			{
				address = "http://" + address;
			}
			return new Uri(address);
		}

		// Token: 0x060033A4 RID: 13220 RVA: 0x000B3FE4 File Offset: 0x000B21E4
		private static ArrayList CreateBypassList(ArrayList al)
		{
			string[] array = al.ToArray(typeof(string)) as string[];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = "^" + Regex.Escape(array[i]).Replace("\\*", ".*").Replace("\\?", ".") + "$";
			}
			return new ArrayList(array);
		}

		// Token: 0x04001E29 RID: 7721
		[CompilerGenerated]
		private Uri <AutomaticConfigurationScript>k__BackingField;

		// Token: 0x04001E2A RID: 7722
		[CompilerGenerated]
		private bool <AutomaticallyDetectSettings>k__BackingField;
	}
}

using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security;
using System.Text;
using Mono.Security.Cryptography;
using Mono.Xml;

namespace Mono.Security
{
	// Token: 0x02000080 RID: 128
	internal class StrongNameManager
	{
		// Token: 0x06000261 RID: 609 RVA: 0x0000B818 File Offset: 0x00009A18
		public static void LoadConfig(string filename)
		{
			if (File.Exists(filename))
			{
				SecurityParser securityParser = new SecurityParser();
				using (StreamReader streamReader = new StreamReader(filename))
				{
					string xml = streamReader.ReadToEnd();
					securityParser.LoadXml(xml);
				}
				SecurityElement securityElement = securityParser.ToXml();
				if (securityElement != null && securityElement.Tag == "configuration")
				{
					SecurityElement securityElement2 = securityElement.SearchForChildByTag("strongNames");
					if (securityElement2 != null && securityElement2.Children.Count > 0)
					{
						SecurityElement securityElement3 = securityElement2.SearchForChildByTag("pubTokenMapping");
						if (securityElement3 != null && securityElement3.Children.Count > 0)
						{
							StrongNameManager.LoadMapping(securityElement3);
						}
						SecurityElement securityElement4 = securityElement2.SearchForChildByTag("verificationSettings");
						if (securityElement4 != null && securityElement4.Children.Count > 0)
						{
							StrongNameManager.LoadVerificationSettings(securityElement4);
						}
					}
				}
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000B8F8 File Offset: 0x00009AF8
		private static void LoadMapping(SecurityElement mapping)
		{
			if (StrongNameManager.mappings == null)
			{
				StrongNameManager.mappings = new Hashtable();
			}
			object syncRoot = StrongNameManager.mappings.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in mapping.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					if (!(securityElement.Tag != "map"))
					{
						string text = securityElement.Attribute("Token");
						if (text != null && text.Length == 16)
						{
							text = text.ToUpper(CultureInfo.InvariantCulture);
							string text2 = securityElement.Attribute("PublicKey");
							if (text2 != null)
							{
								if (StrongNameManager.mappings[text] == null)
								{
									StrongNameManager.mappings.Add(text, text2);
								}
								else
								{
									StrongNameManager.mappings[text] = text2;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000BA08 File Offset: 0x00009C08
		private static void LoadVerificationSettings(SecurityElement settings)
		{
			if (StrongNameManager.tokens == null)
			{
				StrongNameManager.tokens = new Hashtable();
			}
			object syncRoot = StrongNameManager.tokens.SyncRoot;
			lock (syncRoot)
			{
				foreach (object obj in settings.Children)
				{
					SecurityElement securityElement = (SecurityElement)obj;
					if (!(securityElement.Tag != "skip"))
					{
						string text = securityElement.Attribute("Token");
						if (text != null)
						{
							text = text.ToUpper(CultureInfo.InvariantCulture);
							string text2 = securityElement.Attribute("Assembly");
							if (text2 == null)
							{
								text2 = "*";
							}
							string text3 = securityElement.Attribute("Users");
							if (text3 == null)
							{
								text3 = "*";
							}
							StrongNameManager.Element element = (StrongNameManager.Element)StrongNameManager.tokens[text];
							if (element == null)
							{
								element = new StrongNameManager.Element(text2, text3);
								StrongNameManager.tokens.Add(text, element);
							}
							else if ((string)element.assemblies[text2] == null)
							{
								element.assemblies.Add(text2, text3);
							}
							else if (text3 == "*")
							{
								element.assemblies[text2] = "*";
							}
							else
							{
								string value = (string)element.assemblies[text2] + "," + text3;
								element.assemblies[text2] = value;
							}
						}
					}
				}
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000BBCC File Offset: 0x00009DCC
		public static byte[] GetMappedPublicKey(byte[] token)
		{
			if (StrongNameManager.mappings == null || token == null)
			{
				return null;
			}
			string key = CryptoConvert.ToHex(token);
			string text = (string)StrongNameManager.mappings[key];
			if (text == null)
			{
				return null;
			}
			return CryptoConvert.FromHex(text);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000BC08 File Offset: 0x00009E08
		public static bool MustVerify(AssemblyName an)
		{
			if (an == null || StrongNameManager.tokens == null)
			{
				return true;
			}
			string key = CryptoConvert.ToHex(an.GetPublicKeyToken());
			StrongNameManager.Element element = (StrongNameManager.Element)StrongNameManager.tokens[key];
			if (element != null)
			{
				string users = element.GetUsers(an.Name);
				if (users == null)
				{
					users = element.GetUsers("*");
				}
				if (users != null)
				{
					return !(users == "*") && users.IndexOf(Environment.UserName) < 0;
				}
			}
			return true;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000BC80 File Offset: 0x00009E80
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Public Key Token\tAssemblies\t\tUsers");
			stringBuilder.Append(Environment.NewLine);
			foreach (object obj in StrongNameManager.tokens)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				stringBuilder.Append((string)dictionaryEntry.Key);
				StrongNameManager.Element element = (StrongNameManager.Element)dictionaryEntry.Value;
				bool flag = true;
				foreach (object obj2 in element.assemblies)
				{
					DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
					if (flag)
					{
						stringBuilder.Append("\t");
						flag = false;
					}
					else
					{
						stringBuilder.Append("\t\t\t");
					}
					stringBuilder.Append((string)dictionaryEntry2.Key);
					stringBuilder.Append("\t");
					string text = (string)dictionaryEntry2.Value;
					if (text == "*")
					{
						text = "All users";
					}
					stringBuilder.Append(text);
					stringBuilder.Append(Environment.NewLine);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000259F File Offset: 0x0000079F
		public StrongNameManager()
		{
		}

		// Token: 0x04000EAA RID: 3754
		private static Hashtable mappings;

		// Token: 0x04000EAB RID: 3755
		private static Hashtable tokens;

		// Token: 0x02000081 RID: 129
		private class Element
		{
			// Token: 0x06000268 RID: 616 RVA: 0x0000BDE4 File Offset: 0x00009FE4
			public Element()
			{
				this.assemblies = new Hashtable();
			}

			// Token: 0x06000269 RID: 617 RVA: 0x0000BDF7 File Offset: 0x00009FF7
			public Element(string assembly, string users) : this()
			{
				this.assemblies.Add(assembly, users);
			}

			// Token: 0x0600026A RID: 618 RVA: 0x0000BE0C File Offset: 0x0000A00C
			public string GetUsers(string assembly)
			{
				return (string)this.assemblies[assembly];
			}

			// Token: 0x04000EAC RID: 3756
			internal Hashtable assemblies;
		}
	}
}

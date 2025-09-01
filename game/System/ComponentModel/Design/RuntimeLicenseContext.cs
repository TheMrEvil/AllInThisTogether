using System;
using System.Collections;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace System.ComponentModel.Design
{
	// Token: 0x0200044E RID: 1102
	internal class RuntimeLicenseContext : LicenseContext
	{
		// Token: 0x060023DC RID: 9180 RVA: 0x00081478 File Offset: 0x0007F678
		private string GetLocalPath(string fileName)
		{
			Uri uri = new Uri(fileName);
			return uri.LocalPath + uri.Fragment;
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x000814A0 File Offset: 0x0007F6A0
		public override string GetSavedLicenseKey(Type type, Assembly resourceAssembly)
		{
			if (this.savedLicenseKeys == null || this.savedLicenseKeys[type.AssemblyQualifiedName] == null)
			{
				if (this.savedLicenseKeys == null)
				{
					this.savedLicenseKeys = new Hashtable();
				}
				if (resourceAssembly == null)
				{
					resourceAssembly = Assembly.GetEntryAssembly();
				}
				if (resourceAssembly == null)
				{
					foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
					{
						if (!assembly.IsDynamic)
						{
							string text = this.GetLocalPath(assembly.EscapedCodeBase);
							text = new FileInfo(text).Name;
							Stream stream = assembly.GetManifestResourceStream(text + ".licenses");
							if (stream == null)
							{
								stream = this.CaseInsensitiveManifestResourceStreamLookup(assembly, text + ".licenses");
							}
							if (stream != null)
							{
								DesigntimeLicenseContextSerializer.Deserialize(stream, text.ToUpper(CultureInfo.InvariantCulture), this);
								break;
							}
						}
					}
				}
				else if (!resourceAssembly.IsDynamic)
				{
					string text2 = this.GetLocalPath(resourceAssembly.EscapedCodeBase);
					text2 = Path.GetFileName(text2);
					string text3 = text2 + ".licenses";
					Stream manifestResourceStream = resourceAssembly.GetManifestResourceStream(text3);
					if (manifestResourceStream == null)
					{
						string text4 = null;
						CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
						string name = resourceAssembly.GetName().Name;
						foreach (string text5 in resourceAssembly.GetManifestResourceNames())
						{
							if (compareInfo.Compare(text5, text3, CompareOptions.IgnoreCase) == 0 || compareInfo.Compare(text5, name + ".exe.licenses", CompareOptions.IgnoreCase) == 0 || compareInfo.Compare(text5, name + ".dll.licenses", CompareOptions.IgnoreCase) == 0)
							{
								text4 = text5;
								break;
							}
						}
						if (text4 != null)
						{
							manifestResourceStream = resourceAssembly.GetManifestResourceStream(text4);
						}
					}
					if (manifestResourceStream != null)
					{
						DesigntimeLicenseContextSerializer.Deserialize(manifestResourceStream, text2.ToUpper(CultureInfo.InvariantCulture), this);
					}
				}
			}
			return (string)this.savedLicenseKeys[type.AssemblyQualifiedName];
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x00081680 File Offset: 0x0007F880
		private Stream CaseInsensitiveManifestResourceStreamLookup(Assembly satellite, string name)
		{
			CompareInfo compareInfo = CultureInfo.InvariantCulture.CompareInfo;
			string name2 = satellite.GetName().Name;
			foreach (string text in satellite.GetManifestResourceNames())
			{
				if (compareInfo.Compare(text, name, CompareOptions.IgnoreCase) == 0 || compareInfo.Compare(text, name2 + ".exe.licenses") == 0 || compareInfo.Compare(text, name2 + ".dll.licenses") == 0)
				{
					name = text;
					break;
				}
			}
			return satellite.GetManifestResourceStream(name);
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00081701 File Offset: 0x0007F901
		public RuntimeLicenseContext()
		{
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00081709 File Offset: 0x0007F909
		// Note: this type is marked as 'beforefieldinit'.
		static RuntimeLicenseContext()
		{
		}

		// Token: 0x040010C1 RID: 4289
		private static TraceSwitch s_runtimeLicenseContextSwitch = new TraceSwitch("RuntimeLicenseContextTrace", "RuntimeLicenseContext tracing");

		// Token: 0x040010C2 RID: 4290
		private const int ReadBlock = 400;

		// Token: 0x040010C3 RID: 4291
		internal Hashtable savedLicenseKeys;
	}
}

using System;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a <see langword="location" /> element within a configuration file.</summary>
	// Token: 0x02000022 RID: 34
	public class ConfigurationLocation
	{
		// Token: 0x06000129 RID: 297 RVA: 0x00002050 File Offset: 0x00000250
		internal ConfigurationLocation()
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000057B8 File Offset: 0x000039B8
		internal ConfigurationLocation(string path, string xmlContent, Configuration parent, bool allowOverride)
		{
			if (!string.IsNullOrEmpty(path))
			{
				char c = path[0];
				if (c <= '.')
				{
					if (c != ' ' && c != '.')
					{
						goto IL_3C;
					}
				}
				else if (c != '/' && c != '\\')
				{
					goto IL_3C;
				}
				throw new ConfigurationErrorsException("<location> path attribute must be a relative virtual path.  It cannot start with any of ' ' '.' '/' or '\\'.");
				IL_3C:
				path = path.TrimEnd(ConfigurationLocation.pathTrimChars);
			}
			this.path = path;
			this.xmlContent = xmlContent;
			this.parent = parent;
			this.allowOverride = allowOverride;
		}

		/// <summary>Gets the relative path to the resource whose configuration settings are represented by this <see cref="T:System.Configuration.ConfigurationLocation" /> object.</summary>
		/// <returns>The relative path to the resource whose configuration settings are represented by this <see cref="T:System.Configuration.ConfigurationLocation" />.</returns>
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600012B RID: 299 RVA: 0x0000582B File Offset: 0x00003A2B
		public string Path
		{
			get
			{
				return this.path;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600012C RID: 300 RVA: 0x00005833 File Offset: 0x00003A33
		internal bool AllowOverride
		{
			get
			{
				return this.allowOverride;
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600012D RID: 301 RVA: 0x0000583B File Offset: 0x00003A3B
		internal string XmlContent
		{
			get
			{
				return this.xmlContent;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600012E RID: 302 RVA: 0x00005843 File Offset: 0x00003A43
		internal Configuration OpenedConfiguration
		{
			get
			{
				return this.configuration;
			}
		}

		/// <summary>Creates an instance of a Configuration object.</summary>
		/// <returns>A Configuration object.</returns>
		// Token: 0x0600012F RID: 303 RVA: 0x0000584C File Offset: 0x00003A4C
		public Configuration OpenConfiguration()
		{
			if (this.configuration == null)
			{
				if (!this.parentResolved)
				{
					Configuration parentWithFile = this.parent.GetParentWithFile();
					if (parentWithFile != null)
					{
						string configPathFromLocationSubPath = this.parent.ConfigHost.GetConfigPathFromLocationSubPath(this.parent.LocationConfigPath, this.path);
						this.parent = parentWithFile.FindLocationConfiguration(configPathFromLocationSubPath, this.parent);
					}
				}
				this.configuration = new Configuration(this.parent, this.path);
				using (XmlTextReader xmlTextReader = new ConfigXmlTextReader(new StringReader(this.xmlContent), this.path))
				{
					this.configuration.ReadData(xmlTextReader, this.allowOverride);
				}
				this.xmlContent = null;
			}
			return this.configuration;
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000591C File Offset: 0x00003B1C
		internal void SetParentConfiguration(Configuration parent)
		{
			if (this.parentResolved)
			{
				return;
			}
			this.parentResolved = true;
			this.parent = parent;
			if (this.configuration != null)
			{
				this.configuration.Parent = parent;
			}
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00005949 File Offset: 0x00003B49
		// Note: this type is marked as 'beforefieldinit'.
		static ConfigurationLocation()
		{
		}

		// Token: 0x04000089 RID: 137
		private static readonly char[] pathTrimChars = new char[]
		{
			'/'
		};

		// Token: 0x0400008A RID: 138
		private string path;

		// Token: 0x0400008B RID: 139
		private Configuration configuration;

		// Token: 0x0400008C RID: 140
		private Configuration parent;

		// Token: 0x0400008D RID: 141
		private string xmlContent;

		// Token: 0x0400008E RID: 142
		private bool parentResolved;

		// Token: 0x0400008F RID: 143
		private bool allowOverride;
	}
}

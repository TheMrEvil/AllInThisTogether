using System;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000068 RID: 104
	internal class SectionInfo : ConfigInfo
	{
		// Token: 0x06000372 RID: 882 RVA: 0x00009E4D File Offset: 0x0000804D
		public SectionInfo()
		{
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00009E78 File Offset: 0x00008078
		public SectionInfo(string sectionName, SectionInformation info)
		{
			this.Name = sectionName;
			this.TypeName = info.Type;
			this.allowLocation = info.AllowLocation;
			this.allowDefinition = info.AllowDefinition;
			this.allowExeDefinition = info.AllowExeDefinition;
			this.requirePermission = info.RequirePermission;
			this.restartOnExternalChanges = info.RestartOnExternalChanges;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00009EFC File Offset: 0x000080FC
		public override object CreateInstance()
		{
			object obj = base.CreateInstance();
			ConfigurationSection configurationSection = obj as ConfigurationSection;
			if (configurationSection != null)
			{
				configurationSection.SectionInformation.AllowLocation = this.allowLocation;
				configurationSection.SectionInformation.AllowDefinition = this.allowDefinition;
				configurationSection.SectionInformation.AllowExeDefinition = this.allowExeDefinition;
				configurationSection.SectionInformation.RequirePermission = this.requirePermission;
				configurationSection.SectionInformation.RestartOnExternalChanges = this.restartOnExternalChanges;
				configurationSection.SectionInformation.SetName(this.Name);
			}
			return obj;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00009F7F File Offset: 0x0000817F
		public override bool HasDataContent(Configuration config)
		{
			return config.GetSectionInstance(this, false) != null || config.GetSectionXml(this) != null;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00009F97 File Offset: 0x00008197
		public override bool HasConfigContent(Configuration cfg)
		{
			return base.StreamName == cfg.FileName;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00009FAC File Offset: 0x000081AC
		public override void ReadConfig(Configuration cfg, string streamName, XmlReader reader)
		{
			base.StreamName = streamName;
			this.ConfigHost = cfg.ConfigHost;
			while (reader.MoveToNextAttribute())
			{
				string name = reader.Name;
				uint num = <PrivateImplementationDetails>.ComputeStringHash(name);
				if (num <= 1766272347U)
				{
					if (num != 1066839313U)
					{
						if (num != 1361572173U)
						{
							if (num != 1766272347U)
							{
								goto IL_29E;
							}
							if (!(name == "requirePermission"))
							{
								goto IL_29E;
							}
							string value = reader.Value;
							bool flag = value == "true";
							if (!flag && value != "false")
							{
								base.ThrowException("Invalid attribute value", reader);
							}
							this.requirePermission = flag;
							continue;
						}
						else if (!(name == "type"))
						{
							goto IL_29E;
						}
					}
					else
					{
						if (!(name == "allowLocation"))
						{
							goto IL_29E;
						}
						string value2 = reader.Value;
						this.allowLocation = (value2 == "true");
						if (!this.allowLocation && value2 != "false")
						{
							base.ThrowException("Invalid attribute value", reader);
							continue;
						}
						continue;
					}
				}
				else
				{
					if (num <= 1931054735U)
					{
						if (num != 1841158919U)
						{
							if (num != 1931054735U)
							{
								goto IL_29E;
							}
							if (!(name == "allowExeDefinition"))
							{
								goto IL_29E;
							}
						}
						else
						{
							if (!(name == "restartOnExternalChanges"))
							{
								goto IL_29E;
							}
							string value3 = reader.Value;
							bool flag2 = value3 == "true";
							if (!flag2 && value3 != "false")
							{
								base.ThrowException("Invalid attribute value", reader);
							}
							this.restartOnExternalChanges = flag2;
							continue;
						}
					}
					else if (num != 2369371622U)
					{
						if (num != 3263379011U)
						{
							goto IL_29E;
						}
						if (!(name == "allowDefinition"))
						{
							goto IL_29E;
						}
						string value4 = reader.Value;
						try
						{
							this.allowDefinition = (ConfigurationAllowDefinition)Enum.Parse(typeof(ConfigurationAllowDefinition), value4);
							continue;
						}
						catch
						{
							base.ThrowException("Invalid attribute value", reader);
							continue;
						}
					}
					else
					{
						if (!(name == "name"))
						{
							goto IL_29E;
						}
						this.Name = reader.Value;
						if (this.Name == "location")
						{
							base.ThrowException("location is a reserved section name", reader);
							continue;
						}
						continue;
					}
					string value5 = reader.Value;
					try
					{
						this.allowExeDefinition = (ConfigurationAllowExeDefinition)Enum.Parse(typeof(ConfigurationAllowExeDefinition), value5);
						continue;
					}
					catch
					{
						base.ThrowException("Invalid attribute value", reader);
						continue;
					}
				}
				this.TypeName = reader.Value;
				continue;
				IL_29E:
				base.ThrowException(string.Format("Unrecognized attribute: {0}", reader.Name), reader);
			}
			if (this.Name == null || this.TypeName == null)
			{
				base.ThrowException("Required attribute missing", reader);
			}
			reader.MoveToElement();
			reader.Skip();
		}

		// Token: 0x06000378 RID: 888 RVA: 0x0000A2C0 File Offset: 0x000084C0
		public override void WriteConfig(Configuration cfg, XmlWriter writer, ConfigurationSaveMode mode)
		{
			writer.WriteStartElement("section");
			writer.WriteAttributeString("name", this.Name);
			writer.WriteAttributeString("type", this.TypeName);
			if (!this.allowLocation)
			{
				writer.WriteAttributeString("allowLocation", "false");
			}
			if (this.allowDefinition != ConfigurationAllowDefinition.Everywhere)
			{
				writer.WriteAttributeString("allowDefinition", this.allowDefinition.ToString());
			}
			if (this.allowExeDefinition != ConfigurationAllowExeDefinition.MachineToApplication)
			{
				writer.WriteAttributeString("allowExeDefinition", this.allowExeDefinition.ToString());
			}
			if (!this.requirePermission)
			{
				writer.WriteAttributeString("requirePermission", "false");
			}
			writer.WriteEndElement();
		}

		// Token: 0x06000379 RID: 889 RVA: 0x0000A380 File Offset: 0x00008580
		public override void ReadData(Configuration config, XmlReader reader, bool overrideAllowed)
		{
			if (!config.HasFile && !this.allowLocation)
			{
				throw new ConfigurationErrorsException("The configuration section <" + this.Name + "> cannot be defined inside a <location> element.", reader);
			}
			if (!config.ConfigHost.IsDefinitionAllowed(config.ConfigPath, this.allowDefinition, this.allowExeDefinition))
			{
				object obj = (this.allowExeDefinition != ConfigurationAllowExeDefinition.MachineToApplication) ? this.allowExeDefinition : this.allowDefinition;
				throw new ConfigurationErrorsException(string.Concat(new string[]
				{
					"The section <",
					this.Name,
					"> can't be defined in this configuration file (the allowed definition context is '",
					(obj != null) ? obj.ToString() : null,
					"')."
				}), reader);
			}
			if (config.GetSectionXml(this) != null)
			{
				base.ThrowException("The section <" + this.Name + "> is defined more than once in the same configuration file.", reader);
			}
			config.SetSectionXml(this, reader.ReadOuterXml());
		}

		// Token: 0x0600037A RID: 890 RVA: 0x0000A470 File Offset: 0x00008670
		public override void WriteData(Configuration config, XmlWriter writer, ConfigurationSaveMode mode)
		{
			ConfigurationSection sectionInstance = config.GetSectionInstance(this, false);
			string text;
			if (sectionInstance != null)
			{
				ConfigurationSection parentElement = (config.Parent != null) ? config.Parent.GetSectionInstance(this, false) : null;
				text = sectionInstance.SerializeSection(parentElement, this.Name, mode);
				string externalDataXml = sectionInstance.ExternalDataXml;
				string filePath = config.FilePath;
				if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(externalDataXml))
				{
					using (StreamWriter streamWriter = new StreamWriter(Path.Combine(Path.GetDirectoryName(filePath), sectionInstance.SectionInformation.ConfigSource)))
					{
						streamWriter.Write(externalDataXml);
					}
				}
				if (sectionInstance.SectionInformation.IsProtected)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("<{0} configProtectionProvider=\"{1}\">\n", this.Name, sectionInstance.SectionInformation.ProtectionProvider.Name);
					stringBuilder.Append(config.ConfigHost.EncryptSection(text, sectionInstance.SectionInformation.ProtectionProvider, ProtectedConfiguration.Section));
					stringBuilder.AppendFormat("</{0}>", this.Name);
					text = stringBuilder.ToString();
				}
			}
			else
			{
				text = config.GetSectionXml(this);
			}
			if (!string.IsNullOrEmpty(text))
			{
				writer.WriteRaw(text);
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x000023B9 File Offset: 0x000005B9
		internal override void Merge(ConfigInfo data)
		{
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0000A5A0 File Offset: 0x000087A0
		internal override bool HasValues(Configuration config, ConfigurationSaveMode mode)
		{
			ConfigurationSection sectionInstance = config.GetSectionInstance(this, false);
			if (sectionInstance == null)
			{
				return false;
			}
			ConfigurationSection parent = (config.Parent != null) ? config.Parent.GetSectionInstance(this, false) : null;
			return sectionInstance.HasValues(parent, mode);
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0000A5DC File Offset: 0x000087DC
		internal override void ResetModified(Configuration config)
		{
			ConfigurationSection sectionInstance = config.GetSectionInstance(this, false);
			if (sectionInstance != null)
			{
				sectionInstance.ResetModified();
			}
		}

		// Token: 0x04000138 RID: 312
		private bool allowLocation = true;

		// Token: 0x04000139 RID: 313
		private bool requirePermission = true;

		// Token: 0x0400013A RID: 314
		private bool restartOnExternalChanges;

		// Token: 0x0400013B RID: 315
		private ConfigurationAllowDefinition allowDefinition = ConfigurationAllowDefinition.Everywhere;

		// Token: 0x0400013C RID: 316
		private ConfigurationAllowExeDefinition allowExeDefinition = ConfigurationAllowExeDefinition.MachineToApplication;
	}
}

using System;
using System.Configuration.Internal;
using System.Text;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x02000012 RID: 18
	internal abstract class ConfigInfo
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000263E File Offset: 0x0000083E
		public virtual object CreateInstance()
		{
			if (this.Type == null)
			{
				this.Type = this.ConfigHost.GetConfigType(this.TypeName, true);
			}
			return Activator.CreateInstance(this.Type, true);
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002674 File Offset: 0x00000874
		public string XPath
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder(this.Name);
				for (ConfigInfo parent = this.Parent; parent != null; parent = parent.Parent)
				{
					stringBuilder.Insert(0, parent.Name + "/");
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000026BE File Offset: 0x000008BE
		// (set) Token: 0x06000041 RID: 65 RVA: 0x000026C6 File Offset: 0x000008C6
		public string StreamName
		{
			get
			{
				return this.streamName;
			}
			set
			{
				this.streamName = value;
			}
		}

		// Token: 0x06000042 RID: 66
		public abstract bool HasConfigContent(Configuration cfg);

		// Token: 0x06000043 RID: 67
		public abstract bool HasDataContent(Configuration cfg);

		// Token: 0x06000044 RID: 68 RVA: 0x000026CF File Offset: 0x000008CF
		protected void ThrowException(string text, XmlReader reader)
		{
			throw new ConfigurationErrorsException(text, reader);
		}

		// Token: 0x06000045 RID: 69
		public abstract void ReadConfig(Configuration cfg, string streamName, XmlReader reader);

		// Token: 0x06000046 RID: 70
		public abstract void WriteConfig(Configuration cfg, XmlWriter writer, ConfigurationSaveMode mode);

		// Token: 0x06000047 RID: 71
		public abstract void ReadData(Configuration config, XmlReader reader, bool overrideAllowed);

		// Token: 0x06000048 RID: 72
		public abstract void WriteData(Configuration config, XmlWriter writer, ConfigurationSaveMode mode);

		// Token: 0x06000049 RID: 73
		internal abstract void Merge(ConfigInfo data);

		// Token: 0x0600004A RID: 74
		internal abstract bool HasValues(Configuration config, ConfigurationSaveMode mode);

		// Token: 0x0600004B RID: 75
		internal abstract void ResetModified(Configuration config);

		// Token: 0x0600004C RID: 76 RVA: 0x00002050 File Offset: 0x00000250
		protected ConfigInfo()
		{
		}

		// Token: 0x04000039 RID: 57
		public string Name;

		// Token: 0x0400003A RID: 58
		public string TypeName;

		// Token: 0x0400003B RID: 59
		protected Type Type;

		// Token: 0x0400003C RID: 60
		private string streamName;

		// Token: 0x0400003D RID: 61
		public ConfigInfo Parent;

		// Token: 0x0400003E RID: 62
		public IInternalConfigHost ConfigHost;
	}
}

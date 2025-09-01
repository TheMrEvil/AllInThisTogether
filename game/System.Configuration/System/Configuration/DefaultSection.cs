using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a basic configuration-section handler that exposes the configuration section's XML for both read and write access.</summary>
	// Token: 0x0200003C RID: 60
	public sealed class DefaultSection : ConfigurationSection
	{
		// Token: 0x06000217 RID: 535 RVA: 0x000076F1 File Offset: 0x000058F1
		static DefaultSection()
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000076FD File Offset: 0x000058FD
		protected internal override void DeserializeSection(XmlReader xmlReader)
		{
			if (base.RawXml == null)
			{
				base.RawXml = xmlReader.ReadOuterXml();
				return;
			}
			xmlReader.Skip();
		}

		// Token: 0x06000219 RID: 537 RVA: 0x0000771A File Offset: 0x0000591A
		[MonoTODO]
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00007722 File Offset: 0x00005922
		[MonoTODO]
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			base.Reset(parentSection);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000772B File Offset: 0x0000592B
		[MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00007733 File Offset: 0x00005933
		[MonoTODO]
		protected internal override string SerializeSection(ConfigurationElement parentSection, string name, ConfigurationSaveMode saveMode)
		{
			return base.SerializeSection(parentSection, name, saveMode);
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000773E File Offset: 0x0000593E
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return DefaultSection.properties;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.DefaultSection" /> class.</summary>
		// Token: 0x0600021E RID: 542 RVA: 0x00002147 File Offset: 0x00000347
		public DefaultSection()
		{
		}

		// Token: 0x040000DF RID: 223
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}

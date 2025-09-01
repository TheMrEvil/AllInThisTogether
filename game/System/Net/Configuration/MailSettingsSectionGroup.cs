using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> class.</summary>
	// Token: 0x0200076C RID: 1900
	public sealed class MailSettingsSectionGroup : ConfigurationSectionGroup
	{
		/// <summary>Gets the SMTP settings for the local computer.</summary>
		/// <returns>A <see cref="T:System.Net.Configuration.SmtpSection" /> object that contains configuration information for the local computer.</returns>
		// Token: 0x17000D94 RID: 3476
		// (get) Token: 0x06003BEB RID: 15339 RVA: 0x000CD36E File Offset: 0x000CB56E
		public SmtpSection Smtp
		{
			get
			{
				return (SmtpSection)base.Sections["smtp"];
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.MailSettingsSectionGroup" /> class.</summary>
		// Token: 0x06003BEC RID: 15340 RVA: 0x0002EA01 File Offset: 0x0002CC01
		public MailSettingsSectionGroup()
		{
		}
	}
}

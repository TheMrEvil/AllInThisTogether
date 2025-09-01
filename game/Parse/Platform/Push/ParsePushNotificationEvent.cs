using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Parse.Infrastructure.Utilities;

namespace Parse.Platform.Push
{
	// Token: 0x0200002D RID: 45
	public class ParsePushNotificationEvent : EventArgs
	{
		// Token: 0x06000239 RID: 569 RVA: 0x000096A2 File Offset: 0x000078A2
		internal ParsePushNotificationEvent(IDictionary<string, object> content)
		{
			this.Content = content;
			this.TextContent = JsonUtilities.Encode(content);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000096BD File Offset: 0x000078BD
		internal ParsePushNotificationEvent(string stringPayload)
		{
			this.TextContent = stringPayload;
			this.Content = (JsonUtilities.Parse(stringPayload) as IDictionary<string, object>);
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600023B RID: 571 RVA: 0x000096DD File Offset: 0x000078DD
		// (set) Token: 0x0600023C RID: 572 RVA: 0x000096E5 File Offset: 0x000078E5
		public IDictionary<string, object> Content
		{
			[CompilerGenerated]
			get
			{
				return this.<Content>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<Content>k__BackingField = value;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600023D RID: 573 RVA: 0x000096EE File Offset: 0x000078EE
		// (set) Token: 0x0600023E RID: 574 RVA: 0x000096F6 File Offset: 0x000078F6
		public string TextContent
		{
			[CompilerGenerated]
			get
			{
				return this.<TextContent>k__BackingField;
			}
			[CompilerGenerated]
			internal set
			{
				this.<TextContent>k__BackingField = value;
			}
		}

		// Token: 0x04000054 RID: 84
		[CompilerGenerated]
		private IDictionary<string, object> <Content>k__BackingField;

		// Token: 0x04000055 RID: 85
		[CompilerGenerated]
		private string <TextContent>k__BackingField;
	}
}

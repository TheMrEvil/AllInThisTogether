using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001F7 RID: 503
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	[UsedByNativeCode]
	public class HelpURLAttribute : Attribute
	{
		// Token: 0x0600166E RID: 5742 RVA: 0x00023F73 File Offset: 0x00022173
		public HelpURLAttribute(string url)
		{
			this.m_Url = url;
			this.m_DispatchingFieldName = "";
			this.m_Dispatcher = false;
		}

		// Token: 0x0600166F RID: 5743 RVA: 0x00023F96 File Offset: 0x00022196
		internal HelpURLAttribute(string defaultURL, string dispatchingFieldName)
		{
			this.m_Url = defaultURL;
			this.m_DispatchingFieldName = dispatchingFieldName;
			this.m_Dispatcher = !string.IsNullOrEmpty(dispatchingFieldName);
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001670 RID: 5744 RVA: 0x00023FBD File Offset: 0x000221BD
		public string URL
		{
			get
			{
				return this.m_Url;
			}
		}

		// Token: 0x040007DA RID: 2010
		internal readonly string m_Url;

		// Token: 0x040007DB RID: 2011
		internal readonly bool m_Dispatcher;

		// Token: 0x040007DC RID: 2012
		internal readonly string m_DispatchingFieldName;
	}
}

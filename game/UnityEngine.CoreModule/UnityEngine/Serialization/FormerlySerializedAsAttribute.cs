using System;
using UnityEngine.Scripting;

namespace UnityEngine.Serialization
{
	// Token: 0x020002CE RID: 718
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = true, Inherited = false)]
	[RequiredByNativeCode]
	public class FormerlySerializedAsAttribute : Attribute
	{
		// Token: 0x06001DD3 RID: 7635 RVA: 0x000308A8 File Offset: 0x0002EAA8
		public FormerlySerializedAsAttribute(string oldName)
		{
			this.m_oldName = oldName;
		}

		// Token: 0x170005CC RID: 1484
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x000308BC File Offset: 0x0002EABC
		public string oldName
		{
			get
			{
				return this.m_oldName;
			}
		}

		// Token: 0x040009B5 RID: 2485
		private string m_oldName;
	}
}

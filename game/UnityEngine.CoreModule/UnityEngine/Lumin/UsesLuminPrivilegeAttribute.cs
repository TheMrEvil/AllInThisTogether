using System;

namespace UnityEngine.Lumin
{
	// Token: 0x02000397 RID: 919
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class UsesLuminPrivilegeAttribute : Attribute
	{
		// Token: 0x06001EFF RID: 7935 RVA: 0x00032824 File Offset: 0x00030A24
		public UsesLuminPrivilegeAttribute(string privilege)
		{
			this.m_Privilege = privilege;
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x00032838 File Offset: 0x00030A38
		public string privilege
		{
			get
			{
				return this.m_Privilege;
			}
		}

		// Token: 0x04000A35 RID: 2613
		private readonly string m_Privilege;
	}
}

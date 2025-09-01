using System;

namespace System.Security
{
	/// <summary>Manages the stack walk that determines whether all callers in the call stack have the required permissions to access a protected resource.</summary>
	// Token: 0x020003C9 RID: 969
	public interface IStackWalk
	{
		/// <summary>Asserts that the calling code can access the resource identified by the current permission object, even if callers higher in the stack have not been granted permission to access the resource.</summary>
		/// <exception cref="T:System.Security.SecurityException">The calling code does not have <see cref="F:System.Security.Permissions.SecurityPermissionFlag.Assertion" />.</exception>
		// Token: 0x06002856 RID: 10326
		void Assert();

		/// <summary>Determines at run time whether all callers in the call stack have been granted the permission specified by the current permission object.</summary>
		/// <exception cref="T:System.Security.SecurityException">A caller higher in the call stack does not have the permission specified by the current permission object.  
		///  -or-  
		///  A caller in the call stack has called <see cref="M:System.Security.IStackWalk.Deny" /> on the current permission object.</exception>
		// Token: 0x06002857 RID: 10327
		void Demand();

		/// <summary>Causes every <see cref="M:System.Security.IStackWalk.Demand" /> for the current object that passes through the calling code to fail.</summary>
		// Token: 0x06002858 RID: 10328
		void Deny();

		/// <summary>Causes every <see cref="M:System.Security.IStackWalk.Demand" /> for all objects except the current one that passes through the calling code to fail, even if code higher in the call stack has been granted permission to access other resources.</summary>
		// Token: 0x06002859 RID: 10329
		void PermitOnly();
	}
}

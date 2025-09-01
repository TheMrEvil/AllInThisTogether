using System;
using System.Security;
using System.Security.Permissions;

namespace System.Runtime.InteropServices
{
	/// <summary>Wraps objects the marshaler should marshal as a <see langword="VT_DISPATCH" />.</summary>
	// Token: 0x02000716 RID: 1814
	[ComVisible(true)]
	[Serializable]
	public sealed class DispatchWrapper
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" /> class with the object being wrapped.</summary>
		/// <param name="obj">The object to be wrapped and converted to <see cref="F:System.Runtime.InteropServices.VarEnum.VT_DISPATCH" />.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="obj" /> is not a class or an array.  
		/// -or-  
		/// <paramref name="obj" /> does not support <see langword="IDispatch" />.</exception>
		/// <exception cref="T:System.InvalidOperationException">The <paramref name="obj" /> parameter was marked with a <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" /> attribute that was passed a value of <see langword="false" />.  
		///  -or-  
		///  The <paramref name="obj" /> parameter inherits from a type marked with a <see cref="T:System.Runtime.InteropServices.ComVisibleAttribute" /> attribute that was passed a value of <see langword="false" />.</exception>
		// Token: 0x060040D5 RID: 16597 RVA: 0x000E170D File Offset: 0x000DF90D
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		public DispatchWrapper(object obj)
		{
			if (obj != null)
			{
				Marshal.Release(Marshal.GetIDispatchForObject(obj));
			}
			this.m_WrappedObject = obj;
		}

		/// <summary>Gets the object wrapped by the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.</summary>
		/// <returns>The object wrapped by the <see cref="T:System.Runtime.InteropServices.DispatchWrapper" />.</returns>
		// Token: 0x170009F1 RID: 2545
		// (get) Token: 0x060040D6 RID: 16598 RVA: 0x000E172B File Offset: 0x000DF92B
		public object WrappedObject
		{
			get
			{
				return this.m_WrappedObject;
			}
		}

		// Token: 0x04002AFA RID: 11002
		private object m_WrappedObject;
	}
}

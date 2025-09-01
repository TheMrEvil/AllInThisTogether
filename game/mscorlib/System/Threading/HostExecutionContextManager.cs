using System;
using System.Runtime.ConstrainedExecution;
using System.Security.Permissions;

namespace System.Threading
{
	/// <summary>Provides the functionality that allows a common language runtime host to participate in the flow, or migration, of the execution context.</summary>
	// Token: 0x020002EF RID: 751
	public class HostExecutionContextManager
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.HostExecutionContextManager" /> class.</summary>
		// Token: 0x060020AF RID: 8367 RVA: 0x0000259F File Offset: 0x0000079F
		public HostExecutionContextManager()
		{
		}

		/// <summary>Captures the host execution context from the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.HostExecutionContext" /> object representing the host execution context of the current thread.</returns>
		// Token: 0x060020B0 RID: 8368 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		public virtual HostExecutionContext Capture()
		{
			throw new NotImplementedException();
		}

		/// <summary>Restores the host execution context to its prior state.</summary>
		/// <param name="previousState">The previous context state to revert to.</param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="previousState" /> is <see langword="null" />.  
		/// -or-  
		/// <paramref name="previousState" /> was not created on the current thread.  
		/// -or-  
		/// <paramref name="previousState" /> is not the last state for the <see cref="T:System.Threading.HostExecutionContext" />.</exception>
		// Token: 0x060020B1 RID: 8369 RVA: 0x000479F8 File Offset: 0x00045BF8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
		[MonoTODO]
		public virtual void Revert(object previousState)
		{
			throw new NotImplementedException();
		}

		/// <summary>Sets the current host execution context to the specified host execution context.</summary>
		/// <param name="hostExecutionContext">The <see cref="T:System.Threading.HostExecutionContext" /> to be set.</param>
		/// <returns>An object for restoring the <see cref="T:System.Threading.HostExecutionContext" /> to its previous state.</returns>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="hostExecutionContext" /> was not acquired through a capture operation.  
		/// -or-  
		/// <paramref name="hostExecutionContext" /> has been the argument to a previous <see cref="M:System.Threading.HostExecutionContextManager.SetHostExecutionContext(System.Threading.HostExecutionContext)" /> method call.</exception>
		// Token: 0x060020B2 RID: 8370 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		[SecurityPermission(SecurityAction.LinkDemand, Infrastructure = true)]
		public virtual object SetHostExecutionContext(HostExecutionContext hostExecutionContext)
		{
			throw new NotImplementedException();
		}
	}
}

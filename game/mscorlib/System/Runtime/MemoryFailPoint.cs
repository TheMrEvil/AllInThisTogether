using System;
using System.Runtime.ConstrainedExecution;
using System.Security;

namespace System.Runtime
{
	/// <summary>Checks for sufficient memory resources before executing an operation. This class cannot be inherited.</summary>
	// Token: 0x02000553 RID: 1363
	public sealed class MemoryFailPoint : CriticalFinalizerObject, IDisposable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.MemoryFailPoint" /> class, specifying the amount of memory required for successful execution.</summary>
		/// <param name="sizeInMegabytes">The required memory size, in megabytes. This must be a positive value.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The specified memory size is negative.</exception>
		/// <exception cref="T:System.InsufficientMemoryException">There is insufficient memory to begin execution of the code protected by the gate.</exception>
		// Token: 0x060035B8 RID: 13752 RVA: 0x000C1FF0 File Offset: 0x000C01F0
		[MonoTODO]
		public MemoryFailPoint(int sizeInMegabytes)
		{
			throw new NotImplementedException();
		}

		/// <summary>Ensures that resources are freed and other cleanup operations are performed when the garbage collector reclaims the <see cref="T:System.Runtime.MemoryFailPoint" /> object.</summary>
		// Token: 0x060035B9 RID: 13753 RVA: 0x000C2000 File Offset: 0x000C0200
		~MemoryFailPoint()
		{
		}

		/// <summary>Releases all resources used by the <see cref="T:System.Runtime.MemoryFailPoint" />.</summary>
		// Token: 0x060035BA RID: 13754 RVA: 0x000479F8 File Offset: 0x00045BF8
		[MonoTODO]
		[SecuritySafeCritical]
		public void Dispose()
		{
			throw new NotImplementedException();
		}
	}
}

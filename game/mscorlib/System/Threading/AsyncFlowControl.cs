using System;
using System.Security;

namespace System.Threading
{
	/// <summary>Provides the functionality to restore the migration, or flow, of the execution context between threads.</summary>
	// Token: 0x020002CA RID: 714
	public struct AsyncFlowControl : IDisposable
	{
		// Token: 0x06001EE8 RID: 7912 RVA: 0x00072BD8 File Offset: 0x00070DD8
		[SecurityCritical]
		internal void Setup()
		{
			this.useEC = true;
			Thread currentThread = Thread.CurrentThread;
			this._ec = currentThread.GetMutableExecutionContext();
			this._ec.isFlowSuppressed = true;
			this._thread = currentThread;
		}

		/// <summary>Releases all resources used by the current instance of the <see cref="T:System.Threading.AsyncFlowControl" /> class.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.AsyncFlowControl" /> structure is not used on the thread where it was created.  
		///  -or-  
		///  The <see cref="T:System.Threading.AsyncFlowControl" /> structure has already been used to call <see cref="M:System.Threading.AsyncFlowControl.Dispose" /> or <see cref="M:System.Threading.AsyncFlowControl.Undo" />.</exception>
		// Token: 0x06001EE9 RID: 7913 RVA: 0x00072C11 File Offset: 0x00070E11
		public void Dispose()
		{
			this.Undo();
		}

		/// <summary>Restores the flow of the execution context between threads.</summary>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="T:System.Threading.AsyncFlowControl" /> structure is not used on the thread where it was created.  
		///  -or-  
		///  The <see cref="T:System.Threading.AsyncFlowControl" /> structure has already been used to call <see cref="M:System.Threading.AsyncFlowControl.Dispose" /> or <see cref="M:System.Threading.AsyncFlowControl.Undo" />.</exception>
		// Token: 0x06001EEA RID: 7914 RVA: 0x00072C1C File Offset: 0x00070E1C
		[SecuritySafeCritical]
		public void Undo()
		{
			if (this._thread == null)
			{
				throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl object can be used only once to call Undo()."));
			}
			if (this._thread != Thread.CurrentThread)
			{
				throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl object must be used on the thread where it was created."));
			}
			if (this.useEC)
			{
				if (Thread.CurrentThread.GetMutableExecutionContext() != this._ec)
				{
					throw new InvalidOperationException(Environment.GetResourceString("AsyncFlowControl objects can be used to restore flow only on the Context that had its flow suppressed."));
				}
				ExecutionContext.RestoreFlow();
			}
			this._thread = null;
		}

		/// <summary>Gets a hash code for the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</returns>
		// Token: 0x06001EEB RID: 7915 RVA: 0x00072C94 File Offset: 0x00070E94
		public override int GetHashCode()
		{
			if (this._thread != null)
			{
				return this._thread.GetHashCode();
			}
			return this.ToString().GetHashCode();
		}

		/// <summary>Determines whether the specified object is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</summary>
		/// <param name="obj">An object to compare with the current structure.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is an <see cref="T:System.Threading.AsyncFlowControl" /> structure and is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EEC RID: 7916 RVA: 0x00072CBB File Offset: 0x00070EBB
		public override bool Equals(object obj)
		{
			return obj is AsyncFlowControl && this.Equals((AsyncFlowControl)obj);
		}

		/// <summary>Determines whether the specified <see cref="T:System.Threading.AsyncFlowControl" /> structure is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure.</summary>
		/// <param name="obj">An <see cref="T:System.Threading.AsyncFlowControl" /> structure to compare with the current structure.</param>
		/// <returns>
		///   <see langword="true" /> if <paramref name="obj" /> is equal to the current <see cref="T:System.Threading.AsyncFlowControl" /> structure; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EED RID: 7917 RVA: 0x00072CD3 File Offset: 0x00070ED3
		public bool Equals(AsyncFlowControl obj)
		{
			return obj.useEC == this.useEC && obj._ec == this._ec && obj._thread == this._thread;
		}

		/// <summary>Compares two <see cref="T:System.Threading.AsyncFlowControl" /> structures to determine whether they are equal.</summary>
		/// <param name="a">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		/// <param name="b">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		/// <returns>
		///   <see langword="true" /> if the two structures are equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EEE RID: 7918 RVA: 0x00072D01 File Offset: 0x00070F01
		public static bool operator ==(AsyncFlowControl a, AsyncFlowControl b)
		{
			return a.Equals(b);
		}

		/// <summary>Compares two <see cref="T:System.Threading.AsyncFlowControl" /> structures to determine whether they are not equal.</summary>
		/// <param name="a">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		/// <param name="b">An <see cref="T:System.Threading.AsyncFlowControl" /> structure.</param>
		/// <returns>
		///   <see langword="true" /> if the structures are not equal; otherwise, <see langword="false" />.</returns>
		// Token: 0x06001EEF RID: 7919 RVA: 0x00072D0B File Offset: 0x00070F0B
		public static bool operator !=(AsyncFlowControl a, AsyncFlowControl b)
		{
			return !(a == b);
		}

		// Token: 0x04001AF2 RID: 6898
		private bool useEC;

		// Token: 0x04001AF3 RID: 6899
		private ExecutionContext _ec;

		// Token: 0x04001AF4 RID: 6900
		private Thread _thread;
	}
}

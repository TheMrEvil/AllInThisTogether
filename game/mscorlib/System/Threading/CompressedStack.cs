using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security;
using Unity;

namespace System.Threading
{
	/// <summary>Provides methods for setting and capturing the compressed stack on the current thread. This class cannot be inherited.</summary>
	// Token: 0x020002ED RID: 749
	[Serializable]
	public sealed class CompressedStack : ISerializable
	{
		// Token: 0x0600209D RID: 8349 RVA: 0x000768DC File Offset: 0x00074ADC
		internal CompressedStack(int length)
		{
			if (length > 0)
			{
				this._list = new ArrayList(length);
			}
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x000768F4 File Offset: 0x00074AF4
		internal CompressedStack(CompressedStack cs)
		{
			if (cs != null && cs._list != null)
			{
				this._list = (ArrayList)cs._list.Clone();
			}
		}

		/// <summary>Creates a copy of the current compressed stack.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object representing the current compressed stack.</returns>
		// Token: 0x0600209F RID: 8351 RVA: 0x0007691D File Offset: 0x00074B1D
		[ComVisible(false)]
		public CompressedStack CreateCopy()
		{
			return new CompressedStack(this);
		}

		/// <summary>Captures the compressed stack from the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> object.</returns>
		// Token: 0x060020A0 RID: 8352 RVA: 0x000472C8 File Offset: 0x000454C8
		public static CompressedStack Capture()
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets the compressed stack for the current thread.</summary>
		/// <returns>A <see cref="T:System.Threading.CompressedStack" /> for the current thread.</returns>
		/// <exception cref="T:System.Security.SecurityException">A caller in the call chain does not have permission to access unmanaged code.  
		///  -or-  
		///  The request for <see cref="T:System.Security.Permissions.StrongNameIdentityPermission" /> failed.</exception>
		// Token: 0x060020A1 RID: 8353 RVA: 0x000472C8 File Offset: 0x000454C8
		[SecurityCritical]
		public static CompressedStack GetCompressedStack()
		{
			throw new NotSupportedException();
		}

		/// <summary>Sets the <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object with the logical context information needed to recreate an instance of this execution context.</summary>
		/// <param name="info">The <see cref="T:System.Runtime.Serialization.SerializationInfo" /> object to be populated with serialization information.</param>
		/// <param name="context">The <see cref="T:System.Runtime.Serialization.StreamingContext" /> structure representing the destination context of the serialization.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="info" /> is <see langword="null" />.</exception>
		// Token: 0x060020A2 RID: 8354 RVA: 0x0005D900 File Offset: 0x0005BB00
		[MonoTODO("incomplete")]
		[SecurityCritical]
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
		}

		/// <summary>Runs a method in the specified compressed stack on the current thread.</summary>
		/// <param name="compressedStack">The <see cref="T:System.Threading.CompressedStack" /> to set.</param>
		/// <param name="callback">A <see cref="T:System.Threading.ContextCallback" /> that represents the method to be run in the specified security context.</param>
		/// <param name="state">The object to be passed to the callback method.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="compressedStack" /> is <see langword="null" />.</exception>
		// Token: 0x060020A3 RID: 8355 RVA: 0x000472C8 File Offset: 0x000454C8
		[SecurityCritical]
		public static void Run(CompressedStack compressedStack, ContextCallback callback, object state)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x00076925 File Offset: 0x00074B25
		internal bool Equals(CompressedStack cs)
		{
			if (this.IsEmpty())
			{
				return cs.IsEmpty();
			}
			return !cs.IsEmpty() && this._list.Count == cs._list.Count;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x0007695B File Offset: 0x00074B5B
		internal bool IsEmpty()
		{
			return this._list == null || this._list.Count == 0;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x060020A6 RID: 8358 RVA: 0x00076975 File Offset: 0x00074B75
		internal IList List
		{
			get
			{
				return this._list;
			}
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x000173AD File Offset: 0x000155AD
		internal CompressedStack()
		{
			ThrowStub.ThrowNotSupportedException();
		}

		// Token: 0x04001B6B RID: 7019
		private ArrayList _list;
	}
}

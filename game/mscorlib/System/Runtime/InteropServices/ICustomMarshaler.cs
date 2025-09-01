using System;

namespace System.Runtime.InteropServices
{
	/// <summary>Provides custom wrappers for handling method calls.</summary>
	// Token: 0x020006D9 RID: 1753
	public interface ICustomMarshaler
	{
		/// <summary>Converts the unmanaged data to managed data.</summary>
		/// <param name="pNativeData">A pointer to the unmanaged data to be wrapped.</param>
		/// <returns>An object that represents the managed view of the COM data.</returns>
		// Token: 0x06004034 RID: 16436
		object MarshalNativeToManaged(IntPtr pNativeData);

		/// <summary>Converts the managed data to unmanaged data.</summary>
		/// <param name="ManagedObj">The managed object to be converted.</param>
		/// <returns>A pointer to the COM view of the managed object.</returns>
		// Token: 0x06004035 RID: 16437
		IntPtr MarshalManagedToNative(object ManagedObj);

		/// <summary>Performs necessary cleanup of the unmanaged data when it is no longer needed.</summary>
		/// <param name="pNativeData">A pointer to the unmanaged data to be destroyed.</param>
		// Token: 0x06004036 RID: 16438
		void CleanUpNativeData(IntPtr pNativeData);

		/// <summary>Performs necessary cleanup of the managed data when it is no longer needed.</summary>
		/// <param name="ManagedObj">The managed object to be destroyed.</param>
		// Token: 0x06004037 RID: 16439
		void CleanUpManagedData(object ManagedObj);

		/// <summary>Returns the size of the native data to be marshaled.</summary>
		/// <returns>The size, in bytes, of the native data.</returns>
		// Token: 0x06004038 RID: 16440
		int GetNativeDataSize();
	}
}

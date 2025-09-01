using System;

namespace System.Runtime.InteropServices.ComTypes
{
	/// <summary>Provides a managed definition of the <see langword="IAdviseSink" /> interface.</summary>
	// Token: 0x0200018B RID: 395
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[Guid("0000010F-0000-0000-C000-000000000046")]
	[ComImport]
	public interface IAdviseSink
	{
		/// <summary>Notifies all registered advisory sinks that the object has changed from the running state to the loaded state.  This method is called by a server.</summary>
		// Token: 0x06000A70 RID: 2672
		[PreserveSig]
		void OnClose();

		/// <summary>Notifies all data objects currently registered advisory sinks that data in the object has changed.</summary>
		/// <param name="format">A <see cref="T:System.Runtime.InteropServices.ComTypes.FORMATETC" />, passed by reference, which describes the format, target device, rendering, and storage information of the calling data object.</param>
		/// <param name="stgmedium">A <see cref="T:System.Runtime.InteropServices.ComTypes.STGMEDIUM" />, passed by reference, which defines the storage medium (global memory, disk file, storage object, stream object, Graphics Device Interface (GDI) object, or undefined) and ownership of that medium for the calling data object.</param>
		// Token: 0x06000A71 RID: 2673
		[PreserveSig]
		void OnDataChange([In] ref FORMATETC format, [In] ref STGMEDIUM stgmedium);

		/// <summary>Notifies all registered advisory sinks that the object has been renamed. This method is called by a server.</summary>
		/// <param name="moniker">A pointer to the <see langword="IMoniker" /> interface on the new full moniker of the object.</param>
		// Token: 0x06000A72 RID: 2674
		[PreserveSig]
		void OnRename(IMoniker moniker);

		/// <summary>Notifies all registered advisory sinks that the object has been saved. This method is called by a server.</summary>
		// Token: 0x06000A73 RID: 2675
		[PreserveSig]
		void OnSave();

		/// <summary>Notifies an object's registered advisory sinks that its view has changed. This method is called by a server.</summary>
		/// <param name="aspect">The aspect, or view, of the object. Contains a value taken from the <see cref="T:System.Runtime.InteropServices.ComTypes.DVASPECT" /> enumeration.</param>
		/// <param name="index">The portion of the view that has changed. Currently, only -1 is valid.</param>
		// Token: 0x06000A74 RID: 2676
		[PreserveSig]
		void OnViewChange(int aspect, int index);
	}
}

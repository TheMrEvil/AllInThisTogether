using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices.WindowsRuntime
{
	/// <summary>Provides an event for resolving reflection-only type requests for types that are provided by Windows Metadata files, and methods for performing the resolution.</summary>
	// Token: 0x0200079A RID: 1946
	[MonoTODO]
	public static class WindowsRuntimeMetadata
	{
		/// <summary>Locates the Windows Metadata files for the specified namespace, given the specified locations to search.</summary>
		/// <param name="namespaceName">The namespace to resolve.</param>
		/// <param name="packageGraphFilePaths">The application paths to search for Windows Metadata files, or <see langword="null" /> to search only for Windows Metadata files from the operating system installation.</param>
		/// <returns>An enumerable list of strings that represent the Windows Metadata files that define <paramref name="namespaceName" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system version does not support the Windows Runtime.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceName" /> is <see langword="null" />.</exception>
		// Token: 0x060044EA RID: 17642 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static IEnumerable<string> ResolveNamespace(string namespaceName, IEnumerable<string> packageGraphFilePaths)
		{
			throw new NotImplementedException();
		}

		/// <summary>Locates the Windows Metadata files for the specified namespace, given the specified locations to search.</summary>
		/// <param name="namespaceName">The namespace to resolve.</param>
		/// <param name="windowsSdkFilePath">The path to search for Windows Metadata files provided by the SDK, or <see langword="null" /> to search for Windows Metadata files from the operating system installation.</param>
		/// <param name="packageGraphFilePaths">The application paths to search for Windows Metadata files.</param>
		/// <returns>An enumerable list of strings that represent the Windows Metadata files that define <paramref name="namespaceName" />.</returns>
		/// <exception cref="T:System.PlatformNotSupportedException">The operating system version does not support the Windows Runtime.</exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="namespaceName" /> is <see langword="null" />.</exception>
		// Token: 0x060044EB RID: 17643 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static IEnumerable<string> ResolveNamespace(string namespaceName, string windowsSdkFilePath, IEnumerable<string> packageGraphFilePaths)
		{
			throw new NotImplementedException();
		}

		/// <summary>Occurs when the resolution of a Windows Metadata file fails in the design environment.</summary>
		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060044EC RID: 17644 RVA: 0x000E4F78 File Offset: 0x000E3178
		// (remove) Token: 0x060044ED RID: 17645 RVA: 0x000E4FAC File Offset: 0x000E31AC
		public static event EventHandler<DesignerNamespaceResolveEventArgs> DesignerNamespaceResolve
		{
			[CompilerGenerated]
			add
			{
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.DesignerNamespaceResolve;
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<DesignerNamespaceResolveEventArgs> value2 = (EventHandler<DesignerNamespaceResolveEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<DesignerNamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.DesignerNamespaceResolve, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.DesignerNamespaceResolve;
				EventHandler<DesignerNamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<DesignerNamespaceResolveEventArgs> value2 = (EventHandler<DesignerNamespaceResolveEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<DesignerNamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.DesignerNamespaceResolve, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		/// <summary>Occurs when the resolution of a Windows Metadata file fails in the reflection-only context.</summary>
		// Token: 0x1400001A RID: 26
		// (add) Token: 0x060044EE RID: 17646 RVA: 0x000E4FE0 File Offset: 0x000E31E0
		// (remove) Token: 0x060044EF RID: 17647 RVA: 0x000E5014 File Offset: 0x000E3214
		public static event EventHandler<NamespaceResolveEventArgs> ReflectionOnlyNamespaceResolve
		{
			[CompilerGenerated]
			add
			{
				EventHandler<NamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve;
				EventHandler<NamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<NamespaceResolveEventArgs> value2 = (EventHandler<NamespaceResolveEventArgs>)Delegate.Combine(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<NamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
			[CompilerGenerated]
			remove
			{
				EventHandler<NamespaceResolveEventArgs> eventHandler = WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve;
				EventHandler<NamespaceResolveEventArgs> eventHandler2;
				do
				{
					eventHandler2 = eventHandler;
					EventHandler<NamespaceResolveEventArgs> value2 = (EventHandler<NamespaceResolveEventArgs>)Delegate.Remove(eventHandler2, value);
					eventHandler = Interlocked.CompareExchange<EventHandler<NamespaceResolveEventArgs>>(ref WindowsRuntimeMetadata.ReflectionOnlyNamespaceResolve, value2, eventHandler2);
				}
				while (eventHandler != eventHandler2);
			}
		}

		// Token: 0x04002C41 RID: 11329
		[CompilerGenerated]
		private static EventHandler<DesignerNamespaceResolveEventArgs> DesignerNamespaceResolve;

		// Token: 0x04002C42 RID: 11330
		[CompilerGenerated]
		private static EventHandler<NamespaceResolveEventArgs> ReflectionOnlyNamespaceResolve;
	}
}

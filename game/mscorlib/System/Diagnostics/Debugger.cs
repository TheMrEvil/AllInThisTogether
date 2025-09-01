using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Diagnostics
{
	/// <summary>Enables communication with a debugger. This class cannot be inherited.</summary>
	// Token: 0x020009C0 RID: 2496
	[ComVisible(true)]
	public sealed class Debugger
	{
		/// <summary>Gets a value that indicates whether a debugger is attached to the process.</summary>
		/// <returns>
		///   <see langword="true" /> if a debugger is attached; otherwise, <see langword="false" />.</returns>
		// Token: 0x17000F4D RID: 3917
		// (get) Token: 0x060059C9 RID: 22985 RVA: 0x001332A1 File Offset: 0x001314A1
		public static bool IsAttached
		{
			get
			{
				return Debugger.IsAttached_internal();
			}
		}

		// Token: 0x060059CA RID: 22986
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsAttached_internal();

		/// <summary>Signals a breakpoint to an attached debugger.</summary>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.Permissions.UIPermission" /> is not set to break into the debugger.</exception>
		// Token: 0x060059CB RID: 22987 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void Break()
		{
		}

		/// <summary>Checks to see if logging is enabled by an attached debugger.</summary>
		/// <returns>
		///   <see langword="true" /> if a debugger is attached and logging is enabled; otherwise, <see langword="false" />. The attached debugger is the registered managed debugger in the <see langword="DbgManagedDebugger" /> registry key. For more information on this key, see Enabling JIT-Attach Debugging.</returns>
		// Token: 0x060059CC RID: 22988
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool IsLogging();

		/// <summary>Launches and attaches a debugger to the process.</summary>
		/// <returns>
		///   <see langword="true" /> if the startup is successful or if the debugger is already attached; otherwise, <see langword="false" />.</returns>
		/// <exception cref="T:System.Security.SecurityException">The <see cref="T:System.Security.Permissions.UIPermission" /> is not set to start the debugger.</exception>
		// Token: 0x060059CD RID: 22989 RVA: 0x000479F8 File Offset: 0x00045BF8
		public static bool Launch()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060059CE RID: 22990
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Log_icall(int level, ref string category, ref string message);

		/// <summary>Posts a message for the attached debugger.</summary>
		/// <param name="level">A description of the importance of the message.</param>
		/// <param name="category">The category of the message.</param>
		/// <param name="message">The message to show.</param>
		// Token: 0x060059CF RID: 22991 RVA: 0x001332A8 File Offset: 0x001314A8
		public static void Log(int level, string category, string message)
		{
			Debugger.Log_icall(level, ref category, ref message);
		}

		/// <summary>Notifies a debugger that execution is about to enter a path that involves a cross-thread dependency.</summary>
		// Token: 0x060059D0 RID: 22992 RVA: 0x00004BF9 File Offset: 0x00002DF9
		public static void NotifyOfCrossThreadDependency()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.Debugger" /> class.</summary>
		// Token: 0x060059D1 RID: 22993 RVA: 0x0000259F File Offset: 0x0000079F
		[Obsolete("Call the static methods directly on this type", true)]
		public Debugger()
		{
		}

		// Token: 0x060059D2 RID: 22994 RVA: 0x001332B4 File Offset: 0x001314B4
		// Note: this type is marked as 'beforefieldinit'.
		static Debugger()
		{
		}

		/// <summary>Represents the default category of message with a constant.</summary>
		// Token: 0x0400378D RID: 14221
		public static readonly string DefaultCategory = "";
	}
}

using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x020002F8 RID: 760
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class InternalThread : CriticalFinalizerObject
	{
		// Token: 0x06002124 RID: 8484
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void Thread_free_internal();

		// Token: 0x06002125 RID: 8485 RVA: 0x00077BBC File Offset: 0x00075DBC
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		~InternalThread()
		{
			this.Thread_free_internal();
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x00077BE8 File Offset: 0x00075DE8
		public InternalThread()
		{
		}

		// Token: 0x04001B86 RID: 7046
		private int lock_thread_id;

		// Token: 0x04001B87 RID: 7047
		private IntPtr handle;

		// Token: 0x04001B88 RID: 7048
		private IntPtr native_handle;

		// Token: 0x04001B89 RID: 7049
		private IntPtr name_chars;

		// Token: 0x04001B8A RID: 7050
		private int name_free;

		// Token: 0x04001B8B RID: 7051
		private int name_length;

		// Token: 0x04001B8C RID: 7052
		private ThreadState state;

		// Token: 0x04001B8D RID: 7053
		private object abort_exc;

		// Token: 0x04001B8E RID: 7054
		private int abort_state_handle;

		// Token: 0x04001B8F RID: 7055
		internal long thread_id;

		// Token: 0x04001B90 RID: 7056
		private IntPtr debugger_thread;

		// Token: 0x04001B91 RID: 7057
		private UIntPtr static_data;

		// Token: 0x04001B92 RID: 7058
		private IntPtr runtime_thread_info;

		// Token: 0x04001B93 RID: 7059
		private object current_appcontext;

		// Token: 0x04001B94 RID: 7060
		private object root_domain_thread;

		// Token: 0x04001B95 RID: 7061
		internal byte[] _serialized_principal;

		// Token: 0x04001B96 RID: 7062
		internal int _serialized_principal_version;

		// Token: 0x04001B97 RID: 7063
		private IntPtr appdomain_refs;

		// Token: 0x04001B98 RID: 7064
		private int interruption_requested;

		// Token: 0x04001B99 RID: 7065
		private IntPtr longlived;

		// Token: 0x04001B9A RID: 7066
		internal bool threadpool_thread;

		// Token: 0x04001B9B RID: 7067
		private bool thread_interrupt_requested;

		// Token: 0x04001B9C RID: 7068
		internal int stack_size;

		// Token: 0x04001B9D RID: 7069
		internal byte apartment_state;

		// Token: 0x04001B9E RID: 7070
		internal volatile int critical_region_level;

		// Token: 0x04001B9F RID: 7071
		internal int managed_id;

		// Token: 0x04001BA0 RID: 7072
		private int small_id;

		// Token: 0x04001BA1 RID: 7073
		private IntPtr manage_callback;

		// Token: 0x04001BA2 RID: 7074
		private IntPtr flags;

		// Token: 0x04001BA3 RID: 7075
		private IntPtr thread_pinning_ref;

		// Token: 0x04001BA4 RID: 7076
		private IntPtr abort_protected_block_count;

		// Token: 0x04001BA5 RID: 7077
		private int priority = 2;

		// Token: 0x04001BA6 RID: 7078
		private IntPtr owned_mutex;

		// Token: 0x04001BA7 RID: 7079
		private IntPtr suspended_event;

		// Token: 0x04001BA8 RID: 7080
		private int self_suspended;

		// Token: 0x04001BA9 RID: 7081
		private IntPtr thread_state;

		// Token: 0x04001BAA RID: 7082
		private IntPtr netcore0;

		// Token: 0x04001BAB RID: 7083
		private IntPtr netcore1;

		// Token: 0x04001BAC RID: 7084
		private IntPtr netcore2;

		// Token: 0x04001BAD RID: 7085
		private IntPtr last;
	}
}

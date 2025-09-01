using System;
using System.Collections;
using System.Reflection;
using System.Security;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000200 RID: 512
	[RequiredByNativeCode]
	internal class SetupCoroutine
	{
		// Token: 0x060016AD RID: 5805 RVA: 0x00024434 File Offset: 0x00022634
		[SecuritySafeCritical]
		[RequiredByNativeCode]
		public unsafe static void InvokeMoveNext(IEnumerator enumerator, IntPtr returnValueAddress)
		{
			bool flag = returnValueAddress == IntPtr.Zero;
			if (flag)
			{
				throw new ArgumentException("Return value address cannot be 0.", "returnValueAddress");
			}
			*(byte*)((void*)returnValueAddress) = (enumerator.MoveNext() ? 1 : 0);
		}

		// Token: 0x060016AE RID: 5806 RVA: 0x00024470 File Offset: 0x00022670
		[RequiredByNativeCode]
		public static object InvokeMember(object behaviour, string name, object variable)
		{
			object[] args = null;
			bool flag = variable != null;
			if (flag)
			{
				args = new object[]
				{
					variable
				};
			}
			return behaviour.GetType().InvokeMember(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, behaviour, args, null, null, null);
		}

		// Token: 0x060016AF RID: 5807 RVA: 0x000244B0 File Offset: 0x000226B0
		public static object InvokeStatic(Type klass, string name, object variable)
		{
			object[] args = null;
			bool flag = variable != null;
			if (flag)
			{
				args = new object[]
				{
					variable
				};
			}
			return klass.InvokeMember(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, null, args, null, null, null);
		}

		// Token: 0x060016B0 RID: 5808 RVA: 0x00002072 File Offset: 0x00000272
		public SetupCoroutine()
		{
		}
	}
}

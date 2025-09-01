using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x020001BC RID: 444
	[NativeHeader("Runtime/Export/Logging/UnityLogWriter.bindings.h")]
	internal class UnityLogWriter : TextWriter
	{
		// Token: 0x06001376 RID: 4982 RVA: 0x0001B400 File Offset: 0x00019600
		[ThreadAndSerializationSafe]
		public static void WriteStringToUnityLog(string s)
		{
			bool flag = s == null;
			if (!flag)
			{
				UnityLogWriter.WriteStringToUnityLogImpl(s);
			}
		}

		// Token: 0x06001377 RID: 4983
		[FreeFunction(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WriteStringToUnityLogImpl(string s);

		// Token: 0x06001378 RID: 4984 RVA: 0x0001B41F File Offset: 0x0001961F
		public static void Init()
		{
			Console.SetOut(new UnityLogWriter());
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x06001379 RID: 4985 RVA: 0x0001B430 File Offset: 0x00019630
		public override Encoding Encoding
		{
			get
			{
				return Encoding.UTF8;
			}
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x0001B447 File Offset: 0x00019647
		public override void Write(char value)
		{
			UnityLogWriter.WriteStringToUnityLog(value.ToString());
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x0001B457 File Offset: 0x00019657
		public override void Write(string s)
		{
			UnityLogWriter.WriteStringToUnityLog(s);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x0001B461 File Offset: 0x00019661
		public override void Write(char[] buffer, int index, int count)
		{
			UnityLogWriter.WriteStringToUnityLogImpl(new string(buffer, index, count));
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x0001B472 File Offset: 0x00019672
		public UnityLogWriter()
		{
		}
	}
}

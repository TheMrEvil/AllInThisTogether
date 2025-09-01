using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000214 RID: 532
	[NativeHeader("Runtime/Mono/MonoBehaviour.h")]
	[NativeClass(null)]
	[ExtensionOfNativeClass]
	[RequiredByNativeCode]
	[StructLayout(LayoutKind.Sequential)]
	public class ScriptableObject : Object
	{
		// Token: 0x06001759 RID: 5977 RVA: 0x000258DA File Offset: 0x00023ADA
		public ScriptableObject()
		{
			ScriptableObject.CreateScriptableObject(this);
		}

		// Token: 0x0600175A RID: 5978
		[NativeConditional("ENABLE_MONO")]
		[Obsolete("Use EditorUtility.SetDirty instead")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void SetDirty();

		// Token: 0x0600175B RID: 5979 RVA: 0x000258EC File Offset: 0x00023AEC
		public static ScriptableObject CreateInstance(string className)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromName(className);
		}

		// Token: 0x0600175C RID: 5980 RVA: 0x00025904 File Offset: 0x00023B04
		public static ScriptableObject CreateInstance(Type type)
		{
			return ScriptableObject.CreateScriptableObjectInstanceFromType(type, true);
		}

		// Token: 0x0600175D RID: 5981 RVA: 0x00025920 File Offset: 0x00023B20
		public static T CreateInstance<T>() where T : ScriptableObject
		{
			return (T)((object)ScriptableObject.CreateInstance(typeof(T)));
		}

		// Token: 0x0600175E RID: 5982 RVA: 0x00025948 File Offset: 0x00023B48
		[EditorBrowsable(EditorBrowsableState.Never)]
		internal static ScriptableObject CreateInstance(Type type, Action<ScriptableObject> initialize)
		{
			bool flag = !typeof(ScriptableObject).IsAssignableFrom(type);
			if (flag)
			{
				throw new ArgumentException("Type must inherit ScriptableObject.", "type");
			}
			ScriptableObject scriptableObject = ScriptableObject.CreateScriptableObjectInstanceFromType(type, false);
			try
			{
				initialize(scriptableObject);
			}
			finally
			{
				ScriptableObject.ResetAndApplyDefaultInstances(scriptableObject);
			}
			return scriptableObject;
		}

		// Token: 0x0600175F RID: 5983
		[NativeMethod(IsThreadSafe = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void CreateScriptableObject([Writable] ScriptableObject self);

		// Token: 0x06001760 RID: 5984
		[FreeFunction("Scripting::CreateScriptableObject")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern ScriptableObject CreateScriptableObjectInstanceFromName(string className);

		// Token: 0x06001761 RID: 5985
		[FreeFunction("Scripting::CreateScriptableObjectWithType")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ScriptableObject CreateScriptableObjectInstanceFromType(Type type, bool applyDefaultsAndReset);

		// Token: 0x06001762 RID: 5986
		[FreeFunction("Scripting::ResetAndApplyDefaultInstances")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ResetAndApplyDefaultInstances([NotNull("NullExceptionObject")] Object obj);
	}
}

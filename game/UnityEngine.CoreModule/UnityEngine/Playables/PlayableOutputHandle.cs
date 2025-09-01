using System;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Playables
{
	// Token: 0x0200044A RID: 1098
	[NativeHeader("Runtime/Export/Director/PlayableOutputHandle.bindings.h")]
	[UsedByNativeCode]
	[NativeHeader("Runtime/Director/Core/HPlayable.h")]
	[NativeHeader("Runtime/Director/Core/HPlayableOutput.h")]
	public struct PlayableOutputHandle : IEquatable<PlayableOutputHandle>
	{
		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060026B2 RID: 9906 RVA: 0x00040B28 File Offset: 0x0003ED28
		public static PlayableOutputHandle Null
		{
			get
			{
				return PlayableOutputHandle.m_Null;
			}
		}

		// Token: 0x060026B3 RID: 9907 RVA: 0x00040B40 File Offset: 0x0003ED40
		[VisibleToOtherModules]
		internal bool IsPlayableOutputOfType<T>()
		{
			return this.GetPlayableOutputType() == typeof(T);
		}

		// Token: 0x060026B4 RID: 9908 RVA: 0x00040B64 File Offset: 0x0003ED64
		public override int GetHashCode()
		{
			return this.m_Handle.GetHashCode() ^ this.m_Version.GetHashCode();
		}

		// Token: 0x060026B5 RID: 9909 RVA: 0x00040B90 File Offset: 0x0003ED90
		public static bool operator ==(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return PlayableOutputHandle.CompareVersion(lhs, rhs);
		}

		// Token: 0x060026B6 RID: 9910 RVA: 0x00040BAC File Offset: 0x0003EDAC
		public static bool operator !=(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return !PlayableOutputHandle.CompareVersion(lhs, rhs);
		}

		// Token: 0x060026B7 RID: 9911 RVA: 0x00040BC8 File Offset: 0x0003EDC8
		public override bool Equals(object p)
		{
			return p is PlayableOutputHandle && this.Equals((PlayableOutputHandle)p);
		}

		// Token: 0x060026B8 RID: 9912 RVA: 0x00040BF4 File Offset: 0x0003EDF4
		public bool Equals(PlayableOutputHandle other)
		{
			return PlayableOutputHandle.CompareVersion(this, other);
		}

		// Token: 0x060026B9 RID: 9913 RVA: 0x00040C14 File Offset: 0x0003EE14
		internal static bool CompareVersion(PlayableOutputHandle lhs, PlayableOutputHandle rhs)
		{
			return lhs.m_Handle == rhs.m_Handle && lhs.m_Version == rhs.m_Version;
		}

		// Token: 0x060026BA RID: 9914 RVA: 0x00040C4A File Offset: 0x0003EE4A
		[VisibleToOtherModules]
		internal bool IsNull()
		{
			return PlayableOutputHandle.IsNull_Injected(ref this);
		}

		// Token: 0x060026BB RID: 9915 RVA: 0x00040C52 File Offset: 0x0003EE52
		[VisibleToOtherModules]
		internal bool IsValid()
		{
			return PlayableOutputHandle.IsValid_Injected(ref this);
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x00040C5A File Offset: 0x0003EE5A
		[FreeFunction("PlayableOutputHandleBindings::GetPlayableOutputType", HasExplicitThis = true, ThrowsException = true)]
		internal Type GetPlayableOutputType()
		{
			return PlayableOutputHandle.GetPlayableOutputType_Injected(ref this);
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x00040C62 File Offset: 0x0003EE62
		[FreeFunction("PlayableOutputHandleBindings::GetReferenceObject", HasExplicitThis = true, ThrowsException = true)]
		internal Object GetReferenceObject()
		{
			return PlayableOutputHandle.GetReferenceObject_Injected(ref this);
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x00040C6A File Offset: 0x0003EE6A
		[FreeFunction("PlayableOutputHandleBindings::SetReferenceObject", HasExplicitThis = true, ThrowsException = true)]
		internal void SetReferenceObject(Object target)
		{
			PlayableOutputHandle.SetReferenceObject_Injected(ref this, target);
		}

		// Token: 0x060026BF RID: 9919 RVA: 0x00040C73 File Offset: 0x0003EE73
		[FreeFunction("PlayableOutputHandleBindings::GetUserData", HasExplicitThis = true, ThrowsException = true)]
		internal Object GetUserData()
		{
			return PlayableOutputHandle.GetUserData_Injected(ref this);
		}

		// Token: 0x060026C0 RID: 9920 RVA: 0x00040C7B File Offset: 0x0003EE7B
		[FreeFunction("PlayableOutputHandleBindings::SetUserData", HasExplicitThis = true, ThrowsException = true)]
		internal void SetUserData([Writable] Object target)
		{
			PlayableOutputHandle.SetUserData_Injected(ref this, target);
		}

		// Token: 0x060026C1 RID: 9921 RVA: 0x00040C84 File Offset: 0x0003EE84
		[FreeFunction("PlayableOutputHandleBindings::GetSourcePlayable", HasExplicitThis = true, ThrowsException = true)]
		internal PlayableHandle GetSourcePlayable()
		{
			PlayableHandle result;
			PlayableOutputHandle.GetSourcePlayable_Injected(ref this, out result);
			return result;
		}

		// Token: 0x060026C2 RID: 9922 RVA: 0x00040C9A File Offset: 0x0003EE9A
		[FreeFunction("PlayableOutputHandleBindings::SetSourcePlayable", HasExplicitThis = true, ThrowsException = true)]
		internal void SetSourcePlayable(PlayableHandle target, int port)
		{
			PlayableOutputHandle.SetSourcePlayable_Injected(ref this, ref target, port);
		}

		// Token: 0x060026C3 RID: 9923 RVA: 0x00040CA5 File Offset: 0x0003EEA5
		[FreeFunction("PlayableOutputHandleBindings::GetSourceOutputPort", HasExplicitThis = true, ThrowsException = true)]
		internal int GetSourceOutputPort()
		{
			return PlayableOutputHandle.GetSourceOutputPort_Injected(ref this);
		}

		// Token: 0x060026C4 RID: 9924 RVA: 0x00040CAD File Offset: 0x0003EEAD
		[FreeFunction("PlayableOutputHandleBindings::GetWeight", HasExplicitThis = true, ThrowsException = true)]
		internal float GetWeight()
		{
			return PlayableOutputHandle.GetWeight_Injected(ref this);
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x00040CB5 File Offset: 0x0003EEB5
		[FreeFunction("PlayableOutputHandleBindings::SetWeight", HasExplicitThis = true, ThrowsException = true)]
		internal void SetWeight(float weight)
		{
			PlayableOutputHandle.SetWeight_Injected(ref this, weight);
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x00040CBE File Offset: 0x0003EEBE
		[FreeFunction("PlayableOutputHandleBindings::PushNotification", HasExplicitThis = true, ThrowsException = true)]
		internal void PushNotification(PlayableHandle origin, INotification notification, object context)
		{
			PlayableOutputHandle.PushNotification_Injected(ref this, ref origin, notification, context);
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x00040CCA File Offset: 0x0003EECA
		[FreeFunction("PlayableOutputHandleBindings::GetNotificationReceivers", HasExplicitThis = true, ThrowsException = true)]
		internal INotificationReceiver[] GetNotificationReceivers()
		{
			return PlayableOutputHandle.GetNotificationReceivers_Injected(ref this);
		}

		// Token: 0x060026C8 RID: 9928 RVA: 0x00040CD2 File Offset: 0x0003EED2
		[FreeFunction("PlayableOutputHandleBindings::AddNotificationReceiver", HasExplicitThis = true, ThrowsException = true)]
		internal void AddNotificationReceiver(INotificationReceiver receiver)
		{
			PlayableOutputHandle.AddNotificationReceiver_Injected(ref this, receiver);
		}

		// Token: 0x060026C9 RID: 9929 RVA: 0x00040CDB File Offset: 0x0003EEDB
		[FreeFunction("PlayableOutputHandleBindings::RemoveNotificationReceiver", HasExplicitThis = true, ThrowsException = true)]
		internal void RemoveNotificationReceiver(INotificationReceiver receiver)
		{
			PlayableOutputHandle.RemoveNotificationReceiver_Injected(ref this, receiver);
		}

		// Token: 0x060026CA RID: 9930 RVA: 0x00040CE4 File Offset: 0x0003EEE4
		// Note: this type is marked as 'beforefieldinit'.
		static PlayableOutputHandle()
		{
		}

		// Token: 0x060026CB RID: 9931
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsNull_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x060026CC RID: 9932
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsValid_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x060026CD RID: 9933
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Type GetPlayableOutputType_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x060026CE RID: 9934
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object GetReferenceObject_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x060026CF RID: 9935
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetReferenceObject_Injected(ref PlayableOutputHandle _unity_self, Object target);

		// Token: 0x060026D0 RID: 9936
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern Object GetUserData_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x060026D1 RID: 9937
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetUserData_Injected(ref PlayableOutputHandle _unity_self, [Writable] Object target);

		// Token: 0x060026D2 RID: 9938
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetSourcePlayable_Injected(ref PlayableOutputHandle _unity_self, out PlayableHandle ret);

		// Token: 0x060026D3 RID: 9939
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcePlayable_Injected(ref PlayableOutputHandle _unity_self, ref PlayableHandle target, int port);

		// Token: 0x060026D4 RID: 9940
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceOutputPort_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x060026D5 RID: 9941
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float GetWeight_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x060026D6 RID: 9942
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetWeight_Injected(ref PlayableOutputHandle _unity_self, float weight);

		// Token: 0x060026D7 RID: 9943
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void PushNotification_Injected(ref PlayableOutputHandle _unity_self, ref PlayableHandle origin, INotification notification, object context);

		// Token: 0x060026D8 RID: 9944
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern INotificationReceiver[] GetNotificationReceivers_Injected(ref PlayableOutputHandle _unity_self);

		// Token: 0x060026D9 RID: 9945
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddNotificationReceiver_Injected(ref PlayableOutputHandle _unity_self, INotificationReceiver receiver);

		// Token: 0x060026DA RID: 9946
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void RemoveNotificationReceiver_Injected(ref PlayableOutputHandle _unity_self, INotificationReceiver receiver);

		// Token: 0x04000E30 RID: 3632
		internal IntPtr m_Handle;

		// Token: 0x04000E31 RID: 3633
		internal uint m_Version;

		// Token: 0x04000E32 RID: 3634
		private static readonly PlayableOutputHandle m_Null = default(PlayableOutputHandle);
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000004 RID: 4
	[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTrackingFacade.h")]
	[StaticAccessor("XRInputTrackingFacade::Get()", StaticAccessorType.Dot)]
	[RequiredByNativeCode]
	[NativeConditional("ENABLE_VR")]
	public static class InputTracking
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000003 RID: 3 RVA: 0x0000205C File Offset: 0x0000025C
		// (remove) Token: 0x06000004 RID: 4 RVA: 0x00002090 File Offset: 0x00000290
		public static event Action<XRNodeState> trackingAcquired
		{
			[CompilerGenerated]
			add
			{
				Action<XRNodeState> action = InputTracking.trackingAcquired;
				Action<XRNodeState> action2;
				do
				{
					action2 = action;
					Action<XRNodeState> value2 = (Action<XRNodeState>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<XRNodeState>>(ref InputTracking.trackingAcquired, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<XRNodeState> action = InputTracking.trackingAcquired;
				Action<XRNodeState> action2;
				do
				{
					action2 = action;
					Action<XRNodeState> value2 = (Action<XRNodeState>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<XRNodeState>>(ref InputTracking.trackingAcquired, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000005 RID: 5 RVA: 0x000020C4 File Offset: 0x000002C4
		// (remove) Token: 0x06000006 RID: 6 RVA: 0x000020F8 File Offset: 0x000002F8
		public static event Action<XRNodeState> trackingLost
		{
			[CompilerGenerated]
			add
			{
				Action<XRNodeState> action = InputTracking.trackingLost;
				Action<XRNodeState> action2;
				do
				{
					action2 = action;
					Action<XRNodeState> value2 = (Action<XRNodeState>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<XRNodeState>>(ref InputTracking.trackingLost, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<XRNodeState> action = InputTracking.trackingLost;
				Action<XRNodeState> action2;
				do
				{
					action2 = action;
					Action<XRNodeState> value2 = (Action<XRNodeState>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<XRNodeState>>(ref InputTracking.trackingLost, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000007 RID: 7 RVA: 0x0000212C File Offset: 0x0000032C
		// (remove) Token: 0x06000008 RID: 8 RVA: 0x00002160 File Offset: 0x00000360
		public static event Action<XRNodeState> nodeAdded
		{
			[CompilerGenerated]
			add
			{
				Action<XRNodeState> action = InputTracking.nodeAdded;
				Action<XRNodeState> action2;
				do
				{
					action2 = action;
					Action<XRNodeState> value2 = (Action<XRNodeState>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<XRNodeState>>(ref InputTracking.nodeAdded, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<XRNodeState> action = InputTracking.nodeAdded;
				Action<XRNodeState> action2;
				do
				{
					action2 = action;
					Action<XRNodeState> value2 = (Action<XRNodeState>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<XRNodeState>>(ref InputTracking.nodeAdded, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000009 RID: 9 RVA: 0x00002194 File Offset: 0x00000394
		// (remove) Token: 0x0600000A RID: 10 RVA: 0x000021C8 File Offset: 0x000003C8
		public static event Action<XRNodeState> nodeRemoved
		{
			[CompilerGenerated]
			add
			{
				Action<XRNodeState> action = InputTracking.nodeRemoved;
				Action<XRNodeState> action2;
				do
				{
					action2 = action;
					Action<XRNodeState> value2 = (Action<XRNodeState>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<XRNodeState>>(ref InputTracking.nodeRemoved, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<XRNodeState> action = InputTracking.nodeRemoved;
				Action<XRNodeState> action2;
				do
				{
					action2 = action;
					Action<XRNodeState> value2 = (Action<XRNodeState>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<XRNodeState>>(ref InputTracking.nodeRemoved, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		[RequiredByNativeCode]
		private static void InvokeTrackingEvent(InputTracking.TrackingStateEventType eventType, XRNode nodeType, long uniqueID, bool tracked)
		{
			XRNodeState obj = default(XRNodeState);
			obj.uniqueID = (ulong)uniqueID;
			obj.nodeType = nodeType;
			obj.tracked = tracked;
			Action<XRNodeState> action;
			switch (eventType)
			{
			case InputTracking.TrackingStateEventType.NodeAdded:
				action = InputTracking.nodeAdded;
				break;
			case InputTracking.TrackingStateEventType.NodeRemoved:
				action = InputTracking.nodeRemoved;
				break;
			case InputTracking.TrackingStateEventType.TrackingAcquired:
				action = InputTracking.trackingAcquired;
				break;
			case InputTracking.TrackingStateEventType.TrackingLost:
				action = InputTracking.trackingLost;
				break;
			default:
				throw new ArgumentException("TrackingEventHandler - Invalid EventType: " + eventType.ToString());
			}
			bool flag = action != null;
			if (flag)
			{
				action(obj);
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000229C File Offset: 0x0000049C
		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.TryGetFeatureValue with the CommonUsages.devicePosition usage instead.")]
		[NativeConditional("ENABLE_VR", "Vector3f::zero")]
		public static Vector3 GetLocalPosition(XRNode node)
		{
			Vector3 result;
			InputTracking.GetLocalPosition_Injected(node, out result);
			return result;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022B4 File Offset: 0x000004B4
		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.TryGetFeatureValue with the CommonUsages.deviceRotation usage instead.")]
		[NativeConditional("ENABLE_VR", "Quaternionf::identity()")]
		public static Quaternion GetLocalRotation(XRNode node)
		{
			Quaternion result;
			InputTracking.GetLocalRotation_Injected(node, out result);
			return result;
		}

		// Token: 0x0600000E RID: 14
		[Obsolete("This API is obsolete, and should no longer be used. Please use XRInputSubsystem.TryRecenter() instead.")]
		[NativeConditional("ENABLE_VR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Recenter();

		// Token: 0x0600000F RID: 15
		[NativeConditional("ENABLE_VR")]
		[Obsolete("This API is obsolete, and should no longer be used. Please use InputDevice.name with the device associated with that tracking data instead.")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern string GetNodeName(ulong uniqueId);

		// Token: 0x06000010 RID: 16 RVA: 0x000022CC File Offset: 0x000004CC
		public static void GetNodeStates(List<XRNodeState> nodeStates)
		{
			bool flag = nodeStates == null;
			if (flag)
			{
				throw new ArgumentNullException("nodeStates");
			}
			nodeStates.Clear();
			InputTracking.GetNodeStates_Internal(nodeStates);
		}

		// Token: 0x06000011 RID: 17
		[NativeConditional("ENABLE_VR")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetNodeStates_Internal([NotNull("ArgumentNullException")] List<XRNodeState> nodeStates);

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000012 RID: 18
		// (set) Token: 0x06000013 RID: 19
		[Obsolete("This API is obsolete, and should no longer be used. Please use the TrackedPoseDriver in the Legacy Input Helpers package for controlling a camera in XR.")]
		[NativeConditional("ENABLE_VR")]
		public static extern bool disablePositionalTracking { [NativeName("GetPositionalTrackingDisabled")] [MethodImpl(MethodImplOptions.InternalCall)] get; [NativeName("SetPositionalTrackingDisabled")] [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000014 RID: 20
		[StaticAccessor("XRInputTracking::Get()", StaticAccessorType.Dot)]
		[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTracking.h")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ulong GetDeviceIdAtXRNode(XRNode node);

		// Token: 0x06000015 RID: 21
		[NativeHeader("Modules/XR/Subsystems/Input/Public/XRInputTracking.h")]
		[StaticAccessor("XRInputTracking::Get()", StaticAccessorType.Dot)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void GetDeviceIdsAtXRNode_Internal(XRNode node, [NotNull("ArgumentNullException")] List<ulong> deviceIds);

		// Token: 0x06000016 RID: 22
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalPosition_Injected(XRNode node, out Vector3 ret);

		// Token: 0x06000017 RID: 23
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetLocalRotation_Injected(XRNode node, out Quaternion ret);

		// Token: 0x04000001 RID: 1
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<XRNodeState> trackingAcquired;

		// Token: 0x04000002 RID: 2
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<XRNodeState> trackingLost;

		// Token: 0x04000003 RID: 3
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private static Action<XRNodeState> nodeAdded;

		// Token: 0x04000004 RID: 4
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<XRNodeState> nodeRemoved;

		// Token: 0x02000005 RID: 5
		private enum TrackingStateEventType
		{
			// Token: 0x04000006 RID: 6
			NodeAdded,
			// Token: 0x04000007 RID: 7
			NodeRemoved,
			// Token: 0x04000008 RID: 8
			TrackingAcquired,
			// Token: 0x04000009 RID: 9
			TrackingLost
		}
	}
}

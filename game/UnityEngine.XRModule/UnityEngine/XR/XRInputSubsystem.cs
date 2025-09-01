using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.XR
{
	// Token: 0x02000026 RID: 38
	[NativeType(Header = "Modules/XR/Subsystems/Input/XRInputSubsystem.h")]
	[NativeConditional("ENABLE_XR")]
	[UsedByNativeCode]
	public class XRInputSubsystem : IntegratedSubsystem<XRInputSubsystemDescriptor>
	{
		// Token: 0x06000120 RID: 288
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern uint GetIndex();

		// Token: 0x06000121 RID: 289
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TryRecenter();

		// Token: 0x06000122 RID: 290 RVA: 0x000043B4 File Offset: 0x000025B4
		public bool TryGetInputDevices(List<InputDevice> devices)
		{
			bool flag = devices == null;
			if (flag)
			{
				throw new ArgumentNullException("devices");
			}
			devices.Clear();
			bool flag2 = this.m_DeviceIdsCache == null;
			if (flag2)
			{
				this.m_DeviceIdsCache = new List<ulong>();
			}
			this.m_DeviceIdsCache.Clear();
			this.TryGetDeviceIds_AsList(this.m_DeviceIdsCache);
			for (int i = 0; i < this.m_DeviceIdsCache.Count; i++)
			{
				devices.Add(new InputDevice(this.m_DeviceIdsCache[i]));
			}
			return true;
		}

		// Token: 0x06000123 RID: 291
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool TrySetTrackingOriginMode(TrackingOriginModeFlags origin);

		// Token: 0x06000124 RID: 292
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TrackingOriginModeFlags GetTrackingOriginMode();

		// Token: 0x06000125 RID: 293
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern TrackingOriginModeFlags GetSupportedTrackingOriginModes();

		// Token: 0x06000126 RID: 294 RVA: 0x00004448 File Offset: 0x00002648
		public bool TryGetBoundaryPoints(List<Vector3> boundaryPoints)
		{
			bool flag = boundaryPoints == null;
			if (flag)
			{
				throw new ArgumentNullException("boundaryPoints");
			}
			return this.TryGetBoundaryPoints_AsList(boundaryPoints);
		}

		// Token: 0x06000127 RID: 295
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool TryGetBoundaryPoints_AsList(List<Vector3> boundaryPoints);

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000128 RID: 296 RVA: 0x00004474 File Offset: 0x00002674
		// (remove) Token: 0x06000129 RID: 297 RVA: 0x000044AC File Offset: 0x000026AC
		public event Action<XRInputSubsystem> trackingOriginUpdated
		{
			[CompilerGenerated]
			add
			{
				Action<XRInputSubsystem> action = this.trackingOriginUpdated;
				Action<XRInputSubsystem> action2;
				do
				{
					action2 = action;
					Action<XRInputSubsystem> value2 = (Action<XRInputSubsystem>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<XRInputSubsystem>>(ref this.trackingOriginUpdated, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<XRInputSubsystem> action = this.trackingOriginUpdated;
				Action<XRInputSubsystem> action2;
				do
				{
					action2 = action;
					Action<XRInputSubsystem> value2 = (Action<XRInputSubsystem>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<XRInputSubsystem>>(ref this.trackingOriginUpdated, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600012A RID: 298 RVA: 0x000044E4 File Offset: 0x000026E4
		// (remove) Token: 0x0600012B RID: 299 RVA: 0x0000451C File Offset: 0x0000271C
		public event Action<XRInputSubsystem> boundaryChanged
		{
			[CompilerGenerated]
			add
			{
				Action<XRInputSubsystem> action = this.boundaryChanged;
				Action<XRInputSubsystem> action2;
				do
				{
					action2 = action;
					Action<XRInputSubsystem> value2 = (Action<XRInputSubsystem>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<XRInputSubsystem>>(ref this.boundaryChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<XRInputSubsystem> action = this.boundaryChanged;
				Action<XRInputSubsystem> action2;
				do
				{
					action2 = action;
					Action<XRInputSubsystem> value2 = (Action<XRInputSubsystem>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<XRInputSubsystem>>(ref this.boundaryChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004554 File Offset: 0x00002754
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeTrackingOriginUpdatedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem integratedSubsystemByPtr = SubsystemManager.GetIntegratedSubsystemByPtr(internalPtr);
			XRInputSubsystem xrinputSubsystem = integratedSubsystemByPtr as XRInputSubsystem;
			bool flag = xrinputSubsystem != null && xrinputSubsystem.trackingOriginUpdated != null;
			if (flag)
			{
				xrinputSubsystem.trackingOriginUpdated(xrinputSubsystem);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004590 File Offset: 0x00002790
		[RequiredByNativeCode(GenerateProxy = true)]
		private static void InvokeBoundaryChangedEvent(IntPtr internalPtr)
		{
			IntegratedSubsystem integratedSubsystemByPtr = SubsystemManager.GetIntegratedSubsystemByPtr(internalPtr);
			XRInputSubsystem xrinputSubsystem = integratedSubsystemByPtr as XRInputSubsystem;
			bool flag = xrinputSubsystem != null && xrinputSubsystem.boundaryChanged != null;
			if (flag)
			{
				xrinputSubsystem.boundaryChanged(xrinputSubsystem);
			}
		}

		// Token: 0x0600012E RID: 302
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal extern void TryGetDeviceIds_AsList(List<ulong> deviceIds);

		// Token: 0x0600012F RID: 303 RVA: 0x000045CC File Offset: 0x000027CC
		public XRInputSubsystem()
		{
		}

		// Token: 0x040000E5 RID: 229
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<XRInputSubsystem> trackingOriginUpdated;

		// Token: 0x040000E6 RID: 230
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Action<XRInputSubsystem> boundaryChanged;

		// Token: 0x040000E7 RID: 231
		private List<ulong> m_DeviceIdsCache;
	}
}

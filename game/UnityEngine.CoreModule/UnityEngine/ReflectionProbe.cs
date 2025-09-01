using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine.Bindings;
using UnityEngine.Internal;
using UnityEngine.Rendering;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000105 RID: 261
	[NativeHeader("Runtime/Camera/ReflectionProbes.h")]
	public sealed class ReflectionProbe : Behaviour
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060005D5 RID: 1493
		// (set) Token: 0x060005D6 RID: 1494
		[Obsolete("type property has been deprecated. Starting with Unity 5.4, the only supported reflection probe type is Cube.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[NativeName("ProbeType")]
		public extern ReflectionProbeType type { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00008258 File Offset: 0x00006458
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x0000826E File Offset: 0x0000646E
		[NativeName("BoxSize")]
		public Vector3 size
		{
			get
			{
				Vector3 result;
				this.get_size_Injected(out result);
				return result;
			}
			set
			{
				this.set_size_Injected(ref value);
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00008278 File Offset: 0x00006478
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x0000828E File Offset: 0x0000648E
		[NativeName("BoxOffset")]
		public Vector3 center
		{
			get
			{
				Vector3 result;
				this.get_center_Injected(out result);
				return result;
			}
			set
			{
				this.set_center_Injected(ref value);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060005DB RID: 1499
		// (set) Token: 0x060005DC RID: 1500
		[NativeName("Near")]
		public extern float nearClipPlane { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060005DD RID: 1501
		// (set) Token: 0x060005DE RID: 1502
		[NativeName("Far")]
		public extern float farClipPlane { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060005DF RID: 1503
		// (set) Token: 0x060005E0 RID: 1504
		[NativeName("IntensityMultiplier")]
		public extern float intensity { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060005E1 RID: 1505 RVA: 0x00008298 File Offset: 0x00006498
		[NativeName("GlobalAABB")]
		public Bounds bounds
		{
			get
			{
				Bounds result;
				this.get_bounds_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060005E2 RID: 1506
		// (set) Token: 0x060005E3 RID: 1507
		[NativeName("HDR")]
		public extern bool hdr { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060005E4 RID: 1508
		// (set) Token: 0x060005E5 RID: 1509
		[NativeName("RenderDynamicObjects")]
		public extern bool renderDynamicObjects { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060005E6 RID: 1510
		// (set) Token: 0x060005E7 RID: 1511
		public extern float shadowDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060005E8 RID: 1512
		// (set) Token: 0x060005E9 RID: 1513
		public extern int resolution { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060005EA RID: 1514
		// (set) Token: 0x060005EB RID: 1515
		public extern int cullingMask { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060005EC RID: 1516
		// (set) Token: 0x060005ED RID: 1517
		public extern ReflectionProbeClearFlags clearFlags { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x000082B0 File Offset: 0x000064B0
		// (set) Token: 0x060005EF RID: 1519 RVA: 0x000082C6 File Offset: 0x000064C6
		public Color backgroundColor
		{
			get
			{
				Color result;
				this.get_backgroundColor_Injected(out result);
				return result;
			}
			set
			{
				this.set_backgroundColor_Injected(ref value);
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060005F0 RID: 1520
		// (set) Token: 0x060005F1 RID: 1521
		public extern float blendDistance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060005F2 RID: 1522
		// (set) Token: 0x060005F3 RID: 1523
		public extern bool boxProjection { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060005F4 RID: 1524
		// (set) Token: 0x060005F5 RID: 1525
		public extern ReflectionProbeMode mode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060005F6 RID: 1526
		// (set) Token: 0x060005F7 RID: 1527
		public extern int importance { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060005F8 RID: 1528
		// (set) Token: 0x060005F9 RID: 1529
		public extern ReflectionProbeRefreshMode refreshMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060005FA RID: 1530
		// (set) Token: 0x060005FB RID: 1531
		public extern ReflectionProbeTimeSlicingMode timeSlicingMode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060005FC RID: 1532
		// (set) Token: 0x060005FD RID: 1533
		public extern Texture bakedTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060005FE RID: 1534
		// (set) Token: 0x060005FF RID: 1535
		public extern Texture customBakedTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000600 RID: 1536
		// (set) Token: 0x06000601 RID: 1537
		public extern RenderTexture realtimeTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000602 RID: 1538
		public extern Texture texture { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000603 RID: 1539 RVA: 0x000082D0 File Offset: 0x000064D0
		public Vector4 textureHDRDecodeValues
		{
			[NativeName("CalculateHDRDecodeValues")]
			get
			{
				Vector4 result;
				this.get_textureHDRDecodeValues_Injected(out result);
				return result;
			}
		}

		// Token: 0x06000604 RID: 1540
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void Reset();

		// Token: 0x06000605 RID: 1541 RVA: 0x000082E8 File Offset: 0x000064E8
		public int RenderProbe()
		{
			return this.RenderProbe(null);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x00008304 File Offset: 0x00006504
		public int RenderProbe([UnityEngine.Internal.DefaultValue("null")] RenderTexture targetTexture)
		{
			return this.ScheduleRender(this.timeSlicingMode, targetTexture);
		}

		// Token: 0x06000607 RID: 1543
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern bool IsFinishedRendering(int renderId);

		// Token: 0x06000608 RID: 1544
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int ScheduleRender(ReflectionProbeTimeSlicingMode timeSlicingMode, RenderTexture targetTexture);

		// Token: 0x06000609 RID: 1545
		[NativeHeader("Runtime/Camera/CubemapGPUUtility.h")]
		[FreeFunction("CubemapGPUBlend")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern bool BlendCubemap(Texture src, Texture dst, float blend, RenderTexture target);

		// Token: 0x0600060A RID: 1546
		[StaticAccessor("GetReflectionProbes()")]
		[NativeMethod("UpdateSampleData")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void UpdateCachedState();

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x0600060B RID: 1547
		[StaticAccessor("GetReflectionProbes()")]
		public static extern int minBakedCubemapResolution { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x0600060C RID: 1548
		[StaticAccessor("GetReflectionProbes()")]
		public static extern int maxBakedCubemapResolution { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00008324 File Offset: 0x00006524
		[StaticAccessor("GetReflectionProbes()")]
		public static Vector4 defaultTextureHDRDecodeValues
		{
			get
			{
				Vector4 result;
				ReflectionProbe.get_defaultTextureHDRDecodeValues_Injected(out result);
				return result;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x0600060E RID: 1550
		[StaticAccessor("GetReflectionProbes()")]
		public static extern Texture defaultTexture { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x0600060F RID: 1551 RVA: 0x0000833C File Offset: 0x0000653C
		// (remove) Token: 0x06000610 RID: 1552 RVA: 0x00008370 File Offset: 0x00006570
		public static event Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> reflectionProbeChanged
		{
			[CompilerGenerated]
			add
			{
				Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> action = ReflectionProbe.reflectionProbeChanged;
				Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> action2;
				do
				{
					action2 = action;
					Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> value2 = (Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent>>(ref ReflectionProbe.reflectionProbeChanged, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> action = ReflectionProbe.reflectionProbeChanged;
				Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> action2;
				do
				{
					action2 = action;
					Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> value2 = (Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent>>(ref ReflectionProbe.reflectionProbeChanged, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000611 RID: 1553 RVA: 0x000083A4 File Offset: 0x000065A4
		// (remove) Token: 0x06000612 RID: 1554 RVA: 0x000083D8 File Offset: 0x000065D8
		[Obsolete("ReflectionProbe.defaultReflectionSet has been deprecated. Use ReflectionProbe.defaultReflectionTexture. (UnityUpgradable) -> UnityEngine.ReflectionProbe.defaultReflectionTexture", true)]
		public static event Action<Cubemap> defaultReflectionSet
		{
			[CompilerGenerated]
			add
			{
				Action<Cubemap> action = ReflectionProbe.defaultReflectionSet;
				Action<Cubemap> action2;
				do
				{
					action2 = action;
					Action<Cubemap> value2 = (Action<Cubemap>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Cubemap>>(ref ReflectionProbe.defaultReflectionSet, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Cubemap> action = ReflectionProbe.defaultReflectionSet;
				Action<Cubemap> action2;
				do
				{
					action2 = action;
					Action<Cubemap> value2 = (Action<Cubemap>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Cubemap>>(ref ReflectionProbe.defaultReflectionSet, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000613 RID: 1555 RVA: 0x0000840C File Offset: 0x0000660C
		// (remove) Token: 0x06000614 RID: 1556 RVA: 0x00008440 File Offset: 0x00006640
		public static event Action<Texture> defaultReflectionTexture
		{
			[CompilerGenerated]
			add
			{
				Action<Texture> action = ReflectionProbe.defaultReflectionTexture;
				Action<Texture> action2;
				do
				{
					action2 = action;
					Action<Texture> value2 = (Action<Texture>)Delegate.Combine(action2, value);
					action = Interlocked.CompareExchange<Action<Texture>>(ref ReflectionProbe.defaultReflectionTexture, value2, action2);
				}
				while (action != action2);
			}
			[CompilerGenerated]
			remove
			{
				Action<Texture> action = ReflectionProbe.defaultReflectionTexture;
				Action<Texture> action2;
				do
				{
					action2 = action;
					Action<Texture> value2 = (Action<Texture>)Delegate.Remove(action2, value);
					action = Interlocked.CompareExchange<Action<Texture>>(ref ReflectionProbe.defaultReflectionTexture, value2, action2);
				}
				while (action != action2);
			}
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x00008474 File Offset: 0x00006674
		[RequiredByNativeCode]
		private static void CallReflectionProbeEvent(ReflectionProbe probe, ReflectionProbe.ReflectionProbeEvent probeEvent)
		{
			Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> action = ReflectionProbe.reflectionProbeChanged;
			bool flag = action != null;
			if (flag)
			{
				action(probe, probeEvent);
			}
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x0000849C File Offset: 0x0000669C
		[RequiredByNativeCode]
		private static void CallSetDefaultReflection(Texture defaultReflectionCubemap)
		{
			Action<Texture> action = ReflectionProbe.defaultReflectionTexture;
			bool flag = action != null;
			if (flag)
			{
				action(defaultReflectionCubemap);
			}
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000084C0 File Offset: 0x000066C0
		public ReflectionProbe()
		{
		}

		// Token: 0x06000618 RID: 1560
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_size_Injected(out Vector3 ret);

		// Token: 0x06000619 RID: 1561
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_size_Injected(ref Vector3 value);

		// Token: 0x0600061A RID: 1562
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_center_Injected(out Vector3 ret);

		// Token: 0x0600061B RID: 1563
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_center_Injected(ref Vector3 value);

		// Token: 0x0600061C RID: 1564
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_bounds_Injected(out Bounds ret);

		// Token: 0x0600061D RID: 1565
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_backgroundColor_Injected(out Color ret);

		// Token: 0x0600061E RID: 1566
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_backgroundColor_Injected(ref Color value);

		// Token: 0x0600061F RID: 1567
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_textureHDRDecodeValues_Injected(out Vector4 ret);

		// Token: 0x06000620 RID: 1568
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_defaultTextureHDRDecodeValues_Injected(out Vector4 ret);

		// Token: 0x04000373 RID: 883
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<ReflectionProbe, ReflectionProbe.ReflectionProbeEvent> reflectionProbeChanged;

		// Token: 0x04000374 RID: 884
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Cubemap> defaultReflectionSet;

		// Token: 0x04000375 RID: 885
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private static Action<Texture> defaultReflectionTexture;

		// Token: 0x02000106 RID: 262
		public enum ReflectionProbeEvent
		{
			// Token: 0x04000377 RID: 887
			ReflectionProbeAdded,
			// Token: 0x04000378 RID: 888
			ReflectionProbeRemoved
		}
	}
}

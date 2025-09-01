using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000069 RID: 105
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/ScaleConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class ScaleConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060005CC RID: 1484 RVA: 0x00007E99 File Offset: 0x00006099
		private ScaleConstraint()
		{
			ScaleConstraint.Internal_Create(this);
		}

		// Token: 0x060005CD RID: 1485
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] ScaleConstraint self);

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060005CE RID: 1486
		// (set) Token: 0x060005CF RID: 1487
		public extern float weight { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060005D0 RID: 1488 RVA: 0x00007EAC File Offset: 0x000060AC
		// (set) Token: 0x060005D1 RID: 1489 RVA: 0x00007EC2 File Offset: 0x000060C2
		public Vector3 scaleAtRest
		{
			get
			{
				Vector3 result;
				this.get_scaleAtRest_Injected(out result);
				return result;
			}
			set
			{
				this.set_scaleAtRest_Injected(ref value);
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x00007ECC File Offset: 0x000060CC
		// (set) Token: 0x060005D3 RID: 1491 RVA: 0x00007EE2 File Offset: 0x000060E2
		public Vector3 scaleOffset
		{
			get
			{
				Vector3 result;
				this.get_scaleOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_scaleOffset_Injected(ref value);
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060005D4 RID: 1492
		// (set) Token: 0x060005D5 RID: 1493
		public extern Axis scalingAxis { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060005D6 RID: 1494
		// (set) Token: 0x060005D7 RID: 1495
		public extern bool constraintActive { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060005D8 RID: 1496
		// (set) Token: 0x060005D9 RID: 1497
		public extern bool locked { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060005DA RID: 1498 RVA: 0x00007EEC File Offset: 0x000060EC
		public int sourceCount
		{
			get
			{
				return ScaleConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060005DB RID: 1499
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] ScaleConstraint self);

		// Token: 0x060005DC RID: 1500
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		// Token: 0x060005DD RID: 1501 RVA: 0x00007F04 File Offset: 0x00006104
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			ScaleConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060005DE RID: 1502
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] ScaleConstraint self, List<ConstraintSource> sources);

		// Token: 0x060005DF RID: 1503 RVA: 0x00007F2D File Offset: 0x0000612D
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00007F37 File Offset: 0x00006137
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060005E1 RID: 1505
		[NativeName("RemoveSource")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x060005E2 RID: 1506 RVA: 0x00007F4C File Offset: 0x0000614C
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00007F70 File Offset: 0x00006170
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource result;
			this.GetSourceInternal_Injected(index, out result);
			return result;
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00007F87 File Offset: 0x00006187
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060005E5 RID: 1509 RVA: 0x00007F9B File Offset: 0x0000619B
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x060005E6 RID: 1510 RVA: 0x00007FA8 File Offset: 0x000061A8
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The ScaleConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060005E7 RID: 1511
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_scaleAtRest_Injected(out Vector3 ret);

		// Token: 0x060005E8 RID: 1512
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_scaleAtRest_Injected(ref Vector3 value);

		// Token: 0x060005E9 RID: 1513
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_scaleOffset_Injected(out Vector3 ret);

		// Token: 0x060005EA RID: 1514
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_scaleOffset_Injected(ref Vector3 value);

		// Token: 0x060005EB RID: 1515
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x060005EC RID: 1516
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x060005ED RID: 1517
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000068 RID: 104
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	[NativeHeader("Modules/Animation/Constraints/RotationConstraint.h")]
	[UsedByNativeCode]
	public sealed class RotationConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060005AA RID: 1450 RVA: 0x00007D25 File Offset: 0x00005F25
		private RotationConstraint()
		{
			RotationConstraint.Internal_Create(this);
		}

		// Token: 0x060005AB RID: 1451
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] RotationConstraint self);

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060005AC RID: 1452
		// (set) Token: 0x060005AD RID: 1453
		public extern float weight { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060005AE RID: 1454 RVA: 0x00007D38 File Offset: 0x00005F38
		// (set) Token: 0x060005AF RID: 1455 RVA: 0x00007D4E File Offset: 0x00005F4E
		public Vector3 rotationAtRest
		{
			get
			{
				Vector3 result;
				this.get_rotationAtRest_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotationAtRest_Injected(ref value);
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x00007D58 File Offset: 0x00005F58
		// (set) Token: 0x060005B1 RID: 1457 RVA: 0x00007D6E File Offset: 0x00005F6E
		public Vector3 rotationOffset
		{
			get
			{
				Vector3 result;
				this.get_rotationOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_rotationOffset_Injected(ref value);
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060005B2 RID: 1458
		// (set) Token: 0x060005B3 RID: 1459
		public extern Axis rotationAxis { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060005B4 RID: 1460
		// (set) Token: 0x060005B5 RID: 1461
		public extern bool constraintActive { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060005B6 RID: 1462
		// (set) Token: 0x060005B7 RID: 1463
		public extern bool locked { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060005B8 RID: 1464 RVA: 0x00007D78 File Offset: 0x00005F78
		public int sourceCount
		{
			get
			{
				return RotationConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x060005B9 RID: 1465
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] RotationConstraint self);

		// Token: 0x060005BA RID: 1466
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		// Token: 0x060005BB RID: 1467 RVA: 0x00007D90 File Offset: 0x00005F90
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			RotationConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x060005BC RID: 1468
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] RotationConstraint self, List<ConstraintSource> sources);

		// Token: 0x060005BD RID: 1469 RVA: 0x00007DB9 File Offset: 0x00005FB9
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00007DC3 File Offset: 0x00005FC3
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x060005BF RID: 1471
		[NativeName("RemoveSource")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x060005C0 RID: 1472 RVA: 0x00007DD8 File Offset: 0x00005FD8
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00007DFC File Offset: 0x00005FFC
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource result;
			this.GetSourceInternal_Injected(index, out result);
			return result;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00007E13 File Offset: 0x00006013
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00007E27 File Offset: 0x00006027
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00007E34 File Offset: 0x00006034
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The RotationConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060005C5 RID: 1477
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		// Token: 0x060005C6 RID: 1478
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		// Token: 0x060005C7 RID: 1479
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationOffset_Injected(out Vector3 ret);

		// Token: 0x060005C8 RID: 1480
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationOffset_Injected(ref Vector3 value);

		// Token: 0x060005C9 RID: 1481
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x060005CA RID: 1482
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x060005CB RID: 1483
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}

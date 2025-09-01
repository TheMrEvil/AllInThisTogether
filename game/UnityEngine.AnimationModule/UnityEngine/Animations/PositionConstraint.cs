using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x02000067 RID: 103
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/PositionConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class PositionConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x06000588 RID: 1416 RVA: 0x00007BB2 File Offset: 0x00005DB2
		private PositionConstraint()
		{
			PositionConstraint.Internal_Create(this);
		}

		// Token: 0x06000589 RID: 1417
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] PositionConstraint self);

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600058A RID: 1418
		// (set) Token: 0x0600058B RID: 1419
		public extern float weight { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x00007BC4 File Offset: 0x00005DC4
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x00007BDA File Offset: 0x00005DDA
		public Vector3 translationAtRest
		{
			get
			{
				Vector3 result;
				this.get_translationAtRest_Injected(out result);
				return result;
			}
			set
			{
				this.set_translationAtRest_Injected(ref value);
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x00007BE4 File Offset: 0x00005DE4
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x00007BFA File Offset: 0x00005DFA
		public Vector3 translationOffset
		{
			get
			{
				Vector3 result;
				this.get_translationOffset_Injected(out result);
				return result;
			}
			set
			{
				this.set_translationOffset_Injected(ref value);
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000590 RID: 1424
		// (set) Token: 0x06000591 RID: 1425
		public extern Axis translationAxis { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000592 RID: 1426
		// (set) Token: 0x06000593 RID: 1427
		public extern bool constraintActive { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000594 RID: 1428
		// (set) Token: 0x06000595 RID: 1429
		public extern bool locked { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x00007C04 File Offset: 0x00005E04
		public int sourceCount
		{
			get
			{
				return PositionConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x06000597 RID: 1431
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] PositionConstraint self);

		// Token: 0x06000598 RID: 1432
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		// Token: 0x06000599 RID: 1433 RVA: 0x00007C1C File Offset: 0x00005E1C
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			PositionConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x0600059A RID: 1434
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] PositionConstraint self, List<ConstraintSource> sources);

		// Token: 0x0600059B RID: 1435 RVA: 0x00007C45 File Offset: 0x00005E45
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x0600059C RID: 1436 RVA: 0x00007C4F File Offset: 0x00005E4F
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x0600059D RID: 1437
		[NativeName("RemoveSource")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x0600059E RID: 1438 RVA: 0x00007C64 File Offset: 0x00005E64
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x0600059F RID: 1439 RVA: 0x00007C88 File Offset: 0x00005E88
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource result;
			this.GetSourceInternal_Injected(index, out result);
			return result;
		}

		// Token: 0x060005A0 RID: 1440 RVA: 0x00007C9F File Offset: 0x00005E9F
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00007CB3 File Offset: 0x00005EB3
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00007CC0 File Offset: 0x00005EC0
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The PositionConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x060005A3 RID: 1443
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_translationAtRest_Injected(out Vector3 ret);

		// Token: 0x060005A4 RID: 1444
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_translationAtRest_Injected(ref Vector3 value);

		// Token: 0x060005A5 RID: 1445
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_translationOffset_Injected(out Vector3 ret);

		// Token: 0x060005A6 RID: 1446
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_translationOffset_Injected(ref Vector3 value);

		// Token: 0x060005A7 RID: 1447
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x060005A8 RID: 1448
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x060005A9 RID: 1449
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}

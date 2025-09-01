using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;
using UnityEngine.Scripting;

namespace UnityEngine.Animations
{
	// Token: 0x0200006A RID: 106
	[UsedByNativeCode]
	[RequireComponent(typeof(Transform))]
	[NativeHeader("Modules/Animation/Constraints/LookAtConstraint.h")]
	[NativeHeader("Modules/Animation/Constraints/Constraint.bindings.h")]
	public sealed class LookAtConstraint : Behaviour, IConstraint, IConstraintInternal
	{
		// Token: 0x060005EE RID: 1518 RVA: 0x0000800D File Offset: 0x0000620D
		private LookAtConstraint()
		{
			LookAtConstraint.Internal_Create(this);
		}

		// Token: 0x060005EF RID: 1519
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void Internal_Create([Writable] LookAtConstraint self);

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060005F0 RID: 1520
		// (set) Token: 0x060005F1 RID: 1521
		public extern float weight { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060005F2 RID: 1522
		// (set) Token: 0x060005F3 RID: 1523
		public extern float roll { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060005F4 RID: 1524
		// (set) Token: 0x060005F5 RID: 1525
		public extern bool constraintActive { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060005F6 RID: 1526
		// (set) Token: 0x060005F7 RID: 1527
		public extern bool locked { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x00008020 File Offset: 0x00006220
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x00008036 File Offset: 0x00006236
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

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060005FA RID: 1530 RVA: 0x00008040 File Offset: 0x00006240
		// (set) Token: 0x060005FB RID: 1531 RVA: 0x00008056 File Offset: 0x00006256
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

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005FC RID: 1532
		// (set) Token: 0x060005FD RID: 1533
		public extern Transform worldUpObject { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005FE RID: 1534
		// (set) Token: 0x060005FF RID: 1535
		public extern bool useUpObject { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000600 RID: 1536 RVA: 0x00008060 File Offset: 0x00006260
		public int sourceCount
		{
			get
			{
				return LookAtConstraint.GetSourceCountInternal(this);
			}
		}

		// Token: 0x06000601 RID: 1537
		[FreeFunction("ConstraintBindings::GetSourceCount")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int GetSourceCountInternal([NotNull("ArgumentNullException")] LookAtConstraint self);

		// Token: 0x06000602 RID: 1538
		[FreeFunction(Name = "ConstraintBindings::GetSources", HasExplicitThis = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern void GetSources([NotNull("ArgumentNullException")] List<ConstraintSource> sources);

		// Token: 0x06000603 RID: 1539 RVA: 0x00008078 File Offset: 0x00006278
		public void SetSources(List<ConstraintSource> sources)
		{
			bool flag = sources == null;
			if (flag)
			{
				throw new ArgumentNullException("sources");
			}
			LookAtConstraint.SetSourcesInternal(this, sources);
		}

		// Token: 0x06000604 RID: 1540
		[FreeFunction("ConstraintBindings::SetSources", ThrowsException = true)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetSourcesInternal([NotNull("ArgumentNullException")] LookAtConstraint self, List<ConstraintSource> sources);

		// Token: 0x06000605 RID: 1541 RVA: 0x000080A1 File Offset: 0x000062A1
		public int AddSource(ConstraintSource source)
		{
			return this.AddSource_Injected(ref source);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x000080AB File Offset: 0x000062AB
		public void RemoveSource(int index)
		{
			this.ValidateSourceIndex(index);
			this.RemoveSourceInternal(index);
		}

		// Token: 0x06000607 RID: 1543
		[NativeName("RemoveSource")]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void RemoveSourceInternal(int index);

		// Token: 0x06000608 RID: 1544 RVA: 0x000080C0 File Offset: 0x000062C0
		public ConstraintSource GetSource(int index)
		{
			this.ValidateSourceIndex(index);
			return this.GetSourceInternal(index);
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x000080E4 File Offset: 0x000062E4
		[NativeName("GetSource")]
		private ConstraintSource GetSourceInternal(int index)
		{
			ConstraintSource result;
			this.GetSourceInternal_Injected(index, out result);
			return result;
		}

		// Token: 0x0600060A RID: 1546 RVA: 0x000080FB File Offset: 0x000062FB
		public void SetSource(int index, ConstraintSource source)
		{
			this.ValidateSourceIndex(index);
			this.SetSourceInternal(index, source);
		}

		// Token: 0x0600060B RID: 1547 RVA: 0x0000810F File Offset: 0x0000630F
		[NativeName("SetSource")]
		private void SetSourceInternal(int index, ConstraintSource source)
		{
			this.SetSourceInternal_Injected(index, ref source);
		}

		// Token: 0x0600060C RID: 1548 RVA: 0x0000811C File Offset: 0x0000631C
		private void ValidateSourceIndex(int index)
		{
			bool flag = this.sourceCount == 0;
			if (flag)
			{
				throw new InvalidOperationException("The LookAtConstraint component has no sources.");
			}
			bool flag2 = index < 0 || index >= this.sourceCount;
			if (flag2)
			{
				throw new ArgumentOutOfRangeException("index", string.Format("Constraint source index {0} is out of bounds (0-{1}).", index, this.sourceCount));
			}
		}

		// Token: 0x0600060D RID: 1549
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationAtRest_Injected(out Vector3 ret);

		// Token: 0x0600060E RID: 1550
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationAtRest_Injected(ref Vector3 value);

		// Token: 0x0600060F RID: 1551
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void get_rotationOffset_Injected(out Vector3 ret);

		// Token: 0x06000610 RID: 1552
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void set_rotationOffset_Injected(ref Vector3 value);

		// Token: 0x06000611 RID: 1553
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern int AddSource_Injected(ref ConstraintSource source);

		// Token: 0x06000612 RID: 1554
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void GetSourceInternal_Injected(int index, out ConstraintSource ret);

		// Token: 0x06000613 RID: 1555
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern void SetSourceInternal_Injected(int index, ref ConstraintSource source);
	}
}

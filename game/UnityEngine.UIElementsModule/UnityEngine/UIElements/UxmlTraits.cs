using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace UnityEngine.UIElements
{
	// Token: 0x020002E7 RID: 743
	public abstract class UxmlTraits
	{
		// Token: 0x060018B0 RID: 6320 RVA: 0x0006551D File Offset: 0x0006371D
		protected UxmlTraits()
		{
			this.canHaveAnyAttribute = true;
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060018B1 RID: 6321 RVA: 0x0006552F File Offset: 0x0006372F
		// (set) Token: 0x060018B2 RID: 6322 RVA: 0x00065537 File Offset: 0x00063737
		public bool canHaveAnyAttribute
		{
			[CompilerGenerated]
			get
			{
				return this.<canHaveAnyAttribute>k__BackingField;
			}
			[CompilerGenerated]
			protected set
			{
				this.<canHaveAnyAttribute>k__BackingField = value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060018B3 RID: 6323 RVA: 0x00065540 File Offset: 0x00063740
		public virtual IEnumerable<UxmlAttributeDescription> uxmlAttributesDescription
		{
			get
			{
				foreach (UxmlAttributeDescription attributeDescription in this.GetAllAttributeDescriptionForType(base.GetType()))
				{
					yield return attributeDescription;
					attributeDescription = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x00065560 File Offset: 0x00063760
		public virtual IEnumerable<UxmlChildElementDescription> uxmlChildElementsDescription
		{
			get
			{
				yield break;
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x00002166 File Offset: 0x00000366
		public virtual void Init(VisualElement ve, IUxmlAttributes bag, CreationContext cc)
		{
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0006557F File Offset: 0x0006377F
		private IEnumerable<UxmlAttributeDescription> GetAllAttributeDescriptionForType(Type t)
		{
			Type baseType = t.BaseType;
			bool flag = baseType != null;
			if (flag)
			{
				foreach (UxmlAttributeDescription ident in this.GetAllAttributeDescriptionForType(baseType))
				{
					yield return ident;
					ident = null;
				}
				IEnumerator<UxmlAttributeDescription> enumerator = null;
			}
			foreach (FieldInfo fieldInfo in from f in t.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
			where typeof(UxmlAttributeDescription).IsAssignableFrom(f.FieldType)
			select f)
			{
				yield return (UxmlAttributeDescription)fieldInfo.GetValue(this);
				fieldInfo = null;
			}
			IEnumerator<FieldInfo> enumerator2 = null;
			yield break;
			yield break;
		}

		// Token: 0x04000A9A RID: 2714
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <canHaveAnyAttribute>k__BackingField;

		// Token: 0x020002E8 RID: 744
		[CompilerGenerated]
		private sealed class <get_uxmlAttributesDescription>d__6 : IEnumerable<UxmlAttributeDescription>, IEnumerable, IEnumerator<UxmlAttributeDescription>, IEnumerator, IDisposable
		{
			// Token: 0x060018B7 RID: 6327 RVA: 0x00065596 File Offset: 0x00063796
			[DebuggerHidden]
			public <get_uxmlAttributesDescription>d__6(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060018B8 RID: 6328 RVA: 0x000655B8 File Offset: 0x000637B8
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				int num = this.<>1__state;
				if (num == -3 || num == 1)
				{
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
				}
			}

			// Token: 0x060018B9 RID: 6329 RVA: 0x000655F8 File Offset: 0x000637F8
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					int num = this.<>1__state;
					if (num != 0)
					{
						if (num != 1)
						{
							return false;
						}
						this.<>1__state = -3;
						attributeDescription = null;
					}
					else
					{
						this.<>1__state = -1;
						enumerator = base.GetAllAttributeDescriptionForType(base.GetType()).GetEnumerator();
						this.<>1__state = -3;
					}
					if (!enumerator.MoveNext())
					{
						this.<>m__Finally1();
						enumerator = null;
						result = false;
					}
					else
					{
						attributeDescription = enumerator.Current;
						this.<>2__current = attributeDescription;
						this.<>1__state = 1;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060018BA RID: 6330 RVA: 0x000656C8 File Offset: 0x000638C8
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x170005FE RID: 1534
			// (get) Token: 0x060018BB RID: 6331 RVA: 0x000656E5 File Offset: 0x000638E5
			UxmlAttributeDescription IEnumerator<UxmlAttributeDescription>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060018BC RID: 6332 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x170005FF RID: 1535
			// (get) Token: 0x060018BD RID: 6333 RVA: 0x000656E5 File Offset: 0x000638E5
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060018BE RID: 6334 RVA: 0x000656F0 File Offset: 0x000638F0
			[DebuggerHidden]
			IEnumerator<UxmlAttributeDescription> IEnumerable<UxmlAttributeDescription>.GetEnumerator()
			{
				UxmlTraits.<get_uxmlAttributesDescription>d__6 <get_uxmlAttributesDescription>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_uxmlAttributesDescription>d__ = this;
				}
				else
				{
					<get_uxmlAttributesDescription>d__ = new UxmlTraits.<get_uxmlAttributesDescription>d__6(0);
					<get_uxmlAttributesDescription>d__.<>4__this = this;
				}
				return <get_uxmlAttributesDescription>d__;
			}

			// Token: 0x060018BF RID: 6335 RVA: 0x00065738 File Offset: 0x00063938
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlAttributeDescription>.GetEnumerator();
			}

			// Token: 0x04000A9B RID: 2715
			private int <>1__state;

			// Token: 0x04000A9C RID: 2716
			private UxmlAttributeDescription <>2__current;

			// Token: 0x04000A9D RID: 2717
			private int <>l__initialThreadId;

			// Token: 0x04000A9E RID: 2718
			public UxmlTraits <>4__this;

			// Token: 0x04000A9F RID: 2719
			private IEnumerator<UxmlAttributeDescription> <>s__1;

			// Token: 0x04000AA0 RID: 2720
			private UxmlAttributeDescription <attributeDescription>5__2;
		}

		// Token: 0x020002E9 RID: 745
		[CompilerGenerated]
		private sealed class <get_uxmlChildElementsDescription>d__8 : IEnumerable<UxmlChildElementDescription>, IEnumerable, IEnumerator<UxmlChildElementDescription>, IEnumerator, IDisposable
		{
			// Token: 0x060018C0 RID: 6336 RVA: 0x00065740 File Offset: 0x00063940
			[DebuggerHidden]
			public <get_uxmlChildElementsDescription>d__8(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060018C1 RID: 6337 RVA: 0x000080DB File Offset: 0x000062DB
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
			}

			// Token: 0x060018C2 RID: 6338 RVA: 0x00065760 File Offset: 0x00063960
			bool IEnumerator.MoveNext()
			{
				int num = this.<>1__state;
				if (num != 0)
				{
					return false;
				}
				this.<>1__state = -1;
				return false;
			}

			// Token: 0x17000600 RID: 1536
			// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00065786 File Offset: 0x00063986
			UxmlChildElementDescription IEnumerator<UxmlChildElementDescription>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060018C4 RID: 6340 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000601 RID: 1537
			// (get) Token: 0x060018C5 RID: 6341 RVA: 0x00065786 File Offset: 0x00063986
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060018C6 RID: 6342 RVA: 0x00065790 File Offset: 0x00063990
			[DebuggerHidden]
			IEnumerator<UxmlChildElementDescription> IEnumerable<UxmlChildElementDescription>.GetEnumerator()
			{
				UxmlTraits.<get_uxmlChildElementsDescription>d__8 <get_uxmlChildElementsDescription>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<get_uxmlChildElementsDescription>d__ = this;
				}
				else
				{
					<get_uxmlChildElementsDescription>d__ = new UxmlTraits.<get_uxmlChildElementsDescription>d__8(0);
					<get_uxmlChildElementsDescription>d__.<>4__this = this;
				}
				return <get_uxmlChildElementsDescription>d__;
			}

			// Token: 0x060018C7 RID: 6343 RVA: 0x000657D8 File Offset: 0x000639D8
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlChildElementDescription>.GetEnumerator();
			}

			// Token: 0x04000AA1 RID: 2721
			private int <>1__state;

			// Token: 0x04000AA2 RID: 2722
			private UxmlChildElementDescription <>2__current;

			// Token: 0x04000AA3 RID: 2723
			private int <>l__initialThreadId;

			// Token: 0x04000AA4 RID: 2724
			public UxmlTraits <>4__this;
		}

		// Token: 0x020002EA RID: 746
		[CompilerGenerated]
		[Serializable]
		private sealed class <>c
		{
			// Token: 0x060018C8 RID: 6344 RVA: 0x000657E0 File Offset: 0x000639E0
			// Note: this type is marked as 'beforefieldinit'.
			static <>c()
			{
			}

			// Token: 0x060018C9 RID: 6345 RVA: 0x000020C2 File Offset: 0x000002C2
			public <>c()
			{
			}

			// Token: 0x060018CA RID: 6346 RVA: 0x000657EC File Offset: 0x000639EC
			internal bool <GetAllAttributeDescriptionForType>b__10_0(FieldInfo f)
			{
				return typeof(UxmlAttributeDescription).IsAssignableFrom(f.FieldType);
			}

			// Token: 0x04000AA5 RID: 2725
			public static readonly UxmlTraits.<>c <>9 = new UxmlTraits.<>c();

			// Token: 0x04000AA6 RID: 2726
			public static Func<FieldInfo, bool> <>9__10_0;
		}

		// Token: 0x020002EB RID: 747
		[CompilerGenerated]
		private sealed class <GetAllAttributeDescriptionForType>d__10 : IEnumerable<UxmlAttributeDescription>, IEnumerable, IEnumerator<UxmlAttributeDescription>, IEnumerator, IDisposable
		{
			// Token: 0x060018CB RID: 6347 RVA: 0x00065803 File Offset: 0x00063A03
			[DebuggerHidden]
			public <GetAllAttributeDescriptionForType>d__10(int <>1__state)
			{
				this.<>1__state = <>1__state;
				this.<>l__initialThreadId = Thread.CurrentThread.ManagedThreadId;
			}

			// Token: 0x060018CC RID: 6348 RVA: 0x00065824 File Offset: 0x00063A24
			[DebuggerHidden]
			void IDisposable.Dispose()
			{
				switch (this.<>1__state)
				{
				case -4:
				case 2:
					try
					{
					}
					finally
					{
						this.<>m__Finally2();
					}
					break;
				case -3:
				case 1:
					try
					{
					}
					finally
					{
						this.<>m__Finally1();
					}
					break;
				}
			}

			// Token: 0x060018CD RID: 6349 RVA: 0x00065894 File Offset: 0x00063A94
			bool IEnumerator.MoveNext()
			{
				bool result;
				try
				{
					switch (this.<>1__state)
					{
					case 0:
					{
						this.<>1__state = -1;
						baseType = t.BaseType;
						bool flag = baseType != null;
						if (!flag)
						{
							goto IL_D7;
						}
						enumerator = base.GetAllAttributeDescriptionForType(baseType).GetEnumerator();
						this.<>1__state = -3;
						break;
					}
					case 1:
						this.<>1__state = -3;
						ident = null;
						break;
					case 2:
						this.<>1__state = -4;
						fieldInfo = null;
						goto IL_167;
					default:
						return false;
					}
					if (enumerator.MoveNext())
					{
						ident = enumerator.Current;
						this.<>2__current = ident;
						this.<>1__state = 1;
						return true;
					}
					this.<>m__Finally1();
					enumerator = null;
					IL_D7:
					enumerator2 = t.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic).Where(new Func<FieldInfo, bool>(UxmlTraits.<>c.<>9.<GetAllAttributeDescriptionForType>b__10_0)).GetEnumerator();
					this.<>1__state = -4;
					IL_167:
					if (!enumerator2.MoveNext())
					{
						this.<>m__Finally2();
						enumerator2 = null;
						result = false;
					}
					else
					{
						fieldInfo = enumerator2.Current;
						this.<>2__current = (UxmlAttributeDescription)fieldInfo.GetValue(this);
						this.<>1__state = 2;
						result = true;
					}
				}
				catch
				{
					this.System.IDisposable.Dispose();
					throw;
				}
				return result;
			}

			// Token: 0x060018CE RID: 6350 RVA: 0x00065A4C File Offset: 0x00063C4C
			private void <>m__Finally1()
			{
				this.<>1__state = -1;
				if (enumerator != null)
				{
					enumerator.Dispose();
				}
			}

			// Token: 0x060018CF RID: 6351 RVA: 0x00065A69 File Offset: 0x00063C69
			private void <>m__Finally2()
			{
				this.<>1__state = -1;
				if (enumerator2 != null)
				{
					enumerator2.Dispose();
				}
			}

			// Token: 0x17000602 RID: 1538
			// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00065A86 File Offset: 0x00063C86
			UxmlAttributeDescription IEnumerator<UxmlAttributeDescription>.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060018D1 RID: 6353 RVA: 0x0000810E File Offset: 0x0000630E
			[DebuggerHidden]
			void IEnumerator.Reset()
			{
				throw new NotSupportedException();
			}

			// Token: 0x17000603 RID: 1539
			// (get) Token: 0x060018D2 RID: 6354 RVA: 0x00065A86 File Offset: 0x00063C86
			object IEnumerator.Current
			{
				[DebuggerHidden]
				get
				{
					return this.<>2__current;
				}
			}

			// Token: 0x060018D3 RID: 6355 RVA: 0x00065A90 File Offset: 0x00063C90
			[DebuggerHidden]
			IEnumerator<UxmlAttributeDescription> IEnumerable<UxmlAttributeDescription>.GetEnumerator()
			{
				UxmlTraits.<GetAllAttributeDescriptionForType>d__10 <GetAllAttributeDescriptionForType>d__;
				if (this.<>1__state == -2 && this.<>l__initialThreadId == Thread.CurrentThread.ManagedThreadId)
				{
					this.<>1__state = 0;
					<GetAllAttributeDescriptionForType>d__ = this;
				}
				else
				{
					<GetAllAttributeDescriptionForType>d__ = new UxmlTraits.<GetAllAttributeDescriptionForType>d__10(0);
					<GetAllAttributeDescriptionForType>d__.<>4__this = this;
				}
				<GetAllAttributeDescriptionForType>d__.t = t;
				return <GetAllAttributeDescriptionForType>d__;
			}

			// Token: 0x060018D4 RID: 6356 RVA: 0x00065AE4 File Offset: 0x00063CE4
			[DebuggerHidden]
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.System.Collections.Generic.IEnumerable<UnityEngine.UIElements.UxmlAttributeDescription>.GetEnumerator();
			}

			// Token: 0x04000AA7 RID: 2727
			private int <>1__state;

			// Token: 0x04000AA8 RID: 2728
			private UxmlAttributeDescription <>2__current;

			// Token: 0x04000AA9 RID: 2729
			private int <>l__initialThreadId;

			// Token: 0x04000AAA RID: 2730
			private Type t;

			// Token: 0x04000AAB RID: 2731
			public Type <>3__t;

			// Token: 0x04000AAC RID: 2732
			public UxmlTraits <>4__this;

			// Token: 0x04000AAD RID: 2733
			private Type <baseType>5__1;

			// Token: 0x04000AAE RID: 2734
			private IEnumerator<UxmlAttributeDescription> <>s__2;

			// Token: 0x04000AAF RID: 2735
			private UxmlAttributeDescription <ident>5__3;

			// Token: 0x04000AB0 RID: 2736
			private IEnumerator<FieldInfo> <>s__4;

			// Token: 0x04000AB1 RID: 2737
			private FieldInfo <fieldInfo>5__5;
		}
	}
}

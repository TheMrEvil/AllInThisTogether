using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Reflection.Emit
{
	// Token: 0x02000922 RID: 2338
	[StructLayout(LayoutKind.Sequential)]
	internal class EventOnTypeBuilderInst : EventInfo
	{
		// Token: 0x06004FFA RID: 20474 RVA: 0x000FA844 File Offset: 0x000F8A44
		internal EventOnTypeBuilderInst(TypeBuilderInstantiation instantiation, EventBuilder evt)
		{
			this.instantiation = instantiation;
			this.event_builder = evt;
		}

		// Token: 0x06004FFB RID: 20475 RVA: 0x000FA85A File Offset: 0x000F8A5A
		internal EventOnTypeBuilderInst(TypeBuilderInstantiation instantiation, EventInfo evt)
		{
			this.instantiation = instantiation;
			this.event_info = evt;
		}

		// Token: 0x17000D21 RID: 3361
		// (get) Token: 0x06004FFC RID: 20476 RVA: 0x000FA870 File Offset: 0x000F8A70
		public override EventAttributes Attributes
		{
			get
			{
				if (this.event_builder == null)
				{
					return this.event_info.Attributes;
				}
				return this.event_builder.attrs;
			}
		}

		// Token: 0x06004FFD RID: 20477 RVA: 0x000FA894 File Offset: 0x000F8A94
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			MethodInfo methodInfo = (this.event_builder != null) ? this.event_builder.add_method : this.event_info.GetAddMethod(nonPublic);
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06004FFE RID: 20478 RVA: 0x000FA8E8 File Offset: 0x000F8AE8
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			MethodInfo methodInfo = (this.event_builder != null) ? this.event_builder.raise_method : this.event_info.GetRaiseMethod(nonPublic);
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06004FFF RID: 20479 RVA: 0x000FA93C File Offset: 0x000F8B3C
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			MethodInfo methodInfo = (this.event_builder != null) ? this.event_builder.remove_method : this.event_info.GetRemoveMethod(nonPublic);
			if (methodInfo == null || (!nonPublic && !methodInfo.IsPublic))
			{
				return null;
			}
			return TypeBuilder.GetMethod(this.instantiation, methodInfo);
		}

		// Token: 0x06005000 RID: 20480 RVA: 0x000FA990 File Offset: 0x000F8B90
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			MethodInfo[] array;
			if (this.event_builder == null)
			{
				array = this.event_info.GetOtherMethods(nonPublic);
			}
			else
			{
				MethodInfo[] array2 = this.event_builder.other_methods;
				array = array2;
			}
			MethodInfo[] array3 = array;
			if (array3 == null)
			{
				return new MethodInfo[0];
			}
			ArrayList arrayList = new ArrayList();
			foreach (MethodInfo methodInfo in array3)
			{
				if (nonPublic || methodInfo.IsPublic)
				{
					arrayList.Add(TypeBuilder.GetMethod(this.instantiation, methodInfo));
				}
			}
			MethodInfo[] array4 = new MethodInfo[arrayList.Count];
			arrayList.CopyTo(array4, 0);
			return array4;
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x06005001 RID: 20481 RVA: 0x000FAA20 File Offset: 0x000F8C20
		public override Type DeclaringType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x17000D23 RID: 3363
		// (get) Token: 0x06005002 RID: 20482 RVA: 0x000FAA28 File Offset: 0x000F8C28
		public override string Name
		{
			get
			{
				if (this.event_builder == null)
				{
					return this.event_info.Name;
				}
				return this.event_builder.name;
			}
		}

		// Token: 0x17000D24 RID: 3364
		// (get) Token: 0x06005003 RID: 20483 RVA: 0x000FAA20 File Offset: 0x000F8C20
		public override Type ReflectedType
		{
			get
			{
				return this.instantiation;
			}
		}

		// Token: 0x06005004 RID: 20484 RVA: 0x000472C8 File Offset: 0x000454C8
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005005 RID: 20485 RVA: 0x000472C8 File Offset: 0x000454C8
		public override object[] GetCustomAttributes(bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005006 RID: 20486 RVA: 0x000472C8 File Offset: 0x000454C8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw new NotSupportedException();
		}

		// Token: 0x04003158 RID: 12632
		private TypeBuilderInstantiation instantiation;

		// Token: 0x04003159 RID: 12633
		private EventBuilder event_builder;

		// Token: 0x0400315A RID: 12634
		private EventInfo event_info;
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020008F4 RID: 2292
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class RuntimeEventInfo : EventInfo, ISerializable
	{
		// Token: 0x06004CFA RID: 19706
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_event_info(RuntimeEventInfo ev, out MonoEventInfo info);

		// Token: 0x06004CFB RID: 19707 RVA: 0x000F39AC File Offset: 0x000F1BAC
		internal static MonoEventInfo GetEventInfo(RuntimeEventInfo ev)
		{
			MonoEventInfo result;
			RuntimeEventInfo.get_event_info(ev, out result);
			return result;
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06004CFC RID: 19708 RVA: 0x000F39C2 File Offset: 0x000F1BC2
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06004CFD RID: 19709 RVA: 0x000F39CA File Offset: 0x000F1BCA
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.GetBindingFlags();
			}
		}

		// Token: 0x06004CFE RID: 19710 RVA: 0x000F39D2 File Offset: 0x000F1BD2
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06004CFF RID: 19711 RVA: 0x000F39DF File Offset: 0x000F1BDF
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004D00 RID: 19712 RVA: 0x000F39EC File Offset: 0x000F1BEC
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06004D01 RID: 19713 RVA: 0x000F39F9 File Offset: 0x000F1BF9
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, null, MemberTypes.Event);
		}

		// Token: 0x06004D02 RID: 19714 RVA: 0x000F3A20 File Offset: 0x000F1C20
		internal BindingFlags GetBindingFlags()
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			MethodInfo methodInfo = eventInfo.add_method;
			if (methodInfo == null)
			{
				methodInfo = eventInfo.remove_method;
			}
			if (methodInfo == null)
			{
				methodInfo = eventInfo.raise_method;
			}
			return RuntimeType.FilterPreCalculate(methodInfo != null && methodInfo.IsPublic, this.GetDeclaringTypeInternal() != this.ReflectedType, methodInfo != null && methodInfo.IsStatic);
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06004D03 RID: 19715 RVA: 0x000F3A95 File Offset: 0x000F1C95
		public override EventAttributes Attributes
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).attrs;
			}
		}

		// Token: 0x06004D04 RID: 19716 RVA: 0x000F3AA4 File Offset: 0x000F1CA4
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.add_method != null && eventInfo.add_method.IsPublic))
			{
				return eventInfo.add_method;
			}
			return null;
		}

		// Token: 0x06004D05 RID: 19717 RVA: 0x000F3AE0 File Offset: 0x000F1CE0
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.raise_method != null && eventInfo.raise_method.IsPublic))
			{
				return eventInfo.raise_method;
			}
			return null;
		}

		// Token: 0x06004D06 RID: 19718 RVA: 0x000F3B1C File Offset: 0x000F1D1C
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.remove_method != null && eventInfo.remove_method.IsPublic))
			{
				return eventInfo.remove_method;
			}
			return null;
		}

		// Token: 0x06004D07 RID: 19719 RVA: 0x000F3B58 File Offset: 0x000F1D58
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			MonoEventInfo eventInfo = RuntimeEventInfo.GetEventInfo(this);
			if (nonPublic)
			{
				return eventInfo.other_methods;
			}
			int num = 0;
			MethodInfo[] other_methods = eventInfo.other_methods;
			for (int i = 0; i < other_methods.Length; i++)
			{
				if (other_methods[i].IsPublic)
				{
					num++;
				}
			}
			if (num == eventInfo.other_methods.Length)
			{
				return eventInfo.other_methods;
			}
			MethodInfo[] array = new MethodInfo[num];
			num = 0;
			foreach (MethodInfo methodInfo in eventInfo.other_methods)
			{
				if (methodInfo.IsPublic)
				{
					array[num++] = methodInfo;
				}
			}
			return array;
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06004D08 RID: 19720 RVA: 0x000F3BED File Offset: 0x000F1DED
		public override Type DeclaringType
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).declaring_type;
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06004D09 RID: 19721 RVA: 0x000F3BFA File Offset: 0x000F1DFA
		public override Type ReflectedType
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).reflected_type;
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06004D0A RID: 19722 RVA: 0x000F3C07 File Offset: 0x000F1E07
		public override string Name
		{
			get
			{
				return RuntimeEventInfo.GetEventInfo(this).name;
			}
		}

		// Token: 0x06004D0B RID: 19723 RVA: 0x000F3C14 File Offset: 0x000F1E14
		public override string ToString()
		{
			Type eventHandlerType = this.EventHandlerType;
			return ((eventHandlerType != null) ? eventHandlerType.ToString() : null) + " " + this.Name;
		}

		// Token: 0x06004D0C RID: 19724 RVA: 0x00052A66 File Offset: 0x00050C66
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x000F1905 File Offset: 0x000EFB05
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004D0E RID: 19726 RVA: 0x000F190E File Offset: 0x000EFB0E
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004D0F RID: 19727 RVA: 0x000F3C38 File Offset: 0x000F1E38
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06004D10 RID: 19728 RVA: 0x000F3C40 File Offset: 0x000F1E40
		public override int MetadataToken
		{
			get
			{
				return RuntimeEventInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x000F3C48 File Offset: 0x000F1E48
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeEventInfo>(other);
		}

		// Token: 0x06004D12 RID: 19730
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeEventInfo monoEvent);

		// Token: 0x06004D13 RID: 19731 RVA: 0x000F3C51 File Offset: 0x000F1E51
		public RuntimeEventInfo()
		{
		}

		// Token: 0x0400304E RID: 12366
		private IntPtr klass;

		// Token: 0x0400304F RID: 12367
		private IntPtr handle;
	}
}

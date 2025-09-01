using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020008F6 RID: 2294
	[Serializable]
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeFieldInfo : RtFieldInfo, ISerializable
	{
		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06004D18 RID: 19736 RVA: 0x0000DDA9 File Offset: 0x0000BFA9
		internal BindingFlags BindingFlags
		{
			get
			{
				return BindingFlags.Default;
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06004D19 RID: 19737 RVA: 0x000F3C61 File Offset: 0x000F1E61
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004D1A RID: 19738 RVA: 0x000F39D2 File Offset: 0x000F1BD2
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return (RuntimeType)this.DeclaringType;
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06004D1B RID: 19739 RVA: 0x000F39DF File Offset: 0x000F1BDF
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return (RuntimeType)this.ReflectedType;
			}
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x000F3C69 File Offset: 0x000F1E69
		internal RuntimeModule GetRuntimeModule()
		{
			return this.GetDeclaringTypeInternal().GetRuntimeModule();
		}

		// Token: 0x06004D1D RID: 19741 RVA: 0x000F3C76 File Offset: 0x000F1E76
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			MemberInfoSerializationHolder.GetSerializationInfo(info, this.Name, this.ReflectedTypeInternal, this.ToString(), MemberTypes.Field);
		}

		// Token: 0x06004D1E RID: 19742
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern object UnsafeGetValue(object obj);

		// Token: 0x06004D1F RID: 19743 RVA: 0x000F3CA0 File Offset: 0x000F1EA0
		internal override void CheckConsistency(object target)
		{
			if ((this.Attributes & FieldAttributes.Static) == FieldAttributes.Static || this.DeclaringType.IsInstanceOfType(target))
			{
				return;
			}
			if (target == null)
			{
				throw new TargetException(Environment.GetResourceString("Non-static field requires a target."));
			}
			throw new ArgumentException(string.Format(CultureInfo.CurrentUICulture, Environment.GetResourceString("Field '{0}' defined on type '{1}' is not a field on the target object which is of type '{2}'."), this.Name, this.DeclaringType, target.GetType()));
		}

		// Token: 0x06004D20 RID: 19744 RVA: 0x000F3D08 File Offset: 0x000F1F08
		[DebuggerStepThrough]
		[DebuggerHidden]
		internal override void UnsafeSetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			bool flag = false;
			RuntimeFieldHandle.SetValue(this, obj, value, null, this.Attributes, null, ref flag);
		}

		// Token: 0x06004D21 RID: 19745 RVA: 0x000F3D29 File Offset: 0x000F1F29
		[DebuggerHidden]
		[DebuggerStepThrough]
		public unsafe override void SetValueDirect(TypedReference obj, object value)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("The TypedReference must be initialized."));
			}
			RuntimeFieldHandle.SetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), value, (RuntimeType)this.DeclaringType);
		}

		// Token: 0x06004D22 RID: 19746 RVA: 0x000F3D64 File Offset: 0x000F1F64
		[DebuggerStepThrough]
		[DebuggerHidden]
		public unsafe override object GetValueDirect(TypedReference obj)
		{
			if (obj.IsNull)
			{
				throw new ArgumentException(Environment.GetResourceString("The TypedReference must be initialized."));
			}
			return RuntimeFieldHandle.GetValueDirect(this, (RuntimeType)this.FieldType, (void*)(&obj), (RuntimeType)this.DeclaringType);
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06004D23 RID: 19747 RVA: 0x000F3D9E File Offset: 0x000F1F9E
		public override FieldAttributes Attributes
		{
			get
			{
				return this.attrs;
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06004D24 RID: 19748 RVA: 0x000F3DA6 File Offset: 0x000F1FA6
		public override RuntimeFieldHandle FieldHandle
		{
			get
			{
				return this.fhandle;
			}
		}

		// Token: 0x06004D25 RID: 19749
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type ResolveType();

		// Token: 0x17000C87 RID: 3207
		// (get) Token: 0x06004D26 RID: 19750 RVA: 0x000F3DAE File Offset: 0x000F1FAE
		public override Type FieldType
		{
			get
			{
				if (this.type == null)
				{
					this.type = this.ResolveType();
				}
				return this.type;
			}
		}

		// Token: 0x06004D27 RID: 19751
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type GetParentType(bool declaring);

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06004D28 RID: 19752 RVA: 0x000F3DD0 File Offset: 0x000F1FD0
		public override Type ReflectedType
		{
			get
			{
				return this.GetParentType(false);
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06004D29 RID: 19753 RVA: 0x000F3DD9 File Offset: 0x000F1FD9
		public override Type DeclaringType
		{
			get
			{
				return this.GetParentType(true);
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06004D2A RID: 19754 RVA: 0x000F3DE2 File Offset: 0x000F1FE2
		public override string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x00052A66 File Offset: 0x00050C66
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x000F1905 File Offset: 0x000EFB05
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x000F190E File Offset: 0x000EFB0E
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06004D2E RID: 19758
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal override extern int GetFieldOffset();

		// Token: 0x06004D2F RID: 19759
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern object GetValueInternal(object obj);

		// Token: 0x06004D30 RID: 19760 RVA: 0x000F3DEC File Offset: 0x000F1FEC
		public override object GetValue(object obj)
		{
			if (!base.IsStatic)
			{
				if (obj == null)
				{
					throw new TargetException("Non-static field requires a target");
				}
				if (!this.DeclaringType.IsAssignableFrom(obj.GetType()))
				{
					throw new ArgumentException(string.Format("Field {0} defined on type {1} is not a field on the target object which is of type {2}.", this.Name, this.DeclaringType, obj.GetType()), "obj");
				}
			}
			if (!base.IsLiteral)
			{
				this.CheckGeneric();
			}
			return this.GetValueInternal(obj);
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x000F3E5E File Offset: 0x000F205E
		public override string ToString()
		{
			return string.Format("{0} {1}", this.FieldType, this.name);
		}

		// Token: 0x06004D32 RID: 19762
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void SetValueInternal(FieldInfo fi, object obj, object value);

		// Token: 0x06004D33 RID: 19763 RVA: 0x000F3E78 File Offset: 0x000F2078
		public override void SetValue(object obj, object val, BindingFlags invokeAttr, Binder binder, CultureInfo culture)
		{
			if (!base.IsStatic)
			{
				if (obj == null)
				{
					throw new TargetException("Non-static field requires a target");
				}
				if (!this.DeclaringType.IsAssignableFrom(obj.GetType()))
				{
					throw new ArgumentException(string.Format("Field {0} defined on type {1} is not a field on the target object which is of type {2}.", this.Name, this.DeclaringType, obj.GetType()), "obj");
				}
			}
			if (base.IsLiteral)
			{
				throw new FieldAccessException("Cannot set a constant field");
			}
			if (binder == null)
			{
				binder = Type.DefaultBinder;
			}
			this.CheckGeneric();
			if (val != null)
			{
				val = ((RuntimeType)this.FieldType).CheckValue(val, binder, culture, invokeAttr);
			}
			RuntimeFieldInfo.SetValueInternal(this, obj, val);
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x000F3F1C File Offset: 0x000F211C
		internal RuntimeFieldInfo Clone(string newName)
		{
			return new RuntimeFieldInfo
			{
				name = newName,
				type = this.type,
				attrs = this.attrs,
				klass = this.klass,
				fhandle = this.fhandle
			};
		}

		// Token: 0x06004D35 RID: 19765
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern object GetRawConstantValue();

		// Token: 0x06004D36 RID: 19766 RVA: 0x000F3C38 File Offset: 0x000F1E38
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributes(this);
		}

		// Token: 0x06004D37 RID: 19767 RVA: 0x000F3F5A File Offset: 0x000F215A
		private void CheckGeneric()
		{
			if (this.DeclaringType.ContainsGenericParameters)
			{
				throw new InvalidOperationException("Late bound operations cannot be performed on fields with types for which Type.ContainsGenericParameters is true.");
			}
		}

		// Token: 0x06004D38 RID: 19768
		[MethodImpl(MethodImplOptions.InternalCall)]
		public extern int get_core_clr_security_level();

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06004D39 RID: 19769 RVA: 0x000F3F74 File Offset: 0x000F2174
		public override bool IsSecurityTransparent
		{
			get
			{
				return this.get_core_clr_security_level() == 0;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x06004D3A RID: 19770 RVA: 0x000F3F7F File Offset: 0x000F217F
		public override bool IsSecurityCritical
		{
			get
			{
				return this.get_core_clr_security_level() > 0;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x06004D3B RID: 19771 RVA: 0x000F3F8A File Offset: 0x000F218A
		public override bool IsSecuritySafeCritical
		{
			get
			{
				return this.get_core_clr_security_level() == 1;
			}
		}

		// Token: 0x06004D3C RID: 19772 RVA: 0x000F3F95 File Offset: 0x000F2195
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeFieldInfo>(other);
		}

		// Token: 0x17000C8E RID: 3214
		// (get) Token: 0x06004D3D RID: 19773 RVA: 0x000F3F9E File Offset: 0x000F219E
		public override int MetadataToken
		{
			get
			{
				return RuntimeFieldInfo.get_metadata_token(this);
			}
		}

		// Token: 0x06004D3E RID: 19774
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int get_metadata_token(RuntimeFieldInfo monoField);

		// Token: 0x06004D3F RID: 19775
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern Type[] GetTypeModifiers(bool optional);

		// Token: 0x06004D40 RID: 19776 RVA: 0x000F3FA6 File Offset: 0x000F21A6
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.GetCustomModifiers(true);
		}

		// Token: 0x06004D41 RID: 19777 RVA: 0x000F3FAF File Offset: 0x000F21AF
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.GetCustomModifiers(false);
		}

		// Token: 0x06004D42 RID: 19778 RVA: 0x000F3FB8 File Offset: 0x000F21B8
		private Type[] GetCustomModifiers(bool optional)
		{
			return this.GetTypeModifiers(optional) ?? Type.EmptyTypes;
		}

		// Token: 0x06004D43 RID: 19779 RVA: 0x000F3FCA File Offset: 0x000F21CA
		public RuntimeFieldInfo()
		{
		}

		// Token: 0x04003050 RID: 12368
		internal IntPtr klass;

		// Token: 0x04003051 RID: 12369
		internal RuntimeFieldHandle fhandle;

		// Token: 0x04003052 RID: 12370
		private string name;

		// Token: 0x04003053 RID: 12371
		private Type type;

		// Token: 0x04003054 RID: 12372
		private FieldAttributes attrs;
	}
}

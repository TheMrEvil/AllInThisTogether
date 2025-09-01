using System;
using System.Globalization;
using System.Reflection;
using QFSW.QC.Utilities;

namespace QFSW.QC.Internal
{
	// Token: 0x02000066 RID: 102
	internal class FieldDelegateMethod : FieldMethod
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00009FB8 File Offset: 0x000081B8
		public FieldDelegateMethod(FieldInfo fieldInfo) : base(fieldInfo)
		{
			if (!this._fieldInfo.IsStrongDelegate())
			{
				throw new ArgumentException("Invalid delegate type.", "fieldInfo");
			}
			if (this._fieldInfo.IsStatic)
			{
				this._internalDelegate = new Func<FieldInfo, object[], object>(FieldDelegateMethod.StaticInvoker);
			}
			else
			{
				this._internalDelegate = new Func<object, FieldInfo, object[], object>(this.NonStaticInvoker);
			}
			this._parameters = this._fieldInfo.FieldType.GetMethod("Invoke").GetParameters();
			for (int i = 0; i < this._parameters.Length; i++)
			{
				this._parameters[i] = new CustomParameter(this._parameters[i], string.Format("arg{0}", i));
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A074 File Offset: 0x00008274
		public override object Invoke(object obj, BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture)
		{
			object[] array = new object[this._internalDelegate.Method.GetParameters().Length];
			if (array.Length < 2)
			{
				throw new Exception("FieldDelegateMethod's internal delegate must contain at least two paramaters.");
			}
			if (!base.IsStatic)
			{
				array[0] = obj;
			}
			array[array.Length - 2] = this._fieldInfo;
			array[array.Length - 1] = parameters;
			return this._internalDelegate.DynamicInvoke(array);
		}

		// Token: 0x0600021E RID: 542 RVA: 0x0000A0D8 File Offset: 0x000082D8
		private static object StaticInvoker(FieldInfo field, params object[] args)
		{
			Delegate @delegate = (Delegate)field.GetValue(null);
			if (@delegate != null)
			{
				return @delegate.DynamicInvoke(args);
			}
			throw new Exception("Delegate was invalid and could not be invoked.");
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000A108 File Offset: 0x00008308
		private object NonStaticInvoker(object obj, FieldInfo field, params object[] args)
		{
			Delegate @delegate = (Delegate)field.GetValue(obj);
			if (@delegate != null)
			{
				return @delegate.DynamicInvoke(args);
			}
			throw new Exception("Delegate was invalid and could not be invoked.");
		}
	}
}

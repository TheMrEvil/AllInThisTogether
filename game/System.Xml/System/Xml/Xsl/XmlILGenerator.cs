using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using System.Xml.XPath;
using System.Xml.Xsl.IlGen;
using System.Xml.Xsl.Qil;
using System.Xml.Xsl.Runtime;

namespace System.Xml.Xsl
{
	// Token: 0x02000334 RID: 820
	internal class XmlILGenerator
	{
		// Token: 0x060021AE RID: 8622 RVA: 0x0000216B File Offset: 0x0000036B
		public XmlILGenerator()
		{
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x000D60D0 File Offset: 0x000D42D0
		public XmlILCommand Generate(QilExpression query, TypeBuilder typeBldr)
		{
			this.qil = query;
			bool useLRE = !this.qil.IsDebug && typeBldr == null;
			bool isDebug = this.qil.IsDebug;
			this.optVisitor = new XmlILOptimizerVisitor(this.qil, !this.qil.IsDebug);
			this.qil = this.optVisitor.Optimize();
			XmlILModule.CreateModulePermissionSet.Assert();
			if (typeBldr != null)
			{
				this.module = new XmlILModule(typeBldr);
			}
			else
			{
				this.module = new XmlILModule(useLRE, isDebug);
			}
			this.helper = new GenerateHelper(this.module, this.qil.IsDebug);
			this.CreateHelperFunctions();
			MethodInfo methExec = this.module.DefineMethod("Execute", typeof(void), new Type[0], new string[0], XmlILMethodAttributes.NonUser);
			XmlILMethodAttributes xmlAttrs = (this.qil.Root.SourceLine == null) ? XmlILMethodAttributes.NonUser : XmlILMethodAttributes.None;
			MethodInfo methRoot = this.module.DefineMethod("Root", typeof(void), new Type[0], new string[0], xmlAttrs);
			foreach (EarlyBoundInfo earlyBoundInfo in this.qil.EarlyBoundTypes)
			{
				this.helper.StaticData.DeclareEarlyBound(earlyBoundInfo.NamespaceUri, earlyBoundInfo.EarlyBoundType);
			}
			this.CreateFunctionMetadata(this.qil.FunctionList);
			this.CreateGlobalValueMetadata(this.qil.GlobalVariableList);
			this.CreateGlobalValueMetadata(this.qil.GlobalParameterList);
			this.GenerateExecuteFunction(methExec, methRoot);
			this.xmlIlVisitor = new XmlILVisitor();
			this.xmlIlVisitor.Visit(this.qil, this.helper, methRoot);
			XmlQueryStaticData staticData = new XmlQueryStaticData(this.qil.DefaultWriterSettings, this.qil.WhitespaceRules, this.helper.StaticData);
			if (typeBldr != null)
			{
				this.CreateTypeInitializer(staticData);
				this.module.BakeMethods();
				return null;
			}
			this.module.BakeMethods();
			return new XmlILCommand((ExecuteDelegate)this.module.CreateDelegate("Execute", typeof(ExecuteDelegate)), staticData);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x000D632C File Offset: 0x000D452C
		private void CreateFunctionMetadata(IList<QilNode> funcList)
		{
			foreach (QilNode qilNode in funcList)
			{
				QilFunction qilFunction = (QilFunction)qilNode;
				Type[] array = new Type[qilFunction.Arguments.Count];
				string[] array2 = new string[qilFunction.Arguments.Count];
				for (int i = 0; i < qilFunction.Arguments.Count; i++)
				{
					QilParameter qilParameter = (QilParameter)qilFunction.Arguments[i];
					array[i] = XmlILTypeHelper.GetStorageType(qilParameter.XmlType);
					if (qilParameter.DebugName != null)
					{
						array2[i] = qilParameter.DebugName;
					}
				}
				Type returnType;
				if (XmlILConstructInfo.Read(qilFunction).PushToWriterLast)
				{
					returnType = typeof(void);
				}
				else
				{
					returnType = XmlILTypeHelper.GetStorageType(qilFunction.XmlType);
				}
				XmlILMethodAttributes xmlAttrs = (qilFunction.SourceLine == null) ? XmlILMethodAttributes.NonUser : XmlILMethodAttributes.None;
				MethodInfo functionBinding = this.module.DefineMethod(qilFunction.DebugName, returnType, array, array2, xmlAttrs);
				for (int j = 0; j < qilFunction.Arguments.Count; j++)
				{
					XmlILAnnotation.Write(qilFunction.Arguments[j]).ArgumentPosition = j;
				}
				XmlILAnnotation.Write(qilFunction).FunctionBinding = functionBinding;
			}
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x000D6494 File Offset: 0x000D4694
		private void CreateGlobalValueMetadata(IList<QilNode> globalList)
		{
			foreach (QilNode qilNode in globalList)
			{
				QilReference qilReference = (QilReference)qilNode;
				Type storageType = XmlILTypeHelper.GetStorageType(qilReference.XmlType);
				XmlILMethodAttributes xmlAttrs = (qilReference.SourceLine == null) ? XmlILMethodAttributes.NonUser : XmlILMethodAttributes.None;
				MethodInfo functionBinding = this.module.DefineMethod(qilReference.DebugName.ToString(), storageType, new Type[0], new string[0], xmlAttrs);
				XmlILAnnotation.Write(qilReference).FunctionBinding = functionBinding;
			}
		}

		// Token: 0x060021B2 RID: 8626 RVA: 0x000D652C File Offset: 0x000D472C
		private MethodInfo GenerateExecuteFunction(MethodInfo methExec, MethodInfo methRoot)
		{
			this.helper.MethodBegin(methExec, null, false);
			this.EvaluateGlobalValues(this.qil.GlobalVariableList);
			this.EvaluateGlobalValues(this.qil.GlobalParameterList);
			this.helper.LoadQueryRuntime();
			this.helper.Call(methRoot);
			this.helper.MethodEnd();
			return methExec;
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x000D658C File Offset: 0x000D478C
		private void CreateHelperFunctions()
		{
			MethodInfo methInfo = this.module.DefineMethod("SyncToNavigator", typeof(XPathNavigator), new Type[]
			{
				typeof(XPathNavigator),
				typeof(XPathNavigator)
			}, new string[2], (XmlILMethodAttributes)3);
			this.helper.MethodBegin(methInfo, null, false);
			Label label = this.helper.DefineLabel();
			this.helper.Emit(OpCodes.Ldarg_0);
			this.helper.Emit(OpCodes.Brfalse, label);
			this.helper.Emit(OpCodes.Ldarg_0);
			this.helper.Emit(OpCodes.Ldarg_1);
			this.helper.Call(XmlILMethods.NavMoveTo);
			this.helper.Emit(OpCodes.Brfalse, label);
			this.helper.Emit(OpCodes.Ldarg_0);
			this.helper.Emit(OpCodes.Ret);
			this.helper.MarkLabel(label);
			this.helper.Emit(OpCodes.Ldarg_1);
			this.helper.Call(XmlILMethods.NavClone);
			this.helper.MethodEnd();
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x000D66B0 File Offset: 0x000D48B0
		private void EvaluateGlobalValues(IList<QilNode> iterList)
		{
			foreach (QilNode qilNode in iterList)
			{
				QilIterator nd = (QilIterator)qilNode;
				if (this.qil.IsDebug || OptimizerPatterns.Read(nd).MatchesPattern(OptimizerPatternName.MaybeSideEffects))
				{
					MethodInfo functionBinding = XmlILAnnotation.Write(nd).FunctionBinding;
					this.helper.LoadQueryRuntime();
					this.helper.Call(functionBinding);
					this.helper.Emit(OpCodes.Pop);
				}
			}
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x000D6748 File Offset: 0x000D4948
		public void CreateTypeInitializer(XmlQueryStaticData staticData)
		{
			byte[] array;
			Type[] array2;
			staticData.GetObjectData(out array, out array2);
			FieldInfo fldInfo = this.module.DefineInitializedData("__staticData", array);
			FieldInfo fldInfo2 = this.module.DefineField("staticData", typeof(object));
			FieldInfo fldInfo3 = this.module.DefineField("ebTypes", typeof(Type[]));
			ConstructorInfo methInfo = this.module.DefineTypeInitializer();
			this.helper.MethodBegin(methInfo, null, false);
			this.helper.LoadInteger(array.Length);
			this.helper.Emit(OpCodes.Newarr, typeof(byte));
			this.helper.Emit(OpCodes.Dup);
			this.helper.Emit(OpCodes.Ldtoken, fldInfo);
			this.helper.Call(XmlILMethods.InitializeArray);
			this.helper.Emit(OpCodes.Stsfld, fldInfo2);
			if (array2 != null)
			{
				LocalBuilder locBldr = this.helper.DeclareLocal("$$$types", typeof(Type[]));
				this.helper.LoadInteger(array2.Length);
				this.helper.Emit(OpCodes.Newarr, typeof(Type));
				this.helper.Emit(OpCodes.Stloc, locBldr);
				for (int i = 0; i < array2.Length; i++)
				{
					this.helper.Emit(OpCodes.Ldloc, locBldr);
					this.helper.LoadInteger(i);
					this.helper.LoadType(array2[i]);
					this.helper.Emit(OpCodes.Stelem_Ref);
				}
				this.helper.Emit(OpCodes.Ldloc, locBldr);
				this.helper.Emit(OpCodes.Stsfld, fldInfo3);
			}
			this.helper.MethodEnd();
		}

		// Token: 0x04001BB3 RID: 7091
		private QilExpression qil;

		// Token: 0x04001BB4 RID: 7092
		private GenerateHelper helper;

		// Token: 0x04001BB5 RID: 7093
		private XmlILOptimizerVisitor optVisitor;

		// Token: 0x04001BB6 RID: 7094
		private XmlILVisitor xmlIlVisitor;

		// Token: 0x04001BB7 RID: 7095
		private XmlILModule module;
	}
}

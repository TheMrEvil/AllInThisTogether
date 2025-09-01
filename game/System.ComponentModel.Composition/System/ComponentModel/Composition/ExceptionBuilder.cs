using System;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	// Token: 0x02000034 RID: 52
	internal static class ExceptionBuilder
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00005739 File Offset: 0x00003939
		public static Exception CreateDiscoveryException(string messageFormat, params string[] arguments)
		{
			return new InvalidOperationException(ExceptionBuilder.Format(messageFormat, arguments));
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x00005747 File Offset: 0x00003947
		public static ArgumentException CreateContainsNullElement(string parameterName)
		{
			Assumes.NotNull<string>(parameterName);
			return new ArgumentException(ExceptionBuilder.Format(Strings.Argument_NullElement, new string[]
			{
				parameterName
			}), parameterName);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00005769 File Offset: 0x00003969
		public static ObjectDisposedException CreateObjectDisposed(object instance)
		{
			Assumes.NotNull<object>(instance);
			return new ObjectDisposedException(instance.GetType().ToString());
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x00005781 File Offset: 0x00003981
		public static NotImplementedException CreateNotOverriddenByDerived(string memberName)
		{
			Assumes.NotNullOrEmpty(memberName);
			return new NotImplementedException(ExceptionBuilder.Format(Strings.NotImplemented_NotOverriddenByDerived, new string[]
			{
				memberName
			}));
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000057A2 File Offset: 0x000039A2
		public static ArgumentException CreateExportDefinitionNotOnThisComposablePart(string parameterName)
		{
			Assumes.NotNullOrEmpty(parameterName);
			return new ArgumentException(ExceptionBuilder.Format(Strings.ExportDefinitionNotOnThisComposablePart, new string[]
			{
				parameterName
			}), parameterName);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000057C4 File Offset: 0x000039C4
		public static ArgumentException CreateImportDefinitionNotOnThisComposablePart(string parameterName)
		{
			Assumes.NotNullOrEmpty(parameterName);
			return new ArgumentException(ExceptionBuilder.Format(Strings.ImportDefinitionNotOnThisComposablePart, new string[]
			{
				parameterName
			}), parameterName);
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000057E6 File Offset: 0x000039E6
		public static CompositionException CreateCannotGetExportedValue(ComposablePart part, ExportDefinition definition, Exception innerException)
		{
			Assumes.NotNull<ComposablePart, ExportDefinition, Exception>(part, definition, innerException);
			return new CompositionException(ErrorBuilder.CreateCannotGetExportedValue(part, definition, innerException));
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000057FD File Offset: 0x000039FD
		public static ArgumentException CreateReflectionModelInvalidPartDefinition(string parameterName, Type partDefinitionType)
		{
			Assumes.NotNullOrEmpty(parameterName);
			Assumes.NotNull<Type>(partDefinitionType);
			return new ArgumentException(string.Format(CultureInfo.CurrentCulture, Strings.ReflectionModel_InvalidPartDefinition, partDefinitionType), parameterName);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00005821 File Offset: 0x00003A21
		public static ArgumentException ExportFactory_TooManyGenericParameters(string typeName)
		{
			Assumes.NotNullOrEmpty(typeName);
			return new ArgumentException(ExceptionBuilder.Format(Strings.ExportFactory_TooManyGenericParameters, new string[]
			{
				typeName
			}), typeName);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00005844 File Offset: 0x00003A44
		private static string Format(string format, params string[] arguments)
		{
			return string.Format(CultureInfo.CurrentCulture, format, arguments);
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Linq.Expressions;
using System.Reflection;
using Microsoft.Internal;

namespace System.ComponentModel.Composition
{
	// Token: 0x02000030 RID: 48
	internal static class ConstraintServices
	{
		// Token: 0x0600017A RID: 378 RVA: 0x00004C80 File Offset: 0x00002E80
		public static Expression<Func<ExportDefinition, bool>> CreateConstraint(string contractName, string requiredTypeIdentity, IEnumerable<KeyValuePair<string, Type>> requiredMetadata, CreationPolicy requiredCreationPolicy)
		{
			ParameterExpression parameterExpression = Expression.Parameter(typeof(ExportDefinition), "exportDefinition");
			Expression expression = ConstraintServices.CreateContractConstraintBody(contractName, parameterExpression);
			if (!string.IsNullOrEmpty(requiredTypeIdentity))
			{
				Expression right = ConstraintServices.CreateTypeIdentityContraint(requiredTypeIdentity, parameterExpression);
				expression = Expression.AndAlso(expression, right);
			}
			if (requiredMetadata != null)
			{
				Expression expression2 = ConstraintServices.CreateMetadataConstraintBody(requiredMetadata, parameterExpression);
				if (expression2 != null)
				{
					expression = Expression.AndAlso(expression, expression2);
				}
			}
			if (requiredCreationPolicy != CreationPolicy.Any)
			{
				Expression right2 = ConstraintServices.CreateCreationPolicyContraint(requiredCreationPolicy, parameterExpression);
				expression = Expression.AndAlso(expression, right2);
			}
			return Expression.Lambda<Func<ExportDefinition, bool>>(expression, new ParameterExpression[]
			{
				parameterExpression
			});
		}

		// Token: 0x0600017B RID: 379 RVA: 0x00004CFD File Offset: 0x00002EFD
		private static Expression CreateContractConstraintBody(string contractName, ParameterExpression parameter)
		{
			Assumes.NotNull<ParameterExpression>(parameter);
			return Expression.Equal(Expression.Property(parameter, ConstraintServices._exportDefinitionContractNameProperty), Expression.Constant(contractName ?? string.Empty, typeof(string)));
		}

		// Token: 0x0600017C RID: 380 RVA: 0x00004D30 File Offset: 0x00002F30
		private static Expression CreateMetadataConstraintBody(IEnumerable<KeyValuePair<string, Type>> requiredMetadata, ParameterExpression parameter)
		{
			Assumes.NotNull<IEnumerable<KeyValuePair<string, Type>>>(requiredMetadata);
			Assumes.NotNull<ParameterExpression>(parameter);
			Expression expression = null;
			foreach (KeyValuePair<string, Type> keyValuePair in requiredMetadata)
			{
				Expression expression2 = ConstraintServices.CreateMetadataContainsKeyExpression(parameter, keyValuePair.Key);
				expression = ((expression != null) ? Expression.AndAlso(expression, expression2) : expression2);
				expression = Expression.AndAlso(expression, ConstraintServices.CreateMetadataOfTypeExpression(parameter, keyValuePair.Key, keyValuePair.Value));
			}
			return expression;
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00004DB8 File Offset: 0x00002FB8
		private static Expression CreateCreationPolicyContraint(CreationPolicy policy, ParameterExpression parameter)
		{
			Assumes.IsTrue(policy > CreationPolicy.Any);
			Assumes.NotNull<ParameterExpression>(parameter);
			return Expression.MakeBinary(ExpressionType.OrElse, Expression.MakeBinary(ExpressionType.OrElse, Expression.Not(ConstraintServices.CreateMetadataContainsKeyExpression(parameter, "System.ComponentModel.Composition.CreationPolicy")), ConstraintServices.CreateMetadataValueEqualsExpression(parameter, CreationPolicy.Any, "System.ComponentModel.Composition.CreationPolicy")), ConstraintServices.CreateMetadataValueEqualsExpression(parameter, policy, "System.ComponentModel.Composition.CreationPolicy"));
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00004E14 File Offset: 0x00003014
		private static Expression CreateTypeIdentityContraint(string requiredTypeIdentity, ParameterExpression parameter)
		{
			Assumes.NotNull<string>(requiredTypeIdentity);
			Assumes.NotNull<ParameterExpression>(parameter);
			return Expression.MakeBinary(ExpressionType.AndAlso, ConstraintServices.CreateMetadataContainsKeyExpression(parameter, "ExportTypeIdentity"), ConstraintServices.CreateMetadataValueEqualsExpression(parameter, requiredTypeIdentity, "ExportTypeIdentity"));
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00004E3F File Offset: 0x0000303F
		private static Expression CreateMetadataContainsKeyExpression(ParameterExpression parameter, string constantKey)
		{
			Assumes.NotNull<ParameterExpression, string>(parameter, constantKey);
			return Expression.Call(Expression.Property(parameter, ConstraintServices._exportDefinitionMetadataProperty), ConstraintServices._metadataContainsKeyMethod, new Expression[]
			{
				Expression.Constant(constantKey)
			});
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00004E6C File Offset: 0x0000306C
		private static Expression CreateMetadataOfTypeExpression(ParameterExpression parameter, string constantKey, Type constantType)
		{
			Assumes.NotNull<ParameterExpression, string>(parameter, constantKey);
			Assumes.NotNull<ParameterExpression, Type>(parameter, constantType);
			return Expression.Call(Expression.Constant(constantType, typeof(Type)), ConstraintServices._typeIsInstanceOfTypeMethod, new Expression[]
			{
				Expression.Call(Expression.Property(parameter, ConstraintServices._exportDefinitionMetadataProperty), ConstraintServices._metadataItemMethod, new Expression[]
				{
					Expression.Constant(constantKey)
				})
			});
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00004ED0 File Offset: 0x000030D0
		private static Expression CreateMetadataValueEqualsExpression(ParameterExpression parameter, object constantValue, string metadataName)
		{
			Assumes.NotNull<ParameterExpression, object>(parameter, constantValue);
			return Expression.Call(Expression.Constant(constantValue), ConstraintServices._metadataEqualsMethod, new Expression[]
			{
				Expression.Call(Expression.Property(parameter, ConstraintServices._exportDefinitionMetadataProperty), ConstraintServices._metadataItemMethod, new Expression[]
				{
					Expression.Constant(metadataName)
				})
			});
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00004F24 File Offset: 0x00003124
		public static Expression<Func<ExportDefinition, bool>> CreatePartCreatorConstraint(Expression<Func<ExportDefinition, bool>> baseConstraint, ImportDefinition productImportDefinition)
		{
			ParameterExpression parameterExpression = baseConstraint.Parameters[0];
			Expression instance = Expression.Property(parameterExpression, ConstraintServices._exportDefinitionMetadataProperty);
			Expression left = Expression.Call(instance, ConstraintServices._metadataContainsKeyMethod, new Expression[]
			{
				Expression.Constant("ProductDefinition")
			});
			Expression expression = Expression.Call(instance, ConstraintServices._metadataItemMethod, new Expression[]
			{
				Expression.Constant("ProductDefinition")
			});
			Expression right = Expression.Invoke(productImportDefinition.Constraint, new Expression[]
			{
				Expression.Convert(expression, typeof(ExportDefinition))
			});
			return Expression.Lambda<Func<ExportDefinition, bool>>(Expression.AndAlso(baseConstraint.Body, Expression.AndAlso(left, right)), new ParameterExpression[]
			{
				parameterExpression
			});
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00004FD0 File Offset: 0x000031D0
		// Note: this type is marked as 'beforefieldinit'.
		static ConstraintServices()
		{
		}

		// Token: 0x04000094 RID: 148
		private static readonly PropertyInfo _exportDefinitionContractNameProperty = typeof(ExportDefinition).GetProperty("ContractName");

		// Token: 0x04000095 RID: 149
		private static readonly PropertyInfo _exportDefinitionMetadataProperty = typeof(ExportDefinition).GetProperty("Metadata");

		// Token: 0x04000096 RID: 150
		private static readonly MethodInfo _metadataContainsKeyMethod = typeof(IDictionary<string, object>).GetMethod("ContainsKey");

		// Token: 0x04000097 RID: 151
		private static readonly MethodInfo _metadataItemMethod = typeof(IDictionary<string, object>).GetMethod("get_Item");

		// Token: 0x04000098 RID: 152
		private static readonly MethodInfo _metadataEqualsMethod = typeof(object).GetMethod("Equals", new Type[]
		{
			typeof(object)
		});

		// Token: 0x04000099 RID: 153
		private static readonly MethodInfo _typeIsInstanceOfTypeMethod = typeof(Type).GetMethod("IsInstanceOfType");
	}
}

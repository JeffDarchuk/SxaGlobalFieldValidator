using System;
using System.Collections.Generic;
using System.Linq;
using Sitecore.Abstractions;
using Sitecore.CodeDom.Scripts;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Validators;
using Sitecore.XA.Foundation.Multisite;

namespace JeffDarchuk.Foundation.ContentValidation
{
	public class GlobalFieldValidatorManager : DefaultValidatorManager
	{
		private readonly IMultisiteContext _multisiteContext;
		private readonly BaseTemplateManager _templateManager;

		public GlobalFieldValidatorManager(BaseItemScripts itemScripts, IMultisiteContext multisiteContext, BaseTemplateManager templateManager) : base(itemScripts)
		{
			_multisiteContext = multisiteContext ?? throw new ArgumentNullException(nameof(multisiteContext));
			_templateManager = templateManager ?? throw new ArgumentNullException(nameof(templateManager));
		}

		public override ValidatorCollection BuildValidators(ValidatorsMode mode, Item item)
		{
			var validators = base.BuildValidators(mode, item);
			var globalFieldRulesFolder = GetGlobalFieldRulesFolder(item);
			if (globalFieldRulesFolder == null) return validators;
			foreach (var validator in GetAdditionalValidators(item, globalFieldRulesFolder, mode))
			{
				validators.Add(validator);
			}
			return validators;
		}

		private Item GetGlobalFieldRulesFolder(Item item)
		{
			return _multisiteContext.GetSettingsItem(item)?.Children.FirstOrDefault(x =>
				x.TemplateID.ToString() == Templates.GlobalFieldRuleFolder.Id);
		}

		private IEnumerable<BaseValidator> GetAdditionalValidators(Item item, Item globalFieldRulesFolder, ValidatorsMode mode)
		{
			var baseTemplates = new HashSet<string>(_templateManager.GetTemplate(item).GetBaseTemplates().Select(x => x.ID.ToString()));
			foreach (var globalFieldRule in GetGlobalFieldRules(globalFieldRulesFolder))
			{
				var template = globalFieldRule[Templates.GlobalFieldRule.Fields.Template];
				if (!FieldRuleAppliesToItem(item, globalFieldRule, template, baseTemplates)) continue;
				MultilistField validators = null;
				switch (mode)
				{
					case ValidatorsMode.Gutter:
						validators = globalFieldRule.Fields[Templates.FieldTypeValidationRules.Fields.QuickValidationBar];
						break;
					case ValidatorsMode.ValidateButton:
						validators = globalFieldRule.Fields[Templates.FieldTypeValidationRules.Fields.ValidateButton];
						break;
					case ValidatorsMode.ValidatorBar:
						validators = globalFieldRule.Fields[Templates.FieldTypeValidationRules.Fields.ValidatorBar];
						break;
					case ValidatorsMode.Workflow:
						validators = globalFieldRule.Fields[Templates.FieldTypeValidationRules.Fields.Workflow];
						break;
				}
				foreach (var validator in validators?.GetItems() ?? Enumerable.Empty<Item>())
				{
					var baseValidator = BuildValidator(validator, item);
					baseValidator.FieldID = item.Fields[globalFieldRule[Templates.GlobalFieldRule.Fields.Field]].ID;
					yield return baseValidator;
				}
			}
		}

		private IEnumerable<Item> GetGlobalFieldRules(Item globalFieldRulesFolder)
		{
			return globalFieldRulesFolder.Axes.GetDescendants().Where(x => x.TemplateID.ToString() == Templates.GlobalFieldRule.Id);
		}

		private bool FieldRuleAppliesToItem(Item item, Item globalFieldRule, string template, HashSet<string> baseTemplates)
		{
			var useInheritedTemplates = ((CheckboxField)globalFieldRule.Fields[Templates.GlobalFieldRule.Fields.ApplyToInheritedTemplates]).Checked;
			return item.TemplateID.ToString() == template || useInheritedTemplates && baseTemplates.Contains(template);
		}
	}
}
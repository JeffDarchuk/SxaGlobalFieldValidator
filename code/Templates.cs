namespace JeffDarchuk.Foundation.ContentValidation
{
	public struct Templates
	{
		public struct GlobalFieldRuleFolder
		{
			public const string Id = "{02A56CC1-7A24-4292-B3EF-628B60E62518}";
		}

		public struct GlobalFieldRule
		{
			public const string Id = "{C683968B-5CA9-4C40-8C09-4C3807809EC4}";

			public struct Fields
			{
				public const string Template = "{2340DD5A-205F-49A6-9F72-0C9E28B23440}";
				public const string Field = "{A8804AAB-6022-485A-A802-4606DB6C9545}";
				public const string ApplyToInheritedTemplates = "{F9C8F454-CEDD-459B-8F5C-6F7B64DFA0FF}";
			}
		}

		public struct FieldTypeValidationRules
		{
			public struct Fields
			{
				public const string QuickValidationBar = "{08D10FD3-F3FE-4BC3-B351-7E41D4A70381}";
				public const string ValidateButton = "{8E33FBE0-6412-42F8-A445-6A61E0D5660B}";
				public const string ValidatorBar = "{0B446E61-8BF3-4616-AF42-8A4017B3C886}";
				public const string Workflow = "{BAE1F484-EFFA-4F4D-910E-0180FB8B53C5}";
			}
		}
	}
}
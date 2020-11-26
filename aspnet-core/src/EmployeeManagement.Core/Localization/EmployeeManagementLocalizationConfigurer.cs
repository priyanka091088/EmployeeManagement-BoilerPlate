using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace EmployeeManagement.Localization
{
    public static class EmployeeManagementLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(EmployeeManagementConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(EmployeeManagementLocalizationConfigurer).GetAssembly(),
                        "EmployeeManagement.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}

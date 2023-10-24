using CruisePackage;
using CruisePackage.Common;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace BlazorServerVanillaCruisePackage
{
    public class MergeItemHelperBlazor : MergeItemHelperBase
    {
        public const string EntityType_ContextPropertyNameSingularizeLowerCase = "EntityType_ContextPropertyNameSingularizeLowerCase";
        public const string getEditFormItemTemplet = "getEditFormItemTemplet";
        public const string getTableHeaderItemTemplet = "getTableHeaderItemTemplet";
        public const string getTableRowItemTemplet = "getTableRowItemTemplet";
        public const string PageDirectiveCollection = "PageDirectiveCollection";
        public const string PageDirectiveCollectionHref = "PageDirectiveCollectionHref";
        public const string PageDirectiveCollectionHumanize = "PageDirectiveCollectionHumanize";
        public const string PageDirectiveCollectionLowerCase = "PageDirectiveCollectionLowerCase";
        public const string PageDirectiveEdit = "PageDirectiveEdit";
        public const string PageDirectiveEditHref = "PageDirectiveEditHref";
        public const string PageDirectiveEditHumanize = "PageDirectiveEditHumanize";
        public const string PageDirectiveEditLowerCase = "PageDirectiveEditLowerCase";
    }

    internal class BlazorServerVanillaCruisePackage : CruisePackageBase<ProjectInfoVanillaBlazorServer, EntityScaffoldInfoBlazorServer>
    {
        public const string NavMenuNavLinkTemplet = "NavMenuNavLinkTemplet";
        public const string NavMenuTemplet = "NavMenuTemplet";
        public const string PageCollectionTemplet = "PageCollectionTemplet";
        public const string PageEditFormItemTemplet = "PageEditFormItemTemplet";
        public const string PageEditFormTabsCollectionTemplet = "PageEditFormTabsCollectionTemplet";
        public const string PageEditFormTabsSkipTemplet = "PageEditFormTabsSkipTemplet";
        public const string PageEditFormTabsTemplet = "PageEditFormTabsTemplet";
        public const string PageEditFormTemplet = "PageEditFormTemplet";
        public const string TableHeaderItemTemplet = "TableHeaderItemTemplet";
        public const string TableRowItemTemplet = "TableRowItemTemplet";

        List<PropertyValue> _navMenuNavLinkList = new List<PropertyValue>();

        public BlazorServerVanillaCruisePackage()
        {
            ProjectInfo = new ProjectInfoVanillaBlazorServer();
            PackageReferences.Add(new PropertyValue { Property = "Microsoft.EntityFrameworkCore.InMemory" });
            PackageReferences.Add(new PropertyValue { Property = "Microsoft.EntityFrameworkCore" });
            CSPROJPropertyGroups.Add(
                new PropertyValue { Property = "BlazorWebAssemblyLoadAllGlobalizationData", Value = "true" });
        }

        string GetPath(EntityScaffoldInfoBlazorServer esi, string fileName = "")
        {
            string folder = ProjectInfo.PagesFolderName;
            string subFoulder = $"\\{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyNameSingularize, esi, string.Empty)}";
            string path = $"{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.ProjectDirectory, esi, string.Empty)}\\{folder}{subFoulder}";
            if(!String.IsNullOrEmpty(fileName))
                path = $"{path}\\{fileName}";
            return path;
        }
        Func<MergeItemList, CruisePackage.Common.IEntityScaffoldInfo, object, string> GetTabelProperties(
            string templetName)
        {
            return(m, esi, p) =>
            {
                var sb = new StringBuilder();
                foreach(PropertyScaffoldInfo propertyScaffoldInfo in esi?.PropertyScaffoldInfos)
                {
                    var templet = GetTemplatedItem(templetName);
                    templet.TempletTextReplace("Name", propertyScaffoldInfo.Name);
                    templet.TempletTextReplace("ColumnName", propertyScaffoldInfo.ColumnName);
                    templet.TempletTextReplace("TypeName", propertyScaffoldInfo.TypeName);
                    templet.TempletText = templet.ProcessTemplet(m, esi);
                    sb.AppendLine(templet.TempletText);
                }
                return sb.ToString();
            };
        }
        private Task ProcessNavMenu()
        {
            var nl = new StringBuilder();
            foreach(PropertyValue itm in _navMenuNavLinkList)
            {
                var temp = GetTemplatedItem(NavMenuNavLinkTemplet);
                //href,PageName
                //getNavLinks
                temp.TempletTextReplace("href", itm.Value);
                temp.TempletTextReplace("PageName", itm.Property);
                nl.AppendLine(temp.TempletText);
            }
            var nm = GetTemplatedItem(NavMenuTemplet);
            nm.TempletTextReplace("getNavLinks", nl.ToString());
            nm.TempletTextReplace("ProjectBaseNameSpace", ProjectInfo.ProjectBaseNameSpace.Humanize());
            var path = @$"{ProjectInfo.ProjectDirectoryFileFolder.Result}\{nm.SubPath}\";
            SaveFile("NavMenu.razor", path, nm.TempletText);
            return Task.CompletedTask;
        }
        private Task ProcessSimpleTemplet(string templet, string fileName)
        {
            var temp = GetTemplatedItem(templet);
            var path = @$"{ProjectInfo.ProjectDirectoryFileFolder.Result}\{temp.SubPath}\";
            SaveFile(
                fileName,
                path,
                temp.ProcessTemplet(MergeItemList, this.EntityScaffoldInfos.FirstOrDefault(x => x.Ignore == false)));
            return Task.CompletedTask;
        }
        TabData[]? ProcessTabComponents(EntityScaffoldInfoBlazorServer esi)
        {
            List<TabData> td = new List<TabData>();
            //   var sbCollectionTabTemplet = new StringBuilder();
            for(int i = 0; i < esi.NavigationScaffoldInfos.Count; i++)
            {
                NavigationScaffoldInfo itm = esi.NavigationScaffoldInfos.ElementAt(i);
                switch(itm.NavigationType)
                {
                    case NavigationType.Navigation:
                        break;
                    case NavigationType.Collection:
                        TemplatedItem temp = GetTemplatedItem(PageEditFormTabsCollectionTemplet);
                        var templet = new TemplatedItem
                        {
                            CodeType = temp.CodeType,
                            CustomTags = temp.CustomTags,
                            Name = temp.Name,
                            SubPath = temp.SubPath,
                            TempletText = temp.TempletText,
                            Version = temp.Version,
                            ShowCustomTagsOnly = temp.ShowCustomTagsOnly,
                        };
                        var esitemp = new EntityScaffoldInfoBlazorServer { FullName = itm.TargetEntityTypeName };
                        var tableHeaderItemData = this.MergeItemList
                            .GetMergeItemsByName(MergeItemHelperBlazor.getTableHeaderItemTemplet, esitemp, string.Empty);
                        temp.TempletTextReplace(MergeItemHelperBlazor.getTableHeaderItemTemplet, tableHeaderItemData);
                        var tableRowItemData = this.MergeItemList
                            .GetMergeItemsByName(MergeItemHelperBlazor.getTableRowItemTemplet, esitemp, string.Empty);
                        temp.TempletTextReplace(MergeItemHelperBlazor.getTableRowItemTemplet, tableRowItemData);
                        string pdc = $"{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.PageDirectiveCollection, esitemp, null)}";
                        string tcn = $"{pdc}{ProjectInfo.TabComponentName}";
                        td.Add(
                            new TabData
                            {
                                ComponentName = tcn,
                                Templet = temp.ProcessTemplet(MergeItemList, esitemp),
                                Include =
                                    $".Include(x => x.{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esitemp, null)})",
                                Collections =
                                    $"    {MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_TypeName, esitemp, null)}[]? {$"{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esitemp, null)}"} => {MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyNameSingularizeLowerCase, esi, null)}?.{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esitemp, null)}?.ToArray();",
                                Tabs =
                                    $"<{tcn} {MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esitemp, null)}=\"@{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esitemp, null)}\" />"
                            });
                        break;
                    case NavigationType.Skip:
                        break;
                    case NavigationType.Other:
                        break;
                }
            }
            if(td.Count == 0)
                return null;
            return td.ToArray();
        }

        protected override EntityScaffoldInfoBlazorServer GetEntityScaffoldInfo(
            DbContext context,
            IEntityType entityType)
        {
            var esi = base.GetEntityScaffoldInfo(context, entityType);
            string collection = GetPath(
                esi,
                $"{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.PageDirectiveCollection, esi, string.Empty)}.razor");
            if(System.IO.File.Exists(collection))
                esi.ProcessCollectionPage = false;
            else
                esi.ProcessCollectionPage = true;
            string edit = GetPath(
                esi,
                $"{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.PageDirectiveEdit, esi, string.Empty)}.razor");
            if(System.IO.File.Exists(edit))
                esi.ProcessEditPage = false;
            else
                esi.ProcessEditPage = true;
            return esi;
        }
        protected override MergeItemList GetMergeItemList()
        {
            var ml = base.GetMergeItemList();
            ml.Remove(ml.First(x => x.Name == MergeItemHelperBase.EntityType_AllUsings));
            ml.Add(MergeItemHelperBlazor.getTableRowItemTemplet, GetTabelProperties(TableRowItemTemplet), "Function");
            ml.Add(
                MergeItemHelperBlazor.getTableHeaderItemTemplet,
                GetTabelProperties(TableHeaderItemTemplet),
                "Function");
            ml.Add(
                MergeItemHelperBlazor.getEditFormItemTemplet,
                (m, esi, p) =>
                {
                    var sb = new StringBuilder();
                    foreach(PropertyScaffoldInfo propertyScaffoldInfo in esi?.PropertyScaffoldInfos)
                    {
                        var templet = GetTemplatedItem(PageEditFormItemTemplet);
                        //
                        string inputType = string.Empty;
                        switch(propertyScaffoldInfo.TypeName)
                        {
                            case "string":
                                inputType = "InputText";
                                break;
                            case "int":
                                inputType = "InputNumber";
                                break;
                            case "bool":
                                inputType = "InputCheckbox";
                                break;
                            case "datetime":
                            case "DateTime?":
                                inputType = "InputDate";
                                break;
                        }
                        templet.TempletTextReplace("InputType", inputType);
                        templet.TempletTextReplace("Name", propertyScaffoldInfo.Name);
                        templet.TempletTextReplace("ColumnName", propertyScaffoldInfo.ColumnName);
                        templet.TempletTextReplace("TypeName", propertyScaffoldInfo.TypeName);
                        templet.TempletText = templet.ProcessTemplet(m, esi);
                        sb.AppendLine(templet.TempletText);
                    }
                    return sb.ToString();
                },
                "Function");
            ml.Add(
                MergeItemHelperBlazor.EntityType_AllUsings,
                (m, esi, p) =>
                {
                    var sb = new StringBuilder();
                    var gb = EntityScaffoldInfos.GroupBy(x => x.NameSpace);
                    foreach(var item in gb)
                    {
                        sb.AppendLine($"@using {item.Key}");
                    }
                    sb.AppendLine($"@using {DbContextInstance.Instance.Namespace}");
                    return sb.ToString();
                },
                "NameSpace");
            mergeItemList.Add(
                MergeItemHelperBlazor.EntityType_ContextPropertyNameSingularizeLowerCase,
                (m, esi, p) => $"{esi.ContextPropertyName.Singularize().FirstCharToLowerCase()}",
                "DBContext");
            ml.Add(
                MergeItemHelperBlazor.PageDirectiveCollection,
                (m, esi, p) =>
                {
                    string name = $"{m.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esi, string.Empty)}{ProjectInfo.CollectionPageName}";
                    return name;
                },
                "Page");
            ml.Add(
                MergeItemHelperBlazor.PageDirectiveCollectionHref,
                (m, esi, p) =>
                {
                    string name = $"{m.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyNameSingularizeLowerCase, esi, null)}/{m.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esi, string.Empty)}{ProjectInfo.CollectionPageName}";
                    return name;
                },
                "Page");
            ml.Add(
                MergeItemHelperBlazor.PageDirectiveEditHref,
                (m, esi, p) =>
                {
                    string name = $"{m.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyNameSingularizeLowerCase, esi, null)}/{m.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esi, string.Empty)}{ProjectInfo.EditPageName}";
                    return name;
                },
                "Page");
            ml.Add(
                MergeItemHelperBlazor.PageDirectiveCollectionLowerCase,
                (m, esi, p) =>
                {
                    string name = $"{m.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esi, string.Empty)}{ProjectInfo.CollectionPageName}";
                    name = name.FirstCharToLowerCase() ?? name;
                    return name;
                },
                "Page");
            ml.Add(
                MergeItemHelperBlazor.PageDirectiveEdit,
                (m, esi, p) =>
                {
                    string name = $"{m.GetMergeItemsByName(
            MergeItemHelperBlazor.EntityType_ContextPropertyName,
            esi,
            string.Empty)}{ProjectInfo.EditPageName}";
                    return name;
                },
                "Page");
            ml.Add(
                MergeItemHelperBlazor.PageDirectiveEditHumanize,
                (m, esi, p) =>
                {
                    string name = $"{m.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esi, string.Empty)}{ProjectInfo.EditPageName}";
                    return name.Humanize();
                },
                "Page");
            ml.Add(
                MergeItemHelperBlazor.PageDirectiveCollectionHumanize,
                (m, esi, p) =>
                {
                    string name = $"{m.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esi, string.Empty)}{ProjectInfo.CollectionPageName}";
                    return name.Humanize();
                },
                "Page");
            ml.Add(
                MergeItemHelperBlazor.PageDirectiveEditLowerCase,
                (m, esi, p) =>
                {
                    string name = $"{m.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyName, esi, string.Empty)}{ProjectInfo.EditPageName}";
                    name = name.FirstCharToLowerCase() ?? name;
                    return name;
                },
                "Page");
            MergeItemList.SortItems();
            return ml;
        }
        protected override ObservableCollection<TemplatedItem> GetTemplatedItems()
        {
            var ti = base.GetTemplatedItems();
            ti.Add(
                new TemplatedItem
                {
                    Name = NavMenuTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "razor",
                    CustomTags = "getNavLinks",
                    SubPath = "Shared",
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = NavMenuNavLinkTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "razor",
                    CustomTags = "href,PageName",
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = TableRowItemTemplet,
                    TempletText =
                        "                    <td>@[=EntityType_ContextPropertyNameSingularizeLowerCase=].[=ColumnName=]</td>",
                    Version = string.Empty,
                    CodeType = "razor",
                    CustomTags = "ColumnName,Name,TypeName",
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = TableHeaderItemTemplet,
                    TempletText = "                <th>[=ColumnName=]</th>",
                    Version = string.Empty,
                    CodeType = "razor",
                    CustomTags = "ColumnName,Name,TypeName",
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = PageEditFormItemTemplet,
                    TempletText =
                        @"    <div>
        <label>
            [=ColumnName=]:
            <InputText @bind-Value=""[=EntityType_ContextPropertyNameSingularizeLowerCase=].[=ColumnName=]"" />
        </label>
    </div>",
                    Version = string.Empty,
                    CodeType = "razor",
                    CustomTags = "ColumnName,Name,TypeName,InputType",
                });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = CruiseBlazorHelperItemTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CustomTags = "ServiceName",
            //        CodeType = "C#"
            //    });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = MainLayoutTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "razor",
            //        SubPath = "Shared"
            //    });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = ImportsTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "razor",
            //        SubPath = string.Empty
            //    });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = HostTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "ASP/XHTML",
            //        SubPath = "Pages"
            //    });
            ti.Add(
                new TemplatedItem
                {
                    Name = PageCollectionTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "razor"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = PageEditFormTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "razor",
                    CustomTags = "getTabs,getIncludes,getCollectins"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = PageEditFormTabsTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "razor",
                    CustomTags = "getCollectionTabs"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = PageEditFormTabsCollectionTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "razor",
                    CustomTags = string.Empty,
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = PageEditFormTabsSkipTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "razor"
                });
            return new ObservableCollection<TemplatedItem>(ti.OrderBy(x => x.Name));
        }
 
        public void ProcessPages(EntityScaffoldInfoBlazorServer esi)
        {
            if(esi.ProcessCollectionPage)
            {
                var CollectionTemplet = GetTemplatedItem(PageCollectionTemplet);
                var CollectionData = CollectionTemplet.ProcessTemplet(MergeItemList, esi);
                // string PagePath = $"{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.ProjectDirectory, esi, string.Empty)}\\{ProjectInfo.PagesFolderName}\\{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.EntityType_ContextPropertyNameSingularize, esi, string.Empty)}";
                string PagePath = GetPath(esi);
                string CollectionFileName = $"{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.PageDirectiveCollection, esi, string.Empty)}.razor";
                SaveFile(CollectionFileName, PagePath, CollectionData);
            }
            if(esi.ProcessEditPage)
            {
                string PagePath = GetPath(esi);
                var EditTemplet = GetTemplatedItem(PageEditFormTemplet);
                TabData[]? tabData = ProcessTabComponents(esi);
                if(tabData != null)
                {
                    string Includes = string.Empty;
                    StringBuilder collections = new StringBuilder();
                    StringBuilder tabs = new StringBuilder();
                    foreach(TabData td in tabData)
                    {
                        string tabFileName = $"{td.ComponentName}.razor";
                        Includes = $"{Includes}{td.Include}";
                        collections.AppendLine($"{td.Collections}");
                        tabs.AppendLine(td.Tabs);
                        SaveFile(tabFileName, PagePath, td.Templet);
                    }
                    var tabcollection = GetTemplatedItem(PageEditFormTabsTemplet);
                    tabcollection.TempletTextReplace("getCollectionTabs", tabs.ToString());
                    EditTemplet.TempletTextReplace("getTabs", tabcollection.ProcessTemplet(MergeItemList, esi));
                    EditTemplet.TempletTextReplace("getCollectins", collections.ToString());
                    EditTemplet.TempletTextReplace("getIncludes", Includes);
                } else
                {
                    EditTemplet.TempletTextReplace("getTabs", string.Empty);
                    EditTemplet.TempletTextReplace("getCollectins", string.Empty);
                    EditTemplet.TempletTextReplace("getIncludes", string.Empty);
                }
                var EditData = EditTemplet.ProcessTemplet(MergeItemList, esi);
                string EditFileName = $"{MergeItemList.GetMergeItemsByName(MergeItemHelperBlazor.PageDirectiveEdit, esi, string.Empty)}.razor";
                SaveFile(EditFileName, PagePath, EditData);
            }
        }
        public override async Task RunScaffoldingAsync()
        {
            IsRunningScaffolding = true;
            _navMenuNavLinkList.Clear();
            //    await this.AddPackageReferencesAsync();
            //    await this.AddOrUpdateCSPROJPropertyGroupAsync();
            foreach(EntityScaffoldInfoBlazorServer esi in EntityScaffoldInfos)
                if(!esi.Ignore)
                {
                    string pageName = MergeItemList.GetMergeItemsByName(
                        MergeItemHelperBlazor.EntityType_ContextPropertyNameHumanize,
                        esi,
                        null)
                        .Humanize();
                    string href = MergeItemList.GetMergeItemsByName(
                        MergeItemHelperBlazor.PageDirectiveCollectionHref,
                        esi,
                        null);
                    _navMenuNavLinkList.Add(new PropertyValue { Property = pageName, Value = href });
                    ProcessPages(esi);
                }
            await ProcessNavMenu();
            //await ProcessSimpleTemplet(MainLayoutTemplet, "MainLayout.razor");
            //await ProcessSimpleTemplet(HostTemplet, "_Host.cshtml");
            //await ProcessSimpleTemplet(ImportsTemplet, "_Imports.razor");
            await AddRequiredFilesAsync();
            IsRunningScaffolding = false;
        }

        internal class TabData
        {
            public TabData()
            {
            }

            public string Collections { get; set; }
            public string ComponentName { get; set; }
            public string Include { get; set; }
            public string Tabs { get; set; }
            public string Templet { get; set; }
        }
    }
}

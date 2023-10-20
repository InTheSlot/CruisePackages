using CruisePackage;
using CruisePackage.Common;
using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace WPFDevExCruisePackage
{
    public class MergeItemHelper : MergeItemHelperBase
    {
        public const string CollectionView_FolderName = "CollectionView_FolderName";
        public const string CollectionView_FolderPath = "CollectionView_FolderPath";
        public const string CollectionView_Name = "CollectionView_Name";
        public const string CollectionViewModel_FolderName = "CollectionViewModel_FolderName";
        public const string CollectionViewModel_FolderPath = "CollectionViewModel_FolderPath";
        public const string CollectionViewModel_Name = "CollectionViewModel_Name";
        public const string GetDataGridColumns = "GetDataGridColumns";
        public const string GetDataLayoutItemColumns = "GetDataLayoutItemColumns";
        public const string IUnitOfWork_Name = "IUnitOfWork_Name";

        //public const string GetDesignTimeUnitOfWorkFile = "GetDesignTimeUnitOfWorkFile";
        //public const string GetEntityViewModelDetails = "GetEntityViewModelDetails";
        //public const string GetEntityViewModelLookups = "GetEntityViewModelLookups";
        //public const string GetEntityViewModelSkips = "GetEntityViewModelSkips";
        //public const string GetIUnitOfWorkFile = "GetIUnitOfWorkFile";
        //public const string GetMainWindowViewModelModuleDescriptionItems = "GetMainWindowViewModelModuleDescriptionItems";
        //public const string GetMetadataFile = "GetMetadataFile";
        //public const string GetUnitOfWorkFile = "GetUnitOfWorkFile";
        //public const string GetUnitOfWorkSourceFile = "GetUnitOfWorkSourceFile";
        //public const string GetViewTabs = "GetViewTabs";
        public const string View_FolderName = "View_FolderName";
        public const string View_FolderPath = "View_FolderPath";
        public const string View_Name = "View_Name";
        public const string View_Namespace = "View_Namespace";
        //public const string View_xClass = "View_xClass";
        public const string ViewModel_FolderName = "ViewModel_FolderName";
        public const string ViewModel_FolderPath = "ViewModel_FolderPath";
        public const string ViewModel_Name = "ViewModel_Name";
        public const string ViewModel_Namespace = "ViewModel_Namespace";
    }

    internal class WPFDevExCruisePackage : CruisePackageBase<ProjectInfoWPF, EntityScaffoldInfoWPF>
    {
        public const string CodeBehindTemplet = "CodeBehindTemplet";
        public const string CollectionTabTemplet = "CollectionTabTemplet";
        public const string CollectionViewModelTemplet = "CollectionViewModelTemplet";
        public const string CollectionViewTemplet = "CollectionViewTemplet";
        public const string DataLayoutItemTemplet = "DataLayoutItemTemplet";
        //public const string DesignTimeUnitOfWorkFileTemplet = "DesignTimeUnitOfWorkFileTemplet";
        //public const string DesignTimeUnitOfWorkItemFileTemplet = "DesignTimeUnitOfWorkItemFileTemplet";
        public const string EntityViewModelDetailsTemplet = "EntityViewModelDetailsTemplet";
        public const string EntityViewModelLookupTemplet = "EntityViewModelLookupTemplet";
        public const string EntityViewModelSkipsTemplet = "EntityViewModelSkipsTemplet";
        public const string EntityViewModelTemplet = "EntityViewModelTemplet";
        public const string EntityViewTemplet = "EntityViewTemplet";
        public const string GridColumnTemplet = "GridColumnTemplet";
        //public const string IUnitOfWorkFileTemplet = "IUnitOfWorkFileTemplet";
        //public const string IUnitOfWorkItemFileTemplet = "IUnitOfWorkItemFileTemplet";
        public const string MainWindowViewModelModuleDescriptionItemsTemplet = "MainWindowViewModelModuleDescriptionItemsTemplet";
        public const string MainWindowViewModelTemplet = "MainWindowViewModelTemplet";
        public const string MainWindowViewTemplet = "MainWindowViewTemplet";
        //public const string MetadataFileTemplet = "MetadataFileTemplet";
        //public const string MetadataItemFileTemplet = "MetadataItemFileTemplet";
        public const string OtherTabsTemplet = "OtherTabsTemplet";
        public const string SkipTabTemplet = "SkipTabTemplet";
        //public const string UnitOfWorkFileTemplet = "UnitOfWorkFileTemplet";
        //public const string UnitOfWorkItemFileTemplet = "UnitOfWorkItemFileTemplet";
        //public const string UnitOfWorkSourceFileTemplet = "UnitOfWorkSourceFileTemplet";
        public const string ViewTabsTemplet = "ViewTabsTemplet";

        public WPFDevExCruisePackage() { ProjectInfo = new ProjectInfoWPF();
            this.PackageReferences.Add(new PropertyValue { Property = "DevExpress.Wpf", Value = "*" });
            this.CSPROJPropertyGroups.Add(new PropertyValue {  Property= "UseWPF", Value= "True" });
            this.CSPROJPropertyGroups.Add(new PropertyValue { Property = "OutputType", Value = "WinExe" });
        }

        private string GetCodeBehind(string className, IEntityScaffoldInfo esi)
        {
            var cbTemplet = GetTemplatedItem(CodeBehindTemplet);
            cbTemplet.TempletTextReplace("ClassName", className);
            return cbTemplet.ProcessTemplet(MergeItemList, esi);
        }
        private string GetEntityViewModelDetails(IEntityScaffoldInfo esi)
        {
            var sb = new StringBuilder();
            foreach(NavigationScaffoldInfo navigationScaffoldInfo in esi.NavigationScaffoldInfos
                .Where(x => x.NavigationType == NavigationType.Collection))
            {
                var lookupTemplet = GetTemplatedItem(EntityViewModelDetailsTemplet);
                lookupTemplet.TempletTextReplace(
                    "MethodName",
                    $"{esi.TypeName}{navigationScaffoldInfo.TargetPKNavProperty}");
                lookupTemplet.TempletTextReplace("TargetDBSetName", navigationScaffoldInfo.TargetDBSetName);
                lookupTemplet.TempletTextReplace("TargetEntityTypeName", navigationScaffoldInfo.TargetEntityTypeName);
                lookupTemplet.TempletTextReplace("TargetPKNavProperty", navigationScaffoldInfo.TargetPKNavProperty);
                lookupTemplet.TempletTextReplace("TargetPKNavIDProperty", navigationScaffoldInfo.TargetPKNavIDProperty);
                lookupTemplet.TempletTextReplace("TargetPKTypeName", navigationScaffoldInfo.TargetPKTypeName);
                lookupTemplet.TempletTextReplace("ContextPropertyName", navigationScaffoldInfo.ContextPropertyName);
                lookupTemplet.TempletTextReplace("ParentNavProperty", navigationScaffoldInfo.ParentNavProperty);
                lookupTemplet.TempletText = lookupTemplet.ProcessTemplet(mergeItemList, esi);
                lookupTemplet.TempletText = lookupTemplet.ProcessTemplet(mergeItemList, esi);
                sb.AppendLine(lookupTemplet.TempletText);
                sb.AppendLine();
            }

            return sb.ToString();
        }
        private   string GetEntityViewModelLookups(IEntityScaffoldInfo esi)
        {
            var sb = new StringBuilder();
            foreach(NavigationScaffoldInfo navigationScaffoldInfo in esi.NavigationScaffoldInfos
                .Where(x => x.NavigationType == NavigationType.Navigation))
            {
                var lookupTemplet = GetTemplatedItem(EntityViewModelLookupTemplet);
                lookupTemplet.TempletTextReplace("TargetDBSetName", navigationScaffoldInfo.TargetDBSetName);
                lookupTemplet.TempletTextReplace("TargetPKNavProperty", navigationScaffoldInfo.TargetPKNavProperty);
                lookupTemplet.TempletTextReplace("TargetEntityTypeName", navigationScaffoldInfo.TargetEntityTypeName);
                lookupTemplet.TempletTextReplace("TargetPKNavIDProperty", navigationScaffoldInfo.TargetPKNavIDProperty);
                lookupTemplet.TempletTextReplace("ParentNavProperty", navigationScaffoldInfo.ParentNavProperty);
                lookupTemplet.TempletTextReplace("TargetPKTypeName", navigationScaffoldInfo.TargetPKTypeName);
                lookupTemplet.TempletTextReplace("ContextPropertyName", navigationScaffoldInfo.ContextPropertyName);
                lookupTemplet.TempletText = lookupTemplet.ProcessTemplet(mergeItemList, esi);
                sb.AppendLine(lookupTemplet.TempletText);
                sb.AppendLine();
            }

            return sb.ToString();
        }
        private string GetEntityViewModelSkips(IEntityScaffoldInfo esi)
        {
            var sb = new StringBuilder();
            foreach(NavigationScaffoldInfo navigationScaffoldInfo in esi.NavigationScaffoldInfos
                .Where(x => x.NavigationType == NavigationType.Skip))
            {
                var lookupTemplet = GetTemplatedItem(EntityViewModelSkipsTemplet);
                lookupTemplet.TempletTextReplace(
                    "MethodName",
                    $"{esi.TypeName}{navigationScaffoldInfo.TargetPKNavProperty}");
                lookupTemplet.TempletTextReplace("TargetDBSetName", navigationScaffoldInfo.TargetDBSetName);
                lookupTemplet.TempletTextReplace("TargetEntityTypeName", navigationScaffoldInfo.TargetEntityTypeName);
                lookupTemplet.TempletTextReplace("TargetPKNavProperty", navigationScaffoldInfo.TargetPKNavProperty);
                lookupTemplet.TempletTextReplace("TargetPKNavIDProperty", navigationScaffoldInfo.TargetPKNavIDProperty);
                lookupTemplet.TempletTextReplace("TargetPKTypeName", navigationScaffoldInfo.TargetPKTypeName);
                lookupTemplet.TempletTextReplace("ContextPropertyName", navigationScaffoldInfo.ContextPropertyName);
                lookupTemplet.TempletTextReplace("ParentNavProperty", navigationScaffoldInfo.ParentNavProperty);
                lookupTemplet.TempletText = lookupTemplet.ProcessTemplet(mergeItemList, esi);
                lookupTemplet.TempletText = lookupTemplet.ProcessTemplet(mergeItemList, esi);
                sb.AppendLine(lookupTemplet.TempletText);
                sb.AppendLine();
            }
            var s = sb.ToString();
            return sb.ToString();
        }
        private string GetMainWindowViewModelModuleDescriptionItemsTemplet(IEntityScaffoldInfo esi)
        {
            var sb = new StringBuilder();
            foreach(EntityScaffoldInfoWPF entityScaffoldInfoWPF in EntityScaffoldInfos.Where(x => x.Ignore == false))
            {
                var t = GetTemplatedItem(MainWindowViewModelModuleDescriptionItemsTemplet);
                sb.AppendLine(t.ProcessTemplet(MergeItemList, entityScaffoldInfoWPF));
            }
            return sb.ToString();
        }
        private string GetViewTabs(IEntityScaffoldInfo esi)
        {
            var sbCollectionTabTemplet = new StringBuilder();
            var sbSkipTabTemplet = new StringBuilder();
            var sbOtherTemplet = new StringBuilder();
            var MainTemplet = GetTemplatedItem(ViewTabsTemplet);
            for(int i = 0; i < esi.NavigationScaffoldInfos.Count; i++)
            {
                var itm = esi.NavigationScaffoldInfos.ElementAt(i);
                switch(itm.NavigationType)
                {
                    case NavigationType.Navigation:
                        break;
                    case NavigationType.Collection:
                        var temp = GetTemplatedItem(CollectionTabTemplet);
                        var templet = new TemplatedItem
                        {
                            CodeType = temp.CodeType,
                            CustomTags = temp.CustomTags,
                            Name = temp.Name,
                            SubPath = temp.SubPath,
                            TempletText = temp.TempletText,
                            Version = temp.Version
                        };
                        TemplatedItemParameter tp = new TemplatedItemParameter
                        {
                            TagName = MergeItemHelper.GetDataGridColumns,
                            Parameter = i.ToString()
                        };
                        templet.TempletTextReplace("DetailsName", $"{esi.TypeName}{itm.TargetPKNavProperty}");
                        templet.TempletTextReplace(
                            "Localization",
                            $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.DBContext_TypeName, esi, null)}Resources.{itm.ContextPropertyName}Plural");
                        var ct = mergeItemList.GetMergeItemsByName(MergeItemHelper.GetDataGridColumns, esi, tp);
                        templet.TempletTextReplace(MergeItemHelper.GetDataGridColumns, ct);
                        var esitemp = new EntityScaffoldInfoWPF { FullName = itm.TargetEntityTypeName };
                        sbCollectionTabTemplet.AppendLine(templet.ProcessTemplet(mergeItemList, esitemp));
                        break;
                    case NavigationType.Skip:
                        var tempSkip = GetTemplatedItem(SkipTabTemplet);
                        var templetSkip = new TemplatedItem
                        {
                            CodeType = tempSkip.CodeType,
                            CustomTags = tempSkip.CustomTags,
                            Name = tempSkip.Name,
                            SubPath = tempSkip.SubPath,
                            TempletText = tempSkip.TempletText,
                            Version = tempSkip.Version
                        };
                        TemplatedItemParameter tpSkip = new TemplatedItemParameter
                        {
                            TagName = MergeItemHelper.GetDataGridColumns,
                            Parameter = i.ToString()
                        };
                        templetSkip.TempletTextReplace("DetailsName", $"{esi.TypeName}{itm.TargetPKNavProperty}");
                        templetSkip.TempletTextReplace(
                            "Localization",
                            $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.DBContext_TypeName, esi, null)}Resources.{itm.ContextPropertyName}Plural");
                        var ctSkip = mergeItemList.GetMergeItemsByName(MergeItemHelper.GetDataGridColumns, esi, tpSkip);
                        templetSkip.TempletTextReplace(MergeItemHelper.GetDataGridColumns, ctSkip);
                        var esitempSkip = new EntityScaffoldInfoWPF { FullName = itm.TargetEntityTypeName };
                        sbSkipTabTemplet.AppendLine(templetSkip.ProcessTemplet(mergeItemList, esitempSkip));
                        break;
                    case NavigationType.Other:
                        break;
                }
            }
            MainTemplet.TempletTextReplace(CollectionTabTemplet, sbCollectionTabTemplet.ToString());
            MainTemplet.TempletTextReplace(SkipTabTemplet, sbSkipTabTemplet.ToString());
            MainTemplet.TempletTextReplace(OtherTabsTemplet, sbOtherTemplet.ToString());
            return MainTemplet.TempletText;
        }
        private void ProcessCollectionViewModelTemplet(EntityScaffoldInfo esi)
        {
            var templet = GetTemplatedItem(CollectionViewModelTemplet);
            var data = templet.ProcessTemplet(MergeItemList, esi);
            string path = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.CollectionViewModel_FolderPath, esi, templet.SubPath)}";
            string fileName = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.CollectionViewModel_Name, esi, ".cs")}";
            SaveFile(fileName, path, data);
        }
        private void ProcessCollectionViewTemplet(EntityScaffoldInfo esi)
        {
            var templet = GetTemplatedItem(CollectionViewTemplet);
            //var ct = mergeItemList.GetMergeItemsByName(MergeItemHelper.GetDataGridColumns, esi);
            //templet.TempletTextReplace(MergeItemHelper.GetDataGridColumns, ct);
            var data = templet.ProcessTemplet(MergeItemList, esi);
            string path = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.CollectionView_FolderPath, esi, templet.SubPath)}";
            string fileName = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.CollectionView_Name, esi, ".xaml")}";
            SaveFile(fileName, path, data);
            string cbfileName = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.CollectionView_Name, esi, ".xaml.cs")}";
            SaveFile(
                cbfileName,
                path,
                GetCodeBehind(MergeItemList.GetMergeItemsByName(MergeItemHelper.CollectionView_Name, esi), esi));
        }
        private void ProcessEntityViewModelTemplet(EntityScaffoldInfo esi)
        {
            var templet = GetTemplatedItem(EntityViewModelTemplet);
            templet.TempletTextReplace("getEntityViewModelLookups", GetEntityViewModelLookups(esi));
            templet.TempletTextReplace("getEntityViewModelSkips", GetEntityViewModelSkips(esi));
            templet.TempletTextReplace("getEntityViewModelDetails", GetEntityViewModelDetails(esi));
            var data = templet.ProcessTemplet(MergeItemList, esi);
            string path = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.ViewModel_FolderPath, esi, templet.SubPath)}";
            string fileName = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.ViewModel_Name, esi, ".cs")}";
            SaveFile(fileName, path, data);
        }
        private void ProcessEntityViewTemplet(EntityScaffoldInfo esi)
        {
            var templet = GetTemplatedItem(EntityViewTemplet);
            templet.TempletTextReplace("getViewTabs", GetViewTabs(esi));

            var data = templet.ProcessTemplet(MergeItemList, esi);
            string path = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.View_FolderPath, esi, templet.SubPath)}";
            string fileName = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.View_Name, esi, ".xaml")}";
            SaveFile(fileName, path, data);

            string cbfileName = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.View_Name, esi, ".xaml.cs")}";
            SaveFile(
                cbfileName,
                path,
                GetCodeBehind(MergeItemList.GetMergeItemsByName(MergeItemHelper.View_Name, esi), esi));
        }
        private void ProcessMainContextViewModelTemplet()
        {
            var templet = GetTemplatedItem(MainWindowViewModelTemplet);
            var esi = EntityScaffoldInfos.FirstOrDefault();
            templet.TempletTextReplace(
                "getDescriptionItemsTemplet",
                GetMainWindowViewModelModuleDescriptionItemsTemplet(esi));
            var data = templet.ProcessTemplet(MergeItemList, esi);
            string path = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.ProjectDirectory, esi)}\\{MergeItemList.GetMergeItemsByName(MergeItemHelper.ViewModel_FolderName,esi)}";
            string fileName = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.DBContext_TypeName, esi)}{MergeItemList.GetMergeItemsByName(MergeItemHelper.ViewModel_FolderName, esi)}.cs";
            SaveFile(fileName, path, data);
        }
        private void ProcessMainContextViewTemplet()
        {
            var templet = GetTemplatedItem(MainWindowViewTemplet);
            var esi = EntityScaffoldInfos.FirstOrDefault();
            var data = templet.ProcessTemplet(MergeItemList, esi);
            string path = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.ProjectDirectory, esi)}\\{MergeItemList.GetMergeItemsByName(MergeItemHelper.View_FolderName, esi)}";
            string className = $"{MergeItemList.GetMergeItemsByName(MergeItemHelper.DBContext_TypeName, esi)}{MergeItemList.GetMergeItemsByName(MergeItemHelper.View_FolderName, esi).Singularize()}";
            string fileName = $"{className}.xaml";
            SaveFile(fileName, path, data);

            string cbfileName = $"{fileName}.cs";
            SaveFile(cbfileName, path, GetCodeBehind(className, esi));
        }

        protected override EntityScaffoldInfoWPF GetEntityScaffoldInfo(DbContext context, IEntityType entityType)
        {
            EntityScaffoldInfoWPF esi = base.GetEntityScaffoldInfo(context, entityType);
            if(esi == null)
                return null;
            string viewName = $"{(mergeItemList.GetMergeItemsByName(MergeItemHelper.View_Name, esi, string.Empty))}.xaml";
            var viewpath = mergeItemList.GetMergeItemsByName(MergeItemHelper.View_FolderPath, esi, viewName);
            if(!File.Exists(viewpath))
                esi.ReplaceView = true;
            string viewModelName = $"{(mergeItemList.GetMergeItemsByName(MergeItemHelper.ViewModel_Name, esi, string.Empty))}.cs";
            var viewModelpath = mergeItemList.GetMergeItemsByName(
                MergeItemHelper.ViewModel_FolderPath,
                esi,
                viewModelName);
            if(!File.Exists(viewModelpath))
                esi.ReplaceViewModel = true;
            string collectionViewName = $"{(mergeItemList.GetMergeItemsByName(MergeItemHelper.CollectionView_Name, esi, string.Empty))}.xaml";
            var collectionViewpath = mergeItemList.GetMergeItemsByName(
                MergeItemHelper.View_FolderPath,
                esi,
                collectionViewName);
            if(!File.Exists(collectionViewpath))
                esi.ReplaceCollectionView = true;
            string collectionViewModelName = $"{(mergeItemList.GetMergeItemsByName(MergeItemHelper.CollectionViewModel_Name, esi, string.Empty))}.cs";
            var collectionViewModelpath = mergeItemList.GetMergeItemsByName(
                MergeItemHelper.ViewModel_FolderPath,
                esi,
                collectionViewModelName);
            if(!File.Exists(collectionViewModelpath))
                esi.ReplaceCollectionViewModel = true;
            return esi;
        }
        protected override MergeItemList GetMergeItemList()
        {
            MergeItemList ml = base.GetMergeItemList();
            mergeItemList.Add(
                MergeItemHelper.View_Namespace,
                (m, esi, p) => $"{m.GetMergeItemsByName(MergeItemHelper.ProjectBase_Namespace, esi, p)}.{m.GetMergeItemsByName(MergeItemHelper.View_FolderName, esi, p)}",
                "NameSpace");
            mergeItemList.Add(
                MergeItemHelper.ViewModel_Namespace,
                (m, esi, p) => $"{m.GetMergeItemsByName(MergeItemHelper.ProjectBase_Namespace, esi, p)}.{m.GetMergeItemsByName(MergeItemHelper.ViewModel_FolderName, esi, p)}",
                "NameSpace");
            mergeItemList.Add(
                MergeItemHelper.View_Name,
                (m, esi, p) => $"{esi.ContextPropertyName.Singularize()}View{p}",
                "View");
            mergeItemList.Add(
                MergeItemHelper.View_FolderName,
                (m, esi, p) => $"{this.ProjectInfo.ViewFolderName}{(!String.IsNullOrEmpty(p?.ToString()) ? $"\\{p}" : string.Empty)}",
                "View");
            mergeItemList.Add(
                MergeItemHelper.ViewModel_Name,
                (m, esi, p) => $"{m.GetMergeItemsByName(MergeItemHelper.View_Name, esi, null)}Model{p}",
                "ViewModel");
            mergeItemList.Add(
                MergeItemHelper.CollectionView_Name,
                (m, esi, p) => $"{esi.ContextPropertyName.Singularize()}CollectionView{p}",
                "View");
            mergeItemList.Add(
                MergeItemHelper.CollectionView_FolderName,
                (m, esi, p) => $"{this.ProjectInfo.ViewModelFolderName}{(!String.IsNullOrEmpty(p?.ToString()) ? $"\\{p}" : string.Empty)}",
                "View");
            mergeItemList.Add(
                MergeItemHelper.CollectionView_FolderPath,
                (m, esi, p) => $"{m.GetMergeItemsByName(MergeItemHelper.ProjectDirectory, esi, p)}\\{m.GetMergeItemsByName(MergeItemHelper.CollectionView_FolderName, esi, esi.TypeName)}",
                "View");
            mergeItemList.Add(
                MergeItemHelper.CollectionViewModel_Name,
                (m, esi, p) => $"{m.GetMergeItemsByName(MergeItemHelper.CollectionView_Name, esi, null)}Model{p}",
                "ViewModel");
            mergeItemList.Add(
                MergeItemHelper.CollectionViewModel_FolderName,
                (m, esi, p) => $"{this.ProjectInfo.ViewModelFolderName}{(!String.IsNullOrEmpty(p?.ToString()) ? $"\\{p}" : string.Empty)}",
                "ViewModel");
            mergeItemList.Add(
                MergeItemHelper.ViewModel_FolderName,
                (m, esi, p) => $"{this.ProjectInfo.ViewModelFolderName}{(!String.IsNullOrEmpty(p?.ToString()) ? $"\\{p}" : string.Empty)}",
                "ViewModel");
            mergeItemList.Add(
                MergeItemHelper.View_FolderPath,
                (m, esi, p) => $"{m.GetMergeItemsByName(MergeItemHelper.ProjectDirectory, esi, p)}\\{m.GetMergeItemsByName(MergeItemHelper.View_FolderName, esi, esi.TypeName)}",
                "View");
            mergeItemList.Add(
                MergeItemHelper.CollectionViewModel_FolderPath,
                (m, esi, p) => $"{m.GetMergeItemsByName(MergeItemHelper.ProjectDirectory, esi, p)}\\{m.GetMergeItemsByName(MergeItemHelper.CollectionViewModel_FolderName, esi, esi.TypeName)}",
                "ViewModel");
            mergeItemList.Add(
                MergeItemHelper.ViewModel_FolderPath,
                (m, esi, p) => $"{m.GetMergeItemsByName(MergeItemHelper.ProjectDirectory, esi, p)}\\{m.GetMergeItemsByName(MergeItemHelper.ViewModel_FolderName, esi, esi.TypeName)}",
                "ViewModel");
            
            mergeItemList.Add(
    MergeItemHelper.IUnitOfWork_Name,
    (m, esi, p) => $"I{m.GetMergeItemsByName(MergeItemHelper.DBContext_TypeName, esi, p)}UnitOfWork",
    "DBContext");



            //mergeItemList.Add(
            //    MergeItemHelper.View_xClass,
            //    (m, esi, p) => $"{m.GetMergeItemsByName(MergeItemHelper.ProjectBase_Namespace, esi, p)}.{m.GetMergeItemsByName(MergeItemHelper.View_FolderName, esi, p)}.{m.GetMergeItemsByName(MergeItemHelper.View_Name, esi, p)}",
            //    "View");

            mergeItemList.Add(
                MergeItemHelper.GetDataGridColumns,
                (m, esi, p) =>
                {
                    var sb = new StringBuilder();
                    TemplatedItemParameter? tp = p as TemplatedItemParameter;
                    if(tp == null)
                    {
                        ICollection<PropertyScaffoldInfo>? propertyScaffoldInfos = esi?.PropertyScaffoldInfos;
                        if(propertyScaffoldInfos != null)
                            foreach(PropertyScaffoldInfo propertyScaffoldInfo in propertyScaffoldInfos)
                            {
                                var templet = base.GetTemplatedItem(GridColumnTemplet);
                                templet.TempletTextReplace("Name", propertyScaffoldInfo.Name);
                                templet.TempletTextReplace("ColumnName", propertyScaffoldInfo.ColumnName);
                                templet.TempletTextReplace("TypeName", propertyScaffoldInfo.TypeName);
                                sb.AppendLine(templet.TempletText);
                            }
                    } else
                    {
                        NavigationScaffoldInfo nsi = esi.NavigationScaffoldInfos
                            .ElementAt(Convert.ToInt32(tp.Parameter));
                        if(nsi != null && nsi.PropertyScaffoldInfos != null)
                            foreach(PropertyScaffoldInfo propertyScaffoldInfo in nsi.PropertyScaffoldInfos)
                            {
                                var templet = base.GetTemplatedItem(GridColumnTemplet);
                                templet.TempletTextReplace("Name", propertyScaffoldInfo.Name);
                                templet.TempletTextReplace("ColumnName", propertyScaffoldInfo.ColumnName);
                                templet.TempletTextReplace("TypeName", propertyScaffoldInfo.TypeName);
                                sb.AppendLine(templet.TempletText);
                            }
                    }
                    return sb.ToString();
                },
                "View");

            mergeItemList.Add(
                MergeItemHelper.GetDataLayoutItemColumns,
                (m, esi, p) =>
                {
                    var sb = new StringBuilder();
                    ICollection<PropertyScaffoldInfo>? propertyScaffoldInfos = esi?.PropertyScaffoldInfos;
                    if(propertyScaffoldInfos != null)
                        foreach(PropertyScaffoldInfo propertyScaffoldInfo in propertyScaffoldInfos)
                        {
                            var templet = GetTemplatedItem(DataLayoutItemTemplet);
                            templet.TempletTextReplace("Name", propertyScaffoldInfo.Name);
                            templet.TempletTextReplace("ColumnName", propertyScaffoldInfo.ColumnName);
                            templet.TempletTextReplace("TypeName", propertyScaffoldInfo.TypeName);
                            sb.AppendLine(templet.TempletText);
                        }
                    return sb.ToString();
                },
                "View");

            MergeItemList.SortItems();
            return ml;
        }
        protected override ObservableCollection<TemplatedItem> GetTemplatedItems()
        {
            var ti = base.GetTemplatedItems();
            ti.Add(
                new TemplatedItem
                {
                    Name = MainWindowViewTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "XML"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = MainWindowViewModelTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "C#",
                    CustomTags = "getDescriptionItemsTemplet"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = CollectionViewTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "XML"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = CollectionViewModelTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "C#"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = EntityViewTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "XML",
                    CustomTags = "getViewTabs"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = MainWindowViewModelModuleDescriptionItemsTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "XML"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = EntityViewModelTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "C#",
                    CustomTags = "getEntityViewModelLookups,getEntityViewModelSkips,getEntityViewModelDetails"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = EntityViewModelLookupTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "C#",
                    CustomTags =
                        "TargetDBSetName,TargetEntityTypeName,TargetPKNavProperty,TargetPKNavIDProperty,TargetPKTypeName,ParentNavProperty"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = EntityViewModelDetailsTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "C#",
                    CustomTags =
                        "MethodName,TargetDBSetName,TargetEntityTypeName,TargetPKNavProperty,TargetPKNavIDProperty,TargetPKTypeName,ParentNavProperty"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = EntityViewModelSkipsTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "C#",
                    CustomTags =
                        "MethodName,TargetDBSetName,TargetEntityTypeName,TargetPKNavProperty,TargetPKNavIDProperty,TargetPKTypeName,ContextPropertyName,ParentNavProperty"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = CodeBehindTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "C#",
                    CustomTags = "ClassName"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = GridColumnTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "XML",
                    CustomTags = "ColumnName,Name,TypeName"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = DataLayoutItemTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "XML",
                    CustomTags = "ColumnName,Name,TypeName"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = ViewTabsTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "XML",
                    CustomTags = "CollectionTabTemplet,SkipTabTemplet,OtherTemplet"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = CollectionTabTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "XML",
                    CustomTags = "DetailsName,Localization"
                });
            ti.Add(
                new TemplatedItem
                {
                    Name = SkipTabTemplet,
                    TempletText = string.Empty,
                    Version = string.Empty,
                    CodeType = "XML",
                    CustomTags = "DetailsName,Localization"
                });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = DesignTimeUnitOfWorkItemFileTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "C#",
            //        CustomTags = string.Empty
            //    });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = DesignTimeUnitOfWorkFileTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "C#",
            //        CustomTags = "GetDesignTimeUnitOfWorkItem"
            //    });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = UnitOfWorkItemFileTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "C#"
            //    });
            //ti.Add(1
            //    new TemplatedItem
            //    {
            //        Name = UnitOfWorkFileTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "C#",
            //        CustomTags = "GetUnitOfWorkItem"
            //    });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = IUnitOfWorkItemFileTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "C#"
            //    });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = IUnitOfWorkFileTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "C#",
            //        CustomTags = "GetIUnitOfWorkItem"
            //    });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = MetadataFileTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "C#",
            //        CustomTags = "GetMetadataItem"
            //    });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = MetadataItemFileTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "C#"
            //    });
            //ti.Add(
            //    new TemplatedItem
            //    {
            //        Name = UnitOfWorkSourceFileTemplet,
            //        TempletText = string.Empty,
            //        Version = string.Empty,
            //        CodeType = "C#"
            //    });
            return new ObservableCollection<TemplatedItem>(ti.OrderBy(x => x.Name));
        }

        public override async Task RunScaffoldingAsync()
        {
            IsRunningScaffolding = true;

            await this.AddOrUpdateCSPROJPropertyGroupAsync();
            await AddRequiredFilesAsync();
            ProcessMainContextViewTemplet();
            ProcessMainContextViewModelTemplet();
            foreach(EntityScaffoldInfo esi in EntityScaffoldInfos)
            {
                if(!esi.Ignore)
                {
                    ProcessCollectionViewTemplet(esi);
                    ProcessCollectionViewModelTemplet(esi);
                    ProcessEntityViewTemplet(esi);
                    ProcessEntityViewModelTemplet(esi);
                }
            }

            IsRunningScaffolding = false;
        }
    }
}

//mergeItemList.Add(
//    MergeItemHelper.GetDesignTimeUnitOfWorkFile,
//    (m, esi, p) =>
//    {
//        var sbItems = new StringBuilder();
//        var mainTemplet = GetTemplatedItem(DesignTimeUnitOfWorkFileTemplet);
//        mainTemplet.TempletText = mainTemplet.ProcessTemplet(
//            MergeItemList,
//            EntityScaffoldInfos.FirstOrDefault());
//        foreach(EntityScaffoldInfoWPF efi in EntityScaffoldInfos)
//        {
//            if(efi.Ignore)
//                continue;
//            var itemTemplet = GetTemplatedItem(DesignTimeUnitOfWorkItemFileTemplet);
//            var child = new EntityScaffoldInfoWPF();
//            child.FullName = efi.FullName;
//            child.Ignore = false;
//            sbItems.AppendLine(itemTemplet.ProcessTemplet(MergeItemList, child));
//        }
//        mainTemplet.TempletTextReplace("GetDesignTimeUnitOfWorkItem", sbItems.ToString());
//        return mainTemplet.TempletText;
//    },
//    "DBContext");
//mergeItemList.Add(
//    MergeItemHelper.GetIUnitOfWorkFile,
//    (m, esi, p) =>
//    {
//        var sbItems = new StringBuilder();
//        var mainTemplet = GetTemplatedItem(IUnitOfWorkFileTemplet);
//        mainTemplet.TempletText = mainTemplet.ProcessTemplet(
//            MergeItemList,
//            EntityScaffoldInfos.FirstOrDefault());
//        foreach(EntityScaffoldInfoWPF efi in EntityScaffoldInfos)
//        {
//            if(efi.Ignore)
//                continue;
//            var itemTemplet = GetTemplatedItem(IUnitOfWorkItemFileTemplet);
//            var child = new EntityScaffoldInfoWPF();
//            child.FullName = efi.FullName;
//            child.Ignore = false;
//            sbItems.AppendLine(itemTemplet.ProcessTemplet(MergeItemList, child));
//        }
//        mainTemplet.TempletTextReplace("GetIUnitOfWorkItem", sbItems.ToString());
//        return mainTemplet.TempletText;
//    },
//    "DBContext");
//mergeItemList.Add(
//    MergeItemHelper.GetUnitOfWorkFile,
//    (m, esi, p) =>
//    {
//        var sbItems = new StringBuilder();
//        var mainTemplet = GetTemplatedItem(UnitOfWorkFileTemplet);
//        mainTemplet.TempletText = mainTemplet.ProcessTemplet(
//            MergeItemList,
//            EntityScaffoldInfos.FirstOrDefault());
//        foreach(EntityScaffoldInfoWPF efi in EntityScaffoldInfos)
//        {
//            if(efi.Ignore)
//                continue;
//            var itemTemplet = GetTemplatedItem(UnitOfWorkItemFileTemplet);
//            var child = new EntityScaffoldInfoWPF();
//            child.FullName = efi.FullName;
//            child.Ignore = false;
//            sbItems.AppendLine(itemTemplet.ProcessTemplet(MergeItemList, child));
//        }
//        mainTemplet.TempletTextReplace("GetUnitOfWorkItem", sbItems.ToString());
//        return mainTemplet.TempletText;
//    },
//    "DBContext");
//mergeItemList.Add(
//    MergeItemHelper.GetMetadataFile,
//    (m, esi, p) =>
//    {
//        var sbItems = new StringBuilder();
//        var mainTemplet = GetTemplatedItem(MetadataFileTemplet);
//        mainTemplet.TempletText = mainTemplet.ProcessTemplet(
//            MergeItemList,
//            EntityScaffoldInfos.FirstOrDefault());
//        foreach(EntityScaffoldInfoWPF efi in EntityScaffoldInfos)
//        {
//            if(efi.Ignore)
//                continue;
//            var itemTemplet = GetTemplatedItem(MetadataItemFileTemplet);
//            var child = new EntityScaffoldInfoWPF();
//            child.FullName = efi.FullName;
//            child.Ignore = false;
//            sbItems.AppendLine(itemTemplet.ProcessTemplet(MergeItemList, child));
//        }
//        mainTemplet.TempletTextReplace("GetMetadataItem", sbItems.ToString());
//        return mainTemplet.TempletText;
//    },
//    "DBContext");
//mergeItemList.Add(
//    MergeItemHelper.GetUnitOfWorkSourceFile,
//    (m, esi, p) =>
//    {
//        var sbItems = new StringBuilder();
//        var mainTemplet = GetTemplatedItem(UnitOfWorkSourceFileTemplet);
//        mainTemplet.TempletText = mainTemplet.ProcessTemplet(
//            MergeItemList,
//            EntityScaffoldInfos.FirstOrDefault());
//        return mainTemplet.TempletText;
//    },
//    "DBContext");

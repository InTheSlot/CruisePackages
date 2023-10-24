using CruisePackage.Common;
using System;
using System.ComponentModel;

namespace BlazorServerVanillaCruisePackage
{
    public class ProjectInfoVanillaBlazorServer : ProjectInfo
    {
        string collectionPageName = "CollectionPage";
        string dataFolderName = "Data";
        string editPageName = "EditPage";
        string pagesFolderName = "Pages";
        string resourcesFolderName = "Resources";
        string tabComponentName = "TabComponent";

        public ProjectInfoVanillaBlazorServer()
        {
        }

        [Category("PageNames")]
        [Description("Name of Collection Pages")]
        [DisplayName("Collection Page Name")]
        public string CollectionPageName
        {
            get => collectionPageName;
            set
            {
                if(collectionPageName == value)
                    return;
                collectionPageName = value;
                OnPropertyChanged();
            }
        }
        [Category("Folders")]
        [Description("Path to the Data Folder of the project")]
        [DisplayName("Resources Folder Name")]
        public string DataFolderName
        {
            get => dataFolderName;
            set
            {
                if(dataFolderName == value)
                    return;
                dataFolderName = value;
                OnPropertyChanged();
            }
        }
        [Category("PageNames")]
        [Description("Name of Edit Pages")]
        [DisplayName("Edit Page Name")]
        public string EditPageName
        {
            get => editPageName;
            set
            {
                if(editPageName == value)
                    return;
                editPageName = value;
                OnPropertyChanged();
            }
        }
        [Category("Folders")]
        [Description("Path to the Folders directory of the project")]
        [DisplayName("Pages Folder Name")]
        public string PagesFolderName
        {
            get => pagesFolderName;
            set
            {
                if(pagesFolderName == value)
                    return;
                pagesFolderName = value;
                OnPropertyChanged();
            }
        }
        [Category("Folders")]
        [Description("Path to the Resources directory of the project, Leave empty to not create Resource files")]
        [DisplayName("Resources Folder Name")]
        public string ResourcesFolderName
        {
            get => resourcesFolderName;
            set
            {
                if(resourcesFolderName == value)
                    return;
                resourcesFolderName = value;
                OnPropertyChanged();
            }
        }
        [Category("PageNames")]
        [Description("Name of Tab Component")]
        [DisplayName("Tab Component Name")]
        public string TabComponentName
        {
            get => tabComponentName;
            set
            {
                if(tabComponentName == value)
                    return;
                tabComponentName = value;
                OnPropertyChanged();
            }
        }
    }
}
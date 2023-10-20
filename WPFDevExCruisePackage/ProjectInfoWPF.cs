using CruisePackage.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace WPFDevExCruisePackage
{
    public class ProjectInfoWPF : ProjectInfo
    {
        string viewFolderName = "Views";
        string viewModelFolderName = "ViewModels";

        public ProjectInfoWPF()
        {
        }

        [Display(
            Name = "View Folder Name",
            GroupName = "Folders",
            Description = "Provides the Folder name to use when placing Views",
            Order = 4)]
        public string ViewFolderName
        {
            get => viewFolderName;
            set
            {
                if(viewFolderName == value)
                    return;
                viewFolderName = value;
                OnPropertyChanged();
            }
        }
        [Display(
            Name = "ViewModel Folder Name",
            GroupName = "Folders",
            Description = "Provides the Folder name to use when placing Viewmodels",
            Order = 4)]
        public string ViewModelFolderName
        {
            get => viewModelFolderName;
            set
            {
                if(viewModelFolderName == value)
                    return;
                viewModelFolderName = value;
                OnPropertyChanged();
            }
        }
    }
}

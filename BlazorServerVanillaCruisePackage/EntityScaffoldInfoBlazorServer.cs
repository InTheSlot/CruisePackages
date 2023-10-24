using CruisePackage.Common;
using System;
using System.ComponentModel.DataAnnotations;

namespace BlazorServerVanillaCruisePackage
{
    public class EntityScaffoldInfoBlazorServer : EntityScaffoldInfo
    {
        bool processCollectionPage;
        bool processEditPage;
        bool processTabComponent;

        [Display(GroupName = "Process Item", Description = "Check to Procces Collection Pages", Order = 3)]
        public bool ProcessCollectionPage
        {
            get => processCollectionPage;
            set
            {
                if(processCollectionPage == value)
                    return;
                processCollectionPage = value;
                OnPropertyChanged();
            }
        }
        [Display(GroupName = "Process Item", Description = "Check to Procces Edit Pages", Order = 4)]
        public bool ProcessEditPage
        {
            get => processEditPage;
            set
            {
                if(processEditPage == value)
                    return;
                processEditPage = value;
                OnPropertyChanged();
            }
        }
        [Display(GroupName = "Process Item", Description = "Check to Procces Tab Components", Order = 5)]
        public bool ProcessTabComponent
        {
            get => processTabComponent;
            set
            {
                if(processTabComponent == value)
                    return;
                processTabComponent = value;
                OnPropertyChanged();
            }
        }
    }
}
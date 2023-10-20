using CruisePackage.Common;

namespace WPFDevExCruisePackage
{
    public class EntityScaffoldInfoWPF : EntityScaffoldInfo
    {
        bool replaceCollectionView;
        bool replaceCollectionViewModel;
        bool replaceView;
        bool replaceViewModel;

        public EntityScaffoldInfoWPF()
        {
        }

        public bool ReplaceCollectionView
        {
            get => replaceCollectionView;
            set
            {
                if(replaceCollectionView == value)
                {
                    return;
                }
                replaceCollectionView = value;
                OnPropertyChanged();
            }
        }
        public bool ReplaceCollectionViewModel
        {
            get => replaceCollectionViewModel;
            set
            {
                if(replaceCollectionViewModel == value)
                    return;
                replaceCollectionViewModel = value;
                OnPropertyChanged();
            }
        }
        public bool ReplaceView
        {
            get => replaceView;
            set
            {
                if(replaceView == value)
                {
                    return;
                }
                replaceView = value;
                OnPropertyChanged();
            }
        }
        public bool ReplaceViewModel
        {
            get => replaceViewModel;
            set
            {
                if(replaceViewModel == value)
                    return;
                replaceViewModel = value;
                OnPropertyChanged();
            }
        }
    }
}
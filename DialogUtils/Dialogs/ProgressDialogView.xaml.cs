using System.Windows.Controls;

namespace DialogUtils.Dialogs
{
    /// <summary>
    /// Interaction logic for ProgressDialogView.xaml
    /// </summary>
    public partial class ProgressDialogView : UserControl
    {
        public ProgressDialogView(ProgressDialogViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

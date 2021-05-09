using System.Windows.Controls;

namespace DialogUtils.Dialogs
{
    /// <summary>
    /// Interaction logic for InputDialogView.xaml
    /// </summary>
    public partial class InputDialogView : UserControl
    {
        public InputDialogView(InputDialogViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

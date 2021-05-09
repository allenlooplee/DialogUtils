using System.Windows.Controls;

namespace DialogUtils.Dialogs
{
    /// <summary>
    /// Interaction logic for MessageDialogView.xaml
    /// </summary>
    public partial class MessageDialogView : UserControl
    {
        public MessageDialogView(MessageDialogViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}

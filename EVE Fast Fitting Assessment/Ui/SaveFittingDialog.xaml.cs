using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EVE_Fast_Fitting_Assessment.Ui
{
    /// <summary>
    /// Interaction logic for SaveFittingDialog.xaml
    /// </summary>
    public partial class SaveFittingDialog : Window
    { 
        public SaveFittingDialog()
        {
            InitializeComponent();

            TxtName.Focus();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            //TODO validate input befor enabling ok button
            DialogResult = true;
            Close();
        }

        public static bool TryGetNameAndDescription(  out string name, out string description)
        {
            var prompt = new SaveFittingDialog();
            prompt.UpdateLayout();

            if (prompt.ShowDialog().GetValueOrDefault())
            {
                name = prompt.TxtName.Text;
                description = prompt.TxtDescription.Text;
                return true;
            }

            name = null;
            description = null;
            return false;
        }
    }
}

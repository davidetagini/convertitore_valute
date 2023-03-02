using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CurrencyConverter
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            BindCurrency();
        }


        #region metodi_app_dacaricare
        private void BindCurrency()
        {
            DataTable datacurrency = new DataTable();

            datacurrency.Columns.Add("Text");
            datacurrency.Columns.Add("Value");

            datacurrency.Rows.Add("Seleziona", 0);

            datacurrency.Rows.Add("INR", 1); // nomi valute
            datacurrency.Rows.Add("USD", 75);
            datacurrency.Rows.Add("EUR", 85);
            datacurrency.Rows.Add("SAR", 20);
            datacurrency.Rows.Add("POUND", 5);
            datacurrency.Rows.Add("DEM", 43);

            cmbFromCurrency.ItemsSource = datacurrency.DefaultView;
            cmbFromCurrency.DisplayMemberPath = "Text";
            cmbFromCurrency.SelectedValuePath = "Value";
            cmbFromCurrency.SelectedIndex = 0; // si parte da 0

            cmbToCurrency.ItemsSource = datacurrency.DefaultView;
            cmbToCurrency.DisplayMemberPath = "Text";
            cmbToCurrency.SelectedValuePath = "Value";
            cmbToCurrency.SelectedIndex = 0;
        }

        private void PulisciCampi()
        {
            if (txtCurrency.Text != "" && cmbFromCurrency.Items.Count > 0 && cmbToCurrency.Items.Count > 0)
            {
                txtCurrency.Text = string.Empty;

                cmbFromCurrency.SelectedIndex = 0;

                cmbToCurrency.SelectedIndex = 0;

                lblCurrency.Content = "";

                txtCurrency.Focus();
            }

            else
            {
                MessageBox.Show("Inserire dei valori nei campi per completare il ripristino", "Convertitore monetario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void CalcoloConversione()
        {
            double ConvertedValue;

            if (txtCurrency.Text == null || txtCurrency.Text.Trim() == "")
            {
                MessageBox.Show("Si prega di inserire una valuta", "Convertitore monetario", MessageBoxButton.OK, MessageBoxImage.Information);

                txtCurrency.Focus();

                return;
            }

            else if (cmbFromCurrency.SelectedValue == null || cmbFromCurrency.SelectedIndex == 0)
            {
                MessageBox.Show("Per favore, selezionare una valuta da", "Convertitore monetario", MessageBoxButton.OK, MessageBoxImage.Information);

                cmbFromCurrency.Focus();

                return;
            }

            else if (cmbToCurrency.SelectedValue == null || cmbToCurrency.SelectedIndex == 0) 
            {
                MessageBox.Show("Per favore, selezionare una valuta in", "Convertitore monetario", MessageBoxButton.OK, MessageBoxImage.Information);

                cmbToCurrency.Focus();

                return;
            }

            if (cmbFromCurrency.Text == cmbToCurrency.Text) 
            {
                ConvertedValue = double.Parse(txtCurrency.Text);

                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }

            else
            {
                ConvertedValue = (double.Parse(cmbFromCurrency.SelectedValue.ToString()) * double.Parse(txtCurrency.Text)) / double.Parse(cmbToCurrency.SelectedValue.ToString());

                lblCurrency.Content = cmbToCurrency.Text + " " + ConvertedValue.ToString("N3");
            }
        }
        #endregion

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");

            e.Handled = regex.IsMatch(e.Text);
        }

        private void Convert_Click(object sender, RoutedEventArgs e)
        {
            CalcoloConversione(); // caricato in un metodo per ottimizzare il programma
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            PulisciCampi();
        }
    }
}

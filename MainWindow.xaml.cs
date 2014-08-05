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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Linq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OrderDataClassesDataContext OrderDbContext = new OrderDataClassesDataContext();

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;

            Customer firstCust = (from c in OrderDbContext.Customers select c).FirstOrDefault();
            CustOrder firstOrder = (from o in OrderDbContext.CustOrders select o).FirstOrDefault();
            MessageBox.Show("Den første customer er " + firstCust.CustName);
            MessageBox.Show("Den første ordre er " + firstOrder.Customer.CustName);

            string FirstCustFirstOrderFirstItemProductName =
                (from i in
                     (from o in
                          (from c in OrderDbContext.Customers
                           select c).FirstOrDefault().CustOrders
                      select o).FirstOrDefault().OrderItems
                 select i).FirstOrDefault().Product.ProdName;
            MessageBox.Show("Første item var " + FirstCustFirstOrderFirstItemProductName);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
       

            fillListCustomer();
        }

        public void fillListCustomer()
        {
            var customers = from c in OrderDbContext.Customers select c;
            //var customers = from c in OrderDbContext.Customers select new 
            //{
            //    CustomerNumber = c.CustNo,
            //    CustomerName = c.CustName
            //};
            //foreach(var cust in customers)
            //{
            //    MessageBox.Show(cust.CustomerName);
            //    MessageBox.Show(cust.CustomerNumber);
            //}
            cmbCustomer.DataContext = customers;
        }
           
    }
}

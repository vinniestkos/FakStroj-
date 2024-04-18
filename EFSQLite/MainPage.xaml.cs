using EFSQLite.Data;
using EFSQLite.Models;
using EFSQLite.Records;

namespace EFSQLite;

public partial class MainPage : ContentPage
{
	MyContext _context;
    MyContext2 context_;
    public MainPage()
	{
		_context = new();
        context_ = new();
        InitializeComponent();
		lst.ItemsSource = _context.Customers.ToList(); // připojení zdroje dat k ListView
        lst2.ItemsSource = context_.Suppliers.ToList(); 
    }

    private async void SupplierButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SupplierRecords());
    }

    private async void CustomerButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CustomerRecords());
    }

    private void UlozObjednavku(object sender, EventArgs e)
    {
        Customer newCustomer = new Customer

        {
            Name = forCustomerName.Text,
            Address = forCustomerAddress.Text,
            City = forCustomerCity.Text,
            PSC = forCustomerPSC.Text,
            ICO= forCustomerICO.Text,
            DIC= forCustomerDIC.Text
        };

        _context.Add(newCustomer); // přidá záznam do Data Setu
        _context.SaveChanges(); // uloží změny do databáze !!!!!!


        Supplier newSupplier = new Supplier
        {
            Name = forSupplierName.Text,
            Address = forSupplierAddress.Text,
            City = forSupplierCity.Text,
            PSC = forSupplierPSC.Text,
            ICO = forSupplierICO.Text,
            DIC = forSupplierDIC.Text
        };
        context_.Add(newSupplier); 
        context_.SaveChanges(); 

        refresh();
    }

    //private void Smazat(object sender, EventArgs e)
    //{
    //    Customer keSmazani = lst.SelectedItem as Customer;
    //    if (keSmazani != null)
    //    {
    //        _context.Customers.Remove(keSmazani); // odebrání Customera z data setu
    //        _context.SaveChanges(); // uloží změny do databáze
    //        refresh();
    //    }
    //}

    private async void Detajly(object sender, EventArgs e)
    {
        int id = (lst.SelectedItem as Customer).Id;
        CustomerRecords dp = new(id, _context);
        await Navigation.PushAsync(dp);
    }

    private async void Detajly2(object sender, EventArgs e)
    {
        int id = (lst2.SelectedItem as Customer).Id;
        SupplierRecords dp2 = new(id, context_);
        await Navigation.PushAsync(dp2);
    }

    void refresh()
    {
        lst.ItemsSource = null;
        lst.ItemsSource = _context.Customers.ToList();

        lst2.ItemsSource = null;
        lst2.ItemsSource = context_.Suppliers.ToList();
    }


}


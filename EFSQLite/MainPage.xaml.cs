using EFSQLite.Data;
using EFSQLite.Models;
using EFSQLite.Records;
using Microsoft.EntityFrameworkCore.Internal;

using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using System.Diagnostics;
using System.Net;
using System.Xml.Linq;

namespace EFSQLite;


public partial class MainPage : ContentPage
{
    
	MyContext _context;
    MyContext2 context_;
    public MainPage()
	{
        QuestPDF.Settings.License = LicenseType.Community;

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
               
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header()
                    .Text("Hello PDF!");


                page.Content()
                    .PaddingVertical(1, Unit.Centimetre)
                    .Column(x =>
                    {
                        x.Spacing(20);

                        x.Item().Text(Placeholders.LoremIpsum());
                        x.Item().Image(Placeholders.Image(200, 100));
                    });

                page.Footer()
                    .AlignCenter()
                    .Text(x =>
                    {
                        x.Span("Page ");
                        x.CurrentPageNumber();
                    });
            });
        })
.GeneratePdf("hello.pdf");

        //Document.Create(document =>
        //{
        //    document.Page(page =>
        //    {

        //        page.Size(PageSizes.A4);

        //        page.Header()
        //        .Text(forCustomerName);


        //    });
        //});

        _context = new();
        context_ = new();
        QuestPDF.Settings.License = LicenseType.Community;
        InitializeComponent();
        lst.ItemsSource = _context.Customers.ToList();
        lst2.ItemsSource = context_.Suppliers.ToList(); // připojení zdroje dat k ListView

    }

    private async void SupplierButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SupplierRecords());
     
    }

    private async void CustomerButton(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CustomerRecords());
    }
    void refresh()
    {
        lst.ItemsSource = null;
        lst.ItemsSource = _context.Customers.ToList();
        lst2.ItemsSource = null;
        lst2.ItemsSource = context_.Suppliers.ToList();
    }
    public void UlozObjednavku(object sender, EventArgs e)
    {
      

        Customer newCustomer = new Customer

        {
            Name = forCustomerName.Text,
            Address = forCustomerAddress.Text,
            City = forCustomerCity.Text,
            PSC = forCustomerPSC.Text,
            ICO= forCustomerICO.Text,
            DIC= forCustomerDIC.Text,
            Product = produkt.Text,
            Quantity = množství.Text,
            Price = cena.Text
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
            DIC = forSupplierDIC.Text,
            Product = produkt.Text,
            Quantity = množství.Text,
            Price = cena.Text
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


   



   


}


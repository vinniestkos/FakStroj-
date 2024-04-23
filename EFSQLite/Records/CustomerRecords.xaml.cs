using EFSQLite.Data;
using EFSQLite.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;

namespace EFSQLite;

public partial class CustomerRecords : ContentPage
{
    new public string Id { get; set; }
    public string Name { get; set; }

    public string Address { get; set; }

    public string City { get; set; }

    public string PSC { get; set; }

    public string ICO { get; set; }

    public string DIC { get; set; }

  

    MyContext _context;

    public CustomerRecords()
    {
        lst.ItemsSource = _context.Customers.ToList();
        InitializeComponent();
        BindingContext = this;
    }

    public CustomerRecords (int id, MyContext context)
    {
        _context = context;

        Customer c = (from customer in context.Customers
                      where customer.Id == id
                      select customer).FirstOrDefault();

        if (c != null)
        {
            Id = $"odbìratel s id:{c.Id}";
            Name = $"jméno odbìratele: {c.Name}";
            Address = $"adresa odbìratele: {c.Address}, {c.PSC} {c.City}";
            ICO = $"IÈO odbìratele s: {c.ICO}";
            DIC = $"DIÈ odbìratele s: {c.DIC}";
        }
    }



   
    private async void Detajly(object sender, EventArgs e)
    {
        int id = (lst.SelectedItem as Customer).Id;
        CustomerRecords dp = new(id, _context);
        await Navigation.PushAsync(dp);
    }
    void refresh()
    {
        lst.ItemsSource = null;
        lst.ItemsSource = _context.Customers.ToList();
    }
    

    private void Button_Clicked(object sender, EventArgs e)
    {
        refresh();
    }
}

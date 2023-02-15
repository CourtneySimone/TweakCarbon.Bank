// See https://aka.ms/new-console-template for more information
using TweakBank.Models;
using TweakBank.Repository;

Console.WriteLine("Hello, World!");


CustomerRepository Repo = new CustomerRepository();

Customer CUST = new Customer() { FirstName = "johno"};
Repo.InsertRecord(CUST);

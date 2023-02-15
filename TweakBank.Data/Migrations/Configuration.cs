namespace TweakBank.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TweakBank.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TweakBank.Data.TweakBankContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "TweakBank.Data.TweakBankContext";
        }

        protected override void Seed(TweakBank.Data.TweakBankContext context)
        {
          var transactiontype1 = new TransactionType() { TransactionName = "Transfer" };
          var transactiontype2 = new TransactionType() { TransactionName = "Withdraw" };
          var transactiontype3 = new TransactionType() { TransactionName = "GetBalance" };
          var transactiontype4 = new TransactionType() { TransactionName = "GetHistory" };
          var transactiontype5 = new TransactionType() { TransactionName = "CreateCustomer" };
          var transactiontype6 = new TransactionType() { TransactionName = "Deposit" };
          var transactiontype7 = new TransactionType() { TransactionName = "SendEWallet" };
          var transactiontype8 = new TransactionType() { TransactionName = "GetAllAccounts" };
          var transactiontype9 = new TransactionType() { TransactionName = "GetCustomers" };
          var transactiontype10 = new TransactionType() { TransactionName = "recievedFunds" };
          var transactiontype11 = new TransactionType() { TransactionName = "CreateAccount" };

            context.TransactionTypes.Add(transactiontype1);
            context.TransactionTypes.Add(transactiontype2);
            context.TransactionTypes.Add(transactiontype3);
            context.TransactionTypes.Add(transactiontype4);
            context.TransactionTypes.Add(transactiontype5);
            context.TransactionTypes.Add(transactiontype6);
            context.TransactionTypes.Add(transactiontype7);
            context.TransactionTypes.Add(transactiontype8);
            context.TransactionTypes.Add(transactiontype9);
            context.TransactionTypes.Add(transactiontype10);
            context.SaveChanges();

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}

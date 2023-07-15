using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConsoleApp1.Entities;
using ConsoleApp1.Providers;

namespace ConsoleApp1
{
	public delegate bool DoAfterGetAddresses(List<CustomerAddress> ListOfAddresses);

	public class MarketingDepartment
	{

		public bool ExecuteNewCampaign(decimal budget)
		{
			bool success = false;

			AddressProvider MyAddressProvider = new AddressProvider();

			// here I will declare a delegate object of the delegate type above
			// I will not instantiate it yet, because that depends on the budget
			DoAfterGetAddresses ToDoAfterAddresses;

			if (budget < 10000)
			{
				BallpenCompany MyBallpenCompany = new BallpenCompany();

				// here I'll instantiate the delegate object (Step 2)
				// I need to assign a method with the same signature as declared
				// Also notice that I don't need to give the parameter yet
				ToDoAfterAddresses = MyBallpenCompany.SendBallPens;
				foreach (var item in AddressProvider.GetAddressesNewProspects())
				{
					Console.WriteLine($" {item.Name} Müşterisine Kalem Gönderildi");
				}
				
			}
			else
			{
				CoffeeCupCompany MyCoffeeCupCompany = new CoffeeCupCompany();

				//same here
				ToDoAfterAddresses = MyCoffeeCupCompany.SendCoffeeCups;
				foreach (var item in AddressProvider.GetAddressesNewProspects())
				{
					Console.WriteLine($" {item.Name} Müşterisine Kahve Takımı Gönderildi");
				}
			}

			// it's now time to let the AddressProvider handle the campaign
			// note how we pass the delegate instance as a parameter
			success = MyAddressProvider.HandleCampaign(ToDoAfterAddresses);

			return success;
		}
	}

	public class AddressProvider
	{
		//here's the method that accepts the delegate object as parameter
		public bool HandleCampaign(DoAfterGetAddresses ToDoAfterAddresses)
		{
			bool success = false;

			//get the addresses first
			List<CustomerAddress> ListOfAddresses = GetAddressesNewProspects();

			//now invoke the delegate (Step 3)
			//at this stage, the ListOfAddresses is needed to pass as argument
			success = ToDoAfterAddresses(ListOfAddresses);

			// this means that the AddressProvider does NOT know what method
			// exactly has been called : MyBallpenCompany.SendBallPens
			// or MyCoffeeCupCompany.SendCoffeeCups
			// that was the decision of the MarketingDepartment

			// Furthermore, the signature tells me that the 
			// delegated method will return a boolean, 
			// that I can return that to the MarketingDepartment

			return success;
		}

		public static List<CustomerAddress> GetAddressesNewProspects()
		{
			List<CustomerAddress> customerAddresses = new List<CustomerAddress>();
			customerAddresses.Add(new CustomerAddress() { Name = "Idealworks Bilişim", City = "Bursa" });
			customerAddresses.Add(new CustomerAddress() { Name = "Coder Bilişim", City = "Konya" });
			customerAddresses.Add(new CustomerAddress() { Name = "Mold Kalıp", City = "Bursa" });
			return customerAddresses;


		}
	}


}

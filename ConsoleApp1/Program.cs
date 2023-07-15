namespace ConsoleApp1
{
	internal class Program
	{
		static void Main(string[] args)
		{
			
			MarketingDepartment marketingDepartment = new MarketingDepartment();
            Console.WriteLine("Bütçe Tutarı Giriniz :");
			decimal result=Convert.ToDecimal(Console.ReadLine());
            marketingDepartment.ExecuteNewCampaign(result);

        }
	}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace AddressCleanse_HttpClient
{
    class Program
    {
        static void Main(string[] args)
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                try
                {
                    var builder = new UriBuilder("https://rapid.peachtreedata.com/api/v1/AddressCleanseGeoCode");
                    builder.Port = -1;
                    var query = HttpUtility.ParseQueryString(builder.Query);
                    query["CustomerID"] = "";
                    query["APIKey"] = "";
                    query["CompanyName"] = "Peachtree Data";
                    query["Address1"] = "2905 Premiere Parkway";
                    query["ZIPCode"] = "30097";
                    builder.Query = query.ToString();
                    string url = builder.ToString();

                    Console.WriteLine("URL: {0}\n\n", url);

                    HttpResponseMessage response = await client.GetAsync(url);
                    
                    response.EnsureSuccessStatusCode();

                    AddressCleanseGeoCodeResponse address = await response.Content.ReadAsAsync<AddressCleanseGeoCodeResponse>();

                    Console.WriteLine("Primary Address:   {0}", address.PrimaryAddress);
                    Console.WriteLine("Secondary Address: {0}", address.SecondaryAddress);
                    Console.WriteLine("ZIP Code:          {0}", address.Zip10);
                    Console.WriteLine("Latitude:          {0}", address.Latitude);
                    Console.WriteLine("Longitude:         {0}", address.Longitude);
                    Console.WriteLine("Geo Assignment:    {0}", address.AssignmentLevel);
                    Console.WriteLine("Status:            {0}", address.StatusMessage);
                    Console.WriteLine("\n\nPress any key to continue...");
                    Console.ReadKey();
                }
                catch (HttpRequestException e)
                {
                    Console.WriteLine("\nException Caught!");
                    Console.WriteLine("Message: {0}", e.Message);
                    Console.WriteLine("\n\nPress any key to continue...");
                    Console.ReadKey();
                }

            }
        }
    }

    public class AddressCleanseGeoCodeResponse
    {
        public string Status { get; set; }
        public string StatusMessage { get; set; }
        public string RawDetailCode { get; set; }
        public string AddressID { get; set; }
        public string PrimaryAddress { get; set; }
        public string PrimaryNumber { get; set; }
        public string PrimaryPrefix { get; set; }
        public string PrimaryName { get; set; }
        public string PrimaryType { get; set; }
        public string PrimaryPostfix { get; set; }
        public string PrimaryAddressRemainder { get; set; }
        public string SecondaryAddress { get; set; }
        public string SecondaryUnitDescription { get; set; }
        public string SecondaryUnitNumber { get; set; }
        public string City { get; set; }
        public string City13 { get; set; }
        public string State { get; set; }
        public string Zip10 { get; set; }
        public string Zip5 { get; set; }
        public string Zip4 { get; set; }
        public string AddressType { get; set; }
        public string IsDefault { get; set; }
        public string DeliveryPointBarcode { get; set; }
        public string DeliveryPointBarcodeCheckDigit { get; set; }
        public string LineOfTravel { get; set; }
        public string LineOfTravelOrder { get; set; }
        public string CarrierRoute { get; set; }
        public string CountyCode { get; set; }
        public string CountyName { get; set; }
        public string DpvFootnote { get; set; }
        public string IsDpvNoStat { get; set; }
        public string DpvStatus { get; set; }
        public string SuiteLinkResult { get; set; }
        public string IsVacant { get; set; }
        public string NonPostalSecondaryAddress { get; set; }
        public string ExtraLine1 { get; set; }
        public string ExtraLine2 { get; set; }
        public string SecondaryAddressExtraneous { get; set; }
        public string AssignmentLevel { get; set; }
        public string CensusTractBLock { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MetroStatAreaCode { get; set; }
    }
}

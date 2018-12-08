using System;
using System.IO;
using Newtonsoft.Json;

namespace Convert
{
   class Program
   {
      static void Main( string[] args )
      {
         if ( args[0] == "" || args[0] == "?" )
         {
            Console.Out.WriteLine( "help:" );
            Console.Out.WriteLine( "takes two arguments, converting from json to comma separated file:" );
            Console.Out.WriteLine( "input file (the json format)" );
            Console.Out.WriteLine( "output file (the result generated as csv)" );
         }
         BookingsData data= LoadJson(args[0]);
         data.ExportData( args[1] );
      }

      public static BookingsData LoadJson( string args )
      {
         using ( StreamReader r = new StreamReader( args ) )
         {
            string json = r.ReadToEnd();
            return JsonConvert.DeserializeObject<BookingsData>( json );
         }
      }

      public class Data
      {
         [JsonProperty("bookings")]
         public Booking[] bookings;
      }

      public class BookingsData
      {
         [JsonProperty("message")]
         public string message;
         [JsonProperty("data")]
         public Data data;
         [JsonProperty("status")]
         public string status;

         public void ExportData( string file )
         {
            using ( StreamWriter wr = new StreamWriter( file ) )
            {
               wr.WriteLine( "Facility, Date, Start, End" );
               foreach ( var booking in data.bookings )
               {
                  booking.ExportData( wr );
               }
            }
         }
      }

      public class Booking
      {
         [JsonProperty("id_booking")]
         public int id_booking;
         [JsonProperty("start_timestamp")]
         public DateTime start_timestamp;
         [JsonProperty("end_timestamp")]
         public DateTime end_timestamp;
         [JsonProperty("id_category_facility")]
         public int id_category_facility;
         [JsonProperty("id_facility")]
         public int id_facility;
         [JsonProperty("id_user")]
         public int id_user;
         [JsonProperty("timestamp")]
         public DateTime timestamp;
         [JsonProperty("paid")]
         public int paid;
         [JsonProperty("price")]
         public int price;
         [JsonProperty("total_paid")]
         public int total_paid;
         [JsonProperty("total_due_to_pay")]
         public int total_due_to_pay;

         //Facility , Date,  Start, End.
         public void ExportData( StreamWriter wr )
         {
            wr.Write( id_facility );
            wr.Write( "," );
            string date = start_timestamp.ToString("dd-MM-yy");
            wr.Write( date );
            wr.Write( "," );
            wr.Write( start_timestamp.ToString( "HH" ) + ":" + start_timestamp.ToString( "mm" ) );
            wr.Write( "," );
            wr.Write( end_timestamp.ToString( "HH" ) + ":" + end_timestamp.ToString( "mm" ) );
            wr.Write( Environment.NewLine );
         }
      }
   }
}

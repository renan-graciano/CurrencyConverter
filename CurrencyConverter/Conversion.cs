using System;
using System.Collections.Generic;
using MasterCard.Api.CurrencyConversion;
using MasterCard.Core;
using MasterCard.Core.Exceptions;
using MasterCard.Core.Model;
using MasterCard.Core.Security.OAuth;

namespace CurrencyConverter
{
    class Conversion
    {
        static void Main(string[] args)
        {
            string consumerKey = "jzAnk11zzLt0VwgLZSvCmn-leleJcCQglxglnTLBa8ea5038!fe659264cb6d4b71b23fa346c22f54f80000000000000000";   // You should copy this from "My Keys" on your project page e.g. UTfbhDCSeNYvJpLL5l028sWL9it739PYh6LU5lZja15xcRpY!fd209e6c579dc9d7be52da93d35ae6b6c167c174690b72fa
            string keyAlias = "keyalias";   // For production: change this to the key alias you chose when you created your production key - keyalias
            string keyPassword = "testRenan2019";   // For production: change this to the key alias you chose when you created your production key - testRenan2019
            var path = MasterCard.Core.Util.GetCurrenyAssemblyPath(); // This returns the path to your assembly so it be used to locate your cert
            string certPath = @"C:\Users\rgcarneiro\Downloads\currency-converter-1553275958-sandbox.p12"; // e.g. /Users/yourname/project/sandbox.p12 | C:\Users\yourname\project\sandbox.p12

            //string consumerKey = "iPoGLRORfDZckRnCN4Eo1g7rlu_T1uVqxhMUd_Klcc7cdfca!c097a25876bf483ca5c6b3ebf92701770000000000000000";   // You should copy this from "My Keys" on your project page e.g. UTfbhDCSeNYvJpLL5l028sWL9it739PYh6LU5lZja15xcRpY!fd209e6c579dc9d7be52da93d35ae6b6c167c174690b72fa
            //string keyAlias = "ConversionKey";   // For production: change this to the key alias you chose when you created your production key - keyalias
            //string keyPassword = "ConvertRenan2019";   // For production: change this to the key alias you chose when you created your production key - testRenan2019
            //var path = MasterCard.Core.Util.GetCurrenyAssemblyPath(); // This returns the path to your assembly so it be used to locate your cert
            //string certPath = @"C:\Users\rgcarneiro\Downloads\ConversionKey-production.p12"; // e.g. /Users/yourname/project/sandbox.p12 | C:\Users\yourname\project\sandbox.p12

            ApiConfig.SetAuthentication(new OAuthAuthentication(consumerKey, certPath, keyAlias, keyPassword));   // You only need to set this once
            ApiConfig.SetDebug(false);   // Enable http wire logging
                                         // This is needed to change the environment to run the sample code. For production: ApiConfig.SetSandbox(false);
                                         //ApiConfig.SetEnvironment((Environment)Enum.Parse(typeof(Environment), "sandbox_mtf".ToUpper()));
            ApiConfig.SetSandbox(true);

            GetConversion();

        }

        private static void GetConversion()
        {
           
            try
            {
                RequestMap map = new RequestMap();
                map.Set("fxDate", "2019-03-22");
                map.Set("transCurr", "USD");
                map.Set("crdhldBillCurr", "BRL");
                map.Set("bankFee", "0");
                map.Set("transAmt", "10");
                ConversionRate response = ConversionRate.Query(map);

                Out(response, "name"); //-->settlement-conversion-rate
                Out(response, "description"); //-->Settlement conversion rate and billing amount
                Out(response, "date"); //-->2017-11-03 03:59:50
                Out(response, "data.conversionRate"); //-->0.57
                Out(response, "data.crdhldBillAmt"); //-->13.11
                Out(response, "data.fxDate"); //-->2016-09-30
                Out(response, "data.transCurr"); //-->ALL
                Out(response, "data.crdhldBillCurr"); //-->DZD
                Out(response, "data.transAmt"); //-->23
                Out(response, "data.bankFee"); //-->5
                Console.ReadKey();
            }
            catch (ApiException e)
            {
                Err("HttpStatus: {0}", e.HttpStatus.ToString());
                Err("Message: {0}", e.Message);
                Err("ReasonCode: {0}", e.ReasonCode);
                Err("Source: {0}", e.Source);
                Console.ReadKey();
            }
        }
        public static void Err(String message, String value)
        {
            Console.Error.WriteLine(message, value);
        }

        public static void Err(SmartMap response, String key)
        {
            Console.Error.WriteLine(key + "---> " + response[key]);
        }

        public static void Out(SmartMap response, String key)
        {
            Console.WriteLine(key + "---> " + response[key]);
        }

        public static void Out(Dictionary<String, Object> response, String key)
        {
            Console.WriteLine(key + "---> " + response[key]);
        }

    }
}

using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;


namespace My.Function
{
    public static class KebabExample
    {
        [FunctionName("KebabExample")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            double meatAmount;
            if (!Double.TryParse(req.Query["meatAmount"].ToString(), out meatAmount))
                return new OkObjectResult("Insert a valid meatAmount query param in the URL please 😭😭😭 ");


            List<Ingredient> IngredientList = new List<Ingredient>(); 
            IngredientList.Add(new Ingredient("🥩 Ground Lamb", 2, "kilos"));
            IngredientList.Add(new Ingredient("🧅 Small Onion", 1, "units"));
            IngredientList.Add(new Ingredient("🧄 Garlic", 4, "cloves"));
            IngredientList.Add(new Ingredient("🧂 Cumin", 1.5, "teaspoons"));
            IngredientList.Add(new Ingredient("🧂 Sumac", 1.5, "teaspoons"));
            IngredientList.Add(new Ingredient("🧂 Salt", 0.5, "teaspoons"));
            IngredientList.Add(new Ingredient("🧂 Black Pepper", 0.25, "teaspoons"));
            IngredientList.Add(new Ingredient("🌶️ Red Pepper Flakes", 2, "teaspoons"));

            string responseMessage = "List of Ingredients for " + meatAmount + "kg of meat:\n\n";
            foreach (var ing in IngredientList)
            {
                responseMessage += (ing.Name +": " +  (ing.Quantity * meatAmount)  + " " + ing.Type + "\n");
            }

            // Given that 2 kilos of meat should be around 4 servings, and each serving is around 10 cms 
            double lengthReference = 40;
            double length = (lengthReference * meatAmount/2); 
            if (length > 100) 
                responseMessage += "\nAnd it should be ~" + length/100 + " meters! 😄 \n"; 
            else 
                responseMessage += "\nAnd it should be ~" + length + " cm! 😄 \n"; 
            
            double servingReference = 4; 
            double servings = servingReference * meatAmount / 2; 
            responseMessage += "\n~" + servings + " servings! 😄 \n\n"; 

            /**
            responseMessage += "―";
            for (int i = 0; i < Math.Round(servings); i++) {
                responseMessage += "{}@{}@{}";
            }
            responseMessage += "-";*/
            return new OkObjectResult(responseMessage);
        }
    }

    public class Ingredient {
        public string Name;
        public double Quantity;
        public string Type;
        public Ingredient (string name, double quantity, string type){
            this.Name = name; 
            this.Quantity = (quantity/2); 
            this.Type = type;
        }
    }
}

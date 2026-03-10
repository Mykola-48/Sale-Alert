using Microsoft.EntityFrameworkCore;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace Course_Project_OOP_3
{
    public partial class Form1 : Form
    {
        public static List<string> itemIdList = new List<string> { "406710158545", "406710166983", "406710167500", "406710165751" };
        public static string id = "";
        public static string name = "";
        public static string currentPrice = "";
        public static decimal correctPrice;
        public static string token;
        public static string clientId = PrivateInfo.clientId;
        public static string clientSecret = PrivateInfo.clientSecret;

        public async Task GetPhonePrice()
        {
            // Protection if Token will be null
            #region
            try
            {
                token = await GetAccessToken(clientId, clientSecret);

                if (token == null)
                {
                    MessageBox.Show("Error: coudln't get an access token!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: coudln't get an access token!\n {ex}");
                return;
            }
            #endregion

            await GetAllItemsFromList(token);

            label1.Text = "Успіх! Продукт додано в SQL Express!!!!!!!";
            await Task.Delay(3000);
        }



        // Getting TOKEN for requests for Ebay API (TOKEN lasts for 2-12 hours only)
        static async Task<string> GetAccessToken(string clientId, string clientSecret)
        {
            using var client = new HttpClient();  // It is like Browser that will make requests.

            var auth = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));  // All private info for getting access

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", auth);  // Adding Headers for this request

            var content = new StringContent("grant_type=client_credentials&scope=https://api.ebay.com/oauth/api_scope", Encoding.UTF8, "application/x-www-form-urlencoded"); // Body of the request

            var response = await client.PostAsync("https://api.ebay.com/identity/v1/oauth2/token", content);  // Sending request to Ebay

            var json = await response.Content.ReadAsStringAsync(); // Read answer as json format

            using var doc = JsonDocument.Parse(json); // Taking data from json

            token = doc.RootElement.GetProperty("access_token").GetString()!; // ! Tells that here 100% NOT NULL

            return token;
        }

        async Task GetAllItemsFromList(string token)
        {
            using var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token); // Adding authorization token

            foreach (string itemId in itemIdList)
            {
                await GetSingleItem(httpClient, itemId); // Getting info one by one from list
            }
        }

        public static async Task SaveDataToBase()
        {
            try
            {
                // Creating connection with the DataBase
                using (var dbContext = new AppDbContext())
                {
                    var targetProduct = dbContext.Products.FirstOrDefault(p => p.Name == name);
                    try
                    {
                        correctPrice = decimal.Parse(currentPrice);
                    }
                    catch (FormatException)
                    {
                        correctPrice = decimal.Parse(currentPrice.Replace(".", ","));
                    }

                    // Adding values to Product table if there are not one
                    if (targetProduct == null)
                    {
                        targetProduct = new Product
                        {
                            Id = id,
                            Name = name
                        };

                        // Adding to table and saving
                        dbContext.Products.Add(targetProduct);
                        dbContext.SaveChanges();
                    }

                    if (targetProduct != null)
                    {
                        var newPrice = new PriceHistory
                        {
                            CurrentPrice = correctPrice,
                            AddedDate = DateTime.Now,
                            ProductId = targetProduct.Id
                        };

                        dbContext.PriceHistories.Add(newPrice);
                        dbContext.SaveChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                var realMessage = ex.InnerException != null ? ex.InnerException.Message : ex.Message;

                MessageBox.Show($"Конкретна помилка: {realMessage}");
            }
        }

        public static async Task GetSingleItem(HttpClient httpClient, string itemId)
        {
            try
            {
                var url = $"https://api.ebay.com/buy/browse/v1/item/v1|{itemId}|0?fieldgroups=PRODUCT"; // Item that I want to get info

                var response = await httpClient.GetAsync(url); // Getting answer form server with all info

                var json = await response.Content.ReadAsStringAsync(); // Reading main info

                using var doc = JsonDocument.Parse(json); // Converting text


                // Getting exact info that needed (name, price)
                try
                {
                    id = itemId;
                    name = doc.RootElement.GetProperty("title").GetString();
                    currentPrice = doc.RootElement.GetProperty("price").GetProperty("value").GetString();
                    var currency = doc.RootElement.GetProperty("price").GetProperty("currency").GetString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {response.StatusCode}\n {ex}");
                }
                

                //label1.Text = name + $" price = {price} {currency}";
                //richTextBox1.Text = name + $" price = {price} {currency}";
                await SaveDataToBase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex}");
            }
        }

        public static async Task AddNewItemWithData(string itemId)
        {
            if (token == null) // if token = null, we are getting new token
            {
                #region
                // Protection if Token will be null
                try
                {
                    token = await GetAccessToken(clientId, clientSecret);

                    if (token == null)
                    {
                        MessageBox.Show("Error: coudln't get an access token!");
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: coudln't get an access token!\n {ex}");
                    return;
                }
                #endregion
            }


            itemIdList.Add(itemId); // adding to List new item

            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            await GetSingleItem(httpClient, itemId);
        }
    }
}






























                                                    // WEB SCRAPING WITH PLAYWRIGHT

//using Microsoft.EntityFrameworkCore;
//using Microsoft.Playwright;

//namespace Course_Project_OOP_3
//{
//    public partial class Form1 : Form
//    {
//        public async Task GetPhonePrice()
//        {
//            // Clean table PriceHistory
//            #region
//            bool cleanHistory = false;
//            if (cleanHistory == true)
//            {
//                using (var db = new AppDbContext())
//                {
//                    // Очищаємо таблицю історії цін
//                    db.Database.ExecuteSqlRaw("DELETE FROM PriceHistories");

//                    // Якщо хочеш обнулити ID (лічильник), додай цей рядок:
//                    db.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PriceHistories', RESEED, 0)");

//                    db.SaveChanges();
//                }
//                using (var db = new AppDbContext())
//                {
//                    // Очищаємо таблицю Products
//                    db.Database.ExecuteSqlRaw("DELETE FROM Products");

//                    // Якщо хочеш обнулити ID (лічильник), додай цей рядок:
//                    db.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Products', RESEED, 0)");

//                    db.SaveChanges();
//                }
//            }
//            #endregion

//            using var playwright = await Playwright.CreateAsync();
//            await using var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });

//            var page1 = await browser.NewPageAsync();
//            var page2 = await browser.NewPageAsync();
//            var page3 = await browser.NewPageAsync();
//            var page4 = await browser.NewPageAsync();

//            var pagesArray = new[] { page1, page2, page3, page4 };

//            await page1.GotoAsync("https://rozetka.com.ua/ua/482583919/p482583919/"); // 13
//            await page2.GotoAsync("https://rozetka.com.ua/ua/571698364/p571698364/"); // 14
//            await page3.GotoAsync("https://rozetka.com.ua/ua/realme-15t-12-256gb-suit-titanium/p544272245/"); // 15
//            await page4.GotoAsync("https://rozetka.com.ua/ua/samsung-sm-s942bzvheuc/p570560113/");// Samsung



//            foreach (var page in pagesArray)
//            {
//                var dataArray = await ScrapingExactData(page);

//                this.label1.Text = dataArray[0];
//                this.label2.Text = dataArray[1];
//                //this.label3.Text = dataArray[2];

//                try
//                {
//                    // 1. Створюємо зв'язок з базою через твій клас
//                    using (var db = new AppDbContext())
//                    {
//                        var targetProduct = db.Products.FirstOrDefault(p => p.Name == dataArray[0]);
//                        var cleanPrice = dataArray[1].Replace(" ", "").Replace("₴", "");

//                        // Adding to values to Product table if there are not one
//                        if (targetProduct == null)
//                        {
//                            targetProduct = new Product
//                            {
//                                Name = dataArray[0],
//                            };

//                            // 3. Додаємо в список і зберігаємо
//                            db.Products.Add(targetProduct);
//                            db.SaveChanges();
//                        }

//                        if (targetProduct != null)
//                        {
//                            var newPrice = new PriceHistory
//                            {
//                                CurrentPrice = decimal.Parse(cleanPrice),
//                                AddedDate = DateTime.Now,
//                                ProductId = targetProduct.Id
//                            };

//                            db.PriceHistories.Add(newPrice);
//                            db.SaveChanges();
//                            //MessageBox.Show("Успіх! Продукт додано в SQL Express.");
//                        }
//                    }
//                }
//                catch (Exception ex)
//                {
//                    var realMessage = ex.InnerException != null
//        ? ex.InnerException.Message
//        : ex.Message;

//                    MessageBox.Show($"Конкретна помилка: {realMessage}");
//                }
//            }

//            //MessageBox.Show("Browser is working...");   // Stopping browser from closing itself
//        }

//        public async Task<string[]> ScrapingExactData(IPage page) // Asynk function that will return Array(string) with all needed information about product
//        {
//            string name = await GetInnerTextSafe(page, "h1.title__font", "No Name");                            // Product name
//            string currentPrice = await GetInnerTextSafe(page, "p.product-price__big", "No current price");     // Current price
//            //string previousPrice = await GetInnerTextSafe(page, "p.product-price__small", "No previous price"); // Previous(before sale) price

//            return new string[] { name, currentPrice }; //previousPrice };
//        }

//        public async Task<string> GetInnerTextSafe(IPage page, string selector, string defaultValue) // Getting Names and praces
//        {
//            try
//            {
//                return await page.Locator(selector).InnerTextAsync();
//            }
//            catch
//            {
//                return defaultValue;
//            }
//        }
//    }
//}
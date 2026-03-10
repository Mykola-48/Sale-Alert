using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Playwright;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Course_Project_OOP_3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public async void Form1_Load(object sender, EventArgs e) 
        {
            using (var db = new AppDbContext())
            {
                db.Database.EnsureCreated();
            }
        }



        private void textBox1_MouseClick(object sender, MouseEventArgs e)
        {
            if (textBox1.Text == "Enter item number(12 numbers)")
            {
                textBox1.Text = "";
            }
        }


        // IMPORTANT
        public async void AddNewItemButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 12 && textBox1.Text.All(char.IsDigit)) // Checking input only has numbers
            {

                string newItemId = textBox1.Text;

                await AddNewItemWithData(newItemId);
                label2.Text = "New Item Added";
            }
            else
            {
                label2.Text = "Enter correct Id only numbers(12)";
            }
        }

        private async void ProcessALLItemsInListButton_Click(object sender, EventArgs e)
        {
            await GetPhonePrice();
        }

        private async void ClearAllTablesButton_Click(object sender, EventArgs e)
        {
            // Clean all tables
            using (var db = new AppDbContext())
            {
                // Очищаємо таблицю історії цін
                db.Database.ExecuteSqlRaw("DELETE FROM PriceHistories");

                db.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('PriceHistories', RESEED, 0)"); // auto increment starts form 1 again

                db.SaveChanges();
            }
            using (var db = new AppDbContext())
            {
                // Очищаємо таблицю Products
                db.Database.ExecuteSqlRaw("DELETE FROM Products");

                db.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Products', RESEED, 0)"); // auto increment starts form 1 again

                db.SaveChanges();
            }
            label3.Text = "ВСЕ ОЧИЩЕНО!!!!!!!!!!!";
        }

    }
}






















//Telegram Bot Sending Messages
#region  
//TOKEN = '8118671888:AAGzXDoT4KpH0hSg9j_GvBj5naMPN8QLRvY'
//CHAT_IDS = [
//    5112832505,   # Fathers bot id
//    5440773190    # My bot id
//]
//text = ''
//allTextforSending = []
//        url = f"https://api.telegram.org/bot{TOKEN}/sendMessage"
//
//FILE_PATH_Papa = "file_With_Papa_Ids.txt"
//FILE_PATH_MY = "file_With_MY_Ids.txt"
//
//TEXT_Warning = "point"           # ⚠️ SELL point GBP/CHF (Stoch = 100),  rsi = 77  ➔  85  (2025-12-03 11:24)
//TEXT_Second_Function = "candle"  # Second Function
//TEXT_Error_2 = "Found"           # [Errno 2] No such file or directory: 'D:\\AIData\\Data_3_M\\FinalData_EURUSD.csv'
//TEXT_CHECKING_INFO = "info"      # I am checking info :)
//
//lock = asyncio.Lock()
//
//async def send_Telegram_Messages_From_Array():
//    async with lock:
//        if not global_Variables.message_was_send and global_Variables.file_Is_New:
//
//            async with aiohttp.ClientSession() as session:
//                global_Variables.message_was_send = True
//
//                for text in global_Variables.allTextforSending:
//
//                    for chat_id in CHAT_IDS:
//                        payload = {
//            'chat_id': chat_id,
//                            'text': text,
//                            'parse_mode': 'HTML',
//                            'disable_notification': False    # 🔇 True = без звуку,  False = зі звуком    ########################################################################## 
//                        }
//
//                        try:
//                            async with session.post(url, data= payload) as response:
//                                result = await response.json()
//                                print(result)
//
//                                if chat_id == 5112832505:
//                                    # ⭐ Якщо текст містить фрагмент → записати тільки ID
//                                    if (TEXT_Warning in text or TEXT_Error_2 in text or TEXT_Second_Function in text) and "result" in result:
//                                        msg_id = result["result"]["message_id"]
//
//                                        with open(FILE_PATH_Papa, "a", encoding= "utf-8") as f:
//                                            f.write(f"{msg_id}\n")
//
//                                if chat_id == 5440773190:
//                                    # ⭐ Якщо текст містить фрагмент → записати тільки ID
//                                    if (TEXT_Warning in text or TEXT_Error_2 in text or TEXT_Second_Function in text) and "result" in result:
//                                        msg_id = result["result"]["message_id"]
//
//                                        with open(FILE_PATH_MY, "a", encoding= "utf-8") as f:
//                                            f.write(f"{msg_id}\n")
//
//                        except Exception as e:
//                            print(f"❌ Помилка при відправці повідомлення: {e}")
//
//
//async def send_TG_Messages_Save_its_ID(text, silent: bool):
//    async with aiohttp.ClientSession() as session:
//        for chat_id in CHAT_IDS:
//            
//            payload = {
//                'chat_id': chat_id,
//                'text': text,
//                'parse_mode': 'HTML',
//                'disable_notification': silent   # 🔇 True = без звуку,  False = зі звуком
//            }
//        
//            try:
//                async with session.post(url, data= payload) as response:
//                    result = await response.json()
//        
//                    if chat_id == 5112832505:
//                        # ⭐ Записувати ВСІ message_id
//                        if "result" in result:
//                            msg_id = result["result"]["message_id"]
//
//                            with open(FILE_PATH_Papa, "a", encoding= "utf-8") as f:
//                                f.write(f"{msg_id}\n")
//        
//                    if chat_id == 5440773190:
//                        # ⭐ Записувати ВСІ message_id
//                        if "result" in result:
//                            msg_id = result["result"]["message_id"]
//
//                            with open(FILE_PATH_MY, "a", encoding= "utf-8") as f:
//                                f.write(f"{msg_id}\n")
//        
//            except Exception as e:
//                global_Variables.allTextforSending.append(f"❌ Помилка при відправці повідомлення: {e}")
//                print(f"❌ Помилка при відправці повідомлення: {e}")
//
//
//async def delete_Telegram_Messages():
//    for chat_id in CHAT_IDS:
//        if chat_id == 5112832505:
//            try:
//                with open(FILE_PATH_Papa, "r", encoding= "utf-8") as f:
//                    ids = [line.strip() for line in f if line.strip()]
//    except FileNotFoundError:
//                print("Файл не знайдено.")
//                return
//
//            if not ids:
//                print("Файл порожній, видаляти нічого.")
//                return
//
//            all_deleted_ok = True   # <-- Флаг, чи всі повідомлення видалені успішно
//
//            async with aiohttp.ClientSession() as session:
//                for msg_id in ids:
//                    delete_url = f"https://api.telegram.org/bot{TOKEN}/deleteMessage"
//                    payload = {
//                        "chat_id": chat_id,
//                        "message_id": int (msg_id)
//}
//
//try:
//                        async with session.post(delete_url, data = payload) as response:
//                            res = await response.json()
//
//                            # Якщо Telegram повернув помилку
//                            if not res.get("ok", False):
//                                print(f"❌ Telegram не видалив {msg_id}: {res}")
//                                all_deleted_ok = False
//                            else:
//                                print(f"✔ Видалено: {msg_id}")
//
//                    except Exception as e:
//                        print(f"❌ Помилка видалення {msg_id}: {e}")
//                        all_deleted_ok = False
//
//            # очищаємо файл лише якщо ВСЕ пройшло успішно
//            if all_deleted_ok:
//                open(FILE_PATH_Papa, "w", encoding = "utf-8").close()
//                print("✅ Файл очищено (всі повідомлення видалені).")
//            else:
//                print("⚠ Деякі повідомлення НЕ були видалені — файл НЕ очищено.")
//
//        if chat_id == 5440773190:
//            try:
//                with open(FILE_PATH_MY, "r", encoding = "utf-8") as f:
//                    ids = [line.strip() for line in f if line.strip()]
//            except FileNotFoundError:
//                print("Файл не знайдено.")
//                return
//
//            if not ids:
//    print("Файл порожній, видаляти нічого.")
//                return
//
//            all_deleted_ok = True   # <-- Флаг, чи всі повідомлення видалені успішно
//
//            async with aiohttp.ClientSession() as session:
//                for msg_id in ids:
//                    delete_url = f"https://api.telegram.org/bot{TOKEN}/deleteMessage"
//                    payload = {
//    "chat_id": chat_id,
//                        "message_id": int(msg_id)
//                    }
//
//try:
//                        async with session.post(delete_url, data = payload) as response:
//                            res = await response.json()
//
//                            # Якщо Telegram повернув помилку
//                            if not res.get("ok", False):
//                                print(f"❌ Telegram не видалив {msg_id}: {res}")
//                                all_deleted_ok = False
//                            else:
//                                print(f"✔ Видалено: {msg_id}")
//
//                    except Exception as e:
//                        print(f"❌ Помилка видалення {msg_id}: {e}")
//                        all_deleted_ok = False
//
//            # очищаємо файл лише якщо ВСЕ пройшло успішно
//            if all_deleted_ok:
//                open(FILE_PATH_MY, "w", encoding = "utf-8").close()
//                print("✅ Файл очищено (всі повідомлення видалені).")
//            else:
//                print("⚠ Деякі повідомлення НЕ були видалені — файл НЕ очищено.")
#endregion





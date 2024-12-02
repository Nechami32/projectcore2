using OurApi.Models;
using OurApi.Interfaces;
using System.Text.Json;
using System;

namespace OurApi.Services
{
    public class GlassesService : Iglass
    {
        List<Glasse> glasses ; // sửa đổi từ "users" thành "glasses"
        int nextId = 3;
        private string fileName;

        public GlassesService()
        {
            fileName = Path.Combine("data", "glasses.json");
            using (var jsonFile = File.OpenText(fileName))
            {
                glasses = JsonSerializer.Deserialize<List<Glasse>>(jsonFile.ReadToEnd(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            
        }
        private void saveToFile()
        {

           File.WriteAllText(fileName, JsonSerializer.Serialize(glasses));
        }
        public List<Glasse> GetAll(int userId) => glasses.FindAll(t=>t.UserId==userId); // מחזיר את כל המשקפיים

        public Glasse Get(int id) => glasses.FirstOrDefault(g => g.id == id); // מחזיר משקפיים לפי ID

        public int Add(Glasse glass,int userId) // הוספת משקפיים
        {
            glass.id = nextId++;
            glass.UserId=userId;
            glasses.Add(glass);
            saveToFile();
            return glass.id;
        }
 
        public void Delete(int id) // מחיקת משקפיים
        {
            var glass = Get(id);
            if (glass is null)
                return;

            glasses.Remove(glass);
            saveToFile();
        }

        public void Update(Glasse g,int userId) // עדכון משקפיים
        {
            var index = glasses.FindIndex(gl => gl.id == g.id);
            if (index == -1)
                return;
        g.UserId=userId;

            glasses[index] = g;
            saveToFile();
        }


         public int Count => glasses.Count; // מחזיר את מספר המשקפיים
    }
}


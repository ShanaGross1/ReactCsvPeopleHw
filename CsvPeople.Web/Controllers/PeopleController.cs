using CsvHelper;
using Faker;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CsvPeople.Data;
using CsvPeople.Web.ViewModels;
using System.Buffers.Text;
using System.Text;
using CsvHelper.Configuration;
using System.Globalization;

namespace CsvPeople.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private string _connectionString;
        public PeopleController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("ConStr");
        }

        [HttpGet("getpeople")]
        public List<Person> GetPeople()
        {
            var repo = new PeopleRepository(_connectionString);
            return repo.GetPeople();
        }

        [HttpPost("deleteall")]
        public void DeleteAll()
        {
            var repo = new PeopleRepository(_connectionString);
            repo.DeleteAll();
        }


        [HttpGet("generate")]
        public IActionResult GeneratePeople(int amount)
        {
            var people = Enumerable.Range(1, amount).Select(_ => new Person
            {
                FirstName = Name.First(),
                LastName = Name.Last(),
                Age = RandomNumber.Next(10, 100),
                Address = Address.StreetAddress(),
                Email = Internet.Email()
            }).ToList();

            var writer = GenerateCSV(people);
            return File(Encoding.UTF8.GetBytes(writer), "text/csv", "people.csv");
        }

        [HttpPost("Upload")]
        public void Upload(UploadViewModel viewModel)
        {
            int indexOfComma = viewModel.Base64Data.IndexOf(',');
            string base64 = viewModel.Base64Data.Substring(indexOfComma + 1);
            byte[] bytes = Convert.FromBase64String(base64);
            var people = ParseCsv(bytes);

            var repo = new PeopleRepository(_connectionString);
            repo.AddPeople(people);
        }

        private string GenerateCSV(List<Person> people)
        {
            var writer = new StringWriter();
            var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
            csvWriter.WriteRecords(people);
            return writer.ToString();
        }

        private List<Person> ParseCsv(byte[] bytes)
        {
            using var memoryStream = new MemoryStream(bytes);
            using var reader = new StreamReader(memoryStream);
            using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);
            return csvReader.GetRecords<Person>().ToList();
        }
    }
}

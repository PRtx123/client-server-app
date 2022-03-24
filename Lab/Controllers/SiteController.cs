using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Lab.Controllers
{
    public class SiteController : Controller
    {
        private BookRepository _bookRepository;

        public SiteController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet]
        public IActionResult StaticPage()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Mission()
        {
            return View();
        }

        [HttpPost]
        public string GetGreatestCommonDivisor(string stringNumbers)
        {
            string[] stringNumbersMassive = stringNumbers.Split(new char[] { ' ' });
            var numbers = new int[stringNumbersMassive.Length];
            for (int i = 0; i < stringNumbersMassive.Length; i++)
            {
                numbers[i] = Convert.ToInt32(stringNumbersMassive[i]);
            }
            int firstNumber = numbers[0];
            int secondNumber = numbers[1];
            while (firstNumber != 0 && secondNumber != 0)
            {
                if (firstNumber > secondNumber)
                {
                    firstNumber = firstNumber % secondNumber;
                }
                else
                {
                    secondNumber = secondNumber % firstNumber;
                }
            }
            return (firstNumber + secondNumber).ToString();
        }

        [HttpGet]
        public IActionResult Books()
        {
            return View(_bookRepository.GetBooks());
        }

        [HttpPost]
        public async Task<int> AddBook(string name, string author, string genre)
        {
            return await _bookRepository.AddBookAsync(name, author, genre);
        }

        [HttpPost]
        public async Task<IActionResult> EditBook(int currentId, string name, string author, string genre)
        {
            await _bookRepository.UpdateBookAsync(currentId, name, author, genre);
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteBook(int id)
        {
            await _bookRepository.DeleteBookAsync(id);
            return StatusCode(200);
        }
    }
}
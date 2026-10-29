
using System;
using System.Text;
using System.Threading.Tasks;
using MovieDB.Data;
using MovieDB.DTOs;
using MovieDB.Services;

namespace MovieDB
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            using var context = new MovieDbContext();
            var movieService = new MovieService(context);

            Console.WriteLine("=== MovieDB ===");
            Console.WriteLine("1. ფილმის განახლება");
            Console.WriteLine("2. ფილმის წაშლა");
            Console.Write("აირჩიე მოქმედება: ");

            string? choice = Console.ReadLine();

            if (choice == "1")
            {
                Console.Write("ფილმის ID: ");
                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("არასწორი ID.");
                    return;
                }

                Console.Write("ახალი სათაური: ");
                string title = Console.ReadLine() ?? string.Empty;

                Console.Write("გამოსვლის წელი: ");
                if (!int.TryParse(Console.ReadLine(), out int year))
                {
                    Console.WriteLine("არასწორი წელი.");
                    return;
                }

                Console.Write("Studio ID: ");
                if (!int.TryParse(Console.ReadLine(), out int studioId))
                {
                    Console.WriteLine("არასწორი Studio ID.");
                    return;
                }

                var dto = new UpdateMovieDto
                {
                    Title = title,
                    ReleaseYear = year,
                    StudioId = studioId
                };

                bool updated = await movieService.UpdateMovie(id, dto);

                Console.WriteLine(updated
                    ? "ფილმი წარმატებით განახლდა!"
                    : "ფილმი ვერ მოიძებნა.");
            }
            else if (choice == "2")
            {
                Console.Write("წასაშლელი ფილმის ID: ");

                if (!int.TryParse(Console.ReadLine(), out int id))
                {
                    Console.WriteLine("არასწორი ID.");
                    return;
                }

                Console.Write("დარწმუნებული ხარ, რომ გსურს წაშლა? (კი/არა): ");
                string? confirmation = Console.ReadLine();

                if (confirmation?.Trim().Equals(
                    "კი", StringComparison.OrdinalIgnoreCase) == true)
                {
                    bool deleted = await movieService.DeleteMovie(id);

                    Console.WriteLine(deleted
                        ? "ფილმი წარმატებით წაიშალა!"
                        : "ფილმი ვერ მოიძებნა.");
                }
                else
                {
                    Console.WriteLine("წაშლა გაუქმებულია.");
                }
            }
            else
            {
                Console.WriteLine("არასწორი არჩევანი.");
            }

            Console.WriteLine("პროგრამის დასასრულებლად დააჭირე ნებისმიერ ღილაკს...");
            Console.ReadKey();
        }
    }
}

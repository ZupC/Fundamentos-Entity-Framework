using System.Security.Cryptography.X509Certificates;
using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Bloh
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var context = new BlogDataContext())
            {
                // var user = new User
                // {
                //     Name = "User Teste",
                //     Email = "user@teste.com",
                //     PasswordHash = "password123",
                //     Bio = "Bio do usuário",
                //     Image = "imagem_user.jpg",
                //     Slug = "user-teste"
                // };

                // var category = new Category
                // {
                //     Name = "Category Teste",
                //     Slug = "category-teste"
                // };

                // var post = new Post
                // {
                //     Author = user,
                //     Category = category,
                //     Body = "<p>Hello world</p>",
                //     Slug = "comecando com EF Core",
                //     Summary = "Resumo do post",
                //     Title = "Começando com Entity Framework Core",
                //     CreateDate = DateTime.Now,
                //     LastUpdateDate = DateTime.Now
                // };

                // context.Posts.Add(post);
                // context.SaveChanges();

                var posts = context.Posts
                    // .AsNoTracking()
                    .Include(x => x.Author)
                    .Include(x => x.Category)
                    .Where(x => x.AuthorId == 1)
                    .OrderByDescending(x => x.LastUpdateDate)
                    .ToList();

                foreach (var post in posts)
                {
                    Console.WriteLine($"{post.Title} escrito por {post.Author?.Name} em {post.Category?.Name}");
                }
            }
        }
    }
}
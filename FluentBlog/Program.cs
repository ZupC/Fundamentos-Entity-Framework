using Blog.Data;
using Blog.Models;
using Microsoft.EntityFrameworkCore;

namespace Blog
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var context = new BlogDataContext();
            #region post simples
            var user = new User
            {
                Bio = "Teste",
                Email = "teste@teste.com",
                Image = "https://google.com",
                Name = "Teste",
                PasswordHash = "1234",
                Slug = "Cairo-zupirolli",
                GitHub = "Cairo-Zupirolli"
            };

            context.Users.Add(user);
            context.SaveChanges();
            #endregion

            #region Performance e outros
                #region AsNoTracking
            // var post = context.Posts.FirstOrDefault(x => x.Id == 1);
            // var posts = context.Posts.AsNoTracking();// o uso do asNoTracking remove os metadados da query a deixando mais rapida, é bom para se usar com querys de visualização, onde não precisa editar nada no banco
            #endregion

                #region async await
            // var post = await context.Posts.ToListAsync();
            // var tags = await context.Users.ToListAsync();

            // var posts = await GetPosts(context);
            #endregion

                #region Lazy loading VS Eager Loading
                    // o lazy loading para funcionar precisamos mudar os atributos do model para virtual ai ele vai poder ser sobrescrito, porem não é muito otimizado
                    // pois ele realizara multiplos selects inves de somente 1 select com inner join

                    // já o Eager loading é basicamente o uso do include na hora de realizar a chamada, assim o EF já irá montar uma query com o inner join
                    // var posts = context.Posts.Include(x => x.Tags);
                    #endregion

                #region Skip, Take e Paginação de dados
                // skip e take é basicamente um paginacação na hora da busca dos dados via select, onde a gente seleciona o tanto de registros que trazemos

                //    var posts = GetPostsSkipTake(context);
                //    posts = GetPostsSkipTake(context, 25, 50);
                #endregion

                #region ThenInclude
                //É bom evitar usar o then Include pois ele faz um subselect que não é otimizado
                // melhor usar uma querie manual

                    // var posts = context.Posts
                    //             .Include(x => x.Author)
                    //                 .ThenInclude(x => x.Roles)
                    //             .Include(x => x.Category);
                #endregion

                #region Mapeando queries puras e views
                    // para fazer isso a gente tem que criar um model somente para essa query
                    // adicionar ele no dataContext 
                    // e tambem adicionar lá dentro do modelBuilder o sql necessario

                    // var posts = context.PostWithTagsCounts.ToList();
                #endregion
            #endregion

            Console.WriteLine("Teste");
        }

        // public static async Task<List<Post>> GetPosts(BlogDataContext context)
        // {
        //     return await context.Posts.ToListAsync();
        // }

        // public static List<Post> GetPostsSkipTake(BlogDataContext context, int skip = 0, int take = 25)
        // {
        //     var posts = context
        //                 .Posts
        //                 .AsNoTracking()
        //                 .Skip(skip)
        //                 .Take(take)
        //                 .ToList();
        //     return posts;
        // }
    }
}
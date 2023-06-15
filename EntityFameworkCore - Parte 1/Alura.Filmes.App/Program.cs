using System;
using System.Linq;
using Alura.Filmes.App.Dados;
using Alura.Filmes.App.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Alura.Filmes.App
{
    class Program
    {
        static void Main(string[] args)
        {
            AtoresPorFilme();
            FilmesPorCategoria();
            FilmesPorIdioma();
        }

        public static void AtoresPorFilme()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                contexto.LogSQLToConsole();

                var filme = contexto.Filmes.Include(f => f.Atores).ThenInclude(fa => fa.Ator).First();

                Console.WriteLine(filme);
                Console.WriteLine("Elenco: ");

                foreach (var ator in filme.Atores)
                {
                    Console.WriteLine(ator.Ator);
                }
            }
        }

        public static void FilmesPorCategoria()
        { 
            using (var contexto = new AluraFilmesContexto())
            {
                var categorias = contexto.Categorias.Include(c => c.Filmes).ThenInclude(fc => fc.Filme);

                foreach (var c in categorias)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Filmes da categoria {c}: ");
                    foreach (var fc in c.Filmes)
                    {
                        Console.WriteLine(fc.Filme);
                    }
                }
            }
        }

        public static void FilmesPorIdioma()
        {
            using (var contexto = new AluraFilmesContexto())
            {
                var idiomas = contexto.Idiomas.Include(i => i.FilmesFalados);

                foreach (var idioma in idiomas)
                {
                    Console.WriteLine(idioma);

                    foreach (var filme in idioma.FilmesFalados)
                    {
                        Console.WriteLine(filme);
                    }

                    Console.WriteLine("\n");
                }
            }
        }

    }
}
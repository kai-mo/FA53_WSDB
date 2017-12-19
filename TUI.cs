using System;
using System.Collections.Generic;

namespace GamesManager
{
    public class TUI
    {
        private IBusinessLayer businesslayer;

        public TUI(IBusinessLayer businesslayer)
        {
            this.businesslayer = businesslayer;
            ShowMenu();
        }

        private void ShowMenu()
        {
            Console.Clear();

            Console.Write(
    @"GAMES MANAGER

Show Games          (a)
New Game            (b)
Edit Game           (c)
Delete Game         (d)
-----------------------
Show Developers     (e)
New Developer       (f)
Edit Developer      (g)
Delete Developer    (h)
-----------------------
Show Assignments    (i)
-----------------------
Exit                (q)

Choose an option: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "a": ShowAllGames(); break;
                case "b": AddGame(); break;
                case "c": EditGame(); break;
                case "d": DeleteGame(); break;
                case "e": ShowAllDevelopers(); break;
                case "f": AddDeveloper(); break;
                case "g": EditDeveloper(); break;
                case "h": DeleteDeveloper(); break;
                case "i": ShowAssignments(); break;
                case "q": return;
                default: break;
            }

            ShowMenu();
        }

        private void ShowAllGames()
        {
            Console.Clear();

            foreach (Game game in businesslayer.GetGames())
            {
                Console.WriteLine("- " + game.Name);
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }

        private void ShowAllDevelopers()
        {
            Console.Clear();

            foreach (Developer developer in businesslayer.GetDevelopers())
            {
                Console.WriteLine("- " + developer.Name);
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }

        private void AddGame()
        {
            string new_game, input;
            int choice;
            List<Developer> developers = businesslayer.GetDevelopers();

            do
            {
                Console.Clear();
                Console.Write("Enter name of new game or q to abort: ");
                new_game = Console.ReadLine();
            } while (new_game.Equals(""));

            if (new_game.Equals("q"))
            {
                return;
            }

            do
            {
                int i = 1;

                Console.Clear();
                Console.WriteLine("Choose developer:\n");

                foreach (Developer developer in developers)
                {
                    Console.WriteLine(i++ + ". " + developer.Name);
                }

                Console.Write("\nChoice [q to abort]: ");
                input = Console.ReadLine();

                if (input.Equals("q"))
                {
                    return;
                }
            } while (!Int32.TryParse(input, out choice) || choice < 1 || choice > developers.Count);

            businesslayer.AddGame(new_game, developers[choice - 1].Name);
        }

        private void AddDeveloper()
        {
            string new_developer;

            do
            {
                Console.Clear();
                Console.Write("Enter name of new developer or q to abort: ");
                new_developer = Console.ReadLine();
            } while (new_developer.Equals(""));

            if (!new_developer.Equals("q"))
            {
                businesslayer.AddDeveloper(new_developer);
            }
        }

        private void EditGame()
        {
            List<Game> games = businesslayer.GetGames();
            int index;
            string new_name;
            string old_name;

            Console.Clear();

            for (int i = 1; i <= games.Count; i++)
            {
                Console.WriteLine(i.ToString() + ". " + games[i - 1].Name);
            }

            do
            {
                Console.Write("\nChoose number of game: ");
            } while (!Int32.TryParse(Console.ReadLine(), out index) ||
                    index > games.Count || index < 1);


            Console.Write("Enter new name for '" + games[index - 1].Name + "': ");
            old_name = games[index - 1].Name;
            new_name = Console.ReadLine();

            businesslayer.EditGame(new_name, old_name);
        }

        private void EditDeveloper()
        {
            List<Developer> developers = businesslayer.GetDevelopers();
            int index;
            string new_name;
            string old_name;

            Console.Clear();

            for (int i = 1; i <= developers.Count; i++)
            {
                Console.WriteLine(i.ToString() + ". " + developers[i - 1].Name);
            }

            do
            {
                Console.Write("\nChoose number of developer: ");
            } while (!Int32.TryParse(Console.ReadLine(), out index) ||
                     index > developers.Count || index < 1);


            Console.Write("Enter new name for '" + developers[index - 1].Name + "': ");
            old_name = developers[index - 1].Name;
            new_name = Console.ReadLine();

            businesslayer.EditDeveloper(new_name, old_name);
        }

        private void DeleteDeveloper()
        {
            List<Developer> developers = businesslayer.GetDevelopers();
            int index;

            Console.Clear();

            for (int i = 1; i <= developers.Count; i++)
            {
                Console.WriteLine(i.ToString() + ". " + developers[i - 1].Name);
            }

            do
            {
                Console.Write("\nChoose number of developer: ");
                index = Int32.Parse(Console.ReadLine()) - 1;
            } while (index >= developers.Count || index < 0);

            businesslayer.DeleteDeveloper(developers[index].Name, true);
        }

        private void DeleteGame()
        {
            List<Game> games = businesslayer.GetGames();
            int index;

            Console.Clear();

            for (int i = 1; i <= games.Count; i++)
            {
                Console.WriteLine(i.ToString() + ". " + games[i - 1].Name);
            }

            do
            {
                Console.Write("\nChoose number of game: ");
                index = Int32.Parse(Console.ReadLine()) - 1;
            } while (index >= games.Count || index < 0);

            businesslayer.DeleteGame(games[index].Name);
        }

        private void ShowAssignments()
        {
            List<Developer> developers = businesslayer.GetDevelopers();

            Console.Clear();

            foreach (Developer developer in developers)
            {
                Console.WriteLine(developer.Name + ":");

                foreach (Game game in developer.Games)
                {
                    Console.WriteLine("\t" + game.Name);
                }

                Console.WriteLine();
            }

            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey();
        }
    }
}
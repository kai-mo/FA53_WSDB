interface IDataAccess
{
    void add(Game game);

    void add(Developer developer);

    void edit(Game game);

    void edit(Developer developer);

    void delete(Game game);

    void delete(Developer developer);

    Game[] getGames();

    Developer[] getDevelopers();
}

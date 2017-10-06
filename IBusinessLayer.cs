interface IBusinessLayer
{
    void addGame(string name);

    void addDeveloper(string name);

    void editGame(string name);

    void editDeveloper(string name);

    void deleteGame(string name);

    void deleteDeveloper(string name);

    Game[] getGames();

    Developer[] getDevelopers();
}

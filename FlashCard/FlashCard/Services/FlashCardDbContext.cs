namespace FlashCard.Services;

public class FlashCardDbContext : DbContext
{
    public FlashCardDbContext(DbContextOptions<FlashCardDbContext> contextOptions) : base(contextOptions)
    {

    }

    public DbSet<WordDictionary> WordDictionaries { get; set; }
}


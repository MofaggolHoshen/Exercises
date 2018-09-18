using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace UnitTest
{
    class Class1
    {
    }

    public class Author
    {
        public int AuthorId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Book> Books { get; set; }
    }

    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public int AuthorFK { get; set; }
    }

    //public class Book
    //{
    //    public int BookId { get; set; }
    //    public string Title { get; set; }
    //    public Author Author { get; set; }
    //    [ForeignKey("Author")]
    //    public int AuthorFK { get; set; }
    //}
    //public class Book
    //{
    //    public int BookId { get; set; }
    //    public string Title { get; set; }
    //    [ForeignKey("AuthorFK")]
    //    public Author Author { get; set; }
    //    public int AuthorFK { get; set; }
    //}

    //public class Author
    //{
    //    public int AuthorId { get; set; }
    //    public string FirstName { get; set; }
    //    public string LastName { get; set; }
    //    [ForeignKey("AuthorFK")]
    //    public ICollection<Book> Books { get; set; }
    //}



}

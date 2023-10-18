using System;
using System.Collections.Generic;

public class Movie
{
    public int MovieId { get; set; }
    public string MovieName { get; set; }
    public string Language { get; set; }

    public Movie(int movieId, string movieName, string language)
    {
        MovieId = movieId;
        MovieName = movieName;
        Language = language;
    }
}

public class Customer
{
    public int CustId { get; set; }
    public DateTime BorrowDate { get; set; }
    public int MovieId { get; set; }
    public DateTime? ReturnDate { get; set; }

    public Customer(int custId, DateTime borrowDate, int movieId)
    {
        CustId = custId;
        BorrowDate = borrowDate;
        MovieId = movieId;
    }

    public void Borrow(List<Movie> availableMovies, List<Customer> borrowedMovies)
    {
        if (ReturnDate != null)
        {
            throw new InvalidOperationException("Movie already borrowed.");
        }

        Movie movie = availableMovies.Find(m => m.MovieId == MovieId);

        if (movie == null)
        {
            throw new InvalidOperationException("Movie not found.");
        }

        availableMovies.Remove(movie);
        borrowedMovies.Add(this);
        ReturnDate = BorrowDate.AddDays(10);
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Movie> availableMovies = new List<Movie>
        {
            new Movie(1, "Movie 1", "English"),
            new Movie(2, "Movie 2", "Spanish"),
            new Movie(3, "Movie 3", "French")
        };

        List<Customer> borrowedMovies = new List<Customer>();

        try
        {
            // Borrow a movie CD
            Customer customer = new Customer(101, DateTime.Now, 1);
            customer.Borrow(availableMovies, borrowedMovies);

            Console.WriteLine("Movie CD Borrowed by Customer: " + customer.CustId);

            // Attempt to borrow the same movie CD again (should throw an exception)
            Customer invalidCustomer = new Customer(102, DateTime.Now, 1);
            invalidCustomer.Borrow(availableMovies, borrowedMovies);
        }
        catch (InvalidOperationException ex)
        {
            Console.WriteLine("Error: " + ex.Message);
        }
    }
}
